using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace lab3
{
    /// <summary>
    /// Тип квартиры
    /// </summary>
    public enum FlatType
    {
        OneRoomed,
        TwoRoomed,
        ThreeRoomed,
        FourRoomed
    }
    /// <summary>
    /// Тип комнаты
    /// </summary>
    public enum BuildingType
    {
        House,
        NonResidentialPremises
    }
    /// <summary>
    /// Здание
    /// </summary>
    [XmlInclude(typeof(House))]
    [XmlInclude(typeof(NonResidentialPremises))]
    public abstract class Building
    {
        /// <summary>
        /// Количество людей
        /// </summary>
        public double HumansCount { set; get; }
        
        /// <summary>
        /// Адресс
        /// </summary>
        public string Address { set; get; }
        
        public abstract BuildingType Type { get; }

        public abstract void ToCountHumans();

        public void Print()
        {
            Console.WriteLine($"{Type.ToString()} / {Address} / {HumansCount}");
        }

    }
    /// <summary>
    /// Жилое здание
    /// </summary>
    public class House : Building
    {

        public readonly BuildingType _type = BuildingType.House;
        
        public override BuildingType Type
        {
            get { return _type; }
        }
        /// <summary>
        /// Квартиры
        /// </summary>
        public List<FlatType> Flats { set; get; }
        
        public override void ToCountHumans()
        {

            int RoomsCount = 0;
            foreach (var flat in Flats)
            {
                RoomsCount += (int)flat + 1;
            }

            HumansCount = Math.Round(RoomsCount * 1.3);
        }
        
        public House(string address, List<FlatType> flats)
        {
            Flats = flats;
            ToCountHumans();
            Address = address;
        }

        public House()
        {
            Flats = new List<FlatType>();
        }
        




    }
    /// <summary>
    /// Нежилое здание
    /// </summary>
    public class NonResidentialPremises : Building
    {
        
        private readonly BuildingType _type = BuildingType.NonResidentialPremises;
        
        public override BuildingType Type
        {
            get { return _type; }
        }
        
        /// <summary>
        /// Площадь
        /// </summary>
        public float Area { set; get; }

        public NonResidentialPremises()
        {
            Area = 0f;
        }
        
        public NonResidentialPremises(string address, float area)
        {
            Area = area;
            ToCountHumans();
            Address = address;
        }

        public override void ToCountHumans()
        {
            HumansCount = Math.Round(Area * 0.2);
        }
    }

    public class ManagementCompany
    {
        private List<Building> _buildings;

        public double HumansCount;
        
        public ManagementCompany()
        {
            _buildings = new List<Building>();
            HumansCount = 0;
        }

        public ManagementCompany(List<Building> buildings)
        {
            _buildings = buildings;
            foreach (var building in _buildings)
            {
                HumansCount += building.HumansCount;
            }
        }

        public void Add(Building building)
        {
            _buildings.Add(building);
            HumansCount += building.HumansCount;
        }
        public void AddRange(List<Building> buildings)
        {
            _buildings.AddRange(buildings);
            HumansCount += buildings.Sum(x => x.HumansCount);
        }
        
        public void Print()
        {
            foreach (var building in _buildings)
            {
                building.Print();
            }
        }

        public void PrintFirtsThreeBuildings()
        {
            if (_buildings.Count < 3)
            {
                Console.WriteLine("Not enough objects");
                return;
            }
            for (var i = 0; i < 3; i++)
            {
                _buildings[i].Print();
            }
        }

        public void PrintLastFourAddresses()
        {
            if (_buildings.Count < 4)
            {
                Console.WriteLine("Not enough objects");
                return;
            }

            for (var i = _buildings.Count - 4; i < _buildings.Count; i++)
            {
                _buildings[i].Print();
            }
        }
        
        public void Sort()
        {
            _buildings = _buildings.OrderBy(a => a.HumansCount)
                .ThenBy(a => (int) a.Type)
                .ThenBy(a => a.Address)
                .ToList();
        }

        public void toXml(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Building>));
            
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "\t";
                XmlWriter writer = XmlWriter.Create(fs, settings);
                serializer.Serialize(writer, _buildings);
            
                Console.WriteLine("Successful");
                writer.Close();
            }
        }

        public static ManagementCompany fromXml(string filename)
        {
            ManagementCompany company = new ManagementCompany();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Building>));

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                List<Building> buildings = serializer.Deserialize(fs) as List<Building>;
                if (buildings != null) company.AddRange(buildings);
                
            }

            return company;
        }

    }
}
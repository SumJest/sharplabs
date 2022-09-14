namespace LabNamespace
{
    internal class Project
    {

        public enum FlatType
        {
            OneRoomed,
            TwoRoomed,
            ThreeRoomed,
            FourRoomed
        }

        public enum BuildingType
        {
            House,
            NonResidentialPremises
        }

        public abstract class Building
        {
            public double HumansCount;

            public string Address;
            
            public abstract BuildingType Type { get; }

            public abstract void ToCountHumans();

            public void Print()
            {
                Console.WriteLine($"{Type.ToString()} / {Address} / {HumansCount}");
            }

        }

        class House : Building
        {

            private readonly BuildingType _type = BuildingType.House;
            
            public override BuildingType Type
            {
                get { return _type; }
            }
            
            private List<FlatType> _flats;
            
            public override void ToCountHumans()
            {

                int RoomsCount = 0;
                foreach (var flat in _flats)
                {
                    RoomsCount += (int)flat + 1;
                }

                HumansCount = RoomsCount * 1.3;
            }
            
            public House(string address, List<FlatType> flats)
            {
                _flats = flats;
                ToCountHumans();
                Address = address;
            }
            




        }

        class NonResidentialPremises : Building
        {
            
            private readonly BuildingType _type = BuildingType.NonResidentialPremises;
            
            public override BuildingType Type
            {
                get { return _type; }
            }
            
            private float _area;
            
            public NonResidentialPremises(string address, float area)
            {
                _area = area;
                ToCountHumans();
                Address = address;
            }

            public override void ToCountHumans()
            {
                HumansCount = _area * 0.2;
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
                _sort();
                foreach (var building in _buildings)
                {
                    HumansCount += building.HumansCount;
                }
            }

            public void Add(Building building)
            {
                _buildings.Add(building);
                HumansCount += building.HumansCount;
                _sort();
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
            
            private void _sort()
            {
                _buildings = _buildings.OrderBy(a => a.HumansCount)
                    .ThenBy(a => (int) a.Type)
                    .ThenBy(a => a.Address)
                    .ToList();
            }

        }

        private static void Main(string[] args)
        {
            
        }
    }
}
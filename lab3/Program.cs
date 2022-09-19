using System.Runtime.InteropServices.ComTypes;

namespace lab3
{
    public class Project
    {


        private static void Main(string[] args)
        {

            ManagementCompany company;
            Console.WriteLine("Please write file name which contains company xml structure (relative path with .xml extension)");
            Console.WriteLine("Or leave path empty to use example");
            Console.WriteLine($"Current path: {Directory.GetCurrentDirectory()}\\");
            Console.Write("Filename: ");
            string filename = Console.ReadLine();
            if (filename == String.Empty)
            {
                company = new ManagementCompany();
                company.Add(new House("Chelyabinsk ul. Smart, d. 178", new List<FlatType>() {FlatType.FourRoomed, FlatType.TwoRoomed, FlatType.OneRoomed}));
                company.Add(new House("Chelyabinsk ul. Smart, d. 95", new List<FlatType>() {FlatType.ThreeRoomed, FlatType.TwoRoomed, FlatType.ThreeRoomed}));
                company.Add(new House("Chelyabinsk ul. Smart, d. 132", new List<FlatType>() {FlatType.TwoRoomed, FlatType.OneRoomed, FlatType.FourRoomed}));
                company.Add(new NonResidentialPremises("Chelyabinsk ul. Lenina, d. 37", 232.6F));
                company.Add(new NonResidentialPremises("Chelyabinsk ul. Smart, d. 178", 45F));
                company.Add(new House("Chelyabinsk ul. Pushkina, d. 95", new List<FlatType>()
                {
                    FlatType.ThreeRoomed, FlatType.TwoRoomed, FlatType.ThreeRoomed,
                    FlatType.FourRoomed, FlatType.FourRoomed, FlatType.ThreeRoomed,
                    FlatType.FourRoomed, FlatType.ThreeRoomed, FlatType.TwoRoomed
                }));
            }
            else
            {
                try
                {
                    company = ManagementCompany.fromXml(filename);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Wrong filename or file content");
                    return;
                }
            }

            while (true)
            {
                // Menu
                Console.WriteLine("\n\n1. Sort\n2. Print all building\n3. Print first 3 buildings\n4. Print last 4 buildings" +
                                  "\n5. Print humans count\n6. Save to file (.xml)\n0. Exit\n\n");
                
                Console.Write("Choose: ");
                string input = Console.ReadLine();
                int choice = 0;
                bool is_int = int.TryParse(input, out choice);
                if (!is_int)
                {
                    Console.WriteLine("Choose right number!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        company.Sort();
                        break;
                    case 2:
                        company.Print();
                        break;
                    case 3:
                        company.PrintFirtsThreeBuildings();
                        break;
                    case 4:
                        company.PrintLastFourAddresses();
                        break;
                    case 5:
                        Console.WriteLine($"\nHumans count: {company.HumansCount}");
                        break;
                    case 6:
                        Console.WriteLine("Please write file name to save company xml structure (relative path with .xml extension)");
                        Console.WriteLine($"Current path: {Directory.GetCurrentDirectory()}\\");
                        Console.Write("Filename: ");
                        string savefilename = Console.ReadLine();
                        try
                        {
                            company.toXml(savefilename);
                            Console.WriteLine($"Company successfully saved to {savefilename}.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error to save to {savefilename}");
                        }
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Choose right number!");
                        break;
                        
                }
            }
        }
            
            
    }
}

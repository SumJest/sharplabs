

namespace lab4
{
    internal class Project
    {

        private static List<Phone> _phones = new List<Phone>();
        
        public static void LinkEvent(string url, string name, int level)
        {
            //Console.WriteLine($"{level} - {url} - {name}");
        }

        public static void PhoneEvent(Phone phone)
        {
            Console.WriteLine($"Phone found: {phone.Number}. In page by link: {phone.Url}");
            _phones.Add(phone);
        }
        
        private static void Main(string[] args)
        {
            Console.Write("Enter url (with last \"/\"): ");
            string url = Console.ReadLine();
            Console.Write("Enter the nesting: ");
            int nesting = 2;
            if (!int.TryParse(Console.ReadLine(), out nesting))
            {
                Console.WriteLine($"Enter NUMBER! Nesting is {nesting}");
            }

            int page_count = 10;
            Console.Write("Enter max page value: ");
            if (!int.TryParse(Console.ReadLine(), out page_count))
            {
                Console.WriteLine($"Enter NUMBER! Max page count is {page_count}");
            }
            
            Handler handler = new Handler(url, nesting, page_count);
            handler.UrlFoundEvent += LinkEvent;
            handler.PhoneFoundEvent += PhoneEvent;
            CSVWriter writer = new CSVWriter("phones.csv");
            handler.Start();
            if (_phones.Count > 0)
            {
                writer.Write(_phones.ToArray());
            }
        }
    }
}


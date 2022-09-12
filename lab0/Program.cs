namespace TestNameSpace
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Hello, " + name + "!");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

    }

}


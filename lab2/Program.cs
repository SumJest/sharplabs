using System;
using System.Linq;

namespace LabNamespace
{

    class String
    {
        private char[] _string;

        public String(string str)
        {
            _string = str.ToCharArray();
        }

        public String(char[] str)
        {
            _string = str;
        }
        
        public override string ToString()
        {
            return new string(_string);
        }
        public static implicit operator String(string str) => new String(str);
        public static explicit operator string(String str) => str.ToString();

        public char this[int i]
        {
            get { return _string[i]; }
            set { _string[i] = value; }
        }
        
        public char[] ToCharArray()
        {
            return _string;
        }

        public int Count()
        {
            return _string.Length;
        }
        
        public static String operator +(String a, object b)
        {
            return new String(a.ToCharArray().Concat(b.ToString().ToCharArray()).ToArray());
        }

        public static bool operator ==(String a, object b)
        {
            return a.ToCharArray().SequenceEqual(b.ToString().ToCharArray());
        }
        
        public static bool operator !=(String a, object b)
        {
            return !a.ToCharArray().SequenceEqual(b.ToString().ToCharArray());
        }
        


    }
    
    
    
    internal class Project
    {


        private static void Main(string[] args)
        {
            
            Console.Write("Enter first string: ");
            String str1 = Console.ReadLine();
            Console.Write("Enter second string: ");
            String str2 = Console.ReadLine();
            
            Console.WriteLine("\n1. +\n2. ==\n3. !=\n4. str1[]\n0. Exit");
            
            while (true)
            {
                Console.Write("\nEnter number: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Result: " + (str1 + str2));
                        break;
                    case 2:
                        Console.WriteLine("Result: " + (str1 == str2));
                        break;
                    case 3:
                        Console.WriteLine("Result: " + (str1 != str2));
                        break;
                    case 4:
                        Console.Write("Enter index of first string: ");
                        int index = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine("\nResult: " + str1[index]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\nWrong index!");
                        }
                    
                        

                        break;
                
                    case 0:
                        return;
                
                    default:
                        Console.WriteLine("Wrong number!");
                        break;
                }
            }
        }
    }
}


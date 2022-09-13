using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;


namespace TestNameSpace
{
    public class Book
    {
        
        public string Name;
        public DateTime Date;
        public string Author;

        public Book(string name, DateTime date, string author)
        {
            Name = name;
            Date = date;
            Author = author;
        }
        // Date format: dd.MM.yyyy
        public Book(string name, string date, string author)
        {
            Name = name;
            Author = author;
            Date = DateTime.Parse(date);

        }

        public override string ToString()
        {
            return $"{Name} / {Author} / {Date.ToString("dd.MM.yyyy")}";
        }
    }

    class HomeLibrary
    {
        public List<Book> _books;

        public HomeLibrary(List<Book> books)
        {
            _books = books;
        }

        public HomeLibrary()
        {
            _books = new List<Book>();
        }

        public void Add(Book book)
        {
            _books.Add(book);
        }

        public void Remove(int index)
        {
            _books.RemoveAt(index);
        }

        public List<Book> FindByName(string name)
        {
            return _books.FindAll(i => i.Name.Contains(name));
        }
        public List<Book> FindByAuthor(string author)
        {
            return _books.FindAll(i => i.Author.Contains(author));
        }
        public List<Book> FindByDate(string date)
        {
            return _books.FindAll(i => i.Date.ToString("dd.MM.yyyy") == date);
        }

        public void Sort(Func<Book, IComparable> f)
        {
            _books = _books.OrderBy(f).ToList();
        }

        public void Print()
        {
            for(int i = 0; i < _books.Count; i++)
            {
                Console.WriteLine($"{i}. {_books[i]}");
            }
        }
        
    }
    
    internal class Program
    {
		
		
		enum UserState { IDLE, FIND_FIELD, FIND_DATA, ADD_NAME, ADD_AUTHOR, ADD_DATE, REMOVE_DATA, SORT_FIELD } 

        private static void Main(string[] args)
        {

	        HomeLibrary library = new HomeLibrary();
            
            library.Add(new Book("First Book", "11.12.2003", "Test A.B"));
            library.Add(new Book("Second Book", "11.10.2006", "Fakhrutdinov R.S"));
            library.Add(new Book("How to be smart", "05.07.2011", "Fakhrutdinov S.R"));
            library.Add(new Book("C# for newbees", "12.02.2022", "God J.H"));
            library.Add(new Book("The path of success", "10.08.2016", "Sobolev N.D"));
            
            
            library.Print();
            
            Console.WriteLine("\n\n");

            /*library.Sort(a => a.Date);
			library.Print();*/
            
            Console.WriteLine("0. Find\n1. Add\n2. Remove\n3. Sort\n4. Print");
            Console.Write("Enter number: ");
			UserState state = UserState.IDLE;
			Dictionary<string, string> data = new Dictionary<string, string>();
			Func<string, List<Book>> findFunc = library.FindByName;
			int choice = 0;
			while (true)
			{

				object input = Console.ReadLine();

				switch (state)
				{
					case UserState.IDLE:
						choice = Convert.ToInt32(input);
						switch (choice)
						{
							case 0:
								state = UserState.FIND_FIELD;
								Console.WriteLine("\n0. ByName\n1. ByAuthor\n2. ByDate");
								Console.Write("Enter number: ");
								break;
							case 1:
								state = UserState.ADD_NAME;
								Console.Write("\nEnter book name: ");
								break;
							case 2:
								state = UserState.REMOVE_DATA;
								Console.Write("\nEnter book index: ");
								break;
							case 3:
								state = UserState.SORT_FIELD;
								Console.WriteLine("\n0. ByName\n1. ByAuthor\n2. ByDate\n");
								Console.Write("Enter number: ");
								break;
							case 4:
								library.Print();
								Console.WriteLine("\n0. Find\n1. Add\n2. Remove\n3. Sort\n4. Print");
								Console.Write("Enter number: ");
								break;
							default:
								Console.WriteLine("Enter pls 0-3");
								break;
						}

						break;
					case UserState.FIND_FIELD:
						choice = Convert.ToInt32(input);

						switch (choice)
						{
							case 0:
								state = UserState.FIND_DATA;
								findFunc = library.FindByName;
								Console.Write("\nEnter name: ");
								break;
							case 1:
								state = UserState.FIND_DATA;
								findFunc = library.FindByAuthor;
								Console.Write("\nEnter author: ");
								break;
							case 2:
								state = UserState.FIND_DATA;
								findFunc = library.FindByDate;
								Console.Write("\nEnter date: ");
								break;
							default:
								Console.WriteLine("Wrong number");
								break;

						}

						break;
					case UserState.FIND_DATA:
						List<Book> books = findFunc(input.ToString());

						if (books.Count == 0)
						{
							Console.WriteLine("No books found");
						}
						else
						{
							Console.WriteLine("\nFound books:");
						}

						for (int i = 0; i < books.Count; i++)
						{
							Console.WriteLine(books[i]);
						}

						state = UserState.IDLE;
						Console.WriteLine("\n0. Find\n1. Add\n2. Remove\n3. Sort\n4. Print");
						Console.Write("Enter number: ");
						break;
					case UserState.ADD_NAME:
						string name = input.ToString();
						data.Add("name", name);
						Console.Write("\nEnter author: ");
						state = UserState.ADD_AUTHOR;
						break;
					case UserState.ADD_AUTHOR:
						string author = input.ToString();
						data.Add("author", author);
						Console.WriteLine("\nEnter creation date in format dd.mm.yyyy: ");
						state = UserState.ADD_DATE;
						break;
					case UserState.ADD_DATE:
						string date = input.ToString();
						library.Add(new Book(data["name"], date, data["author"]));
						state = UserState.IDLE;
						Console.WriteLine("\n0. Find\n1. Add\n2. Remove\n3. Sort\n4. Print");
						Console.Write("Enter number: ");
						break;
					case UserState.REMOVE_DATA:
						int index = Convert.ToInt32(input);
						library.Remove(index);
						state = UserState.IDLE;
						Console.WriteLine("\n0. Find\n1. Add\n2. Remove\n3. Sort\n4. Print");
						Console.Write("Enter number: ");
						break;
					case UserState.SORT_FIELD:
						choice = Convert.ToInt32(input);
						switch (choice)
						{
							case 0:
								library.Sort(a => a.Name);
								break;
							case 1:
								library.Sort(a => a.Author);
								break;
							case 2:
								library.Sort(a => a.Date);
								break;
							default:
								Console.WriteLine("Wrong number");
								break;
						}

						state = UserState.IDLE;
						Console.WriteLine("\n0. Find\n1. Add\n2. Remove\n3. Sort\n4. Print");
						Console.Write("Enter number: ");
						break;

				}

			}

        }

    }

}

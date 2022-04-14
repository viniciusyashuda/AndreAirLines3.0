using System;
using RobotSQL.Service;

namespace RobotSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {


            string option;
            bool flag = true;


            do
            {


                Console.WriteLine("******** Robot Menu ********\n");
                Console.WriteLine("Choose an option: \n");
                Console.WriteLine("|1| Read file and insert data in SQL Server");
                Console.WriteLine("|2| Print all");
                Console.WriteLine("|3| Print an object by it's id");
                Console.WriteLine("|4| Print an object by it's code");
                Console.WriteLine("|5| Compare performance Entity Framework vs Dapper");
                Console.WriteLine("|6| Leave\n");
                Console.Write("Option: ");
                option = Console.ReadLine();


                switch (option)
                {

                    case "1":

                        Console.Clear();

                        foreach (var airport in ReadFileCSV.ReadCSV())
                            new AirportService().Add(airport);

                        Console.WriteLine("Press |SPACE| to continue");
                        Console.ReadKey();

                        Console.Clear();

                        break;

                    case "2":

                        Console.Clear();

                        foreach (var airportSQL in new AirportService().GetAll())
                        {
                            Console.WriteLine(airportSQL);
                        }

                        Console.WriteLine("Press |SPACE| to continue");
                        Console.ReadKey();

                        Console.Clear();

                        break;

                    case "3":

                        Console.Clear();

                        Console.Write("Insert the Id of the object you want to search: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.WriteLine(new AirportService().GetById(id));

                        Console.WriteLine("Press |SPACE| to continue");
                        Console.ReadKey();

                        Console.Clear();

                        break;   
                        
                    case "4":

                        Console.Clear();

                        Console.Write("Insert the Code of the object you want to search: ");
                        string code = Console.ReadLine();

                        Console.WriteLine(new AirportService().GetByCode(code));

                        Console.WriteLine("Press |SPACE| to continue");
                        Console.ReadKey();

                        Console.Clear();

                        break;


                    case "5":

                        Console.Clear();

                        new Dapper_vs_EntityFramework().PerformanceTest();

                        Console.WriteLine("Press |SPACE| to continue");
                        Console.ReadKey();

                        Console.Clear();

                        break;

                    case "6":
                        flag = false;
                        break;

                    default:

                        Console.Clear();

                        Console.WriteLine("Please, enter a valid option!");

                        Console.WriteLine("Press |SPACE| to continue");
                        Console.ReadKey();

                        Console.Clear();

                        break;

                }

            }
            while (flag == true);














        }
    }
}

using System;
using RobotSQL.Service;

namespace RobotSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {

            foreach (var airport in ReadFileCSV.ReadCSV())
                new AirportService().Add(airport);

            foreach (var item in new AirportService().GetAll())
            {
                Console.WriteLine(item);
            }

        }
    }
}

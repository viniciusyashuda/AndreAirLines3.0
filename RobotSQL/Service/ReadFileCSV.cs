using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace RobotSQL.Service
{
    public class ReadFileCSV
    {

        public static List<AirportSQL> ReadCSV()
        {

            var list_airport = new List<AirportSQL>();

            string path = @"C:\Users\Vinícius Yashuda\source\repos\AndreAirLinesMongo\RobotSQL\File\Dados.csv";

            using (var reader = new StreamReader(path))
            {

                var line = reader.ReadLine();

                while (line != null)
                {

                    line = reader.ReadLine();

                    if (line != null)
                    {

                        var values = line.Split(';');

                        list_airport.Add(new AirportSQL(values[0], values[1], values[2], values[3]));

                    }


                }

            }

            return list_airport;

        }

    }
}

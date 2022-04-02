using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataSource
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars("C:\\Users\\vnaw\\Source\\linqfundamentals\\DataSource\\fuel.csv");
            var manufacturers = ProcessManufacturers("C:\\Users\\vnaw\\Source\\linqfundamentals\\DataSource\\manufacturers.csv");

            //var query = cars.OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name);
            
            /*
            //this is the same as before
            var query = from car in cars
                        where car.Manufacturer == "BMW" && car.Year == 2021
                        orderby car.Combined descending, car.Name
                        select car;
            */

            /*
            //use first to get the first result.
            var top = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2021)
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .FirstOrDefault();
            */
            
            //Boolean things. All, Any, Contains
            //var result = cars.All(c => c.Manufacturer == "Ford");

            /*
            //using anonymouse classes we can return a new object with just those properties
            var query = from car in cars
                        where car.Manufacturer == "BMW" && car.Year == 2021
                        orderby car.Combined descending, car.Name
                        select new
                        {
                            car.Manufacturer,
                            car.Name,
                            car.Combined
                        };
            */

            //This is legal syntax too but writing it out isn't right
            //var result = cars.Select(c => new { c.Manufacturer, c.Name, c.Combined });
            
            //Console.WriteLine($"Top Car: {top.Name}: {top.Combined}");
            //Console.WriteLine(result);

            /*
            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name}: {car.Combined}");
            }
            */

            /*
            This is to show what a select many does
            var result = cars.Select(c => c.Name);

            foreach(var name in result)
            {
                foreach(var character in name)
                {
                    Console.WriteLine(character);
                }
            }

            //select many equivalent
            var result = cars.SelectMany(c => c.Name)
                            .OrderBy(c => c);

            foreach(var character in result)
            {
                Console.WriteLine(character);
            }
            */

            /*
            //This is the linq query way to write stuff
            var query = from car in cars
                        join manufacturer in manufacturers 
                            on car.Manufacturer equals manufacturer.Name
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            car.Manufacturer,
                            car.Name,
                            car.Combined,
                            manufacturer.Headquarters
                        };

            //This is the linq extension way to write the same thing
            var query2 = 
                cars.Join(manufacturers,
                        c => c.Manufacturer,
                        m => m.Name, 
                        (c,m) => new
                        {
                            c.Manufacturer,
                            c.Name,
                            c.Combined,
                            m.Headquarters
                        })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);

            //This is similar but instead of limiting your attributes at hte join
            //We have all hte attributes there and then do a select later
            var query3 = 
                cars.Join(manufacturers,
                        c => c.Manufacturer,
                        m => m.Name, 
                        (c,m) => new
                        {
                            Car = c,
                            Manufacturer = m
                        })
                    .OrderByDescending(c => c.Car.Combined)
                    .ThenBy(c => c.Car.Name)
                    .Select(c => new
                        {
                            c.Car.Manufacturer,
                            c.Car.Name,
                            c.Car.Combined,
                            c.Manufacturer.Headquarters
                        });

            //when doing multicolumn joins you have to create new anon objects
            //it is important you also have the same alias names too
            var query4 = from car in cars
                        join manufacturer in manufacturers 
                            on new { car.Manufacturer, car.Year } 
                                equals 
                                new { Manufacturer = manufacturer.Name, manufacturer.Year }
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            car.Manufacturer,
                            car.Name,
                            car.Combined,
                            manufacturer.Headquarters
                        };

            //this is the same as query 4
            var query5 = 
                cars.Join(manufacturers,
                        c => new { c.Manufacturer, c.Year },
                        m => new { Manufacturer = m.Name, m.Year }, 
                        (c,m) => new
                        {
                            c.Manufacturer,
                            c.Name,
                            c.Combined,
                            m.Headquarters
                        })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);


            foreach (var car in query4.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Manufacturer} {car.Name}: {car.Combined}");
            }

            Console.WriteLine();

            foreach (var car in query5.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Manufacturer} {car.Name}: {car.Combined}");
            }
            
            */

            var query6 = 
                from car in cars
                group car by car.Manufacturer.ToUpper() into manufacturer
                orderby manufacturer.Key
                select manufacturer;

            foreach(var group in query6)
            {
                Console.WriteLine(group.Key);
                foreach(var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }

                //results.key is the item you are grouping by
                //results.Count() is the number of items per the Key
                //Console.WriteLine($"{result.Key} has {result.Count()} cars");
            }
            
        }

        private static List<Car> ProcessCars(string path)
        {
            /*
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .Select(Car.ParseFromCsv)
                    .ToList();
            */

            /*
            //this is the same
            var query =
                from line in File.ReadAllLines(path)
                                .Skip(1)
                where line.Length > 1
                select Car.ParseFromCsv(line);

            return query.ToList();
            */

            var query =

                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToCar();

            return query.ToList();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =

                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToManufacturer();

            return query.ToList();
        }
	}
}
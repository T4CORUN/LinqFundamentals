using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            //exampleOne();

            exampleSix();
        }

		public static void exampleSix()
        {          
            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10);

            foreach( var number in numbers)
            {
                Console.WriteLine(number);
            }
        }

		public static void exampleFive()
        {          
            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",      Rating = 8.9f,      Year = 2008 },
                new Movie { Title = "The King's Speech",    Rating = 8.0f,      Year = 2010 },
                new Movie { Title = "Casablanca",           Rating = 8.5f,      Year = 1942 },
                new Movie { Title = "Star Wars V",          Rating = 8.7f,      Year = 1980 }
            };
            
            var query = from movie in movies
                        where movie.Year > 2000
                        orderby movie.Rating descending
                        select movie;
            
            var enumerator = query.GetEnumerator();

            //When you do the orderByDescending, it has to look through all the items first to know which one is actually first
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }

		public static void exampleFour()
        {          
            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",      Rating = 8.9f,      Year = 2008 },
                new Movie { Title = "The King's Speech",    Rating = 8.0f,      Year = 2010 },
                new Movie { Title = "Casablanca",           Rating = 8.5f,      Year = 1942 },
                new Movie { Title = "Star Wars V",          Rating = 8.7f,      Year = 1980 }
            };
            
            var query = movies.Where(m => m.Year > 2000)
                                .OrderByDescending(m => m.Rating);
            
            var enumerator = query.GetEnumerator();

            //When you do the orderByDescending, it has to look through all the items first to know which one is actually first
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }

		public static void exampleThree()
        {          
            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",      Rating = 8.9f,      Year = 2008 },
                new Movie { Title = "The King's Speech",    Rating = 8.0f,      Year = 2010 },
                new Movie { Title = "Casablanca",           Rating = 8.5f,      Year = 1942 },
                new Movie { Title = "Star Wars V",          Rating = 8.7f,      Year = 1980 }
            };
            
            var query = Enumerable.Empty<Movie>();
            
            try
            {
                //when you remove the .ToList() it skips over this so the exception is never caught
                //the query doesn't execute until you hit query.Count() below
                query = movies.Filter2(m => m.Year > 2000);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
            //This count has to produce a concrete result so it scans all of it now
            Console.WriteLine(query.Count());
            var enumerator = query.GetEnumerator();

            //This is deferred execution, so this requires another scan. Thus doubling work
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }


		public static void exampleTwo()
        {          
            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",      Rating = 8.9f,      Year = 2008 },
                new Movie { Title = "The King's Speech",    Rating = 8.0f,      Year = 2010 },
                new Movie { Title = "Casablanca",           Rating = 8.5f,      Year = 1942 },
                new Movie { Title = "Star Wars V",          Rating = 8.7f,      Year = 1980 }
            };
            
            //it will skip this line
            var query = movies.Filter2(m => m.Year > 2000);
            
            query = query.Take(1);

            //it will skip this item
            var enumerator = query.GetEnumerator();

            //now it starts stepping through the enumerator and using the Filter2 jumping in and out due to the yield
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }

		public static void exampleOne()
        {          
            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",      Rating = 8.9f,      Year = 2008 },
                new Movie { Title = "The King's Speech",    Rating = 8.0f,      Year = 2010 },
                new Movie { Title = "Casablanca",           Rating = 8.5f,      Year = 1942 },
                new Movie { Title = "Star Wars V",          Rating = 8.7f,      Year = 1980 }
            };
            
            //In our custom MyLinq class it will go through each item.
            //var query = movies.Filter(m => m.Year > 2000);
            
            //The output is different than the above when you do this.
            //var query = movies.Where(m => m.Year > 2000);

            //This uses yield. the output is what linq what is doing. This gives us the behavior Deferred execution
            var query = movies.Filter2(m => m.Year > 2000);

            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
            }
        }
    }

}

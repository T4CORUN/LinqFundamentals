using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Samples.Linq;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //Two_ExtensionExample();
           //Three_LambdaExpression();
           Seven_QueryVsMethod();
        }

		public static void Seven_QueryVsMethod()
        {
            var developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 1, Name = "Chris" },
            };

            var sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            //These are equivalent
            var query = developers.Where(e => e.Name.Length == 5)
                                                .OrderByDescending(e => e.Name)
                                                .Select(e => e).Count();

            var query2 =    from developer in developers  
                            where developer.Name.Length == 5
                            orderby developer.Name descending
                            select developer;

            var cnt = query2.Count();

            //compare to the Five_Dunno example. In this example it is much more readable
            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);
            }

        }

		public static void Six_VarKeyword()
        {
            var developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 1, Name = "Chris" },
            };

            var sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            var query = developers.Where(e => e.Name.Length == 5)
                                                .OrderBy(e => e.Name);

            //compare to the Five_Dunno example. In this example it is much more readable
            foreach (var employee in query)
            {
                Console.WriteLine(employee.Name);
            }

        }

		public static void Five_Dunno()
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 1, Name = "Chris" },
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            foreach (var employee in developers.Where(e => e.Name.Length == 5)
                                                .OrderBy(e => e.Name))
            {
                Console.WriteLine(employee.Name);
            }

        }

        public static void Four_FuncType()
        {
            Func<int, int> square = x => x * x;

            Func<int, int, int> add = ( x, y ) => x + y;

            Func<int, int, int> add2 = ( x, y ) => 
            {
                int temp = x + y;
                return temp;
            };

            Action<int> write = x => Console.WriteLine(x);

            Console.WriteLine(square(add2(3,5)));
            write(square(add2(3,5)));
        }

		public static void Three_LambdaExpression()
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 1, Name = "Chris" },
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };


            //named method approach. Calls a method NameStartsWithS
            //foreach (var employee in developers.Where(NameStartsWithS))
            
            //anonymous method. You don't need an external method
            //foreach (var employee in developers.Where(delegate (Employee employee) { return employee.Name.StartsWith("S"); }))
            
            //Lambda expression. Much simpler syntax. Don't need external method
            foreach (var employee in developers.Where(e => e.Name.StartsWith("S")))
            {
                Console.WriteLine(employee.Name);
            }

        }

		private static bool NameStartsWithS(Employee employee)
		{
            return employee.Name.StartsWith("S");
		}

		public static void Two_ExtensionExample()
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 1, Name = "Chris" },
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            Console.WriteLine(developers.Count());
            IEnumerator<Employee> enumerator = developers.GetEnumerator();

            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }  
        }

        public static void One_IEnumerableExample()
        {
            //this is the old school way
            //He showed us this so we know where it came from
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 1, Name = "Chris" },
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            IEnumerator<Employee> enumerator = developers.GetEnumerator();

            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }
        }
    }
}

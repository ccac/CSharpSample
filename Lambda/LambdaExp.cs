using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class LambdaExp
    {
        static void Main(string[] args)
        {
            Sort();

            Console.WriteLine("========this is a separator of sort method ========");

            ConvertAll();

            Console.WriteLine("========this is a separator of convertall method ========");

            FindAll();

            Console.WriteLine("========this is a separator of findall method ========");

            FindSpecification();
        }

        

        static void Sort()
        {
            List<int> list = new List<int>();
            var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            list.AddRange(numbers);
            
            //.net2.0
            /*
            list.Sort(delegate(int a, int b)
            {
                return a.CompareTo(b);
            }
            );
             */

            // use Lambda
            list.Sort((a, b) => a.CompareTo(b));

            PrintList<int>(list);
        }

        static void ConvertAll()
        {
            List<int> list = new List<int>();
            var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            list.AddRange(numbers);

            //.net2.0
            /*
            List<int> convertlist = list.ConvertAll(delegate(int i)
            {
                return i * 2;
            }
            );
             */

            // use Lambda
            var convertlist = list.ConvertAll(i => i * 2);

            PrintList<int>(convertlist);
        }

        static void FindAll()
        {
            List<int> list = new List<int>();
            var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            list.AddRange(numbers);

            //.net2.0
            /*
            List<int> findlist = list.FindAll(delegate(int i)
            {
                return i < 5;
            }
            );
             */
             

            // use Lambda
            var findlist = list.FindAll(i => i < 5);

            PrintList<int>(findlist);
        }

        static void FindSpecification()
        {
            IList<string> strs = new List<string>();
            strs.Add("hello");
            strs.Add("world");
            strs.Add("hide");

            //.net2.0
            Algorithm.FindSpecification<string>(
                strs,
                Console.WriteLine,
                delegate(string st)
                {
                    return st.Contains('o');
                }
                );

            Console.WriteLine("========this is a separator of findspecification method ========");

            //lambda
            Algorithm.FindSpecification<string>(
                strs,
                str => Console.WriteLine(str),
                str1 => str1.Contains('h')
                    );
        }

        static void PrintList<T>(List<T> list)
        {
            //.net2.0
            //list.ForEach(delegate(T t)
            //{
            //    Console.WriteLine(t);
            //}
            //);

            //lambda
            list.ForEach(t => Console.WriteLine(t));
        }
    }
}

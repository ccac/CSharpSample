using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Algorithm
    {
        public static void FindSpecification<T>(IEnumerable<T> collection,
            Action<T> action, 
            Predicate<T> filter)
        {
            foreach (T t in collection)
            {
                if (filter(t))
                    action(t);
            }
        }
    }
}

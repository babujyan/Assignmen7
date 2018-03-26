using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLINQ;

namespace Assignmen7
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> a = new List<int>();
            for (int i = 100; i > 0; i--)
            {
                a.Add(i);
            }


           
            var b = a.ExtensionOrderBy<int, int>(f => f);

            foreach(int i in b)
            {
                Console.WriteLine(i);
            }

        }
    }
}

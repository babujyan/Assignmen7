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
            for (int j = 100; j > 0; j--)
            {
                for (int i = 100; i > 0; i--)
                {
                    a.Add(i);
                }
            }



            //var b = a.ExtensionGroupBy<int,int>(f => f=1);

            //foreach(var i in b)
            //{
            //    Console.WriteLine(i);
            //}



        }    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    new Thread(Go).Start();
        //    Go();
        //    Console.ReadKey();
        //}

        static void Go()
        {
            for (int i = 0; i < 5; i++)
                Console.Write('P');
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1
{
    class ThreadTest2
    {
        static bool done;

        //static void Main()
        //{
        //    new Thread(Go).Start();
        //    Go();
        //    Console.ReadKey();
        //}

        //static void Go()
        //{
        //    if (!done) { done = true; Console.WriteLine("Done"); }
        //}

        static void Go()
        {
            if (!done)
            {
                Console.WriteLine("Done");
                done = true;
            }
        }
    }
}

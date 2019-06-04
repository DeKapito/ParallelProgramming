using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1
{
    class ThreadSafe
    {
        static bool done;
        static object locker = new object();

        //static void Main()
        //{
        //    new Thread(Go).Start();
        //    Go();
        //    Console.ReadKey();
        //}

        static void Go()
        {
            lock (locker)
            {
                if(!done)
                {
                    Console.WriteLine("Done");
                    done = true;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1
{
    class ThreadTest
    {
        bool done;

        //static void Main()
        //{
        //    ThreadTest tt = new ThreadTest();
        //    new Thread(tt.Go).Start();
        //    tt.Go();
        //    Console.ReadKey();
        //}

        void Go()
        {
            if (!done) { done = true; Console.WriteLine("Done"); }
        }
    }
}

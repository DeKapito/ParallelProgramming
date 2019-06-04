using System;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static int CheckRows(ref int[,] input, int start, int end)
        {
            int n = input.GetLength(1);
            int count = 0;
            for (int i = start; i < end; i++)
                for (int j = 0; j < n; j++)
                    if (field[i, j] == 1
                        && ((j > 0 && (input[i, j - 1] == 0))  //Якщо елемент не в першому стовпці і зліва пуста клітинка
                            || (j == 0))                       //Або якщо в першому
                        && ((i > 0 && (input[i - 1, j] == 0))  //Якщо не в першому рядку і зверху є пуста клітинка
                            || (i == 0))                       //Або в першому
                       )
                    {
                        count++;
                    }
            return count;
        }


        static int[,] field = {{0, 0, 0, 1, 1, 0, 0, 1},
                               {1, 1, 0, 1, 1, 0, 0, 1},
                               {1, 1, 0, 1, 1, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 1},
                               {0, 1, 1, 0, 1, 1, 0, 0},
                               {0, 1, 1, 0, 1, 1, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 1},
                               {1, 1, 0, 0, 0, 0, 0, 0}};

        static void Main(string[] args)
        {
            int n = 8;

            Task<int> check1 = new Task<int>(() => CheckRows(ref field, 0, 2));
            Task<int> check2 = new Task<int>(() => CheckRows(ref field, 3, 5));
            Task<int> check3 = new Task<int>(() => CheckRows(ref field, 6, 8));

            check1.Start();
            check2.Start();
            check3.Start();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write("{0} ", field[i, j]);
                }
                Console.WriteLine();
            }

            int count = check1.Result + check2.Result + check3.Result;
            Console.WriteLine("Count: {0}", count);
            Console.ReadKey();
        }
    }
}

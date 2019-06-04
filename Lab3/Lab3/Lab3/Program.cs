using System;

namespace Lab3
{
    class Program
    {
        static double[,] GenerateB(int size)
        {
            double[,] result = new double[size, size];

            int middle = size / 2;

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    if (i <= j)
                    {
                        result[i, j] = 1;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = size - i; j < size; j++)
                {
                    result[i, j] = 0;
                }
            }

            return result;
        }

        static double[,] GenerateA(int size)
        {
            double[,] result = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = Math.Abs(i - j) + 1;
                }
            }

            return result;
        }

        public static double[,,] MultipleOne(double[,] A, double[,] B)
        {
            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);

            int rowsB = B.GetLength(0);
            int colsB = B.GetLength(1);

            if (colsA != rowsB)
            {
                throw new Exception("Size of matrix's wrong");
            }

            double[,,] result = new double[rowsA, colsB, rowsA+1];

            int count = 0;
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    for (int k = 0; k < rowsB; k++)
                    {
                        result[i, j, k+1] = result[i, j, k] + A[i, k] * B[k, j];
                        count += 2;
                    }
                }
            }

            Console.WriteLine("Counts: {0}", count);

            return result;
        }

        public static int counter2 = 0;
        public static void MultipleRecurse(int i, int j, int k, ref double[,] A, ref double[,] B, ref double[,,] result)
        {
            int size = A.GetLength(0);

            if (i < size && j < size && k < size)
            {
                result[i, j, k + 1] = result[i, j, k];
                if (A[i, k] != 0 && B[k, j] != 0)
                {
                    //result[i, j, 4] = A[i, k] * B[k, j];
                    result[i, j, k + 1] = result[i, j, k] + A[i, k] * B[k, j];
                    counter2++;
                }

                MultipleRecurse(i, j, k + 1, ref A, ref B, ref result);
                MultipleRecurse(i + 1, j, k, ref A, ref B, ref result);
                MultipleRecurse(i, j + 1, k, ref A, ref B, ref result);
            }   
        }

        static void ShowMatrix(double[,] matrix, string title = "")
        {
            Console.WriteLine(title);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write("{0}  ", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void ShowMatrix(double[,,] matrix, string title = "")
        {
            int size = matrix.GetLength(0);
            Console.WriteLine(title);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0}  ", matrix[i, j, size]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter n");
            int size = Convert.ToInt32(Console.ReadLine());

            double[,] A = GenerateA(size);
            double[,] B = GenerateB(size);
            ShowMatrix(A, "Matrix A");
            ShowMatrix(B, "Matrix B");
            ShowMatrix(MultipleOne(A, B), "Once assign a variable");

            double[,,] recurseResult = new double[size, size, size+1];
            MultipleRecurse(0, 0, 0, ref A, ref B, ref recurseResult);
            ShowMatrix(recurseResult, "Recursive local");
            Console.WriteLine("Counts: {0}", counter2);
            Console.ReadKey();
        }
    }
}

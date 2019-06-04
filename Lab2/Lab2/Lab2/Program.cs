using System;
using System.Threading;

namespace Lab2
{
    class Matrix
    {
        public int rows { get; set; }
        public int cols { get; set; }

        private double[,] innerMatrix;
        private static Random rand = new Random();

        public Matrix(int rows, int columns = 1)
        {
            this.innerMatrix = new double[rows, columns];
            this.rows = rows;
            this.cols = columns;
        }

        public double this[int row, int column = 0]
        {
            get { return innerMatrix[row, column]; }
            set { innerMatrix[row, column] = value; }
        }

        public void Show(string title = "")
        {
            Console.WriteLine(title);
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    Console.Write("{0,5:f2}  ", this.innerMatrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public Matrix GetTransp()
        {
            Matrix transp = new Matrix(this.cols, this.rows);

            for (int i = 0; i < transp.rows; i++)
            {
                for (int j = 0; j < transp.cols; j++)
                {
                    transp[i, j] = this.innerMatrix[j, i];
                }
            }

            return transp;
        }

        public static Matrix GetRandomMatrix(int rows, int cols)
        {
            Matrix result = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = rand.Next(0, rows + 5);
                }
            }

            return result;
        }

        public static Matrix operator+ (Matrix A, Matrix B)
        {
            if (A.rows != B.rows || A.cols != B.cols)
            {
                throw new Exception();
            }

            int rows = A.rows;
            int cols = A.cols;
            Matrix result = new Matrix(rows, cols);

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    result[i, j] = A[i, j] + B[i, j];
                }
            }

            return result;
        }

        public static Matrix operator* (Matrix A, Matrix B)
        {
            int rowsA = A.rows;
            int colsA = A.cols;

            int rowsB = B.rows;
            int colsB = B.cols;

            if (colsA != rowsB)
            {
                throw new Exception("Size of matrix's wrong");
            }

            Matrix resultMatrix = new Matrix(rowsA, colsB);

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    for (int k = 0; k < rowsB; k++)
                    {
                        resultMatrix[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return resultMatrix;
        }

        public static Matrix operator *(double a, Matrix B)
        {
            int rows = B.rows;
            int cols = B.cols;

            Matrix resultMatrix = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    resultMatrix[i, j] = B[i, j] * a;
                }
            }

            return resultMatrix;
        }
    }

    class Program
    {
        static int n;
        static Matrix b1, c1;
        static Matrix A, A1, A2, B2;
        static Matrix b, y1, y2;
        static Matrix C2;

        static Matrix Y3;

        static Matrix Y3Double;
        static Matrix first, second, third, fourth, result;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter n");
            n = Convert.ToInt32(Console.ReadLine());

            b1 = new Matrix(n);
            c1 = new Matrix(n);

            A = new Matrix(n, n);
            A1 = new Matrix(n, n);
            A2 = new Matrix(n, n);
            B2 = new Matrix(n, n);
            C2 = new Matrix(n, n);
            Y3 = new Matrix(n, n);
            Y3Double = new Matrix(n, n);

            b = new Matrix(n);
            y2 = new Matrix(n);
            y1 = new Matrix(n);
            first = new Matrix(n);
            second = new Matrix(n);
            third = new Matrix(n);
            fourth = new Matrix(n);
            result = new Matrix(n);

            Thread t_setA = new Thread(set_A);
            Thread t_set_b = new Thread(set_b);
            Thread t_set_b1_c1 = new Thread(set_b1_c1);
            
            Thread t_setA1_A2_B2 = new Thread(set_A1_A2_B2);
            Thread t_set_C2 = new Thread(set_C2);
            
            Thread t_get_y1 = new Thread(get_y1);
            Thread t_get_y2 = new Thread(get_y2);
            Thread t_get_Y3 = new Thread(get_Y3);

            Thread t_first = new Thread(get_First);
            Thread t_second = new Thread(get_Second);
            Thread t_third = new Thread(get_Third);
            Thread t_fourth = new Thread(get_Fourth);
            Thread t_result = new Thread(get_Result);

            t_setA.Start();
            t_set_b.Start();
            t_set_b1_c1.Start();

            t_setA1_A2_B2.Start();

            t_setA.Join();
            t_set_b.Join();
            t_get_y1.Start();

            t_set_b1_c1.Join();
            t_setA1_A2_B2.Join();
            t_get_y2.Start();

            t_set_C2.Start();
            t_set_C2.Join();
            t_get_Y3.Start();

            t_first.Start();
            t_first.Join();
            t_second.Start();
            t_second.Join();
            t_third.Start();
            t_third.Join();
            t_fourth.Start();
            t_fourth.Join();

            t_result.Start();

            b1.Show("b1");
            c1.Show("c1");
            A.Show("A");
            A1.Show("A1");
            A2.Show("A2");
            B2.Show("B2");
            C2.Show("C2");
            b.Show("b");

            Console.WriteLine("Results");
            y1.Show("y1");
            y2.Show("y2");
            Y3.Show("Y3");

            result.Show("Result");

            Console.ReadKey();

        }

        static void set_A()
        {
            A = Matrix.GetRandomMatrix(n, n);
        }

        static void set_A1_A2_B2()
        {
            A1 = Matrix.GetRandomMatrix(n, n);
            A2 = Matrix.GetRandomMatrix(n, n);
            B2 = Matrix.GetRandomMatrix(n, n);
        }

        static void set_b()
        {
            for (int i = 0; i < n; i++)
            {
                b[i] = 8.0 / (i + 1);
            }
        }

        static void get_y1()
        {
            y1 = A * b;
        }

        static void set_b1_c1()
        {
            b1 = Matrix.GetRandomMatrix(n, 1);
            c1 = Matrix.GetRandomMatrix(n, 1);
        }

        static void get_y2()
        {
            y2 = A1 * (2*b1 + 3*c1);
        }

        static void set_C2()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C2[i, j] = 1.0 / (i + j + 2);
                }
            }
        }

        static void get_Y3()
        {
            Y3 = A2 * (B2 + (-1 * C2));
        }

        static void get_First()
        {
            double temp = (y1.GetTransp() * Y3 * y1)[0];
            first =  temp * y2 * y2.GetTransp();
        }

        static void get_Second()
        {
            second = Y3 * Y3;
        }

        static void get_Third()
        {
            third = y1 * y2.GetTransp();
        }

        static void get_Fourth()
        {
            double temp = (y2.GetTransp() * Y3 * y2)[0];
            fourth =  temp * y1 + y1;
        }

        static void get_Result()
        {
            result = (first + second + third) * fourth;
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Lab5
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

        public static Matrix GetRandomMatrix(int rows, int cols = 1)
        {
            Matrix result = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = rand.Next(0, rows);
                }
            }

            return result;
        }

        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.rows != B.rows || A.cols != B.cols)
            {
                throw new Exception();
            }

            int rows = A.rows;
            int cols = A.cols;
            Matrix result = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = A[i, j] + B[i, j];
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix A, Matrix B)
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
                        //Thread.Sleep(0);
                    }
                    //Thread.Sleep(0);
                }
                //Thread.Sleep(0);
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
                    //Thread.Sleep(0);
                    //Thread.Sleep(0);
                }
                //Thread.Sleep(0);
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

            Task<Matrix> tGetA = new Task<Matrix>(() => Matrix.GetRandomMatrix(n, n));
            Task<Matrix> tGet_b = new Task<Matrix>(() => get_b(n));
            Task<Matrix> tGet_b1 = new Task<Matrix>(() => Matrix.GetRandomMatrix(n));
            Task<Matrix> tGet_c1 = new Task<Matrix>(() => Matrix.GetRandomMatrix(n));

            Task<Matrix> t_getA1 = new Task<Matrix>(() => Matrix.GetRandomMatrix(n, n));
            Task<Matrix> t_getA2 = new Task<Matrix>(() => Matrix.GetRandomMatrix(n, n));
            Task<Matrix> t_getB2 = new Task<Matrix>(() => Matrix.GetRandomMatrix(n, n));
            Task<Matrix> t_getC2 = new Task<Matrix>(() => getC2(n));

            Task<Matrix> t_get_y1 = new Task<Matrix>(() => get_y1(A, b));
            Task<Matrix> t_get_y2 = new Task<Matrix>(() => get_y2(A1, b1, c1));
            Task<Matrix> t_get_Y3 = new Task<Matrix>(() => getY3(A2, B2, C2));

            Task<Matrix> t_first = new Task<Matrix>(() => getFirst(y1, y2, Y3));
            Task<Matrix> t_second = new Task<Matrix>(() => getSecond(Y3));
            Task<Matrix> t_third = new Task<Matrix>(() => getThird(y1, y2));
            Task<Matrix> t_fourth = new Task<Matrix>(() => getFourth(y1, y2, Y3));
            Task<Matrix> t_result = new Task<Matrix>(() => getResult(first, second, third, fourth));

            tGetA.Start();
            tGet_b.Start();

            tGet_b1.Start();
            tGet_c1.Start();
            t_getA1.Start();
            t_getA2.Start();
            t_getB2.Start();

            A = tGetA.Result;
            b = tGet_b.Result;

            t_get_y1.Start();

            A1 = t_getA1.Result;
            b1 = tGet_b1.Result;
            c1 = tGet_c1.Result;
            t_get_y2.Start();

            t_getC2.Start();

            A2 = t_getA2.Result;
            B2 = t_getB2.Result;
            C2 = t_getC2.Result;
            t_get_Y3.Start();

            y1 = t_get_y1.Result;
            y2 = t_get_y2.Result;
            Y3 = t_get_Y3.Result;
            t_first.Start();
            t_second.Start();
            t_third.Start();
            t_fourth.Start();

            first = t_first.Result;
            second = t_second.Result;
            third = t_third.Result;
            fourth = t_fourth.Result;
            t_result.Start();

            result = t_result.Result;

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

        static Matrix get_b(int n)
        {
            Matrix b = new Matrix(n);
            for (int i = 0; i < n; i++)
            {
                b[i] = 8.0 / (i + 1);
            }

            return b;
        }

        static Matrix get_y1(Matrix A, Matrix b)
        {
            return A * b;
        }

        static Matrix get_y2(Matrix A1, Matrix b1, Matrix c1)
        {
            return A1 * (2 * b1 + 3 * c1);
        }

        static Matrix getC2(int n)
        {
            Matrix C2 = new Matrix(n, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C2[i, j] = 1.0 / (i + j + 2);
                }
            }

            return C2;
        }

        static Matrix getY3(Matrix A2, Matrix B2, Matrix C2)
        {
            return A2 * (B2 + (-1 * C2));
        }

        static Matrix getFirst(Matrix y1, Matrix y2, Matrix Y3)
        {
            double temp = (y1.GetTransp() * Y3 * y1)[0];
            return temp * y2 * y2.GetTransp();
        }

        static Matrix getSecond(Matrix Y3)
        {
            return Y3 * Y3;
        }

        static Matrix getThird(Matrix y1, Matrix y2)
        {
            return y1 * y2.GetTransp();
        }

        static Matrix getFourth(Matrix y1, Matrix y2, Matrix Y3)
        {
            double temp = (y2.GetTransp() * Y3 * y2)[0];
            return temp * y1 + y1;
        }

        static Matrix getResult(Matrix first, Matrix second, Matrix third, Matrix fourth)
        {
            return (first + second + third) * fourth;
        }
    }
}

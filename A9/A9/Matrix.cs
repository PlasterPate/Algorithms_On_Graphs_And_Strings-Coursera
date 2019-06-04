using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    public class Matrix
    {
        private double[,] myMatrix;
        private long MATRIX_SIZE;
        public Matrix(long matrixSize, double[,] matrix)
        {
            MATRIX_SIZE = matrixSize;
            MyMatrix = new double[MATRIX_SIZE, MATRIX_SIZE + 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    MyMatrix[i, j] = matrix[i, j];
                }
            }
        }

        public double[] GaussianAlgorithm()
        {
            double[] result = new double[MATRIX_SIZE];
            MakeUpperTriangular();
            MakeDiagonalMatrix();
            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                result[i] = Round(MyMatrix[i, MATRIX_SIZE]);
            }
            return result;
        }

        private double Round(double number)
        {
            return Math.Round(number * 2) / 2;
        }

        private void MakeUpperTriangular()
        {
            for (int i = 0; i < MATRIX_SIZE - 1; i++)
            {
                FirstPivotReady(i, i);
                var pivot = MyMatrix[i, i];
                Multiply(i, 1 / pivot);
                for (int k = i + 1; k < MATRIX_SIZE; k++)
                {
                    Add(i, k, -1 * MyMatrix[k, i]);
                }
            }
        }

        private void MakeDiagonalMatrix()
        {
            for (int i = (int)MATRIX_SIZE - 1; i > 0 ; i--)
            {
                var pivot = MyMatrix[i, i];
                Multiply(i, 1 / pivot);
                for (int k = i - 1; k >= 0; k--)
                {
                    Add(i, k, -1 * MyMatrix[k, i]);
                }
            }
        }

        private void FirstPivotReady(int row, int col)
        {
            for (int i = row; i < MATRIX_SIZE; i++)
            {
                if (MyMatrix[i, col] != 0)
                {
                    SwapRow(row, i);
                }
            }

        }

        private void Add(int srcRow, int destRow, double multiplier)
        {
            if (srcRow == destRow)
                return;
            for (int j = 0; j < MATRIX_SIZE + 1; j++)
            {
                MyMatrix[destRow, j] += multiplier * MyMatrix[srcRow, j];
            }
        }

        public void Multiply(int srcRow, double multiplier)
        {
            if (multiplier == 0)
                return;
            for (int j = 0; j < MATRIX_SIZE + 1; j++)
            {
                MyMatrix[srcRow, j] *= multiplier;
            }
        }

        private void SwapRow(int srcRow, int destRow)
        {
            if (srcRow == destRow)
                return;
            for (int j = 0; j < MATRIX_SIZE + 1; j++)
            {
                var temp = MyMatrix[srcRow, j];
                MyMatrix[srcRow, j] = MyMatrix[destRow, j];
                MyMatrix[destRow, j] = temp;
            }
        }

        public double[,] MyMatrix { get => myMatrix; set => myMatrix = value; }
    }
}

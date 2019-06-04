using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    public class Matrix2
    {
        public double[,] Matrix;
        public int constCount;
        public int varCount;
        public Matrix2(int c, int v, double[,] matrix1)
        {
            constCount = c;
            varCount = v;
            Matrix = new double[c + 1, c + v + 1];
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                Matrix[i, varCount + i] = 1;
                Matrix[i, varCount + constCount] = matrix1[i, matrix1.GetLength(1) - 1];
                for (int j = 0; j < varCount; j++)
                {
                    Matrix[i, j] = matrix1[i, j];
                }
            }
            for (int j = 0; j < varCount; j++)
            {
                Matrix[constCount, j] *= -1;
            }
        }

        public string Solve()
        {
            double[] answers = FindAnswers();
            if (answers == null)
                return "Infinity";
            if (!CheckAnswers(answers))
                return "No Solution";
            StringBuilder result = new StringBuilder("Bounded Solution\n");
            for (int i = 0; i < answers.Length; i++)
            {
                result.Append($"{Round(answers[i])} ");
            }
            return result.ToString();
        }

        private double Round(double number)
        {
            return Math.Round(number * 2) / 2;
        }

        private bool CheckAnswers(double[] answers)
        {
            for (int i = 0; i < constCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < varCount; j++)
                {
                    sum += Matrix[i, j] * answers[j];
                }
                if (sum > Matrix[i, Matrix.GetLength(1) - 1])
                    return false;
            }
            return true;
        }

        private double[] FindAnswers()
        {
            double[] result = new double[varCount];
            int pivotCol;
            int pivotRow;
            while ((pivotCol = FindPivotCol()) != -1)
            {
                pivotRow = FindPivotRow(pivotCol);
                if (pivotRow == -1)
                    return null;
                double pivot = Matrix[pivotRow, pivotCol];
                Multiply(pivotRow, 1 / pivot);
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    Add(pivotRow, i, -1 * (Matrix[i, pivotCol]));
                }
            }
            for (int j = 0; j < varCount; j++)
            {
                double value = 0;
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    if (Matrix[i, j] == 1 && value == 0)
                        value = Matrix[i, Matrix.GetLength(1) - 1];
                    else if (Matrix[i, j] == 0)
                        continue;
                    else
                    {
                        value = 0;
                        break;
                    }
                }
                result[j] = value;
            }
            return result;
        }

        private int FindPivotCol()
        {
            double min = 0;
            int idx = -1;
            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                if(Math.Round(Matrix[constCount, j], 2) < min)
                {
                    min = Matrix[constCount, j];
                    idx = j;
                }
            }
            return idx;
        }

        private int FindPivotRow(int col)
        {
            double min = double.MaxValue;
            int idx = -1;
            for (int i = 0; i < constCount; i++)
            {
                double divideResult = Matrix[i, Matrix.GetLength(1) - 1] / Matrix[i, col];
                if(divideResult < min && divideResult >= 0)
                {
                    idx = i;
                    min = divideResult;
                }
            }
            return idx;
        }

        private void Add(int srcRow, int destRow, double multiplier)
        {
            if (srcRow == destRow)
                return;
            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                Matrix[destRow, j] += multiplier * Matrix[srcRow, j];
            }
        }

        public void Multiply(int srcRow, double multiplier)
        {
            if (multiplier == 0)
                return;
            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                Matrix[srcRow, j] *= multiplier;
            }
        }
    }
}

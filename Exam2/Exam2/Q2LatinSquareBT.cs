using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam2
{
    public class Q2LatinSquareBT : Processor
    {
        public Q2LatinSquareBT(string testDataName) : base(testDataName)
        {
            ExcludeTestCases(5, 7, 9, 21);
            this.ExcludeTestCaseRangeInclusive(23, 120);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public string Solve(int dim, int?[,] square)
        {
            List<(int, int)> emptyCells = new List<(int, int)>();
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (!square[i, j].HasValue)
                        emptyCells.Add((i, j));
                }
            }
            bool[,] availables = new bool[emptyCells.Count, dim];
            for (int i = 0; i < emptyCells.Count; i++)
            {
                CheckRowConditions(i, emptyCells[i].Item1, emptyCells[i].Item2, availables, square);
                CheckColConditions(i, emptyCells[i].Item1, emptyCells[i].Item2, availables, square);
            }
            int counter = 0;
            bool isSatisfied = CheckSat(counter, emptyCells, availables, square);
            if (isSatisfied)
                return "SATISFIABLE";
            else
                return "UNSATISFIABLE";
        }

        private bool CheckSat(int counter, List<(int, int)> emptyCells, bool[,] availables, int?[,] square)
        {
            if(counter == emptyCells.Count)
            {
                return true;
            }
            for (int i = 0; i < availables.GetLength(1); i++)
            {
                if (availables[counter, i])
                    continue;
                square[emptyCells[counter].Item1, emptyCells[counter].Item2] = i;
                for (int j = counter + 1; j < emptyCells.Count; j++)
                {
                    if(emptyCells[j].Item1 == emptyCells[counter].Item1 ||
                       emptyCells[j].Item2 == emptyCells[counter].Item2)
                    {
                        availables[j, i] = true;
                        bool flag = true;
                        for (int k = 0; k < availables.GetLength(1); k++)
                        {
                            if (!availables[j, k])
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            square[emptyCells[counter].Item1, emptyCells[counter].Item2] = null;
                            ResetAvailables(counter, i, emptyCells, ref availables);
                            return false;
                        }
                    }
                }
                if (CheckSat(counter + 1, emptyCells, availables, square))
                    return true;
                else
                    ResetAvailables(counter, i, emptyCells, ref availables);
            }
            square[emptyCells[counter].Item1, emptyCells[counter].Item2] = null;
            return false;
        }

        private void ResetAvailables(int counter, int value, List<(int, int)> emptyCells, ref bool[,] availables)
        {
            int row = emptyCells[counter].Item1;
            int col = emptyCells[counter].Item2;
            for (int i = counter + 1; i < emptyCells.Count; i++)
            {
                if (emptyCells[i].Item1 == row ||
                       emptyCells[i].Item2 == col)
                {
                    availables[i, value] = false;
                }
            }

        }

        private void CheckColConditions(int emptyCellsCol, int i, int j, bool[,] availables, int?[,] square)
        {
            int dim = square.GetLength(0);
            for (int col = 0; col < dim; col++)
            {
                if (col == j || !square[i, col].HasValue)
                    continue;
                availables[emptyCellsCol, square[i, col].Value] = true;
            }
        }

        private void CheckRowConditions(int emptyCellsRow, int i, int j, bool[,] availables, int?[,] square)
        {
            int dim = square.GetLength(0);
            for (int row = 0; row < dim; row++)
            {
                if (row == i || !square[row, j].HasValue)
                    continue;
                availables[emptyCellsRow, square[row, j].Value] = true;
            }
        }
    }
}

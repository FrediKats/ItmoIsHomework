using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab3
{
    internal static class PotentialDetector
    {
        private static (int, int)[][] _coeffs;
        private static double[][] _plan;

        public static (int i, int j)[] Detect(double[][] plan)
        {
            _coeffs = Tools.CreateArray<(int, int)>(plan.Length, plan[0].Length);
            _plan = plan.CloneArray();
            int group = 0;

            for (int i = 0; i < _coeffs.Length; i++)
            for (int j = 0; j < _coeffs[0].Length; j++)
                if (_coeffs[i][j].Equals((0, 0)))
                {
                    group++;
                    SetRow(group, i);
                    SetColumn(group, j);
                }

            var cells = new (int, int)[group - 1];

            for (int i = 0; i < _coeffs.Length; i++)
            for (int j = 0; j < _coeffs[0].Length; j++)
            {
                if (_coeffs[i][j].Item1 == 1
                    && _coeffs[i][j].Item2 != 1
                    && cells[_coeffs[i][j].Item2 - 2].Equals((0, 0)))
                {
                    cells[_coeffs[i][j].Item2 - 2] = (i, j);
                }
                else if (_coeffs[i][j].Item2 == 1
                         && _coeffs[i][j].Item1 != 1
                         && cells[_coeffs[i][j].Item1 - 2].Equals((0, 0)))
                {
                    cells[_coeffs[i][j].Item1 - 2] = (i, j);
                }
            }

            return cells;
        }

        private static void SetRow(int group, int row)
        {
            for (int i = 0; i < _coeffs[row].Length; i++)
            {
                _coeffs[row][i].Item2 = group;

                if (_plan[row][i] > 0 && _coeffs[row][i].Item1 == 0)
                {
                    SetColumn(group, i);
                }
            }
        }

        private static void SetColumn(int group, int column)
        {
            for (int i = 0; i < _coeffs.Length; i++)
            {
                _coeffs[i][column].Item1 = group;

                if (_plan[i][column] > 0 && _coeffs[i][column].Item2 == 0)
                {
                    SetRow(group, i);
                }
            }
        }
    }
}

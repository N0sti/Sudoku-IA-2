using System.Collections.Generic;
using System.Linq;
using Sudoku.Shared;
using DlxLib;

namespace Sudoku.AlgorithmX
{
    public class AlgorithmXSolver : ISudokuSolver
    {
        private const int NBCONSTRAIN = 9 * 9 * 4;

        // Implémentation requise par l'interface
        public SudokuGrid Solve(SudokuGrid grid)
        {
	        grid = grid; // Stocke la grille donnée
           var matrix = matrixBuilder(grid); // Construit la matrice de contraintes

            // Résolution en utilisant une bibliothèque externe (par exemple, DlxLib)
            var dlx = new Dlx();
            var solutions = dlx.Solve(matrix);

            // Si une solution est trouvée, met à jour la grille
            foreach (var solution in solutions)
            {
                convertSolutionToSudoku(grid, solution.RowIndexes, matrix);
                break; // On ne prend que la première solution trouvée
            }

            return grid; // Retourne la grille résolue
        }

        private int[,] matrixBuilder(SudokuGrid sudoku)
        {
            int nbCaseRemplie = sudoku.Cells.ToJaggedArray().Aggregate(0, (acc, x) => acc + x.Aggregate(0, (a, b) => a + ((b == 0) ? 0 : 1)));
           var matrix = new int[(81 - nbCaseRemplie) * 9 + nbCaseRemplie, NBCONSTRAIN];
            int imatrix = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    imatrix = buildLine(matrix, i, j, sudoku.Cells[i,j], imatrix);
                }
            }

            return matrix;
        }

        private int buildLine(int[,] matrix, int i, int j, int value, int imatrix)
        {
            if (value == 0)
            {
                int RCC = calcRCConstrain(i, j);
                int RNC = calcRNConstrain(i, 1);
                int CNC = calcCNConstrain(j, 1);
                int BNC = calcBNConstrain(i, j, 1);
                int end = imatrix + 9;
                for (; imatrix < end; imatrix++)
                {
                    matrix[imatrix, RCC] = 1;
                    matrix[imatrix, RNC++] = 1;
                    matrix[imatrix, CNC++] = 1;
                    matrix[imatrix, BNC++] = 1;
                }
                return end;
            }
            else
            {
                matrix[imatrix, calcRCConstrain(i, j)] = 1;
                matrix[imatrix, calcRNConstrain(i, value)] = 1;
                matrix[imatrix, calcCNConstrain(j, value)] = 1;
                matrix[imatrix, calcBNConstrain(i, j, value)] = 1;
                return imatrix + 1;
            }
        }

        private int calcRCConstrain(int i, int j) => 9 * i + j;
        private int calcRNConstrain(int i, int value) => 81 + 9 * i + value - 1;
        private int calcCNConstrain(int j, int value) => 162 + 9 * j + value - 1;
        private int calcBNConstrain(int i, int j, int value) => 243 + ((i / 3) * 3 + j / 3) * 9 + value - 1;

        private void convertSolutionToSudoku(SudokuGrid grid, IEnumerable<int> r, int[,] m)
        {
            foreach (int row in r)
            {
                int x = 0, y = 0, nb = 0;
                for (int j = 0; j < 81; j++)
                {
                    if (m[row, j] == 1)
                    {
                        x = j % 9;
                        y = j / 9;
                        break;
                    }
                }
                for (int j = 81; j < 162; j++)
                {
                    if (m[row, j] == 1)
                    {
                        nb = (j - 81) % 9 + 1;
                        break;
                    }
                }
                grid.Cells[y, x] = nb;
            }
        }
    }
}

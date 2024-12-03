using System.Linq;
using Sudoku.Shared;

namespace Sudoku.SolverTechniques
{
    public static class Solver_LockedCandidates
    {
        public static bool Apply(SudokuGrid grid)
        {
            bool changed = false;

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    int[] availableNumbers = grid.GetAvailableNumbers(r, c);
                    if (availableNumbers.Length == 1)
                    {
                        int candidate = availableNumbers[0];
                        if (grid.Cells[r, c] != candidate)
                        {
                            grid.Cells[r, c] = candidate;
                            changed = true;
                        }
                    }
                }
            }

            for (int c = 0; c < 9; c++)
            {
                for (int r = 0; r < 9; r++)
                {
                    int[] availableNumbers = grid.GetAvailableNumbers(r, c);
                    if (availableNumbers.Length == 1)
                    {
                        int candidate = availableNumbers[0];
                        if (grid.Cells[r, c] != candidate)
                        {
                            grid.Cells[r, c] = candidate;
                            changed = true;
                        }
                    }
                }
            }

            for (int box = 0; box < 9; box++)
            {
                int startRow = (box / 3) * 3;
                int startCol = (box % 3) * 3;

                for (int r = startRow; r < startRow + 3; r++)
                {
                    for (int c = startCol; c < startCol + 3; c++)
                    {
                        int[] availableNumbers = grid.GetAvailableNumbers(r, c);
                        if (availableNumbers.Length == 1)
                        {
                            int candidate = availableNumbers[0];
                            if (grid.Cells[r, c] != candidate)
                            {
                                grid.Cells[r, c] = candidate;
                                changed = true;
                            }
                        }
                    }
                }
            }

            return changed;
        }
    }
}

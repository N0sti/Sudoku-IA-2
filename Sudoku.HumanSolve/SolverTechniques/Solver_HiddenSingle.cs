using System.Linq;
using Sudoku.Shared;

namespace Sudoku.SolverTechniques
{
    public static class Solver_HiddenSingle
    {
        public static bool Apply(SudokuGrid grid)
        {
            bool changed = false;

            foreach (var group in SudokuGrid.AllNeighbours)
            {
                for (int digit = 1; digit <= 9; digit++)
                {
                    var potentialCells = group
                        .Where(cell => grid.Cells[cell.row, cell.column] == 0 &&
                                       grid.GetAvailableNumbers(cell.row, cell.column).Contains(digit))
                        .ToList();

                    if (potentialCells.Count == 1)
                    {
                        var (row, column) = potentialCells.First();
                        grid.Cells[row, column] = digit;
                        changed = true;
                    }
                }
            }

            return changed;
        }
    }
}

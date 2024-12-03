using System.Linq;
using Sudoku.Shared;

namespace Sudoku.SolverTechniques
{
    public static class Solver_AvoidableRectangle
    {
        public static bool Apply(SudokuGrid grid)
        {
            bool changed = false;

            // Affiche l'état initial de la grille
            for (int y = 0; y < 9; y++)
            {
            }

            for (int x1 = 0; x1 < 9; x1++)
            {
                for (int x2 = x1 + 1; x2 < 9; x2++)
                {
                    for (int y1 = 0; y1 < 9; y1++)
                    {
                        for (int y2 = y1 + 1; y2 < 9; y2++)
                        {
                            for (int value1 = 1; value1 <= 9; value1++)
                            {
                                for (int value2 = value1 + 1; value2 <= 9; value2++)
                                {
                                    int[] candidates = { value1, value2 };
                                    var cells = new[]
                                    {
                                        (x: x1, y: y1), (x: x1, y: y2),
                                        (x: x2, y: y1), (x: x2, y: y2)
                                    };

                                    foreach (var cell in cells)
                                    {
                                        var availableNumbers = grid.GetAvailableNumbers(cell.x, cell.y);
                                    }

                                   if (cells.Any(cell =>
    grid.Cells[cell.y, cell.x] == 0 && // Vérifie si la cellule est vide
    !candidates.All(candidate =>
        grid.GetAvailableNumbers(cell.x, cell.y).Contains(candidate))))
{
    continue;
}


                                    var filledCells = cells.Where(cell => grid.Cells[cell.y, cell.x] != 0).ToList();
                                    var emptyCells = cells.Where(cell => grid.Cells[cell.y, cell.x] == 0).ToList();


                                    if (filledCells.Count == 3 || filledCells.Count == 2)
                                    {
                                        var targetCells = filledCells.Count == 3 ? emptyCells : cells.ToList();

                                        foreach (var cell in targetCells)
                                        {
                                            // Filtre les candidats en fonction des valeurs déjà présentes
                                            var available = grid.GetAvailableNumbers(cell.x, cell.y);
                                            var filteredCandidates = available.Intersect(candidates).ToArray();


                                            if (filteredCandidates.Length == 1)
                                            {
                                                grid.Cells[cell.y, cell.x] = filteredCandidates[0];
                                                changed = true;
                                            }
                                            else
                                            {
                                            }
                                        }
                                    }

                                    if (changed)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changed;
        }
    }
}

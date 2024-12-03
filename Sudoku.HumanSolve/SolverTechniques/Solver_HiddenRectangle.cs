using System.Linq;
using Sudoku.Shared;

namespace Sudoku.SolverTechniques
{
    public static class Solver_HiddenRectangle
    {
        public static bool Apply(SudokuGrid grid)
        {
            bool changed = false;

            for (int x1 = 0; x1 < 9; x1++)
            {
                var c1 = grid.GetColumn(x1);
                for (int x2 = x1 + 1; x2 < 9; x2++)
                {
                    var c2 = grid.GetColumn(x2);

                    // On parcourt les paires de lignes (y1, y2)
                    for (int y1 = 0; y1 < 9; y1++)
                    {
                        for (int y2 = y1 + 1; y2 < 9; y2++)
                        {
                            // On parcourt les valeurs candidates
                            for (int value1 = 1; value1 <= 9; value1++)
                            {
                                for (int value2 = value1 + 1; value2 <= 9; value2++)
                                {
                                    int[] candidates = { value1, value2 };
                                    var cells = new[]
                                    {
                                        grid.Cells[y1, x1], grid.Cells[y2, x1],
                                        grid.Cells[y1, x2], grid.Cells[y2, x2]
                                    };

                                    // Vérifie si les cellules contiennent toutes les candidates
                                    if (cells.Any(cell => !grid.GetAvailableNumbers(x1, y1).Contains(value1) || !grid.GetAvailableNumbers(x2, y2).Contains(value2)))
                                    {
                                        continue;
                                    }

                                    // Regroupe les cellules par nombre de candidats
                                    var groupedCells = cells.ToLookup(cell => grid.GetAvailableNumbers(x1, y1).Count());
                                    var cellsWithMoreThanTwoCandidates = groupedCells.Where(g => g.Key > 2).SelectMany(g => g);
                                    int gtTwoCount = cellsWithMoreThanTwoCandidates.Count();

                                    // Si le nombre de cellules avec plus de 2 candidats est valide, on effectue l'opération
                                    if (gtTwoCount >= 2 && gtTwoCount <= 3)
                                    {
                                        bool updated = false;

                                        // On examine les cellules avec exactement 2 candidats
                                        foreach (var cell in groupedCells[2])
                                        {
                                            int newX = x1 == cell ? x2 : x1;
                                            int newY = y1 == cell ? y2 : y1;

                                            // Vérifie si la valeur de candidate doit être supprimée
                                            foreach (int candidate in candidates)
                                            {
                                                if (!grid.GetAvailableNumbers(newX, newY).Contains(candidate))
                                                {
                                                    grid.Cells[newX, newY] = candidate;
                                                    updated = true;
                                                }
                                            }
                                        }

                                        // Si une cellule a été mise à jour, on marque le changement
                                        if (updated)
                                        {
                                            // LogAction("Hidden Rectangle", "{0}: {1}", Utils.PrintCells(cells), Utils.PrintCandidates(candidates));
                                            changed = true;
                                        }
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

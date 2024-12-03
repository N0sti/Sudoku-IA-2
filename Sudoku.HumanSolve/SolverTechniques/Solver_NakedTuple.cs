using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Shared;

namespace Sudoku.SolverTechniques
{
    public static class Solver_NakedTuple
    {
        public static bool Apply(SudokuGrid grid)
        {
            bool changed = false;

            for (int tupleSize = 2; tupleSize <= 4; tupleSize++)
            {
                changed |= NakedTuple(grid, tupleSize);
            }

            return changed;
        }

        private static bool NakedTuple(SudokuGrid grid, int tupleSize)
        {
            bool changed = false;

            foreach (var group in SudokuGrid.AllNeighbours)
            {
                changed |= FindAndEliminate(grid, group, tupleSize);
            }

            return changed;
        }

        private static bool FindAndEliminate(SudokuGrid grid, (int row, int column)[] region, int tupleSize)
        {
            bool changed = false;

            var candidateCells = region
                .Where(cell => grid.Cells[cell.row, cell.column] == 0) // Cellules vides
                .Select(cell => new CellCandidates
                {
                    Row = cell.row,
                    Column = cell.column,
                    Candidates = grid.GetAvailableNumbers(cell.row, cell.column)
                })
                .Where(cell => cell.Candidates.Length > 1 && cell.Candidates.Length <= tupleSize)
                .ToList();

            var tuples = GetCombinations(candidateCells, tupleSize)
                .Where(combo => combo.SelectMany(c => c.Candidates).Distinct().Count() <= tupleSize)
                .ToList();

            foreach (var tuple in tuples)
            {
                var involvedPositions = tuple.Select(c => (c.Row, c.Column)).ToArray();
                var involvedCandidates = tuple.SelectMany(c => c.Candidates).Distinct().ToArray();

                foreach (var cell in candidateCells.Where(c => !involvedPositions.Contains((c.Row, c.Column))))
                {
                    foreach (var candidate in involvedCandidates)
                    {
                        if (cell.Candidates.Contains(candidate))
                        {
                            grid.RemoveCandidate(cell.Row, cell.Column, candidate);
                            changed = true;
                        }
                    }
                }
            }

            return changed;
        }

        private static IEnumerable<List<CellCandidates>> GetCombinations(List<CellCandidates> cells, int tupleSize)
        {
            return Enumerable.Range(0, 1 << cells.Count)
                .Select(index => cells.Where((cell, i) => (index & (1 << i)) != 0).ToList())
                .Where(combination => combination.Count == tupleSize);
        }

        private class CellCandidates
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public int[] Candidates { get; set; }
        }
    }
}

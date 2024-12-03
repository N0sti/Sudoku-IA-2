using Sudoku.Shared;

namespace Sudoku.HumanSolving
{
    public class SolverTechnique
    {
        public Func<SudokuGrid, bool> Function { get; }
        public string Url { get; }

        public SolverTechnique(Func<SudokuGrid, bool> function, string url)
        {
            Function = function;
            Url = url;
        }
    }
}

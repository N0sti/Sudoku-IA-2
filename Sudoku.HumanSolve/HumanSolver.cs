using Sudoku.Shared;
using Sudoku.SolverTechniques;

namespace Sudoku.HumanSolving
{
    public partial class HumanSolver : ISudokuSolver
    {
        private static readonly string[] TupleStr = new string[5] { string.Empty, "single", "pair", "triple", "quadruple" };
        private readonly Func<SudokuGrid, bool>[] _techniques;
        private SudokuGrid _grid; // Instance de la grille de Sudoku

        // Constructeur sans paramètre pour le benchmark
        public HumanSolver() 
        {
            _grid = new SudokuGrid(); 
            _techniques = InitSolverTechniques();
        }

        // Constructeur avec paramètre
        public HumanSolver(SudokuGrid grid)
        {
            _grid = grid;
            _techniques = InitSolverTechniques();
        }

        private Func<SudokuGrid, bool>[] InitSolverTechniques()
        {
            return new Func<SudokuGrid, bool>[]
            {
                Solver_HiddenSingle.Apply,
                //Solver_HiddenRectangle.Apply,
                //Solver_NakedTuple.Apply,
                //Solver_XYChain.Apply,
                //Solver_YWing.Apply,
                Solver_LockedCandidates.Apply,
                Solver_AvoidableRectangle.Apply,
            };
        }

        // Implémentation de l'interface ISudokuSolver : méthode Solve
        public SudokuGrid Solve(SudokuGrid s)
        {
            bool solved = false;

            foreach (var technique in _techniques)
            {
                if (technique(s))
                {
                    solved = true;
                }
            }

            return s;
        }
    }
}

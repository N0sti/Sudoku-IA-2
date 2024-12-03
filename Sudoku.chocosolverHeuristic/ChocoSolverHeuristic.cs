using System;
using System.IO;
using Newtonsoft.Json;
using Python.Runtime;
using Sudoku.Shared;

namespace Sudoku.Solvers
{
    public class ChocoSolverHeuristic : PythonSolverBase
    {
        public override SudokuGrid Solve(SudokuGrid sudokuGrid)
        {
            string scriptPath = Path.Combine(Environment.CurrentDirectory, @"PythonChocoSolverHeuristic.py");

            using (PyModule scope = Py.CreateScope())
            {
                // Transmettre la grille à Python
                scope.Set("sudoku_grid", sudokuGrid.Cells);

                // Charger et exécuter le script Python
                string code = File.ReadAllText(scriptPath);
                scope.Exec(code);

                // Récupérer la solution depuis Python
                PyObject result = scope.Get("solution");

                // Convertir la solution NumPy en tableau .NET
                var managedResult = result.As<int[][]>().To2D();

                return new SudokuGrid { Cells = managedResult };
            }
        }

        protected override void InitializePythonComponents()
        {
            // Déclarer les dépendances pip nécessaires
            InstallPipModule("pychoco");
            base.InitializePythonComponents();
        }
    }
}
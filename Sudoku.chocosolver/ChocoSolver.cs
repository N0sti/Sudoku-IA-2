using System;
using System.Diagnostics;
using System.IO;
using System.Resources;
using Newtonsoft.Json;
using Python.Runtime;
using Sudoku.Shared;

namespace Sudoku.Solvers
{
    public class ChocoSolver : PythonSolverBase
	{
        public override SudokuGrid Solve(SudokuGrid sudokuGrid)
        {
            string scriptPath = Path.Combine(Environment.CurrentDirectory, @"PythonChocoSolver.py");

			using (PyModule scope = Py.CreateScope())
			{

				
				// create a Python variable "instance"
				scope.Set("sudoku_grid", sudokuGrid.Cells);

				// run the Python script
				string code = File.ReadAllText(scriptPath);
				scope.Exec(code);

				PyObject result = scope.Get("solution");

				// Convertissez le résultat NumPy en tableau .NET
				var managedResult = result.As < int[][]>().To2D();

				return new SudokuGrid() { Cells = managedResult };
			}
		}

        protected override void InitializePythonComponents()
        {
	        //declare your pip packages here
	        InstallPipModule("pychoco");
	        base.InitializePythonComponents();
        }
	}
}

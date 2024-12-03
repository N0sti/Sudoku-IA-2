using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Shared;
using Decider.Csp.BaseTypes;
using Decider.Csp.Integer;
using Decider.Csp.Global;

namespace Sudoku.CSPSolver
{
    public class CSPSolver : ISudokuSolver
    {
        public SudokuGrid Solve(SudokuGrid s)
        {
            // Create variables for each cell
            var variables = new VariableInteger[9, 9];
            var variableList = new List<VariableInteger>();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (s.Cells[row, col] == 0) // Unassigned cells get the full domain (1-9)
                    {
                        var variable = new VariableInteger($"Cell_{row}_{col}", 1, 9);
                        variables[row, col] = variable;
                        variableList.Add(variable);
                    }
                    else // Assigned cells have a fixed domain
                    {
                        var variable = new VariableInteger($"Cell_{row}_{col}", s.Cells[row, col], s.Cells[row, col]);
                        variables[row, col] = variable;
                        variableList.Add(variable);
                    }
                }
            }

            // Create constraints
            var constraints = new List<IConstraint>();

            // Row constraints: All cells in each row must be distinct
            for (int row = 0; row < 9; row++)
            {
                var rowVariables = Enumerable.Range(0, 9).Select(col => variables[row, col]).ToArray();
                constraints.Add(new AllDifferentInteger(rowVariables));
            }

            // Column constraints: All cells in each column must be distinct
            for (int col = 0; col < 9; col++)
            {
                var colVariables = Enumerable.Range(0, 9).Select(row => variables[row, col]).ToArray();
                constraints.Add(new AllDifferentInteger(colVariables));
            }

            // Block constraints: All cells in each 3x3 block must be distinct
            for (int blockRow = 0; blockRow < 3; blockRow++)
            {
                for (int blockCol = 0; blockCol < 3; blockCol++)
                {
                    var blockVariables = new List<VariableInteger>();
                    for (int row = blockRow * 3; row < blockRow * 3 + 3; row++)
                    {
                        for (int col = blockCol * 3; col < blockCol * 3 + 3; col++)
                        {
                            blockVariables.Add(variables[row, col]);
                        }
                    }
                    constraints.Add(new AllDifferentInteger(blockVariables.ToArray()));
                }
            }

            // Create the CSP state
            var state = new StateInteger(variableList.ToArray(), constraints);

            // Solve the CSP
            if (state.Search() == StateOperationResult.Unsatisfiable)
            {
                throw new ApplicationException("No solution found for the Sudoku puzzle.");
            }

            // Fill the solved grid back into the SudokuGrid structure
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    s.Cells[row, col] = variables[row, col].Value;
                }
            }

            return s;
        }
    }
}

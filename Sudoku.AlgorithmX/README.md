
# Sudoku Solver with Algorithm X

This project implements a Sudoku solver based on Donald Knuth's Algorithm X using the dancing links technique. It consists of a matrix representation of constraints and an efficient backtracking search algorithm to solve puzzles of varying difficulty.

---

## **1. Overview**

### **Objective**
The project provides a robust implementation of the Algorithm X technique for solving Sudoku puzzles. It uses a combination of matrix representation and logical constraint satisfaction to find valid solutions efficiently.

### **Features**
- Implements Algorithm X with dancing links for efficient backtracking.
- Converts Sudoku puzzles into an exact cover matrix for solving.
- Includes utility functions to map solutions back to the Sudoku grid.
- Supports the resolution of both simple and complex Sudoku puzzles.

---

## **2. Code Structure**

### **2.1. Classes**

#### **MatrixList**
- Represents the exact cover matrix for Algorithm X.
- Handles the transformation of Sudoku puzzles into a matrix format that Algorithm X can process.
- Includes methods for covering and uncovering columns during the backtracking process.

#### **AlgorithmXSolver**
- Implements the ISudokuSolver interface.
- Uses the `MatrixList` class to build the constraint matrix.
- Solves the puzzle using an external library for exact cover problems (e.g., DlxLib).

---

### **2.2. Core Functionalities**

#### **Exact Cover Matrix Representation**
- Each constraint (row, column, sub-grid uniqueness) is mapped to a row in the exact cover matrix.
- Each cell in the Sudoku puzzle is represented by a combination of constraints.

#### **Backtracking with Algorithm X**
- Implements a recursive search algorithm with logical constraint satisfaction to find solutions.
- Efficiently covers and uncovers rows and columns during the search process.

#### **Matrix Conversion Utilities**
- Converts the matrix solution back into a Sudoku grid for easy interpretation.

---

## **3. Usage**

### **Solve(SudokuGrid grid)**
- Method implemented in the `AlgorithmXSolver` class.
  - **Input**: A `SudokuGrid` object representing the puzzle.
  - **Output**: The solved `SudokuGrid`.

### **Example**
```csharp
var solver = new AlgorithmXSolver();
SudokuGrid puzzle = /* Initialize your Sudoku puzzle */;
SudokuGrid solution = solver.Solve(puzzle);
```

---

## **4. Limitations**
- May require significant computational resources for highly complex puzzles.
- Dependent on the correctness of the constraint matrix generation.

---

## **5. Future Improvements**
- Optimize the matrix generation process for faster computation.
- Extend the solver to handle non-standard Sudoku variants (e.g., 16x16 grids).

---

## **6. References**
- Donald Knuth, "Dancing Links": [https://www.youtube.com/watch?v=_cR9zDlvP88]
- DlxLib library: [https://github.com/taylorjg/DlxLib]
- Exact cover problem: [https://en.wikipedia.org/wiki/Exact_cover#Sudoku]
- Solving Sudoku efficiently with Dancing Links: [https://www.kth.se/social/files/58861771f276547fe1dbf8d1/HLaestanderMHarrysson_dkand14.pdf]
- Sudoku Exact cover matrix: [https://www.stolaf.edu/people/hansonr/sudoku/exactcovermatrix.htm]

---

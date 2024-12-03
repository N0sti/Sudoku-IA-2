import pychoco

def solve_sudoku(sudoku_grid):
    model = pychoco.Model()

    cells = [[model.intvar(1, 9) for _ in range(9)] for _ in range(9)]

    for i in range(9):
        model.all_different([cells[i][j] for j in range(9)]).post()  # Lignes
        model.all_different([cells[j][i] for j in range(9)]).post()  # Colonnes

    for boxRow in range(0, 9, 3):
        for boxCol in range(0, 9, 3):
            model.all_different([
                cells[row][col]
                for row in range(boxRow, boxRow + 3)
                for col in range(boxCol, boxCol + 3)
            ]).post() 

    for i in range(9):
        for j in range(9):
            if sudoku_grid[i,j] != 0:
                model.arithm(cells[i][j], "=", sudoku_grid[i,j]).post()

    solver = model.get_solver()
    if solver.solve():
        return [[cells[i][j].get_value() for j in range(9)] for i in range(9)]
    else:
        return None

solution = solve_sudoku(sudoku_grid)


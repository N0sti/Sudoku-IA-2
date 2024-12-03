import pychoco

def solve_sudoku_heuristic(sudoku_grid):
    model = pychoco.Model()

    # Créer les variables
    cells = [[model.intvar(1, 9) for _ in range(9)] for _ in range(9)]

    # Contraintes pour les lignes et colonnes
    for i in range(9):
        model.all_different([cells[i][j] for j in range(9)]).post()  # Lignes
        model.all_different([cells[j][i] for j in range(9)]).post()  # Colonnes

    # Contraintes pour les sous-grilles (3x3)
    for boxRow in range(0, 9, 3):
        for boxCol in range(0, 9, 3):
            model.all_different([
                cells[row][col]
                for row in range(boxRow, boxRow + 3)
                for col in range(boxCol, boxCol + 3)
            ]).post()

    # Contraintes pour les valeurs déjà présentes
    for i in range(9):
        for j in range(9):
            if sudoku_grid[i,j] != 0:
                model.arithm(cells[i][j], "=", sudoku_grid[i,j]).post()

    # Configurer le solveur avec une recherche heuristique
    solver = model.get_solver()
    solver.set_default_search()

    # Résolution et extraction des valeurs
    if solver.solve():
        return [[cells[i][j].get_value() for j in range(9)] for i in range(9)]
    else:
        return None
    
solution = solve_sudoku_heuristic(sudoku_grid)
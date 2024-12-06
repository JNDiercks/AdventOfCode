test = False
filePath = './test_input.txt' if test else './input.txt'

directions = [(-1,0), (0,1), (1,0), (0,-1)]
current_direction = 0

def find_char_index(char, grid):
    for row_index, row in enumerate(grid):
        for col_index, element in enumerate(row):
            if element == char:
                return (row_index, col_index)
    return None

def turn90(current_direction):
    return (current_direction + 1)%len(directions)

def nextPosition(position, direction):
  return (position[0]+direction[0], position[1]+direction[1])

def move(grid, position, direction_index):
    next_position = nextPosition(position, directions[direction_index])
    if 0 <= position[0] < y_size-1 and 0 <= position[1] < x_size-1:

      if(grid[next_position[0]][next_position[1]]) == '#':
        direction_index = turn90(direction_index)    
        next_position = nextPosition(position, directions[direction_index])

      grid[next_position[0]][next_position[1]] = 'X'
      return True, next_position, direction_index

    return False, next_position, direction_index
         

with open(filePath, 'r') as f:
  data = f.read()
  data = [list(x) for x in data.split('\n')]
  position = find_char_index('^', data)
  print(position)
  y_size = len(data)
  x_size = len(data[0])
  print("Size: ", y_size, "x", x_size)
  nextMoveSuccessful = True

  while nextMoveSuccessful: 
    nextMoveSuccessful, position, current_direction = move(data, position, current_direction)

  result = [x == 'X' for y in data for x in y ].count(True)
  print("Result: ", result)
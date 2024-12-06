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

def move(grid, position, direction_index, obstical):
    next_position = nextPosition(position, directions[direction_index])
    if 0 <= next_position[0] < y_size and 0 <= next_position[1] < x_size:
      if(grid[next_position[0]][next_position[1]]) == '#' or next_position == obstical:
        direction_index = turn90(direction_index)    
        return True, position, direction_index
      return True, next_position, direction_index
    return False, next_position, direction_index
         

with open(filePath, 'r') as f:
  data = f.read()
  data = [list(x) for x in data.split('\n')]
  start = find_char_index('^', data)
  y_size = len(data)
  x_size = len(data[0])

  obstical_positions = []
  for idy, y in enumerate(data):
    for idx, x in enumerate(y):
      if (idy, idx) is not start and x != '#':
        position = start
        current_direction = 0
        nextMoveSuccessful = True
        seen = set() 
        while nextMoveSuccessful: 
          nextMoveSuccessful, position, current_direction = move(data, position, current_direction, (idy, idx))
          location = (*position, current_direction)
          if location in seen:
            obstical_positions.append((idy, idx))
            break 
          else:
            seen.add((*position, current_direction))

  result = len(obstical_positions)
  print("Result: ", result)
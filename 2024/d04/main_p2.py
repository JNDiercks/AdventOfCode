import re
# 1880

test = False
filePath = './test_input.txt' if test else './input.txt'

def findXMASMatch(grid, index) -> int:
  topleft = (index[0] - 1, index[1] - 1)
  topright = (index[0] - 1, index[1] + 1)
  bottomleft = (index[0] + 1, index[1] - 1)
  bottomright = (index[0] + 1, index[1] + 1)
  up = grid[topleft[0]][topleft[1]]=='M' and grid[topright[0]][topright[1]]=='M' and grid[bottomleft[0]][bottomleft[1]]=='S' and grid[bottomright[0]][bottomright[1]]=='S'
  right = grid[topleft[0]][topleft[1]]=='S' and grid[topright[0]][topright[1]]=='M' and grid[bottomleft[0]][bottomleft[1]]=='S' and grid[bottomright[0]][bottomright[1]]=='M'
  down = grid[topleft[0]][topleft[1]]=='S' and grid[topright[0]][topright[1]]=='S' and grid[bottomleft[0]][bottomleft[1]]=='M' and grid[bottomright[0]][bottomright[1]]=='M'
  left = grid[topleft[0]][topleft[1]]=='M' and grid[topright[0]][topright[1]]=='S' and grid[bottomleft[0]][bottomleft[1]]=='M' and grid[bottomright[0]][bottomright[1]]=='S'
  # print(up or right or down or left)
  print("Count: ", [up,right,down,left].count(True))
  return up or right or down or left

with open(filePath, 'r') as f:
  data = f.read()
  data = [x for x in data.split('\n')]

  y_size = len(data)
  x_size = len(data[0])
  print("Size: ", y_size, "x", x_size)
  matches = 0
  for idy, y in enumerate(data):
    for idx, x in enumerate(y):
      if (data[idy][idx] == 'A' and 0 < idy < len(data)-1 and 0 < idx < len(y)-1):
        # print(idy, idx)
        matches = matches + findXMASMatch(data, (idy, idx))

  print("Result:")
  print(matches)

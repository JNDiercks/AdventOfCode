import re

test = False
filePath = './test_input.txt' if test else './input.txt'

x_directions = [-1, 0, 1]
y_directions = [-1, 0, 1]
directions = [(y,x) for x in x_directions for y in y_directions if not (x == 0 and y == 0)]
print(directions)

def findMatches(grid, index, match) -> int:
  matchCount = 0
  for d in directions:
    isMatch = True
    for idm, m in enumerate(match):
      if not (0 <= index[0] + d[0]*idm < len(grid) and 0 <= index[1] + d[1]*idm < len(grid[0])):
          isMatch = False
          break
      
      if(grid[index[0]+d[0]*idm][index[1]+d[1]*idm] != m):
        isMatch = False
        break

    if (isMatch):
      matchCount = matchCount + 1
  return matchCount

with open(filePath, 'r') as f:
  data = f.read()
  data = [x for x in data.split('\n')]

  y_size = len(data)
  x_size = len(data[0])
  print("Size: ", y_size, "x", x_size)
  find = "XMAS"
  matches = 0
  for idy, y in enumerate(data):
    for idx, x in enumerate(y):
      if (data[idy][idx] == find[0]):
        matches = matches + findMatches(data, (idy,idx), find)

  print("Result:")
  print(matches)

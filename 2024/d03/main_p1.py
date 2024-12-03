import re

test = False
filePath = './test_input.txt' if test else './input.txt'
with open(filePath, 'r') as f:
  data = f.read()
  regex = r"mul\((\d{1,3}),(\d{1,3})\)"
  matches = re.finditer(regex, data, re.MULTILINE)

  matchedGroups = [x.groups() for x in matches]
  result = sum([int(x[0])*int(x[1]) for x in matchedGroups])
  print(result)

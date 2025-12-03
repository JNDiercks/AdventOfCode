import re

test = False
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
  data = f.read()
  data = data.split('\n')
  data = [d for d in data if len(d) > 0]
  print(data)
  
  start = 50
  position = start
  result = 0

  for l in data:
    print(l)
    matches = re.match(r'(\w)(\d+)', l)
    print(matches[1], matches[2])
    operation = matches[1]
    value = int(matches[2])

    if operation == 'L':
      position = position - value
    else: 
      position = position + value
      
    if position == 0 or position % 100 == 0:
      result += 1
      position = 0

  print(result)
  


    # print("Operation: ", l, "Current Position: ", position, "New Position: ", new_position, "Result: ", result)
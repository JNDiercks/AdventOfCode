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
  zero_clicks = 0

  for l in data:
    matches = re.match(r'(\w)(\d+)', l)
    operation = matches[1]
    value = int(matches[2]) * (-1 if operation == 'L' else 1)
    clicks, rotation = divmod(abs(value), 100)
    zero_clicks += clicks

    if operation == 'R':
        if position + rotation >= 100:
            zero_clicks +=1 
    else:
        if position and position - rotation <= 0:
            zero_clicks +=1 
    
    position = (position - rotation) % 100

    print(zero_clicks)
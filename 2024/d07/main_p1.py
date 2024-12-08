from itertools import product

test = False
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
  data = f.read()
  data = [(int(x.split(':')[0]), list(map(int,(x.split(':')[1]).strip().split(' ')))) for x in data.split('\n') if len(x) > 0]
  
  correct = set()
  for x in data:
    r = x[0]
    m = x[1]
    
    for l in product(['+','*'], repeat=len(m)-1):
      value = m[0]
      for idy, y in enumerate(m[1:]):
        if l[idy] == '*':
          value = value * y      
        if l[idy] == '+':
          value = value + y    
      if value == r:        
        correct.add(r)
        break

  print(correct)
  result = sum(correct)
  print("Result: ", result)
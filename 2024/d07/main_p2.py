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
    
    for l in product(['+','*', '||'], repeat=len(m)-1):
      res = m[0]
      for idll, ll in enumerate(l):
        if ll == '||':
          res = int(str(res)+str(m[idll+1])) 
        if ll == '+':
          res = res + m[idll+1]
        if ll == '*':
          res = res * m[idll+1]
        
      if res == r:        
        correct.add(r)
        break

  print(correct)
  result = sum(correct)
  print("Result: ", result)
import re

test = True
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
  data = f.read()
  data = data.split('\n')
  data = [d for d in data if len(d) > 0]
        
  x = []
  y = []
  for l in data: 
    # print(l)
    values = re.split("\s+", l)
    x.append(int(values[0]))
    y.append(int(values[1]))
  x.sort()
  y.sort()
  result = sum([abs(b-a) for a,b in zip(x,y)])

print("Result")
print(x)
print(y)
print(result)

import re
test = False
filePath = './test_input.txt' if test else './input.txt'

def nums(input: str):
  temp = re.findall(r'\d+', input)
  return [int(x) for x in temp]

with open(filePath, 'r') as f:
  data = f.read()
  presents = [nums(x) for x in data.split('\n')]
  
  ribbon = 0
  for p in presents:
    (l, w, h) = p
    sides = [l, w, h]
    sides.sort()
    ribbon += 2*(sides[0]+sides[1]) + (l * w * h)
    print(ribbon)
  
print(ribbon)
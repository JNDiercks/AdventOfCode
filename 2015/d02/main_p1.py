import re
test = False
filePath = './test_input.txt' if test else './input.txt'

def nums(input: str):
  temp = re.findall(r'\d+', input)
  return [int(x) for x in temp]

with open(filePath, 'r') as f:
  data = f.read()
  presents = [nums(x) for x in data.split('\n')]
  
  wrapping_paper = 0
  for p in presents:
    (l, w, h) = p
    s1 = l*w
    s2 = w*h
    s3 = h*l
    wrapping_paper += 2*(s1 + s2 + s3) + min(s1, s2 ,s3)
  
print(wrapping_paper)
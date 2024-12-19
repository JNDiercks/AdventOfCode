test = False
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
  data = f.read()
  data = list(data)
  print(data)
  print(len(data))
  i = 0
  floor = 0
  while i < len(data):
    if data[i] == '(':
      floor  += 1
    else:
      floor -= 1
    if floor == -1:
      break
    i += 1

  print("Position: ", i+1)
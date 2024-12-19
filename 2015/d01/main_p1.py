test = False
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
  data = f.read()
  data = list(data)
  print(data)
  
  open = data.count('(')
  close = data.count(')')
  print("Floor: ", open - close)
# time: 1h
test = False
filepath = './test_input.txt' if test else './input.txt'

with open(filepath, 'r') as f:
  data = f.read()
  data = list(data)
  disk = [str(index // 2) if index % 2 == 0 else "." for (index, entry) in enumerate(map(int, data)) for _ in range(entry)]
  
  i = 0
  
  while i < len(disk):
    if disk[i] == '.':
      item = disk.pop()
      while item == '.':
        item = disk.pop()
      if i < len(disk):
        disk[i] = item
      else:
        disk.append(item)
    i += 1
    
  print("\n".join(disk))
  result = sum([idx*int(x) for idx, x in enumerate(disk)])
  print("result: ", result)

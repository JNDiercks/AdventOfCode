# time: 
test = False
filepath = './test_input.txt' if test else './input.txt'

with open(filepath, 'r') as f:
  data = f.read()
  data = list(data)
  i = 0
  disk = []
  for idx, x in enumerate(data):
    if idx % 2 == 0:
      files = []
      for i in range(int(x)):
        files.append(str(idx//2))
      disk.append(files)
    else:
        if int(x) > 0:
          disk.append(list("."*int(x)))
  
  index = 0
  while True:
    if index >= len(disk):
      break

    if '.' not in disk[index]:
      index += 1
      continue
    
    free_block_length = len(disk[index])
    for x in range(len(disk)-1, index, -1):
      block_length = len(disk[x])
      if block_length <= free_block_length and '.' not in disk[x]:
        disk[index] = disk[x]
        # del disk[x]
        disk[x] = ['.' for _ in range(block_length)]
        block_difference = free_block_length-block_length
        if block_difference > 0:
          disk.insert(index+1,['.' for _ in range(block_difference)])
        break
    index += 1
    
  d =[idx*int(x) for idx, x in enumerate([f for e in disk for f in e]) if x != '.'] 
  print(d)
  result = sum(d)
  print("result: ", result)
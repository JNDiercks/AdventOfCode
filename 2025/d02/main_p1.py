import  math

test = False
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
  data = f.read()
  data = data.split(',')
  data = [(int(d.split('-')[0]), int(d.split('-')[1])) for d in data if len(d) > 0]
  
  invalidIdsSum = 0

  for d in data:
    for i in range(d[0], d[1]+1, 1):
      id = str(i)
      length = len(id)
      middle = math.floor(int(length/2))
      for sequence_length in range(1, middle+1):
        if length % sequence_length != 0:
          continue

        invalid = True
        start_sequence = id[:sequence_length]
        for n in range(1,int(length/sequence_length)):
          next_sequence = id[n*sequence_length:(n+1)*sequence_length]
          if start_sequence != next_sequence:
            invalid = False
            break

        if invalid:
          invalidIdsSum += i
          break

  print(invalidIdsSum)
  


    #print("Operation: ", l, "Current Position: ", position, "New Position: ", new_position, "Result: ", result)
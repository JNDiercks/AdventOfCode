test = True
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
  data = f.read()
  data = [list(x) for x in data.split('\n') if len(x) > 0]
  y = len(data)
  x = len(data[0])
  
  antidode_count = 0
  antidodes = set()
  for gy in range(y):
    for gx in range(x):
      antenna = data[gy][gx]
      if antenna != '.' and antenna != '#':
        for ggy in range(y):
          for ggx in range(x):
            antenna2 = data[ggy][ggx]
            if antenna2 == antenna and gy != ggy and gx != ggx:
              (dy, dx) = (ggy-gy, ggx-gx)

              antidode1 = (gy-dy, gx-dx)
              if 0 <= antidode1[0] < y and 0 <= antidode1[1] < x and antidode1 not in antidodes:
                antidodes.add(antidode1)

              antidode2 = (ggy+dy, ggx+dx)
              if 0 <= antidode2[0] < y and 0 <= antidode2[1] < x and antidode2 not in antidodes:
                antidodes.add(antidode2)
         
  print("Result: ", len(antidodes))
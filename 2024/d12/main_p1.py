# time:
test = True
filepath = './test_input.txt' if test else './input.txt'

def get_fences_and_neighbors(map, idy, idx):
  plant = map[idy][idx]
  fences = 0
  neighbors = set()
  for d in directions:
    (nidy, nidx) = (idy+d[0], idx+d[1])
    if not 0 <= nidy < y or not 0 <= nidx < x:
      fences += 1
      continue
    if plant != map[nidy][nidx]:
      fences += 1
      continue
    neighbors.add((nidy, nidx))
  return fences, neighbors

with open(filepath, 'r') as f:
  data = f.read()
  map = [list(x) for x in data.split('\n')]

y = len(map)
x = len(map[0])

directions = [(-1,0), (0,1), (1,0), (0,-1)]

fence_areas = []

seen = set()

for idy in range(y):
  for idx in range(x):
    if (idy, idx) not in seen:
      fence_count = 0
      to_explore = [(idy, idx)]
      current_area = set()
      while len(to_explore) > 0:
        (eidy, eidx) = to_explore.pop()
        seen.add((eidy, eidx))
        current_area.add((eidy, eidx))
        fences, neighbors = get_fences_and_neighbors(map, eidy, eidx)
        fence_count += fences
        for n in neighbors:
          if n not in seen and n not in to_explore:
            to_explore.append(n)

      fence_areas.append((map[idy][idx], len(current_area), fence_count))
      
fence_cost = sum([a*f for (p,a,f) in fence_areas])
print("result: ", fence_cost)

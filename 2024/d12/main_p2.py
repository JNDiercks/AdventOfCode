# time:
test = False
filepath = './test_input.txt' if test else './input.txt'

def count_inside_corners(map, idy, idx):
  corners = 0
  for idd in range(0,8,2):
    (nidy, nidx) = (idy+adj8[idd][0], idx+adj8[idd][1])
    (lnidy, lnidx) = (idy+adj8[(idd+1)%8][0], idx+adj8[(idd+1)%8][1])
    (rnidy, rnidx) = (idy+adj8[idd-1][0], idx+adj8[idd-1][1])
    if 0 <= nidy < y and 0 <= nidx < x:
      if map[idy][idx] == map[nidy][nidx]:
        continue
      if 0 <= lnidy < y and 0 <= lnidx < x and 0 <= rnidy < y and 0 <= rnidx < x:
        if map[idy][idx] == map[lnidy][lnidx] and map[idy][idx] == map[rnidy][rnidx]:
          corners += 1
  return corners

def check_fence(map, idy, idx, nidy, nidx):
  if nidy not in range(y) or nidx not in range(x):
    return True
  if map[idy][idx] != map[nidy][nidx]:
    return True
  return False

def count_corners(map, idy, idx):
  corners = 0
  for idd in range(0,8,2):
    (lnidy, lnidx) = (idy+adj8[(idd+1)%8][0], idx+adj8[(idd+1)%8][1])
    (rnidy, rnidx) = (idy+adj8[idd-1][0], idx+adj8[idd-1][1])
    corners += 1 if all([check_fence(map, idy, idx, lnidy, lnidx), check_fence(map, idy, idx, rnidy, rnidx)]) else 0
    
  return corners

def get_neighbors(map, idy, idx):
  plant = map[idy][idx]
  neighbors = set()
  for d in adj4:
    (nidy, nidx) = (idy+d[0], idx+d[1])
    if 0 <= nidy < y and 0 <= nidx < x:
      if plant == map[nidy][nidx]:
        neighbors.add((nidy, nidx))

  return neighbors

with open(filepath, 'r') as f:
  data = f.read()
  map = [list(x) for x in data.split('\n')]

y = len(map)
x = len(map[0])

adj4 = [(-1,0), (0,1), (1,0), (0,-1)]
adj8 = [(-1,-1), (-1, 0), (-1, 1), (0,1), (1,1), (1, 0), (1,-1), (0,-1)]

fence_areas = []
seen = set()

for idy in range(y):
  for idx in range(x):
    if (idy, idx) not in seen:
      to_explore = [(idy, idx)]
      current_area = set()
      fence_count = 0
      while len(to_explore) > 0:
        corners = 0
        (eidy, eidx) = to_explore.pop()
        seen.add((eidy, eidx))
        current_area.add((eidy, eidx))
        corners = count_corners(map, eidy, eidx)
        corners += count_inside_corners(map, eidy, eidx)
        neighbors = get_neighbors(map, eidy, eidx)
        for n in neighbors:
          if n not in seen and n not in to_explore:
            to_explore.append(n)
        fence_count += corners

      fence_areas.append((map[idy][idx], len(current_area), fence_count))
      
fence_cost = sum([a*f for (p,a,f) in fence_areas])
print("result: ", fence_cost)
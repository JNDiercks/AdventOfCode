test = False
filepath = './test_input.txt' if test else './input.txt'

with open(filepath, 'r') as f:
  data = f.read()
  map = [list(x) for x in data.split('\n')]

  y = len(map)
  x = len(map[0])

directions = [(-1,0), (0,1), (1,0), (0,-1)]

def search_trail(iy, ix, map, trailheads):
    height = int(map[iy][ix])
    for d in directions:
      (niy, nix) = (iy+d[0], ix+d[1])     
      if 0 <= niy < y and 0 <= nix < x and map[niy][nix] != '.':
        next_height = int(map[niy][nix])
        if next_height == height + 1:
          if next_height == 9:
            trailheads.append((niy, nix))
          else:
            search_trail(niy, nix, map, trailheads)

  
trails = []
for iy in range(y):
  for ix in range(x):
    if map[iy][ix] == '0':
      trailheads = [] 
      search_trail(iy, ix, map, trailheads)
      trails.append(len(trailheads))
      
print("\n".join([f"{x[0]}, {x[1]}" for x in trailheads]))
print("result: ", sum(trails))

test = False
filePath = './test_input.txt' if test else './input.txt'

def find_positions(map):
    start = (0,0)
    end = (0,0)
    for idy in range(len(map)):
        for idx in range(len(map[0])):
            if map[idy][idx] == 'S':
                start = (idy, idx)
            if map[idy][idx] == 'E':
                end = (idy, idx)
    return start, end


directions = [(-1,0), (0, 1), (1,0), (0,-1)]

def get_move_directions(id):
    left = id -1
    right = (id + 1) % 4 
    return [directions[left], directions[id], directions[right]]

with open(filePath, 'r') as f:
    data = f.read()
    maze = [list(x) for x in data.split('\n')]
    start, end = find_positions(maze)
    
direction = 1
min_distances = {}
previous = {}
min_distances[(*start,direction)]=0
queue = [(*start, direction)] 

while len(queue) > 0:
    current_position = queue.pop()
    direction = current_position[2]
    current_distance = min_distances[current_position]
    for d in get_move_directions(direction):
        next_position = (current_position[0]+d[0], current_position[1]+d[1], directions.index(d))
        if maze[next_position[0]][next_position[1]] != '#':
            new_distance = current_distance + 1 + (1000 if direction != directions.index(d) else 0)
            if next_position not in min_distances:
                min_distances[next_position] = new_distance
                queue.append(next_position)
                prev = set()
                prev.add(current_position)
                previous[next_position] = prev 
            elif min_distances[next_position] > new_distance:
                min_distances[next_position] = new_distance
                queue.append(next_position)
                prev = set()
                prev.add(current_position)
                previous[next_position] = prev 
            elif min_distances[next_position] == new_distance:
                new_set = previous[next_position]
                new_set.add(current_position)
                previous[next_position] = new_set

queue = [(*end, 0), (*end, 1),(*end, 2),(*end, 3)]
path = set()
while len(queue) > 0:
    next_position = queue.pop()
    path.add((next_position[0], next_position[1]))
   
    if next_position not in previous:
        continue    

    prevs = previous[next_position]
    for p in prevs:
        queue.append(p)
    

print("P1: ", len(path))
for key, value in min_distances.items():
    (y, x, _) = key
    if y == end[0] and x == end[1]:
        print("P2: ", value)
        break
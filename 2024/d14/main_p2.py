import re
# time: 
test = False
filepath = './test_input.txt' if test else './input.txt'

def get_quadrant(robot):
    [X,Y,_,_] = robot
    quadrant = 0
    if X == x // 2 \
        or Y == y // 2:
        return None
    if X > x // 2:
        quadrant += 1
    if Y > y // 2:
        quadrant += 2
    return quadrant

def nums(input):
    temp = re.findall(r'-*\d+', input)
    return list(map(int, temp))


with open(filepath, 'r') as f:
    data = f.read()
    robots = [nums(x) for x in data.split('\n')]

x, y = (11, 7) if test else (101, 103)

bathroom = [[0 for _ in range(x)] for _ in range(y)]
for r in robots:
    (px, py, vx, vy) = r
    bathroom[py][px] += 1 

print("\n".join(["".join(list(map(str,row))) for row in bathroom]))
print("-"*20)

seconds = 10000
for s in range(seconds):
    bathroom = [[0 for _ in range(x)] for _ in range(y)]
    for idr, r in enumerate(robots):
        (px, py, vx, vy) = r
        (px, py, vx, vy) = ((px+vx)%x, (py+vy)%y, vx, vy) 
        robots[idr] = (px, py, vx, vy)
    
    robot_positions = [(r[0], r[1]) for r in robots]
    for r in robots:
        (px, py, vx, vy) = r
        bathroom[py][px] += 1 

    if len(set(robot_positions)) == len(robot_positions):
        print("\n".join(["".join(list(map(str,row))).replace('0', ".").replace('1', '#') for row in bathroom]))
        print(s) 
        raise
import re
# time: <50 minutes
test = False
filepath = './test_input.txt' if test else './input.txt'


def nums(input):
    temp = re.findall(r'\d+', input)
    return list(map(int, temp))


with open(filepath, 'r') as f:
    data = f.read()
    games = [x for x in data.split('\n\n')]

tokens = 0
for game in games:
    game = game.split('\n')
    (ax, ay) = nums(game[0])
    (bx, by) = nums(game[1])
    (x, y) = nums(game[2])
    (x, y) = (x+10000000000000, y+10000000000000)
    denominator = ((ay*bx)-(ax*by))
    A = (y*bx-x*by) / denominator
    B = (x*ay-y*ax) / denominator
    if A == A//1 and B == B//1:
        tokens += 3*A+B

print("Total cost: ", int(tokens))

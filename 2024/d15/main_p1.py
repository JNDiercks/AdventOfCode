test = False
filePath = './test_input.txt' if test else './input.txt'


def starting_position(map):
    y = len(map)
    x = len(map[0])
    for idy in range(y):
        for idx in range(x):
            if map[idy][idx] == '@':
                return (idy, idx)
    raise ValueError("No starting position found.")


def get_direction(direction: str):
    match direction:
        case '^':
            return (-1, 0)
        case '>':
            return (0, 1)
        case 'v':
            return (1, 0)
        case '<':
            return (0, -1)


def move_boxes(warehouse, next_position, direction):
    (bpy, bpx) = (next_position[0], next_position[1])
    (nbpy, nbpx) = (bpy + direction[0], bpx + direction[1])
    next_position_object = warehouse[nbpy][nbpx]

    if next_position_object == '#':
        return False

    if next_position_object == 'O':
        if not move_boxes(warehouse, (nbpy, nbpx), direction):
            return False

    warehouse[bpy][bpx] = '.'
    (bpy, bpx) = (nbpy, nbpx)
    warehouse[nbpy][nbpx] = 'O'
    return True


with open(filePath, 'r') as f:
    data = f.read()
    (warehouse, instructions) = data.split('\n\n')
    warehouse = [list(x) for x in warehouse.split('\n')]
    instructions = list("".join(instructions.split('\n')))

    y = len(warehouse)
    x = len(warehouse[0])

    (rpy, rpx) = starting_position(warehouse)

    for move in instructions:
        direction = get_direction(move)
        next_position = (rpy + direction[0], rpx + direction[1])
        next_position_object = warehouse[next_position[0]][next_position[1]]
        if next_position_object == '#':
            continue
        if next_position_object == 'O':
            if not move_boxes(warehouse, next_position, direction):
                continue

        warehouse[rpy][rpx] = '.'
        (rpy, rpx) = next_position
        warehouse[rpy][rpx] = '@'
        # print("\n".join(["".join(x) for x in warehouse]))


gps_coordinates = 0
for idy in range(y):
    for idx in range(x):
        if warehouse[idy][idx] == 'O':
            gps_coordinates += (100 * idy + idx)

print(gps_coordinates)

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


def get_box_position(map, position):
    p = map[position[0]][position[1]]
    if p == '[':
        return (position[0], position[1], position[0], position[1]+1)
    elif p == ']':
        return (position[0], position[1]-1, position[0], position[1])
    raise


def check_box_movements(map, pushed_box_location, direction, affected_boxes):
    (lpby, lpbx, rpby, rpbx) = get_box_position(map, pushed_box_location)
    (lnpby, lnpbx) = (lpby + direction[0], lpbx +
                      direction[1] + (direction[1]*abs(direction[1])))
    (rnpby, rnpbx) = (rpby + direction[0], rpbx +
                      direction[1] + (direction[1]*abs(direction[1])))
    affected_boxes.append((lpby, lpbx, rpby, rpbx))
    left_object = map[lpby][lpbx]
    right_object = map[rpby][rpbx]

    left_next_object = map[lnpby][lnpbx]
    right_next_object = map[rnpby][rnpbx]

    # right left movement possible
    if (right_next_object == '.' or left_next_object == '.') and direction[0] == 0:
        affected_boxes.append((lpby, lpbx, rpby, rpbx))
        return True, affected_boxes

    if left_next_object == '#' or right_next_object == '#':
        return False, affected_boxes

    if left_next_object == '.' and right_next_object == '.':
        affected_boxes.append((lpby, lpbx, rpby, rpbx))
        return True, affected_boxes

    if (left_object == left_next_object and right_object == right_next_object):
        can_move, boxes = check_box_movements(
            map, (lnpby, lnpbx), direction, affected_boxes)
        boxes.append((lpby, lpbx, rpby, rpbx))
        return can_move, boxes

    if (left_object != left_next_object and right_object != right_next_object):
        can_move_left = can_move_right = True
        boxes_left = boxes_right = []
        if map[lnpby][lnpbx] != '.':
            can_move_left, boxes_left = check_box_movements(
                map, (lnpby, lnpbx), direction, affected_boxes)
        if map[rnpby][rnpbx] != '.':
            can_move_right, boxes_right = check_box_movements(
                map, (rnpby, rnpbx), direction, affected_boxes)
        if can_move_left and can_move_right:
            return True, affected_boxes + boxes_left + boxes_right
        return False, affected_boxes


def move_boxes(warehouse, boxes, direction):
    for b in boxes:
        (lby, lbx, rby, rbx) = b
        warehouse[lby][lbx] = '.'
        warehouse[rby][rbx] = '.'

    for b in boxes:
        (lby, lbx, rby, rbx) = b
        warehouse[lby+direction[0]][lbx+direction[1]] = '['
        warehouse[rby+direction[0]][rbx+direction[1]] = ']'
    return warehouse


def correct_warehouse_layout(warehouse):
    y = len(warehouse)
    for idy in range(y):
        warehouse[idy] = list("".join(warehouse[idy]).replace(
            ".", "..").replace("#", "##").replace("O", "[]").replace("@", "@."))
    return warehouse


with open(filePath, 'r') as f:
    data = f.read()
    (warehouse, instructions) = data.split('\n\n')
    warehouse = correct_warehouse_layout(
        [list(x) for x in warehouse.split('\n')])
    instructions = list("".join(instructions.split('\n')))
    y = len(warehouse)
    x = len(warehouse[0])

    (rpy, rpx) = starting_position(warehouse)

    for move in instructions:
        can_move = True
        direction = get_direction(move)
        next_position = (rpy + direction[0], rpx + direction[1])
        next_position_object = warehouse[next_position[0]][next_position[1]]
        if next_position_object == '#':
            continue
        if next_position_object in '[]':
            can_move, boxes = check_box_movements(
                warehouse, next_position, direction, [])
            if can_move:
                warehouse = move_boxes(warehouse, set(boxes), direction)

        if can_move:
            warehouse[rpy][rpx] = '.'
            (rpy, rpx) = next_position
            warehouse[rpy][rpx] = '@'

gps_coordinates = 0
for idy in range(y):
    for idx in range(x):
        if warehouse[idy][idx] == '[':
            gps_coordinates += (100 * idy + idx)

print(gps_coordinates)

# time:
import time

test = True
filepath = './test_input.txt' if test else './input.txt'


def split_number(number):
    stone = str(number)
    half_length = len(stone) // 2
    return (int(stone[:half_length]), int(stone[half_length:]))


def add_or_increase(stone, amount, stones):
    if stone in stones:
        new_stones[stone] += amount
    else:
        new_stones[stone] = amount


with open(filepath, 'r') as f:
    data = f.read()
    stones = dict()
    for stone in data.split(' '):
        if stone in stones:
            stones[int(stone)] += 1
        else:
            stones[int(stone)] = 1

    blinks = 25
    ids = 0
    for b in range(blinks):
        start_time = time.perf_counter()
        new_stones = dict()
        for stone, stone_count in stones.items():
            if stone == 0:
                add_or_increase(1, stone_count, new_stones)
                continue
            if len(str(stone)) % 2 == 0:
                (l, r) = split_number(stone)
                add_or_increase(l, stone_count, new_stones)
                add_or_increase(r, stone_count, new_stones)
                continue
            add_or_increase(stone*2024, stone_count, new_stones)

        stones = new_stones
        runtime = time.perf_counter() - start_time

print("result: ", sum(stones.values()))

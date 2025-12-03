test = False
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
  data = f.read()
  data = data.split('\n')

  battery_length = 2
  joltage = 0

  for d in data:
    digits = list(d)
    digits.append(0)
    number = max([int(d) for d in digits[:-battery_length]])
    index = digits.index(str(number))
    numbers = [number]
    for i in range(battery_length-1):
      active_numbers = digits[index+1:-battery_length+1+i]
      number = max([int(d) for d in active_numbers])
      index = digits[index+1:].index(str(number)) + index + 1
      numbers.append(number)

    if len(numbers) != battery_length:
      raise

    s = [str(n) for n in numbers]
    m = int(''.join(s))
    print(m)
    joltage += m

  print(joltage)
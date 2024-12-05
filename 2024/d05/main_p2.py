import re

test = False
filePath = './test_input.txt' if test else './input.txt'

def swap(list, a, b):
  a_index, b_index = list.index(a), list.index(b)
  list[b_index], list[a_index] = list[a_index], list[b_index]

def swapIfNecessary(pages, rules, order):
  if pages not in rules:
    swap(order, pages[0], pages[1])

def isInRules(pages, rules):
  return pages in rules

def isValid(order, rules):
  return all([isInRules([first, second], rules) for id_first, first in enumerate(order) for second in order[id_first+1:]])

def makeValid(order, rules):
  for i in range(len(order)):
    for second in order[i+1:]:
        swapIfNecessary([order[i], second], rules, order)

def isValidOrder(order, rules):
  if not isValid(order, rules):
    makeValid(order, rules)
    return int(order[len(order)//2])
  else:
    return 0

with open(filePath, 'r') as f:
  data = f.read()
  ordering_rules = [x.split('|') for x in data.split('\n') if re.match(r"\d\d\|\d\d", x)]
  produce_rules = [x.split(',') for x in data.split('\n') if not re.match(r"\d\d\|\d\d", x) and len(x) > 0]
  result = sum([isValidOrder(order, ordering_rules) for order in produce_rules])
  print("Result: ", result)
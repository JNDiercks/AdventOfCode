import re

test = False
filePath = './test_input.txt' if test else './input.txt'

def isInRules(pages, rules):
  return pages in rules

def isValidOrder(order, rules):
  isValid = all([isInRules([first, second], rules) for id_first, first in enumerate(order) for second in order[id_first+1:]])
  return int(order[len(order)//2]) if isValid else 0

with open(filePath, 'r') as f:
  data = f.read()
  ordering_rules = [x.split('|') for x in data.split('\n') if re.match(r"\d\d\|\d\d", x)]
  produce_rules = [x.split(',') for x in data.split('\n') if not re.match(r"\d\d\|\d\d", x) and len(x) > 0]
  result = sum([isValidOrder(order, ordering_rules) for order in produce_rules])
  print("Result: ", result)

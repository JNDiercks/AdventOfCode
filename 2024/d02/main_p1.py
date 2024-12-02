import re
from itertools import pairwise

test = False
filePath = './test_input.txt' if test else './input.txt'

def isSafeReport(levels):
    values = re.split(r"\s+", levels)
    pairwiseDifferences = [int(x[1]) - int(x[0]) for x in pairwise(values)]
    maxDifference = max(map(abs,pairwiseDifferences))
    allIncreasing = all([x > 0 for x in pairwiseDifferences])
    allDecreasing = all([x < 0 for x in pairwiseDifferences])
    return 1 <= maxDifference <= 3 and (allIncreasing or allDecreasing)

with open(filePath, 'r') as f:
  data = f.read()
  data = data.split('\n')
  data = [d for d in data if len(d) > 0]

  isValid = True 
  safeReportCount = 0
  for l in data: 
    safeReportCount = safeReportCount + 1 if isSafeReport(l) else safeReportCount

  print("Safe reports:")
  print(safeReportCount)
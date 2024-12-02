import re
from itertools import pairwise

test = False
filePath = './test_input.txt' if test else './input.txt'

def isSafeReport(pairwiseDifferences):
    maxDifference = max(map(abs,pairwiseDifferences))
    allIncreasing = all([x > 0 for x in pairwiseDifferences])
    allDecreasing = all([x < 0 for x in pairwiseDifferences])
    return 1 <= maxDifference <= 3 and (allIncreasing or allDecreasing)

def getFirstNonNegatveIndex(list):
    for idx, x in enumerate(list):
      if x >= 0:
        return idx

def getFirstNonPositiveIndex(list):
    for idx, x in enumerate(list):
      if x <= 0:
        return idx

def isSingleBadLevelReport(pairwiseDifferences):
    increasingCount = [x > 0 for x in pairwiseDifferences].count(True)
    almostAllIncreasing = increasingCount == len(pairwiseDifferences) - 1
    decreasingCount = [x < 0 for x in pairwiseDifferences].count(True)
    almostAllDecreasing = decreasingCount == len(pairwiseDifferences) - 1

    if almostAllIncreasing or increasingCount == 1:
      negativeIndex = getFirstNonPositiveIndex(pairwiseDifferences)
      del pairwiseDifferences[negativeIndex]
      if isSafeReport(pairwiseDifferences):
        return True
    if almostAllDecreasing or decreasingCount == 1:
      positiveIndex = getFirstNonNegatveIndex(pairwiseDifferences)
      del pairwiseDifferences[positiveIndex]
      if isSafeReport(pairwiseDifferences):
        return True
    
    return False

with open(filePath, 'r') as f:
  data = f.read()
  data = data.split('\n')
  data = [d for d in data if len(d) > 0]

  isValid = True 
  safeReportCount = 0
  for l in data:
    values = re.split(r"\s+", l)
    pairwiseDifferences = [int(x[1]) - int(x[0]) for x in pairwise(values)]
    if isSafeReport(pairwiseDifferences):
      safeReportCount = safeReportCount + 1
    elif isSingleBadLevelReport(pairwiseDifferences):
      safeReportCount = safeReportCount + 1
    else:
       print("UNSAFE: ", values)
    
  print("Safe reports:")
  print(safeReportCount)
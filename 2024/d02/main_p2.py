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
    print("first positive:", list)
    for idx, x in enumerate(list):
      if x >= 0:
        return idx

def getFirstNonPositiveIndex(list):
    print("first negative:", list)
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
      print("n index:" , negativeIndex)
      del pairwiseDifferences[negativeIndex]
      if isSafeReport(pairwiseDifferences):
        return True
    if almostAllDecreasing or decreasingCount == 1:
      positiveIndex = getFirstNonNegatveIndex(pairwiseDifferences)
      print("p index:" , positiveIndex)
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
    print("------------------------")
    values = re.split(r"\s+", l)
    print("values: ", values)
    pairwiseDifferences = [int(x[1]) - int(x[0]) for x in pairwise(values)]
    print("pairs: ", pairwiseDifferences)
    if isSafeReport(pairwiseDifferences):
      print("Is safe: ", l)
      safeReportCount = safeReportCount + 1
    elif isSingleBadLevelReport(pairwiseDifferences):
      print("Potentionally unsafe is safe by removeing one level:", l)
      safeReportCount = safeReportCount + 1
    else:
       print("UNSAFE: ", values)
    


  print("Safe reports:")
  print(safeReportCount)
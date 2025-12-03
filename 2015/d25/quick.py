def RowFirst(row): return row*(row-1)/2+1
def ColN(row, col): return (col-1)*row+(col*(col-1)/2)
def CodeNum(row, col): return RowFirst(row) + ColN(row,col)

startCode = 20151125
row = 2981
col = 3075
ci = int(CodeNum(row, col))

code = startCode
for i in range(ci-1): code = code * 252533 % 33554393

print(code)
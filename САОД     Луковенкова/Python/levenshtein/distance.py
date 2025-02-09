def distance(a, b):
    "Calculates the Levenshtein distance between a and b."
    n, m = len(a), len(b)
    if n > m:
        # Make sure n <= m, to use O(min(n,m)) space
        a, b = b, a
        n, m = m, n
 
    current_row = range(n+1) # Keep current and previous row, not entire matrix
    for i in range(1, m+1):
        previous_row, current_row = current_row, [i]+[0]*m
        for j in range(1,n+1):
            add, delete, change = previous_row[j]+1, current_row[j-1]+1, previous_row[j-1]
            if a[j-1] != b[i-1]:
                change += 1
            current_row[j] = min(add, delete, change)
 
    return current_row[n]
    
if __name__=="__main__":
	assert distance("","")==0
	assert distance("","123")==3
	assert distance("123","")==3
	assert distance("1234","123")==1
	assert distance("1234","1234")==0
	assert distance("12334","1234")==1
	assert distance("qwerty2qwerty","qwertyqwerty")==1

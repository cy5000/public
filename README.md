Fast Polygon decomposition for big polygons

Mark Keil's Algorithm to decompose a polygon into convex polygons is nice because of its simplicity:

diags = decomp(poly)
	min, tmp : EdgeList
	ndiags : Integer
	for each reflex vertex i
		for every other vertex j
			if i can see j
				left = the polygon given by vertices i to j
				right = the polygon given by vertices j to i
				tmp = decomp(left) + decomp(right)
				if(tmp.size < ndiags)
					min = tmp
					ndiags = tmp.size
					min += the diagonal i to j
	return min
	
But for big polygon, it's very slow.
So let's skip optimality and just take the first possible version:

diags = decomp(poly)
	min, tmp : EdgeList
	ndiags : Integer
	for each reflex vertex i
		for every other vertex j
			if i can see j
				left = the polygon given by vertices i to j
				right = the polygon given by vertices j to i
				tmp = decomp(left) + decomp(right)
				return tmp;
	return min

We get a fast result, although subpolygons do not address natural subregions of the original polygons. 
So let's try close neighbours of our reflex vertices first: 

diags = decomp(poly)
	min, tmp : EdgeList
	ndiags : Integer
	for each reflex vertex i
		for every other vertex j sorted by distance to i 
			if i can see j
				left = the polygon given by vertices i to j
				right = the polygon given by vertices j to i
				tmp = decomp(left) + decomp(right)
				return tmp;
	return min

The result looks more natural. Although we get some adjacent polygons which might be merged together to bigger, still convex polygons.
We end up with something which seems to be good and fast enough for most applications:

#!/usr/bin/python
#--*-- encoding: utf-8 --*--

from random import randrange

class SearchTree():
	def __init__(self,distance,pivots):
		self.distance = distance			# функция, измеряющая расстояния 
		self.pivots = pivots				# узлы
		self.levelCount = len(self.pivots)# число уровней дерева
		self.levels = {}					# дерево
		self.__tmp__ = []					# ВВЕДЕНА ПО НЕОБРАЗОВАННОСТИ: НЕ ЗНАЮ, КАК ИСПОЛЬЗОВАТЬ ВНУТРИ РЕКУРСИИ ПЕРЕМЕННЫЕ ВЫЗЫВАЮЩЕЙ ФУНКЦИИ. НУЖНО ПЕРЕПИСАТЬ ПРОЦЕДУРЫ searchlevel И candidates
			
	def distvect(self,element):
		"""
		Возвращает вектор расстояний от element до pivots
		"""
		result = []
		for p in self.pivots:
			result.append(self.distance(p,element))
		return result
		
	def put(self,element):
		"""
		Помещает на дерево элемент
		"""
		vec = self.distvect(element)
		
		level = self.levels # текущий уровень
		levelNum = 0 # номер текущего уровня
		while levelNum < self.levelCount-1:
		# пробегаем все уровни дерева, кроме последнего (последний уровень - листья)
			dist = vec[levelNum]
			try:
				level = level[dist] # пробуем подняться на следующий уровень
			except KeyError:
				# это первый встреченный элемент на расстоянии dist от self.pivots[levelNum]
				level[dist] = {} # создали новый уровень
				level = level[dist] # переходим на следующий уровень
			levelNum = levelNum + 1
		# Заполняем листья дерева элементами
		dist = vec[levelNum]
		try:
			folioj = level[dist] # пробуем взять листья, которые лежат на расстоянии dist от self.pivots[levelNum]
			try:
				folioj[element] = folioj[element]+1 # посчитали
			except KeyError:
				folioj[element] = 1 # Такого элемента еще не встречалось, будет первым :)
		except KeyError:
			# это первый встреченный лист на расстоянии dist от self.pivots[levelNum]
			level[dist] = {element:1} # создали словарь, хранящий листья
		
		
	def putlist(self,lst):
		"""
		Помещает на дерево список элементов
		"""
		for element in lst:
			self.put(element)
			
	def candidates(self,element,maxDist):
		"""
		Возвращает список элементов, которые МОГУТ лежать в шаре радиуса maxDist с центром element,
		т.е. выдает список элементов, расстояние от которых до element <= maxDist.
		В этом списке могут находиться элементы, лежащие дальше, чем на расстоянии maxDist, поэтому 
		для точного установления принадлежности элементов шару, нужно выполнить дополнительную проверку,
		рассчитав соотв. расстояния для элементов списка кандидатов.
		"""
		
		def searchlevel(self,currLevelNum,level):
			# Рекурсивная процедура, пробегает по уровню дерева, заходя в те ветки, расстояние для которых от текущего элемента не больше maxDist
			dist = self.distance(element,self.pivots[currLevelNum])
			fromDist = max(0,dist-maxDist)
			toDist = dist + maxDist
			if currLevelNum == self.levelCount-1:
				# это последний уровень - собираем листья
				for i in range(fromDist,toDist+1):
					try:
						self.__tmp__ = self.__tmp__+level[i].keys()
					except KeyError:
						pass
				return 0
			else:
				# переходим на следующие уровни
				for i in range(fromDist,toDist+1):
					try:
						newLevel = level[i]
						searchlevel(self,currLevelNum+1,newLevel)
					except KeyError:
						pass
			
		self.__tmp__ = []
		searchlevel(self,0,self.levels)
		return self.__tmp__	
		
	def nearest(self, element, maxDist):
		"""
		Возвращает пару (lst,dist), 
			где lst -список ближайших к element элементов (неск. эл. могут лежать на одинак. расстоянии)
					dist - расстояние, на котором находится элемент, ближайший к element.
		Ищется в шаре радиуса maxDist, если в шар не попало ничего - возвр. None
		"""	
		cands = self.candidates(element,maxDist)
		if cands == []:
			return (None, 0)
		else:
			dist = maxDist+0.000001
			res = []
			for el in cands:
				d = self.distance(element,el)
				if d==0:
					# точное совпадение
					return ([el],0)
				elif d < dist:
					# нашли элемент ближе
					res = [el]
					dist = d
				elif d == dist:
					res.append(el)
			return (res, dist)
	
	
class RandomSearchTree(SearchTree):
	"""
	Метрическое дерево со случайно выбранными узлами из списка
	"""
	def __init__(self,distance,levelCount,elementList):
		count = len(elementList)
		p=[]
		for i in range(count):
			p.append(elementList[randrange(count)])
		SearchTree.__init__(self,distance,p)
		self.putlist(elementList)
		
			
		
#######################################
# Тесты
#######################################
		
		
def distvecttest():
	pivots = ['','12345']
	tree = SearchTree(distance,pivots)
	assert tree.distvect(pivots[0])==[0,5]
	assert tree.distvect(pivots[1])==[5,0]
	assert tree.distvect('123')==[3,2]
	
	
def puttest():
	pivots = ['','12345']
	tree = SearchTree(distance,pivots)
	tree.put('234')
	assert tree.levels=={3: {2: {'234': 1}}}
	tree.put('234')
	assert tree.levels=={3: {2: {'234': 2}}}
	tree.put('23')
	assert tree.levels=={
							2: {3:{'23':1}},
							3: {2: {'234': 2}}}
	pivots = ['','1','22']
	tree = SearchTree(distance,pivots)
	tree.put('')
	tree.put('1')
	tree.put('22')
	tree.put('111')
	assert tree.levels == {
							0:{1:{2:{'':1}}},
							1:{0:{2:{'1':1}}},
							2:{2:{0:{'22':1}}},
							3:{2:{3:{'111':1}}}
						}

def putlisttest():
	pivots = ['','1','22']
	tree = SearchTree(distance,pivots)
	tree.putlist(['','1','22','333'])
	assert tree.levels == {
							0:{1:{2:{'':1}}},
							1:{0:{2:{'1':1}}},
							2:{2:{0:{'22':1}}},
							3:{3:{3:{'333':1}}}
						}
	
def candidatestest():
	pivots = ['','1','22']
	tree = SearchTree(distance,pivots)
	tree.putlist(['','1','22','333'])	
	assert tree.candidates('',0) == ['']
	assert tree.candidates('22',0) == ['22']
	assert tree.candidates('',1) == ['','1']
	tree.putlist(['33','23','222'])
	assert tree.levels == {
							0:{1:{2:	{'':1}}},
							1:{0:{2:	{'1':1}}},
							2:{2:{0:	{'22':1},
							      2:	{'33':1},
							      1:	{'23':1}}},
							3:{3:{3:	{'333':1},
							      1:	{'222':1}}}
						}
	cands = tree.candidates('22',1)
	for x in ['22','23','222']:
		assert x in cands
	for x in ['','1','333','33']:
		assert not (x in cands)
		
	cands = tree.candidates('55',3)
	for x in ['','1','22','333','33','23','222']:
		assert x in cands
	
def nearesttest():
	pivots = ['','1','22','544']
	tree = SearchTree(distance,pivots)
	lst = ['','1','22','333','44','455','11','122']
	tree.putlist(lst)
	
	assert tree.nearest('33333333',1)[0] == None
	
	for x in lst:
		assert tree.nearest(x,2) == ([x],0)
	
	nrst=tree.nearest('2',1)
	ansv=['', '1', '22']
	for x in ansv:
		assert x in nrst[0]
		assert nrst[1] == 1
		
	nrst=tree.nearest('2',3)
	ansv=['', '1', '22']
	for x in ansv:
		assert x in nrst[0]
		assert nrst[1] == 1
		
	nrst=tree.nearest('55',3)
	ansv=['455']
	for x in ansv:
		assert x in nrst[0]
		assert nrst[1] == 1

def ramdomsearchtreetest():
	lst=['','1','22','333','44','455','11','122']
	tree=RandomSearchTree(distance,3,lst)
	assert tree.nearest('33333333',1)[0]==None
	
	for x in lst:
		assert tree.nearest(x,2)==([x],0)
	
	nrst=tree.nearest('2',1)
	ansv=['', '1', '22']
	for x in ansv:
		assert x in nrst[0]
		assert nrst[1] == 1
		
	nrst=tree.nearest('2',3)
	ansv=['', '1', '22']
	for x in ansv:
		assert x in nrst[0]
		assert nrst[1] == 1
		
	nrst=tree.nearest('55',3)
	ansv=['455']
	for x in ansv:
		assert x in nrst[0]
		assert nrst[1] == 1
	

if __name__=="__main__":
	from distance import distance
	print "Test..."
	distvecttest()
	puttest()
	putlisttest()
	candidatestest()
	nearesttest()
	ramdomsearchtreetest()
	print "Ok"


#!/usr/bin/python
#--*-- encoding: utf-8 --*--

from dbfpy import dbf # импортируем библиотеку
from types import *
from ConfigParser import *

from searchtree import RandomSearchTree
from distance import distance




def getList(fileName,fieldName):
	"""
	получает список содержимого поля fieldName из записей заданного файла
	"""
	db = dbf.Dbf(fileName)
	res = []
	try:
		for rec in db: # копируем записи из одного файла в другой
			res.append(rec[fieldName])
	finally:
		db.close()
	return res

def copyFile(inFile,inField,outFile,outStrField,maxStrLen,outNumField,tree,maxDist):
	try:
		db = dbf.Dbf(inFile)
		newDB = dbf.Dbf(outFile,new=True)
		for f in db.header.fields: # копируем структуру dbf-файла
			newDB.addField(f)
		# добавим еще строковое и числовое поля:
		f=(outStrField,'C',maxStrLen)
		newDB.addField(f)
		f=(outNumField,'N',10,2)
		newDB.addField(f)
		i=0
		for rec in db: # копируем записи из одного файла в другой
			r = newDB.newRecord()
			newData = []
			for f in rec.fieldData:
				newData.append(f)
			# ищем альтернативную строку:
			pattern = rec[inField]
			(answ, dist) = tree.nearest(pattern,maxDist)
			if answ:
				#str:
				new = ' '.join(answ)
				new = new[:maxStrLen]
				newData.append(new)
				#num:
				newData.append(dist)
			else:
				newData.append(None) # strField
				newData.append(-1.0) # numField
			r.fieldData=newData
			r.store() # сохраняем текущую запись
			print u"Копирование записи %s" % i
			i = i+1
	finally:
		db.close()
		newDB.close()
		
		
					
def main():
	confFile = open('config.conf')
	conf = confFile.readlines()
	for line in conf:
		if line[0]<>'#':
			exec(line)
	print "Идет индексирование - очень долгий процесс :)"
	elems = getList(alternativeFile,alternativeField)
	tree = RandomSearchTree(distance, levelCount, elems)
	print "Производится копирование файлов"
	copyFile(inFile,inField,newFile,newStringField,maxStrLen,newNumField,tree,maxDist)
	
main()

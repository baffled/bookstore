# Python example 1 - Final Version
# This extends the plotstockgenre.py Proof of Concept to be called
# directly from a UniVerse business language procedure.
# 
# Usage: CALL books.bp u2_plotStockGenre
# 
# See the accompanying document on Python Samples in the docs folder.
# 
import uuid
import pandas as pd
import xml.etree.ElementTree as ET
import u2py
import matplotlib.pyplot as plt
from matplotlib import rcParams

def run(startDate,endDate,path):
	cmd = "SELECT BOOK_GENRE, SUM(QTY) FROM UNNEST U2_ORDERS ON BOOK_ID "
	cmd = cmd + "WHERE ORDER_DATE BETWEEN '" + startDate
	cmd = cmd + "' AND '" + endDate
	cmd = cmd + "' GROUP BY BOOK_GENRE TOXML;"
	xmlData = u2py.run(cmd, capture=True)	
	etree = ET.fromstring(xmlData.replace('\n',''))
	dfcols = ['Genre','Qty']
	df = pd.DataFrame(columns=dfcols)
	for i in etree.iter(tag='U2_ORDERS'):
		df = df.append(pd.Series([i.get('BOOK_GENRE'),int(i.get('QTY')) ],index=dfcols),ignore_index=True)
# not before pandas 0.21df.infer_objects()
	df[['Qty']] = df[['Qty']].apply(pd.to_numeric)
	rcParams.update({'figure.autolayout':True})
	ax = df['Qty'].plot()
	ax.set_xticks(df.index)
	ax.set_xticklabels(df.Genre, rotation=90)
	imageName = str(uuid.uuid4()) + '.png'
	plt.savefig(path + imageName)
	return imageName

run('01 JAN 2008','01 JAN 2009','c:\\temp\\')	


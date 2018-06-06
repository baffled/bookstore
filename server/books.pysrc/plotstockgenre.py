# Python example 1 - Proof of Concept
# This demonstrates how to use pandas to sore UniVerse data retrieved
# using an XML query. The data is plotted using matplotlib.
# 
# Usage: RUNPY books.pysrc plotstockgenre.py
# 
# See the accompanying document on Python Samples in the docs folder.
# 

import pandas as pd
import xml.etree.ElementTree as ET
import u2py
import matplotlib.pyplot as plt
from matplotlib import rcParams

xmlData = u2py.run("SELECT GENRE,SUM(STOCK_LEVEL) FROM U2_BOOKS GROUP BY GENRE TOXML;", capture=True)
etree = ET.fromstring(xmlData.replace('\n',''))
dfcols = ['Genre','Stock']
df = pd.DataFrame(columns=dfcols)
for i in etree.iter(tag='U2_BOOKS'):
  df = df.append(pd.Series([i.get('GENRE'),int(i.get('STOCK_LEVEL')) ],index=dfcols),ignore_index=True)
# not before pandas 0.21df.infer_objects()
df[['Stock']] = df[['Stock']].apply(pd.to_numeric)
rcParams.update({'figure.autolayout':True})
ax = df['Stock'].plot()
ax.set_xticks(df.index)
ax.set_xticklabels(df.Genre, rotation=90)
imageName = 'stock.png'
plt.savefig(imageName)
print('Saved chart image as',imageName)


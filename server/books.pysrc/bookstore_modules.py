# Modules for the bookstore personal web server

import u2py
import uuid
import collections
import json
import pandas as pd
import xml.etree.ElementTree as ET
import matplotlib.pyplot as plt

from matplotlib import rcParams

class BaseAPIClass:
    def run(self,args):
      pass
    
class api_hello(BaseAPIClass):
    def run(self,args):
       return {'message':'Hello World'}
       
class api_search(BaseAPIClass):

    def run(self,args):
        results = []
        if "search" in args:
            search = args["search"][0].upper()
            cmd = 'SORT U2_BOOKS ID TITLE AUTHOR_NAME ISBN SALE_PRICE MEDIA'
            cmd = cmd + ' WITH U_TITLE LIKE "...'+ search + '..."'
            cmd = cmd + ' OR WITH U_AUTHOR_NAME LIKE "...' + search + '..."'
            cmd = cmd + ' ID.SUPP TOXML'
            xmlData = u2py.run(cmd, capture=True)	
            etree = ET.fromstring(xmlData.replace('\n',''))
            for i in etree.iter(tag='U2_BOOKS'):
              book = {'id':i.get('ID'),'title':i.get('TITLE'), 'author':i.get('AUTHOR_NAME'), 'isbn':i.get('ISBN'), 'price':i.get('SALE_PRICE'), 'media':i.get('MEDIA')}
              results.append(book)
        return results

class api_getbook(BaseAPIClass):
    def run(self, args):
        results = {}
        if "id" in args:
            id = args["id"][0]
            sub = u2py.Subroutine("u2_getBookDataEx",4)
            sub.args[0] = id
            sub.args[1] = '0'
            sub.call()
            bookData = sub.args[2]
            errText = sub.args[3]
            results = {'id':id,'title':str(bookData.extract(1)),'author':str(bookData.extract(3)),'isbn':str(bookData.extract(4)),
                       'dept':str(bookData.extract(5)),'genre':str(bookData.extract(6)),
                       'media':str(bookData.extract(7)),'publisher':str(bookData.extract(9)),
                       'price':str(bookData.extract(10)),'tax_rate':str(bookData.extract(16)),
                       'description':str(bookData.extract(20))}
        return results            

class api_savecart(BaseAPIClass):
    def run(self, args):
        orderId = ''
        
        if 'client' in args:
            U2_BOOKS = u2py.File("U2_BOOKS")
            U2_SHIPPING = u2py.File("U2_SHIPPING")
            order = u2py.DynArray()
            order.replace(1, args['client'])
            order.replace(2, 'NEW')                             # order status
            if 'deliv_addr' in args : 
               order.replace(3, args['deliv_addr'])
            order.replace(4, 'WEB')                             # origin
            if 'ship_id' in args : 
               shipId = args['ship_id']
            else:
               shipId = 'FREE'
               
            order.replace(5, shipId)                            # shipping type
            shipRec = U2_SHIPPING.read(shipId)
            order.replace(6, str(shipRec.extract(2)))                   # shipping cost
            ct = 0
            for id in args['books']:
               ct = ct + 1
               order.replace(10,ct,id)                          # book key
               bookRec = U2_BOOKS.read(id)
               order.replace(11,ct,'1')                         # qty
               order.replace(12,ct,str(bookRec.extract(8)))     # price
               order.replace(13,ct,str(bookRec.extract(12)))    # tax code
            sub = u2py.Subroutine("u2_setSalesOrder", 3)
            sub.args[0] = ''
            sub.args[1] = order
            sub.call()
            orderId = str(sub.args[0])
            errText = str(sub.args[2])
        else:
            errText = 'Missing cart details'
            
        return {'order':orderId,'error':errText}  

class api_chartsales(BaseAPIClass):
    def run(self, args):
      imageName = ''
      errText = ''
      try:
          startDate = args['start_date']
          endDate = args['end_date']
          path = '..\\..\\bookstore\\web\\work\\'
          cmd = "SELECT BOOK_GENRE, SUM(QTY) FROM UNNEST U2_ORDERS ON BOOK_ID "
          cmd = cmd + "WHERE ORDER_DATE BETWEEN '" + startDate[0]
          cmd = cmd + "' AND '" + endDate[0]
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
      except:
        errText = "Error building chart"
      return {'chart' : imageName,'error' : errText}
      
        
        
	        
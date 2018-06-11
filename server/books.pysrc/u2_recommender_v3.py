# @@Name        : u2_recommender_v3
# @@Description : Simple recommendations (version 3)
# @@Version     : 1.0
# -----------------------------------------------------------------------------
# @@Info{
#
#   This operates as a standard http service that can be easily accessed from 
#   web calls and from the UniVerse routines by calling the regular
#   callHTTP() function from the Business Language.
#
#   The recommendations are based on other clients' purchases (collaborative
#   filtering). For each order we examine the list of books, add those to the
#   set for that same client so we can pair them across the entire purchase
#   history for the client.
#
#   To preserve the arrays, this persists as an http service listener.
#   See the books.bp u2_recommender_v3 subroutine for the driver information.
# -----------------------------------------------------------------------------
import u2py
import socket
from http.server import BaseHTTPRequestHandler, HTTPServer
import sys
import json
import bisect
import threading
import os

class u2_recommender:
    def __init__(self):
        self.U2_ORDERS = u2py.File("U2_ORDERS")
        self.U2_BOOKS = u2py.File("U2_BOOKS")
        self.clientBooks = {}
        self.bookClients = {}
        self.pairs       = {}
        self.debugFile   = open('debug.log','a')
    
    def build(self, limit):
        self.log('starting build')
        slist = u2py.List(0, self.U2_ORDERS)
        ct = 0
        for orderId in slist:
            ct = ct + 1
            if ct > limit and limit > 0:
                break
            orderDA = self.U2_ORDERS.read(orderId)
            self.log('building order ' + str(orderId))
            self.buildOrderToModel(orderDA)
        return ct    
    
    def buildOrder(self, orderId):
        orderDA = self.U2_ORDERS.read(orderId)
        self.buildOrderToModel(orderDA)
        return 1
        
    def buildOrderToModel(self, orderDA):            
        order = orderDA.to_list()
        clientId = order[0]
        if len(order) < 10: return
        self.log('adding ' + str(len(order[9])) + ' books')
        for bookId in order[9]:
           if clientId not in self.clientBooks: 
              self.clientBooks[clientId] = [bookId]
           else:
              pos = bisect.bisect(self.clientBooks[clientId], bookId)
              if not (pos > 0 and self.clientBooks[clientId][pos-1] == bookId):
                 self.clientBooks[clientId].insert(pos,bookId)
           if bookId not in self.bookClients: 
              self.bookClients[bookId] = [clientId]
           else:
              pos = bisect.bisect(self.bookClients[bookId], clientId)
              if not (pos > 0 and self.bookClients[bookId][pos-1] == clientId):
                 self.bookClients[bookId].insert(pos,clientId)
           
    def log(self, text):  
        return
        try:
           self.debugFile.write(text + '\n')
        except:
           self.debugFile.close()
           
    def recommend(self, myBookId,myClientId, limit, noWeight):
        # get details of my book
        bookDA = self.U2_BOOKS.read(myBookId)
        myAuthorId = str(bookDA.extract(2))
        myGenre    = str(bookDA.extract(5))
        otherBooks = {}
        self.log('Getting recommendations for ' + myBookId + ' client ' + myClientId)
        print('Getting recommendations for ' + myBookId + ' client ' + myClientId)
        # get all clients who have purchased this book
        for clientId in self.bookClients[myBookId]:
            bookArray = self.clientBooks[clientId]
            # and all books purchased by those clients            
            for bookId in bookArray:
               if bookId != myBookId:
                   pos = 0
                   if myClientId != '':
                      pos = bisect.bisect(self.clientBooks[myClientId],bookId)
                   if not(pos > 0 and self.clientBooks[myClientId][pos-1] == bookId):                        
                      if bookId not in otherBooks: otherBooks[bookId] = 0                   
                      otherBooks[bookId] = otherBooks[bookId] + 1
        # apply weightings on the result (author and genre)
        if not noWeight in ['yes','true','1']:
           for bookId in otherBooks:
               try:
                   bookDA = self.U2_BOOKS.read(bookId)
                   thisAuthor = str(bookDA.extract(2))
                   thisGenre = str(bookDA.extract(5))
                   if thisAuthor == myAuthorId:
                       otherBooks[bookId] = otherBooks[bookId] * 3
                   elif thisGenre == myGenre:
                       otherBooks[bookId] = otherBooks[bookId] * 2
               except:
                   otherBooks[bookId] = 0
        # sort into reverse order of weighted values
        sortedBooks = []
        for key, value in sorted(otherBooks.items(), key=lambda item: (item[1], item[0]), reverse=True):            
            sortedBooks.append({'titleId': key,'counter' :value})
            if len(sortedBooks) > 10: break
        return json.dumps(sortedBooks)
    
    def processRequest(self, request):
        try:
            action = request['action']
        except:
            action = 'unknown'
           
#        print("got action %s", action) 
        if action == 'build': 
            return self.build(0)
        elif action == 'add': 
            return self.buildOrder(request['orderId'])
        elif action == 'recommend':
            titleId = request['titleId'] if 'titleId' in request else ''            
            clientId = request['clientId'] if 'clientId' in request else ''            
            noWeight = request['noWeight'] if 'noWeight' in request else ''
            return self.recommend(titleId,clientId,0, noWeight)
        elif action == 'close':
            # shutdown call only works on another thread.
            def kill_me(server):
                server.shutdown()
            thread = threading.Thread(target=kill_me, args=(self.server,))
            thread.start()
            self.send_error(500)
        else:
            return 'Unknown action'
                            
class RequestHandler(BaseHTTPRequestHandler):
   def _set_headers(self):
        self.send_response(200)
        self.send_header('Content-type', 'text/html')
        self.end_headers()
        
   def do_GET(self):
      send.send_response(200)
   
   def do_POST(self):
      content_length = int(self.headers['Content-Length'])
      post_data = self.rfile.read(content_length)
      try:
         request = json.loads(post_data.decode('iso-8859-1'))
         self.server.recommender.log(str(request))
         response = self.server.recommender.processRequest(request)
         self._set_headers()
         self.send_response(200)
         self.wfile.write(bytes(response,'iso-8859-1'))
      except: 
         self._set_headers()        
      
      
def createRecommender():
   return u2_recommender()

if __name__ == "__main__":
    HOST,PORT = "localhost",10009    
    a = u2_recommender()
    myServer = HTTPServer((HOST,PORT), RequestHandler)
    myServer.recommender = a
    try:
       myServer.serve_forever()
    except KeyboardInterrupt:
       print('Got a keyboard interrupt')
    myServer.server_close()
   

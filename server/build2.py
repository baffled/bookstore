# Build script for bookstore
# This (re)creates or refreshes the bookstore UniVerse workspace
import os
import u2py

class BookstoreSetup:
  def __init__(self):
    if os.name == 'nt':
      self.acc_home = '..\\universe'
      self.uv_path = 'c:\\u2\\uv'
      self.asset_path = '..\\..\\repository\\assets'
      self.delim = '\\'	  
    else:
      self.acc_home = '../universe'
      self.uv_path = '/usr/uv'
      self.delim = '/'
      self.asset_path = '../../repository/assets'
    self.bin_path = self.makePath(self.uv_path, 'bin')
    self.home = os.getcwd()
    self.python_path = self.makePath(self.uv_path,'python')
    
  def createFolder(self,directory):
    try:
        if not os.path.exists(directory):
            os.makedirs(directory)
    except OSError:
        print ('Error: Creating directory. ' +  directory)

  def createAccount(self):
    print('creating directory ' + self.acc_home)
    self.createFolder(self.acc_home)
    os.chdir(self.acc_home)
    thisAccount = self.makePath(self.acc_home, "mv_books")
    print('creating account ' + thisAccount)
    if not os.path.exists(thisAccount):
      os.makedirs(thisAccount)
    os.chdir(thisAccount)
    print('Checking for VOC file')
    if not os.path.exists(self.makePath(thisAccount, "VOC")):
      self.exec(self.makePath(self.bin_path,"mkaccount") + " PICK")
    else:
      self.exec(self.makePath(self.bin_path,"uv") + " UPDATE.ACCOUNT")
      
  def exec(self, cmd):
    print('Executing ' + cmd)
    os.system(cmd)
    
  def initAccount(self):
    print('Initializing account')
    self.VOC_FILE = u2py.File('VOC')
    u2py.run('LONGNAMES ON', capture=False)
    u2py.run('TERM ,9999', capture=False)
    
    self.setupUtilityPrograms()
    self.setupSourceFiles()
    self.setupDataFiles()
    self.setupPythonPath()
    self.setupPip()
    self.setupPackages()
    
  def loadAssets(self, fileName):
    tempFL = u2py.File(fileName)
    filePath = self.makePath(self.asset_path,fileName.lower()) + '.tab'
    if not os.path.exists(filePath):
      return False
    print('Loading assets for ' + fileName)
    ct = 0
    with open(filePath, 'rU') as f:
      for line in f:
        da = bytearray(line.rstrip('\n'),'utf-8')
        for i in range(len(da)):
          if da[i] == 9: da[i] = 254     # tab to field mark
          elif da[i] == 124: da[i] = 253 # pipe to value mark
            
        rec = u2py.DynArray(da)
        recId = str(rec.extract(1))
        rec.delete(1)
        tempFL.write(recId, rec)
        ct+=1
    print(str(ct) + ' records loaded')
    return True    

  def makePath(self, path, path2):
    return path + self.delim + path2
	
  def compilePrograms(self, fileName):  
    u2py.run('CREATE.FILE ' + fileName +'.O 1,1 1,1,19', capture=False)
    u2py.run('BASIC ' + fileName + ' * ', capture=False)
    u2py.run('CATALOG ' + fileName + '  * ', capture=False)

  def createFilePointer(self, fileName, pointerName):
    rec = u2py.DynArray()
    rec.replace(1,'F')
    rec.replace(2,'../../repository/server/' + fileName)
    rec.replace(3,'D_VOC')
    self.VOC_FILE.write(pointerName,rec)
    
  def createRemotes(self, fileName):
    tempFL = u2py.File(fileName)
    slist = u2py.List(0, tempFL)
    for id in slist:
      rec = u2py.DynArray()
      rec.replace(1,'R')
      rec.replace(2,fileName)
      rec.replace(3,str(id))
      self.VOC_FILE.write(str(id), rec)

  def setupDataFiles(self):
    self.createFilePointer('books.tables','books.tables.safe')
    u2py.run('MAKETABLE books.tables.safe * /NOINCLUDES')
    tempFL = u2py.File('books.tables.safe')
    slist = u2py.List(1, tempFL)
    for id in slist:
      fileName = str(id)
      if not fileName == 'DEFAULT':
        if not self.loadAssets(fileName):      
          self.createFilePointer(fileName, fileName + '.safe')      
          cmd = 'COPYI FROM ' + fileName + '.safe TO ' + fileName + ' ALL OVERWRITING'
          u2py.run(cmd)

  def setupPackages(self):
    packageList = [( 'pandas','0.20.3'),('matplotlib','2.2.2')]
    for package in packageList:
       cmd = self.makePath(self.python_path,'scripts') + self.delim + 'pip install '
       cmd = cmd + package[0] + '==' + package[1]
       os.system(cmd)
	   
  def setupPip(self):
    if os.name == 'nt':
       if not os.path.exists('c:\\Python34'):
          cmd = 'mklink /d c:\\Python34 ' + self.python_path
          os.system(cmd)
		  
  def setupPythonPath(self):
    myPath = self.makePath(self.uv_path,'python')
    myPath = self.makePath(myPath,'my.pth')
    thisPath = self.makePath(self.home,'server')
    thisPath = self.makePath(thisPath,'books.pysrc')
    found = False
    with open(myPath, 'rU') as f:
      for line in f:
        if line == thisPath: found = True
    if found == False:
      open(myPath,'a+').write('\n' + thisPath)
	 
  def setupSourceFiles(self):
    for fileName in ['books.source','books.inc','books.bp','books.pa','books.scripts',
                     'books.tables','test.bp','test.source','images','books.pysrc',
                     'TESTS','TEST_BACKOUT','TEST_BATCH','TEST_DBG','TEST_INC','TEST_LOCAL',
                     'TEST_MOCKS','TEST_OUT','TEST_PROFILE','TEST_RESULTS','TEST_RUN',
                     'TEST_SCRIPTS','TEST_STATE']:
      rec = u2py.DynArray()
      rec.replace(1,'F')
      rec.replace(2,'../../repository/server/' + fileName)
      rec.replace(3,'D_VOC')
      if fileName[-3:] == '.bp':
        rec.replace(4,'M')
        rec.replace(7,1,fileName)
        rec.replace(8,1,fileName)
        rec.replace(7,2,'unidata')
        rec.replace(8,2,'unidata')      
      self.VOC_FILE.write(fileName,rec)
      u2py.run('CREATE.FILE DICT ' + fileName + ' 1,1', capture=False)
      if fileName[-3:] == '.bp':
        self.compilePrograms(fileName)
      elif fileName[-3:] == '.pa':
        self.createRemotes(fileName)
  
  def setupUtilityPrograms(self):
    self.createFilePointer('blutil.bp','blutil.bp')
    self.compilePrograms('blutil.bp')

  
  def startUniVerse(self):
    print('Starting UniVerse command shell in the demonstration bookstore')
    print('Type OFF to quit when you have finished')    
    cmd = self.makePath(self.bin_path,'uv')
    os.system(cmd)
    print('Thank you for using the demonstration bookstore')
      
    
  def run(self):
    self.home = os.getcwd()
    self.createAccount()
    self.initAccount()
    self.startUniVerse()


if __name__ == "__main__":
    a = BookstoreSetup()
    a.run()
    

 

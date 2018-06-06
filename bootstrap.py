# Bootstrap script for Bookstore Demonstration
# This bootstraps the build script making sure it runs the correct
# version of python

import os
path = ''
cmd = ''
prompting = False

    
if os.name == 'nt':
    delim = '\\'
    path = 'c:\\u2\\uv\\python'
    pypath = 'c:\\u2\\uv\\python\\python.exe'
    if not os.path.exists(path):
        print('Please enter the UniVerse home path')        
else:
    # TBD read from /.uvhome 
    delim = '/'   
    path = '/usr/uv/python'
    pypath = '/usr/uv/python/python'

cmd = pypath + ' .' + delim + 'server' + delim + 'build2.py'
print(cmd)
os.system(cmd)


    

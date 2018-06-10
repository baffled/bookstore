# Simple personal web server for bookstore pages
# Usage: 
# RUNPY books.pysrc webserver --host foobar --port 8080 --rootdir path
# This serves pages if they exist, calls methods under /api
# Use /stop to kill the server



import argparse
from http.server import BaseHTTPRequestHandler, HTTPServer
import cgi
import logging
import os
import sys
import json
import threading

from bookstore_modules import *

def make_request_handler_class(opts):
    '''
    Factory to make the request handler and add arguments to it.

    It exists to allow the handler to access the opts.path variable
    locally.
    '''
    class MyRequestHandler(BaseHTTPRequestHandler):
        '''
        Factory generated request handler class that contain
        additional class variables.
        '''
        m_opts = opts

        def do_HEAD(self):
            '''
            Handle a HEAD request.
            '''
            logging.debug('HEADER %s' % (self.path))
            self.send_response(200)
            self.send_header('Content-type', 'text/html')
            self.end_headers()

        def do_GET(self):
            '''
            Handle a GET request.
            '''
            logging.debug('GET %s' % (self.path))

            # Parse out the arguments.
            # The arguments follow a '?' in the URL. Here is an example:
            #   http://example.com?arg1=val1
            args = {}
            idx = self.path.find('?')
            if idx >= 0:
                rpath = self.path[:idx]
                args = cgi.parse_qs(self.path[idx+1:])
            else:
                rpath = self.path

            # Print out logging information about the path and args.
            if 'content-type' in self.headers:
                ctype, _ = cgi.parse_header(self.headers['content-type'])
                
            if len(args):
                i = 0
                for key in sorted(args):
                    logging.debug('ARG[%d] %s=%s' % (i, key, args[key]))
                    i += 1

            # See whether it is an api call or a known command
            if rpath.startswith('/api'):
                methodName = rpath[5:]
                print('Method name ' + methodName)
                self.do_method(methodName, args)
            
            elif rpath == '/stop':
               print("Server is stopping ...")
               def kill_me(server):
                    server.shutdown()
               thread = threading.Thread(target=kill_me, args=(self.server,))
               thread.start()
               self.send_error(500)
            
            elif rpath.endswith('favicon.ico'):
                   self.send_error(404)
                   
            else:            
                self.send_file(rpath, args)
        
        def do_POST(self):
            '''
            Handle POST requests. These map to methods.
            '''
            logging.debug('POST %s' % (self.path))

            ctype, pdict = cgi.parse_header(self.headers['content-type'])
            if ctype == 'multipart/form-data':
                postvars = cgi.parse_multipart(self.rfile, pdict)
            elif ctype == 'application/x-www-form-urlencoded':
                length = int(self.headers['content-length'])
                postvars = cgi.parse_qs(self.rfile.read(length), keep_blank_values=1)
            else:
                length = int(self.headers['content-length'])
                try:
                   post_data = self.rfile.read(length)
                   postvars = json.loads(post_data.decode('iso-8859-1'))
                except:
                   postvars = {}
            rpath = self.path
            if rpath.startswith('/api'):
                methodName = rpath[5:]
                print('Method name ' + methodName)
                self.do_method(methodName, postvars)
            
        def do_method(self, methodName, args):
            # Tell the browser everything is okay and that there is
            # stuff to display.
            self.send_response(200)  # OK
            self.send_header('Content-type', 'application/json')
            self.end_headers()
            targetClass = globals()['api_' + methodName]
            instance = targetClass()
            result = instance.run(args)
            self.wfile.write(bytes(json.dumps(result),'utf-8'))

        def send_file(self, rpath, args):        
            # Check to see whether the file is stored locally, if so return it
            
            # Get the file path.
            path = MyRequestHandler.m_opts.rootdir + rpath
            dirpath = None

            # If it is a directory look for index.html
            if os.path.exists(path) and os.path.isdir(path):
                dirpath = path  # the directory portion
                index_files = ['/index.html', '/index.htm', ]
                for index_file in index_files:
                    tmppath = path + index_file
                    if os.path.exists(tmppath):
                        path = tmppath
                        break

            if os.path.exists(path) and os.path.isfile(path):
                # This is valid file, send it as the response
                # after determining whether it is a type that
                # the server recognizes.
                print('found path ' + path)
                _, ext = os.path.splitext(path)
                ext = ext.lower()
                content_type = {
                    '.css': ('text/css','utf8'),
                    '.gif': ('image/gif','none'),
                    '.htm': ('text/html','utf8'),
                    '.html': ('text/html','utf8'),
                    '.jpeg': ('image/jpeg','none'),
                    '.jpg': ('image/jpg','none'),
                    '.js': ('text/javascript','utf8'),
                    '.png': ('image/png','none'),
                    '.text': ('text/plain','utf8'),
                    '.txt': ('text/plain','utf8')
                }

                # If it is a known extension, set the correct
                # content type in the response.
                if ext in content_type:
                    self.send_response(200)  # OK
                    self.send_header('Content-type', content_type[ext][0])
                    self.end_headers()
                    if content_type[ext][1] == 'none':
                       with open(path, 'rb') as ifp:
                          self.wfile.write(ifp.read())
                       ifp.close()
                    else:
                       with open(path) as ifp:
                          self.wfile.write(bytes(ifp.read(),content_type[ext][1]))
                       ifp.close()
                else:
                    # Unknown file type or a directory.
                    # Treat it as plain text.
                    self.send_response(200)  # OK
                    self.send_header('Content-type', 'text/plain')
                    self.end_headers()

                    with open(path, 'r') as ifp:
                        self.wfile.write(bytes(ifp.read(),'utf8'))
            else:
                self.send_error(404)
                        
        
    return MyRequestHandler

       
      
def err(msg):
    '''
    Report an error message and exit.
    '''
    print('ERROR: %s' % (msg))
    sys.exit(1)


def getopts():
    '''
    Get the command line options.
    '''

    # Get the help from the module documentation.
    this = os.path.basename(sys.argv[0])    
    epilog = ' '
    rawd = argparse.RawDescriptionHelpFormatter
    parser = argparse.ArgumentParser(formatter_class=rawd,
                                     epilog=epilog)

    parser.add_argument('-H', '--host',
                        action='store',
                        type=str,
                        default='localhost',
                        help='hostname, default=%(default)s')

    parser.add_argument('-l', '--level',
                        action='store',
                        type=str,
                        default='info',
                        choices=['notset', 'debug', 'info', 'warning', 'error', 'critical',],
                        help='define the logging level, the default is %(default)s')

    parser.add_argument('--no-dirlist',
                        action='store_true',
                        help='disable directory listings')

    parser.add_argument('-p', '--port',
                        action='store',
                        type=int,
                        default=8080,
                        help='port, default=%(default)s')

    parser.add_argument('-r', '--rootdir',
                        action='store',
                        type=str,
                        default=os.path.abspath('.'),
                        help='web directory root that contains the HTML/CSS/JS files %(default)s')

    parser.add_argument('-v', '--verbose',
                        action='count',
                        help='level of verbosity')

    opts = parser.parse_args()
    opts.rootdir = os.path.abspath(opts.rootdir)
    if not os.path.isdir(opts.rootdir):
        err('Root directory does not exist: ' + opts.rootdir)
    if opts.port < 1 or opts.port > 65535:
        err('Port is out of range [1..65535]: %d' % (opts.port))
    return opts


def httpd(opts):
    '''
    HTTP server
    '''
    RequestHandlerClass = make_request_handler_class(opts)
    server = HTTPServer((opts.host, opts.port), RequestHandlerClass)
    logging.info('Server starting %s:%s (level=%s)' % (opts.host, opts.port, opts.level))
    try:
        server.serve_forever()
    except KeyboardInterrupt:
        pass
    server.server_close()
    logging.info('Server stopping %s:%s' % (opts.host, opts.port))


def get_logging_level(opts):
    '''
    Get the logging levels specified on the command line.
    The level can only be set once.
    '''
    if opts.level == 'notset':
        return logging.NOTSET
    elif opts.level == 'debug':
        return logging.DEBUG
    elif opts.level == 'info':
        return logging.INFO
    elif opts.level == 'warning':
        return logging.WARNING
    elif opts.level == 'error':
        return logging.ERROR
    elif opts.level == 'critical':
        return logging.CRITICAL


def main():
    ''' main entry '''
    opts = getopts()
    logging.basicConfig(format='%(asctime)s [%(levelname)s] %(message)s', level=get_logging_level(opts))
    httpd(opts)


if __name__ == '__main__':
    main()  # this allows library functionality

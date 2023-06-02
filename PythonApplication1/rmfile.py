
#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import os
import time
import datetime

class rmfile:
    def diff():
      '''time diff'''
      starttime = datetime.datetime.now()
      time.sleep(10)
      endtime = datetime.datetime.now()
      print ("time diff: %d" % ((endtime-starttime).seconds))
    def fileremove(filename, timedifference):
      '''remove file'''
      date = datetime.datetime.fromtimestamp(os.path.getmtime(filename))
      print (date)
      now = datetime.datetime.now()
      print (now)
      print ('seconds difference: %d' % ((now - date).seconds))
      if (now - date).seconds > timedifference:
        if os.path.exists(filename):
          os.remove(filename)
          print ('remove file: %s' % filename)
        else:
          print ('no such file: %s' % filename)
    FILE_DIR = r'D:\2022-09'
    if __name__ == '__main__':
      print ('Script is running...')
      #diff()
      while True:
        ITEMS = os.listdir(FILE_DIR)
        NEWLIST = []
        for names in ITEMS:
          if names.endswith(".txt"):
            NEWLIST.append(FILE_DIR +"\\"+ names)
        #print NEWLIST
        for names in NEWLIST:
          print ('current file: %s' % (names))
          fileremove(names, 10)
        time.sleep(10)
      print ("never arrive...")
d1=rmfile()
d1.run()

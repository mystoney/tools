#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import re
class anhei:
    def __init__(self):
        self.wepList = "./weplist.txt"
        self.num = 4
        self.weps = []
        self.dedupComb = []
        self.highComb = []
    #call once before every funtion
    def prepare(self):
        self.readList()
    #call once when all other functions done
    def final(self):
        nmax = 0
        imax = 0
        for i in range(len(self.highComb)):
            if self.highComb[i][1][0] > nmax:
                nmax =  self.highComb[i][1][0]
                imax = i
                
        print("名,坚韧,耐力,精神,力量,智力")
        print(self.highComb[imax])

    #读取列表
    def readList(self):
        with open(self.wepList,'r') as f:
            lines = f.readlines()
            for i in range(len(lines)):
                line=lines[i].rstrip()
                if re.findall("^#",line):
                    continue
                self.weps.append(line)

    def _combine(self,arr, n, start, current, result):
        if n == 0:
            result.append(list(current))
            return
        for i in range(start, len(arr)):
            current.append(arr[i])
            self._combine(arr, n - 1, i + 1, current, result)
            current.pop()
    #4把不同武器组合
    def combine(self,arr, n):
        result = []
        current = []
        self._combine(arr, n, 0, current, result)
        return result
    #去重，一人只能出一把 
    def dedup(self):
        combs=self.combine(self.weps,self.num) 
        for comb in combs:
            names = []
            for w in comb:
                names.append(w.split(",")[0])
            names.sort()
            dup = 0
            for i in range(len(names)-1):
                if names[i] == names[i+1]:
                    dup = 1
                    break
            if dup == 0:
                self.dedupComb.append(comb)
    #计算
    def calc(self):
        for i in range(len(self.dedupComb)):
            jr,nl,js,ll,zl = 0,0,0,0,0
            for w in self.dedupComb[i]:
                jr += int(w.split(",")[1])
                nl += int(w.split(",")[2])
                js += int(w.split(",")[3])
                ll += int(w.split(",")[4])
                zl += int(w.split(",")[5])
            arr = [jr/4,nl/4,js/4,ll/4,zl/4]
            arr.sort()
            if arr[0] != 0:
                self.highComb.append([self.dedupComb[i],arr])

    def run(self):
        self.prepare()
        self.dedup()
        self.calc()
        self.final()

d1=anhei()
d1.run()

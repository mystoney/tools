#!/usr/bin/env python3
# -*- coding: utf-8 -*-
## 66的
corpID="ww4f8e2c46c776cd48"
agentSecret="Qnh5_6f16Vee4S8eWTYQWb71fYU9rzdUohuL0YqofeU"
## 77的
#corpID="wwed1606c46cbfc117"
#agentSecret="Qnh5_6f16Vee4S8eWTYQWb71fYU9rzdUohuL0YqofeU"
##一样的
agentID="1000002"
import time,json,random,requests,datetime,re
def send_to_wecom(text,wecom_cid,wecom_aid,wecom_secret,wecom_touid='@all'):
    get_token_url = f"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={wecom_cid}&corpsecret={wecom_secret}"
    response = requests.get(get_token_url).content
    access_token = json.loads(response).get('access_token')
    if access_token and len(access_token) > 0:
        send_msg_url = f'https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={access_token}'
        data = {
            "touser":wecom_touid,
            "agentid":wecom_aid,
            "msgtype":"text",
            "text":{
                "content":text
            },
            "enable_duplicate_check":"0",
            "duplicate_check_interval":7200
        }
        response = requests.post(send_msg_url,data=json.dumps(data)).content
        return response
    else:
        return False

def eq(nows,cfgs):
    if re.findall("\*",cfgs):
        return True
    if str(nows) == cfgs.replace(" ",""):
        return True
    return False

def match(nows,cfgs):
    if re.findall("\*",cfgs):
        return True
    if re.findall(str(nows),cfgs):
        return True
    return False

def send(remind):
    now = datetime.datetime.now()
    mi,h,d,mo,w,name,msg = remind.split(",")
    msgs = msg.split("\\n")
    message = ""
    for i in range(len(msgs)):
        message += msgs[i] + "\r\n"
    if eq(now.minute,mi) and eq(now.hour,h) and eq(now.day,d) and eq(now.month,mo) and match(str(now.weekday()+1),w):
        send_to_wecom(message,corpID,agentID,agentSecret,name)

def read_send():
    with open("./reminds.txt",'r') as f:
        reminds = f.readlines()
        for r in reminds:
            r = r.rstrip()
            if re.findall("^\s*#",r):
                continue
            send(r)

read_send()

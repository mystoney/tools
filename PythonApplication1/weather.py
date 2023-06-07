#!/usr/bin/env python3
# -*- coding: utf-8 -*-
## 66的
corpID="ww4f8e2c46c776cd48"
agentSecret="Qnh5_6f16Vee4S8eWTYQWb71fYU9rzdUohuL0YqofeU"
## 77的
corpID="wwed1606c46cbfc117"
agentSecret="dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0"
##一样的
agentID="1000002"
weatherKey="eb52e34d9cbb4dd5a7c1e07f4b731902"
vips=[["LiYao1","成都市青羊区","101270117"],
      ["XuLiXia","天津市北辰区","101030600"]
     ]
tempDelta = 5 #温度差大于这么多就提醒加衣服
import json,requests,datetime,re
yestodayData = "./yestodayData.txt"
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
#天气
def get_weather(locationID):
    get_weather_url = f"https://devapi.qweather.com/v7/weather/3d?location={locationID}&key={weatherKey}"
    response = requests.get(get_weather_url).content
    js = json.loads(response)
    if js.get('code') == '200':
        return js.get('daily')
    else:
        return False
#空气质量
def get_air(locationID):
    get_air_url = f"https://devapi.qweather.com/v7/air/5d?location={locationID}&key={weatherKey}"
    response = requests.get(get_air_url).content
    js = json.loads(response)
    if js.get('code') == '200':
        return js.get('daily')
    else:
        return False

#
def tips(locationID,idx,weather,air):
    tips = "\r\n"
    if re.findall("雨",weather[idx]['textDay']):
        tips += "\r\n雨天记得带伞"
    if int(air[idx]['aqi']) > 100:
        tips += "\r\n空气污染记得戴口罩"
    if idx == 1:
        delta = int(weather[0]['tempMin']) - int(weather[1]['tempMin'])
        if delta > tempDelta: #温差大于x度提醒
            tips += "\r\n比今天低"+ str(delta) +"度，天冷记得加衣服"
    else:
        try:
            with open(yestodayData,'r',encoding='utf-8') as f:
                lines = f.readlines()
                for line in lines:
                    loc,temp = line.split(",")
                    if loc == locationID:
                        delta = int(temp) - int(weather[0]['tempMin'])
                        if delta > tempDelta:
                            tips += "\r\n比昨天低"+ str(delta) +"度，天冷记得加衣服"
        except:
            pass
    return tips

def sendmsg(): 
    now = datetime.datetime.now()
    idx = 0
    day = "今天"
    sav = ""
    if now.hour > 18:
        idx = 1
        day = "明天"
    for p in vips:
        weather = get_weather(p[2])
        temp,text,windScale = weather[idx]['tempMin'] + "~" + weather[idx]['tempMax'] + "°C",weather[idx]['textDay'],weather[idx]['windScaleDay']
        air = get_air(p[2])
        aqi,cate,primary = air[idx]['aqi'],air[idx]['category'],air[idx]['primary']
        sendText = '<a href="https://www.qweather.com/">=='+p[1]+" : "+day+'==</a>\r\n'
        sendText += f"天        气：{text}\r\n温        度：{temp}\r\n风        力：{windScale}\r\n\r\n空气质量：{cate}\r\n空气指数：{aqi}"
        if int(aqi) > 50 and len(primary) > 0 and primary != 'NA':
            sendText += "\r\n污  染  物："+primary
        send_to_wecom(sendText + tips(p[2],idx,weather,air),corpID,agentID,agentSecret,p[0])
        if idx == 0 :
            sav += p[2] + "," +weather[idx]['tempMin'] + "\r\n"
    if idx == 0:
        with open(yestodayData,'w',encoding='utf-8') as f:
            f.write(sav)
#
sendmsg()

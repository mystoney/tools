#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import re
class GetWeather:
    def get_weather():
        url = "https://d1.weather.com.cn/sk_2d/101010100.html?_=1618886817920"
        requests_url = requests.get(url)
        message = json.loads(requests_url.text.encode("latin1").decode("utf8").replace("var dataSK = ", ""))
        cityname = message['cityname']
        aqi = int(message['aqi'])
        sd = message['sd']
        wd = message['WD']
        ws = message['WS']
        temp = message['temp']
        weather = message['weather']
        if aqi <= 50:
            airQuality = "优"
        elif aqi <= 100:
            airQuality = "良"
        elif aqi <= 150:
            airQuality = "轻度污染"
        elif aqi <= 200:
            airQuality = "中度污染"
        elif aqi <= 300:
            airQuality = "重度污染"
        else:
            airQuality = "严重污染"
        return cityname + " " + '今日天气：' + weather + ' 温度：' + temp + ' 摄氏度 ' + wd + ws + ' 相对湿度：' + sd + ' 空气质量：' \
               + str(aqi) + "（" + airQuality + "）"

d1=get_weather()
d1.run()


using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorAndControl
{
    class WeCom
    {
        public string SendToWeCom(
          string text,// 推送消息
          string weComCId, // 企业Id①
          string weComSecret,// 应用secret②
          string weComAId,// 应用ID③
          string weComTouId)
        {

            

            // 获取Token
            string getTokenUrl = $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={weComCId}&corpsecret={weComSecret}";
            string token = JsonConvert
            .DeserializeObject<dynamic>(new RestClient(getTokenUrl)
            .Get(new RestRequest()).Content).access_token;
            //System.Console.WriteLine(token);
            //System.Console.WriteLine("\r\n");


            if (!String.IsNullOrWhiteSpace(token))
            {

                var request = new RestRequest();
                var client = new RestClient($"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={token}");
                var data = new
                {
                 //touser = weComTouId,
                    toparty = weComTouId,
                    agentid = weComAId,
                    msgtype = "text",
                    text = new
                    {
                        content = text
                    },
                    duplicate_check_interval = 600
                };
                string serJson = JsonConvert.SerializeObject(data);
                //System.Console.WriteLine(serJson);
                //System.Console.WriteLine("\r\n");
                request.Method = Method.Post;
                request.AddHeader("Accept", "application/json");
                //request.Parameters.Clear();                
                request.AddParameter("application/json", serJson, ParameterType.RequestBody);
                return client.Execute(request).Content;
            }
            return "-1";
        }
    }



   

   

}

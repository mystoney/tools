using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace MyContrals
{
    public class JSON
    {
        public static string GetRequestTest(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 20000;

            //byte[] btBodys = Encoding.UTF8.GetBytes(body);
            //httpWebRequest.ContentLength = btBodys.Length;
            //httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            httpWebResponse.Close();
            streamReader.Close();
            return responseContent;
        }

        //生成 例如{"success":"0"}的json 方法

        public string jsonstr_onekey(string keycode1, string keyvalue1)
        {
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            writer.WritePropertyName(keycode1);
            writer.WriteValue(keyvalue1);
            writer.WriteEndObject();
            writer.Flush();
            string jsonText = sw.GetStringBuilder().ToString();
            return jsonText;
        }

        //生成 例如{"success":"0","errcode":"400"}的json 方法
        public string jsonstr_twokey(string keycode1, string keyvalue1, string keycode2, string keyvalue2)
        {
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            writer.WritePropertyName(keycode1);
            writer.WriteValue(keyvalue1);
            writer.WritePropertyName(keycode2);
            writer.WriteValue(keyvalue2);
            writer.WriteEndObject();
            writer.Flush();
            string jsonText = sw.GetStringBuilder().ToString();
            return jsonText;
        }



        //生成多个参数json

        public string jsonstr_threekey(string keycode1, string keyvalue1, string keycode2, string keyvalue2, string keycode3, string keyvalue3)
        {
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            writer.WritePropertyName(keycode1);
            writer.WriteValue(keyvalue1);
            writer.WritePropertyName(keycode2);
            writer.WriteValue(keyvalue2);
            writer.WritePropertyName(keycode3);
            writer.WriteValue(keyvalue3);
            writer.WriteEndObject();
            writer.Flush();
            string jsonText = sw.GetStringBuilder().ToString();
            return jsonText;
        }

        public string DataTableToJson(DataTable table)
        {
            var JsonString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
            }
            return JsonString.ToString();
        }
        public string DataTableToJsonWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }
        public string DataTableToJsonWithJsonNet(DataTable table)
        {
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table);
            return JsonString;
        }


        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合 处理简单Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DataTable ToDataTableOne(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                string json1 = "{\"code\":\"200\",\"updateTime\":\"2023-06-01T14:07+08:00\",\"fxLink\":\"https://www.qweather.com/weather/qingyang-101270117.html\",\"now\":{\"obsTime\":\"2023-06-01T13:57+08:00\",\"temp\":\"28\",\"feelsLike\":\"25\",\"icon\":\"101\",\"text\":\"多云\",\"wind360\":\"21\",\"windDir\":\"东北风\",\"windScale\":\"3\",\"windSpeed\":\"17\",\"humidity\":\"42\",\"precip\":\"0.0\",\"pressure\":\"949\",\"vis\":\"16\",\"cloud\":\"91\",\"dew\":\"15\"},\"refer\":{\"sources\":[\"QWeather\",\"NMC\",\"ECMWF\"],\"license\":[\"CC BY-SA 4.0\"]}}";
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json1);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        //Columns
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        //Rows
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合 处理复杂Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTableTwo(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        //Columns
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                if (current != "data")
                                    dataTable.Columns.Add(current, dictionary[current].GetType());
                                else
                                {
                                    ArrayList list = dictionary[current] as ArrayList;
                                    foreach (Dictionary<string, object> dic in list)
                                    {
                                        foreach (string key in dic.Keys)
                                        {
                                            dataTable.Columns.Add(key, dic[key].GetType());
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        //Rows
                        string root = "";
                        foreach (string current in dictionary.Keys)
                        {
                            if (current != "data")
                                root = current;
                            else
                            {
                                ArrayList list = dictionary[current] as ArrayList;
                                foreach (Dictionary<string, object> dic in list)
                                {
                                    DataRow dataRow = dataTable.NewRow();
                                    dataRow[root] = dictionary[root];
                                    foreach (string key in dic.Keys)
                                    {
                                        dataRow[key] = dic[key];
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合 处理不规则Json，因为列并不确定，所以直接定义列，不动态生成
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTableThree(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                dataTable.Columns.Add("id");
                dataTable.Columns.Add("mac");
                dataTable.Columns.Add("rssi");
                dataTable.Columns.Add("ch");
                dataTable.Columns.Add("ts");
                dataTable.Columns.Add("tmc");
                dataTable.Columns.Add("tc");
                dataTable.Columns.Add("ds");
                dataTable.Columns.Add("essid");
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }//Rows
                        string root = "";
                        foreach (string current in dictionary.Keys)
                        {
                            if (current != "data")
                                root = current;
                            else
                            {
                                ArrayList list = dictionary[current] as ArrayList;
                                foreach (Dictionary<string, object> dic in list)
                                {
                                    DataRow dataRow = dataTable.NewRow();
                                    dataRow[root] = dictionary[root];
                                    foreach (string key in dic.Keys)
                                    {
                                        dataRow[key] = dic[key];
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }


    }
}
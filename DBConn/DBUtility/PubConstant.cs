
/*************************************************************************************
 * 
 *@FileName:PubConstant.cs
 *@Copyright (c) 2008 CICT
 *@Author:songying
 *@Create Date:2008-03-18
 *@Description:
 *@History 
 * 
 *************************************************************************************/
using System;
using System.Configuration;
using DBCon.DBUtility;

namespace DBCon.DBUtility
{
    public class PubConstant
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionSQLString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionSQL"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];

                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionOLEString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionOLE"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];

                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionODBCString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionODBC"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];

                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// 得到App.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }
    }
}

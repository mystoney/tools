using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;
using DBCon.DBUtility;
using System.Windows.Forms;
using System.Data.Odbc;

namespace DBConn.DataAcess
{
    public abstract class OdbcConn
    {
        
        //���ݿ������ַ���(App.config������)�����Զ�̬����connectionString֧�ֶ����ݿ�.		

        public static string connectionString = PubConstant.ConnectionODBCString;



        public OdbcConn()
        {
            //connectionString = PubConstant.GetConnectionString(Connstr);
        }

        #region  ִ�м�SQL���

        /// <summary>
        /// ִ��SQL��䣬����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public static int ExecuteSql(string SQLString)
        {
            //using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                
                //using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
                using (OdbcCommand cmd = new OdbcCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (Exception ex)
                    //catch (System.Data.OleDb.OleDbException E)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                        throw new Exception(ex.Source);
                        throw new Exception(ex.StackTrace);
                        //return 0;
                    }
                }
            }
        }

        /// <summary>
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">����SQL���</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (OdbcConnection  conn = new OdbcConnection (connectionString))
            {
                conn.Open();
                OdbcCommand cmd = new OdbcCommand();
                cmd.Connection = conn;
                
                OdbcTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                string strsql = "";
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                         strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message + "[" + strsql + "]");
                }
            }
        }
        /// <summary>
        /// ִ�д�һ���洢���̲����ĵ�SQL��䡣
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <param name="content">��������,����һ���ֶ��Ǹ�ʽ���ӵ����£���������ţ�����ͨ�������ʽ���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                OdbcCommand cmd = new OdbcCommand(SQLString, connection);
                //System.Data.OleDb.OleDbParameter myParameter = new System.Data.OleDb.OleDbParameter("@content", OleDbType.VarChar);
                System.Data.SqlClient.SqlParameter myParameter = new SqlParameter("@content", System.Data.SqlDbType.VarChar);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// �����ݿ������ͼ���ʽ���ֶ�(������������Ƶ���һ��ʵ��)
        /// </summary>
        /// <param name="strSQL">SQL���</param>
        /// <param name="fs">ͼ���ֽ�,���ݿ���ֶ�����Ϊimage�����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                OdbcCommand cmd = new OdbcCommand(strSQL, connection);
                SqlParameter myParameter = new SqlParameter("@fs", System.Data.SqlDbType.Binary);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <returns>��ѯ�����object��</returns>
        public static object GetSingle(string SQLString)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                using (OdbcCommand cmd = new OdbcCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        /// <summary>
        /// ִ�в�ѯ��䣬����OleDbDataReader
        /// </summary>
        /// <param name="strSQL">��ѯ���</param>
        /// <returns>OleDbDataReader</returns>
        public static OdbcDataReader ExecuteReader(string strSQL)
        {
            OdbcConnection  connection = new OdbcConnection (connectionString);
            OdbcCommand cmd = new OdbcCommand(strSQL, connection);
            try
            {
                connection.Open();
                
                OdbcDataReader myReader = cmd.ExecuteReader();
                
                return myReader;
            }
            catch (SqlException e)
            {
                throw new Exception(e.StackTrace);
            }
        }
        /// <summary>
        /// ִ�в�ѯ��䣬����DataSet
        /// </summary>
        /// <param name="SQLString">��ѯ���</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();

                    OdbcDataAdapter command = new OdbcDataAdapter(SQLString, connection);
                    
                     command.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }


        #endregion

        #region ִ�д�������SQL���

        /// <summary>
        /// ִ��SQL��䣬����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">SQL���Ĺ�ϣ��keyΪsql��䣬value�Ǹ�����OleDbParameter[]��</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (OdbcConnection  conn = new OdbcConnection (connectionString))
            {
                conn.Open();
                using (OdbcTransaction trans = conn.BeginTransaction())
                {
                    OdbcCommand cmd = new OdbcCommand();
                    try
                    {
                        //ѭ��
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <returns>��ѯ�����object��</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// ִ�в�ѯ��䣬����OleDbDataReader
        /// </summary>
        /// <param name="strSQL">��ѯ���</param>
        /// <returns>OleDbDataReader</returns>
        public static OdbcDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            OdbcConnection  connection = new OdbcConnection (connectionString);
            OdbcCommand cmd = new OdbcCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                OdbcDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// ִ�в�ѯ��䣬����DataSet
        /// </summary>
        /// <param name="SQLString">��ѯ���</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                OdbcCommand cmd = new OdbcCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OdbcDataAdapter da = new OdbcDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        private static void PrepareCommand(OdbcCommand cmd, OdbcConnection conn, OdbcTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region �洢���̲���

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>OleDbDataReader</returns>
        public static OdbcDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            OdbcConnection  connection = new OdbcConnection (connectionString);
            OdbcDataReader returnReader;
            connection.Open();
            OdbcCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }


        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="tableName">DataSet����еı���</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                OdbcDataAdapter sqlDA = new OdbcDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


        /// <summary>
        /// ���� OleDbCommand ����(��������һ���������������һ������ֵ)
        /// </summary>
        /// <param name="connection">���ݿ�����</param>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>OleDbCommand</returns>
        private static OdbcCommand BuildQueryCommand(OdbcConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OdbcCommand command = new OdbcCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// ִ�д洢���̣�����Ӱ�������		
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="rowsAffected">Ӱ�������</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (OdbcConnection  connection = new OdbcConnection (connectionString))
            {
                int result;
                connection.Open();
                OdbcCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close(); 
                return result;
            }
        }

        /// <summary>
        /// ���� OleDbCommand ����ʵ��(��������һ������ֵ)	
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>OleDbCommand ����ʵ��</returns>
        private static OdbcCommand BuildIntCommand(OdbcConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OdbcCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                System.Data.SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion
    
    }
}

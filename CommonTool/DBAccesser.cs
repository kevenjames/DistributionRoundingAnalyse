using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace CommonTool
{
    public class DBAccesser
    {
        private static DBAccesser m_Instance;
        private static object _lock = new object();
        OleDbConnection m_AccessConn = null;
        public Log m_log;

        private DBAccesser(string strAccessConnect)
        {
            m_log = Log.GetInstance(GShare.cstDBOperate);
            m_AccessConn = new OleDbConnection(strAccessConnect);
        }
        public static DBAccesser GetInstance(string strAccessConnect)
        {
            if (m_Instance == null)
            {
                lock (_lock)
                {
                    if (m_Instance == null)
                        m_Instance = new DBAccesser(strAccessConnect);
                }
            }

            return m_Instance;
        }
        public DataSet SelectOperate(string strSQL, string strTable)
        {
            DataSet ds = null;
            try
            {
                OleDbCommand cmd = new OleDbCommand(strSQL, m_AccessConn);
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);

                ds = new DataSet();
                m_AccessConn.Open();
                adp.Fill(ds, strTable);
            }
            catch (Exception ex)
            {
                Log.Log_Error("Connect DB failed");
            }
            finally
            {
                m_AccessConn.Close();
            }

            return ds;
        }
        public void UpdateOperate(string strSQL)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(strSQL, m_AccessConn);
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);

                m_AccessConn.Open();
            }
            catch (Exception ex)
            {
                Log.Log_Error("Connect DB failed");
            }
            finally
            {
                m_AccessConn.Close();
            }
        }
    }
}

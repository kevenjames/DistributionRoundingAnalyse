using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CommonTool
{
    public class SQLDB
    {
        private static SQLDB m_Instance;
        private static object _lock = new object();
        SqlConnection m_AccessConn = null;
        public Log m_log;

        private SQLDB(string strAccessConnect)
        {
            m_log = Log.GetInstance(GShare.cstDBOperate);
            m_AccessConn = new SqlConnection(strAccessConnect);
            m_AccessConn.Open();
        }
        public static SQLDB GetInstance(string strAccessConnect)
        {
            if (m_Instance == null)
            {
                lock (_lock)
                {
                    if (m_Instance == null)
                        m_Instance = new SQLDB(strAccessConnect);
                }
            }

            return m_Instance;
        }
        public SqlDataReader Select(string strSQL)
        {
            SqlDataReader dr = null;

            if (m_AccessConn == null)
                return dr;

            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, m_AccessConn);
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Log.Log_Error(ex.Message);
            }
            finally
            {
                m_AccessConn.Close();
            }

            return dr;
        }
        public bool Update(string strSQL)
        {
            bool bSuccessed = false;
            if (m_AccessConn == null)
                return bSuccessed;
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, m_AccessConn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.Log_Error(ex.Message);
                return false;
            }
            finally
            {
                m_AccessConn.Close();
            }

            return true;
        }
    }
}

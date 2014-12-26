using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Common {
    public class DBController {
        #region Variables
        private String mSessionId;
        #endregion
        #region Constructor
        public DBController(string sessionId) {
            mSessionId = sessionId;
        }
        #endregion
        #region Public Interface
        public void setSynchronizationParameters(ref bool? isSynchronizationEnabled, ref int? periodicityInMinutes) {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try {
                conn = new SqlConnection(CommonMethods.getConnectionString(ConfigurationManager.AppSettings["SROCalendarProvider"]));
                conn.Open();                
                cmd = new SqlCommand("SRSSynchronizationControlGetAndSet", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (isSynchronizationEnabled.HasValue) {
                    cmd.Parameters.Add("@IsSynchronizationActive", SqlDbType.Bit).Value = isSynchronizationEnabled.Value;
                }
                if (periodicityInMinutes.HasValue) {
                    cmd.Parameters.Add("@SynchronizationPeriodicityInMinutes", SqlDbType.Int).Value = periodicityInMinutes.Value;
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (CommonRoutines.HasData(ds)) {
                    periodicityInMinutes = CommonRoutines.ObjectToInt(ds.Tables[0].Rows[0]["SynchronizationPeriodicityInMinutes"]);
                    isSynchronizationEnabled = CommonRoutines.ObjectToBool(ds.Tables[0].Rows[0]["IsSynchronizationActive"]);
                }
            } catch (Exception e) {
            } finally {
                try { cmd.Dispose(); } catch { }
                try { conn.Close(); } catch { }
            }
        }
        public void setLoginParamenters(ref string id, ref string password) {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try {
                conn = new SqlConnection(CommonMethods.getConnectionString(ConfigurationManager.AppSettings["SROCalendarProvider"]));
                conn.Open();
                cmd = new SqlCommand("SRSSynchronizationControlGetAndSet", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (id!=null) {
                    cmd.Parameters.Add("@webControlUser", SqlDbType.VarChar).Value = id;
                }
                if (password!=null) {
                    cmd.Parameters.Add("@webControlPassword", SqlDbType.VarChar).Value = password;
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (CommonRoutines.HasData(ds)) {
                    id = CommonRoutines.ObjectToStringV2(ds.Tables[0].Rows[0]["webcontroluser"]);
                    password = CommonRoutines.ObjectToStringV2(ds.Tables[0].Rows[0]["webcontrolpassword"]);
                }
            } catch (Exception e) {
            } finally {
                try { cmd.Dispose(); } catch { }
                try { conn.Close(); } catch { }
            }
        }
        public void getLoginParameters(out string id, out string password) {
            id = null;
            password = null;
            SqlCommand cmd=new SqlCommand("SRSSynchronizationControlGetAndSet");
            DataSet ds = Common.CommonRoutines.getDataSetSProc(
                cmd,
                CommonMethods.getConnectionString(ConfigurationManager.AppSettings["SROCalendarProvider"]));
            if(CommonRoutines.HasData(ds)) {
                password=CommonRoutines.ObjectToStringV2(ds.Tables[0].Rows[0]["webcontrolpassword"]);
                id = CommonRoutines.ObjectToStringV2(ds.Tables[0].Rows[0]["webcontroluser"]);
            }
        }
        public void getSynchronizationParameters(
                out bool isSynchronizationEnabled, 
                out int periodicityInMinutes) {
            periodicityInMinutes = 60;
            isSynchronizationEnabled = true;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try {
                conn = new SqlConnection(CommonMethods.getConnectionString(ConfigurationManager.AppSettings["SROCalendarProvider"]));
                conn.Open();
                cmd = new SqlCommand("SRSSynchronizationControlGetAndSet", conn);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds);
                if(CommonRoutines.HasData(ds)) {
                    periodicityInMinutes=CommonRoutines.ObjectToInt(ds.Tables[0].Rows[0]["SynchronizationPeriodicityInMinutes"]);
                    isSynchronizationEnabled=CommonRoutines.ObjectToBool(ds.Tables[0].Rows[0]["IsSynchronizationActive"]);
                }
            } catch (Exception e) {
            } finally {
                try { cmd.Dispose(); } catch { }
                try { conn.Close(); } catch { }
            }
        }
        public void synchronizeData() {
            decimal? latitude;
            decimal? longitude;
            bool updateUpdateTable = false;
            Dictionary<int, string> cal_ids = new Dictionary<int,string>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try {
                conn = new SqlConnection(CommonMethods.getConnectionString(ConfigurationManager.AppSettings["SROCalendarProvider"]));
                conn.Open();
                cmd = new SqlCommand("SRCalendarPut", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = GetMySqlCalendar();
                if (CommonRoutines.HasData(ds)) {
                    foreach (DataRow dr in ds.Tables[0].Rows) {
                        cal_ids.Add((int)dr["cal_id"], ((int)dr["cal_id"]).ToString());
                        cmd.Parameters.Clear();
                        /*
                         * exec SRCalendarPut
		@cal_id = 1,
		@cal_date = '1/2/2017',
		@cal_time  ='08:16 PM',
		@cal_name = 'New year''s celebration 2',
		@cal_location = 'Near Denver ',
		@cal_location_latitude = 40.71470422, 
		@cal_location_longitude = -106.05932222,
		@cal_url = 'www.ibm.com',
		@cal_description = 'Here is an event at Mike''s house2'
                         */
                        cmd.Parameters.Add("@cal_id", SqlDbType.Int).Value = dr["cal_id"];
                        cmd.Parameters.Add("@cal_date", SqlDbType.Date).Value = CommonRoutines.dateFromYYYYMMDD(dr["cal_date"].ToString());
                        cmd.Parameters.Add("@cal_time", SqlDbType.VarChar).Value = CommonRoutines.timeInStringFormatFromNbrOfSecondsPastMidnight(CommonRoutines.ObjectToInt(dr["cal_time"]));
                        cmd.Parameters.Add("@cal_name",SqlDbType.VarChar).Value=CommonRoutines.ObjectToStringV2(dr["cal_name"]);
                        cmd.Parameters.Add("@cal_location", SqlDbType.VarChar).Value = CommonRoutines.ObjectToStringV2(dr["cal_location"]);
                        CommonRoutines.deriveLatLongFromLocation(CommonRoutines.ObjectToStringV2(dr["cal_location"]), out latitude, out longitude);
                        cmd.Parameters.Add("@cal_location_latitude", SqlDbType.Decimal).Value = latitude;
                        cmd.Parameters.Add("@cal_location_longitude", SqlDbType.Decimal).Value = longitude;
                        cmd.Parameters.Add("@cal_url", SqlDbType.VarChar).Value = CommonRoutines.ObjectToStringV2(dr["cal_url"]);
                        cmd.Parameters.Add("@cal_description", SqlDbType.VarChar).Value = CommonRoutines.ObjectToStringV2(dr["cal_description"]);
                        SqlParameter parmRet = new SqlParameter("@changed", SqlDbType.Bit);
                        parmRet.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(parmRet);
                        cmd.ExecuteNonQuery();
                        if (((bool)parmRet.Value)) {
                            updateUpdateTable = true;
                        }
                        if (updateUpdateTable) { 
                            SqlConnection conn2 = null;
                            SqlCommand cmd2 = null;
                            try {
                                DateTime theDateTime = DateTime.Now;
                                theDateTime = theDateTime.AddTicks(-(theDateTime.Ticks % TimeSpan.TicksPerSecond));
                                conn2=new SqlConnection(CommonMethods.getConnectionString(ConfigurationManager.AppSettings["SROUpdateProvider"]));
                                conn2.Open();
                                cmd2=new SqlCommand("SRUpdateCalendarUpdate",conn2);
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Parameters.Add("@NewDate",SqlDbType.DateTime).Value=theDateTime;
                                cmd2.ExecuteNonQuery();
                            } catch {
                            } finally {
                                try {cmd2.Dispose();} catch {}
                                try {conn2.Close();} catch {}
                            }
                        }
                    }
                }
            } catch (Exception e) {
            } finally {
                try { cmd.Dispose(); } catch { }
                try { conn.Close(); } catch { }
            }
            /*
             * Now remove any deleted items
            */

            SqlConnection conn3 = null;
            SqlCommand cmd3 = null;
            try {
                conn3 = new SqlConnection(CommonMethods.getConnectionString(ConfigurationManager.AppSettings["SROCalendarProvider"]));
                cmd3 = new SqlCommand("SrCalendarGet", conn3);
                conn3.Open();
                cmd3.CommandType = CommandType.StoredProcedure;
                DataSet ds3 = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd3);
                da.Fill(ds3);
                if (CommonRoutines.HasData(ds3)) {
                    foreach (DataRow dr3 in ds3.Tables[0].Rows) {
                        int cal_id = CommonRoutines.ObjectToInt(dr3["cal_id"]);
                        string strCal_id = null;
                        try {
                            strCal_id = cal_ids[cal_id];
                        } catch { }
                        if (strCal_id == null) { // it's in SRCalendar, and it came from web_cal_entry , but it's not in
                            SqlConnection conn4 = null;
                            SqlCommand cmd4 = null;
                            try {
                                conn4 = new SqlConnection(CommonMethods.getConnectionString(ConfigurationManager.AppSettings["SROCalendarProvider"]));
                                conn4.Open();
                                cmd4 = new SqlCommand("SRCalendarDelete", conn4);
                                cmd4.CommandType = CommandType.StoredProcedure;
                                cmd4.Parameters.Add("@cal_id", SqlDbType.Int).Value = cal_id;
                                cmd4.ExecuteNonQuery();
                            } finally {
                                try { cmd4.Dispose(); } catch { }
                                try { conn4.Close(); } catch { }
                            }
                        }
                    }
                }
            } finally {
                try { cmd3.Dispose(); } catch { }
                try { conn3.Close(); } catch { }
            }


        }
        #endregion
        #region Private Interface
        private DataSet GetMySqlCalendar() {
            String query = "SELECT DISTINCT " +
                "webcal_entry.cal_id," +
                "webcal_entry.cal_date," +
                "webcal_entry.cal_time," +
                "webcal_entry.cal_name," +
                "webcal_entry.cal_location," +
                "webcal_entry.cal_url," +
                "webcal_entry.cal_description" +
             " FROM webcal_entry" +
                " LEFT OUTER JOIN webcal_entry_categories ON webcal_entry.cal_id = webcal_entry_categories.cal_id" +
                " LEFT OUTER JOIN webcal_entry_user ON webcal_entry.cal_id = webcal_entry_user.cal_id" +
             " WHERE cal_status='A' and cat_id=1";
            return CommonMethods.getDataSet(query, mSessionId);
        }
        #endregion

    }
}

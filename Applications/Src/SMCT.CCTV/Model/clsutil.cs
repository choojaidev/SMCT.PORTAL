using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SMCTPortal.Model
{
   
    public class clsutil { 
    public string key = "b14ca5898a4e4133bbce2ea2315a1916";
   
        //public string cnnSTR = System.Configuration.ConfigurationSettings.AppSettings["DRIVE"].ToString();
        //public System.Data.DataTable getRecords(string sql)
        //{
        //    System.Data.DataTable obj = new System.Data.DataTable();
        //    try
        //    {
        //        var cnDB = new System.Data.SqlClient.SqlConnection(cnnSTR);
        //        var cmdDB = new System.Data.SqlClient.SqlCommand(sql, cnDB);

        //        cnDB.Open();
        //        obj.Load(cmdDB.ExecuteReader());

        //        cmdDB.Dispose();
        //        cnDB.Dispose();

        //    }
        //    catch (System.Exception ex)
        //    {

        //    }
        //    return obj;
        //}
        //public string updateRecords(List<string> sql)
        //{
        //    var ret = "";

        //    var cmd = new SqlCommand();
        //    var conn = new SqlConnection();
        //    SqlTransaction trn;

        //    conn.ConnectionString = cnnSTR;
        //    conn.Open();
        //    trn = conn.BeginTransaction();
        //    cmd.Connection = conn;
        //    cmd.Transaction = trn;
        //    try
        //    {
        //        foreach (var itm in sql)
        //        {
        //            cmd.CommandText = itm;
        //            cmd.ExecuteNonQuery();
        //        }

        //    }
        //    catch (Exception ex) { ret = ex.Message.ToString(); }

        //    if (ret == "") { trn.Commit(); } else { trn.Rollback(); }

        //    return ret;
        //}
        //public string updateRecord(string sql)
        //{
        //    var ret = "";

        //    var cmd = new SqlCommand();
        //    var conn = new SqlConnection();
        //    SqlTransaction trn;

        //    conn.ConnectionString = cnnSTR;
        //    conn.Open();
        //    trn = conn.BeginTransaction();
        //    cmd.Connection = conn;
        //    cmd.Transaction = trn;
        //    try
        //    {
        //        cmd.CommandText = sql;
        //        cmd.ExecuteNonQuery();


        //    }
        //    catch (Exception ex) { ret = ex.Message.ToString(); }

        //    if (ret == "") { trn.Commit(); } else { trn.Rollback(); }

        //    return ret;
        //}

        public string getSysDate(bool time = false)
        {
            string ret = "";
            string yy = System.DateTime.Now.Year.ToString();
            string mm = ("0" + System.DateTime.Now.Month.ToString());
            string dd = ("0" + System.DateTime.Now.Day.ToString());
            string hh = ("0" + System.DateTime.Now.Hour.ToString());
            string min = ("0" + System.DateTime.Now.Minute.ToString());
            string sec = ("0" + System.DateTime.Now.Second.ToString());
            string msec = ("0" + System.DateTime.Now.Millisecond.ToString());
            mm = mm.Substring(mm.Length - 2, 2);
            dd = dd.Substring(dd.Length - 2, 2);
            hh = hh.Substring(hh.Length - 2, 2);
            min = min.Substring(min.Length - 2, 2);
            sec = sec.Substring(sec.Length - 2, 2);
            ret = yy + mm + dd;
            if (time) { ret += hh + min + sec; }

            return ret;

        }
        public string xencryp( string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        public string xdecryp( string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

}

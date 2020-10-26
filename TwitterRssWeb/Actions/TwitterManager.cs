using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TwitterRssWeb.Models;
using TwitterRssWeb.Actions;
using TwitterRssWeb.Controllers;
using System.Configuration;

namespace TwitterRssWeb.Actions
{
    public class TwitterManager
    {
        static string connection = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        public static OracleConnection dbConn = new OracleConnection(connection);
        //public static OracleConnection dbConn = new OracleConnection("Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=RSS;Password=Krcmst11079");
        //Oracle Veri Tabanına Bağlantısı
  
        public static async Task<Twitter> UptadeTwitter(Twitter guid)
        {
          

            var dbhour= guid.dbhour = DateTime.Now;
            var convert = Convert.ToInt32(guid.checkeded);

            dbConn.Open();

            OracleCommand update = new OracleCommand("UPDATE HTMLTABLE SET CHECKED= :checked , CHECKDATE= :checkdate  WHERE GUID= :guid ", dbConn);
            //Güncelleme komutu guid sutununda eşleşen veriyi checked ve checkdate sutununu güncellenmesi.

            update.Parameters.Add(":checked", convert);//Sorgumuzun Gerektirdiği verileri parametre şeklinde eklenmesi.
            update.Parameters.Add(":checkdate", dbhour);
            update.Parameters.Add(":guid", guid.guid);

            update.ExecuteNonQuery();

            Twitter result = new Twitter();
            result.guid = guid.guid;
            result.checkeded = guid.checkeded;
            result.dbhour = dbhour;

            dbConn.Close();
            return result;
        }
        public static async Task<List<Twitter>> GetTwitter()
        {
            var a = DateTime.Now.AddDays(-1);
            
            List<Twitter> TwitterList = new  List<Twitter>();

            var strQuery = @"Select GUID,PUBDATE,TITLE,CHECKED from HTMLTABLE WHERE PUBDATE>'"+a+ "'  Order BY PUBDATE DESC ";
            //Dün ve bugun atılan twitlerin getirilmesi için yazılan sql sorgusu.


            dbConn.Open();
            OracleCommand selectCommand = new OracleCommand(strQuery, dbConn);
            OracleDataAdapter adapter = new OracleDataAdapter(selectCommand);
            DataTable selectResults = new DataTable();
            
            var list = adapter.Fill(selectResults);
            OracleDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Twitter SelectedTwitter = new Twitter();
                SelectedTwitter.guid = reader["GUID"].ToString();//Veri tabanından istediğimiz sutunlardaki verileri tek tek çağırıp listelenmesi.
                SelectedTwitter.pubdate = Convert.ToDateTime(reader["PUBDATE"].ToString());
                SelectedTwitter.title = reader["TITLE"].ToString();
                SelectedTwitter.checkeded = Convert.ToInt32(reader["CHECKED"]);
                TwitterList.Add(SelectedTwitter);
            }
            

            dbConn.Close();

            
           return TwitterList;
        }



    }
}
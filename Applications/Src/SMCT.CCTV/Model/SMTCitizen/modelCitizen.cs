using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;

namespace SMCTPortal.Model.SMTCitizen
{
    public class modelCitizen
    {
        public string id { get; set; }
        public string citizenNo { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string FileName { get; set; }


         public string[] getAllData()
        {
            string directloc = @"./Data/Billboard/";

            // files list from the root directory and prints it
            string[] _files = Directory.GetFiles(directloc);
            return _files;

        }
        public List<modelCitizen> getData(string id)
        {
            clsutil _util = new clsutil();
            List<modelCitizen> modelCitizens = new List<modelCitizen>();
            //JObject o1 = JObject.Parse(System.IO.File.ReadAllText("./Data/Billboard/BB_dfdf_20230818165456.json"));

            // read JSON directly from a file
            try { 
            
            
            
            TextReader tr = File.OpenText("./Data/Billboard/BB_xx_20230821144643.json");
            StringReader strRead = new StringReader(tr.ReadToEnd());
            string str = strRead.ReadToEnd();
            string strEncryp = _util.xdecryp(str.ToString());
            Newtonsoft.Json.JsonSerializer jseri = new Newtonsoft.Json.JsonSerializer();
            List<modelCitizen> lsBillboard = new List<modelCitizen>();
            using (TextReader sr = new StringReader(strEncryp))
            {
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    //var _Serializer = new Newtonsoft.Json.JsonSerializer();
                    modelCitizen itm = new modelCitizen();
                    if (itm.id == null) { itm.id = o2.GetValue("id")?.ToString(); }
                    //if (itm.ItemCreateBy == null) { itm.ItemCreateBy = o2.GetValue("ItemCreateBy")?.ToString(); }
                    //if (itm.ItemCreateDate == null) { itm.ItemCreateDate = o2.GetValue("ItemCreateDate")?.ToString(); }
                    // if (itm.ItemEndDate == null) { itm.ItemEndDate = o2.GetValue("ItemEndDate")?.ToString(); }
                    //if (itm.ItemImg == null) { itm.ItemImg = o2.GetValue("ItemImg")?.ToString(); }
                    //if (itm.ItemIsActive == null) { itm.ItemIsActive = o2.GetValue("ItemIsActive")?.ToString(); }
                    //if (itm.ItemLink == null) { itm.ItemLink = o2.GetValue("ItemLink")?.ToString(); }
                    //if (itm.ItemModifyDate == null) { itm.ItemModifyDate = o2.GetValue("ItemModifyDate")?.ToString(); }
                    //if (itm.ItemName == null) { itm.ItemName = o2.GetValue("ItemName")?.ToString(); }
                    //if (itm.ItemNo == null) { itm.ItemNo = o2.GetValue("ItemNo")?.ToString(); }
                    //if (itm.ItemStartDate == null) { itm.ItemStartDate = o2.GetValue("ItemStartDate")?.ToString(); }
                    //if (itm.ItemTitle == null) { itm.ItemTitle = o2.GetValue("ItemTitle")?.ToString(); }
               
                    modelCitizens.Add(itm);
                }
                //    JObject json = JObject.Parse(str);
                //if (json.Count > 0) { 

                //}
                //JsonTextReader reader = new JsonTextReader(strEncryp);
                //           object parsedData = jseri.Deserialize(reader);
            }








                //    


                //using (StreamReader file = System.IO.File.OpenText("./Data/Billboard/BB_dfdf_20230818165456.json"))
                //using (JsonTextReader reader = new JsonTextReader(file))
                //{
                //    reader = _util.xdecryp(reader);

                //    JObject o2 = (JObject)JToken.ReadFrom(reader);
                //    //var _Serializer = new Newtonsoft.Json.JsonSerializer();
                //    modelCitizen itm = new modelCitizen();
                //    if (itm.id == null) { itm.id = o2.GetValue("id")?.ToString(); }
                //    if (itm.ItemCreateBy == null) { itm.ItemCreateBy = o2.GetValue("ItemCreateBy")?.ToString(); }
                //    if (itm.ItemCreateDate == null) { itm.ItemCreateDate = o2.GetValue("ItemCreateDate")?.ToString(); }
                //    //   if (itm.ItemDTE == null) { itm.ItemDTE = o2.GetValue("ItemDTE")?.ToString(); }
                //    if (itm.ItemEndDate == null) { itm.ItemEndDate = o2.GetValue("ItemEndDate")?.ToString(); }
                //    if (itm.ItemImg == null) { itm.ItemImg = o2.GetValue("ItemImg")?.ToString(); }
                //    if (itm.ItemIsActive == null) { itm.ItemIsActive = o2.GetValue("ItemIsActive")?.ToString(); }
                //    if (itm.ItemLink == null) { itm.ItemLink = o2.GetValue("ItemLink")?.ToString(); }
                //    if (itm.ItemModifyDate == null) { itm.ItemModifyDate = o2.GetValue("ItemModifyDate")?.ToString(); }
                //    if (itm.ItemName == null) { itm.ItemName = o2.GetValue("ItemName")?.ToString(); }
                //    if (itm.ItemNo == null) { itm.ItemNo = o2.GetValue("ItemNo")?.ToString(); }
                //    if (itm.ItemStartDate == null) { itm.ItemStartDate = o2.GetValue("ItemStartDate")?.ToString(); }
                //    if (itm.ItemTitle == null) { itm.ItemTitle = o2.GetValue("ItemTitle")?.ToString(); }
                //    //   if (itm.id == null) { itm.id = o2.GetValue("id")?.ToString(); }
                //    modelCitizens.Add(itm);

                //}
            }
            catch { }
            return modelCitizens;
        }
        public static void saveData(modelCitizen item)
        {
            clsutil _util = new clsutil();
            // =============================================================================

            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            string fileName = "BB_" + item.id + "_" + _util.getSysDate(true) + ".json";
            item.FileName = fileName;
            string data = System.Text.Json.JsonSerializer.Serialize(item);
            data = _util.xencryp(data);
            try
            {
                ostrm = new FileStream("./Data/Citizen/" + fileName, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            Console.WriteLine(data);
            //Console.WriteLine("This is a line of text");
            //Console.WriteLine("Everything written to Console.Write() or");
            //Console.WriteLine("Console.WriteLine() will be written to a file");
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");

             
        }
    }
}

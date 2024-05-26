using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using SMCTPortal.Model;
using System.Collections.Generic;
using System;
using SMCTPortal.Model.SMTCitizen;

namespace SmartCon5.Models
{
    public class modelCitizenMaster
    {
        public string ItemId { get; set; }
        public string ItemFileName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemGroup { get; set; }
        public string ItemIsActive { get; set; }
        public string ItemCreateBy { get; set; }
        public string ItemCreateDate { get; set; }
        public string ItemModifyBy { get; set; }
        public string ItemModifyDate { get; set; }

        public List<modelCitizenMaster> getMasterData()
        {      List<modelCitizenMaster> lsMST = new List<modelCitizenMaster>();
            try {
            clsutil _util = new clsutil();
            TextReader tr = File.OpenText("./Data/CitizenData/CT_Master.json");
            StringReader strRead = new StringReader(tr.ReadToEnd());
            string str = strRead.ReadToEnd();
            string strEncryp = str;// _util.xdecryp(str.ToString());
            Newtonsoft.Json.JsonSerializer jseri = new Newtonsoft.Json.JsonSerializer();
      
            using (TextReader sr = new StringReader(strEncryp))
            {
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    //var _Serializer = new Newtonsoft.Json.JsonSerializer();
                    modelCitizenMaster itm = new modelCitizenMaster();
                    itm.ItemId = o2.GetValue("ItemId")?.ToString();
                    itm.ItemGroup = o2.GetValue("ItemGroup")?.ToString();
                    itm.ItemFileName = o2.GetValue("ItemFileName")?.ToString();
                    itm.ItemCreateBy = o2.GetValue("ItemCreateBy")?.ToString();
                    itm.ItemCreateDate = o2.GetValue("ItemCreateDate")?.ToString();
                    itm.ItemDescription = o2.GetValue("ItemDescription")?.ToString();
                    itm.ItemModifyDate = o2.GetValue("ItemModifyDate")?.ToString();
                    itm.ItemModifyBy = o2.GetValue("ItemModifyBy")?.ToString();
                    itm.ItemIsActive = o2.GetValue("ItemIsActive")?.ToString();
                        
                    lsMST.Add(itm);
                }
            }
            tr.Close();
            strRead.Dispose();
            } catch (Exception ex) { }
            return lsMST;
        }
        public  void SaveMaster(modelCitizen item)
        {
            // Master Section
            clsutil _util = new clsutil();
            List<modelCitizenMaster> existingMaster = getMasterData();


            FileStream MSTostrm;
            StreamWriter MSTwriter;
            TextWriter MSToldOut = Console.Out;
            string MSTfileName = "BB_Master.json";

            modelCitizenMaster mst = new modelCitizenMaster();
            mst.ItemCreateBy = "";
            if (mst.ItemCreateDate == null) mst.ItemCreateDate = _util.getSysDate(true);
            mst.ItemModifyBy = "";
            mst.ItemModifyDate = _util.getSysDate(true);
            mst.ItemFileName = item.FileName;
            // End Master Section
            string data = System.Text.Json.JsonSerializer.Serialize(mst);
           
            // existing
            try
            {
                if (existingMaster.Count > 0)
                {
                    string exist = System.Text.Json.JsonSerializer.Serialize(existingMaster);
                    data = exist.Replace("[","").Replace("]","").ToString () + data;
                }
            }
            catch (Exception ex) { }

            try
            {
                MSTostrm = new FileStream("./Data/Billboard/" + MSTfileName, FileMode.OpenOrCreate, FileAccess.Write);              

                MSTwriter = new StreamWriter(MSTostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(MSTwriter);

            //   data = _util.xencryp(data);

            Console.WriteLine(data);

            Console.SetOut(MSToldOut);
            MSTwriter.Close();
            MSTostrm.Close();
            Console.WriteLine("Done");
        }

    }
}

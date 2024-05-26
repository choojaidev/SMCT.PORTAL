using System.Collections.Generic;

namespace SMCTPortal.Model.SMAdditional
{
    public class Lotterys
    {
        public class resContent
        {
            public contentData response { get; set; }
        }
        public class contentData
        {
            public string statusMessage { get; set; }
            public int statusCode { get; set; }
            public string message { get; set; }
            public bool status { get; set; }
            public reponseData response { get; set; }
        }
        public class reponseData
        {
            public string youtube_url { get; set; }
            public string pdf_url { get; set; }
            public string date { get; set; }
            public List<int> period { get; set; }
            public string ramark { get; set; }
            public int status { get; set; }
            public string sheetId { get; set; }
            public data data { get; set; }
        }
        public class data
        {
            public dataEntity first { get; set; }
            public dataEntity second { get; set; }
            public dataEntity third { get; set; }
            public dataEntity fourth { get; set; }
            public dataEntity fifth { get; set; }
            public dataEntity last2 { get; set; }
            public dataEntity last3f { get; set; }
            public dataEntity last3b { get; set; }
            public dataEntity near1 { get; set; }



        }
        public class dataEntity
        {
            public string price { get; set; }
            public List<dataNumber> number { get; set; }
        }
        public class dataNumber
        {
            public int round { get; set; }
            public string value { get; set; }
        }

    }
}

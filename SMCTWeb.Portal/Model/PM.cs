using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using System;
using System.Data.Common;

namespace SMCTWebTemplate.Model
{
    public class PMResponse
    {
        public Data data { get; set; }
    }
    public class Data { 
        public Current current { get; set; }
    }
    public class Current
    {
        public Pollution pollution { get; set; }
        public Weather weather { get; set; }

    }
    public class Pollution {
        public DateTime ts { get; set; }
        public double aqius { get; set; }
        public string mainus { get; set; }
        public double aqicn { get; set; }
        public string maincn { get; set; }
    }
    public class Weather
    {
        public DateTime ts { get; set; }
        public double tp { get; set; }
        public string pr { get; set; }
        public double hu { get; set; }
        public double ws { get; set; }
        public double wd { get; set; }
        public string ic { get; set; }
    }
}

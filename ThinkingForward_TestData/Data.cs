using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkingForward_TestData
{
    public class Data
    {
        private Data s;

        public Data()
        {

        }

        public Data(Data s)
        {
            this.Latitude = s.Latitude;
            this.Longitude = s.Longitude;
            this.Geohash = s.Geohash;
            this.Prefix = s.Prefix;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Geohash { get; set; }
        public string Prefix { get; set; }
    }
}

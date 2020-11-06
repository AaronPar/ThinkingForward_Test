using GeoHash.Net.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingForward_TestData
{
    public static class Utils
    {
        public static async Task<List<Data>> ObtainValues(string path)
        {
            List<Data> result = new List<Data>();
            var encoder = new GeoHashEncoder<string>();
            double val1;
            double val2;
            foreach (var line in await File.ReadAllLinesAsync(path))
            {
                Data dataElement = new Data();
                var values = line.Split(",");
                if (Double.TryParse(values[0], out val1) && Double.TryParse(values[0], out val2))
                {
                    dataElement.Latitude = double.Parse(values[0]);
                    dataElement.Longitude = double.Parse(values[1]);
                    dataElement.Geohash = encoder.Encode(double.Parse(values[0]), double.Parse(values[1]), GeoHash.Net.Utilities.Enums.GeoHashPrecision.MaximumPrecision);
                    result.Add(dataElement);
                }
            }
            return result;
        }

        public static void CreateFile(List<Data> FL, string CompTime)
        {
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string path = Path.Combine(solutiondir, @"ThinkingForwardTestFile.txt");
            TextWriter tw = new StreamWriter(path);

            tw.WriteLine(CompTime);
            foreach (var item in FL)
            {
                tw.WriteLine(item.Latitude+","+item.Longitude+","+item.Geohash+","+item.Prefix);
            }

            tw.Close();
        }
    }
}

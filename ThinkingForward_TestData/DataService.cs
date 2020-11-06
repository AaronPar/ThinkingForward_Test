using Microsoft.AspNetCore.Hosting;
using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingForward_TestData
{
    public class DataService : IDataService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public DataService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<int> GetCount()
        {
            var data = await GetData();
            return data.Count;
        }

        public async Task<List<Data>> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = await GetData();
            return data.OrderBy(d => d.Geohash).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<Data>> GetResults()
        {
            return await GetData();
        }

        private async Task<List<Data>> GetData()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            ShortestPrefixCalculator SPC = new ShortestPrefixCalculator();
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string path = Path.Combine(solutiondir, @"ThinkingForward_Test\ThinkingForward_TestData\File\test_points.txt");

            List<Data> results = await Utils.ObtainValues(path);

            results = results.OrderByDescending(x => x.Geohash).ToList();

            results = results.DistinctBy(x => x.Geohash).ToList();

            List<string> prefixList = SPC.findPrefixes(results.Select(x => x.Geohash).ToList(), results.Select(x => x.Geohash).ToList().Count());

            prefixList = (prefixList.OrderByDescending(x => x)).ToList();

            for (int i = 0; i < results.Count(); i++)
            {
                results[i].Prefix = prefixList[i];
            }

            sw.Stop();

            Utils.CreateFile(results,sw.Elapsed.ToString());

            return results;
        }
    }
}

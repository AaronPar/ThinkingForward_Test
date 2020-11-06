using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingForward_TestData
{
    public interface IDataService
    {
        Task<List<Data>> GetResults();
        Task<List<Data>> GetPaginatedResult(int currentPage, int pageSize = 10);
        Task<int> GetCount();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ThinkingForward_TestData;

namespace ThinkingForward_TestUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IDataService _dataService;

        public PaginatedList<Data> Data { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var data = await _dataService.GetResults();

            Data = PaginatedList<Data>.Create(data.Select(s => new Data(s)), pageIndex ?? 1, 100, 5);

            return Page();
        }

    }
}

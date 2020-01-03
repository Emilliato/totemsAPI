using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Totems.Models;
using Totems.Services;

namespace TotemsAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private JsonFileTotemService totemService;
        public IEnumerable<Totem> Totems { get; private set; }
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileTotemService totemService
            )
        {
            _logger = logger;
            this.totemService = totemService;
        }

        public void OnGet()
        {
            Totems = totemService.GetTotems();
        }
    }
}

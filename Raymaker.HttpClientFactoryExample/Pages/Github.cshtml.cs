using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Raymaker.HttpClientFactoryExample.Pages
{
    public class Account
    {        
        public string AccountInfo { get; set; }
        public string AccountName { get; set; }
    }

    public class GithubModel : PageModel
    {
        private readonly ILogger<GithubModel> _logger;
        
        public IHttpClientFactory HttpClientFactory;

        [BindProperty]
        public Account Account { get; set; }

        public GithubModel(ILogger<GithubModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            HttpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            await LoadAccount();
            return Page();
        }

        public async Task LoadAccount()
        {
            var httpClient = HttpClientFactory.CreateClient("GitHub");

            Account.AccountInfo = await httpClient.GetStringAsync($"users/{Account.AccountName}");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Raymaker.HttpClientFactoryExample.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Raymaker.HttpClientFactoryExample.Pages
{
    public class GithubModel : PageModel
    {
        private readonly ILogger<GithubModel> _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public Account Account { get; set; }

        public GithubModel(ILogger<GithubModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await LoadAccount();
            return Page();
        }

        private async Task LoadAccount()
        {
            var httpClient = _httpClientFactory.CreateClient("GitHub");

            Account.AccountInfo = await httpClient.GetStringAsync($"users/{Account.AccountName}");
        }
    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Raymaker.HttpClientDotnetFramework
{
    public class ManagerWithFactory : IManager
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IConfig config;
        
        public ManagerWithFactory(IHttpClientFactory clientFactory, IConfig config)
        {
            this.clientFactory = clientFactory;
            this.config = config;
        }

        public async Task Run()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "users/MunroRaymaker");
            //request.Content = new StringContent("hello world");
            
            var client = this.clientFactory.CreateClient("GitHub");

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}

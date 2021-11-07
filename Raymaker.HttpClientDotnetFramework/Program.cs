using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Raymaker.HttpClientDotnetFramework
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (new Bootstrapper())
            {
                var mgr = Bootstrapper.ServiceProvider.GetRequiredService<IManager>();

                await mgr.Run();
            }
        }
    }
}

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Raymaker.HttpClientDotnetFramework
{
    public class HmacAuthenticationHandler : DelegatingHandler
    {
        private readonly string _secret;

        public HmacAuthenticationHandler(IConfig config)
        {
            _secret = config.HmacKey;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content == null)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            // Adds current date in UTC
            //request.Headers.Date = new DateTimeOffset(DateTime.Now, DateTime.Now - DateTime.UtcNow);

            // Gets a string representation of the request
            var canonicalRepresentation = await request.Content.ReadAsStringAsync();

            // Adds HMAC authorization header
            string hmacSignature = HmacSignatureCalculator.Signature(_secret, canonicalRepresentation);
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(hmacSignature);

            // Expect continue 100 header must not be set or request will fail
            request.Headers.ExpectContinue = false;

            // Sends the actual request and awaits the result
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }

}

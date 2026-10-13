using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreRateLimit
{
    public class ClientHeaderResolveContributor : IClientResolveContributor
    {
        private readonly string _headerName;
        private readonly List<string> _trustedProxies;

        public ClientHeaderResolveContributor(string headerName) : this(headerName, null)
        {
        }

        public ClientHeaderResolveContributor(string headerName, List<string> trustedProxies)
        {
            _headerName = headerName;
            _trustedProxies = trustedProxies;
        }

        public Task<string> ResolveClientAsync(HttpContext httpContext)
        {
            string clientId = null;

            if (!IpAddressUtil.IsTrustedProxy(_trustedProxies, httpContext))
            {
                return Task.FromResult(clientId);
            }

            if (httpContext.Request.Headers.TryGetValue(_headerName, out var values))
            {
                clientId = values.First();
            }

            return Task.FromResult(clientId);
        }
    }
}
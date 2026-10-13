using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AspNetCoreRateLimit
{
    public class IpHeaderResolveContributor : IIpResolveContributor
    {
        private readonly string _headerName;
        private readonly List<string> _trustedProxies;

        public IpHeaderResolveContributor(
            string headerName) : this(headerName, null)
        {
        }

        public IpHeaderResolveContributor(
            string headerName,
            List<string> trustedProxies)
        {
            _headerName = headerName;
            _trustedProxies = trustedProxies;
        }

        public string ResolveIp(HttpContext httpContext)
        {
            IPAddress clientIp = null;

            if (!IpAddressUtil.IsTrustedProxy(_trustedProxies, httpContext))
            {
                return null;
            }

            if (httpContext.Request.Headers.TryGetValue(_headerName, out var values))
            {
                var hops = values.Last().Split(',');
                clientIp = IpAddressUtil.ParseIp(hops[hops.Length - 1]);
            }

            return clientIp?.ToString();
        }
    }
}
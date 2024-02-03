using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Tenant
{
    public sealed class TenantProvider
    {
        private const string TenantHeaderName = "X-Tenant";
        private readonly IHttpContextAccessor _contextAccessor;
        public TenantProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetTenantId()
        {
            //if (_contextAccessor.HttpContext is null)
            //    return new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"); //default tenant id
            var tenantHeader = _contextAccessor.HttpContext?.Request.Headers[TenantHeaderName];
            if (!tenantHeader.HasValue || !Guid.TryParse(tenantHeader.Value, out Guid tenantId) || !Tenants.All.Contains(tenantId))
            {
                // throw new ApplicationException("Tenant header is not found !");
                return Guid.Empty;
            }

            return tenantId;
        }
    }
}

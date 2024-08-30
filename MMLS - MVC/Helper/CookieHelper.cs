
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;

namespace MMLS___MVC.Helper
{
    public class CookieHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _protector;

        public CookieHelper(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _protector = dataProtectionProvider.CreateProtector("CookieProtection");
        }

    }
}

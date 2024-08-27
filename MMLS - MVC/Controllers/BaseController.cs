using BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace MMLS___MVC.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Kiểm tra cookie
            var accountJson = HttpContext.Request.Cookies["Account"];

            if (!string.IsNullOrEmpty(accountJson))
            {
                // Nếu cookie tồn tại, giải mã thông tin tài khoản từ cookie
                var account = JsonConvert.DeserializeObject<AccountDTO>(accountJson);
                string? loginType = "Cookie Login - Remember: True"; // Có thể điều chỉnh nếu cần

                ViewBag.IsLoggedIn = true;
                ViewBag.Account = account;
                ViewBag.LoginType = loginType;

                // Bạn có thể đặt lại session nếu muốn
                HttpContext.Session.SetString("Account", accountJson);
                HttpContext.Session.SetString("LoginType", loginType);
            }
            else
            {
                // Kiểm tra session nếu cookie không tồn tại
                var sessionAccountJson = HttpContext.Session.GetString("Account");

                if (!string.IsNullOrEmpty(sessionAccountJson))
                {
                    var account = JsonConvert.DeserializeObject<AccountDTO>(sessionAccountJson);
                    string? loginType = HttpContext.Session.GetString("LoginType");

                    ViewBag.IsLoggedIn = true;
                    ViewBag.Account = account;
                    ViewBag.LoginType = loginType;
                }
                else
                {
                    ViewBag.IsLoggedIn = false;
                }
            }

            // Gọi lớp cơ sở để tiếp tục xử lý action
            base.OnActionExecuting(context);
        }

    }
}

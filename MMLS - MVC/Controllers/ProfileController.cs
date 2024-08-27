using BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MMLS___MVC.Controllers
{
    public class ProfileController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {

            var accountJson = HttpContext.Session.GetString("Account");
            if (!string.IsNullOrEmpty(accountJson))
            {
                var account = JsonConvert.DeserializeObject<AccountDTO>(accountJson);
                ViewBag.Account = account;
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        [Route("Sign-out")]
        public IActionResult SignOut()
        {
            // Xóa cookie "Account"
            Response.Cookies.Delete("Account");

            // Thực hiện các bước đăng xuất khác như xóa session
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [Route("Clear-session")]
        public IActionResult ClearSession()
        {
            // Thực hiện các bước đăng xuất khác như xóa session
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

    }
}

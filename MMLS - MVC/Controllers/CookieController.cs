using AutoMapper;
using BLL.Models;
using BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MMLS___MVC.Controllers
{
    public class CookieController : BaseController
    {
        private readonly ZP1_MMLSContext _context;
        private readonly IMapper _mapper;

        public CookieController(ZP1_MMLSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var accountJson = HttpContext.Session.GetString("Account");
            if (!string.IsNullOrEmpty(accountJson))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string txtUsername, string txtPassword)
        {

            string hashedPassword = Utils.HashPassword.GetHashedPassword(txtPassword);

            var account = _context.Accounts
                .Where(a => a.Username == txtUsername && a.Password == hashedPassword)
                .Include(a => a.Role)
                .FirstOrDefault();

            bool rememberMe = Request.Form["remember"] == "true";

            if (account != null)
            {
                var AccountDTO = _mapper.Map<AccountDTO>(account);

                var accountJson = JsonConvert.SerializeObject(AccountDTO);
                HttpContext.Session.SetString("Account", accountJson);
                HttpContext.Session.SetString("LoginType", "Cookie Login - Remember: " + rememberMe);

                if (rememberMe)
                {
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(1), // Cookie sẽ hết hạn sau 7 ngày
                        IsEssential = true, // Đảm bảo cookie được gửi
                        HttpOnly = true,    // Cookie chỉ có thể truy cập qua HTTP
                        Secure = true,      // Cookie chỉ được gửi qua HTTPS
                    };

                    // Lưu thông tin người dùng vào cookie dưới dạng JSON
                    Response.Cookies.Append("Account", accountJson, cookieOptions);
                    Response.Cookies.Append("LoginType", "Cookie Login - Remember: " + rememberMe);
                }


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginValidation = "Username or Password Is Not Valid";
                return View();
            }


        }

    }
}

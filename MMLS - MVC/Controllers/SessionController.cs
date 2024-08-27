using AutoMapper;
using BLL.DTO;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MMLS___MVC.Controllers
{
    public class SessionController : BaseController
    {
        private readonly ZP1_MMLSContext _context;
        private readonly IMapper _mapper;

        public SessionController(ZP1_MMLSContext context, IMapper mapper)
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

            if (account != null)
            {
                var AccountDTO = _mapper.Map<AccountDTO>(account);

                var accountJson = JsonConvert.SerializeObject(AccountDTO);
                HttpContext.Session.SetString("Account", accountJson);
                HttpContext.Session.SetString("LoginType", "Session Login");

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

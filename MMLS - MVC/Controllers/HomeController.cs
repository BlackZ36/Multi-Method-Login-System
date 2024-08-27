using BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MMLS___MVC.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

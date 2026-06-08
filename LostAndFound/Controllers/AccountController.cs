using LostAndFound.Data;
using LostAndFound.Models;
using LostAndFound.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AccountController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            var user = dbContext.Users.FirstOrDefault(x =>
                x.StudentNumber == viewModel.StudentNumber &&
                x.Password == viewModel.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid login";
                return View();
            }

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("StudentNumber", user.StudentNumber);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("List", "Items");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Guest()
        {
            return RedirectToAction("List", "Items");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimskaLiga.Models;
using ZimskaLiga.services;

namespace ZimskaLiga.Controllers
{
    public class LayoutController :Controller
    {
        public List<LogInModel> LoginModels = new();

        public IActionResult Index(Int32 IdUser)
        {
            ViewBag.UserName = IdUser;
            ViewBag.TimeUser = UsersServices.GetTime(IdUser);
            ViewBag.Placment = UsersServices.GetPlacment(IdUser);

            ViewBag.LoginModels = UsersServices.GetAllUsers();

            return View();
        }
    }
}

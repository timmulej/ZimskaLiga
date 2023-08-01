using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimskaLiga.Models;
using ZimskaLiga.repository;
using ZimskaLiga.services;
using ZimskaLiga.ViewModels;

namespace ZimskaLiga.Controllers
{
    public class MeasureController : Controller
    {

        private List<LogInModel> _loginModels;

        private LogInModel _loginModel;


        public MeasureController()
        {
            _loginModels = UsersServices.GetAllUsers();
            _loginModel = _loginModels.First();
    
        }

        public IActionResult Index()
        {
            MeasureUsersModel measureUsersModel = new MeasureUsersModel(_loginModels,_loginModel);
            return View(measureUsersModel);
        }


    }
}

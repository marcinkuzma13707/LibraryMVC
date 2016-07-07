using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebModelService.Interfaces;
using WebModelService.ViewModel;
using WebModelService;
using Autofac;

namespace LibraryMVC.Controllers
{
    public class UserController : Controller
    {
        //public UserController() { }

        private IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }   
    
        public ActionResult Index()
        {
            List<UserViewModel> list = _userService.UserList();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            UserViewModel viewModel = new UserViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddUser(UserViewModel newUser)
        {

            if (_userService.DoEmailExist(newUser.Email))
            {
                ModelState.AddModelError("UniqueEmail", "Email nie jest unikalny");
            }

            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            _userService.AddUser(newUser);
            return RedirectToAction("Index", "User");

        }

        public ActionResult UserDetails(int userId)
        {
            var viewModel = _userService.UserDetails(userId);
            return View(viewModel);
        }

        public ActionResult EditUser(int userId)
        {
            var viewModel = _userService.GetUser(userId);
            return View(viewModel);
        }

        public ActionResult DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToAction("Index","User");
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel viewModel)
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel = _userService.GetUser(viewModel.UserId);
            string email = _userService.GetUser(viewModel.UserId).Email;

            if (_userService.DoEmailExist(viewModel.Email)&&!viewModel.Email.Equals(email))
            {
                ModelState.AddModelError("UniqueEmail", "Email nie jest unikalny");
            }
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _userService.UserEdit(viewModel);
             return RedirectToAction("Index", "User");
      
        }

    }
}
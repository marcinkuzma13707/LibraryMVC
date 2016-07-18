using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebModelService.BorrowModel.Contract.ViewModel;
using WebModelService.Interfaces;
using WebModelService.ViewModel;

namespace LibraryMVC.Controllers
{
    public class BorrowController : Controller
    {
        private IBorrowService _borrowService;
        private IUserService _userService;
            public BorrowController(IBorrowService borrowService, IUserService userService)
        {
            this._borrowService = borrowService;
            this._userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetBorrowedBooks([DataSourceRequest] DataSourceRequest request)
        {
            var data = _borrowService.GetBorrowedBooks();
            return Json(data.ToDataSourceResult(request));
        }
        [HttpPost]
        public ActionResult GetUsersWithBooks([DataSourceRequest] DataSourceRequest request)
        {
            var data = _borrowService.GetUsersWithBooks();
            return Json(data.ToDataSourceResult(request));
        }
        [HttpGet]
        public ActionResult AddNewBorrow()
        {
            NewBorrowViewModel newBorrow = new NewBorrowViewModel();
            return PartialView(newBorrow);
        }
        [HttpPost]
        public ActionResult AddNewBorrow(NewBorrowViewModel newBorrow)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(newBorrow);
            }
            _borrowService.AddNewBorrow(newBorrow);
            return Json(new { success = true });
        }
        [HttpGet]
        public ActionResult GetUsers()
        {
            var userList = _userService.UserList();
            return Json(userList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAvailableBooks()
        {
            var availableList = _borrowService.GetAvaibleBooks();
            return Json(availableList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void ReturnBook(int borrowId)
        {
            _borrowService.ReturnBook(borrowId);
        }
        [HttpGet]
        public ActionResult UserBooks(int id)
        {
            UserViewModel user = new UserViewModel();
            user = _borrowService.GetUserDetails(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult UserBooks(int[] booksToReturn)
        {   if(booksToReturn!=null)
            _borrowService.ReturnBooks(booksToReturn);
            return Json(new { success= true });
        }
    }
}
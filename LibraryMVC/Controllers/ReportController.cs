using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LibraryDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebModelService.BookModel.Contract.ViewModel;
using WebModelService.DictBookGenreModel.Contract.ViewModel;
using WebModelService.Interfaces;
using WebModelService.ReportModel.Contract.ViewModel;
using WebModelService.ViewModel;

namespace LibraryMVC.Controllers
{
    public class ReportController : Controller
    {
        private IReportService _reportService;
        private IBookService _bookService;
        public ReportController(IReportService reportService, IUserService userService, IBookService bookService)
        {
            this._reportService = reportService;
            this._bookService = bookService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MostActiveUsers()
        {
            List<UserViewModel> users = _reportService.GetUserList();
            return View(users);
        }
        [HttpPost]
        public ActionResult Users_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = _reportService.GetUserList();
            return Json(data.ToDataSourceResult(request));
        }
        [HttpGet]
        public ActionResult MostBorrowedBooks()
        {
            IQueryable<MostBorrowedBookViewModel> books = _reportService.GetBookList();
            return View(books);
        }
        [HttpPost]
        public ActionResult Books_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = _reportService.GetBookList();
            return Json(data.ToDataSourceResult(request));
        }
    
        [HttpGet]
        public ActionResult GetGenres()
        {
            var genreList = _bookService.GenreList();
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }
      
      
       public JsonResult GetGenres2(string text)
       {
           var genreList = _bookService.GenreList().AsEnumerable();

            if (!string.IsNullOrEmpty(text))
                {
                    genreList = genreList.Where(g => g.Name.Contains(text));
                }

            return Json(genreList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Books_Read2(string title)
        {
            var data = _reportService.GetBookList().AsEnumerable();
            if (!string.IsNullOrEmpty(title))
            {
                data = data.Where(t=>t.Title.Contains(title));
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
  
}
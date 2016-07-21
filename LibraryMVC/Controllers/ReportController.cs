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
        [HttpGet]
        public ActionResult Books_Read()
        {
            var data = _reportService.GetBookList();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadBooksToGrid(string title, string genre, string dateFrom, string dateTo)
        {
            DateTime? minDate = null;
            DateTime? maxDate = null;
            if (!string.IsNullOrEmpty(dateFrom))
            {
                minDate = DateTime.Parse(dateFrom);
            }
            if (!string.IsNullOrEmpty(dateTo))
             { 
                 maxDate = DateTime.Parse(dateTo);
             }
            int genreId;
            int.TryParse(genre,out genreId);
            IQueryable < MostBorrowedBookViewModel >request = _reportService.GetFilteredBooks(title, genreId,minDate,maxDate);
            return Json(request, JsonRequestBehavior.AllowGet);
        }
      
        [HttpGet]
        public ActionResult GetGenres()
        {
            var genreList = _bookService.GenreList();
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }
      [HttpGet]
        public JsonResult GetTitles(string title)
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
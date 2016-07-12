using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebModelService.BookModel.Contract.ViewModel;
using WebModelService.Interfaces;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Books_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_bookService.BookList().ToDataSourceResult(request));
        }
        public ActionResult GetGenres()
        {
            var genreList = _bookService.GenreList();
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int id)
        {
            BookDetailsViewModel book = _bookService.GetBook(id);
            return PartialView(book);
        }
        [HttpGet]
        public ActionResult AddNewBook()
        {
            AddBookModel newBook = new AddBookModel();
            return PartialView(newBook);
        }
        [HttpPost]
        public ActionResult AddNewBook(AddBookModel newBook)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(newBook);
            }
            _bookService.AddBook(newBook);
            return Json(new { success = true });
        }
        [HttpGet]
        public ActionResult EditBook(int id)
        {
            return PartialView(_bookService.FindBook(id));
        }
        [HttpPost]
        public ActionResult EditBook(EditBookModel editedBook)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(editedBook);
            }
            _bookService.EditBook(editedBook);
            return Json(new { success = true });
        }

    }
}
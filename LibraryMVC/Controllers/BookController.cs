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

        [HttpGet]
        public ActionResult AddBook()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(AddBookModel newBook)
        {
            if (!ModelState.IsValid)
            {
                return View(newBook);
            }

            //_bookService.AddBook(newBook);

            return Json(new { success = true });
        }


    }
}
using LibraryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BookModel.Contract.ViewModel;
using WebModelService.Interfaces;

namespace WebModelService.BookModel
{
    public class BookService : IBookService
    {
        private Library _library;
        public BookService(Library library)
        {
            this._library = library;

        }


        public List<BookViewModel> BookList()
        {
            throw new NotImplementedException();
        }
    }
}

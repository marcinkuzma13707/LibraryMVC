using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BookModel.Contract.ViewModel;
using WebModelService.DictBookGenreModel.Contract.ViewModel;

namespace WebModelService.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookViewModel> BookList();
        void AddBook(AddBookModel newBook);
        List<DictBookGenreViewModel> GenreList();
        BookDetailsViewModel GetBook(int bookId);
        EditBookModel FindBook(int bookId);
        void EditBook(EditBookModel editedBook);
    }
}

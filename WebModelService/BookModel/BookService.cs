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

        public void AddBook(AddBookModel newBook)
        {
            Book book = new Book
            {
                Title = newBook.Title,
                ReleaseDate = newBook.ReleaseDate,
                ISBN = newBook.ISBN,
                Count = newBook.Count,
                Author = newBook.Author,
                AddDate = DateTime.Now,
                DictBookGenre = (from genres in _library.DictBookGenre where genres.Name == newBook.BookGenreName select genres).SingleOrDefault()
            };
            _library.Book.Add(book);
            _library.SaveChanges();
        }

        public IEnumerable<BookViewModel> BookList()
        {
            var query = (from book in _library.Book
                         select new BookViewModel
                         {
                             BookId = book.BookId,
                             Author = book.Author,
                             Title = book.Title,
                             ReleaseDate = book.ReleaseDate,
                             ISBN = book.ISBN,
                             BookGenreName = (from genre in _library.DictBookGenre where book.BookGenreId == genre.BookGenreId select genre.Name).FirstOrDefault(),
                             Count=book.Count,
                             AddDate=book.AddDate,
                             ModifiedDate=book.ModifiedDate
                         });
            return query;
        }
    }
}

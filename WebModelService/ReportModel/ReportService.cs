using LibraryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BookModel.Contract.ViewModel;
using WebModelService.Interfaces;
using WebModelService.ReportModel.Contract.ViewModel;
using WebModelService.ViewModel;

namespace WebModelService.ReportModel
{
    public class ReportService : IReportService
    {
        private Library _library;
        public ReportService(Library library)
        {
            this._library = library;
        }


        public IQueryable<MostBorrowedBookViewModel> GetBookList()
        {
            var query = (from book in _library.Book
                         join genres in _library.DictBookGenre on book.BookGenreId equals genres.BookGenreId
                         orderby book.Borrow.Count descending
                         select new MostBorrowedBookViewModel
                         {
                             BookId = book.BookId,
                             Author = book.Author,
                             Title = book.Title,
                             ReleaseDate = book.ReleaseDate,
                             ISBN = book.ISBN,
                             BookGenreId=book.BookGenreId,
                             BookGenreName = genres.Name,
                             Count = book.Count,
                             AddDate=book.AddDate,
                             BorrowedCount=book.Borrow.Count
                             
                         });

            return query;
        }

        public IQueryable<MostBorrowedBookViewModel> GetFilteredBooks(string title, int? genreId, DateTime? dateFrom, DateTime? dateTo)
        {
            var query = (from book in _library.Book select book);

            if (!string.IsNullOrEmpty(title))
            {
                query = (from book in query where book.Title.Contains(title) select book);
            }
            if (genreId.HasValue)
            {
                query = (from book in query where book.BookGenreId==genreId select book);
            }
            if (dateFrom.HasValue)
            {
                query = (from book in query where book.AddDate>dateFrom select book);
            }
            if (dateTo.HasValue)
            {
                query = (from book in query where book.AddDate < dateTo select book);
            }

            var filteredBooks = (from book in query
                          join genres in _library.DictBookGenre on book.BookGenreId equals genres.BookGenreId
                          orderby book.Borrow.Count descending
                          select new MostBorrowedBookViewModel
                          {
                              BookId = book.BookId,
                              Author = book.Author,
                              Title = book.Title,
                              ReleaseDate = book.ReleaseDate,
                              ISBN = book.ISBN,
                              BookGenreId = book.BookGenreId,
                              BookGenreName = genres.Name,
                              Count = book.Count,
                              AddDate = book.AddDate,
                              BorrowedCount = book.Borrow.Count
                          });

                return filteredBooks;
        }

        public List<UserViewModel> GetUserList()
        {
            var userList = (from user in _library.User orderby user.Borrow.Count descending
                           select new UserViewModel
                           {
                               UserId = user.UserId,
                               FirstName = user.FirstName,
                               LastName = user.LastName,
                               BirthDate = user.BirthDate,
                               Email = user.Email,
                               Phone = user.Phone,
                               AddDate = user.AddDate,
                               ModifiedDate = user.ModifiedDate,
                               BooksBorrowed = user.Borrow.Count(),
                               IsActive = user.IsActive,
                               FullName = user.FirstName + " " + user.LastName
                           });
            return userList.ToList();
        }
        public List<BookGenreViewModel> GenreList()
        {
            var query = (from genre in _library.DictBookGenre
                         select new BookGenreViewModel
                         {
                             BookGenreId = genre.BookGenreId,
                             Name = genre.Name
                         });
            return query.ToList();
        }
    }
}
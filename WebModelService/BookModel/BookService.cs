﻿using LibraryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BookModel.Contract.ViewModel;
using WebModelService.Interfaces;
using WebModelService.DictBookGenreModel.Contract.ViewModel;
using WebModelService.BorrowModel.Contract.ViewModel;

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
                DictBookGenre = (from genres in _library.DictBookGenre where genres.BookGenreId == newBook.GenreId select genres).SingleOrDefault()
               
            };
            _library.Book.Add(book);
            _library.SaveChanges();
        }

        public IEnumerable<BookViewModel> BookList()
        {
            var query = (from book in _library.Book
                         join genres in _library.DictBookGenre on book.BookGenreId equals genres.BookGenreId
                         select new BookViewModel
                         {
                             BookId = book.BookId,
                             Author = book.Author,
                             Title = book.Title,
                             ReleaseDate = book.ReleaseDate,
                             ISBN = book.ISBN,
                             BookGenreName = genres.Name,
                             Count = book.Count,
                             AddDate = book.AddDate,
                             ModifiedDate = book.ModifiedDate
                         });
            return query;
        }

        public void EditBook(EditBookModel editedBook)
        {
            var book = _library.Book.SingleOrDefault(b => b.BookId == editedBook.bookId);
            book.Title = editedBook.Title;
            book.Author = editedBook.Author;
            book.Count = editedBook.Count;
            book.DictBookGenre = (from genre in _library.DictBookGenre where editedBook.GenreId == genre.BookGenreId select genre).SingleOrDefault();
            book.ISBN = editedBook.ISBN;
            book.ModifiedDate = DateTime.Now;
            book.ReleaseDate = editedBook.ReleaseDate;
            _library.SaveChanges();
        }

        public EditBookModel FindBook(int bookId)
        {
            var query = (from book in _library.Book
                         where book.BookId == bookId
                         select new EditBookModel
                         {
                             bookId = book.BookId,
                             Author = book.Author,
                             Count = book.Count,
                             GenreId = book.DictBookGenre.BookGenreId,
                             ISBN = book.ISBN,
                             ReleaseDate = book.ReleaseDate,
                             Title = book.Title
                         });
            return query.SingleOrDefault();
        }

        public List<DictBookGenreViewModel> GenreList()
        {
            var query = (from genre in _library.DictBookGenre
                         select new DictBookGenreViewModel
                         {
                             BookGenreId = genre.BookGenreId,
                             Name = genre.Name
                         });
            return query.ToList();
        }

        public BookDetailsViewModel GetBook(int bookId)
        {
            IQueryable<BookBorrowViewModel> query = GetBookData(bookId);
            var tmpBook = query.SingleOrDefault();
            var activeBorrows = new List<BorrowViewModel>();
            var historyOfBorrows = new List<BorrowViewModel>();
            BookDetailsViewModel bookDetails = PrepareBookDetailsModel(tmpBook, activeBorrows, historyOfBorrows);
            return bookDetails;
        }

        private static BookDetailsViewModel PrepareBookDetailsModel(BookBorrowViewModel tmpBook, List<BorrowViewModel> activeBorrows, List<BorrowViewModel> historyOfBorrows)
        {
            historyOfBorrows.AddRange(tmpBook.Borrows.Where(x => x.IsReturned).Select(x => new BorrowViewModel
            {
                FromDate = x.FromDate,
                IsReturned = x.IsReturned,
                Title = x.Title,
                ToDate = x.ToDate
            }).ToList());
            activeBorrows.AddRange(tmpBook.Borrows.Where(x => !x.IsReturned).Select(x => new BorrowViewModel
            {
                FromDate = x.FromDate,
                IsReturned = x.IsReturned,
                Title = x.Title,
                ToDate = x.ToDate
            }).ToList());

            BookDetailsViewModel bookDetails = new BookDetailsViewModel();
            bookDetails.BookId = tmpBook.BookId;
            bookDetails.Title = tmpBook.Title;
            bookDetails.ReleaseDate = tmpBook.ReleaseDate;
            bookDetails.ISBN = tmpBook.ISBN;
            bookDetails.DictBookGenre = tmpBook.DictBookGenre;
            bookDetails.Count = tmpBook.Count;
            bookDetails.Author = tmpBook.Author;
            bookDetails.AddDate = tmpBook.AddDate;
            bookDetails.ModifiedDate = tmpBook.ModifiedDate;
            bookDetails.Borrows = activeBorrows;
            bookDetails.History = historyOfBorrows;
            return bookDetails;
        }

        private IQueryable<BookBorrowViewModel> GetBookData(int bookId)
        {
            var query = (from book in _library.Book
                         where book.BookId == bookId
                         select new BookBorrowViewModel()
                         {

                             BookId = book.BookId,
                             Title = book.Title,
                             ReleaseDate = book.ReleaseDate,
                             ISBN = book.ISBN,
                              DictBookGenre = book.DictBookGenre.Name,
                             Count = book.Count,
                             Author = book.Author,
                             AddDate = book.AddDate,
                             ModifiedDate = book.ModifiedDate,
                             Borrows = book.Borrow.Select(b => new BorrowViewModel()
                             {
                                 Title = b.Book.Title,
                                 FromDate = b.FromDate,
                                 ToDate = b.ToDate,
                                 IsReturned = b.IsReturned,
                             }).ToList()
                         });
            return query;
        }
    }
}

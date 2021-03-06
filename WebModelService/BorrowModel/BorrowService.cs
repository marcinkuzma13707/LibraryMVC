﻿using LibraryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BorrowModel.Contract.ViewModel;
using WebModelService.Interfaces;
using WebModelService.ViewModel;

namespace WebModelService.BorrowModel
{
    public class BorrowService : IBorrowService
    {
        private Library _library;
        public BorrowService(Library library)
        {
            this._library = library;

        }

        public void AddNewBorrow(NewBorrowViewModel newBorrowViewModel)
        {
            User user= (from users in _library.User where users.UserId == newBorrowViewModel.UserId select users).SingleOrDefault();
            List<Book> books = (from book in _library.Book select book).ToList();
            foreach (var bookId in newBorrowViewModel.ChosenBooks)
            {
                Borrow borrow = new Borrow();
                borrow.User = user;
                borrow.Book = books.Single(x => x.BookId == bookId);
                borrow.FromDate = DateTime.Now;
                borrow.ToDate =newBorrowViewModel.ToDate ?? DateTime.Now;
                _library.Borrow.Add(borrow);   
            }
            _library.SaveChanges();
        }


public List<BookToBorrow> GetAvaibleBooks()
        {
            var query = (from book in _library.Book where book.Count > book.Borrow.Count(returnFlag => returnFlag.IsReturned == false)
                         select new BookToBorrow
                         {
                             BookId = book.BookId,
                             Title = book.Title
                         }
                         );
            return query.ToList();

        }
        public List<BorrowedBookViewModel> GetBorrowedBooks()
        {
            var query = (from borrow in _library.Borrow where borrow.IsReturned==false
                         select new BorrowedBookViewModel
                         {
                             BookId = borrow.BookId,
                             UserId = borrow.UserId,
                             BorrowId = borrow.BorrowId,
                             Title = borrow.Book.Title,
                             FirstName = borrow.User.FirstName,
                             LastName = borrow.User.LastName
                         });
            return query.ToList();
        }

      public UserViewModel GetUserDetails(int userId)
      {
          var query = (from user in _library.User
                       where user.UserId == userId

                       select new UserViewModel()
                       {
                           FirstName = user.FirstName,
                           LastName = user.LastName,
                           UserId = user.UserId,
                           History = user.Borrow.Select(borrow => new BorrowViewModel()
                           {
                               BorrowId = borrow.BorrowId,
                               FromDate = borrow.FromDate,
                               ToDate = borrow.ToDate,
                               IsReturned = borrow.IsReturned,
                               Title = borrow.Book.Title
                           })
                       }
                       );
          UserViewModel userDetails = AddActiveBooks(query);
          return userDetails;
        }

        private static UserViewModel AddActiveBooks(IQueryable<UserViewModel> query)
        {
            var userDetails = query.SingleOrDefault();
            var activeBorrows = new List<BorrowViewModel>();

            foreach (BorrowViewModel borrow in userDetails.History)
            {
                if (borrow.IsReturned == false)
                {
                    activeBorrows.Add(borrow);
                }
            }
            userDetails.Borrows = activeBorrows;
            return userDetails;
        }

        public List<UserViewModel> GetUsersWithBooks()
        {
            var query = (from user in _library.User
                         where user.Borrow.Any(borrowedBook => borrowedBook.IsReturned==false)
                         select new UserViewModel
                         {
                             UserId = user.UserId,
                             Email = user.Email,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             BooksBorrowed = user.Borrow.Count()
                         }
                         );
            return query.ToList();
        }

        public void ReturnBook(int borrowId)
        {
            var query = (from borrow in _library.Borrow where borrowId == borrow.BorrowId select borrow).SingleOrDefault();
            query.IsReturned = true;
            _library.SaveChanges();
        }

        public void ReturnBooks(int[] booksToReturn)
        {
            List<Borrow> borrowList = (from borrows in _library.Borrow select borrows).ToList();
            foreach (int borrowId in booksToReturn)
            {
                borrowList.Find(b => b.BorrowId == borrowId).IsReturned = true;
            }
            _library.SaveChanges();
        }
    }
}

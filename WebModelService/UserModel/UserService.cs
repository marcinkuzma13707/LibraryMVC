using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDataAccess;
using WebModelService.Interfaces;
using WebModelService.ViewModel;
using System.Web.Mvc;
using WebModelService.BorrowModel.Contract.ViewModel;

namespace WebModelService
{
    public class UserService : IUserService
    {
        
     
        private Library _library;
        public UserService(Library library)
        {
            this._library = library;
            
        }

        public UserViewModel GetUser(int userId)
        {  
            var userDetails = _library.User.SingleOrDefault(u => u.UserId == userId);

            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserId = userDetails.UserId;
            userViewModel.FirstName = userDetails.FirstName;
            userViewModel.LastName = userDetails.LastName;
            userViewModel.BirthDate = userDetails.BirthDate;
            userViewModel.Email = userDetails.Email;
            userViewModel.Phone = userDetails.Phone;
            userViewModel.AddDate = userDetails.AddDate;
            userViewModel.ModifiedDate = userDetails.ModifiedDate;
            userViewModel.IsActive = userDetails.IsActive;

            return userViewModel;
        }

        public void UserEdit(UserViewModel userViewModel)
        {
            var user = _library.User.SingleOrDefault(u => u.UserId == userViewModel.UserId);

            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Phone = userViewModel.Phone;
            user.Email = userViewModel.Email;
            user.BirthDate =userViewModel.BirthDate ?? DateTime.Now;
            user.ModifiedDate = DateTime.Now;
            _library.SaveChanges();
        }

        public List<UserViewModel> UserList()
        {


            var userList = from u in _library.User
                           join borrow in _library.Borrow on u.UserId equals borrow.UserId into borrowedCount
                           from b in borrowedCount.DefaultIfEmpty()
                           select new UserViewModel
                           {
                               UserId = u.UserId,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               BirthDate = u.BirthDate,
                               Email = u.Email,
                               Phone = u.Phone,
                               AddDate = u.AddDate,
                               ModifiedDate = u.ModifiedDate,
                               BooksBorrowed = borrowedCount.Count(),
                               IsActive = u.IsActive
                           };

            List<UserViewModel> noDuplicates = userList.Distinct().ToList();


            return noDuplicates;


        }

        public void AddUser(UserViewModel newUser)
        {
            User user = new User();
            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.Email = newUser.Email;
            user.Phone = newUser.Phone;
            user.BirthDate = newUser.BirthDate ?? DateTime.Now;
            user.IsActive = true;
            user.AddDate = DateTime.Now;
            _library.User.Add(user);
            _library.SaveChanges();
       
        }

        public void DeleteUser(int id)
        {
            var user = _library.User.SingleOrDefault(u => u.UserId == id);
            user.IsActive = false;
            _library.SaveChanges();
        }

        public UserViewModel UserDetails(int userId)
        {
            var query = (from u in _library.User where u.UserId == userId select new
                            {
                                UserId = u.UserId,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                BirthDate = u.BirthDate,
                                Email = u.Email,
                                Phone = u.Phone,
                                AddDate = u.AddDate,
                                ModifiedDate = u.ModifiedDate,
                                IsActive = u.IsActive,
                                Borrows = u.Borrow.Select(b => new
                                {
                                    b.Book.Title,
                                    b.FromDate,
                                    b.ToDate,
                                    b.IsReturned
                                })
                            });
            var user = query.SingleOrDefault();
            var activeBorrows = new List<BorrowViewModel>();
            var historyOfBorrows = new List<BorrowViewModel>();

            historyOfBorrows.AddRange(user.Borrows.Where(x => x.IsReturned).Select(x => new BorrowViewModel {
                FromDate = x.FromDate,
                IsReturned = x.IsReturned,
                Title = x.Title,
                ToDate = x.ToDate
            }).ToList());

            activeBorrows.AddRange(user.Borrows.Where(x => !x.IsReturned).Select(x => new BorrowViewModel
            {
                FromDate = x.FromDate,
                IsReturned = x.IsReturned,
                Title = x.Title,
                ToDate = x.ToDate
            }).ToList());

            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserId = user.UserId;
            userViewModel.FirstName = user.FirstName;
            userViewModel.LastName = user.LastName;
            userViewModel.BirthDate = user.BirthDate;
            userViewModel.Email = user.Email;
            userViewModel.Phone = user.Phone;
            userViewModel.AddDate = user.AddDate;
            userViewModel.ModifiedDate = user.ModifiedDate;
            userViewModel.IsActive = user.IsActive;
            userViewModel.Borrows = activeBorrows;
            userViewModel.History = historyOfBorrows;

            return userViewModel;
        }

        public bool DoEmailExist(string email, int? id)
        {
            bool emailIncorrect = (from x in _library.User where x.Email == email && x.UserId != id select x).Any();
            return emailIncorrect;
        }
    }
}

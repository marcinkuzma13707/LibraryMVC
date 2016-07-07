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
            var user = from u in _library.User
                          where u.UserId == userId
                          select u;
            var userDetails = _library.User.SingleOrDefault(u => u.UserId == userId);

             UserViewModel userDTO = new UserViewModel();
            userDTO.UserId = userDetails.UserId;
            userDTO.FirstName = userDetails.FirstName;
            userDTO.LastName = userDetails.LastName;
            userDTO.BirthDate = userDetails.BirthDate;
            userDTO.Email = userDetails.Email;
            userDTO.Phone = userDetails.Phone;
            userDTO.AddDate = userDetails.AddDate;
            userDTO.ModifiedDate = userDetails.ModifiedDate;
            userDTO.IsActive = userDetails.IsActive;

            return userDTO;
        }

        public void UserEdit(UserViewModel userViewModel)
        {
            var user = _library.User.SingleOrDefault(u => u.UserId == userViewModel.UserId);

            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Phone = userViewModel.Phone;
            user.Email = userViewModel.Email;
            user.BirthDate=userViewModel.BirthDate;
            user.ModifiedDate = DateTime.Now;
            _library.SaveChanges();
        }

        public List<UserViewModel> UserList()
        {

            //var list = from u in _library.User
            //            select u;
            // List<UserViewModel> listDTO = new List<UserViewModel>();
            // foreach (User u in list)
            // {
            //     UserViewModel userDTO = new UserViewModel();
            //     userDTO.UserId = u.UserId;
            //     userDTO.FirstName = u.FirstName;
            //     userDTO.LastName = u.LastName;
            //     userDTO.BirthDate = u.BirthDate;
            //     userDTO.Email = u.Email;
            //     userDTO.Phone = u.Phone;
            //     userDTO.AddDate = u.AddDate;
            //     userDTO.ModifiedDate = u.ModifiedDate;
            //     userDTO.IsActive = u.IsActive;

            //     listDTO.Add(userDTO);
            // }

            var userList = from u in _library.User
                           join b1 in _library.Borrow on u.UserId equals b1.UserId into borrowedCount
                           from b in borrowedCount.DefaultIfEmpty()
                               //from b in context.Borrows.Where(x => x.UserId == u.UserId).DefaultIfEmpty()
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
            user.BirthDate = newUser.BirthDate;
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
            var query = (from x in _library.User where x.UserId == userId select x).Select(u => new
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

            foreach (var x in user.Borrows)
            {
                if (x.IsReturned)
                    historyOfBorrows.Add(new BorrowViewModel
                    {
                        FromDate = x.FromDate,
                        IsReturned = x.IsReturned,
                        Title = x.Title,
                        ToDate = x.ToDate
                    });
                else
                    activeBorrows.Add(new BorrowViewModel
                    {
                        FromDate = x.FromDate,
                        IsReturned = x.IsReturned,
                        Title = x.Title,
                        ToDate = x.ToDate
                    });
            }
            UserViewModel userV = new UserViewModel();
            userV.UserId = user.UserId;
                 userV.FirstName = user.FirstName;
            userV.LastName = user.LastName;
            userV.BirthDate = user.BirthDate;
            userV.Email = user.Email;
            userV.Phone = user.Phone;
            userV.AddDate = user.AddDate;
            userV.ModifiedDate = user.ModifiedDate;
            userV.IsActive = user.IsActive;
            userV.Borrows = activeBorrows;
            userV.History = historyOfBorrows;



            return userV;
         
            
        }

        public bool DoEmailExist(string email)
        {
            var query = (from x in _library.User where x.Email == email select x).FirstOrDefault();

            if (query == null)
                return false;
            return true;
        }
    }
}

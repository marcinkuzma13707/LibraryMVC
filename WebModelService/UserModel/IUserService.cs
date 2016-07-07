using LibraryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.ViewModel;

namespace WebModelService.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> UserList();
        UserViewModel GetUser(int userId);
        void UserEdit(UserViewModel userViewModel);
        void AddUser(UserViewModel newUser);

        void DeleteUser(int id);

        UserViewModel UserDetails(int userId);

        bool DoEmailExist(string email, int? id);
        
    }
}

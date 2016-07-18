using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BorrowModel.Contract.ViewModel;
using WebModelService.ViewModel;

namespace WebModelService.Interfaces
{
    public interface IBorrowService
    {
        List<BorrowedBookViewModel> GetBorrowedBooks();
        List<UserViewModel> GetUsersWithBooks();
        List<BookToBorrow> GetAvaibleBooks();
        void AddNewBorrow(NewBorrowViewModel newBorrowViewModel);
        void ReturnBook(int borrowId);

        UserViewModel GetUserDetails(int userId);
        void ReturnBooks(int[] booksToReturn);
    }
}

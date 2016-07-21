using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BookModel.Contract.ViewModel;
using WebModelService.ReportModel.Contract.ViewModel;
using WebModelService.ViewModel;

namespace WebModelService.Interfaces
{
    public interface IReportService
    {
        List<UserViewModel> GetUserList();
        IQueryable<MostBorrowedBookViewModel> GetBookList();
        IQueryable<MostBorrowedBookViewModel> GetFilteredBooks(string title, int? genreId, DateTime? dateFrom, DateTime? dateTo);
    }
}

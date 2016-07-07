using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BookModel.Contract.ViewModel;

namespace WebModelService.Interfaces
{
    interface IBookService
    {
        List<BookViewModel> BookList();
    }
}

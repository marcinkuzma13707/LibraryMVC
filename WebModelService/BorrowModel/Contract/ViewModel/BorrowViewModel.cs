using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BookModel.Contract.ViewModel;
using WebModelService.ViewModel;

namespace WebModelService.BorrowModel.Contract.ViewModel
{
   public class BorrowViewModel
    {
        [DisplayName("BorrowId")]
        public int BorrowId { get; set; }
        [DisplayName("UserId")]

        public int UserId { get; set; }
        [DisplayName("BookId")]
        public int BookId { get; set; }
        [DisplayName("From Date")]
        public DateTime FromDate { get; set; }


        [DisplayName("To Date")]
        public DateTime ToDate { get; set; }
        [DisplayName("Is returned")]
        public bool IsReturned { get; set; }

        public  BookViewModel Book { get; set; }

        public  UserViewModel User { get; set; }

        public string Title { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelService.BorrowModel.Contract.ViewModel
{
    public class BorrowedBookViewModel
    {
        [DisplayName("BorrowId")]
        public int BorrowId { get; set; }
        [DisplayName("UserId")]

        public int UserId { get; set; }
        [DisplayName("BookId")]
        public int BookId { get; set; }
        [DisplayName("User")]

        public string User
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        [DisplayName("Book title")]
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

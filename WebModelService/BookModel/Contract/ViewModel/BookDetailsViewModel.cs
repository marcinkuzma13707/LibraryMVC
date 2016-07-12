using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BorrowModel.Contract.ViewModel;

namespace WebModelService.BookModel.Contract.ViewModel
{
    public class BookDetailsViewModel
    {
        [DisplayName("Book Id")]
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        [DisplayName("Release Date")]
        public Nullable<DateTime> ReleaseDate { get; set; }
        [DisplayName("Add Date")]
        public DateTime AddDate { get; set; }
        public string ISBN { get; set; }
        public int Count { get; set; }
        [DisplayName("Book Genre")]
        public string DictBookGenre { get; set; }
        [DisplayName("Modified Date")]
        public Nullable<DateTime> ModifiedDate { get; set; }

        public IEnumerable<BorrowViewModel> Borrows { get; set; }
        public IEnumerable<BorrowViewModel> History { get; set; }
    }
}

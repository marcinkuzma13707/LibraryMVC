using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BorrowModel.Contract.ViewModel;
using WebModelService.DictBookGenreModel.Contract.ViewModel;

namespace WebModelService.BookModel.Contract.ViewModel
{
    public class BookViewModel
    {

        public int BookId { get; set; }

        
        public string Author { get; set; }

      
        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

      
        public string ISBN { get; set; }

        public int BookGenreId { get; set; }

        public int Count { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual DictBookGenreViewModel DictBookGenre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BorrowViewModel> Borrow { get; set; }

    }
}

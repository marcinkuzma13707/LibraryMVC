using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.DictBookGenreModel.Contract.ViewModel;

namespace WebModelService.ReportModel.Contract.ViewModel
{
    public class MostBorrowedBookViewModel
    {
        [DisplayName("Book Id")]
        public int BookId { get; set; }


        public string Author { get; set; }


        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public DateTime AddDate { get; set; }

        public string AddDateDisplay
        {
            get
            {
                return this.AddDate.ToShortDateString();
            }
        }


        public string ISBN { get; set; }
        public string BookGenreName { get; set; }

        public int BookGenreId { get; set; }

        public int Count { get; set; }

        public int BorrowedCount { get; set; }

        public virtual BookGenreViewModel DictBookGenre { get; set; }

    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelService.BorrowModel.Contract.ViewModel
{
   public class NewBorrowViewModel
    {
        [Required]
        [DisplayName("Select book")]
        public int[] ChosenBooks { get; set; }
        [Required]
        public int UserId { get; set; }
        [DisplayName("To Date")]
        [Required]
        public DateTime? ToDate { get; set; }
    }
}

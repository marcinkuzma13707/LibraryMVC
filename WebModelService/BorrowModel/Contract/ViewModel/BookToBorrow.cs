using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelService.BorrowModel.Contract.ViewModel
{
   public class BookToBorrow
    {
       [Required]
       [DisplayName("BookId")]
       public int BookId { get; set; }
       [DisplayName("Book title")]
       public string Title { get; set; }
    }
}

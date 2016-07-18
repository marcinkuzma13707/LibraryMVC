using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelService.BorrowModel.Contract.ViewModel;

namespace WebModelService.ViewModel
{
    public class UserViewModel
    {
        
        public int UserId { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Field is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        [Required(ErrorMessage = "Field is required.")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [DisplayName("Birth date")]
        [Required(ErrorMessage = "Field is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true )]
        public DateTime? BirthDate { get; set;}

        //public string BirthDateDisplay
        //{
        //    get
        //    {
        //        return this.BirthDate.ToShortDateString();
        //    }
        //}
        [MaxLength(50)]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Field is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Phone")]
        [Phone]
        [MaxLength(50)]
        public string Phone { get; set; }
        [ScaffoldColumn(false)]
        public DateTime AddDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }
        [ScaffoldColumn(false)]
        public bool IsActive { get; set; }

        public int BooksBorrowed { get; set; }
        [DisplayName("Borrows")]
        public IEnumerable<BorrowViewModel> Borrows { get; set; }
        [DisplayName("History")]
        public IEnumerable<BorrowViewModel> History { get; set; }
        public string FullName { get; set; }



    }
}

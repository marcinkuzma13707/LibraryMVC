﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelService.BookModel.Contract.ViewModel
{
    public class EditBookModel
    {
        public int bookId { get; set; }
        [DisplayName("Author")]
        [Required]
        [MaxLength(50)]
        public string Author { get; set; }
        [DisplayName("Title")]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [DisplayName("Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
        [DisplayName("ISBN")]
        [Required]
        [MaxLength(50)]
        public string ISBN { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater or equal 0")]
        public int Count { get; set; }
        [Required]
        public int GenreId { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}

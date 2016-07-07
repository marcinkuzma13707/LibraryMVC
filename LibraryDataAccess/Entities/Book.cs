namespace LibraryDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            Borrow = new HashSet<Borrow>();
        }

        public int BookId { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ISBN { get; set; }

        public int BookGenreId { get; set; }

        public int Count { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual DictBookGenre DictBookGenre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Borrow> Borrow { get; set; }
    }
}

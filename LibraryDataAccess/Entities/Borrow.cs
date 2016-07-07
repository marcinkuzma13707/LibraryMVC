namespace LibraryDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Borrow")]
    public partial class Borrow
    {
        public int BorrowId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsReturned { get; set; }

        public virtual Book Book { get; set; }

        public virtual User User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pintea_Razvan_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "Book title is required.")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
        [MinLength(3, ErrorMessage = "Title must contain at least 3 characters.")]
        public string Title { get; set; }
     
        


        public int? AuthorsID { get; set; }

        public Authors? Authors { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }
        public int? PublisherID { get; set; }
        public Publishers? Publisher { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
        public ICollection<Borrowing>? Borrowings { get; set; }
    }

}


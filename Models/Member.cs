using System.ComponentModel.DataAnnotations;

namespace Pintea_Razvan_Lab2.Models
{
    public class Member
    {
        public int ID { get; set; }
        
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage =
"Prenumele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau AnaMaria")]
        
        public string? FirstName { get; set; }
        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [StringLength(30, MinimumLength = 3)]

        public string? LastName { get; set; }
        [StringLength(70)]

        public string? Adress { get; set; }
        public string? Email { get; set; }
        [RegularExpression(@"^0[0-9]*$", ErrorMessage = "Phone number must start with 0.")]
        public string? Phone { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    /// <summary>
    /// Customer entity representing a customer in the database
    /// </summary>
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}

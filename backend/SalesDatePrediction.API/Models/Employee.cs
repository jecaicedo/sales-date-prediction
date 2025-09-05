using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesDatePrediction.API.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; } = string.Empty;
        
        [MaxLength(30)]
        public string? Title { get; set; }
        
        public DateTime? HireDate { get; set; }
        
        [MaxLength(60)]
        public string? Address { get; set; }
        
        [MaxLength(15)]
        public string? City { get; set; }
        
        [MaxLength(15)]
        public string? Country { get; set; }
        
        [MaxLength(24)]
        public string? Phone { get; set; }
        
        [JsonIgnore]
        public string FullName => $"{FirstName} {LastName}";
        
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesDatePrediction.API.Models
{
    public class Shipper
    {
        [Key]
        public int ShipperID { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; } = string.Empty;
        
        [MaxLength(24)]
        public string? Phone { get; set; }
        
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}

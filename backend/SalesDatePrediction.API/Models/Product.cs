using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalesDatePrediction.API.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? UnitPrice { get; set; }
        
        public bool Discontinued { get; set; } = false;
        
        [JsonIgnore]
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}

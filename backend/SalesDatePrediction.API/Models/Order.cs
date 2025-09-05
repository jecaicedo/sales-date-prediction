using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalesDatePrediction.API.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public DateTime? RequiredDate { get; set; }
        
        public DateTime? ShippedDate { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Freight { get; set; }
        
        [MaxLength(40)]
        public string? ShipName { get; set; }
        
        [MaxLength(60)]
        public string? ShipAddress { get; set; }
        
        [MaxLength(15)]
        public string? ShipCity { get; set; }
        
        [MaxLength(15)]
        public string? ShipCountry { get; set; }
        
        public int CustID { get; set; }
        
        [ForeignKey("CustID")]
        public Customer? Customer { get; set; }
        
        public int EmpID { get; set; }
        
        [ForeignKey("EmpID")]
        public Employee? Employee { get; set; }
        
        public int ShipperID { get; set; }
        
        [ForeignKey("ShipperID")]
        public Shipper? Shipper { get; set; }
        
        [JsonIgnore]
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}

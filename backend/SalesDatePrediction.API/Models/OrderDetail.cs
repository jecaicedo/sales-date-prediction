using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesDatePrediction.API.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        public int OrderID { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public int ProductID { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        
        public short Qty { get; set; }
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; } = 0;
        
        [ForeignKey("OrderID")]
        public Order? Order { get; set; }
        
        [ForeignKey("ProductID")]
        public Product? Product { get; set; }
    }
}
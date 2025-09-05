namespace SalesDatePrediction.API.Models;

public class CustomerPrediction
{
    public int CustID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime LastOrderDate { get; set; }
    public DateTime NextPredictedOrder { get; set; }
}

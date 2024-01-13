namespace AspireWithDapr.ServiceDefaults.Models;

public class Order
{
    public string UserName { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}

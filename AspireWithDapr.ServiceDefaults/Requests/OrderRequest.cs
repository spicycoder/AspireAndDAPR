namespace AspireWithDapr.ServiceDefaults.Requests;

public class OrderRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Username { get; set; }
}

using DataLibrary.Models;
namespace MVCDemoApp.Models;

public class OrderDisplayModel
{
    public OrderModel Order { get; set; }=new OrderModel();
    public string ItemPurchased { get; set; }
}

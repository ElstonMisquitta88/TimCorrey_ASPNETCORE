using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCDemoApp.Models;

public class OrderCreateModel
{
    public OrderModel Order { get; set; } = new OrderModel();
    public List<SelectListItem> FoodItems { get; set; } = new List<SelectListItem>();

}

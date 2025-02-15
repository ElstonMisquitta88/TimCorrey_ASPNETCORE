using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ApiDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IFoodData _foodData;
    private readonly IOrderData _orderData;

    public OrderController(IFoodData foodData,IOrderData orderData)
    {
        _foodData = foodData;
        _orderData = orderData;
    }

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(OrderModel Order)
    {
        var food = await _foodData.GetFood(); // Shortcut >> Real Work fetch Specific food item
        Order.Total = Order.Quantity * food.Where(x => x.Id == Order.FoodId).First().Price;
        int id = await _orderData.CreateOrder(Order);
        return Ok(id);
    }
}

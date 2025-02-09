using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemoApp.Models;

namespace MVCDemoApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public OrdersController(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var food = await _foodData.GetFood();

            OrderCreateModel model = new OrderCreateModel();

            food.ForEach(x =>
            {
                model.FoodItems.Add(new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Title
                });
            });

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrderModel Order)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var food = await _foodData.GetFood(); // Shortcut >> Real Work fetch Specific food item
            Order.Total = Order.Quantity * food.Where(x => x.Id == Order.FoodId).First().Price;
            int id = await _orderData.CreateOrder(Order);

            return RedirectToAction("Create"); // TODO
        }
    }
}

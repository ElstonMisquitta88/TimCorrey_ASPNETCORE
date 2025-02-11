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

            return RedirectToAction("Display",new {id}); 
        }


        public async Task<IActionResult> Display(int Id)
        {
            var Order = await _orderData.GetOrderById(Id);
            OrderDisplayModel displayorder = new OrderDisplayModel();
            displayorder.Order = Order; // (1)

            if (Order != null)
            {
                var food = await _foodData.GetFood();
                //(2)
                displayorder.ItemPurchased = food.Where(x => x.Id == Order.FoodId).FirstOrDefault()?.Title;
            }
            return View(displayorder);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderModel Order)
        {
            await _orderData.UpdateOrderName(Order.Id, Order.OrderName);
            return RedirectToAction("Display", new { Order.Id });
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _orderData.DeleteOrder(Id);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OrderModel Order)
        {
            await _orderData.DeleteOrder(Order.Id);
            return RedirectToAction("Create");
        }


        public async Task<IActionResult> DisplayAllOrders()
        {
            var Order = await _orderData.GetAllOrders();
            List<OrderModel> displayorder = new List<OrderModel>();
            displayorder = Order; // (1)
            return View(displayorder);
        }
    }
}

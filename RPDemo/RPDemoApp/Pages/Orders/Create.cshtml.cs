using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RPDemoApp.Pages.Orders
{
    public class CreateModel : PageModel
    {
        // Dependency Injection
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        // Similar to Drop Down List in ASP.NET Web Forms
        public List<SelectListItem> FoodItems { get; set; }


        // This is the model that will be used to bind the data from the form
        // You write to this model and then save it to the database
        [BindProperty]
        public OrderModel Order { get; set; }



        // Constructor
        public CreateModel(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }

        public async Task OnGet()
        {
            // Get the list of food items
            var food = await _foodData.GetFood();

            FoodItems = new List<SelectListItem>();

            // Populate the FoodItems list
            foreach (var item in food)
            {
                FoodItems.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Title
                });
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var food = await _foodData.GetFood(); // Shortcut >> Real Work fetch Specific food item
            Order.Total = Order.Quantity * food.Where(x => x.Id == Order.FoodId).First().Price;
            int id = await _orderData.CreateOrder(Order);

            // Redirect to page (Razor Page)
            return RedirectToPage("./Create"); //TODO
        }

    }
}

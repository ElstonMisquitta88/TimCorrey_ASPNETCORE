using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RPDemoApp.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public List<SelectListItem> FoodItems { get; set; }

        [BindProperty]
        public OrderModel Order { get; set; }

        public CreateModel(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }

        public async Task OnGet()
        {
            var food = await _foodData.GetFood();
            FoodItems = new List<SelectListItem>();

            foreach (var item in food)
            {
                FoodItems.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Title
                });
            }
        }
    }
}

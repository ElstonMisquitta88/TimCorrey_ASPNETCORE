using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemoApp.Pages.Food
{
    public class ListModel : PageModel
    {
        private readonly IFoodData _foodData;

        // This is a property that will be used to display data on the Page
        // This cannot be used to POST Data as we have not used the [BindProperty] attribute
        
        // Property can be accessed in the Razor Page 
        public List<FoodModel> foodList { get; set; }

        public ListModel(IFoodData foodData)
        {
            _foodData = foodData;
        }
        public async Task OnGet()
        {
            foodList = await _foodData.GetFood();
        }
    }
}

using DataLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using MVCDemoApp.Models;

namespace MVCDemoApp.Controllers;

public class FoodController : Controller
{
    private readonly IFoodData _foodData;

    public FoodController(IFoodData foodData)
    {
        _foodData = foodData;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> DisplayFood()
    {
        FoodListModel foodlst = new FoodListModel();
        foodlst.Foodlist = await _foodData.GetFood();
        return View(foodlst);
    }


}

using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemoApp.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderData _orderData;

        public DeleteModel(IOrderData orderData)
        {
            _orderData = orderData;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public OrderModel Order { get; set; }

        public async Task OnGet()
        {
            Order = await _orderData.GetOrderById(Id);
        }

        public async Task<IActionResult> OnPostDeleteOrder()
        {
            await _orderData.DeleteOrder(Id);
            return RedirectToPage("./Create");
        }
    }
}

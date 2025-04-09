using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Meal_Planner.Models;
using System.Threading.Tasks;

namespace Meal_Planner.Controllers
{
    public class Htmx : Controller
    {
        private MealContext _context;
        public Htmx(MealContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Silly(IFormCollection form)
        {
            string itemIdStr = form["itemId"];
            if (itemIdStr == null)
            {
                return NotFound();
            }

            int itemId = int.Parse(itemIdStr);

            FoodItem item = await _context.FoodItems.SingleOrDefaultAsync(x => x.ItemId == itemId);
            if (item == null)
            {
                return NotFound();
            }

            return PartialView("_Silly", item);
        }
    }
}

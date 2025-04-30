using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Meal_Planner.Models;
using Microsoft.EntityFrameworkCore;

namespace Meal_Planner.Pages.Meals
{
    public class RemoveItemModel : PageModel
    {
        private readonly MealContext _context;

        public RemoveItemModel(MealContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int MealId { get; set; }

        [BindProperty]
        public int FoodItemId { get; set; }

        public Meal_Planner.Models.Meal Meal { get; set; } = null!;
        public FoodItem FoodItem { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int mealId, int itemId)
        {
            MealId = mealId;
            FoodItemId = itemId;

            Meal = await _context.Meals
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.MealId == mealId);

            if (Meal == null)
            {
               
                return NotFound();
            }

            FoodItem = await _context.FoodItems
                .FirstOrDefaultAsync(f => f.ItemId == itemId);

            if (FoodItem == null)
            {
                //Console.WriteLine("problem here man NO I MEANT HERE");
                return NotFound(); 
            }

            MealId = Meal.MealId;
            FoodItemId = FoodItem.ItemId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var meal = await _context.Meals
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.MealId == MealId);

            if (meal == null)
            {
                Console.WriteLine("problem here man");
                return NotFound();
            }

            var item = await _context.FoodItems.FindAsync(FoodItemId);

            if (item != null)
            {
                meal.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Meals/Index");
        }
    }
}

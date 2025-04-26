using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Meal_Planner.Models;

namespace Meal_Planner.Pages.FoodItems
{
    public class DeleteModel : PageModel
    {
        private readonly MealContext _context;

        public DeleteModel(MealContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            FoodItem = await _context.FoodItems.FirstOrDefaultAsync(f => f.ItemId == id);

            if (FoodItem == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems
                .Include(f => f.RecipeIngredients)
                .Include(f => f.Meals)
                .FirstOrDefaultAsync(f => f.ItemId == id);

            if (foodItem == null)
            {
                return NotFound();
            }

            // Remove related RecipeIngredients
            _context.RecipeIngredients.RemoveRange(foodItem.RecipeIngredients);

            // Clear Meals relationship (for many-to-many)
            foodItem.Meals.Clear();

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}

using Meal_Planner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Meal_Planner.Pages.MealItems
{
    public class AddItemModel : PageModel
    {
        private readonly MealContext _context;

        public AddItemModel(MealContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int MealId { get; set; }

        [BindProperty]
        public int SelectedFoodItemId { get; set; }

        public Meal_Planner.Models.Meal Meal { get; set; } = default!;
        public List<SelectListItem> FoodItems { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int mealId)
        {
            MealId = mealId;
            Meal = await _context.Meals.FindAsync(mealId);

            if (Meal == null)
                return NotFound();

            FoodItems = await _context.FoodItems
                .Select(fi => new SelectListItem
                {
                    Value = fi.ItemId.ToString(),
                    Text = fi.Name
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var meal = await _context.Meals
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.MealId == MealId);

            var foodItem = await _context.FoodItems.FindAsync(SelectedFoodItemId);

            if (meal == null || foodItem == null)
                return NotFound();

            // Prevent duplicates
            if (!meal.Items.Any(i => i.ItemId == foodItem.ItemId))
            {
                meal.Items.Add(foodItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Meals/Index");
        }
    }
}

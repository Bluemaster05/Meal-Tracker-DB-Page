using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Meal_Planner.Models;

namespace Meal_Planner.Pages.FoodItems
{
    public class CreateModel : PageModel
    {
        private readonly MealContext _context;

        public CreateModel(MealContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = new FoodItem();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FoodItems.Add(FoodItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/FoodItems/Index");
        }
    }
}

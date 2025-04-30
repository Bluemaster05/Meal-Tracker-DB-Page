using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Meal_Planner.Models;

namespace Meal_Planner.Pages.Meals
{
    public class CreateModel : PageModel
    {
        private readonly MealContext _context;

        public CreateModel(MealContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meal_Planner.Models.Meal Meal { get; set; } = new Meal_Planner.Models.Meal();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Meals.Add(Meal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

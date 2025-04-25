using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Meal_Planner.Pages.Meals
{
    public class DeleteModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public DeleteModel(Meal_Planner.Models.MealContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meal_Planner.Models.Meal Meal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.FirstOrDefaultAsync(m => m.MealId == id);

            if (meal == null)
            {
                return NotFound();
            }
            else
            {
                Meal = meal;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.MealId == id);

            if (meal == null)
            {
                return NotFound();
            }

            meal.Items.Clear();
           
            await _context.SaveChangesAsync();

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using Meal_Planner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Meal_Planner.Pages.MealView
{
    public class IndexModel : PageModel
    {
        private readonly MealContext _context;

        public IndexModel(MealContext context)
        {
            _context = context;
        }

        public IList<Meal_Planner.Models.Meal> Meals { get; set; } = new List<Meal_Planner.Models.Meal>();

        public async Task OnGetAsync()
        {
            Meals = await _context.Meals
                    .Include(mi => mi.Items)
                .ToListAsync();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Meal_Planner.Models;
using NuGet.Protocol;

namespace Meal_Planner.Pages.Meal
{
    public class IndexModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public IndexModel(Meal_Planner.Models.MealContext context)
        {
            _context = context;
        }

        public IList<Meal_Planner.Models.Meal> Meals { get; set; } = default!;

        //public IList<Meal_Planner.Models.Ingredient> Ingredients { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Meals = await _context.Meals.ToListAsync();
            //Ingredients = await _context.Ingredients.ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Meal_Planner.Models;

namespace Meal_Planner.Pages.Ingredient
{
    public class IndexModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public IndexModel(Meal_Planner.Models.MealContext context)
        {
            _context = context;
        }

        public IList<Meal_Planner.Models.Ingredient> Ingredient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ingredient = await _context.Ingredients
                .Include(i => i.MIdNavigation).ToListAsync();
        }
    }
}

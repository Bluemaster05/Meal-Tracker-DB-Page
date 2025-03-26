using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Meal_Planner_Page.Models;

namespace Meal_Planner_Page.Pages.Ingredient
{
    public class IndexModel : PageModel
    {
        private readonly Meal_Planner_Page.Models.MealContext _context;

        public IndexModel(Meal_Planner_Page.Models.MealContext context)
        {
            _context = context;
        }

        public IList<Ingredient> Ingredient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ingredient = await _context.Ingredients
                .Include(i => i.MIdNavigation).ToListAsync();
        }
    }
}

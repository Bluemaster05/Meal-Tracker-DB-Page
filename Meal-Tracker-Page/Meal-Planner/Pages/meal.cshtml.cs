using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Meal_Planner.Models;

namespace Meal_Planner.Pages
{
    public class mealModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public mealModel(Meal_Planner.Models.MealContext context)
        {
            _context = context;
        }

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
    }
}

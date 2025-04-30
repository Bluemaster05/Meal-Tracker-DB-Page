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
    public class DeleteModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public DeleteModel(Meal_Planner.Models.MealContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meal_Planner.Models.Ingredient Ingredient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients
                .Include(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(m => m.IngredientId == id);

            if (ingredient == null)
            {
                return NotFound();
            }
            else
            {
                Ingredient = ingredient;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients
        .Include(i => i.RecipeIngredients)
        .FirstOrDefaultAsync(i => i.IngredientId == id);
            if (ingredient != null)
            {
                Ingredient = ingredient;
                //ingredient.RecipeIngredients.Clear();
                _context.RecipeIngredients.RemoveRange(ingredient.RecipeIngredients);
                _context.Ingredients.Remove(Ingredient);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

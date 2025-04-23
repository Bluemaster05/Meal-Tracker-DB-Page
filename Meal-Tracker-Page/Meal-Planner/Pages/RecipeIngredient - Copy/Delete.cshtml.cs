using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Meal_Planner.Models;

namespace Meal_Planner.Pages.RecipeIngredient
{
    public class DeleteModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public DeleteModel(Meal_Planner.Models.MealContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meal_Planner.Models.RecipeIngredient RecipeIngredient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? itemId, int? ingredientId)
        {
            if (itemId == null || ingredientId == null)
            {
                return NotFound();
            }

            var recipeingredient = await _context.RecipeIngredients
                .Include(ri => ri.Ingredient)
                .Include(ri => ri.Item)
                .FirstOrDefaultAsync(m => m.ItemId == itemId && m.IngredientId == ingredientId);

            if (recipeingredient == null)
            {
                return NotFound();
            }
            else
            {
                RecipeIngredient = recipeingredient;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (itemId == null || ingredientId == null)
            //{
            //    return NotFound();
            //}

            var recipeingredient = await _context.RecipeIngredients
    .FirstOrDefaultAsync(m => m.ItemId == RecipeIngredient.ItemId && m.IngredientId == RecipeIngredient.IngredientId);

            if (recipeingredient != null)
            {
                _context.RecipeIngredients.Remove(recipeingredient);
                await _context.SaveChangesAsync();
            }

            //var recipeingredient = await _context.RecipeIngredients.FirstOrDefaultAsync(m => m.ItemId == itemId && m.IngredientId == ingredientId);
            //if (recipeingredient != null)
            //{
            //    RecipeIngredient = recipeingredient;
            //    _context.RecipeIngredients.Remove(RecipeIngredient);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }
    }
}

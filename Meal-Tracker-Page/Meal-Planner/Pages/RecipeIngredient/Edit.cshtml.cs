using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meal_Planner.Models;

namespace Meal_Planner.Pages.RecipeIngredient
{
    public class EditModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public EditModel(Meal_Planner.Models.MealContext context)
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

            var recipeingredient =  await _context.RecipeIngredients.FirstOrDefaultAsync(m => m.ItemId == itemId && m.IngredientId == ingredientId);
            if (recipeingredient == null)
            {
                return NotFound();
            }
            RecipeIngredient = recipeingredient;
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "IngredientId", "IngredientId");
            ViewData["ItemId"] = new SelectList(_context.FoodItems, "ItemId", "ItemId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //Model State Issue with this relation :(
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(RecipeIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(RecipeIngredient.ItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeIngredientExists(int id)
        {
            return _context.RecipeIngredients.Any(e => e.ItemId == id);
        }
    }
}

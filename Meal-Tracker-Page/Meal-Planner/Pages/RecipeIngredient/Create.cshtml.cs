using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Meal_Planner.Models;

namespace Meal_Planner.Pages.RecipeIngredient
{
    public class CreateModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public CreateModel(Meal_Planner.Models.MealContext context)
        {
            _context = context;
        }

        public IList<Meal_Planner.Models.Measurement> Measurements { get; set; } = default!;
        public IList<Meal_Planner.Models.Ingredient> Ingredients { get; set; } = default!;
        public IActionResult OnGet()
        {
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "IngredientId", "Name");
        ViewData["ItemId"] = new SelectList(_context.FoodItems, "ItemId", "Name");
            Measurements = _context.Measurements.ToList();
            Ingredients = _context.Ingredients.ToList();
            return Page();
        }

        [BindProperty]
        public Meal_Planner.Models.RecipeIngredient RecipeIngredient { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState is invalid");
                return Page();
            }
            Console.WriteLine("✅ ModelState is valid");
            _context.RecipeIngredients.Add(RecipeIngredient);
            await _context.SaveChangesAsync();
            Console.WriteLine("✅ Saved to DB");

            return RedirectToPage("./Index");
        }
    }
}

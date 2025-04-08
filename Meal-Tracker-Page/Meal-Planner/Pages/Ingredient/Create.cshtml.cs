using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Meal_Planner.Models;

namespace Meal_Planner.Pages.Ingredient
{
    public class CreateModel : PageModel
    {
        private readonly Meal_Planner.Models.MealContext _context;

        public CreateModel(Meal_Planner.Models.MealContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MId"] = new SelectList(_context.Measurements, "MId", "MId");
            return Page();
        }

        [BindProperty]
        public Meal_Planner.Models.Ingredient Ingredient { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Ingredients.Add(Ingredient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

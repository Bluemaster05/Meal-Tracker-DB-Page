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
        ViewData["MId"] = new SelectList(_context.Measurements, "MId", "Name");
            return Page();
        }

        [BindProperty]
        public Meal_Planner.Models.Ingredient Ingredient { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    Console.WriteLine("AHHHHh");
            //    Console.WriteLine(ModelState.IsValid);
            //    return Page();
            //}
            //Console.WriteLine("AHHHWEWFEFSFDS2");
            try
            {
                _context.Ingredients.Add(Ingredient);
                Console.WriteLine("Does this print??");
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("YEAH IT DIDNT WORK MY GUY");
            }


            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Meal_Planner.Models;
using System.Composition;


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

        public IList<Meal_Planner.Models.Ingredient> Ingredients { get; set; } = default!;

        public Dictionary<string, DataEntry> IngList { get; set; } = new();

        public Dictionary<string, MealNutrition> NutritionByItemList { get; set; } = new();

        public MealNutrition MealNutritionsum { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .Include(m => m.Items)
                .ThenInclude(mi => mi.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .ThenInclude(i => i.MIdNavigation)
                .FirstOrDefaultAsync(m => m.MealId == id);
            IngList = new Dictionary<string, DataEntry>();
            NutritionByItemList = new Dictionary<string, MealNutrition>();



            int numItemsAdded = 0;
            foreach(var item in meal.Items)
            {
                float newcals = 0;
                float newpro = 0;
                float newsod = 0;
                float newtotsug = 0;
                float newtotfat = 0;
                float newdiet = 0;

                    foreach (var ri in item.RecipeIngredients)
                    {
                        if (IngList.ContainsKey(ri.Ingredient.Name))
                        {
                            IngList[ri.Ingredient.Name].Amount += ri.Amount;
                        }
                        else
                        {
                            IngList[ri.Ingredient.Name] = new DataEntry(ri.Amount, ri.Ingredient.MIdNavigation.Name);
                        }

                        newcals += ri.Amount * ri.Ingredient.Calories;
                        newpro += ri.Amount * ri.Ingredient.Protein;
                        newsod += ri.Amount * ri.Ingredient.Sodium;
                        newtotsug += ri.Amount * ri.Ingredient.TotalSugers;
                        newtotfat += ri.Amount * ri.Ingredient.TotalFat;
                        newdiet += ri.Amount * ri.Ingredient.DietaryFiber;
                    }
                newcals = newcals / item.ServingSize ?? 0;
                newpro = newpro / item.ServingSize ?? 0;
                newsod = newsod / item.ServingSize ?? 0;
                newtotsug = newtotsug / item.ServingSize ?? 0;
                newtotfat = newtotfat / item.ServingSize ?? 0;
                newdiet = newdiet / item.ServingSize ?? 0;
                numItemsAdded++;

                NutritionByItemList[item.Name] = new MealNutrition(newcals, newpro, newsod, newtotsug, newtotfat, newdiet);
            }

            MealNutritionsum = new MealNutrition(0, 0, 0, 0, 0, 0);
            foreach (var item in NutritionByItemList)
            {
                MealNutritionsum.Calories += item.Value.Calories;
                MealNutritionsum.Protein += item.Value.Protein;
                MealNutritionsum.Sodium += item.Value.Sodium;
                MealNutritionsum.TotalSugers += item.Value.TotalSugers;
                MealNutritionsum.TotalFat += item.Value.TotalFat;
                MealNutritionsum.DietaryFiber += item.Value.DietaryFiber;
            }

            if (meal == null)
            {
                return NotFound();
            }
            else
            {
                Meal = meal;
                Ingredients = Meal.Items
                .SelectMany(i => i.RecipeIngredients)
                .Select(ri => ri.Ingredient)
                .Where(i => i != null)
                .Distinct()
                .ToList();
            }
            return Page();
        }
    }
}
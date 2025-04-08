using System;
using System.Collections.Generic;

namespace Meal_Planner.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string Name { get; set; } = null!;

    public int MId { get; set; }

    public float Calories { get; set; }

    public float Protein { get; set; }

    public float Sodium { get; set; }

    public float TotalSugers { get; set; }

    public float TotalFat { get; set; }

    public float DietaryFiber { get; set; }

    public virtual Measurement MIdNavigation { get; set; } = null!;

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}

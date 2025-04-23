using System;
using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace Meal_Planner.Models;

public partial class RecipeIngredient
{
    public float Amount { get; set; }
    [Required]
    public int ItemId { get; set; }
    [Required]
    public int IngredientId { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual FoodItem Item { get; set; } = null!;
}

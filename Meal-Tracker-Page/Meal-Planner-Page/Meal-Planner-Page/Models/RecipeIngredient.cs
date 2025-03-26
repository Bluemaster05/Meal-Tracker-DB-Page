using System;
using System.Collections.Generic;

namespace Meal_Planner_Page.Models;

public partial class RecipeIngredient
{
    public float Amount { get; set; }

    public int ItemId { get; set; }

    public int IngredientId { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual FoodItem Item { get; set; } = null!;
}

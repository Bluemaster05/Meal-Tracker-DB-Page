using System;
using System.Collections.Generic;

namespace Meal_Planner.Models;

public partial class FoodItem
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public float? ServingSize { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
}

using System;
using System.Collections.Generic;

namespace Meal_Planner_Page.Models;

public partial class Meal
{
    public int MealId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<FoodItem> Items { get; set; } = new List<FoodItem>();
}

using System;
using System.Collections.Generic;

namespace Meal_Planner.Models;

public partial class Measurement
{
    public int MId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}

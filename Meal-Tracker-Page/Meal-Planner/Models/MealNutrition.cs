namespace Meal_Planner.Models
{
    public class MealNutrition
    {

        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Sodium { get; set; }
        public float TotalSugers { get; set; }

        public float TotalFat { get; set; }

        public float DietaryFiber { get; set; }

        public MealNutrition(float calories, float protein, float sodium, float totalsugers, float totalfat, float dietaryfiber)
        {
            Calories = calories;
            Protein = protein;
            Sodium = sodium;
            TotalSugers = totalsugers;
            TotalFat = totalfat;
            DietaryFiber = dietaryfiber;
        }

        public MealNutrition() { }
    }
}

namespace Meal_Planner.Models
{
    public class DataEntry
    {
        public double Amount { get; set; }
        public string Unit { get; set; }

        public DataEntry(double amount, string unit)
        {
            Amount = amount;
            Unit = unit;
        }

        public DataEntry() { } // Needed if model binding or serialization
    }
}

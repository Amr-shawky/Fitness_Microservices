namespace NutritionService.Domain.Models
{
    public class NutritionFact : BaseEntity
    {
        public int Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public double Fiber { get; set; }

        #region Relationships
        public Meal Meal { get; set; }
        #endregion
    }
}

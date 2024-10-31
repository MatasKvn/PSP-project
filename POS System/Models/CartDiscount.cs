namespace POS_System.Models
{
    public class CartDiscount : Discount
    {
        public CartDiscount(int id, int amount, bool isPercentage, DateTime startDate, int version, bool isDeleted, DateTime? endDate = null, string description = null)
        {
            base.Id = id;
            base.Description = description;
            base.Amount = amount;
            base.IsPercentage = isPercentage;
            base.StartDate = startDate;
            base.EndDate = endDate;
            base.Version = version;
            base.IsDeleted = isDeleted;
        }
    }
}

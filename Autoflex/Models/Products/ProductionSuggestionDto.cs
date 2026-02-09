namespace Autoflex.Models.Products
{
    public class ProductionSuggestionDto
    {
		public string ProductName { get; set; } = null!;
		public int Quantity { get; set; }
		public decimal TotalValue { get; set; }
	}
}

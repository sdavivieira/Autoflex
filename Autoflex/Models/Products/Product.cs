using Autoflex.Models.ProductRawMaterials;

namespace Autoflex.Models.Products
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }

		public ICollection<ProductRawMaterial> RawMaterials { get; set; } = new List<ProductRawMaterial>();
	}
}

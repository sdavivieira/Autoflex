using Autoflex.Models.Products;
using Autoflex.Models.RawMaterials;

namespace Autoflex.Models.ProductRawMaterials
{
	public class ProductRawMaterial
	{
		public int ProductId { get; set; }
		public Product Product { get; set; } = null!;

		public int RawMaterialId { get; set; }
		public RawMaterial RawMaterial { get; set; } = null!;

		public int RequiredQuantity { get; set; }
	}
}

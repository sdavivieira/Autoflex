using Autoflex.Models.ProductRawMaterials;

namespace Autoflex.Models.RawMaterials
{
	public class RawMaterial
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public int StockQuantity { get; set; }

		public ICollection<ProductRawMaterial> ProductRawMaterials { get; set; }
			= new List<ProductRawMaterial>();
	}

}

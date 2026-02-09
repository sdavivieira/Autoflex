using Autoflex.Models.ProductRawMaterials;
using Autoflex.Repository.Interfaces;

namespace Autoflex.Services.Interfaces
{
	public interface IProductRawMaterialService
	{
		Task AddAsync(ProductRawMaterial association);
		Task DeleteAsync(int productId, int rawMaterialId);
        Task UpdateAsync(ProductRawMaterial association);
    }

}

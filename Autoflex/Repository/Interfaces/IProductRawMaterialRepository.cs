using Autoflex.Models.ProductRawMaterials;

namespace Autoflex.Repository.Interfaces
{
    public interface IProductRawMaterialRepository : IRepositoryBase<ProductRawMaterial>
    {
        Task<ProductRawMaterial> GetAsync(int productId, int rawMaterialId);
    }
}

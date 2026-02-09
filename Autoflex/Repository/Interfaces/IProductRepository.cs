using Autoflex.Models.Products;

namespace Autoflex.Repository.Interfaces
{
    public interface IProductRepository: IRepositoryBase<Product>
    {
		Task<IEnumerable<Product>> GetWithRawMaterialsAsync();      
		Task<Product?> GetByIdWithRawMaterialsAsync(int id);

	}
}

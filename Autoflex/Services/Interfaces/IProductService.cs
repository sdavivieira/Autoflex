using Autoflex.Models.Products;
using Autoflex.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autoflex.Services.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetAllAsync();
		Task<Product?> GetByIdAsync(int id);
		Task AddAsync(Product product);
		Task UpdateAsync(Product product);
		Task DeleteAsync(int id);
		Task<Product?> GetByIdWithRawMaterialsAsync(int id);

	}


}

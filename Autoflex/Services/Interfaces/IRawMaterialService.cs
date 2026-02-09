using Autoflex.Models.RawMaterials;
using Autoflex.Repository.Interfaces;

namespace Autoflex.Services.Interfaces
{
    public interface IRawMaterialService
    { 
        Task<IEnumerable<RawMaterial>> GetAllAsync();
        Task AddAsync(RawMaterial rawMaterial);
        Task UpdateAsync(RawMaterial rawMaterial);
        Task DeleteAsync(int id);
        Task<RawMaterial?> GetByIdAsync(int id);

	}
}

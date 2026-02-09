using Autoflex.Models.RawMaterials;

namespace Autoflex.Repository.Interfaces
{
	public interface IRawMaterialRepository : IRepositoryBase<RawMaterial>
	{
		Task<RawMaterial?> GetByIdWithAssociationsAsync(int id);
	}

}

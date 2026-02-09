using Autoflex.Models.ProductRawMaterials;
using Autoflex.Models.RawMaterials;
using Autoflex.Repository.Interfaces;
using Autoflex.Services;
using Moq;
using Xunit;

namespace Autoflex.Tests.BackEnd.Services
{
	public class RawMaterialServiceTests
	{
		private readonly Mock<IRawMaterialRepository> _repoMock;
		private readonly RawMaterialService _service;

		public RawMaterialServiceTests()
		{
			_repoMock = new Mock<IRawMaterialRepository>();
			_service = new RawMaterialService(_repoMock.Object);
		}

		[Fact]
		public async Task AddAsync_ShouldCallRepositoryAdd()
		{
			var material = new RawMaterial { Name = "Material", StockQuantity = 10 };
			await _service.AddAsync(material);
			_repoMock.Verify(r => r.AddAsync(material), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_ShouldThrowException_WhenMaterialUsed()
		{
			var material = new RawMaterial
			{
				Id = 1,
				ProductRawMaterials = new List<ProductRawMaterial> { new ProductRawMaterial() }
			};
			_repoMock.Setup(r => r.GetByIdWithAssociationsAsync(1)).ReturnsAsync(material);

			await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(1));
		}

		[Fact]
		public async Task UpdateAsync_ShouldThrowException_WhenStockTooLow()
		{
			var existing = new RawMaterial
			{
				Id = 1,
				StockQuantity = 5,
				ProductRawMaterials = new List<ProductRawMaterial>
				{
					new ProductRawMaterial { RequiredQuantity = 10 }
				}
			};
			_repoMock.Setup(r => r.GetByIdWithAssociationsAsync(1)).ReturnsAsync(existing);

			var update = new RawMaterial { Id = 1, StockQuantity = 5, Name = "X" };
			await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(update));
		}
	}
}

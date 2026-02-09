using Autoflex.Models.ProductRawMaterials;
using Autoflex.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Autoflex.Controllers
{
	public class ProductRawMaterialController : Controller
	{
		private readonly IProductRawMaterialService _service;

		public ProductRawMaterialController(IProductRawMaterialService service)
		{
			_service = service;
		}

		[HttpPost]
		public async Task<IActionResult> Add(int productId, int selectedRawMaterialId, int requiredQuantity)
		{
			try
			{
				var association = new ProductRawMaterial
				{
					ProductId = productId,
					RawMaterialId = selectedRawMaterialId,
					RequiredQuantity = requiredQuantity
				};

				await _service.AddAsync(association);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> Update(int productId, int rawMaterialId, int requiredQuantity)
		{
			try
			{
				var association = new ProductRawMaterial
				{
					ProductId = productId,
					RawMaterialId = rawMaterialId,
					RequiredQuantity = requiredQuantity
				};

				await _service.UpdateAsync(association);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int productId, int rawMaterialId)
		{
			try
			{
				await _service.DeleteAsync(productId, rawMaterialId);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}
	}
}

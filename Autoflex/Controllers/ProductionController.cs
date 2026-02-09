using Autoflex.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class ProductionController : Controller
{
	private readonly IProductionService _service;

	public ProductionController(IProductionService service)
	{
		_service = service;
	}

	public IActionResult Index()
	{
		return RedirectToAction("Suggestions");
	}

	public async Task<IActionResult> Suggestions()
	{
		var suggestions = await _service.GetSuggestionsAsync();
		return View(suggestions);
	}
}

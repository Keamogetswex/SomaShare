using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomaShareSS3.ViewModels;

namespace SomaShareSS3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            var categories = new List<CategoryViewModel>();

            return View(categories);
        }

        public IActionResult Details(int id)
        {
            // Placeholder: In a real application, this would query a database
            var category = new CategoryViewModel();

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Placeholder: In a real application, this would save to a database
            _logger.LogInformation($"Category '{model.Name}' created.");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            // Placeholder: In a real application, this would query a database
            var category = new CategoryViewModel();

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Placeholder: In a real application, this would update the database
            _logger.LogInformation($"Category '{model.Name}' updated.");

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Placeholder: In a real application, this would delete from the database
            _logger.LogInformation($"Category with id {id} deleted.");

            return RedirectToAction(nameof(Index));
        }
    }
}

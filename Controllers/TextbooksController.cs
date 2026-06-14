using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomaShareSS3.Models;
using SomaShareSS3.ViewModels;

namespace SomaShareSS3.Controllers
{
    public class TextbooksController : Controller
    {
        private readonly ILogger<TextbooksController> _logger;

        public TextbooksController(ILogger<TextbooksController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // temp:placeholder
            var textbooks = new List<TextbookListItemViewModel>();

            return View(textbooks);
        }

        public IActionResult Details(int id)
        {
            // temp:placeholder
            var textbook = new TextbookDetailsViewModel();

            if (textbook == null)
            {
                return NotFound();
            }

            return View(textbook);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TextbookEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // temp:placeholder
            _logger.LogInformation($"Textbook '{model.Title}' created.");

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            // temp:placeholder
            var textbook = new TextbookEditViewModel();

            if (textbook == null)
            {
                return NotFound();
            }

            return View(textbook);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TextbookEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // temp:placeholder
            _logger.LogInformation($"Textbook '{model.Title}' updated.");

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // temp:placeholder
            _logger.LogInformation($"Textbook with id {id} deleted.");

            return RedirectToAction(nameof(Index));
        }
    }
}

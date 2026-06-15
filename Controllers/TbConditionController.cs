using Microsoft.AspNetCore.Mvc;
using SomaShareWebApp.Models.Enums;
using SomaShareWebApp.ViewModels;

namespace SomaShareWebApp.Controllers
{
    public class TbConditionController : Controller
    {
        private readonly ILogger<TbConditionController> _logger;

        public TbConditionController(ILogger<TbConditionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var conditions = Enum.GetValues(typeof(TextbookCondition))
                .Cast<TextbookCondition>()
                .Select(static c => new TextbookConditionViewModel((int)c, default) { Name = c.ToString() })
                .ToList();

            return View(conditions);
        }

        public IActionResult Details(int id, string? description)
        {
            if (!Enum.IsDefined(typeof(TextbookCondition), id))
            {
                return NotFound();
            }

            var condition = (TextbookCondition)id;
            var model = new TextbookConditionViewModel(id, Description: description) { Name = condition.ToString() };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TextbookConditionViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "Condition name is required.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (!Enum.IsDefined(typeof(TextbookCondition), id))
            {
                return NotFound();
            }

            var condition = (TextbookCondition)id;
            var model = new TextbookConditionViewModel(id, default) { Name = condition.ToString() };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TextbookConditionViewModel model)
        {
            if (!Enum.IsDefined(typeof(TextbookCondition), id))
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "Condition name is required.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!Enum.IsDefined(typeof(TextbookCondition), id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

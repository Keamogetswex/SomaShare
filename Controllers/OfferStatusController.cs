using Microsoft.AspNetCore.Mvc;
using SomaShareWebApp.Models.Enums;
using SomaShareWebApp.ViewModels;

namespace SomaShareWebApp.Controllers
{
    public class OfferStatusController : Controller
    {
        private readonly ILogger<OfferStatusController> _logger;

        public OfferStatusController(ILogger<OfferStatusController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var statuses = Enum.GetValues(typeof(OfferStatus))
                .Cast<OfferStatus>()
                .Select(s => new OfferStatusViewModel { Id = (int)s, Name = s.ToString() })
                .ToList();

            return View(statuses);
        }

        public IActionResult Details(int id)
        {
            if (!Enum.IsDefined(typeof(OfferStatus), id))
            {
                return NotFound();
            }

            var status = (OfferStatus)id;
            var model = new OfferStatusViewModel { Id = id, Name = status.ToString() };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OfferStatusViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "Status name is required.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (!Enum.IsDefined(typeof(OfferStatus), id))
            {
                return NotFound();
            }

            var status = (OfferStatus)id;
            var model = new OfferStatusViewModel { Id = id, Name = status.ToString() };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OfferStatusViewModel model)
        {
            if (!Enum.IsDefined(typeof(OfferStatus), id))
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "Status name is required.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!Enum.IsDefined(typeof(OfferStatus), id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

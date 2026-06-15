using Microsoft.AspNetCore.Mvc;
using SomaShareWebApp.Models.Enums;
using SomaShareWebApp.ViewModels;

namespace SomaShareWebApp.Controllers
{
    public class TransactionStatusController : Controller
    {
        private readonly ILogger<TransactionStatusController> _logger;

        public TransactionStatusController(ILogger<TransactionStatusController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var statuses = Enum.GetValues(typeof(TransactionStatus))
                .Cast<TransactionStatus>()
                .Select(s => new TransactionStatusViewModel { Id = (int)s, Name = s.ToString() })
                .ToList();

            return View(statuses);
        }

        public IActionResult Details(int id)
        {
            if (!Enum.IsDefined(typeof(TransactionStatus), id))
            {
                return NotFound();
            }

            var status = (TransactionStatus)id;
            var model = new TransactionStatusViewModel { Id = id, Name = status.ToString() };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactionStatusViewModel model)
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
            if (!Enum.IsDefined(typeof(TransactionStatus), id))
            {
                return NotFound();
            }

            var status = (TransactionStatus)id;
            var model = new TransactionStatusViewModel { Id = id, Name = status.ToString() };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TransactionStatusViewModel model)
        {
            if (!Enum.IsDefined(typeof(TransactionStatus), id))
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
            if (!Enum.IsDefined(typeof(TransactionStatus), id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

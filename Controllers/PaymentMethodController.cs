using Microsoft.AspNetCore.Mvc;
using SomaShareWebApp.Models.Enums;
using SomaShareWebApp.ViewModels;

namespace SomaShareWebApp.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly ILogger<PaymentMethodController> _logger;

        public PaymentMethodController(ILogger<PaymentMethodController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var methods = Enum.GetValues(typeof(PaymentMethod))
                .Cast<PaymentMethod>()
                .Select(m => new PaymentMethodViewModel { Id = (int)m, Name = m.ToString() })
                .ToList();

            return View(methods);
        }

        public IActionResult Details(int id)
        {
            if (!Enum.IsDefined(typeof(PaymentMethod), id))
            {
                return NotFound();
            }

            var method = (PaymentMethod)id;
            var model = new PaymentMethodViewModel { Id = id, Name = method.ToString() };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PaymentMethodViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "Payment method name is required.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (!Enum.IsDefined(typeof(PaymentMethod), id))
            {
                return NotFound();
            }

            var method = (PaymentMethod)id;
            var model = new PaymentMethodViewModel { Id = id, Name = method.ToString() };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PaymentMethodViewModel model)
        {
            if (!Enum.IsDefined(typeof(PaymentMethod), id))
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "Payment method name is required.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!Enum.IsDefined(typeof(PaymentMethod), id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

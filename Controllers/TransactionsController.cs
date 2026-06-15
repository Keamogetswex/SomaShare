using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomaShareWebApp.Models.Enums;
using SomaShareWebApp.ViewModels;

namespace SomaShareWebApp.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            // temp:placeholder
            var transactions = new List<TransactionListItemViewModel>();

            return View(transactions);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            // temp:placeholder
            var transaction = new TransactionEditViewModel();

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        [Authorize]
        public IActionResult Create(int offerId)
        {
            var model = new TransactionEditViewModel { OfferId = offerId };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactionEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // temp:placeholder
            _logger.LogInformation($"Transaction for offer {model.OfferId} created with amount {model.Amount}.");

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            // temp:placeholder
            var transaction = new TransactionEditViewModel();

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TransactionEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // temp:placeholder
            _logger.LogInformation($"Transaction {id} updated.");

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, TransactionStatus status)
        {
            // temp:placeholder
            _logger.LogInformation($"Transaction {id} status updated to {status}.");

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Complete(int id)
        {
            // temp:placeholder
            _logger.LogInformation($"Transaction {id} marked as completed.");

            return RedirectToAction(nameof(Index));
        }
    }
}

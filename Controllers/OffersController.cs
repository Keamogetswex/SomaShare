using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomaShareWebApp.Models.Enums;
using SomaShareWebApp.ViewModels;

namespace SomaShareWebApp.Controllers
{
    public class OffersController : Controller
    {
        private readonly ILogger<OffersController> _logger;

        public OffersController(ILogger<OffersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult My()
        {
            // temp:placeholder
            var offers = new List<OfferListItemViewModel>();
            return View(offers);
        }

        [Authorize]
        public IActionResult Received()
        {
            // temp:placeholder
            var offers = new List<OfferListItemViewModel>();
            return View(offers);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            // temp:placeholder
            var offer = new OfferEditViewModel();

            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OfferEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // temp:placeholder
            _logger.LogInformation($"Offer of {model.OfferAmount} created for textbook {model.TextbookId}.");

            return RedirectToAction(nameof(My));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accept(int id)
        {
            // temp:placeholder
            _logger.LogInformation($"Offer {id} accepted.");

            return RedirectToAction(nameof(Received));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int id)
        {
            // temp:placeholder
            _logger.LogInformation($"Offer {id} rejected.");

            return RedirectToAction(nameof(Received));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(int id)
        {
            // temp:placeholder
            _logger.LogInformation($"Offer {id} withdrawn.");

            return RedirectToAction(nameof(My));
        }
    }
}

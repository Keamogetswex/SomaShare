using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomaShareWebApp.ViewModels;

namespace SomaShareWebApp.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ILogger<ReviewsController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Mine()
        {
            // temp:placeholder
            var reviews = new List<ReviewListItemViewModel>();
            return View(reviews);
        }

        [Authorize]
        public IActionResult Given()
        {
            // temp:placeholder
            var reviews = new List<ReviewListItemViewModel>();
            return View(reviews);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            // temp:placeholder
            var review = new ReviewEditViewModel();

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [Authorize]
        public IActionResult Create(int transactionId)
        {
            var model = new ReviewEditViewModel { TransactionId = transactionId };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Rating < 1 || model.Rating > 5)
            {
                ModelState.AddModelError("Rating", "Rating must be between 1 and 5.");
                return View(model);
            }

            // temp:placeholder
            _logger.LogInformation($"Review for transaction {model.TransactionId} created with rating {model.Rating}.");

            return RedirectToAction(nameof(Given));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            // temp:placeholder
            var review = new ReviewEditViewModel();

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReviewEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Rating < 1 || model.Rating > 5)
            {
                ModelState.AddModelError("Rating", "Rating must be between 1 and 5.");
                return View(model);
            }

            // temp:placeholder
            _logger.LogInformation($"Review {id} updated with rating {model.Rating}.");

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // temp:placeholder
            _logger.LogInformation($"Review {id} deleted.");

            return RedirectToAction(nameof(Given));
        }
    }
}

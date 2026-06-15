using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomaShareWebApp.Models;
using SomaShareWebApp.ViewModels;

namespace SomaShareWebApp.Controllers
{
    public class WantedAdsController : Controller
    {
        private readonly ILogger<WantedAdsController> _logger;

        public WantedAdsController(ILogger<WantedAdsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // temp:placeholder
            var wantedAds = new List<WantedAdListItemViewModel>();

            return View(wantedAds);
        }

        public IActionResult Details(int id)
        {
            // temp:placeholder
            var wantedAd = new WantedAdEditViewModel();

            if (wantedAd == null)
            {
                return NotFound();
            }

            return View(wantedAd);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WantedAdEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // temp:placeholder
            _logger.LogInformation($"Wanted ad '{model.Title}' created.");

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            // temp:placeholder
            var wantedAd = new WantedAdEditViewModel();

            if (wantedAd == null)
            {
                return NotFound();
            }

            return View(wantedAd);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, WantedAdEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // temp:placeholder
            _logger.LogInformation($"Wanted ad '{model.Title}' updated.");

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // temp:placeholder
            _logger.LogInformation($"Wanted ad with id {id} deleted.");

            return RedirectToAction(nameof(Index));
        }
    }
}

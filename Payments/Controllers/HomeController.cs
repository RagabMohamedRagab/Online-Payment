using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Payments.Gateway;
using Payments.GateWayModels;
using System.Diagnostics;

namespace Payments.Controllers {
    public class HomeController : Controller {
        private readonly IStripeGateWay _stripeGateWay;
        private readonly IToastNotification _toast;

        public HomeController(IToastNotification toast, IStripeGateWay stripeGateWay)
        {
            _toast = toast;
            _stripeGateWay = stripeGateWay;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCheckOutSession(Amount amount)
        {
            string result = await _stripeGateWay.OnlinePaymentStripe(amount);
            if (!ModelState.IsValid || string.IsNullOrEmpty(result))
            {
                _toast.AddErrorToastMessage("Failed");
                return View(amount);
            }
            return Redirect(result);
        }
        [HttpGet]
        public IActionResult Done()
        {

            return View();
        }
    }
}
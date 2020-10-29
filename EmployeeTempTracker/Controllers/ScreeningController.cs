using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeTempTracker.Controllers {
    public class ScreeningController : Controller {
        private readonly ILogger<LoginController> _logger;
        public ScreeningController(ILogger<LoginController> logger) {
            _logger = logger;
        }
        private ScreeningControllerLogic viewProcessor_ = new ScreeningControllerLogic();

        // GET https://capstone.ohitski.org/Screening
        public IActionResult Index() {
            return viewProcessor_.Index();
        }
    }
}
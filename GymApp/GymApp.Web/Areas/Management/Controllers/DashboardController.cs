using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Areas.Management.Controllers {
    public class DashboardController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

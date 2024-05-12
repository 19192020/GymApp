using GymApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Areas.Management.Controllers { 
    public class AboutController : Controller {
        GymDbContext db = new GymDbContext();
        // GET: AboutController
        public ActionResult Index() {
            About about = db.Abouts.FirstOrDefault();
            return View(about);
        }

        // GET: AboutController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: AboutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

    }
}

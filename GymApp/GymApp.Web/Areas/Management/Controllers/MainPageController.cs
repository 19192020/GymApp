using GymApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Areas.Management.Controllers {
	public class MainPageController : Controller {
		GymDbContext db = new GymDbContext();
		// GET: MainPageController
		public ActionResult Index() {
			MainPage mainpage = db.MainPages.FirstOrDefault();
			return View(mainpage);
		} 
		  
		// GET: MainPageController/Edit/5
		public ActionResult Edit(int id) {
			return View();
		}

		// POST: MainPageController/Edit/5
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

using GymApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Areas.Management.Controllers {
    public class TrainerController : Controller {
        GymDbContext db = new GymDbContext();
        // GET: TrainerController
        public ActionResult Index() {
            var trainers = db.Trainers.Where(c => c.Deleted == false)
                .ToList();

            return View(trainers);
        }

        // GET: TrainerController/Details/5
        public ActionResult Details(int id) {
            var trainer = db.Trainers.Find(id);
            if (trainer == null) {
                return RedirectToAction("Index");
            }
            return View(trainer);
        }

        // GET: TrainerController/Create
        [HttpGet]
        public ActionResult Create() {
            return View();
        }

        // POST: TrainerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trainer model) {
            try {
                if(ModelState.IsValid) {
                    model.Status = true;
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = 0;
                    model.Deleted = false;
                    db.Trainers.Add(model);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch {
                return View(model);
            }
        }

        // GET: TrainerController/Edit/5
        public ActionResult Edit(int id) {
            var trainer = db.Trainers.Find(id);
            if (trainer == null) {
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // POST: TrainerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trainer model) {
            try {
                if (ModelState.IsValid) {
                    var editTrainer = db.Trainers.Find(model.Id);
                    if (editTrainer == null) {
                        return RedirectToAction(nameof(Index));
                    }

                    editTrainer.FullName = model.FullName;
                    editTrainer.Email = model.Email;
                    editTrainer.Phone = model.Phone;
                    editTrainer.Status = model.Status;
                    editTrainer.UpdatedBy = 0;
                    editTrainer.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch {
                return View(model);
            }
        }

        // GET: TrainerController/Delete/5
        public ActionResult Delete(int id) {
            var trainer = db.Trainers.Find(id);
            if (trainer == null) {
                return RedirectToAction(nameof(Index));
            }
            return View(trainer); 
        }

        // POST: TrainerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            try {
                var trainer = db.Trainers.Find(id);
                if (trainer == null) {
                    return RedirectToAction(nameof(Index));
                }
                //soft delete
                trainer.Deleted = true;
                trainer.UpdatedDate = DateTime.Now;
                trainer.UpdatedBy = 0;
                db.SaveChanges();

                //hard delete
                //db.Trainers.Remove(trainer);
                //db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

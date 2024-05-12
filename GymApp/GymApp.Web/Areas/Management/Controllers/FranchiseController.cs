using GymApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;

namespace GymApp.Web.Areas.Management.Controllers
{
    
    public class FranchiseController : Controller
       
    {
        GymDbContext db = new GymDbContext();
        // GET: FranchiseController
        public ActionResult Index()
        {
            var franchise = db.Franchises.Where(c => c.Deleted == false)
               .ToList();
            return View(franchise);
        }

  
      

        // GET: FranchiseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FranchiseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Franchise model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Status = true;
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = 0;
                    model.Deleted = false;
                    db.Franchises.Add(model);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: FranchiseController/Edit/5
        public ActionResult Edit(int id)
        {
            var franchise = db.Franchises.Find(id);
            if (franchise == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(franchise);
        }

        // POST: FranchiseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Franchise model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editFrancise = db.Franchises.Find(model.Id);
                    if (editFrancise == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                   
                    editFrancise.Email = model.Email;
                    editFrancise.Phone = model.Phone;
                    editFrancise.WorkingTime = model.WorkingTime;
                    editFrancise.UpdatedBy = 0;
                    editFrancise.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: FranchiseController/Delete/5
        public ActionResult Delete(int id)
        {
            var franchise = db.Franchises.Find(id);
            if (franchise == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(franchise);
        }

        // POST: FranchiseController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCom(int id)
        {
            try
            {
                var franchise = db.Franchises.Find(id);
                if (franchise == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                //soft delete
                franchise.Deleted = true;
                franchise.UpdatedDate = DateTime.Now;
                franchise.UpdatedBy = 0;
                db.SaveChanges();

                //hard delete
                //db.Trainers.Remove(trainer);
                //db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

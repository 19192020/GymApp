using GymApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Web.Areas.Management.Controllers
{
    public class CategoryController : Controller
    {
        GymDbContext db = new GymDbContext();
        // GET: CategoryController
        public ActionResult Index()
        {
            var category = db.Categories.Where(c => c.Deleted == false)
               .ToList();

            return View(category);

        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = db.Categories.Include("Courses").FirstOrDefault(c => c.Id == id);//include dahil etme birden fazla dahil edilecekse Include("Colection içine yazılan") kullanılır
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Status = true;
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = 0;
                    model.Deleted = false;
                    db.Categories.Add(model);
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

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editCategory = db.Categories.Find(model.Id);
                    if (editCategory == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    editCategory.Title = model.Title;
                    editCategory.Description = model.Description;
                    editCategory.Status = model.Status;
                    editCategory.UpdatedBy = 0;
                    editCategory.UpdatedDate = DateTime.Now;
                 
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


        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}

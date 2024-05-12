using GymApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Web.Areas.Management.Controllers
{
    public class CourseController : Controller
    {
        GymDbContext db = new GymDbContext();
     
        public ActionResult Index()
        {
            var coursess = db.Courses.Where(c => c.Deleted == false)
                .Include("Trainer")
                .Include("Category")
                .ToList();
            return View(coursess);
        }

        // GET: CoruseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CoruseController/Create
        public ActionResult Create()//bire çok ilişkide bağlılıktan dolayı kullanılır
        {
            ViewBag.TrainerId = new SelectList(db.Trainers.Where(x => x.Deleted == false && x.Status), "Id", "FullName", null);
            //birden fazla verileri taşımak için yapılır 
            ViewBag.CategoryId = new SelectList(db.Categories.Where(x => x.Deleted == false && x.Status), "Id", "Title", null);
            return View();
        }

        // POST: CoruseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Status = true;
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = 0;
                    model.Deleted = false;
                    db.Courses.Add(model);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.TrainerId = new SelectList(db.Trainers.Where(x => x.Deleted == false && x.Status), "Id", "FullName", model.TrainerId);
                //birden fazla verileri taşımak için yapılır 
                ViewBag.CategoryId = new SelectList(db.Categories.Where(x => x.Deleted == false && x.Status), "Id", "Title", model.CategoryId);
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: CoruseController/Edit/5
        public ActionResult Edit(int id)
        {
            var course = db.Courses.Find(id);
            ViewBag.TrainerId = new SelectList(db.Trainers
                .Where(x => x.Deleted == false && x.Status), "Id", "FullName", course.TrainerId);
            ViewBag.CategoryId = new SelectList(db.Categories
                .Where(x => x.Deleted == false && x.Status), "Id", "Title", course.CategoryId);
            if (course == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // POST: CoruseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Course model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editCourse = db.Courses.Find(model.Id);
                    if (editCourse == null)
                    {
                        return View(model);
                    }
                    editCourse.TrainerId = model.TrainerId;
                    editCourse.CategoryId = model.CategoryId;
                    editCourse.StartTime = model.StartTime;
                    editCourse.EndTime = model.EndTime;
                    editCourse.UserCount = model.UserCount;
                    editCourse.Status = model.Status;
                    editCourse.UpdatedBy = 0;
                    editCourse.UpdatedDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.TrainerId = new SelectList(db.Trainers
               .Where(x => x.Deleted == false && x.Status), "Id", "FullName", model.TrainerId);//hazırı çekiyo
                ViewBag.CategoryId = new SelectList(db.Categories
                    .Where(x => x.Deleted == false && x.Status), "Id", "Title", model.CategoryId);
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

       

    // GET: CoruseController/Delete/5
    public ActionResult Delete(int id)
        {
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return RedirectToAction(nameof(Index));
            }
            
            // Silme işlemini onaylamak için bir mesaj göster
            //ViewBag.Message = $"Bu kursu silmek istediğinize emin misiniz?";

            return View(course);
            
        }

        // POST: CoruseController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var course = db.Courses.Find(id);
                if (course == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                course.Deleted = true;
                course.UpdatedDate = DateTime.Now;
                course.UpdatedBy = 0;
                db.SaveChanges();

               

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }



    }
}

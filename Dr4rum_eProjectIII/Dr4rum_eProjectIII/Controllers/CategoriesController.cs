using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dr4rum_eProjectIII.Models;

namespace Dr4rum_eProjectIII.Controllers
{
    public class CategoriesController : Controller
    {
        private DbDocEntities db = new DbDocEntities();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.Group);
            return View(categories.ToList());
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.Group_Name = new SelectList(db.Groups, "Group_Name", "Group_Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category_Name,SetV,Group_Name")] Category category)
        {
            ViewBag.Group_Name = new SelectList(db.Groups, "Group_Name", "Group_Name", category.Group_Name);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception)
            {
                ViewBag.err = "This Category name had been existed yet! Please choice an other name!";
                return View();
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Group_Name = new SelectList(db.Groups, "Group_Name", "Group_Name", category.Group_Name);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_Name,SetV,Group_Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Group_Name = new SelectList(db.Groups, "Group_Name", "Group_Name", category.Group_Name);
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var resultCategory = db.Categories.Where(a => a.Category_Name == id).SingleOrDefault();
            if (resultCategory != null)
            {
                resultCategory.SetV = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Recovery_Category(string id)
        {
            var resultCategory = db.Categories.Where(a => a.Category_Name == id).SingleOrDefault();
            if (resultCategory != null)
            {
                resultCategory.SetV = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
    public class GroupsController : Controller
    {
        private DbDocEntities db = new DbDocEntities();

        // GET: Groups
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Group_Name,SetV")] Group group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Groups.Add(group);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(group);
            }
            catch (Exception)
            {
                ViewBag.err = "This Group name had been existed yet! Please choice an other name!";
                return View();
            }
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var resultGroup = db.Groups.Where(a => a.Group_Name == id).SingleOrDefault();
            if (resultGroup != null)
            {
                resultGroup.SetV = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Recovery_Groups(string id)
        {
            var resultGroup = db.Groups.Where(a => a.Group_Name == id).SingleOrDefault();
            if (resultGroup != null)
            {
                resultGroup.SetV = true;
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

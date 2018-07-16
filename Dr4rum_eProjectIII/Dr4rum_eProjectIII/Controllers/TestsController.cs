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
    public class TestsController : Controller
    {
        private Dr4rumEntities db = new Dr4rumEntities();

        // GET: Tests
        public ActionResult Index()
        {
            var topics = db.Topics.Include(t => t.Account).Include(t => t.Category);
            return View(topics.ToList());
        }

        // GET: Tests/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Tests/Create
        public ActionResult Create()
        {
            ViewBag.Acc_ID = new SelectList(db.Accounts, "Acc_ID", "UserName");
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Group_Name");
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Topic_Title,Acc_ID,Category_Name,setV,Topic_Info,Report,date")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Acc_ID = new SelectList(db.Accounts, "Acc_ID", "UserName", topic.Acc_ID);
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Group_Name", topic.Category_Name);
            return View(topic);
        }

        // GET: Tests/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.Acc_ID = new SelectList(db.Accounts, "Acc_ID", "UserName", topic.Acc_ID);
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Group_Name", topic.Category_Name);
            return View(topic);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Topic_Title,Acc_ID,Category_Name,setV,Topic_Info,Report,date")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Acc_ID = new SelectList(db.Accounts, "Acc_ID", "UserName", topic.Acc_ID);
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Group_Name", topic.Category_Name);
            return View(topic);
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
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

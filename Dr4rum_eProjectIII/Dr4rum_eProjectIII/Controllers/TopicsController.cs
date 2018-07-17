using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dr4rum_eProjectIII.Models;
using System.IO;
using Microsoft.Ajax.Utilities;

namespace Dr4rum_eProjectIII.Controllers
{
    public class TopicsController : Controller
    {
        private Dr4rumEntities db = new Dr4rumEntities();

        // GET: Topics
        public ActionResult Index()
        {
            //    var topics = db.Topics.Include(t => t.Acc_ID==1).Include(t => t.Category);
            //    return View(topics.ToList());
            var topics = db.Topics.Where(t => t.Acc_ID == 1 && t.setV == true).ToList();
            return View(topics);
        }

        // GET: Topics/Details/5
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

        // GET: Topics/Create
        public ActionResult Create()
        {
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name").Distinct();
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Topic_Title,Acc_ID,Category_Name,setV,Topic_Info,Report,date")] Topic topic, HttpPostedFileBase Topic_Info)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return View(topic);
            }
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name", topic.Category_Name);
            return View(topic);
        }

        // GET: Topics/Edit/5
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
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name", topic.Category_Name);
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Topic_Title,Acc_ID,Category_Name,setV,Topic_Info,Report,date")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Acc_ID = new SelectList(db.Accounts, "Acc_ID", "UserName", topic.Acc_ID);
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name", topic.Category_Name);
            return View(topic);
        }

        //Delete
        public ActionResult _PartialDelete(string id)
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
            return PartialView("_PartialDelete", topic); /* Cai return nay nhan 2 tham so ("view de chuyen sang", gia tri truyen qua ben trang do)---ong mo lai coi thu coi*/
        }
        // POST: ChuongTrinhs/Delete/5
        [HttpPost, ActionName("_PartialDelete")]
        [ValidateAntiForgeryToken]
        public JsonResult _PartialDeleteConfirmed(string id)
        {
            Topic topic = db.Topics.Find(id);
            var result = db.Topics.Where(t => t.Topic_Title == id).SingleOrDefault();

            result.setV = false;
            db.SaveChanges();
            return Json(new { Success = true });
        }
    }
}

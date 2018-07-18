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
using System.Threading.Tasks;

namespace Dr4rum_eProjectIII.Controllers
{
    public class TopicsController : Controller
    {
        private Dr4rumEntities db = new Dr4rumEntities();

        // GET: Topics
        public async Task<ActionResult> Index()
        {
            //    var topics = db.Topics.Include(t => t.Acc_ID==1).Include(t => t.Category);
            //    return View(topics.ToList());
            var topics = db.Topics.Where(t => t.Acc_ID == 1 && t.setV == true).ToList();
            return View(topics);
        }

        // GET: Topics/Details/5
        public async Task<ActionResult> Details(string id)
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
        public async Task<ActionResult> Create()
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
        public async Task<ActionResult> Create([Bind(Include = "Topic_Title,Acc_ID,Category_Name,setV,Topic_Info,Report,date")] Topic topic, HttpPostedFileBase Topic_Info)
        {
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name");
            var result = db.Topics.Where(t => t.Topic_Title == topic.Topic_Title).SingleOrDefault();
            if (result == null)
            {
                if (ModelState.IsValid)
                {
                    db.Topics.Add(topic);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Your Topic Infomation is not Valid! Please check again ( May be your number character not > 100 )");
                    return View(topic);
                }
            }
            else
            {
                ModelState.AddModelError("","This topic has been existed!");
                return View(topic);
            }
        }

        // GET: Topics/Edit/5
        public async Task<ActionResult> Edit(string id)
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
        public async Task<ActionResult> Edit([Bind(Include = "Topic_Title,Acc_ID,Category_Name,setV,Topic_Info,Report,date")] Topic topic)
        {
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name", topic.Category_Name);
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Your Topic Infomation is not Valid! Please check again ( May be your number character not > 100 )");
                return View(topic);
            }
        }

        //Delete
        public async Task<ActionResult>_PartialDelete(string id)
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
            return PartialView("_PartialDelete", topic);
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
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dr4rum_eProjectIII.Models;

namespace Dr4rum_eProjectIII.Controllers
{
    public class AdminTopicManagerController : Controller
    {
        Dr4rumEntities3 db = new Dr4rumEntities3();
        // GET: AdminTopicManager
        public async Task<ActionResult> Index()
        {
            List<Topic> lstTopic = db.Topics.ToList();
            return View(lstTopic);
        }

        // GET: AdminTopicManager/Details/title
        public async Task<ActionResult> Details(string title)
        {
            if (title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(title);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }


        // GET: Lecturers/Edit/title
        public async Task<ActionResult> Edit(string title)
        {
            if (title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(title);
            if (title == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name", topic.Category_Name);
            return View(topic);
        }

        // POST: Lecturers/Edit/title
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Topic_Title,Acc_ID,Category_Name,setV,Topic_Info,Report,date")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name", topic.Category_Name);
            return View(topic);
        }
        // GET: Lecturers/Delete/title
        public async Task<ActionResult> Delete(string title)
        {
            var result = db.Topics.Where(t => t.Topic_Title == title).SingleOrDefault();
            if(result != null)
            {
                result.setV = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Lecturers/Recovery/title
        public async Task<ActionResult> Recovery(string title)
        {
            var result = db.Topics.Where(t => t.Topic_Title == title).SingleOrDefault();
            if (result != null)
            {
                result.setV = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
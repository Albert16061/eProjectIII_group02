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
        Dr4rumEntities db = new Dr4rumEntities();
        // GET: AdminTopicManager
        public ActionResult Index()
        {
            List<Topic> lstTopic = db.Topics.ToList();
            return View(lstTopic);
        }

        // GET: AdminTopicManager/Details/title
        public ActionResult Details(string title)
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


        // GET: Lecturers/Edit/5
        public ActionResult Edit(string title)
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

        // POST: Lecturers/Edit/5
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
            ViewBag.Category_Name = new SelectList(db.Categories, "Category_Name", "Category_Name", topic.Category_Name);
            return View(topic);
        }



    }
}
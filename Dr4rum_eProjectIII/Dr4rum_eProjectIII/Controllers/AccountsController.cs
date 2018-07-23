using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dr4rum_eProjectIII.Models;

namespace Dr4rum_eProjectIII.Controllers
{
    public class AccountsController : Controller
    {
        private Dr4rumEntities3 db = new Dr4rumEntities3();
        [HttpGet]
        // GET: Accounts
        public async Task<ActionResult> Index()
        {
            List<Account> ListAcc = db.Accounts.ToList();
            return View(ListAcc);
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Acc_ID,UserName,Password,FirstName,LastName,Email,Address,Birthday,Phone,Gender,Role,Incognito,SetV,Speciality,Experience,Achievement,Avatar")] Account account, HttpPostedFileBase postedFile)
        {
            //kiem tra neu dl hop le
            if (ModelState.IsValid)
            {
                //tao bien luu ten file hinh
                var filename = Path.GetFileName(postedFile.FileName);
                // luu duong dan file hinh
                var path = Path.Combine(Server.MapPath("~/Image/Avatar"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.thongbao = "Avatar is Existed!";
                }
                else
                {
                    postedFile.SaveAs(path);
                }
                account.Avatar = postedFile.FileName;
                db.Accounts.Add(account);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Acc_ID,UserName,Password,FirstName,LastName,Email,Address,Birthday,Phone,Gender,Role,Incognito,SetV,Speciality,Experience,Achievement")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/delete/5
        public ActionResult Delete(int? id)
        {
            var result = db.Accounts.Where(a => a.Acc_ID == id).SingleOrDefault();
            if (result != null)
            {
                result.SetV = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult recovery(int? id)
        {
            var result = db.Accounts.Where(a => a.Acc_ID == id).SingleOrDefault();
            if (result != null)
            {
                result.SetV = true;
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

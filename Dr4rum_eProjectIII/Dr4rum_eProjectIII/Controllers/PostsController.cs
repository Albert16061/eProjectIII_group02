﻿using System;
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
    public class PostsController : Controller
    {
        private DbDocEntities db = new DbDocEntities();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Account).Include(p => p.Topic);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult _PartialCreate()
        {
            return PartialView();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _PartialCreate([Bind(Include = "Post_ID,Topic_Tile,Acc_ID,Post_Info,Like_Num,Dislike_Num,date")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Add successfull!" });
            }
            return Json(new
            {
                Success = false,
                Message = "Please check your post info again and make sure that it is not empty or less than 10 character  !"
            });
        }

        // GET: Posts/Edit/5
        public ActionResult _PartialEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return PartialView(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _PartialEdit([Bind(Include = "Post_ID,Topic_Tile,Acc_ID,Post_Info,Like_Num,Dislike_Num,date")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true, Message = "Edit successfull!" });
            }
            return Json(new
            {
                Success = false,
                Message = "Please check your post info again and make sure that it is not empty or less than 10 character  !"
            });
        }

        // GET: Posts/Delete/5
        public ActionResult _PartialDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialDelete", post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("_PartialDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            var result = db.Posts.Where(y => y.Post_ID == id).Select(x => x.Post_ID).ToList();
            if (result.Count() > 0)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                return Json(new { Success = true,
                    Message = "Delete Successfull !"
                });
            }
            return Json(new
            {
                Success = false,
                Message = "Delete Fail !" });
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

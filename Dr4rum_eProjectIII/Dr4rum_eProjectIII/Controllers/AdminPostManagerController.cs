using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dr4rum_eProjectIII.Models;

namespace Dr4rum_eProjectIII.Controllers
{
    public class AdminPostManagerController : Controller
    {
        DbDocEntities db = new DbDocEntities();
        // GET: AdminPostManager
        public async Task<ActionResult> Index()
        {
            List<Post> lstPost = db.Posts.ToList();
            return View(lstPost);
        }


        // GET: Test/Details/5
        public async Task<ActionResult> Details(int? id)
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
        // POST: Post/Delete/5
        [HttpPost, ActionName("_PartialDelete")]
        [ValidateAntiForgeryToken]
        public JsonResult _PartialDeleteConfirmed(int? id)
        {
            Post post = db.Posts.Find(id);
            var result = db.Posts.Where(y => y.Post_ID == id).ToList();
            if (result.Count > 0)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new
            {
                Success = false,
                Message = "Cannot delete this program, there are some problem about system" });
            }
            }
    }
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dr4rum_eProjectIII.Models;

namespace Dr4rum_eProjectIII.Controllers
{
    public class AdminIndexController : Controller
    {
        Dr4rumEntities3 db = new Dr4rumEntities3();
        //Get: Account List
        public async Task<ActionResult> ListAccount()
        {
            var listA = db.Accounts.Where(a => a.SetV == true).ToList();
            return PartialView(listA);
        }
        //Get: Account Group
        public async Task<ActionResult> ListGroup()
        {
            var listG = db.Groups.Where(a => a.SetV == true).ToList();
            return PartialView(listG);
        }
        //Get: Account Category
        public async Task<ActionResult> ListCat()
        {
            var listC = db.Categories.Where(a => a.SetV == true).ToList();
            return PartialView(listC);
        }
        //Get: Account Topic
        public async Task<ActionResult> ListTopic()
        {
            var listT = db.Topics.Where(a => a.setV == true).ToList();
            return PartialView(listT);
        }
        //Get: Account Post
        public async Task<ActionResult> ListPost()
        {
            var listP = db.Posts.ToList();
            return PartialView(listP);
        }

        // GET: AdminIndex
        public async Task<ActionResult> Index()
        {
            var listGroup = db.Groups.ToList();
            return View(listGroup);
        }
    }
}
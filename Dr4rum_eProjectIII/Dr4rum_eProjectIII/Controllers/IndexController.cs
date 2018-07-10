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
    public class IndexController : Controller
    {
        Dr4rumEntities db = new Dr4rumEntities();
        // GET: Index
        public async Task<ActionResult> Index()
        {
            var listGroup = db.Groups.Where(a => a.SetV == true).ToList();
            return View(listGroup);
        }

        [HttpGet]
        public async Task<ActionResult> ListCategories(string Category_Name)
        {
            //var ListCat = (from c in db.Categories
            //              from t in db.Topics
            //              where c.Category_Name == t.Category_Name && t.Category_Name == catName &&t.setV == true
            //              orderby t.date descending
            //              select t).ToList();

           // List<Topic> topic = db.Topics.Where(a => a.Category_Name == Category_Name && a.setV == true).ToList();

            var listTopic = db.Categories.Where(a => a.Category_Name == Category_Name).ToList();
            return View(listTopic);
        }
    }
}
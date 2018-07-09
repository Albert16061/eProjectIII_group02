using System;
using System.Collections.Generic;
using System.Linq;
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
            var topicTitle = (from a in db.Groups
                              from b in db.Categories
                              from c in db.Topics
                              where a.Group_Name == b.Group_Name && b.Category_Name == c.Category_Name
                               && c.setV == true
                              orderby c.Topic_Title descending
                              select c.Topic_Title).First();
            ViewBag.key = topicTitle;
            return View(listGroup);
        }
    }
}
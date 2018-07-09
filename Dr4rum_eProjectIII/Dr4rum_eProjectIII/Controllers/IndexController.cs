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
            var AccPostTopic = (from a in db.Topics  from b in db.Accounts
                              where a.Acc_ID == b.Acc_ID  && a.setV == true && b.SetV == true
                              orderby a.date descending
                              select b.FirstName).First();
            ViewBag.key = AccPostTopic;
            return View(listGroup);
        }
    }
}
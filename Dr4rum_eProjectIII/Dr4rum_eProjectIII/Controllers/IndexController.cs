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
            return View(listGroup);
        }
    }
}
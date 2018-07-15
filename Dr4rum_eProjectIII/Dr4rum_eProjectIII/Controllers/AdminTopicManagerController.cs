using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
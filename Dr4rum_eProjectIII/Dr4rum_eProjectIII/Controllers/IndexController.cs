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
        DbDocEntities db = new DbDocEntities();
        // GET: Index
        public async Task<ActionResult> Index()
        {
            var listGroup = db.Groups.Where(a => a.SetV == true).ToList();
            return View(listGroup);
        }
        // DropdownList
        public async Task<ActionResult> CategoryDropDown()
        {
            var listGroup = db.Groups.Where(a => a.SetV == true).ToList();
            return PartialView(listGroup);
        }
        //Get: List of Group
        public async Task<ActionResult> ListGroup(string Group_name)
        {
            var listGroup = db.Groups.Where(a => a.Group_Name == Group_name).ToList();
            return View(listGroup);
        }

        //Get: List of Category
        [HttpGet]
        public async Task<ActionResult> ListCategories(string Category_Name)
        {
            var listCat = db.Categories.Where(a => a.Category_Name == Category_Name).ToList();
            return View(listCat);
        }

        //Get: Topic Infomation + Post inside
        [HttpGet]
        public async Task<ActionResult> TopicDetail(string TopicTitle)
        {
            var listTopic = db.Topics.Where(a => a.Topic_Title == TopicTitle).ToList();
            Session["topic"] = TopicTitle;
            return View(listTopic);
        }

        // GET: Index
        public async Task<ActionResult> viewOtherDetails(int? ID)
        {
            var detail = db.Accounts.Where(d => d.Acc_ID == ID).SingleOrDefault();
            return PartialView("viewOtherDetails",detail);
        }

    }
}
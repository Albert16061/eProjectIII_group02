using Dr4rum_eProjectIII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Dr4rum_eProjectIII.Controllers
{
    public class UserAccountController : Controller
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        Dr4rumEntities3 db = new Dr4rumEntities3();
        private string inputPasswordMD5;

        // GET: UserAccount
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult LoginPartial()
        {
            return PartialView("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName, Password")]Account account)
        {
            inputPasswordMD5 = CreateMD5(account.Password).ToString();
            if (account.UserName == null || account.Password == null)
            {
                return View();
            }
            else
            {
                var res = db.Accounts.Where(s => s.UserName == account.UserName && s.Password == inputPasswordMD5).SingleOrDefault();
                if (res != null)
                {
                    Account userProfile = new Account()
                    {
                        Acc_ID = res.Acc_ID,
                        UserName = res.UserName,
                        FirstName = res.FirstName,
                        LastName = res.LastName,
                        Email = res.Email,
                        Phone = res.Phone,
                        Address = res.Address,
                        Birthday = res.Birthday,
                        Gender = Convert.ToBoolean(res.Gender),
                        Speciality = res.Speciality,
                        Achievement = res.Achievement,
                        Experience = res.Experience,
                        Avatar = res.Avatar,
                    };
                    Session["UserAccount"] = userProfile;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Username or Password!";
                    return View(account);
                }
            }

        }



        public ActionResult Logout()
        {
            Session["UserAccount"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account account)
        {
            

            if (ModelState.IsValid == true)
            {
                var rs = db.Accounts.Where(s => s.UserName == account.UserName).SingleOrDefault();
                if (rs != null)
                {
                    ViewBag.MessageForUsername = "Username is used";
                    return View();
                }
                else
                {
                    account.Password = CreateMD5(account.Password);
                    db.Accounts.Add(account);
                    db.SaveChanges();

                    ViewBag.MessageForUsername = "Success";
                    return View();
                    // return RedirectToAction("Login", "UserAccount");
                }
            }
            return View(account);


        }


        public ActionResult Information()
        {
            if (Session["UserInformation"] == null)
                return RedirectToAction("Login");

            Account account = (Account)Session["UserInformation"];
            return View(account);
        }
    }
}
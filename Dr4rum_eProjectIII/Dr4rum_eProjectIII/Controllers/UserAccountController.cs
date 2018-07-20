using Dr4rum_eProjectIII.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
            if (account.UserName != null || account.Password != null)
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
                            Incognito = res.Incognito,
                            Speciality = res.Speciality,
                            Achievement = res.Achievement,
                            Experience = res.Experience,
                            Avatar = res.Avatar,
                            Role = res.Role
                        };
                        Session["UserAccount"] = userProfile;
                        if (userProfile.Role == "A")
                        {
                            return RedirectToAction("Index", "adminIndex");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Username or Password!";
                        return View(account);
                    }
                }
            }
            else
            {
                return View();
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
        public ActionResult Register(Account account, FormCollection form)
        {
            string cfpassword = Convert.ToString(form["txtConfirmPassword"]);

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


        public ActionResult Information(int ID)
        {
            var accID = ID;
            var listInfo = db.Accounts.Where(a => a.Acc_ID == accID).ToList();
            return View(listInfo);
            //Account account = (Account)Session["UserInformation"];
            //return View(account);
        }

        public async Task<ActionResult> EditInformation()
        {
            if (Session["UserAccount"] == null)
                return RedirectToAction("Login");

            Account account = (Account)Session["UserAccount"];
            return View(account);
        }
        [HttpPost]
        public ActionResult EditInformation(Account account, HttpPostedFileBase filehinh)
        {
            if (account.Password != null)
            {

                if (Session["UserAccount"] == null)
                    return RedirectToAction("Login");


                string cfpasswordMD5 = CreateMD5(account.Password);

                Account rs = db.Accounts.SingleOrDefault(s => s.UserName == account.UserName);
                if (filehinh == null)
                {
                    ViewBag.ErrorUpImage = " Choose image";
                    return View();
                }

                if (rs != null)
                {
                    if (rs.Password == cfpasswordMD5.ToLower())
                    {
                        rs.UserName = account.UserName;
                        rs.LastName = account.LastName;
                        rs.FirstName = account.FirstName;
                        rs.Address = account.Address;
                        rs.Phone = account.Phone;
                        rs.Email = account.Email;
                        rs.Birthday = account.Birthday;
                        rs.Gender = account.Gender;
                        rs.Incognito = account.Incognito;
                        rs.Role = account.Role;
                        rs.Speciality = account.Speciality;
                        rs.Experience = account.Experience;
                        rs.Achievement = account.Achievement;
                        var filename = Path.GetFileName(filehinh.FileName);
                        var path = Path.Combine(Server.MapPath("~/Image/avatar"), filename);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ErrorUpImage = "Image existed";
                        }
                        else
                        {
                            filehinh.SaveAs(path);
                        }
                        rs.Avatar = filehinh.FileName;
                        db.SaveChanges();

                        Session["UserAccount"] = rs;

                        return RedirectToAction("Information", new { ID = rs.Acc_ID });
                    }
                    else
                    {
                        ViewBag.ErrorConfirmPassword = "Invalid Password";
                        return View(account);
                    }
                }
                ViewBag.ErrorConfirmPassword = "Account not existed";
                return View(account);
            }
            else
            {
                ViewBag.ErrorConfirmPassword = "Please enter confirm password";
                return View(account);
            }
        }

        public ActionResult ChangePassword()
        {
            if (Session["UserAccount"] == null) return RedirectToAction("Login");
            Account userProfile = (Account)Session["UserAccount"];
            return View(userProfile);
        }

        [HttpPost]
        public ActionResult ChangePassword([Bind(Include = "UserName,Password")] Account account, FormCollection form)
        {
            var oldpass = Convert.ToString(form["old_password"]);
            var cfpass = Convert.ToString(form["confirm_password"]);
            var rs = db.Accounts.SingleOrDefault(s => s.UserName == account.UserName);
            if (rs != null)
            {
                if (rs.Password.ToLower() != CreateMD5(oldpass).ToLower())
                {
                    ViewBag.ErrorOld_password = "Old password is invalid";
                }
                else if (cfpass != account.Password)
                {
                    ViewBag.ErrorConfirm_password = "Password not match";
                }
                else if (oldpass == account.Password)
                {
                    ViewBag.ErrorNew_password = "The new password must be different from the old password";
                }
                else
                {
                    rs.Password = CreateMD5(account.Password).ToLower();
                    db.SaveChanges();
                    return RedirectToAction("Information", new { @ID = rs.Acc_ID });
                }
            }
            return View(account);
        }


        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword([Bind(Include = "UserName")] Account account)
        {
            if (account.UserName != null)
            {

                var rs = db.Accounts.SingleOrDefault(s => s.UserName == account.UserName);
                if (rs != null)
                {
                    return RedirectToAction("ForgetPasswordAddNewPass", new { ID = rs.Acc_ID });
                }
                else
                {
                    ViewBag.ErrormessageForgetPassword = "Not Found";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }



        public ActionResult ForgetPasswordAddNewPass(int ID)
        {
            var rs = db.Accounts.SingleOrDefault(s => s.Acc_ID == ID);
            string phone = rs.Phone;
            string rsphone = rs.Phone.Substring(phone.Length - 3);
            ViewBag.RsPhone = rsphone;
            return View(rs);
        }
        [HttpPost]
        public ActionResult ForgetPasswordAddNewPass([Bind(Include = "Acc_ID,Password,Phone")] Account account, FormCollection form)
        {
            if (account.UserName != null || account.Password != null)
            {
                string oldphone = form["old_phone"].ToString();
                string cfpassword = form["confirm_password"].ToString();
                string newpassword = account.Password;
                string passwordMD5 = CreateMD5(newpassword);

                Account rs = db.Accounts.SingleOrDefault(s => s.Acc_ID == account.Acc_ID);


                if (rs.Phone == oldphone && cfpassword == newpassword)
                {
                    rs.Password = passwordMD5.ToLower();
                    db.SaveChanges();
                    return RedirectToAction("Login");

                }
                else
                {
                    if (account.Phone != oldphone)
                    {
                        ViewBag.ErrorOld_Phone = "Isvalid phone";
                        return View(account);
                    }
                    else if (cfpassword != newpassword)
                    {
                        ViewBag.ErrorConfirm_password = "ConfirmPassword not match";
                        return View(account);
                    }
                }
            }
            return View(account);
        }

        public ActionResult UploadImage()
        {
            ViewData["Message"] = "Please choose a image to Upload";
            const string folderThumbPath = "/Content/Images/";
            var directoryThumbs = new DirectoryInfo(Server.MapPath(folderThumbPath));
            var listImages = directoryThumbs.GetFiles().Select(file => file.Name).ToList();
            ViewData["listImages"] = listImages;
            return View("/Views/Home/UploadImage.aspx");
        }
    }

        
}
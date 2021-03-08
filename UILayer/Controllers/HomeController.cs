using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using DataAccessLayer.Repo;
using System.IO;
using System.Web.Security;

namespace UILayer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        IPhoto photorepo = null;
        IUserInterface userRepo = null;
        ICategory catRepo = null;
        ICart cartRepo = null;
        ISell sellRepo = null;
        public HomeController()
        {
            photorepo = new PhotoRepo();
            userRepo = new UserRepo();
            catRepo = new CategaryRepo();
            cartRepo = new CartRepo();
            sellRepo = new Sell();
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(RM_UserDetailModel model)
        {
            int i = userRepo.registerUser(model);
            if(i >= 1)
            {
                int id = userRepo.GetUserId(model.user_email);
                cartRepo.registerCartUser(id);
                return RedirectToAction("SignIn");
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string user_email, string user_password)
        {
            bool check = userRepo.signIn(user_email, user_password);
            if (check == true)
            {
                FormsAuthentication.SetAuthCookie(user_email, false);
                if (user_email != "rahulmeharkar195@gmail.com")
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.msg = "Wrong Password";
                return View();
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult SignOut(RM_UserDetailModel model)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "Home");

        }
        public JsonResult GetCategary()
        {
            var catlist = catRepo.getAllCategory();
            var jsonResult = Json(catlist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [Authorize]
        [HttpGet]
        public ActionResult addPhoto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addPhoto(HttpPostedFileBase useralbumphoto_photo, PhotoModel model)
        {
            if (ModelState.IsValid)
            {
                if (useralbumphoto_photo.ContentLength > 0)
                {
                    string path = Server.MapPath("~/photo");
                    string filename = Path.GetFileName(useralbumphoto_photo.FileName);
                    string fullpath = Path.Combine(path, filename);
                    string imagepath = Path.Combine("/photo/", filename);
                    try
                    {
                        useralbumphoto_photo.SaveAs(fullpath);
                    }
                    catch (Exception ex)
                    {
                        return Content(ex.Message);
                    }
                    model.useralbumphoto_photo = imagepath;
                    int i = photorepo.AddPhoto(model);
                    if (i >= 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.msg = "Failed";
                        return View();
                    }
                }
                else
                {
                    ViewBag.msg = "Failed";
                    return View();
                }
            }
            else
            {
                ViewBag.msg = "Failed";
                return View();
            }
            }
        public JsonResult ListAllPhoto()
        {
            var photo = photorepo.AllPhoto();
            var jsonResult = Json(photo, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [Authorize]
        [HttpGet]
        public ActionResult EditPhoto(int id)
        {
            TempData["useralbumphoto_id"] = id;
           PhotoViewModel model = new PhotoViewModel();
           model = photorepo.PhotoDetailAdmin(id);
           return View(model);
        }
        [HttpPost]
        public ActionResult EditPhoto(HttpPostedFileBase useralbumphoto_photo, PhotoModel model)
        {
            model.useralbumphoto_id = Convert.ToInt32(TempData["useralbumphoto_id"]);
            int i = photorepo.EditPhoto(model);
            if(i >= 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Failed";
                return View() ;
            }
        }
        [Authorize]
        public ActionResult DeletePhoto(int photo_id)
        {
            int i = photorepo.DeletePhoto(photo_id);
            if(i >= 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult SellDetail()
        {
            decimal avg2 = sellRepo.AlbumAvgQuantity(2);
            decimal avg3 = sellRepo.AlbumAvgQuantity(3);
            decimal avgsell2 = sellRepo.AlbumAvgSellQuantity(2);
            decimal avgsell3 = sellRepo.AlbumAvgSellQuantity(3);
            decimal t1 = avgsell2 + avgsell3;
            decimal t2 = avg2 + avg3;
            decimal persell2 = avgsell2/t1;
            decimal persell3 = avgsell3 /t1;
            decimal per2 = avg2 / t2;
            decimal per3 = avg3 / t2;

            persell2 = persell2 * 100;
            persell3 = persell3 * 100;
            ViewBag.totalsell = ((persell2 + persell3)/ (persell2 + persell3 + per2 + per3)) *100;
            per2 = per2 * 100;
            per3 = per3 * 100;
            ViewBag.totalsell2 = ((per2 + per3)/ (persell2 + persell3 + per2 + per3)) *100;

            persell2 = (persell2 / (persell2 + persell3 + per2 + per3)) * 100;
            persell3 = (persell3 / (persell2 + persell3 + per2 + per3)) * 100;
            per2 = (per2 / (persell2 + persell3 + per2 + per3)) * 100;
            per3 = (per3 / (persell2 + persell3 + per2 + per3)) * 100;

            ViewBag.avg2 = per2;
            ViewBag.avg3 = per3;
            ViewBag.avgsell2 = persell2;
            ViewBag.avgsell3 = persell3;
            //int total = avgsell2 + avgsell3;

            return View();
        }
        public JsonResult BarGraph()
        {
            var data = photorepo.ImageSell();
            var jsonResult = Json(data, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult SellChart()
        {
            return View();
        }
    }
    }
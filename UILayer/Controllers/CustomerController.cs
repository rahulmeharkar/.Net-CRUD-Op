using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using DataAccessLayer.Repo;
namespace UILayer.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        IPhoto photorepo = null;
        IUserInterface userRepo = null;
        ICategory catRepo = null;
        ICart cartRepo = null;
        public CustomerController()
        {
            photorepo = new PhotoRepo();
            userRepo = new UserRepo();
            catRepo = new CategaryRepo();
            cartRepo = new CartRepo();
        }
        public static int GenerateRandomInt(Random rnd)
        {
            return rnd.Next();
        }
        [Authorize]
        public ActionResult Index()
        {
            //int id = userRepo.GetUserId(User.Identity.Name);
            //ViewBag.totalprise = photorepo.totalPrise(User.Identity.Name);
            return View();
        }
        public JsonResult ListAllPhoto()
        {
            var photo = photorepo.AllPhoto();
            var jsonResult = Json(photo, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public JsonResult ListCartItem()
        {
            var photo = cartRepo.cartItem(User.Identity.Name);
            var jsonResult = Json(photo, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [Authorize]
        public ActionResult AddCart(int photo_id,int quantity)
        {
            int id = userRepo.GetUserId(User.Identity.Name);
            bool check = cartRepo.CheckAlreadyPresent(photo_id,id);
            if (check == true)
            {
                int x = cartRepo.updatecart(photo_id, id, quantity);
                return RedirectToAction("CartData");
            }
            else
            {
                if (id != null)
                {
                    int i = cartRepo.addCart(photo_id, id, quantity);
                    if (i >= 1)
                    {
                        int j = cartRepo.IncreeaseCartItem(id);
                        if (j >= 1)
                        {
                            photorepo.DecreaseQuantity(photo_id);
                            return RedirectToAction("CartData");
                        }
                        else
                        {
                            return RedirectToAction("CartData");
                        }

                    }
                    else
                    {
                        return RedirectToAction("CartData");
                    }
                }
                else
                {
                    return RedirectToAction("CartData");
                }
            }
        }
        [Authorize]
        public ActionResult RemoveItem(int photo_id)
        {
            int id = userRepo.GetUserId(User.Identity.Name);
            if (id != null)
            {
                int i = cartRepo.removeItem(id, photo_id);
                if (i >= 1)
                { 
                    photorepo.IncreaseQuantity(photo_id);
                    return RedirectToAction("CartData");
                }
                else
                {
                    return RedirectToAction("CartData");
                }
            }
            else
            {
                return RedirectToAction("CartData");
            }
        }
        public JsonResult TotalPrise()
        {
            int total_prise = photorepo.totalPrise(User.Identity.Name);
            var jsonResult = Json(total_prise, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        [Authorize]
        [HttpGet]
        public ActionResult Checkout()
        {
            ViewBag.totalprise = photorepo.totalPrise(User.Identity.Name);
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Checkout(UserCheckoutModel model)
        {
            Random rnd = new Random();
            model.order_number = GenerateRandomInt(rnd);
            model.user_id = userRepo.GetUserId(User.Identity.Name);
            model.cart_id = cartRepo.getCartId(model.user_id);
            model.total_prise = photorepo.totalPrise(User.Identity.Name);
            int i = cartRepo.CheckOut(model);
            if (i >= 1)
            {
                cartRepo.DeleteAllCartItem(model.user_id);
                return RedirectToAction("CheckoutDetail");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public JsonResult PhotoDetail(int id)
        {
            var result = photorepo.PhotoDetail(id);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public JsonResult UpdatePrise(int photo_id,int cart_itemquantity)
        {
            int i = 0;
            PhotoModel mod = new PhotoModel();
            mod = cartRepo.detail(photo_id,userRepo.GetUserId(User.Identity.Name));
            if(mod.cart_itemquantity > cart_itemquantity)
            {
                i = cartRepo.DecreesCartItem(userRepo.GetUserId(User.Identity.Name),photo_id);
            }
            else
            {
                i = cartRepo.updateprise(photo_id, userRepo.GetUserId(User.Identity.Name));
            }
            
            if(i >= 0)
            {
                photorepo.UpdatesellQuantity(cart_itemquantity, photo_id);
                int total_prise = cartRepo.totalPrise(userRepo.GetUserId(User.Identity.Name), photo_id);
                var jsonResult = Json(total_prise, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            else
            {
                var message = "failed";
                var jsonResult = Json(message, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
        }
        [HttpGet]
        public ActionResult CheckoutDetail()
        {
            int id = userRepo.GetUserId(User.Identity.Name);
            UserCheckoutModel mod = new UserCheckoutModel();
            mod = cartRepo.OrderDetails(id);
            ViewBag.order_number = mod.order_number;
            ViewBag.total = mod.total_prise;
            return View();
        }
        public JsonResult GetImageDetail(int id)
        {
            var result = photorepo.PhotoDetail(id);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult CartData()
        {
            return View();
        }
    }
}
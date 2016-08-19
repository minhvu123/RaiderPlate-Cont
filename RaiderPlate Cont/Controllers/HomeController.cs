using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaiderPlate_Cont.Models;
using PagedList;
namespace RaiderPlate_Cont.Controllers
{
    public class HomeController : Controller
    {
        private raiderplateEntities entity = new raiderplateEntities();   
        public ActionResult Index(int page =1 , int pageSize =10)
        {
            List<hall> listHall = new List<hall>();
            foreach (var r in entity.halls.ToList())
                listHall.Add(r); 
            ViewBag.Halls = new SelectList(listHall, "HallID", "HallName");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        [HttpGet]
        public JsonResult GetRestaurants(String ID)
        {
            int hallID = int.Parse(ID);
            List<location> listRest = new List<location>();
            foreach(location i in entity.locations.ToList())
            {
                if (i.HallID == hallID)
                    listRest.Add(i);
            }
            return Json(new SelectList(listRest, "LocationID", "LocationName"), JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult GetMenu(String ID)
        {
            int i =int.Parse(ID);
            
            List<product> listProduct = new List<product>();
            foreach(product p in entity.products.ToList())
            {
                if (p.locationID == i)
                {
                    product temp = new product
                    {
                        locationID = i,
                        productID = p.productID,
                        productName = p.productName,
                        productPrice = p.productPrice
                    };
                    listProduct.Add(temp);
                }   
            }
            
            return Json(listProduct.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
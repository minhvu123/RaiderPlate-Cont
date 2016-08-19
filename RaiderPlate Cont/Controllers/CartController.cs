using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaiderPlate_Cont.Models;

namespace RaiderPlate_Cont.Controllers
{
    

    public class CartController : Controller
    {
        // GET: Cart
        private raiderplateEntities entity = new raiderplateEntities();
        private int isExisted(int id)
        {
            List<Item> cartList = (List<Item>)(Session["cart"]);
            for (int i = 0; i < cartList.Count; i++)
            {
                if (cartList[i].Product.productID == id)
                    return i;
            }
            return -1;
        }

        
        public ActionResult Index()
        {      
            return View();
        }

        public JsonResult GetCart()
        {
            List<Item> cart = new List<Item>();
            if (Session["cart"] != null)
            {
                foreach( Item it in (List<Item>)Session["cart"])
                {
                    cart.Add(new Item
                    {
                        Product = new product
                        {
                            locationID = it.Product.locationID,
                            productID = it.Product.productID,
                            productName = it.Product.productName,
                            productPrice = it.Product.productPrice
                        },
                        Quantity = it.Quantity
                    });
                }
            }
            return Json(cart.ToList(), JsonRequestBehavior.AllowGet);
        }
        public void Add(int id)
        {
            List<Item> cart = new List<Item>();
            if (Session["cart"] == null)
            {
                cart.Add(new Item(entity.products.Find(id), 1));
            }
            else
            {
                cart = (List<Item>)Session["cart"];
                int i = isExisted(id);
                if (i != -1)
                    cart[i].Quantity++;
                else
                    cart.Add(new Item(entity.products.Find(id), 1));
            }
            Session["cart"] = cart;
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> DeletedId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            if(DeletedId != null)
            {
                foreach (int id in DeletedId)
                {
                    cart.RemoveAt(isExisted(id));
                }
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(FormCollection form)
         {
             String[] quantities = form.GetValues("quantity");
             List<Item> cart = (List<Item>)Session["cart"];
             for (int i = 0; i<cart.Count; i++)
             {
                if (Convert.ToInt32(quantities[i]) != 0)
                    cart[i].Quantity = Convert.ToInt32(quantities[i]);
                else
                    cart.RemoveAt(i);
             }
 
             Session["cart"] = cart;
             return RedirectToAction("Index");
         }
    }
}
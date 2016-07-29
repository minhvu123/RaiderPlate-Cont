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
            List<Item> cart = new List<Item>();
            if (Session["cart"] != null)
                cart = (List<Item>)Session["cart"];            
            return View(cart);
        }
        public  ActionResult Add(int id)
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
            return RedirectToAction("Index");
        }
    }
}
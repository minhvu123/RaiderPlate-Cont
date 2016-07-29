using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RaiderPlate_Cont.Models
{
    public class Item
    {
        private product product = new product();
        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public product Product
        {
            get
            {
                return product;
            }

            set
            {
                product = value;
            }
        }

        public Item() { }

        public Item(product p, int quantity)
        {
            Product = p;
            this.Quantity = quantity;
        }

        
    }
}
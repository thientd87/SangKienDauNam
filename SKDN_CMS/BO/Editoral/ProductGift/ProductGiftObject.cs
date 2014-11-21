using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFISYS.BO.Editoral.ProductGift
{
    public class ProductGiftObject
    {
        public ProductGiftObject()
        {

        }
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _productGift;

        public string ProductGift
        {
            get { return _productGift; }
            set { _productGift = value; }
        }

        private int _order;

        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
    }
}
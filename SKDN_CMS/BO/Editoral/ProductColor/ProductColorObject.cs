using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFISYS.BO.Editoral.ProductColor
{

    public class ProductColorObject
    {
        public ProductColorObject()
        {

        }
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _colorName;

        public string ColorName
        {
            get { return _colorName; }
            set { _colorName = value; }
        }

        private string _colorCode;

        public string ColorCode
        {
            get { return _colorCode; }
            set { _colorCode = value; }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

    }
}
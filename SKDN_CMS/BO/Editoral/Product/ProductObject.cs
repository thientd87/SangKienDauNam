using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFISYS.BO.Editoral.Product
{
    public class Product
    {
        public Product()
        {

        }
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private string _productNameEn;

        public string ProductName_En
        {
            get { return _productNameEn; }
            set { _productNameEn = value; }
        }

        private string _productSumary;

        public string ProductSumary
        {
            get { return _productSumary; }
            set { _productSumary = value; }
        }

        private string _productSumaryEn;

        public string ProductSumary_En
        {
            get { return _productSumaryEn; }
            set { _productSumaryEn = value; }
        }

        private string _productDescription;

        public string ProductDescription
        {
            get { return _productDescription; }
            set { _productDescription = value; }
        }

        private string _productDescriptionEn;

        public string ProductDescription_En
        {
            get { return _productDescriptionEn; }
            set { _productDescriptionEn = value; }
        }

        private int _productCategory;

        public int ProductCategory
        {
            get { return _productCategory; }
            set { _productCategory = value; }
        }

        private int _productColor;

        public int ProductColor
        {
            get { return _productColor; }
            set { _productColor = value; }
        }

        private long _productCost;

        public long ProductCost
        {
            get { return _productCost; }
            set { _productCost = value; }
        }

        private int _productType;

        public int ProductType
        {
            get { return _productType; }
            set { _productType = value; }
        }

        private string _productAvatar;

        public string ProductAvatar
        {
            get { return _productAvatar; }
            set { _productAvatar = value; }
        }

        private int _productLayout;

        public int ProductLayout
        {
            get { return _productLayout; }
            set { _productLayout = value; }
        }
        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public string ProductVideo
        {
            get { return _ProgramVideo; }
            set { _ProgramVideo = value; }
        }

        public string ProductTag
        {
            get { return _ProgramTag; }
            set { _ProgramTag = value; }
        }

        public long ProductViewCount
        {
            get { return _ProductViewCount; }
            set { _ProductViewCount = value; }
        }

        private string _ProgramVideo;
        private string _ProgramTag;
        private long _ProductViewCount;
        private long _ProductRate;
        private int _ProductRateCount;
        private string _ProductOtherCat;

        public string ProductOtherCat
        {
            get { return _ProductOtherCat; }
            set { _ProductOtherCat = value; }
        }

    }
}
using System;
using System.Collections.Generic;
using DFISYS.BO.Editoral.Product_Category;

namespace DFISYS.BO.Editoral
{
    [Serializable]
    public class ProductCategory
    {
        private int iD;
        private string product_Category_Name;
        private string product_Category_Name_En;
        private string product_Category_Desc;
        private string product_Category_Desc_En;
        private int product_Category_CatParent_ID = 0;
        private bool isActive =true;
        private int menuType;
        private string product_Category_Image;
        private int _Product_Category_Order;
        

        public ProductCategory()
        {
        }
       
        public ProductCategory(int iD, string product_Category_Name, string product_Category_Name_En,
                                string product_Category_Desc, string product_Category_Desc_En,
                                int product_Category_CatParent_ID, bool isActive, int menuType,
                                string product_Category_Image, int Product_Category_Order)
        {
            this.iD = iD;
            this.product_Category_Name = product_Category_Name;
            this.product_Category_Name_En = product_Category_Name_En;
            this.product_Category_Desc = product_Category_Desc;
            this.product_Category_Desc_En = product_Category_Desc_En;
            this.product_Category_CatParent_ID = product_Category_CatParent_ID;
            this.isActive = isActive;
            this.menuType = menuType;
            this.product_Category_Image = product_Category_Image;
            this._Product_Category_Order = Product_Category_Order;
        }

        public int ID
        {
            get { return this.iD; }
            set
            {
                if (value == null)
                    throw new Exception("iD not allow nullvalue.");
                this.iD = value;
            }
        }

        public string Product_Category_Name
        {
            get { return this.product_Category_Name; }
            set { this.product_Category_Name = value; }
        }

        public string Product_Category_Name_En
        {
            get { return this.product_Category_Name_En; }
            set { this.product_Category_Name_En = value; }
        }

        public string Product_Category_Desc
        {
            get { return this.product_Category_Desc; }
            set { this.product_Category_Desc = value; }
        }

        public string Product_Category_Desc_En
        {
            get { return this.product_Category_Desc_En; }
            set { this.product_Category_Desc_En = value; }
        }

        public int Product_Category_CatParent_ID
        {
            get { return this.product_Category_CatParent_ID; }
            set { this.product_Category_CatParent_ID = value; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        public int MenuType
        {
            get { return this.menuType; }
            set { this.menuType = value; }
        }

        public string Product_Category_Image
        {
            get { return this.product_Category_Image; }
            set { this.product_Category_Image = value; }
        }

        public override string ToString()
        {
            return this.iD + "; " + this.product_Category_Name + "; " + this.product_Category_Name_En + "; " +
                   this.product_Category_Desc + "; " + this.product_Category_Desc_En + "; " +
                   this.product_Category_CatParent_ID + "; " + this.isActive + "; " + this.menuType + "; " +
                   this.product_Category_Image + "; ";
        }

        public override bool Equals(Object obj)
        {
            ProductCategory me = (ProductCategory) obj;
            bool ret = this.ID.Equals(me.ID);
            return ret;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public List<ProductCategory> CategoryChild { get; set; }

        public int Product_Category_Order
        {
            get { return _Product_Category_Order; }
            set { _Product_Category_Order = value; }
        }
    }
}




using System;

namespace DFISYS.Core.DAL {
    /// <summary>
    /// The base class for <see cref="CategoryRow"/> that 
    /// represents a record in the <c>Category</c> table.
    /// </summary>
    /// <remarks>
    /// Do not change this source code manually. Update the <see cref="CategoryRow"/>
    /// class if you need to add or change some functionality.
    /// </remarks>
    public abstract class CategoryRow_Base {
        private int _cat_ID;
        private int _channel_ID;
        private string _cat_Name;
        private string _cat_Description;
        private string _cat_DisplayURL;
        private int _cat_ParentID = 0;
        private bool _cat_ParentIDNull = false;
        private bool _cat_isColumn;
        private bool _cat_isColumnNull = true;
        private bool _cat_isHidden = false;
        private bool _cat_isHiddenNull = false;
        private int _cat_ViewNum = 0;
        private bool _cat_ViewNumNull = false;
        private int _editionType_ID;
        private bool _editionType_IDNull = true;
        private string _cat_Icon;
        private int _cat_Order;
        private bool _cat_OrderNull = true;
        private string _cat_KeyWords;
        private string _Cat_Name_En;
        private string _Cat_Description_En;
        private bool _IsActive;


        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRow_Base"/> class.
        /// </summary>
        public CategoryRow_Base() {
            // EMPTY
        }

        /// <summary>
        /// Gets or sets the <c>Cat_ID</c> column value.
        /// </summary>
        /// <value>The <c>Cat_ID</c> column value.</value>
        public int Cat_ID {
            get { return _cat_ID; }
            set { _cat_ID = value; }
        }

        public int Channel_ID {
            get { return 1; }
            set { _channel_ID = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_Name</c> column value.
        /// </summary>
        /// <value>The <c>Cat_Name</c> column value.</value>
        public string Cat_Name {
            get { return _cat_Name; }
            set { _cat_Name = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_KeyWords</c> column value.
        /// </summary>
        /// <value>The <c>Cat_KeyWords</c> column value.</value>
        public string Cat_KeyWords
        {
            get { return _cat_KeyWords; }
            set { _cat_KeyWords = value; }
        }
        /// <summary>
        /// Gets or sets the <c>Cat_KeyWords</c> column value.
        /// </summary>
        /// <value>The <c>Cat_KeyWords</c> column value.</value>
        public string Cat_Name_En
        {
            get { return _Cat_Name_En; }
            set { _Cat_Name_En = value; }
        }/// <summary>
        /// Gets or sets the <c>Cat_KeyWords</c> column value.
        /// </summary>
        /// <value>The <c>Cat_KeyWords</c> column value.</value>
        public string Cat_Description_En
        {
            get { return _Cat_Description_En; }
            set { _Cat_Description_En = value; }
        }/// <summary>
        /// Gets or sets the <c>Cat_KeyWords</c> column value.
        /// </summary>
        /// <value>The <c>Cat_KeyWords</c> column value.</value>
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        /// <summary>
        /// Gets or sets the <c>Cat_Description</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Cat_Description</c> column value.</value>
        public string Cat_Description {
            get { return _cat_Description; }
            set { _cat_Description = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_DisplayURL</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Cat_DisplayURL</c> column value.</value>
        public string Cat_DisplayURL {
            get { return _cat_DisplayURL; }
            set { _cat_DisplayURL = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_ParentID</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Cat_ParentID</c> column value.</value>
        public int Cat_ParentID {
            get {
                if (IsCat_ParentIDNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _cat_ParentID;
            }
            set {
                _cat_ParentIDNull = false;
                _cat_ParentID = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="Cat_ParentID"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsCat_ParentIDNull {
            get { return _cat_ParentIDNull; }
            set { _cat_ParentIDNull = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_isColumn</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Cat_isColumn</c> column value.</value>
        public bool Cat_isColumn {
            get {
                if (IsCat_isColumnNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _cat_isColumn;
            }
            set {
                _cat_isColumnNull = false;
                _cat_isColumn = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="Cat_isColumn"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsCat_isColumnNull {
            get { return _cat_isColumnNull; }
            set { _cat_isColumnNull = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_isHidden</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Cat_isHidden</c> column value.</value>
        public bool Cat_isHidden {
            get {
                if (IsCat_isHiddenNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _cat_isHidden;
            }
            set {
                _cat_isHiddenNull = false;
                _cat_isHidden = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="Cat_isHidden"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsCat_isHiddenNull {
            get { return _cat_isHiddenNull; }
            set { _cat_isHiddenNull = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_ViewNum</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Cat_ViewNum</c> column value.</value>
        public int Cat_ViewNum {
            get {
                if (IsCat_ViewNumNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _cat_ViewNum;
            }
            set {
                _cat_ViewNumNull = false;
                _cat_ViewNum = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="Cat_ViewNum"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsCat_ViewNumNull {
            get { return _cat_ViewNumNull; }
            set { _cat_ViewNumNull = value; }
        }

        /// <summary>
        /// Gets or sets the <c>EditionType_ID</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>EditionType_ID</c> column value.</value>
        public int EditionType_ID {
            get {
                if (IsEditionType_IDNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _editionType_ID;
            }
            set {
                _editionType_IDNull = false;
                _editionType_ID = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="EditionType_ID"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsEditionType_IDNull {
            get { return _editionType_IDNull; }
            set { _editionType_IDNull = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_Icon</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Cat_Icon</c> column value.</value>
        public string Cat_Icon {
            get { return _cat_Icon; }
            set { _cat_Icon = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Cat_Order</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Cat_Order</c> column value.</value>
        public int Cat_Order {
            get {
                if (IsCat_OrderNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _cat_Order;
            }
            set {
                _cat_OrderNull = false;
                _cat_Order = value;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="Cat_Order"/>
        /// property value is null.
        /// </summary>
        /// <value>true if the property value is null, otherwise false.</value>
        public bool IsCat_OrderNull {
            get { return _cat_OrderNull; }
            set { _cat_OrderNull = value; }
        }

        /// <summary>
        /// Returns the string representation of this instance.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString() {
            System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
            dynStr.Append(':');
            dynStr.Append("  Cat_ID=");
            dynStr.Append(Cat_ID);
            dynStr.Append("  Cat_Name=");
            dynStr.Append(Cat_Name);
            dynStr.Append("  Cat_Description=");
            dynStr.Append(Cat_Description);
            dynStr.Append("  Cat_DisplayURL=");
            dynStr.Append(Cat_DisplayURL);
            dynStr.Append("  Cat_ParentID=");
            dynStr.Append(IsCat_ParentIDNull ? (object)"<NULL>" : Cat_ParentID);
            dynStr.Append("  Cat_isColumn=");
            dynStr.Append(IsCat_isColumnNull ? (object)"<NULL>" : Cat_isColumn);
            dynStr.Append("  Cat_isHidden=");
            dynStr.Append(IsCat_isHiddenNull ? (object)"<NULL>" : Cat_isHidden);
            dynStr.Append("  Cat_ViewNum=");
            dynStr.Append(IsCat_ViewNumNull ? (object)"<NULL>" : Cat_ViewNum);
            dynStr.Append("  EditionType_ID=");
            dynStr.Append(IsEditionType_IDNull ? (object)"<NULL>" : EditionType_ID);
            dynStr.Append("  Cat_Icon=");
            dynStr.Append(Cat_Icon);
            dynStr.Append("  Cat_Order=");
            dynStr.Append(IsCat_OrderNull ? (object)"<NULL>" : Cat_Order);
            dynStr.Append("  Cat_KeyWords=");
            dynStr.Append(Cat_KeyWords);
            return dynStr.ToString();
        }
    } // End of CategoryRow_Base class
} // End of namespace



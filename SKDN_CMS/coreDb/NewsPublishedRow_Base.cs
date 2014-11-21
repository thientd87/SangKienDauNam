



using System;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="NewsPublishedRow"/> that 
	/// represents a record in the <c>NewsPublished</c> table.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="NewsPublishedRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class NewsPublishedRow_Base
	{
		private long _news_ID ;
		private int _cat_ID ;
		private bool _cat_IDNull = true;
		private string _news_Title ;
		private string _news_Subtitle ;
		private string _news_Image ;
		private string _news_ImageNote ;
		private string _news_Source ;
		private string _news_InitContent ;
		private string _news_Content ;
		private string _news_Athor ;
		private string _news_Approver ;
		private int _news_Status ;
		private bool _news_StatusNull = true;
		private DateTime _news_PublishDate ;
		private bool _news_PublishDateNull = true;
		private bool _news_isFocus ;
		private bool _news_isFocusNull = true;
		private int _news_Mode ;
		private bool _news_ModeNull = true;
		private string _news_Relation ;
		private double _news_Rate ;
		private bool _news_RateNull = true;
		private DateTime _news_ModifedDate ;
		private bool _news_ModifedDateNull = true;
		private string _news_OtherCat ;
		private bool _isComment ;
		private bool _isCommentNull = true;
		private bool _isUserRate ;
		private bool _isUserRateNull = true;
		private int _template ;
		private bool _templateNull = true;
		private string _icon ;
		private int _wordCount=0;
        private string _extension1;
        private string _extension2;
        private string _extension3;
        private int _extension4;
        private bool _extension4Null = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="NewsPublishedRow_Base"/> class.
		/// </summary>
		public NewsPublishedRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>News_ID</c> column value.
		/// </summary>
		/// <value>The <c>News_ID</c> column value.</value>
		public long News_ID
		{
			get { return _news_ID; }
			set { _news_ID = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Cat_ID</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Cat_ID</c> column value.</value>
		public int Cat_ID
		{
			get
			{
				if(IsCat_IDNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _cat_ID;
			}
			set
			{
				_cat_IDNull = false;
				_cat_ID = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Cat_ID"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsCat_IDNull
		{
			get { return _cat_IDNull; }
			set { _cat_IDNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Title</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Title</c> column value.</value>
		public string News_Title
		{
			get { return _news_Title; }
			set { _news_Title = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Subtitle</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Subtitle</c> column value.</value>
		public string News_Subtitle
		{
			get { return _news_Subtitle; }
			set { _news_Subtitle = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Image</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Image</c> column value.</value>
		public string News_Image
		{
			get { return _news_Image; }
			set { _news_Image = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_ImageNote</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_ImageNote</c> column value.</value>
		public string News_ImageNote
		{
			get { return _news_ImageNote; }
			set { _news_ImageNote = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Source</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Source</c> column value.</value>
		public string News_Source
		{
			get { return _news_Source; }
			set { _news_Source = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_InitContent</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_InitContent</c> column value.</value>
		public string News_InitContent
		{
			get { return _news_InitContent; }
			set { _news_InitContent = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Content</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Content</c> column value.</value>
		public string News_Content
		{
			get { return _news_Content; }
			set { _news_Content = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Athor</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Athor</c> column value.</value>
		public string News_Athor
		{
			get { return _news_Athor; }
			set { _news_Athor = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Approver</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Approver</c> column value.</value>
		public string News_Approver
		{
			get { return _news_Approver; }
			set { _news_Approver = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Status</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Status</c> column value.</value>
		public int News_Status
		{
			get
			{
				if(IsNews_StatusNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _news_Status;
			}
			set
			{
				_news_StatusNull = false;
				_news_Status = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="News_Status"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNews_StatusNull
		{
			get { return _news_StatusNull; }
			set { _news_StatusNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_PublishDate</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_PublishDate</c> column value.</value>
		public DateTime News_PublishDate
		{
			get
			{
				if(IsNews_PublishDateNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _news_PublishDate;
			}
			set
			{
				_news_PublishDateNull = false;
				_news_PublishDate = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="News_PublishDate"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNews_PublishDateNull
		{
			get { return _news_PublishDateNull; }
			set { _news_PublishDateNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_isFocus</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_isFocus</c> column value.</value>
		public bool News_isFocus
		{
			get
			{
				if(IsNews_isFocusNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _news_isFocus;
			}
			set
			{
				_news_isFocusNull = false;
				_news_isFocus = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="News_isFocus"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNews_isFocusNull
		{
			get { return _news_isFocusNull; }
			set { _news_isFocusNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Mode</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Mode</c> column value.</value>
		public int News_Mode
		{
			get
			{
				if(IsNews_ModeNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _news_Mode;
			}
			set
			{
				_news_ModeNull = false;
				_news_Mode = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="News_Mode"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNews_ModeNull
		{
			get { return _news_ModeNull; }
			set { _news_ModeNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Relation</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Relation</c> column value.</value>
		public string News_Relation
		{
			get { return _news_Relation; }
			set { _news_Relation = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_Rate</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_Rate</c> column value.</value>
		public double News_Rate
		{
			get
			{
				if(IsNews_RateNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _news_Rate;
			}
			set
			{
				_news_RateNull = false;
				_news_Rate = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="News_Rate"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNews_RateNull
		{
			get { return _news_RateNull; }
			set { _news_RateNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_ModifedDate</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_ModifedDate</c> column value.</value>
		public DateTime News_ModifedDate
		{
			get
			{
				if(IsNews_ModifedDateNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _news_ModifedDate;
			}
			set
			{
				_news_ModifedDateNull = false;
				_news_ModifedDate = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="News_ModifedDate"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsNews_ModifedDateNull
		{
			get { return _news_ModifedDateNull; }
			set { _news_ModifedDateNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>News_OtherCat</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>News_OtherCat</c> column value.</value>
		public string News_OtherCat
		{
			get { return _news_OtherCat; }
			set { _news_OtherCat = value; }
		}

		/// <summary>
		/// Gets or sets the <c>isComment</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>isComment</c> column value.</value>
		public bool isComment
		{
			get
			{
				if(IsisCommentNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _isComment;
			}
			set
			{
				_isCommentNull = false;
				_isComment = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="isComment"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsisCommentNull
		{
			get { return _isCommentNull; }
			set { _isCommentNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>isUserRate</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>isUserRate</c> column value.</value>
		public bool isUserRate
		{
			get
			{
				if(IsisUserRateNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _isUserRate;
			}
			set
			{
				_isUserRateNull = false;
				_isUserRate = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="isUserRate"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsisUserRateNull
		{
			get { return _isUserRateNull; }
			set { _isUserRateNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Template</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Template</c> column value.</value>
		public int Template
		{
			get
			{
				if(IsTemplateNull)
					throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _template;
			}
			set
			{
				_templateNull = false;
				_template = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Template"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsTemplateNull
		{
			get { return _templateNull; }
			set { _templateNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>Icon</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>Icon</c> column value.</value>
		public string Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		public int WordCount
		{
			get { return _wordCount; }
			set { _wordCount = value; }
		}


        public string Extension1
        {
            get { return _extension1; }
            set { _extension1 = value; }
        }

        public string Extension2
        {
            get { return _extension2; }
            set { _extension2 = value; }
        }

        public string Extension3
        {
            get { return _extension3; }
            set { _extension3 = value; }
        }

        public int Extension4
        {
            get { return _extension4; }
            set { _extension4 = value; }
        }

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  News_ID=");
			dynStr.Append(News_ID);
			dynStr.Append("  Cat_ID=");
			dynStr.Append(IsCat_IDNull ? (object)"<NULL>" : Cat_ID);
			dynStr.Append("  News_Title=");
			dynStr.Append(News_Title);
			dynStr.Append("  News_Subtitle=");
			dynStr.Append(News_Subtitle);
			dynStr.Append("  News_Image=");
			dynStr.Append(News_Image);
			dynStr.Append("  News_ImageNote=");
			dynStr.Append(News_ImageNote);
			dynStr.Append("  News_Source=");
			dynStr.Append(News_Source);
			dynStr.Append("  News_InitContent=");
			dynStr.Append(News_InitContent);
			dynStr.Append("  News_Content=");
			dynStr.Append(News_Content);
			dynStr.Append("  News_Athor=");
			dynStr.Append(News_Athor);
			dynStr.Append("  News_Approver=");
			dynStr.Append(News_Approver);
			dynStr.Append("  News_Status=");
			dynStr.Append(IsNews_StatusNull ? (object)"<NULL>" : News_Status);
			dynStr.Append("  News_PublishDate=");
			dynStr.Append(IsNews_PublishDateNull ? (object)"<NULL>" : News_PublishDate);
			dynStr.Append("  News_isFocus=");
			dynStr.Append(IsNews_isFocusNull ? (object)"<NULL>" : News_isFocus);
			dynStr.Append("  News_Mode=");
			dynStr.Append(IsNews_ModeNull ? (object)"<NULL>" : News_Mode);
			dynStr.Append("  News_Relation=");
			dynStr.Append(News_Relation);
			dynStr.Append("  News_Rate=");
			dynStr.Append(IsNews_RateNull ? (object)"<NULL>" : News_Rate);
			dynStr.Append("  News_ModifedDate=");
			dynStr.Append(IsNews_ModifedDateNull ? (object)"<NULL>" : News_ModifedDate);
			dynStr.Append("  News_OtherCat=");
			dynStr.Append(News_OtherCat);
			dynStr.Append("  isComment=");
			dynStr.Append(IsisCommentNull ? (object)"<NULL>" : isComment);
			dynStr.Append("  isUserRate=");
			dynStr.Append(IsisUserRateNull ? (object)"<NULL>" : isUserRate);
			dynStr.Append("  Template=");
			dynStr.Append(IsTemplateNull ? (object)"<NULL>" : Template);
			dynStr.Append("  Icon=");
			dynStr.Append(Icon);
			return dynStr.ToString();
		}
	} // End of NewsPublishedRow_Base class
} // End of namespace



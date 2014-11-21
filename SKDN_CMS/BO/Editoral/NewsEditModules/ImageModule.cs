using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Portal.BO.Editoral.NewsEditModules
{
    public class ImageModule : GeneralModule
	{
        public ImageModule(string moduleRef, string moduleId) 
            : base("/GUI/EditoralOffice/MainOffce/NewsEditModules/ImagesModule", moduleRef, moduleId)
        {
            //this.ConfigFileLocation = "/GUI/EditoralOffice/MainOffce/NewsEditModules/ImagesModule";
            //this.ModuleReference = moduleRef;
            //this.LoadRuntimeProperties();

        }

        public string ImagePath
        {
            set
            {
                setRuntimeProperty("ImagePath", value);        
            }
            get
            {
                return getRuntimeProperty("ImagePath");
            }
        }

        public string ImageLink
        {
            set
            {
                setRuntimeProperty("ImageLink", value);
            }
            get
            {
                return getRuntimeProperty("ImageLink");
            }
        }

        public string ImageTargetType
        {
            set
            {
                setRuntimeProperty("ImageTargetType", value);
            }
            get
            {
                return getRuntimeProperty("ImageTargetType");
            }
        }    

        public string ImageWidth
        {
            set
            {
                setRuntimeProperty("ImageWidth", value);
            }
            get
            {
                return getRuntimeProperty("ImageWidth");
            }
        }

        public string ImageHeight
        {
            set
            {
                setRuntimeProperty("ImageHeight", value);
            }
            get
            {
                return getRuntimeProperty("ImageHeight");
            }
        }

        public string ImageHorizontalPosition
        {
            set
            {
                setRuntimeProperty("ImageHorizontalPosition", value);
            }
            get
            {
                return getRuntimeProperty("ImageHorizontalPosition");
            }
        }

        public string ImageVerticalPosition
        {
            set
            {
                setRuntimeProperty("ImageVerticalPosition", value);
            }
            get
            {
                return getRuntimeProperty("ImageVerticalPosition");
            }
        }

        public string ImageBorderWidth
        {
            set
            {
                setRuntimeProperty("ImageBorderWidth", value);
            }
            get
            {
                return getRuntimeProperty("ImageBorderWidth");
            }
        }

        public string ImageBorderColor
        {
            set
            {
                setRuntimeProperty("ImageBorderColor", value);
            }
            get
            {
                return getRuntimeProperty("ImageBorderColor");
            }
        }

        public string ImageBorderStyle
        {
            set
            {
                setRuntimeProperty("ImageBorderStyle", value);
            }
            get
            {
                return getRuntimeProperty("ImageBorderStyle");
            }
        }
    }
}

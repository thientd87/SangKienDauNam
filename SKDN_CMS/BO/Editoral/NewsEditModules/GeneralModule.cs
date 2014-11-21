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
    public class GeneralModule : Portal.BO.Editoral.ModuleConfig.ModuleCommon
    {
        public GeneralModule(string modulePath, string moduleRef, string moduleId)
        {
            this.ConfigFileLocation = modulePath;
            this.ModuleReference = moduleRef;
            this.LoadRuntimeProperties();
        }

        public string Text
        {
            set
            {
                setRuntimeProperty("Text", value);
            }
            get
            {
                return getRuntimeProperty("Text");
            }
        }

        public string Presentation_Width
        {
            set
            {
                setRuntimeProperty("Presentation_Width", value);
            }
            get
            {
                return getRuntimeProperty("Presentation_Width");
            }
        }

        public string Presentation_Height
        {
            set
            {
                setRuntimeProperty("Presentation_Height", value);
            }
            get
            {
                return getRuntimeProperty("Presentation_Height");
            }
        }

        public string Presentation_HorizontalAlign
        {
            set
            {
                setRuntimeProperty("Presentation_HorizontalAlign", value);
            }
            get
            {
                return getRuntimeProperty("Presentation_HorizontalAlign");
            }
        }

        public string Presentation_VerticalAlign
        {
            set
            {
                setRuntimeProperty("Presentation_VerticalAlign", value);
            }
            get
            {
                return getRuntimeProperty("Presentation_VerticalAlign");
            }
        }

        public string Presentation_PaddingLeft
        {
            set
            {
                setRuntimeProperty("Presentation_PaddingLeft", value);
            }
            get
            {
                return getRuntimeProperty("Presentation_PaddingLeft");
            }
        }

        public string Presentation_PaddingRight
        {
            set
            {
                setRuntimeProperty("Presentation_PaddingRight", value);
            }
            get
            {
                return getRuntimeProperty("Presentation_PaddingRight");
            }
        }

        public string Presentation_PaddingTop
        {
            set
            {
                setRuntimeProperty("Presentation_PaddingTop", value);
            }
            get
            {
                return getRuntimeProperty("Presentation_PaddingTop");
            }
        }

        public string Presentation_PaddingBottom
        {
            set
            {
                setRuntimeProperty("Presentation_PaddingBottom", value);
            }
            get
            {
                return getRuntimeProperty("Presentation_PaddingBottom");
            }
        }
    }
}

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
    public abstract class NewsModule : Portal.API.Module
    {
        public abstract void SaveAllProperties();
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace DFISYS.BO.Editoral.Category
{
    public class CategoryXMLHepler
    {
        public CategoryXMLHepler()
        { 
            
        }

        public static string GetNumberWordByCatId(string strCatId)
        {
            string sReturn = "";
            string strFileXml = HttpContext.Current.Server.MapPath(@"\GUI\EditoralOffice\MainOffce\Menu\CatID.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileXml);
            XmlNodeList _currentCatIDNode = doc.SelectNodes("//CatID");
            XmlNodeList _currentNumberWordNode = doc.SelectNodes("//NumberWord");
            for (int i = 0; i < _currentCatIDNode.Count; i++)
            {
                if (_currentCatIDNode[i].InnerText.Trim() != strCatId)
                {
                    sReturn = _currentCatIDNode[i].InnerText.Trim();
                    break;
                }
            }
            if (sReturn == "")
            { 
                // Neu ko dinh nghia CatId nay thi se mac dinh lay default
                XmlNodeList _currentDefaultNode = doc.SelectNodes("//default");
                sReturn = _currentDefaultNode[0].InnerText.Trim();
            }

            return sReturn;
        }

        

        public static string GennerateScriptToCheckSapo()
        {
            string strFileXml = HttpContext.Current.Server.MapPath(@"\GUI\EditoralOffice\MainOffce\Menu\CatID.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileXml);

            XmlNodeList _currentDefaultNode = doc.SelectNodes("//default");
            XmlNodeList _currentCatIDNode = doc.SelectNodes("//CatID");
            XmlNodeList _currentNumberWordNode = doc.SelectNodes("//NumberWord");

            string strDefault  = _currentDefaultNode[0].InnerText.Trim();

            string sReturn = "";
            sReturn = "<script language='javascript'>\n";
            sReturn += "      var  CatNumberWord = Array(); \n ";
            sReturn += "      var  defaultWord = '" + strDefault + "';  \n";
            sReturn += "      var  numberWord = '" + strDefault + "';  \n";

            string strCatId = "";
            for (int i = 0; i < _currentCatIDNode.Count; i++)
            {
                strCatId = _currentCatIDNode[i].InnerText.Trim();
                if (strCatId != "")
                {
                    sReturn += "      CatNumberWord[" + strCatId + "] = '" + _currentNumberWordNode[i].InnerText.Trim() + "';  \n";

                }
            }

            sReturn += "</script>\n";
            return sReturn;
        }


    }
}

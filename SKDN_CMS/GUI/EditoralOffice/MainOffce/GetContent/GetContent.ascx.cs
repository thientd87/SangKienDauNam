using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HtmlAgilityPack;
using System.Collections.ObjectModel;
using NextCom.ParserFramework.HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using GafinCMS.Entities;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using NextCom;
using Portal.BO.Editoral.Newsedit;
using Portal;

namespace GafinCMS.GUI.EditoralOffice.MainOffce.GetContent {
    public partial class GetContent : UserControl {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void btnGet_Click(object sender, EventArgs e) {
            //string _link = txtLink.Text;
            //if (!String.IsNullOrEmpty(_link)) {
            //    News _obj = CrawlNews(_link);

            //    if (_obj != null && _obj != default(News)) {
            //        long _newsId = Convert.ToInt64(NewsHelper.GenNewsID());

            //        NewsEditHelper.CreateNews(_newsId, 31, string.Empty, _obj.Title, string.Empty, _obj.Source, _obj.Sapo, _obj.Content, HttpContext.Current.User.Identity.Name, false, 0, 0, string.Empty, string.Empty, string.Empty, DateTime.Now, false, false, 0, string.Empty, string.Empty, string.Empty, false, string.Empty, string.Empty, _link, 0, string.Empty, string.Empty);

            //        Response.Redirect("/office/add,templist/" + _newsId + ".aspx");
            //    }
            //    else
            //        lblMess.Text = "Trang này chưa tồn tại, vui lòng liên lạc với phòng kỹ thuật để config.";
            //}
        }

        

        /// <summary>
        /// Method to convert a custom Object to XML string
        /// </summary>
        /// <param name="pObject">Object that is to be serialized to XML</param>
        /// <returns>XML string</returns>

        //public String SerializeObject(Object pObject) {
        //    try {
        //        String XmlizedString = null;
        //        MemoryStream memoryStream = new MemoryStream();
        //        XmlSerializer xs = new XmlSerializer(typeof(News));
        //        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        //        xs.Serialize(xmlTextWriter, pObject);
        //        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        //        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        //        return XmlizedString;
        //    }
        //    catch (Exception e) {
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>

        //private String UTF8ByteArrayToString(Byte[] characters) {
        //    UTF8Encoding encoding = new UTF8Encoding();
        //    String constructedString = encoding.GetString(characters);
        //    return (constructedString);
        //}

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in Deserialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        //private Byte[] StringToUTF8ByteArray(String pXmlString) {
        //    UTF8Encoding encoding = new UTF8Encoding();
        //    Byte[] byteArray = encoding.GetBytes(pXmlString);
        //    return byteArray;
        //}

        

        

    }

    
}
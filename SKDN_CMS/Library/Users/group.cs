using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Portal.SiteSystem.Library;

namespace Portal.SiteSystem.Data.Users
{

    public class Group : DataElement
    {
        public string Name
        {
            get
            {
                return CommonXml.GetAttributeValue(Node,"name");
            }
            set 
            {
                CommonXml.SetAttributeValue(Node, "name",Common.CleanToSafeString(value));
            }
        }

        public Group(XmlNode node)
			: base(node)
        {
		}
        

    }
}

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
using System.IO;
using System.Text.RegularExpressions;

namespace Portal.BO.Editoral.EditNews
{
	public class Block
	{
		public string ObjectReference = string.Empty;
		public string ObjectType = string.Empty;
		public string ObjectVirtualPath = string.Empty;
		public string ObjectEditFormVirtualPath = string.Empty;
		public string ObjectEditFormName = string.Empty;
		public string ObjectPresentationName = string.Empty;
		public string BlockInnerHTML = string.Empty;
		public string NewsFileSetting = string.Empty;
		public Presentation BlockPresentation;
		public Presentation ObjectPresentation;
		public int BlockPosition = 0;

		public Block()
		{
			BlockPresentation = new Presentation();
			ObjectPresentation = new Presentation();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objectReference">module reference</param>
		/// <param name="objectType">module type</param>
		/// <param name="newsFileSetting">full path to news file setting</param>
		public Block(string objectReference, string objectType)
		{
			ObjectReference = objectReference;
			ObjectType = objectType;
			BlockPresentation = new Presentation();
			ObjectPresentation = new Presentation();

			if (!string.IsNullOrEmpty(objectType))
			{
				ObjectPresentationName = getModuleName(ObjectType, false);
				ObjectVirtualPath = "~/GUI/" + ObjectType + "/" + ObjectPresentationName;

				ObjectEditFormName = getModuleName(ObjectType, true);
				ObjectEditFormVirtualPath = "~/GUI/" + ObjectType + "/" + ObjectEditFormName;
			}

			//XmlDocument doc = (XmlDocument)HttpContext.Current.Session["newsFileSetting"];
			//XmlNode block = doc.SelectSingleNode("news/block/object[reference='" + ObjectReference + "']/..");
			//readBlockFromXML(block);
		}


		public Block(int blockIndex, string objectReference, string objectType)
		{
			ObjectReference = objectReference;
			ObjectType = objectType;
			BlockPosition = blockIndex + 1;
			BlockPresentation = new Presentation();
			ObjectPresentation = new Presentation();

			if (!string.IsNullOrEmpty(objectType))
			{
				ObjectVirtualPath = "~/GUI/" + ObjectType + "/" + getModuleName(ObjectType, false);
				ObjectEditFormVirtualPath = "~/GUI/" + ObjectType + "/" + getModuleName(ObjectType, true);
			}

			XmlDocument doc = (XmlDocument)HttpContext.Current.Session["newsFileSetting"];
			XmlNode block = doc.SelectSingleNode("news/block[position()=" + BlockPosition + "]");
			readBlockFromXML(block);
		}

		private void readBlockFromXML(XmlNode block)
		{
			if (block != null)
			{
				XmlNode presentation = block.SelectSingleNode("object/presentation");
				ObjectPresentation.Height = presentation.SelectSingleNode("height").InnerText;
				ObjectPresentation.Width = presentation.SelectSingleNode("width").InnerText;
				ObjectPresentation.Float = presentation.SelectSingleNode("float").InnerText;
				ObjectPresentation.Padding = presentation.SelectSingleNode("padding").InnerText;
				ObjectPresentation.Margin = presentation.SelectSingleNode("margin").InnerText;
				ObjectPresentation.Border = presentation.SelectSingleNode("border").InnerText;

				presentation = block.SelectSingleNode("presentation");
				BlockPresentation.Height = presentation.SelectSingleNode("height").InnerText;
				BlockPresentation.Width = presentation.SelectSingleNode("width").InnerText;
				BlockPresentation.Float = presentation.SelectSingleNode("float").InnerText;
				BlockPresentation.Padding = presentation.SelectSingleNode("padding").InnerText;
				BlockPresentation.Margin = presentation.SelectSingleNode("margin").InnerText;
				BlockPresentation.Border = presentation.SelectSingleNode("border").InnerText;

				BlockInnerHTML = block.SelectSingleNode("innerhtml").InnerXml;
				BlockInnerHTML = Regex.Replace(BlockInnerHTML, "<!\\[CDATA\\[ ?(.*?)\\ ?]\\]>", "$1", RegexOptions.Singleline);

				if (string.IsNullOrEmpty(ObjectReference)) ObjectReference = block.SelectSingleNode("object/reference").InnerText;
				if (string.IsNullOrEmpty(ObjectType)) ObjectType = block.SelectSingleNode("object/type").InnerText;
			}
		}
		private void readBlockFromHTML(XmlNode block)
		{
			if (block != null)
			{
				XmlNode moduleHTML = block.SelectSingleNode("div[@class='module']");
				Presentation.Parse(moduleHTML, ref ObjectPresentation);

				Presentation.Parse(block, ref BlockPresentation);

				BlockInnerHTML = block.SelectSingleNode("span[@class='content']").InnerXml;

				ObjectReference = moduleHTML.SelectSingleNode("@_reference").InnerText;
				ObjectType = moduleHTML.SelectSingleNode("@_type").InnerText;
			}
		}

		public static string getModuleName(string type, bool isEditForm)
		{
			if (!type.Contains("~/GUI/")) type = "~/GUI/" + type;
			type = HttpContext.Current.Server.MapPath(type);
			if (type.EndsWith("\\")) type.Remove(type.Length - 1);

			string moduleName = string.Empty;
			if (File.Exists(type + "\\ModuleSettings.config"))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(type + "\\ModuleSettings.config");
				moduleName = isEditForm ? doc.SelectSingleNode("module/editCtrl").InnerText : doc.SelectSingleNode("module/ctrl").InnerText;
			}
			else
			{
				DirectoryInfo directory = new DirectoryInfo(type);
				FileInfo[] file = directory.GetFiles("*.ascx");
				if (file.Length > 0)
					moduleName = file[0].Name;
			}

			return moduleName;

		}

		public Block(XmlNode block, bool isHTML)
		{
			BlockPresentation = new Presentation();
			ObjectPresentation = new Presentation();

			if (isHTML)
				readBlockFromHTML(block);
			else
				readBlockFromXML(block);

			if (!string.IsNullOrEmpty(ObjectType))
			{
				ObjectVirtualPath = "~/GUI/" + ObjectType + "/" + getModuleName(ObjectType, false);
				ObjectEditFormVirtualPath = "~/GUI/" + ObjectType + "/" + getModuleName(ObjectType, true);
			}
		}

		/// <summary>
		/// NewsFileSetting must be exist
		/// </summary>
		public void ReadBlockProperties()
		{
			if (File.Exists(NewsFileSetting))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(NewsFileSetting);
				XmlNode block = doc.SelectSingleNode("news/block/object[reference='" + ObjectReference + "']/..");

				if (block != null)
				{
					XmlNode presentation = block.SelectSingleNode("object/presentation");
					ObjectPresentation.Height = presentation.SelectSingleNode("height").InnerText;
					ObjectPresentation.Width = presentation.SelectSingleNode("width").InnerText;
					ObjectPresentation.Float = presentation.SelectSingleNode("float").InnerText;
					ObjectPresentation.Padding = presentation.SelectSingleNode("padding").InnerText;
					ObjectPresentation.Margin = presentation.SelectSingleNode("margin").InnerText;
					ObjectPresentation.Border = presentation.SelectSingleNode("border").InnerText;

					presentation = block.SelectSingleNode("presentation");
					BlockPresentation.Height = presentation.SelectSingleNode("height").InnerText;
					BlockPresentation.Width = presentation.SelectSingleNode("width").InnerText;
					BlockPresentation.Width = presentation.SelectSingleNode("float").InnerText;
					BlockPresentation.Padding = presentation.SelectSingleNode("padding").InnerText;
					BlockPresentation.Padding = presentation.SelectSingleNode("margin").InnerText;
					BlockPresentation.Border = presentation.SelectSingleNode("border").InnerText;

					BlockInnerHTML = block.SelectSingleNode("innerhtml").InnerXml;
					BlockInnerHTML = Regex.Replace(BlockInnerHTML, "<!\\[CDATA\\[ ?(.*?)\\ ?]\\]>", "$1");
				}
			}
		}
		public void SaveBlockProperties()
		{
			XmlDocument doc = (XmlDocument)HttpContext.Current.Session["newsFileSetting"];
			XmlNode block = doc.SelectSingleNode("news/block[position()=" + BlockPosition + "]");
			if (block == null)
			{
				XmlNode news, blockpresentation, objectpresentation, blockinnerhtml, objectnode, objecttype, objectReference;
				XmlNode oh, ow, of, op, om, ob, bh, bw, bf, bp, bm, bb;

				block = doc.CreateElement("block");
				blockpresentation = doc.CreateElement("presentation");
				objectpresentation = doc.CreateElement("presentation");

				blockinnerhtml = doc.CreateElement("innerhtml");
				objectnode = doc.CreateElement("object");
				objecttype = doc.CreateElement("type");
				objectReference = doc.CreateElement("reference");
				objectReference.InnerText = ObjectReference;
				objecttype.InnerText = ObjectType;
				oh = doc.CreateElement("height");
				ow = doc.CreateElement("width");
				of = doc.CreateElement("float");
				op = doc.CreateElement("padding");
				om = doc.CreateElement("margin");
				ob = doc.CreateElement("border");
				bh = doc.CreateElement("height");
				bw = doc.CreateElement("width");
				bf = doc.CreateElement("float");
				bp = doc.CreateElement("padding");
				bm = doc.CreateElement("margin");
				bb = doc.CreateElement("border");

				objectnode.AppendChild(objectReference);
				objectnode.AppendChild(objecttype);
				objectnode.AppendChild(objectpresentation);
				objectpresentation.AppendChild(ob);
				objectpresentation.AppendChild(oh);
				objectpresentation.AppendChild(ow);
				objectpresentation.AppendChild(op);
				objectpresentation.AppendChild(om);
				objectpresentation.AppendChild(of);

				block.AppendChild(objectnode);
				block.AppendChild(blockinnerhtml);
				block.AppendChild(blockpresentation);
				blockpresentation.AppendChild(bb);
				blockpresentation.AppendChild(bm);
				blockpresentation.AppendChild(bp);
				blockpresentation.AppendChild(bf);
				blockpresentation.AppendChild(bh);
				blockpresentation.AppendChild(bw);

				news = doc.SelectSingleNode("news");
				news.AppendChild(block);
			}
			XmlNode presentation = block.SelectSingleNode("object/presentation");
			presentation.SelectSingleNode("height").InnerText = ObjectPresentation.Height;
			presentation.SelectSingleNode("width").InnerText = ObjectPresentation.Width;
			presentation.SelectSingleNode("float").InnerText = ObjectPresentation.Float;
			presentation.SelectSingleNode("padding").InnerText = ObjectPresentation.Padding;
			presentation.SelectSingleNode("margin").InnerText = ObjectPresentation.Margin;
			presentation.SelectSingleNode("border").InnerText = ObjectPresentation.Border;

			presentation = block.SelectSingleNode("presentation");
			presentation.SelectSingleNode("height").InnerText = BlockPresentation.Height;
			presentation.SelectSingleNode("width").InnerText = BlockPresentation.Width;
			presentation.SelectSingleNode("float").InnerText = BlockPresentation.Float;
			presentation.SelectSingleNode("padding").InnerText = BlockPresentation.Padding;
			presentation.SelectSingleNode("margin").InnerText = BlockPresentation.Margin;
			presentation.SelectSingleNode("border").InnerText = BlockPresentation.Border;

			block.SelectSingleNode("object/type").InnerText = ObjectType;
			block.SelectSingleNode("object/reference").InnerText = ObjectReference;

			block.SelectSingleNode("innerhtml").InnerXml = string.Format("<![CDATA[{0}]]>", BlockInnerHTML);

			HttpContext.Current.Session["newsFileSetting"] = doc;
		}

	}
	public class Presentation
	{
		public string Height = string.Empty;
		public string Width = string.Empty;
		public string Margin = string.Empty;
		public string Padding = string.Empty;
		public string Border = string.Empty;
		public string Float = string.Empty;
		public string Extension = string.Empty;

		public static void Parse(XmlNode htmlNode, ref Presentation obj)
		{
			if (htmlNode != null && htmlNode.SelectSingleNode("@style") != null)
			{
				string style = htmlNode.SelectSingleNode("@style").InnerText;
				if (!style.EndsWith(";")) style += ";";

				obj.Height = Regex.Match(style, "height:(.*?);").Groups[1].Value.Trim();
				obj.Float = Regex.Match(style, "float:(.*?);").Groups[1].Value.Trim();
				obj.Width = Regex.Match(style, "width:(.*?);").Groups[1].Value.Trim();
				obj.Margin = Regex.Match(style, "margin:(.*?);").Groups[1].Value.Trim();
				obj.Padding = Regex.Match(style, "padding:(.*?);").Groups[1].Value.Trim();
				obj.Border = Regex.Match(style, "border:(.*?);").Groups[1].Value.Trim();
			}
		}
	}
}

//Sharpcms.net is licensed under the open source license GPL - GNU General Public License.

using System;
using System.Collections.Generic;
using System.Text;
using Portal.SiteSystem.Plugin;
using Portal.SiteSystem.Plugin.Types;
using System.Xml;

namespace Portal.SiteSystem.Library
{
    public class ProcessHandler
    {
        public PluginServices Plugins;
        
		public Process Run(System.Web.UI.Page httpPage)
        {
			Plugins = new PluginServices();
			Process process = new Process(httpPage, Plugins);
            Plugins.InvokeAll("system", "init");
            //? lay tu cache ra - neu k co cache thi lay o dau ra?
            XmlDocument xmlDocument = process.Cache["process"] as XmlDocument;
            XmlNode xmlNode = xmlDocument.DocumentElement;
            string[] args = process.CurrentProcess.Trim('/').Split('/');

            LoopThroughProcess(args, xmlNode, process);

            Plugins.InvokeAll("system", "exit");
            Plugins.ClosePlugins();

			return process;
        }

        private IPlugin GetProvider(string name)
        {
            AvailablePlugin plugin = Plugins.AvailablePlugins.Find(name);
            if (plugin != null)
            {
                return plugin.Instance;
            }
            else
            {
                return null;
            }
        }

        private void LoopThroughProcess(string[] args, XmlNode xmlNode, Process process)
        {
            if (args[0] != string.Empty)
            {
                args[0]= CommonXml.RenameIntegerPath(args[0]);
                xmlNode = xmlNode.SelectSingleNode(args[0]);
               
                if (xmlNode != null)
                {
                    if (process.CheckGroups(CommonXml.GetAttributeValue(xmlNode, "rights")))
                    {
                        XmlNodeList contentNodes = xmlNode.SelectNodes("*");
                        foreach (XmlNode contentNode in contentNodes)
                        {
                            // Not very pretty, I know, but it seems to be the
                            // best way to allow for proper debugging
                            if (process.HttpPage.Request.ServerVariables["REMOTE_ADDR"] == "127.0.0.1")
                            {
                                switch (contentNode.Name)
                                {
                                    case "load":
                                        HandlePlugin(contentNode, args, process);
                                        break;
                                    case "handle":
                                        HandlePlugin(contentNode, args, process);
                                        break;
                                    case "template":
                                        process.mainTemplate = process.Settings["templates/" + contentNode.Attributes["name"].Value];
                                        break;
                                    case "redirect":
                                        process.HttpPage.Response.Redirect(process.GetUrl(contentNode.Attributes["href"].Value));
                                        break;
                                }
                            }
                            else
                            {
                                try
                                {
                                    switch (contentNode.Name)
                                    {
                                        case "load":
                                            HandlePlugin(contentNode, args, process);
                                            break;
                                        case "handle":
                                            HandlePlugin(contentNode, args, process);
                                            break;
                                        case "template":
                                            process.mainTemplate = process.Settings["templates/" + contentNode.Attributes["name"].Value];
                                            break;
                                        case "redirect":
                                            process.HttpPage.Response.Redirect(process.GetUrl(contentNode.Attributes["href"].Value));
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    process.AddMessage(e);
                                }
                            }
                        }
                        args = Common.RemoveOne(args);

                        if (args != null)
                        {
                            LoopThroughProcess(args, xmlNode, process);
                        }
                    }
                    else
                    {
                        string redirectUrl = string.Format("login.aspx?redirect={0}", process.QueryOther["process"]);
                        process.HttpPage.Response.Redirect(redirectUrl); // todo: is this the way to do it
                    }
                }
            }
        }

        private void HandlePlugin(XmlNode contentNode, string[] args, Process process)
        {
            IPlugin provider = GetProvider(contentNode.Attributes["provider"].Value);
            if (provider != null)
            {
                switch (contentNode.Name)
                {
                    case "load":
                        ControlList control = process.Content.GetSubControl(CommonXml.GetAttributeValue(contentNode, "place"));
                        string action = CommonXml.GetAttributeValue(contentNode, "action");
                        string value = GetValue(contentNode, process);

                        string pathTrail = JoinPath(Common.RemoveOne(args));
                        if (provider is IPlugin2)
                        {
                            ((IPlugin2)provider).Load(control, action, value, pathTrail);
                        }
                        else
                        {
                            provider.Load(control, action, pathTrail);
                        }
                        break;

                    case "handle":
                        string mainEvent = process.QueryEvents["main"];
                        if (mainEvent != "")
                        {
                            provider.Handle(mainEvent);
                        }
                        break;
                }
            }
        }

        private string GetValue(XmlNode contentNode, Process process)
        {
            StringBuilder value = new StringBuilder(CommonXml.GetAttributeValue(contentNode, "value"));
            if (value.ToString() == string.Empty)
            {
                // No value is specified. Maybe a variable was requested?
                string variable = CommonXml.GetAttributeValue(contentNode, "variable");
                if (variable != string.Empty)
                {
                    value = new StringBuilder(process.Variables[variable]);
                }
            }
            else
            {
                // Replace variables
                ReplaceVariables(process.Variables, value);
            }
            return value.ToString();
        }

        private static void ReplaceVariables(Dictionary<string, string> variables, StringBuilder value)
        {
            while (true)
            {
                int startCurlyBrace = value.ToString().IndexOf("{");
                if (startCurlyBrace < 0)
                {
                    break;
                }

                int endCurlyBrace = value.ToString().IndexOf("}", startCurlyBrace);

                int stringLength = endCurlyBrace - startCurlyBrace - 1;
                string variable = value.ToString().Substring(startCurlyBrace + 1, stringLength);
                if (variables.ContainsKey(variable))
                {
                    value.Replace("{" + variable + "}", variables[variable]);
                }
                else
                {
                    value.Replace("{" + variable + "}", string.Empty);
                }
            }
        }

        private string JoinPath(string[] args)
        {
            if (args != null)
            {
                return string.Join("/", args);
            }
            return string.Empty;
        }
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace DFISYS.GUI.Users.Common
{
    public class Utils
    {
        /// <summary>
        /// Prevents errors when strings (or other types) are null and we just want the
        /// closest string approximation we can get.
        /// </summary>
        /// <param name="obj">the object to check</param>
        /// <returns>the object if it is a non-null string, or the ToString of the object if it is a non-null non-string, or string.empty if it is null</returns>
        public static string SafeString(object obj)
        {
            if (obj == null)
                return string.Empty;
            else
            {
                if (obj.GetType() == typeof(string))
                    return (string)obj;
                else
                    return obj.ToString();
            }
        }
        /// <summary>
        /// lấy ra nút ở footer của grid
        /// nếu có pagging thì đặt hasPagging = true
        /// </summary>
        /// <param name="dg">DataGrid</param>
        /// <param name="cell">vị trí cell của nút</param>
        /// <param name="hasPagging">có pagging không</param>
        /// <returns></returns>
        public static ImageButton GetFooterImageButton(DataGrid dg ,int cell, bool hasPagging)
        {
            if (hasPagging)
            {
                DataGridItem footer = (DataGridItem)dg.Controls[0].Controls[dg.Controls[0].Controls.Count - 2];
                //control[1] vì là trong cell đó có 1 controls là listeral
                return (ImageButton)footer.Cells[cell].Controls[1];
            }
            else
            {
                DataGridItem footer = (DataGridItem)dg.Controls[0].Controls[dg.Controls[0].Controls.Count - 1];
                return (ImageButton)footer.Cells[cell].Controls[1];
            }
        }
    }
    public class Helpers
    {
        /// <summary>
        /// Fire an event when press enter button in textbox
        /// Event to click button
        /// </summary>
        /// <param name="TextBoxToTie"></param>
        /// <param name="ButtonToTie"></param>
        public static void TieButton(Control TextBoxToTie, Control ButtonToTie)
        {
            string formName;
            try
            {
                int i = 0;
                Control c = ButtonToTie.Parent;
                // Step up the control hierarchy until either:
                // 1) We find an HtmlForm control
                // 2) We find a Page control - not what we want, but we should stop searching because we a Page will be higher than the HtmlForm.
                // 3) We complete 500 iterations. Obviously we are in a loop, and should stop.
                while (!(c is System.Web.UI.HtmlControls.HtmlForm) & !(c is System.Web.UI.Page) && i < 500)
                {
                    c = c.Parent;
                    i++;
                }
                // If we have found an HtmlForm, we use it's ClientID for the formName.
                // If not, we use the first form on the page ("forms[0]").
                if (c is System.Web.UI.HtmlControls.HtmlForm)
                    formName = c.ClientID;
                else
                    formName = "forms[0]";
            }
            catch
            {
                //If we catch an exception, we should use the first form on the page ("forms[0]").
                formName = "forms[0]";
            }
            // Tie the button.
            TieButton(TextBoxToTie, ButtonToTie, formName);
        }
        /// 
        ///     This ties a textbox to a button. 
        /// 
        /// 
        ///     This is the textbox to tie to. It doesn't have to be a TextBox control, but must be derived from either HtmlControl or WebControl,
        ///     and the html control should accept an 'onkeydown' attribute.
        /// 
        /// 
        ///     This is the button to tie to. All we need from this is it's ClientID. The Html tag it renders should support click()
        /// 
        /// 
        ///     This is the ClientID of the form that the button resides in.
        /// 
        public static void TieButton(Control TextBoxToTie, Control ButtonToTie, string formName)
        {
            // This is our javascript - we fire the client-side click event of the button if the enter key is pressed.
            string jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document." + formName + ".all['" + ButtonToTie.ClientID + "'].click();return false;} else return true; ";
            // We attach this to the onkeydown attribute - we have to cater for HtmlControl or WebControl.
            if (TextBoxToTie is System.Web.UI.HtmlControls.HtmlControl)
                ((System.Web.UI.HtmlControls.HtmlControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
            else if (TextBoxToTie is System.Web.UI.WebControls.WebControl)
                ((System.Web.UI.WebControls.WebControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
            else
            {
                // We throw an exception if TextBoxToTie is not of type HtmlControl or WebControl.
                throw new ArgumentException("Control TextBoxToTie should be derived from either System.Web.UI.HtmlControls.HtmlControl or System.Web.UI.WebControls.WebControl", "TextBoxToTie");
            }
        }

    }
}

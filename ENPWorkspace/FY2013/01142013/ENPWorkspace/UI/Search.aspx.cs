using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Emerson.WF.K2DataAccess;

public partial class Search : System.Web.UI.Page
{
    private string Username;
    public string DetailsURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Detect JavaScript
        //if (Session["JSChecked"] == null)
        //{
        //    Session["JSChecked"] = "Checked";

        //    if (Request.Url.Query.Trim() == string.Empty)
        //        Response.Write(@"<script language='javascript' type='text/jscript'>   window.location  = '" + Request.Url + "?js=1'; </script>");
        //    else
        //        Response.Write(@"<script language='javascript' type='text/jscript'>   window.location  = '" + Request.Url + "&js=1'; </script>");

        //}
        ////If the javascript Above executed JScript will have a value 
        //if (Request.QueryString["js"] != null)
        //{
        //    Session["JsWorks"] = "True";
        //}

        //if (Session["JsWorks"] == null) // Work under initial assumption that Javascript Doesnt Work // On first load
        //{
        //    Session["JsWorks"] = "False"; //Does Javascript Work
        //}

        //if (Session["JsWorks"].ToString().Equals("False")) // Work if JS Is Set to False
        //{
        //    Response.Write("Browser scripting is disabled.");
        //    Response.End();
        //}
        #endregion

        DetailsURL = string.Empty;

        if (Request.QueryString["pid"] != null) // automatically shows details page in the main pane
        {
            string strProcInstID = Request.QueryString["pid"].ToString();

            try
            {
                DetailsURL = "Search/RequestDetails.aspx?id=" + strProcInstID + "&ref=" + Request.QueryString["ref"].ToString();
                //"RequestDetails.aspx?id=" + gvSearchResults.DataKeys[gvSearchResults.SelectedIndex].Value.ToString() + "&ref=" + Server.UrlEncode(((LinkButton)gvSearchResults.Rows[gvSearchResults.SelectedIndex].Cells[0].Controls[1]).Text)
            }

            catch (Exception ex)
            {
                //if (ex.Message.Contains("K2SQLDataAccess.GetWorklistItemInfo : Worklist item '" + strWorklistSN + "' not found."))

                throw ex;
            }

        }

        else
        {
            DetailsURL = string.Empty;
        }

        Username = User.Identity.Name.ToString();
        ucTopPanel.User = Username;
    }

   
}

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
using Emerson.WF.ADUtilities;


public partial class UI_Search_SearchResults : System.Web.UI.Page
{
    private string SearchFilter;
    private string StatusFilter;
    private string ProcessFilter;
    private string ADGroups;

    private K2SQLDataAccess K2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);


	public DataTable ResultsDataSource
	{
		get { return ViewState["ProcInst"] as DataTable ?? null;}
		set { ViewState["ProcInst"] = value;}
	}
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ADConnector.LDAPDomain == null || ADConnector.LDAPPassword == null || ADConnector.LDAPUsername == null)
        {
            ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
            ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
            ADConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();
        }

        SearchFilter = Request.QueryString["search"];
        StatusFilter = Request.QueryString["status"];

        ProcessFilter = Request.QueryString["process"];

        gvSearchResults.DataSource = ResultsDataSource;

        if (!Page.IsPostBack)
        {
            Session["SearchResultUrl"] = "SearchResults.aspx?search=" + SearchFilter + "&status=" + StatusFilter + "&process=" + ProcessFilter;
            PopulateResult();
        }

        ibtnRefresh.Attributes.Add("onclick", "positionLoadingProgress();");
    }

    private void PopulateResult()
    {
        ADGroups = string.Empty;

        //GetADGroups(ref ADGroups);

        if (SearchFilter != string.Empty)
            ResultsDataSource = K2SQLDA.GetProcInstancesOfUser(User.Identity.Name, ref ADGroups, SearchFilter, StatusFilter.ToUpper(), ProcessFilter);
            //gvSearchResults.DataSource = K2SQLDA.GetProcInstancesOfUser(User.Identity.Name, ref ADGroups, SearchFilter, StatusFilter.ToUpper(), ProcessFilter);

        else
            ResultsDataSource = K2SQLDA.GetProcInstancesOfUser(User.Identity.Name, ref ADGroups, StatusFilter.ToUpper(), ProcessFilter);
            //gvSearchResults.DataSource = K2SQLDA.GetProcInstancesOfUser(User.Identity.Name, ref ADGroups, StatusFilter.ToUpper(), ProcessFilter);

        //SaveADGroups(ADGroups);

        // restore default page and selection settings
        gvSearchResults.PageIndex = 0;
        gvSearchResults.SelectedIndex = -1;

        gvSearchResults.DataSource = ResultsDataSource;
        gvSearchResults.DataBind();

        if (((DataTable)gvSearchResults.DataSource).Rows.Count > 0)
        {
            lblSearchMatchNo.Text = ((DataTable)gvSearchResults.DataSource).Rows.Count.ToString() + " record(s) found.";
        }
        else
        {
            lblSearchMatchNo.Text = "No records found.";
        }
    }

    private void GetADGroups(ref string ADGroups)
    {
        //if (Request.Cookies[User.Identity.Name + "ADGroups"] != null)
        //{
        //    ADGroups = Request.Cookies[User.Identity.Name + "ADGroups"].Value.ToString();
        //}
        //else  // if cookie is not found, set up ADConnection config settings
        //{
        //    ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
        //    ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
        //    ADConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();
        //}
    }

    private void SaveADGroups(string ADGroups)
    {
        // 4/11 saving and retrieval from cookie removed 

        //if (Request.Cookies[User.Identity.Name + "ADGroups"] == null)
        //{
        //    Response.Cookies[User.Identity.Name + "ADGroups"].Value = ADGroups;
        //    Response.Cookies[User.Identity.Name + "ADGroups"].Expires = DateTime.Today.AddDays(1);
        //}
    }


    protected void gvSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("RequestDetails.aspx?id=" + gvSearchResults.DataKeys[gvSearchResults.SelectedIndex].Value.ToString() + "&ref=" + Server.UrlEncode(((LinkButton)gvSearchResults.Rows[gvSearchResults.SelectedIndex].Cells[0].Controls[1]).Text));
    }

    protected void ibtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        ADGroups = string.Empty;

        GetADGroups(ref ADGroups);

        if (SearchFilter != string.Empty)
            ResultsDataSource = K2SQLDA.GetProcInstancesOfUser(User.Identity.Name, ref ADGroups, SearchFilter, StatusFilter.ToUpper(), ProcessFilter);

        else
            ResultsDataSource = K2SQLDA.GetProcInstancesOfUser(User.Identity.Name, ref ADGroups, StatusFilter.ToUpper(), ProcessFilter);

        gvSearchResults.DataSource = SortDataTable(ResultsDataSource, true);
        gvSearchResults.DataBind();
    }

    protected void gvSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvSearchResults.PageIndex = e.NewPageIndex;
        //gvSearchResults.DataSource = (DataTable)ViewState["ProcInst"];
        //gvSearchResults.DataBind();
        HandlePaging(sender, e);
    }

    protected void gvSearchResults_Sorting(object sender, GridViewSortEventArgs e)
    {
        HandleSorting(sender, e);
    }

    #region GridView Handler
    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }

    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }

        return GridViewSortDirection;
    }

    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);

            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }

    public void HandlePaging(object sender, GridViewPageEventArgs e)
    {
        gvSearchResults.DataSource = SortDataTable(gvSearchResults.DataSource as DataTable, true);
        gvSearchResults.PageIndex = e.NewPageIndex;
        gvSearchResults.DataBind();
    }

    public void HandleSorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;

        int pageIndex = gvSearchResults.PageIndex;

        AppendSortImageToHeader();

        gvSearchResults.DataSource = SortDataTable(gvSearchResults.DataSource as DataTable, false);
        gvSearchResults.DataBind();
        gvSearchResults.PageIndex = pageIndex;
    }

    public void AppendSortImageToHeader()
    {
        GridView grid = gvSearchResults;

        //looping variable 
        int i;
        // did we find the column header that's being sorted?
        int foundColumnIndex = -1;

        //const string SORT_ASC = "<span style='font-family: Webdings;'> 5</span>";
        //const string SORT_DESC = "<span style='font-family: Webdings;'> 6</span>";
        const string SORT_ASC = "<span style='font-family: Wingdings 3; font-size: 8px;'> q</span>";
        const string SORT_DESC = "<span style='font-family: Wingdings 3; font-size: 8px'> p</span>";



        // get which column we're sorting on

        for (i = 0; i <= grid.Columns.Count - 1; i++)
        {
            // remove the current sort
            grid.Columns[i].HeaderText = grid.Columns[i].HeaderText.Replace(SORT_ASC, string.Empty);
            grid.Columns[i].HeaderText = grid.Columns[i].HeaderText.Replace(SORT_DESC, string.Empty);

            // if the sort expression of this column matches the passed sort expression, 
            // keep the column number and mark that we've found a match for further processing
            if (GridViewSortExpression == grid.Columns[i].SortExpression)
            {
                //' store the column number, but we need to keep going through the loop
                //' to remove all the previous sorts
                foundColumnIndex = i;
            }

        }

        //' if we found the sort column, append the sort direction 
        if (foundColumnIndex > -1)
        {

            // append either ascending or descending string
            if (GridViewSortDirection == "ASC")
                grid.Columns[foundColumnIndex].HeaderText += SORT_ASC;
            else
                grid.Columns[foundColumnIndex].HeaderText += SORT_DESC;
        }

    }

    protected void gvSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            //DataRow[] drProsStatus = ResultsDataSource.Select("Folio = '" + ((LinkButton)e.Row.Cells[0].Controls[1]).Text + "'");
            //if (drProsStatus[0]["ProcStatus"].ToString().Trim().ToUpper().Equals("ERROR"))

            // if k2 error
            if (((LinkButton)e.Row.Cells[4].Controls[1]).Text.Trim().ToUpper().Equals("ERROR"))
            {
                //e.Row.CssClass = "GridErrorRowStyle";
                //((LinkButton)e.Row.Cells[4].Controls[1]).Text = Emerson.WF.Common.ToProperCase(Emerson.WF.Constants.Status.Error);

                // modify text color to ms-redtext
                //foreach (TableCell tc in e.Row.Cells)
                //    ((LinkButton)tc.Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000"); 

                //((LinkButton)e.Row.Cells[0].Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
                //((LinkButton)e.Row.Cells[1].Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
                //((LinkButton)e.Row.Cells[2].Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
                //((LinkButton)e.Row.Cells[3].Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
                //((LinkButton)e.Row.Cells[4].Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
                //((LinkButton)e.Row.Cells[5].Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
                //((LinkButton)e.Row.Cells[6].Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
                //((LinkButton)e.Row.Cells[7].Controls[1]).ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");

                // add error icon at 1st column, 1st control
                Image im = new Image();
                im.ImageUrl = "~/Images/error14_2.GIF";
                im.Height = 14;
                im.Width = 14;

                Image spacer = new Image();
                spacer.ImageUrl = "~/Images/spacer.gif";
                spacer.Height = 14;
                spacer.Width = 4;
                
                e.Row.Cells[4].Controls.AddAt(1, e.Row.Cells[4].Controls[1]);
                e.Row.Cells[4].Controls.AddAt(2, spacer);
                e.Row.Cells[4].Controls.AddAt(3, im);
                //e.Row.Cells[0].Controls.Add(im);

            }

            ((LinkButton)e.Row.Cells[0].Controls[1]).ToolTip = gvSearchResults.DataKeys[e.Row.RowIndex].Value.ToString();
           
            e.Row.Attributes.Add("onmouseover", "this.className='GridHighlightRowStyle'");

            if (Math.IEEERemainder(e.Row.RowIndex, 2) == 0)
                e.Row.Attributes.Add("onmouseout", "this.className='GridRowStyle'");
            else
                e.Row.Attributes.Add("onmouseout", "this.className='GridAlternatingRowStyle'");

            if (((LinkButton)e.Row.Cells[6].Controls[1]).Text.Trim() == string.Empty)
            {
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                LinkButton lbtnTemp = (LinkButton)e.Row.Cells[6].Controls[1];
                lbtnTemp.Text = "---";
            }
        }
    }

    #endregion

  
}

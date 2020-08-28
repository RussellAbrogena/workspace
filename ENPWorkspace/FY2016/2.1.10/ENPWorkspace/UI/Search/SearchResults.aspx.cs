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
using Microsoft.ApplicationBlocks.Data;

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

        if (SearchFilter != string.Empty)
            //ResultsDataSource = K2SQLDA.GetProcInstancesOfUser("EMRSN\\allan.sim", ref ADGroups, SearchFilter, StatusFilter.ToUpper(), ProcessFilter);
            ResultsDataSource = K2SQLDA.GetProcInstancesOfUser(User.Identity.Name, ref ADGroups, SearchFilter, StatusFilter.ToUpper(), ProcessFilter);
        else
            //ResultsDataSource = K2SQLDA.GetProcInstancesOfUser("EMRSN\\allan.sim", ref ADGroups, StatusFilter.ToUpper(), ProcessFilter);
            ResultsDataSource = K2SQLDA.GetProcInstancesOfUser(User.Identity.Name, ref ADGroups, StatusFilter.ToUpper(), ProcessFilter);
    
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
       
    }

    private void SaveADGroups(string ADGroups)
    {
      
    }

    protected void gvSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsOKToViewWorklist(gvSearchResults.DataKeys[gvSearchResults.SelectedIndex].Value.ToString()))
        {
            if (ViewState["OKToViewWorklistData"].ToString() != null && ViewState["OKToViewWorklistData"].ToString() != string.Empty)
            {
                Response.Redirect(ViewState["OKToViewWorklistData"].ToString());
            }
            else
                Response.Redirect("RequestDetails.aspx?id=" + gvSearchResults.DataKeys[gvSearchResults.SelectedIndex].Value.ToString() + "&ref=" + Server.UrlEncode(((LinkButton)gvSearchResults.Rows[gvSearchResults.SelectedIndex].Cells[0].Controls[1]).Text));
        }
        else
            Response.Redirect("RequestDetails.aspx?id=" + gvSearchResults.DataKeys[gvSearchResults.SelectedIndex].Value.ToString() + "&ref=" + Server.UrlEncode(((LinkButton)gvSearchResults.Rows[gvSearchResults.SelectedIndex].Cells[0].Controls[1]).Text));
    }

    private bool IsOKToViewWorklist(string sProcInstID)
    {
        if (ViewState["OKToViewWorklist"] == null)
        {
            ViewState["OKToViewWorklistData"] = string.Empty;

            string ADGroups = string.Empty;
            //string retVal = CheckWorkListItem(sProcInstID, "EMRSN\\allan.sim");
            string retVal = CheckWorkListItem(sProcInstID, User.Identity.Name.ToString());

            if (retVal != string.Empty && retVal != null)
            {
                ViewState["OKToViewWorklist"] = true;
                ViewState["OKToViewWorklistData"] = retVal;
            }
            else
                ViewState["OKToViewWorklist"] = false;
        }
        return (bool)ViewState["OKToViewWorklist"];
    }

    public static string CheckWorkListItem(string sProcInstID, string sUsernameFQN)
    {
        return Convert.ToString(SqlHelper.ExecuteScalar(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"], "sp_GetWorklistItemOnSearch", sUsernameFQN, Convert.ToInt32(sProcInstID)));
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
     
        int i;
        int foundColumnIndex = -1;

        const string SORT_ASC = "<span style='font-family: Wingdings 3; font-size: 8px;'> q</span>";
        const string SORT_DESC = "<span style='font-family: Wingdings 3; font-size: 8px'> p</span>";

        for (i = 0; i <= grid.Columns.Count - 1; i++)
        {        
            grid.Columns[i].HeaderText = grid.Columns[i].HeaderText.Replace(SORT_ASC, string.Empty);
            grid.Columns[i].HeaderText = grid.Columns[i].HeaderText.Replace(SORT_DESC, string.Empty);

            if (GridViewSortExpression == grid.Columns[i].SortExpression)                      
                foundColumnIndex = i;    
        }
       
        if (foundColumnIndex > -1)
        {
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
            if (((LinkButton)e.Row.Cells[4].Controls[1]).Text.Trim().ToUpper().Equals("ERROR"))
            {            
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

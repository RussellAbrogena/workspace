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

public partial class UI_Worklist_WorklistItems : System.Web.UI.Page
{
    private string WorklistItemIDs;
    private K2SQLDataAccess k2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
    private K2ROMDataAccess k2ROMDA = new K2ROMDataAccess(ConfigurationManager.AppSettings["SourceCode.K2.PlanServer"]);

    public string ProcessFilter
    {
        get { return ViewState["ProcessFilter"] as string ?? null; }
        set { ViewState["ProcessFilter"] = value; }
    }

    public DataTable ItemsDataSource
    {
        get { return ViewState["ProcInst"] as DataTable ?? null; }
        set { ViewState["ProcInst"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ADConnector.LDAPDomain == null || ADConnector.LDAPPassword == null || ADConnector.LDAPUsername == null)
        {
            ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
            ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
            ADConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();
        }

        if (!Page.IsPostBack)
        {
            if (Session["WorklistProcessFilter"] != null)
                ProcessFilter = Session["WorklistProcessFilter"].ToString();
            else
                ProcessFilter = "ALL";

            SetVisibleColumns();

            //WorklistItemIDs = k2ROMDA.GetWorklistItemIDs();
            WorklistItemIDs = GetWorklistItemIDs();  //** replaced as provided by ANC
            ItemsDataSource = k2SQLDA.GetProcInstancesOfUser(WorklistItemIDs, ProcessFilter, User.Identity.Name.ToString());

        }

        PopulateResult();
        ibtnRefresh.Attributes.Add("onclick", "positionLoadingProgress();");

    }

    private string GetWorklistItemIDs()
    {
        string procinstIDs = "";
        string dboconn = ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"];
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(dboconn);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("sp_GetUserWorkListByRow", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@sUsername", SqlDbType.VarChar, 100).Value = User.Identity.Name.ToString();

        DataTable dt = new DataTable();
        con.Open();
        System.Data.SqlClient.SqlDataAdapter adptr = new System.Data.SqlClient.SqlDataAdapter(cmd);
        adptr.Fill(dt);
        con.Close();
        #region value assignment
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow drow in dt.Rows)
            {
                procinstIDs = drow[0].ToString();
            }
        }
        #endregion
        return procinstIDs;
    }


    private void PopulateResult()
    {      
        gvWorklistItems.SelectedIndex = -1;
        gvWorklistItems.DataSource = ItemsDataSource;
        gvWorklistItems.DataBind();

        if (((DataTable)ItemsDataSource).Rows.Count > 0)
        {
            lblSearchMatchNo.Text = ((DataTable)ItemsDataSource).Rows.Count.ToString() + " record(s) found.";
        }
        else
        {
            lblSearchMatchNo.Text = "No records found.";
        }
    }

    private void SetVisibleColumns()
    {
        gvWorklistItems.Columns[5].Visible = ((UserPassport)Session["UserPassport"]).HasK2AdminRights;
    }

    protected void gvWorklistItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {     
        HandlePaging(sender, e);
    }

    protected void gvWorklistItems_Sorting(object sender, GridViewSortEventArgs e)
    {
        HandleSorting(sender, e);
    }

    protected void gvWorklistItems_SelectedIndexChanged(object sender, EventArgs e)
    {      
        Response.Redirect(gvWorklistItems.DataKeys[gvWorklistItems.SelectedIndex].Value.ToString());
    }

    protected void ibtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        //WorklistItemIDs = k2ROMDA.GetWorklistItemIDs();
        WorklistItemIDs = GetWorklistItemIDs();  //** replaced as provided by ANC

        ItemsDataSource = k2SQLDA.GetProcInstancesOfUser(WorklistItemIDs, ProcessFilter, User.Identity.Name.ToString());

        gvWorklistItems.DataSource = SortDataTable(ItemsDataSource, true);
        gvWorklistItems.DataBind();
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
        ItemsDataSource = SortDataTable(ItemsDataSource, true).ToTable();
        gvWorklistItems.DataSource = ItemsDataSource;    

        gvWorklistItems.PageIndex = e.NewPageIndex;
        gvWorklistItems.DataBind();
    }

    public void HandleSorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gvWorklistItems.PageIndex;

        AppendSortImageToHeader();
        ItemsDataSource = SortDataTable(ItemsDataSource, false).ToTable();
        gvWorklistItems.DataSource = ItemsDataSource;

        gvWorklistItems.DataBind();
        gvWorklistItems.PageIndex = pageIndex;
    }

    public void AppendSortImageToHeader()
    {
        GridView grid = gvWorklistItems;      
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

    protected void gvWorklistItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for GridView
/// </summary>
public class GridViewHandler : System.Web.UI.UserControl
{
    public GridViewHandler(GridView gv)
    {
        _GridViewTemp = gv;
    }

    private GridView _GridViewTemp;

    public GridView GridViewTemp
    {
        get { return _GridViewTemp; }
        set { _GridViewTemp = value; }
    }

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
        GridViewTemp.DataSource = SortDataTable(GridViewTemp.DataSource as DataTable, true);
        GridViewTemp.PageIndex = e.NewPageIndex;
        GridViewTemp.DataBind();
    }

    public void HandleSorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;

        int pageIndex = GridViewTemp.PageIndex;

        GridViewTemp.DataSource = SortDataTable(GridViewTemp.DataSource as DataTable, false);

        AppendSortImageToHeader();

        GridViewTemp.DataBind();
        GridViewTemp.PageIndex = pageIndex;

        
    }

    public void AppendSortImageToHeader()
    {
        GridView grid = GridViewTemp; 

        //looping variable 
        int i;
        // did we find the column header that's being sorted?
        int foundColumnIndex = -1;

        const string SORT_ASC = "<span style='font-family: Webdings;'> 5</span>";
        const string SORT_DESC = "<span style='font-family: Webdings;'> 6</span>";

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

}

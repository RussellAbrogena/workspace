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

using System.Collections.Generic;
using System.Text;

using Emerson.WF.K2DataAccess;
using Emerson.WF.ADUtilities;

using Emerson.WF.MasterConfig;

public partial class UI_Search_SearchOptions : System.Web.UI.Page
{
    #region ENPUser ViewState
    public ENPUser LoggedUser
    {
        get
        {
            if (ViewState["_LoggedUser"] == null)
                ViewState["_LoggedUser"] = new ENPUser();

            return (ENPUser)ViewState["_LoggedUser"];
        }
        set { ViewState["_LoggedUser"] = value; }
    }
    #endregion

    private K2SQLDataAccess K2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            try
            {
                LoggedUser.Get(User.Identity.Name.ToString(), ENPUser.GetBy.LoginName);
                PopulateSaveNames();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Contains("User does not exist in User List table.".ToUpper()))
                {
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    cboSavedSearch.Enabled = false;
                }
                else
                {
                    throw ex;
                }
            }

            PopulateProcessFilter();
        }

        btnSave.Attributes.Add("onclick", "return SaveSearch();");
        btnDelete.Attributes.Add("onclick", "return DeleteSavedSearch();");
    }

    private void PopulateProcessFilter()
    {
        string strADGroups = string.Empty;

        GetADGroups(ref strADGroups);
        DataTable pf = new DataTable();
        pf.Columns.Add("ProcSetName");
        pf.Columns.Add("ProcSetID");

        foreach (DataRow dr1 in K2SQLDA.GetProcSetWithPermission(User.Identity.Name, ref strADGroups).Rows)
        {
            bool bExists = false;

            foreach (DataRow dr2 in pf.Rows)
            {
                if (dr2["ProcSetName"].ToString() == dr1["ProcSetName"].ToString())
                {
                    dr2["ProcSetID"] += "," + dr1["ProcSetID"].ToString();
                    bExists = true;
                }
            }

            if (!bExists)
            {
                if (!IsProcessExempted(Convert.ToInt32(dr1["ProcSetID"])))
                {
                    DataRow newdr = pf.NewRow();
                    newdr["ProcSetID"] = dr1["ProcSetID"];
                    newdr["ProcSetName"] = dr1["ProcSetName"];
                    pf.Rows.Add(newdr);
                }
            }
           
        }

        chklProcess.DataSource = pf;  
        chklProcess.DataTextField = "ProcSetName";
        chklProcess.DataValueField = "ProcSetID";
        chklProcess.DataBind();
    
        foreach (ListItem li in chklProcess.Items)
        {           
            li.Selected = false;       
        }   

        SaveADGroups(strADGroups);
    }

    private bool IsProcessExempted(int ProcID)
    {
        string sProcExempted = ConfigurationManager.AppSettings["ProcExemption"].ToString();
        string[] arrProcID = sProcExempted.Split(',');

        bool IsFound = false;

        for (int x = 0; x <= arrProcID.GetUpperBound(0); x++)
        {
            if (Convert.ToInt32(arrProcID[x]) == ProcID)
            {
                IsFound = true;
                break;
            }
        }

        return IsFound;
    }

    private void GetADGroups(ref string ADGroups)
    {
        if (Request.Cookies[User.Identity.Name + "ADGroups"] != null)
        {
            ADGroups = Request.Cookies[User.Identity.Name + "ADGroups"].Value.ToString();
        }
        else  
        {           
            ADGroups = ((UserPassport)Session["UserPassport"]).ADGroupsCommaDel;
        }
    }

    private void SaveADGroups(string ADGroups)
    {
   
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        hdnIsPostBack.Value = "true";
        hdnProcessFilter.Value = GetProcessFilter();
    }

    private string GetProcessFilter()
    {
        string strProcess = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chklProcess.Items)
        {

            if (!li.Selected)
                bolAllSelected = false;
            else
                strProcess += ((strProcess != string.Empty) ? "," : "") + String.Format((li.Value.ToString().Length == 1) ? "0{0}" : "{0}", li.Value.ToString());

        }    
        return strProcess;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        hdnIsPostBack.Value = "false"; 
        SavedSearch oSavedSearch  = new SavedSearch();

        oSavedSearch.SaveName = hdnSaveAs.Value.ToString();
        oSavedSearch.Status = cboStatusFilter.SelectedValue.ToString();
        oSavedSearch.ProcessFilter = GetProcessFilter();
        oSavedSearch.SearchFor = txtSearch.Text.ToString();

        List<SavedSearch> lstSavedSearch = null;

        bool Exists = false;

        if (LoggedUser.SavedSearches != string.Empty)
        {
            lstSavedSearch = oSavedSearch.DeserializeToList<SavedSearch>(LoggedUser.SavedSearches);

            foreach (SavedSearch ss in lstSavedSearch)
            {
                if (ss.SaveName == hdnSaveAs.Value)
                {
                    ss.Status = oSavedSearch.Status;
                    ss.ProcessFilter = oSavedSearch.ProcessFilter;
                    ss.SearchFor = oSavedSearch.SearchFor;
                    Exists = true;
                }
            }

            if (!Exists)
                lstSavedSearch.Add(oSavedSearch);
        }
        else
        {
            lstSavedSearch = new List<SavedSearch>();
            lstSavedSearch.Add(oSavedSearch);
        }

        LoggedUser.SavedSearches = oSavedSearch.SerializeFromList(lstSavedSearch);
        LoggedUser.Update();

        PopulateSaveNames();
        cboSavedSearch.SelectedValue = hdnSaveAs.Value;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + Emerson.WF.Konstants.Message.SaveSuccessful +  "');</script>");
    }
    
    private void PopulateSaveNames()
    {      
        if (LoggedUser.SavedSearches != string.Empty)
        {
            SavedSearch deSavedSearch = new SavedSearch();

            List<SavedSearch> lstSavedSearch = deSavedSearch.DeserializeToList<SavedSearch>(LoggedUser.SavedSearches);

            lstSavedSearch.Sort();

            cboSavedSearch.DataSource = lstSavedSearch;
            cboSavedSearch.DataTextField = "SaveName";
            cboSavedSearch.DataValueField = "SaveName";
            cboSavedSearch.DataBind();
        }
        else 
        {
            SavedSearch oSavedSearch = new SavedSearch();
            oSavedSearch.SaveName = "Default";
            oSavedSearch.Status = "ALL";
            oSavedSearch.ProcessFilter = "ALL";
            oSavedSearch.SearchFor = string.Empty;

            List<SavedSearch> lstSavedSearch = new List<SavedSearch>();
            lstSavedSearch.Add(oSavedSearch);
            LoggedUser.SavedSearches = oSavedSearch.SerializeFromList(lstSavedSearch);
            LoggedUser.Update();

            PopulateSaveNames();
        }
    }

    private void LoadProcessFilter(string ProcessFilter)
    {
        string[] ProcessFilters = ProcessFilter.Split(',');
        
            foreach (ListItem li in chklProcess.Items)
            {
                li.Selected = false;                
                if (ProcessFilter.ToUpper().Trim() == "ALL")
                    li.Selected = true;
                else
                {
                    for (int i = 0; i < ProcessFilters.Length; i++)
                    {                       
                        string[] arrValue = li.Value.ToString().Split(',');
                        foreach (string pfvalue in arrValue)
                        {
                            if (ProcessFilters[i] == String.Format(((pfvalue.Length == 1) ? "0{0}" : "{0}"), pfvalue))
                            {
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
    }

    protected void cboSavedSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSavedSearch();
    }

    private void LoadSavedSearch()
    {
        if (LoggedUser.SavedSearches != string.Empty && cboSavedSearch.SelectedValue != string.Empty)
        {
            SavedSearch deSavedSearch = new SavedSearch();

            List<SavedSearch> lstSavedSearch = deSavedSearch.DeserializeToList<SavedSearch>(LoggedUser.SavedSearches);

            foreach (SavedSearch ss in lstSavedSearch)
            {
                if (ss.SaveName == cboSavedSearch.SelectedValue)
                {
                    txtSearch.Text = ss.SearchFor;
                    cboStatusFilter.SelectedValue = ss.Status.ToUpper();
                    hdnSaveAs.Value = ss.SaveName;
                    LoadProcessFilter(ss.ProcessFilter);
                    break;
                }
            }
        }
        hdnIsPostBack.Value = "false"; 
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (LoggedUser.SavedSearches != string.Empty && cboSavedSearch.SelectedValue != string.Empty && cboSavedSearch.SelectedValue.ToUpper().Trim() != "DEFAULT")
        {
            SavedSearch deSavedSearch = new SavedSearch();

            List<SavedSearch> lstSavedSearch = deSavedSearch.DeserializeToList<SavedSearch>(LoggedUser.SavedSearches);

            foreach (SavedSearch ss in lstSavedSearch)
            {
                if (ss.SaveName == cboSavedSearch.SelectedValue)
                {
                    lstSavedSearch.Remove(ss);
                    LoggedUser.SavedSearches = deSavedSearch.SerializeFromList(lstSavedSearch);

                    break;
                }
            }

            if (lstSavedSearch.Count == 0)
                LoggedUser.SavedSearches = string.Empty;

            LoggedUser.Update(); 
            PopulateSaveNames();
            LoadSavedSearch();
            hdnIsPostBack.Value = "false";      
        }
    }

  
}

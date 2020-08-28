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

        // 6/8/07 added abrs BEGIN -- to address the issue with 2 process versions deployed simultaneously

        //chklProcess.DataSource = K2SQLDA.GetProcSetWithPermission(User.Identity.Name, ref strADGroups);

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
        // 6/8/07 added abrs BEGIN
        
        chklProcess.DataTextField = "ProcSetName";
        chklProcess.DataValueField = "ProcSetID";
        chklProcess.DataBind();

        // select all by default	
        foreach (ListItem li in chklProcess.Items)
        {
            //li.Selected = true;
            li.Selected = false;
            //if (li.Text.Contains("Discount"))
            //    li.Text = "Price Discount (Channels)";
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
        else  // if cookie is not found, set up ADConnection config settings
        {/*
            StringBuilder sb = new StringBuilder();
            string Username = Page.User.Identity.Name.ToString();

            foreach (string strGroup in ((UserPassport)Session["UserPassport"]).ADGroups)
            {
                sb.Append(String.Format(@",{0}{1}", Username.Substring(0, Username.IndexOf(@"\") + 1).Trim(), strGroup.Trim())); 

                ADGroups = sb.ToString().Substring(1);
            }
            */
           
            ADGroups = ((UserPassport)Session["UserPassport"]).ADGroupsCommaDel;

            /*
            ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
            ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
            AConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();
             * */
        }
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

        // *** removed by rcl
        //if (bolAllSelected)
        //    strProcess = "ALL";

        return strProcess;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        hdnIsPostBack.Value = "false"; // do not refresh search results frame

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
        else // load default search parameters
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
                        // 6/8/07 added abrs BEGIN -- to address the issue with 2 process versions deployed simultaneously

                        //if (ProcessFilters[i] == String.Format((li.Value.ToString().Length == 1) ? "0{0}" : "{0}", li.Value.ToString()))
                        //{
                        //    li.Selected = true;
                        //    break;
                        //}
                        
                        // if single id save in the search template, and list item value has multiple values
                        string[] arrValue = li.Value.ToString().Split(',');
                        foreach (string pfvalue in arrValue)
                        {
                            if (ProcessFilters[i] == String.Format(((pfvalue.Length == 1) ? "0{0}" : "{0}"), pfvalue))
                            {
                                li.Selected = true;
                                break;
                            }
                        }

                        // if multiple id is saved in the search template, and list value has single value (happens when user saved a template while more than 1 version were deployed, then after, other versions were dropped) (CRAP!!!)

                        // 6/8/07 added abrs END -- to address the issue with 2 process versions deployed simultaneously
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

        hdnIsPostBack.Value = "false"; // load parameters but do not refresh results grid

        //hdnProcessFilter.Value = GetProcessFilter();
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

            LoggedUser.Update(); // update database

            PopulateSaveNames();
            LoadSavedSearch();

            hdnIsPostBack.Value = "false"; // do not refresh search results           
        }
    }

  
}

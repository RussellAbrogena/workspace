using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Text;

using Emerson.WF.ADUtilities;
using Emerson.WF;

using Emerson.WF.K2DataAccess;
using Emerson.WF.MasterConfig;

/// <summary>
/// Summary description for UserPassport
/// </summary>
public class UserPassport
{
    public UserPassport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public UserPassport(string strUsername)
    {
        Username = strUsername;

        //GetUserInfo();
    }

    #region Username
    private string _Username;
    public string Username
    {
        get
        {
            return _Username;
        }
        set
        {
            _Username = value;
        }

    }
    #endregion

    #region DisplayName
    private string _DisplayName;
    public string DisplayName
    {
        get
        {
            return _DisplayName;
        }
        set
        {
            _DisplayName = value;
        }

    }
    #endregion

    #region HasCAPEXReportsRights
    private bool _HasCAPEXReportsRights;
    public bool HasCAPEXReportsRights
    {
        get 
        {
            return _HasCAPEXReportsRights;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasCAPEXReportsRights = value;
        }
        
    }
    #endregion

    #region HasDocSubReportsRights
    private bool _HasDocSubReportsRights;
    public bool HasDocSubReportsRights
    {
        get
        {
            return _HasDocSubReportsRights;
        }

        set
        {
            _HasDocSubReportsRights = value;
        }

    }
    #endregion

    #region HasTravelReportsRights
    private bool _HasTravelReportsRights;
    public bool HasTravelReportsRights
    {
        get
        {
            return _HasTravelReportsRights;
        }

        set
        {
            _HasTravelReportsRights = value;
        }

    }
    #endregion

    #region HasIPRReportsRights
    private bool _HasIPRReportsRights;
    public bool HasIPRReportsRights
    {
        get
        {
            return _HasIPRReportsRights;
        }

        set
        {
            _HasIPRReportsRights = value;
        }

    }
    #endregion

    #region HasCAPEXAllReportsRights
    private bool _HasCAPEXAllReportsRights;
    public bool HasCAPEXAllReportsRights
    {
        get
        {
            return _HasCAPEXAllReportsRights;
        }

        set
        {
            _HasCAPEXAllReportsRights = value;
        }

    }
    #endregion

    #region HasK2AdminRights
    private bool _HasK2AdminRights;
    public bool HasK2AdminRights
    {
        get
        {
            return _HasK2AdminRights;
        }
        set
        {
            _HasK2AdminRights = value;
        }

    }
    #endregion

    #region HasCSRReportsRights
    private bool _HasCSRReportsRights;
    public bool HasCSRReportsRights
    {
        get
        {
            return _HasCSRReportsRights;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasCSRReportsRights = value;
        }

    }
     #endregion

    #region HasSMRReportsRights
    private bool _HasSMRReportsRights;
    public bool HasSMRReportsRights
    {
        get
        {
            return _HasSMRReportsRights;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasSMRReportsRights = value;
        }

    }
    #endregion

    #region HasOARReportsRights
    private bool _HasOARReportsRights;
    public bool HasOARReportsRights
    {
        get
        {
            return _HasOARReportsRights;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasOARReportsRights = value;
        }

    }
    #endregion


    #region HasCRPReportsRights
    private bool _HasCRPReportsRights;
    public bool HasCRPReportsRights
    {
        get
        {
            return _HasCRPReportsRights;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasCRPReportsRights = value;
        }

    }
    #endregion

    #region HasCRPReportsRightsByContractType
    private bool _HasCRPReportsRightsByContractType;
    public bool HasCRPReportsRightsByContractType
    {
        get
        {
            return _HasCRPReportsRightsByContractType;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasCRPReportsRightsByContractType = value;
        }

    }
    #endregion

    #region HasCRPReportsRightsByContractTypeWithMU
    private bool _HasCRPReportsRightsByContractTypeWithMU;
    public bool HasCRPReportsRightsByContractTypeWithMU
    {
        get
        {
            return _HasCRPReportsRightsByContractTypeWithMU;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasCRPReportsRightsByContractTypeWithMU = value;
        }

    }
    #endregion

    #region HasSCRReportsRights
    private bool _HasSCRReportsRights;
    public bool HasSCRReportsRights
    {
        get
        {
            return _HasSCRReportsRights;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasSCRReportsRights = value;
        }

    }
    #endregion

    #region HasPRReportsRights
    private bool _HasPRReportsRights;
    public bool HasPRReportsRights
    {
        get
        {
            return _HasPRReportsRights;
            //return (FinanceGroups<string>().Count > 0);
        }

        set
        {
            _HasPRReportsRights = value;
        }

    }
      #endregion

    #region HasTSRReportsRights
    private bool _HasTSRReportsRights;
    public bool HasTSRReportsRights
    {
        get
        {
            return _HasTSRReportsRights;
        }

        set
        {
            _HasTSRReportsRights = value;
        }

    }
    #endregion

    #region HasHRTReportRights
    private bool _HasHRTReportRights;
    public bool HasHRTReportRights
    {
        get
        {
            return _HasHRTReportRights;
        }

        set
        {
            _HasHRTReportRights = value;
        }

    }
    #endregion

    #region HasEEPReportRights
    private bool _HasEEPReportRights;
    public bool HasEEPReportRights
    {
        get
        {
            return _HasEEPReportRights;
        }

        set
        {
            _HasEEPReportRights = value;
        }

    }
    #endregion



    #region ADGroups
    private List<FinanceGroup> _FinanceGroups;
    public List<FinanceGroup> FinanceGroups
    {
        get
        {
            if (_FinanceGroups == null)
                _FinanceGroups = new List<FinanceGroup>();

            return _FinanceGroups;
        }
        set { _FinanceGroups = value; }
    }
    #endregion

    #region ADGroups
    private List<string> _ADGroups;
    public List<string> ADGroups
    {
        get
        {
            if (_ADGroups == null)
                _ADGroups = new List<string>();

            return _ADGroups;
        }
        set { _ADGroups = value; }
    }
    #endregion

    #region WFGroups
    private List<string> _WFGroups;
    public List<string> WFGroups
    {
        get
        {
            if (_WFGroups == null)
                _WFGroups = new List<string>();

            return _WFGroups;
        }
        set { _WFGroups = value; }
    }
    #endregion

    #region ADGroupsCommaDel
    private string _ADGroupsCommaDel;
    public string ADGroupsCommaDel
    {
        get
        {

            return _ADGroupsCommaDel;
        }
        set { _ADGroupsCommaDel = value; }
    }
    #endregion


    public void GetUserInfo()
    {
        DataTable dtDomainPasscode = new DataTable();
        dtDomainPasscode.Columns.Add("Code");
        dtDomainPasscode.Columns.Add("FQDN");
        dtDomainPasscode.Columns.Add("Username");
        dtDomainPasscode.Columns.Add("Password");

        XMLDataAccess.MasterDataLocation = ConfigurationManager.AppSettings["XMLMasterDataLocation"].ToString();
        dtDomainPasscode = XMLDataAccess.GetMasterData(XMLDataAccess.XMLData.DomainPasscodes, dtDomainPasscode);

        string DomainCode = _Username.Substring(0, _Username.IndexOf(@"\"));
        ADConnector.LDAPDomain = dtDomainPasscode.Select("Code = '" + DomainCode + "'")[0]["FQDN"].ToString();
        ADConnector.LDAPUsername = dtDomainPasscode.Select("Code = '" + DomainCode + "'")[0]["Username"].ToString();
        ADConnector.LDAPPassword = dtDomainPasscode.Select("Code = '" + DomainCode + "'")[0]["Password"].ToString();

        
        HasK2AdminRights = GetAdminRights();

        ADGroups = GetADGroups<string>();
        FinanceGroups = GetFinanceGroups<FinanceGroup>();
        WFGroups = GetWFGroups<string>();
        HasCAPEXReportsRights = (FinanceGroups.Count > 0);
        HasDocSubReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.DocumentSubmissionReport"]) >= 0); 
        HasTravelReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.TravelReport"]) >= 0);
        HasCAPEXAllReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.CAPEXReport"]) >= 0);
        HasIPRReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.IPRReport"]) >= 0);
        HasCRPReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.CRPReport"]) >= 0);
        HasCRPReportsRightsByContractType = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.CRPReportByContractType"]) >= 0);
        HasCRPReportsRightsByContractTypeWithMU = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.CRPReportByContractTypeWithMU"]) >= 0);
        HasSCRReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.SCRReport"]) >= 0);
        HasPRReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.PRReport"]) >= 0);
        HasCSRReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.CSRReport"]) >= 0);
        HasSMRReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.SMRReport"]) >= 0);
        HasOARReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.OARReport"]) >= 0);
        HasTSRReportsRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.TSRReport"]) >= 0);
        HasHRTReportRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.HRTReport"]) >= 0);
        HasEEPReportRights = (WFGroups.IndexOf(ConfigurationManager.AppSettings["UserGroup.EEPReport"]) >= 0);
        GetDisplayName();
    }

    private List<FinanceGroup> GetFinanceGroups<T>()
    {
        // get finance groups master list
        XMLDataAccess.MasterDataLocation = ConfigurationManager.AppSettings["XMLMasterDataLocation"].ToString();
        DataTable dtFinanceGroups = XMLDataAccess.GetMasterData(XMLDataAccess.XMLData.FinanceGroups);

        List<FinanceGroup> financeGroups = new List<FinanceGroup>();  

        //foreach (string ADGroup in ADGroups<string>()) // for all AD groups of user, check if it the group exists in the finance group master
        //{
        //    foreach (DataRow dr in dtFinanceGroups.Rows)
        //    {
        //        if ((@_Username.Substring(0, @_Username.IndexOf(@"\") + 1) + dr["Description"].ToString().Trim().ToUpper()) == ADGroup.Trim().ToUpper())
        //        {
        //            FinanceGroup fg = new FinanceGroup();
        //            fg.Name = dr["Description"].ToString().Trim();
        //            fg.Country = dr["Code"].ToString().Trim();

        //            financeGroups.Add(fg);
        //        }
        //    }
        //}

        foreach (string WFGroup in GetWFGroups<string>()) // for all AD groups of user, check if it the group exists in the finance group master
        {
            foreach (DataRow dr in dtFinanceGroups.Rows)
            {
                if (dr["Description"].ToString().Trim().ToUpper() == WFGroup.Trim().ToUpper())
                {
                    FinanceGroup fg = new FinanceGroup();
                    fg.Name = dr["Description"].ToString().Trim();
                    fg.Country = dr["Code"].ToString().Trim();

                    financeGroups.Add(fg);
                }
            }
        }

        return financeGroups;
    }

    private List<string> GetADGroups<T>()
    {
        List<string> aDGroups = new List<string>();
        
        /*
        DataTable dtDomainPasscode = new DataTable();
        dtDomainPasscode.Columns.Add("Code");
        dtDomainPasscode.Columns.Add("FQDN");
        dtDomainPasscode.Columns.Add("Username");
        dtDomainPasscode.Columns.Add("Password");

        dtDomainPasscode = XMLDataAccess.GetMasterData(XMLDataAccess.XMLData.DomainPasscodes, dtDomainPasscode);

        string DomainCode = _Username.Substring(0, _Username.IndexOf(@"\") + 1);
        ADConnector.LDAPDomain = dtDomainPasscode.Select("Code = '" + DomainCode + "'")[0]["FQDN"].ToString();
        ADConnector.LDAPUsername = dtDomainPasscode.Select("Code = '" + DomainCode + "'")[0]["Username"].ToString();
        ADConnector.LDAPPassword = dtDomainPasscode.Select("Code = '" + DomainCode + "'")[0]["Password"].ToString();
        */

//        ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
//        ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
//        ADConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();

        string[] groups = ADConnector.GetGroupsOfUser(@_Username.Substring(_Username.Trim().LastIndexOf(@"\") + 1));
        StringBuilder sb = new StringBuilder();

        if (groups != null)
        {
            

            foreach (string group in groups)
            {
                aDGroups.Add(String.Format(@"{0}{1}", @_Username.Substring(0, @_Username.IndexOf(@"\") + 1), group));

                sb.Append(String.Format(@",{0}{1}", _Username.Substring(0, _Username.IndexOf(@"\") + 1).Trim(), group.Trim()));
            }
        }

        aDGroups.Add(String.Format(@"{0}{1}", @_Username.Substring(0, @_Username.IndexOf(@"\") + 1), "Domain Users"));
        sb.Append(String.Format(@",{0}{1}", _Username.Substring(0, _Username.IndexOf(@"\") + 1).Trim(), "Domain Users"));

        ADGroupsCommaDel = sb.ToString().Substring(1);

        return aDGroups;
    }

    private List<string> GetWFGroups<T>()
    {
        ENPUser enpUser = new ENPUser();
        enpUser.Get(Username, ENPUser.GetBy.LoginName);

        return enpUser.WFGroups;

        //List<string> aDGroups = new List<string>();

        //string[] groups = ADConnector.GetGroupsOfUser(@_Username.Substring(_Username.Trim().LastIndexOf(@"\") + 1));

        //if (groups != null)

        //    foreach (string group in groups)
        //    {
        //        aDGroups.Add(String.Format(@"{0}{1}", @_Username.Substring(0, @_Username.IndexOf(@"\") + 1), group));
        //    }

        //aDGroups.Add(String.Format(@"{0}{1}", @_Username.Substring(0, @_Username.IndexOf(@"\") + 1), "Domain Users"));

        //return wFGroups;
    }

    private bool GetAdminRights()
    {
        K2SQLDataAccess k2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
        K2ServiceManager K2SvcMgr = new K2ServiceManager(ConfigurationManager.AppSettings["SourceCode.K2.PlanServer"], ConfigurationManager.AppSettings["SourceCode.K2.ConnectionString"]);

        string strADGroups = string.Empty;

        DataTable dtProcSets = k2SQLDA.GetProcSetWithPermission(_Username, ref strADGroups);
        K2ServiceManager.PermissionSet permissions; 

        foreach (DataRow dr in dtProcSets.Rows) // traverse all processes where user has ANY rights
        {
            permissions = new K2ServiceManager.PermissionSet(K2SvcMgr, Convert.ToInt32(dr["ProcSetID"]), Username, strADGroups); 

            // if user has admin rights to ANY of the Processes, user is treated as K2 Admin
            if (permissions.Admin)
                return true;
        }

        return false;
    }

    private void GetDisplayName()
    {
        DisplayName = ADConnector.GetUserDisplayName(_Username.Substring(_Username.LastIndexOf(@"\") + 1));
    }
}

public class FinanceGroup
{
    public FinanceGroup()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Name
    private string _Name;
    public string Name
    {
        get
        {
            return _Name;
        }
        set
        {
            _Name = value;
        }

    }
    #endregion

    #region Country
    private string _Country;
    public string Country
    {
        get
        {
            return _Country;
        }
        set
        {
            _Country = value;
        }

    }
    #endregion
}


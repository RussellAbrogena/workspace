using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SourceCode.K2ROM;
using Emerson.WF.Remarks;
using Emerson.WF.Attachment;
using System.Security.Principal;
using System.IO;
using System.Data.SqlClient;

using Emerson.WF;
using Emerson.WF.K2DataAccess;


public partial class UserControls_Remarks : System.Web.UI.UserControl
{
    private K2SQLDataAccess K2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);

    // added: al 4/7/07
    //private string _ExtProcessName;
    public string ExtProcessName
    {
        set
        {
            ViewState["_ExtProcessName"] = value;
        }
        get
        {
            return (string)ViewState["_ExtProcessName"];
        }
    }

   
    public string ExtProcessID
    {
        set
        {
            ViewState["_ExtProcessID"] = value;
        }
        get
        {
            return (string)ViewState["_ExtProcessID"];
        }
    }

    public string ProcInstGuid
    {
        set
        {
            ViewState["_ProcInstGuid"] = value;
        }
        get
        {
            return (string)ViewState["_ProcInstGuid"];
        }
    }
    // added: al 4/7/07

    public int ProcInstID
    {
        set
        {
            ViewState["_ProcInstID"] = value;
        }
        get
        {
            return (int)ViewState["_ProcInstID"];
        }
    }

    public bool ViewOnly
    {
        set
        {
            ViewState["_ViewOnly"] = value;
        }
        get
        {
            return (bool)ViewState["_ViewOnly"];
        }
    }

    //the same property is declared in remarks.ascx
    private const string _V_start_temp_guid = "_V_start_temp_guid";
    public Guid requestTempGuid
    {
        set
        {
            ViewState[_V_start_temp_guid] = value;

            Trace.Write("Capex: remarks, temp guid set", ViewState[_V_start_temp_guid].ToString());
        }

        get
        {
            if (ViewState[_V_start_temp_guid] == null)
            {   
                ViewState.Add(_V_start_temp_guid, Guid.NewGuid());

                Trace.Write("Capex: remarks, temp guid created again");
            }

            Trace.Write("Capex: remarks, temp guid get", ViewState[_V_start_temp_guid].ToString());

            return (Guid)ViewState[_V_start_temp_guid];
        }
    }
    //

    protected string DOWNLOAD_LINK_URL = "download.aspx";
    [Serializable]
    private class DownloadLink
    {
        #region AttachmentId
        private string attachmentId = "";

        public string AttachmentId
        {
            get { return attachmentId; }
            set { attachmentId = value; }
        }
        #endregion

        #region Link
        public string Link
        {
            get { return string.Format("{0}/download.aspx?fid=" + attachmentId, 
                ConfigurationManager.AppSettings["WebAppRootPath"]); }
        }
        #endregion

        #region FileName
        private string fileName = "";

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        #endregion

        public DownloadLink()
        {
        }
        public DownloadLink(string id, string name)
        {
            this.AttachmentId = id;
            this.FileName = name;
        }
    }

    private class RemarksWithLinks : BaseRemarks
    {
        #region Links
        public List<DownloadLink> links = new List<DownloadLink>();

        #endregion

        #region GetLinks
        public string GetLinks
        {
            get 
            {
                string result = "";
                foreach (DownloadLink iterLink in links)
                {
                    result += string.Format("<a href='{0}' target='_blank'>{1}</a><br>", iterLink.Link, iterLink.FileName);
                }

                return result;
            }
        }
        #endregion
    }

    private const string _V_Download_Links = "_V_Download_Links";

    //private List<DownloadLink> _downloadLinks = null;
    private List<DownloadLink> downloadLinks
    {
        get
        {
            if (ViewState[_V_Download_Links] == null)
                ViewState.Add(_V_Download_Links, new List<DownloadLink>());

            return (List<DownloadLink>)ViewState[_V_Download_Links];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //btnUpLoad.Attributes.Add("onclick", "setConfirmRequired('0');");
        }

        tblRemarks.Visible = !ViewOnly;
    }

    /// <summary>
    /// Update remark in to current K2 process
    /// </summary>
    /// <param name="process">K2 current process</param>
    /// <returns>True if remark was added to database. False if no remark</returns>
    public bool Bind_Remark(WorklistItem workitem, ProcessInstance process, bool isSaveDraft)
    {   
            BaseRemarks objNewRemark = new BaseRemarks();
            
            objNewRemark.Content = txtComment.Text;//HttpUtility.HtmlEncode(txtComment.Text.Replace("\r\n", "<br>"));
            objNewRemark.CreateAt = DateTime.Now;
            objNewRemark.Title = "Remark";
            objNewRemark.UserName = Page.User.Identity.Name;

            objNewRemark.Attachments = "";
            foreach (DownloadLink file in downloadLinks)
            {
                objNewRemark.AddAttachment(file.AttachmentId);
            }

            bool remarkEmpty = (objNewRemark.Content == "") && (objNewRemark.Attachments == "");

            if (workitem == null || !isSaveDraft)//workitem = null means currently at start page
            {
                if (!remarkEmpty)
                {
                    List<BaseRemarks> remarks = null;

                    string oldxmlRemarksString = process.DataFields["Remarks"].Value.ToString();
                    if (oldxmlRemarksString.Length > 0)
                        remarks = objNewRemark.DeserializeToList<BaseRemarks>(oldxmlRemarksString);
                    else
                        remarks = new List<BaseRemarks>();

                    remarks.Add(objNewRemark);
                    process.DataFields["Remarks"].Value = objNewRemark.SerializeFromList(remarks);
                }
            }
            else//saving draft and currently at approval page
            {
                if (remarkEmpty)
                {
                    //workitem.ActivityInstanceDestination.DataFields["wfaRemarksTmp"].Value = "";
                    workitem.ProcessInstance.DataFields["RemarksTmp"].Value = "";
                }
                else
                {
                    List<BaseRemarks> tmpList = new List<BaseRemarks>();
                    tmpList.Add(objNewRemark);

                    workitem.ProcessInstance.DataFields["RemarksTmp"].Value = objNewRemark.SerializeFromList(tmpList);
                    //workitem.ActivityInstanceDestination.DataFields["wfaRemarksTmp"].Value =
                    //    objNewRemark.SerializeFromList(tmpList);
                }
            }

            // added al: 
            if (isSaveDraft && workitem == null)
            {
                process.DataFields["RemarksTmp"].Value = process.DataFields["Remarks"].Value;
                process.DataFields["Remarks"].Value = string.Empty;
            }
            if (!isSaveDraft && workitem != null)
                workitem.ProcessInstance.DataFields["RemarksTmp"].Value = "";
            // added al: 

            return true;
    }

    private void Show_Remarks(bool isDraft, string processRemarks, string tmpRemarks)
    {   
        List<BaseAttachment> arrAttachments =
            new BaseAttachment().GetBy<BaseAttachment>("", ProcInstGuid, "");

        
        //List<BaseAttachment> arrAttachments = 
        //    new BaseAttachment().GetBy<BaseAttachment>("", requestTempGuid.ToString(), "");

        Trace.Write("Capex: remarks, the number of attachment files is", arrAttachments.Count.ToString());

        BaseRemarks deRemark = new BaseRemarks();

        //previous remarks
        if (processRemarks.Length > 0)
        {
            List<BaseRemarks> baseRemarks = deRemark.DeserializeToList<BaseRemarks>(processRemarks);

            List<RemarksWithLinks> remarks = new List<RemarksWithLinks>();

            foreach (BaseRemarks iter in baseRemarks)
            {
                RemarksWithLinks obj = new RemarksWithLinks();
                obj.Attachments = iter.Attachments;

                obj.Content = iter.Content;
                obj.CreateAt = iter.CreateAt;
                obj.Title = iter.Title;
                obj.UserName = iter.UserName;

                string[] arr = iter.Attachments.Split(';');
                foreach(string s in arr)
                    if (s.Length > 0)
                    {

                        string filename = s;
                        foreach (BaseAttachment iterAtt in arrAttachments)
                        {
                            Trace.Write("Capex: remarks, iterAtt.AttachmentID", iterAtt.AttachmentID);

                            if (iterAtt.AttachmentID == s)
                            {
                                filename = iterAtt.FileName;

                                Trace.Write("Capex: remarks, matched file name", filename);

                                break;
                            }
                        }

                        obj.links.Add(new DownloadLink(s, filename));
                    }

                remarks.Add(obj);
            }

            gridRemarks.DataSource = remarks;
            gridRemarks.DataBind();
        }

        bool ok = false;

        //saving remarks
        if (tmpRemarks.Length > 0)
        {
            List<BaseRemarks> remarks = deRemark.DeserializeToList<BaseRemarks>(tmpRemarks);
            if (remarks.Count > 0)
            {
                txtComment.Text = remarks[0].Content;
                ok = true;

                string[] arrIds = remarks[0].Attachments.Split(';');
                downloadLinks.Clear();

                foreach(string s in arrIds)
                    if (s.Length > 0)
                    {
                        string filename = s;
                        foreach (BaseAttachment iterAtt in arrAttachments)
                        {
                            filename = iterAtt.FileName;
                            if (iterAtt.AttachmentID == s)
                            {
                                filename = iterAtt.FileName;
                                break;
                            }
                        }
                        downloadLinks.Add(new DownloadLink(s, filename));
                    }

                grdDownloadLinks.DataSource = downloadLinks;
                grdDownloadLinks.DataBind();
            }
        }

        if (!ok)
        {
            txtComment.Text = "";
        }
    }

    #region Attachment file

    /// <summary>
    /// upload one file up to temp directory server
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpLoad_Click(object sender, EventArgs e)
    {
        if (objUploadFile.PostedFile.FileName == "") return;
        
        BaseAttachment objAttach = new BaseAttachment();
        try
        {
            //objAttach.AttachmentID
            objAttach.CreatedAt = DateTime.Now;
            objAttach.FileName = Path.GetFileName(objUploadFile.PostedFile.FileName);
            //objAttach.FileRelativePath
            objAttach.FileType = Path.GetExtension(objUploadFile.PostedFile.FileName).ToUpper().Replace(".", "");
            
            //objAttach.ProcessName = CRPUtils.ExtProcessName;
            //objAttach.ProcessID = CRPUtils.ExtProcessId.ToString();
            

            objAttach.ProcessName = ExtProcessName;
            objAttach.ProcessID = ExtProcessID;
            objAttach.RequestID = ProcInstGuid;

            objAttach.UserName = this.Page.User.Identity.Name;

            objAttach.Serialize();

            objAttach.Save(objUploadFile.PostedFile.InputStream);

            //update download links
            downloadLinks.Add(new DownloadLink(
                objAttach.AttachmentID, objAttach.FileName));

            grdDownloadLinks.DataSource = downloadLinks;
            grdDownloadLinks.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
            //logged error
            //Common.ErrorsHandle(this.Parent.Page, ex.ToString());
        }
    }

    /// <summary>
    /// Delete uploaded file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbt_DelCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            string attId = e.CommandArgument.ToString();

            for(int i = 0; i < downloadLinks.Count; i ++)
            {
                if (downloadLinks[i].AttachmentId == attId)
                {
                    BaseAttachment obj = new BaseAttachment();

                    obj.Delete(attId);

                    downloadLinks.Remove(downloadLinks[i]);

                    grdDownloadLinks.DataSource = downloadLinks;
                    grdDownloadLinks.DataBind();

                    break;
                }
            }
        }
    }

    public bool BindDataFields(WorklistItem workitem, ProcessInstance process, bool isSaveDraft)
    {
        // remarks
        bool RemarkIsAdded = false;
        RemarkIsAdded = Bind_Remark(workitem, process, isSaveDraft);

        return RemarkIsAdded;
    }

    public void ShowDataFields(bool isDraft)
    {
        //requestTempGuid = workitem.ProcessInstance.Guid;
        //requestTempGuid = ProcInstGuid;

        // remarks
        //Show_Remarks(isDraft, workitem.ProcessInstance.DataFields["Remarks"].Value.ToString(),
        //    workitem.ActivityInstanceDestination.DataFields["wfaRemarksTmp"].Value.ToString());

        Show_Remarks(isDraft, K2SQLDA.GetProcInstDataFieldValue(ProcInstID, Konstants.DataField.Remarks),
           K2SQLDA.GetProcInstDataFieldValue(ProcInstID, Konstants.DataField.Remarks));
    }

    public void DisableControlInformationForReassign()
    {   
        this.txtComment.Enabled = false;
        this.objUploadFile.Disabled = true;
        this.btnUpLoad.Enabled = false;
    }

    protected Hashtable KeepOldFormvalues()
    {
        //Hashtable objOldValues = new Hashtable();

        //TextBox txtcomment = Information1.FindControl("txtComment") as TextBox;
        //objOldValues.Add(txtcomment.UniqueID, txtcomment.Text);

        //Session["FormControlOldValuesApproval"] = objOldValues;
        //return objOldValues;
        return null;
    }

    #endregion
}

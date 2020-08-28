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

using System.Security.Principal;

using System.IO;

using Emerson.WF.Attachment;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fid = Request.QueryString["fid"];

        if (fid == null)
        {
            Response.End();
            return;
        }

     
            BaseAttachment obj = new BaseAttachment();

            string filePath = obj.GetFilePathToRead(fid);

            Response.Write(User.Identity.Name.ToString());

            Response.Write(filePath);

            if (!File.Exists(filePath))
            {
                Response.End();
                return;
            }

            FileInfo file = new FileInfo(filePath);

            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name.Replace(fid, ""));
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";

            Response.WriteFile(filePath);

            Response.End();
    }

   
}
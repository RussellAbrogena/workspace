<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

        Exception ex = Server.GetLastError().GetBaseException();
        ex.Data.Add("Reference ID", Guid.NewGuid().ToString());
        ex.Data.Add("Application Name", "ENP Workspace");
        ex.Data.Add("Exception Date", DateTime.Now);
        ex.Data.Add("User Logged In", Request.ServerVariables["LOGON_USER"].ToString());

        Emerson.WF.ExceptionHandler exHandler = new Emerson.WF.ExceptionHandler(ex);

        //exHandler.LogToFile();
        exHandler.NotifyViaEmail();

        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["ErrorPageUrl"] + "?errtype=" + Emerson.WF.Constants.ErrorMessage.SystemError);

    }

    void Session_Start(object sender, EventArgs e)
    {
        UserPassport userPassport = new UserPassport(Request.ServerVariables["LOGON_USER"].ToString());
        //userPassport.Username = Request.ServerVariables["LOGON_USER"].ToString();
        Session["UserPassport"] = userPassport;
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
       
</script>

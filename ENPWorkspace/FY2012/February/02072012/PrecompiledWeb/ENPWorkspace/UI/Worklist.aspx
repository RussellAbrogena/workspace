<%@ page language="C#" autoeventwireup="true" inherits="Worklist, App_Web_worklist.aspx.76ff77b3" %>

<%@ Register Src="UserControls/TopPanel.ascx" TagName="TopPanel" TagPrefix="uc1" %>
<%@ Register Src="UserControls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ENP Workspace - Worklist</title>
    <link href="../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
		var cursel = 'Worklist';
		
		/* Program Logic Scripts - BEGIN */

		function ViewItems()
		{
		    var worklistOptionsFrame = frames['WorklistOptionsFrame'];
		    worklistOptionsFrame = worklistOptionsFrame.document.forms[0];
		    		    
		    if (worklistOptionsFrame.elements["hdnIsPostBack"].value == 'true') // after view items was clicked
		    {    
		        //document.getElementById('MainFrame').src = "Worklist/WorklistItems.aspx?process=" + worklistOptionsFrame.elements["hdnProcessFilter"].value;
		        frames['MainFrame'].location = "Worklist/WorklistItems.aspx?process=" + worklistOptionsFrame.elements["hdnProcessFilter"].value;
		        RefreshMenu();
		    }
		    else // first load
		    {
		        if ('<%Response.Write(ApprovalURL);%>' != '')
                    frames['MainFrame'].location = "<%Response.Write(ApprovalURL);%>";
		        else
        		    frames['MainFrame'].location = "Worklist/WorklistItemsExt.aspx?process=ALL";
		    }
		    	    
    		positionLoadingProgress();
		}
		
    	function RefreshMenu()
		{
            frames['MenuFrame'].location = "Menu/MenuItems.aspx?cursel=Worklist";
		}
		
        function TrimFrameContent() {
		    var worklistItemsFrame = frames['MainFrame'];
		    worklistItemsFrame.document.body.style.width = "96%";
        }
		/* Program Logic Scripts - END */
		
		/* UI scripts - BEGIN */	
	
		function positionLoadingProgress(top, left)
        {
        
        //style="position:absolute;z-index:10;left:expression((this.offsetParent.clientWidth/2)-(this.clientWidth/2)+this.offsetParent.scrollLeft);top:expression((this.offsetParent.clientHeight/2)-(this.clientHeight/2)+this.offsetParent.scrollTop);"
           if ((window.screen.height/window.screen.width) == 0.75) // normal resolution
            {
		        var top = parseInt((window.screen.height / 2) + 20)
                var left = parseInt((window.screen.width  / 2) + 50)	        
		    }
		    else // widescreen
            {		        
		        var top = parseInt((window.screen.height / 2) - 10)
                var left = parseInt((window.screen.width  / 2) - 90)
            }
            
                   
            document.getElementById("divLoading").style.display = "";
            document.getElementById("divLoading").style.left = left + "px";
            document.getElementById("divLoading").style.top = top + "px";   
        }

        function hideLoadingProgress()
        {
            if (document.getElementById("divLoading") != null)
                document.getElementById("divLoading").style.display = "none";
        }
		/* UI scripts - ENDs */
	</script>
    <link href="../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top: 0px; margin-bottom: 0px; margin-left:0px; height: 580px;">
    <form id="form1" runat="server">
	<table style="margin-bottom: 1px; height:100%; width:1000px;" cellspacing="0" cellpadding="0">
			<tr>
				<td style="border-right: solid 1px #000000"><uc1:TopPanel ID="ucTopPanel" runat="server" />
                </td>
			</tr>
			<tr>
				<td colspan="2" align="left" valign="top" style="height: 100%; border-bottom: solid 1px #000000; border-right: solid 1px #000000">
				<table style="height:100%; width:1000px;" border="0" cellpadding="0" cellspacing="0">
						<tr valign="top">
						    <!-- Left Panel - BEGIN -->
							<td style="height:100%; width:200px; background-color:#CAD2E0">
							    <table border="0" cellspacing="2" cellpadding="2" style="width: 200px;">
							    <tr>
									    <td valign="top" style="height: 83px"> 
                                            <iframe id="MenuFrame" src="Menu/MenuItems.aspx?cursel=Worklist" frameborder="0" width="100%" height="83px" style="background-color:#CAD2E0" allowtransparency="true">
        						            </iframe>
										 </td>
									</tr>
									<!-- Left - Top Panel - BEGIN -->
									<!--tr style="width:100%; height:422px;" valign="top"-->
									<tr style="width:100%; height:481px;" valign="top">
									    <td valign="top">
									    <table cellspacing="0" cellpadding="0" width="100%" border="1" style=" background-color:White; height:100%; border-color:#1e3c7c;">
									            <tr>
										            <td style="width: 191px" valign="top">
                                                    <iframe onload="ViewItems()" src="Worklist/WorklistOptions.aspx" id="WorklistOptionsFrame" frameborder="0" width="100%" height="481px">
							                        </iframe>
										            </td>
									            </tr>								
								        </table>
									    </td>
									</tr>
									<!-- Left - Top Panel - END -->
									<!-- MenuLine Items - BEGIN -->
									
									<!-- MenuLine Items - END -->
    							</table>
							</td>
							<!-- Left Panel - END -->
							
							<td class="BlackBorder" style="width: 3px"><img src="../Images/spacer.gif" alt=""/></td>
							
							<!-- Main Panel - BEGIN -->
							<td align="left" valign="top" style="height:100%; width:800px; background-color:#D2DEE9">
							    <iframe onload="RefreshMenu(); hideLoadingProgress();" id="MainFrame" frameborder="0" width="100%" scrolling="auto" height="582px">
							    </iframe>
							</td>
							<!-- Main Panel - END -->
						</tr>
					</table>
					<div id='divLoading' style="Z-INDEX: 100; height:0px; CURSOR:wait; POSITION: absolute; vertical-align:middle; display:none; ">
                        <uc2:UpdateProgress id="UpdateProgress2" runat="server"></uc2:UpdateProgress>
                    </div>
				</td>

			</tr>
		</table>
    </form>

</body>
</html>

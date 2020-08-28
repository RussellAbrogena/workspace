<%@ page language="C#" autoeventwireup="true" inherits="Search, App_Web_search.aspx.76ff77b3" %>

<%@ Register Src="UserControls/TopPanel.ascx" TagName="TopPanel" TagPrefix="uc1" %>
<%@ Register Src="UserControls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ENP Workspace - Search</title>
    <link href="../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
		var cursel = 'Search';
		
		/* Program Logic Scripts - BEGIN */
     	function SearchItems()
		{
		    var searchOptionsFrame = frames['SearchOptionsFrame'];
		    searchOptionsFrame = searchOptionsFrame.document.forms[0];    	    
		    
		    if (searchOptionsFrame.elements["hdnIsPostBack"].value == 'true')
		    {    
		        //document.getElementById('SearchFrame').src = "Search/SearchResults.aspx?search=" + searchOptionsFrame.elements["txtSearch"].value + "&status=" + searchOptionsFrame.elements["cboStatusFilter"].value + "&process=" + searchOptionsFrame.elements["hdnProcessFilter"].value;
		        frames['MainFrame'].location = "Search/SearchResults.aspx?search=" + searchOptionsFrame.elements["txtSearch"].value + "&status=" + searchOptionsFrame.elements["cboStatusFilter"].value + "&process=" + searchOptionsFrame.elements["hdnProcessFilter"].value;
		        	    
    		    positionLoadingProgress();
    		    
		    }		    
		    else // first load
		    {
		        if ('<%Response.Write(DetailsURL);%>' != '')
                    frames['MainFrame'].location = "<%Response.Write(DetailsURL);%>";
		        else
        		    frames['MainFrame'].src = "#";
		    }
		    	    
		}
		
		function RefreshMenu()
		{
            frames['MenuFrame'].location = "Menu/MenuItems.aspx?cursel=Worklist";
		}
		
		/* Program Logic Scripts - END */
		
		/* UI scripts - BEGIN */	
		
		function positionLoadingProgress()
        {
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
            
            //left = expression((document.body.clientWidth/2)-(document.body.clientWidth/2)+document.body.scrollLeft);
            //top = expression((document.body.clientHeight/2)-(document.body.clientHeight/2)+document.body.scrollTop);
        
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
	<table style="margin-bottom: 1px; height:100%; width:1000px;" cellspacing="0" cellpadding="0" id="tblMain">
			<tr>
				<td style="border-right: solid 1px #000000"><uc1:TopPanel ID="ucTopPanel" runat="server" />
                </td>
			</tr>
			<tr>
				<td colspan="2" align="left" valign="top" style="height: 100%; border-bottom: solid 1px #000000; border-right: solid 1px #000000">
				<table style="height:100%; width:1000px;" border="0" cellpadding="0" cellspacing="0">
						<tr>
						    <!-- Left Panel - BEGIN -->
						    
						    
							<!--td style="height:100%; width:200px; background-color:#010066"-->
							
							<td style="height:100%; width:200px; background-color:#CAD2E0 ">
							
							
							    <table border="0" cellspacing="2" cellpadding="2" style="width: 200px;">
									<tr>
									    <td>
									        <iframe id="MenuFrame" src="Menu/MenuItems.aspx?cursel=Search" scrolling="auto" frameborder="0" width="100%" height="83px" allowtransparency="true">
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
                                                    <iframe onload="SearchItems()" src="Search/SearchOptions.aspx" id="SearchOptionsFrame" frameborder="0" width="100%" height="481px" style="overflow-y:auto; overflow-x: hidden;">
							                        </iframe>
										            </td>
									            </tr>								
								            </table>
									    </td>
									</tr>
									<!-- Left - Top Panel - END -->
									
									<!-- Left Panel Vertical Extender - BEGIN -->
									<!--tr style="height:100%">
									    <td>
									        <img src="../Images/spacer.gif" alt="" style="height:1px" />
									    </td>
									</tr-->
									<!-- Left Panel Vertical Extender - END -->
									
									<!-- MenuLine Items - BEGIN -->
									
									<!-- MenuLine Items - END -->
									
								</table>
							</td>
							<!-- Left Panel - END -->
							
							<td class="BlackBorder" style="width: 3px"><img src="../Images/spacer.gif" alt=""/></td>
							
							<!-- Main Panel - BEGIN -->
							<td align="left" valign="bottom" style="height:100%; width:800px; background-color:#D2DEE9">
							<iframe src="#" id="MainFrame" frameborder="0" width="100%" scrolling="auto" height="582px" onload="hideLoadingProgress();">
							</iframe>
							</td>
							<!-- Main Panel - END -->
						</tr>
					</table>
					<div id='divLoading' style="Z-INDEX: 100; CURSOR:wait; POSITION: absolute; vertical-align:middle; display:none; ">
                        <uc2:UpdateProgress id="UpdateProgress2" runat="server"></uc2:UpdateProgress>
                    </div>
				</td>
			</tr>
		</table>
    </form>
</body>
</html>

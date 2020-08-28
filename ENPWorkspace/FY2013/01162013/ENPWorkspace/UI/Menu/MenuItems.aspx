<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuItems.aspx.cs" Inherits="UI_Menu_MenuItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Workspace - MenuItems</title>
    <link href="../../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
    
    var cursel = '<% Response.Write(Request.QueryString["cursel"]); %>';
    
    
   
    /* Program Logic Scripts - BEGIN */
		function GetSearch()
		{
		    window.parent.location = "../Search.aspx";
		}
		
		function GetWorklist()
		{
		    window.parent.location = "../Worklist.aspx";
		}
		
		function GetReports()
		{
		    window.parent.location = "../Reports.aspx";
		}
	/* Program Logic Scripts - END */	
    
    /* UI scripts - BEGIN */
        function SetSelectedStyle()
        {
            document.getElementById(cursel + "TD").className ="MenuLineSelect";
        }	
        
		function Swap_Style()
		{
			var elid = window.event.srcElement.id
			if (elid == cursel)
			{
				document.all(elid + "TD").className ="MenuLineOverSelect";
			}
			else
			{
				document.all(elid + "TD").className ="MenuLineOver";
			}	
		}

		function Swap_Style_Back()
		{
			var elid = window.event.srcElement.id;
			
			if (elid == cursel)
			{
				document.all(elid + "TD").className ="MenuLineSelect";
			}
			else
			{
				document.all(elid + "TD").className ="MenuLine";
			}	
		}
    /* UI scripts - BEGIN */	
    </script>
</head>
<body onload="SetSelectedStyle();">
    <form id="form1" runat="server">
    <div>
        <table border="1" cellpadding="0" cellspacing="0" width="100%" >
          <tr>
                <td id="WorkListTD" class="MenuLine" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td id="Worklist" class="WorkListMenuLine" onclick="GetWorklist();" onmouseout="Swap_Style_Back()"
                                onmouseover="Swap_Style()" style="width: 188px; cursor: hand" title="Worklist">
                                Worklist <% Response.Write(WorklistCount); %>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="ReportsTD" class="MenuLine" runat="server">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td id="Reports" class="ReportsMenuLine" onclick="GetReports();" onmouseout="Swap_Style_Back()"
                                onmouseover="Swap_Style()" style="width: 188px; cursor: hand" title="Reports">
                                Reports</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="SearchTD" class="MenuLine" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td id="Search" class="SearchMenuLine" onclick="GetSearch();;" onmouseout="Swap_Style_Back()"
                                onmouseover="Swap_Style()" style="cursor: hand" title="Search">
                                Search</td>
                        </tr>
                    </table>
                </td>
            </tr>
          
            
         
        </table>
    
    </div>
    </form>
</body>
</html>

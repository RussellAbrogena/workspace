<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchOptions.aspx.cs" Inherits="UI_Search_SearchOptions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search Options</title>
    <link href="../../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">BODY { MARGIN: 0px }
	        .style5 { FONT-SIZE: 11px; FONT-FAMILY: Tahoma }
	        .style7 { FONT-WEIGHT: bold; COLOR: #ffffff }
	        .style9 { FONT-SIZE: 11px; COLOR: #1e3c7c; FONT-FAMILY: Tahoma }
	        .style12 { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #1e3c7c; FONT-FAMILY: Tahoma }
	</style>
	
	<script language="javascript" type="text/javascript">
	function toggleMoreOptions(toggle)
	{    
	    if (toggle)
	    {
	        var str = document.all("iconMoreOptions").src;
	        
	        if (str.search("expand") > 0)
	        {
	            document.all("iconMoreOptions").src = str.replace(/expand/, "collapse");
	            document.all("trMoreOptions").style.display = "inline";
	        }
	        else
	        {
	            document.all("iconMoreOptions").src = str.replace(/collapse/, "expand");
	            document.all("trMoreOptions").style.display = "none";
	        }
    	    
	        document.all("hdnMoreOptions").value = document.all("iconMoreOptions").src;
        }
        else
        {
            if (document.all("hdnMoreOptions").value != '')
            {
               var str = document.all("hdnMoreOptions").value;
               document.all("iconMoreOptions").src = document.all("hdnMoreOptions").value;
            }
            else
            {
                document.all("hdnMoreOptions").value = document.all("iconMoreOptions").src;
                return;
            }
               
            if (str.search("expand") > 0)
	            document.all("trMoreOptions").style.display = "none";
	        else
	            document.all("trMoreOptions").style.display = "inline";
        }	    
        
        if (document.all("trMoreOptions").style.display == "inline")
            document.all("iconMoreOptions").title = "Close More Options"
        else
            document.all("iconMoreOptions").title = "View More Options"
	}
	
	function toggleSearchOptions(toggleSearch)
	{    
	    if (toggleSearch)
	    {
	        var str = document.all("iconSearchOptions").src;
	        
	        if (str.search("expand") > 0)
	        {
	            document.all("iconSearchOptions").src = str.replace(/expand/, "collapse");
	            document.all("trSearchOptions").style.display = "inline";
	        }
	        else
	        {
	            document.all("iconSearchOptions").src = str.replace(/collapse/, "expand");
	            document.all("trSearchOptions").style.display = "none";
	        }
    	    
	        document.all("hdnSearchOptions").value = document.all("iconSearchOptions").src;
        }
        else
        {
            if (document.all("hdnSearchOptions").value != '')
            {
               var str = document.all("hdnSearchOptions").value;
               document.all("iconSearchOptions").src = document.all("hdnSearchOptions").value;
            }
            else
            {
                document.all("hdnSearchOptions").value = document.all("iconSearchOptions").src;
                return;
            }
               
            if (str.search("expand") > 0)
	            document.all("trSearchOptions").style.display = "none";
	        else
	            document.all("trSearchOptions").style.display = "inline";
        }	    
        
        if (document.all("trSearchOptions").style.display == "none")
            document.all("iconSearchOptions").title = "View Search Options"
        else
            document.all("iconSearchOptions").title = "Close Search Options"
	}
	
	function SaveSearch()
    {
        var answer = prompt('Save Search As...   ', '');
        
        if (answer == 'Default')
        {
            alert("Please input a different Name.    ");
            return false;
        }
        
        if (answer != '' && answer != null)
        {

            // check if Saved Search Name already exists
            for (var i=0; i<document.getElementById("cboSavedSearch").length; i++)
            {
                if (answer == document.getElementById("cboSavedSearch").options[i].value)
                    if (!confirm("A Saved Search with the name '" + answer + "' already exists. Overwite?  "))
                    {
                        return false;
                        break;
                    }
                    else
                        break;
            }
        
            document.getElementById("hdnSaveAs").value = answer;
        }

        else
            return false;    
    }
    
    function DeleteSavedSearch()
    {
        if (document.getElementById('cboSavedSearch').value == "Default")
        {
            alert("Unable to delete Default.    ");
            return false;
        }
    
        if (confirm("Delete Saved Search '" + document.getElementById('cboSavedSearch').value + "' ?   "))
            return true;
        else
            return false;
    }
	</script>
</head>
<body onload="toggleMoreOptions(false);toggleSearchOptions(false);">
    <form id="form1" runat="server">
					                                    <table cellspacing="2" cellpadding="4" border="0" style="vertical-align: top; width: 100%; height:100%">
												            <tr>
													            <td style="width: 188px; border: solid 1px #92AEC9; background-color:#D2DEE9; font-weight: bold; color:#000000"><span><img alt="" id="iconSearchOptions" style="cursor:hand;" src="../../Images/icon_collapse.GIF" onclick="toggleSearchOptions(true);" title="Close Search Options"/></span>&nbsp;&nbsp;&nbsp;<span>Search Options</span></td>
												            </tr>
												            
												            <tr id="trSearchOptions">
													            <td style="height:100%; width: 188px; border: solid 1px #92AEC9;">
													        <table cellpadding = "4" cellspacing="2">
													             <tr>
													            <td style="width: 180px"><span>To start your search, fill up the parameters and click on the Search button.</span></td>
												            </tr>   
												            <tr>
													            <td style="width: 180px"><span class="style12">Search for</span></td>
												            </tr>
												            <tr>
													            <td style="width: 180px">
                                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="ms-input" Width="154px" MaxLength="50"></asp:TextBox><asp:TextBox
                                                                        ID="hdnTemp" runat="server" Style="display: none; visibility: hidden" Width="92%"></asp:TextBox></td>
												            </tr>
												            <tr>
													            <td style="width: 180px"><span class="style12">Status Filter</span></td>
												            </tr>
												            <tr>
													            <td style="width: 180px">
													            <asp:dropdownlist id="cboStatusFilter" runat="server" CssClass="ms-formdropdown" Width="160px">
															            <asp:ListItem Value="ALL" Selected="True">All</asp:ListItem>
															            <asp:ListItem Value="PENDING">Pending</asp:ListItem>
															            <asp:ListItem Value="APPROVED">Approved</asp:ListItem>
															            <asp:ListItem Value="DECLINED">Declined</asp:ListItem>
															            <asp:ListItem Value="CANCELLED">Cancelled</asp:ListItem>
															            <asp:ListItem Value="ERROR">Error</asp:ListItem>
														         </asp:dropdownlist>
														         </td>
												            </tr>
												            <tr>
													            <td style="width: 180px"><span class="style12">Process Filter</span></td>
												            </tr>
												            <tr>
													            <td valign="top" style="width: 180px">
													            <div id="divProcessList" runat="server" style="width:160px; height:180px; overflow-y:auto; ">
                                                                <asp:checkboxlist RepeatLayout="Table" id="chklProcess" runat="server" CssClass="ms-formdropdown" Width="140px" CellPadding="0" CellSpacing="0"></asp:checkboxlist>
                                                                </div>
													            </td>
												            </tr>
												            <tr>
													            <td valign="top" style="width: 180px">
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="ms-button" Height="22px" Text="Search"
                                                                        Width="44%" OnClick="btnSearch_Click" />
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="ms-button" Height="22px" Text="Save"
                                                                        Width="44%" OnClick="btnSave_Click" ValidationGroup="SaveAs"/></td>
												            </tr>   
													            </table>
													            </td>
												            </tr>
												            <tr>
													            <td style="width: 188px; border: solid 1px #92AEC9; background-color:#D2DEE9; font-weight: bold; color:#000000"><span><img alt="" id="iconMoreOptions" style="cursor:hand;" src="../../Images/icon_expand.GIF" onclick="toggleMoreOptions(true);" title="View More Options"/></span>&nbsp;&nbsp;&nbsp;<span>More Options</span></td>
												            </tr>
												            <tr id="trMoreOptions" style="display: none">
													            <td style="width: 188px; border: solid 1px #92AEC9;">
													            <table cellpadding = "4" cellspacing="2">
													                <tr><td>
													                Saved Searches:
													                </td></tr>
													                <tr><td valign="middle" style="background-color:White;">
													                <asp:dropdownlist id="cboSavedSearch" runat="server" CssClass="ms-formdropdown" Width="160px" OnSelectedIndexChanged="cboSavedSearch_SelectedIndexChanged" AutoPostBack="True">
														            </asp:dropdownlist>
														            <br/>
                                                                    <asp:Button ID="btnDelete" runat="server" CssClass="ms-button" Height="22px" Text="Delete"
                                                                        Width="44%" OnClick="btnDelete_Click"/>
													                </td></tr>
													                <tr><td>
													                </td></tr>
													            </table>
                                                                    <asp:ValidationSummary ID="vsSave" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                        ValidationGroup="SaveAs" />
                                                                    <asp:RequiredFieldValidator ID="rfvSaveName" runat="server" ControlToValidate="hdnSaveAs"
                                                                        Display="None" ErrorMessage="Please input Save Name.   " ValidationGroup="SaveAs" Enabled="False">Please input Save Name.   </asp:RequiredFieldValidator></td>
												            </tr>
											            </table>
        <input id="hdnIsPostBack" runat="server" type="hidden" style="display: none" value="false" />
        <input id="hdnProcessFilter" runat="server" type="hidden" style="display: none" />
        <input id="hdnMoreOptions" runat="server" type="hidden" style="display: none" />
        <input id="hdnSearchOptions" runat="server" type="hidden" style="display: none" />
        <input id="hdnSaveAs" runat="server" style="display: none" />
    </form>
</body>
</html>

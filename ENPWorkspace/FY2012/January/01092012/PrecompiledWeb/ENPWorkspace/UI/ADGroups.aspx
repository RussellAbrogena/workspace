<%@ page language="C#" autoeventwireup="true" inherits="UI_ADGroups, App_Web_adgroups.aspx.76ff77b3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
    <title>Untitled Page</title>
</head>
<body onload=">
    <form id="form1" runat="server">
        Get AD Groups<br />
        &nbsp;<asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get" />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        member of &nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>&nbsp;<asp:Button
            ID="Button2" runat="server" OnClick="Button2_Click" Text="Find" />
        <asp:Label ID="Label2" runat="server"></asp:Label><br />
        <br />
        <br />
        <br />
        Get Tree<br />
        User<br />
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
        Group<br />
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Get Tree" />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>

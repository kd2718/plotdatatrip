<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="SoftwareDogs.Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        #Button1
        {
            text-align: center;
        }
        </style>
</head>
<body bgcolor="#A1DAEC">
    <form id="form1" runat="server">
    <p class="style1">
        <asp:Image ID="imgHeader" runat="server" ImageUrl="~/Pixes/SioftwareDogs.jpg" />
    </p>
    <p class="style1">
        <asp:Label ID="lblLoginScreen" runat="server" Font-Bold="True" 
            Font-Size="XX-Large" Text="Main Menu"></asp:Label>
    </p>
    <div align="center">
        <asp:Button ID="btnGraph" runat="server" Text="Graph Data" 
            onclick="btnGraph_Click" />
        <br />
&nbsp;</div>
    <div align="center">
        <asp:Button ID="Button1" runat="server" Text="View Or Edit Data" 
            onclick="btnData_Click" />
        <br />
&nbsp;</div>
    <div align="center">
        <asp:Button ID="btnDepartment" runat="server" Text="Enter Departments / Households" 
            onclick="btnDepartment_Click" />
        <br />
&nbsp;</div>
    <div align="center">
        <asp:Button ID="btnUnit" runat="server" Text="Manage Remote Units" 
            onclick="btnUnit_Click" />
        <br />
&nbsp;
    </div>
    <div align="center">
        <asp:Button ID="btnSensor" runat="server" Text="Manage Types of Installed Sensors" 
            onclick="btnSensor_Click" />
        <br />
&nbsp;</div>
    <div align="center">
        <asp:Button ID="btnChange" runat="server" Text="Change Login Name Or Password" 
            onclick="btnChange_Click" />
        <br />
&nbsp;</div>
    <div align="center">
        <asp:Button ID="btnLogout" runat="server" Text="Logout" 
            onclick="btnLogOut_Click"/>
    </div>
    </form>
</body>
</html>

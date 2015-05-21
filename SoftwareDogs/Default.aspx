<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SoftwareDogs._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
        <asp:Image ID="imgHeader" runat="server" ImageUrl="~/Pixes/SioftwareDogs.jpg" 
            TabIndex="99" />
    </p>
    <p class="style1">
        &nbsp;</p>
    <p class="style1">
        <asp:Label ID="lblLoginScreen" runat="server" Font-Bold="True" 
            Font-Size="XX-Large" Text="Login Screen" TabIndex="98"></asp:Label>
    </p>
    <p class="style1">
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblName" runat="server" Font-Size="Medium" Text="User Name:"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtName" runat="server" TabIndex="1"></asp:TextBox>
    &nbsp;
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="X-Large" 
            ForeColor="#CC3300" Text="*"></asp:Label>
    </p>
    <p class="style1">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
        &nbsp;&nbsp;         
        <asp:TextBox ID="txtPassword" runat="server" 
            TextMode="Password" Width="129px" TabIndex="2"></asp:TextBox>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="X-Large" 
            ForeColor="#CC3300" Text="*"></asp:Label>
    </p>
    <div align="center">
    
        <asp:Button ID="btnLogin" runat="server" Text="Log In" 
            onclick="btnLogin_Click" TabIndex="3" />
    
    </div>
    </form>
</body>
</html>


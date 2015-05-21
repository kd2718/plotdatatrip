<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SoftwareDogs.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <p class="style1">
        <asp:Image ID="imgHeader" runat="server" ImageUrl="~/Pixes/SioftwareDogs.jpg" />
    </p>
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
            width: 100%;
        }
        .style2
        {
            text-align: right;
        }
    </style>
</head>
<body bgcolor="#A1DAEC">
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td colspan="2" 
                    style="text-align: center; font-size: x-large; font-weight: 700">
    
                    <span class="style1">Change Password</span></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    New User Name:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    New Password:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Confirm Password:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtConfirm" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Button ID="btnChange" runat="server" Text="Change" 
                        onclick="btnChange_Click" />
                    &nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td>
                    &nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="btnAbort" runat="server" 
                        Text="Abort" onclick="btnAbort_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

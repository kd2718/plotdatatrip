﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sensor.aspx.cs" Inherits="SoftwareDogs.AddSensor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <p class="style1" align="center">
        <asp:Image ID="imgHeader" runat="server" ImageUrl="~/Pixes/SioftwareDogs.jpg" />
    </p>
   <title>Untitled Page</title>
    <style type="text/css">

        .style3
        {
            text-align: center;
            font-size: xx-large;
        }
    
        .style1
        {
        }
        </style>
</head>
<body bgcolor="#A1DAEC">
    <form id="form1" runat="server">
    <div>
    
    <p class="style3">
        Sensor Types</p>
    <p class="style1" align="center">
        &nbsp;&nbsp;
        <asp:GridView ID="gData" runat="server" AllowPaging="True" AllowSorting="True" 
            AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" DataKeyNames="iTypeKey,acName,iTypeNumber,Deletable" 
            onrowdeleting="gData_RowDeleting" onrowediting="gData_RowEditing" 
            onselectedindexchanged="gData_SelectedIndexChanged" 
            onsorting="gData_Sorting" AutoGenerateColumns="False" 
            HorizontalAlign="Center" onrowcancelingedit="gData_RowCancelingEdit" 
            onrowdeleted="gData_RowDeleted" onrowupdated="gData_RowUpdated" 
            onrowupdating="gData_RowUpdating" 
            onselectedindexchanging="gData_SelectedIndexChanging" onsorted="gData_Sorted" 
            onunload="gData_Unload" ShowFooter="True" 
            ondatabinding="gData_DataBinding" ondatabound="gData_DataBound" 
            ondisposed="gData_Disposed" oninit="gData_Init" onload="gData_Load" 
            onpageindexchanged="gData_PageIndexChanged" 
            onpageindexchanging="gData_PageIndexChanging" onprerender="RalphIt" 
            onrowcommand="gData_RowCommand" onrowcreated="gData_RowCreated" 
            onrowdatabound="gData_RowDataBound">
            <Columns>
                <asp:BoundField DataField="iTypeKey" HeaderText="Type Key" />
                <asp:BoundField DataField="acName" HeaderText="Name" />
                <asp:BoundField DataField="iTypeNumber" HeaderText="Type Number" />
                <asp:BoundField DataField="Deletable" HeaderText="Deletable" />
            </Columns>
            <AlternatingRowStyle BackColor="#FFCCCC" BorderStyle="Double" />
        </asp:GridView>
    </p>
    <div align="center">
    
        <asp:Button ID="btnAdd" runat="server" Text="Add a Blank Row" 
            onclick="btnAdd_Click" />
    
    &nbsp;&nbsp;
    
        <asp:Button ID="btnDone" runat="server" Text="Done" onclick="btnDone_Click" />
    
    </div>
    <div align="center">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    
    </div>
    </form>
</body>
</html>

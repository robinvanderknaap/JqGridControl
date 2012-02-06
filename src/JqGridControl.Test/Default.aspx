<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="JqGridControl.Test._Default" %>

<%@ Register Assembly="JqGridControl" Namespace="JqGridControl" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <h2>Basic example</h2>
    
    <asp:JqGrid ID="JqGridControl1" runat="server" Url="Default.aspx/GetGridData" PagingEnabled="true">
        <Columns>   
            <asp:JqGridColumn HeaderText="Id" DataField="Id" />              
            <asp:JqGridColumn HeaderText="Firstname" DataField="Firstname" />
            <asp:JqGridColumn HeaderText="Lastname" DataField="Lastname" />
            <asp:JqGridColumn HeaderText="Email" DataField="Email" />
            <asp:JqGridColumn HeaderText="City" DataField="City" />
            <asp:JqGridColumn HeaderText="Date Of Birth" DataField="DateOfBirth" />
        </Columns>
    </asp:JqGrid>

    <h2>Source</h2>
    <pre>&lt;asp:JqGrid ID="JqGrid1" runat="server" Url="Default.aspx/GetGridData" PagingEnabled="true">
    &lt;Columns>   
        &lt;asp:JqGridColumn HeaderText="Id" DataField="Id" />              
        &lt;asp:JqGridColumn HeaderText="Firstname" DataField="Firstname" />
        &lt;asp:JqGridColumn HeaderText="Lastname" DataField="Lastname" />
        &lt;asp:JqGridColumn HeaderText="Email" DataField="Email" />
        &lt;asp:JqGridColumn HeaderText="City" DataField="City" />
        &lt;asp:JqGridColumn HeaderText="Date Of Birth" DataField="DateOfBirth" />
    &lt;/Columns>
&lt;/asp:JqGrid></pre>
</asp:Content>

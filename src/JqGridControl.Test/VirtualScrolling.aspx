<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VirtualScrolling.aspx.cs" Inherits="JqGridControl.Test.VirtualScrolling" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:JqGrid ID="JqGridControl1" runat="server" Url="TestData/Data.aspx/GetGridData" PagingEnabled="true" 
        ToolbarSearchEnabled="true" Title="Toolbar Search Example" RowNumber="50" ViewRecords="true" VirtualScroll="true" Height="300">
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
    <pre>&lt;asp:JqGrid ID="JqGridControl1" runat="server" Url="ToolbarSearch.aspx/GetGridData" PagingEnabled="true" 
        ToolbarSearchEnabled="true" Title="Toolbar Search Example" RowNumber="15" ViewRecords="true">
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
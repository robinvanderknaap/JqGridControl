<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="JqGridControl.Test.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <h2>About JqGrid Control</h2>
    
    <p>JqGrid Control provides an easy way to integrate JqGrid with ASP.NET WebForms. </p>

    <h2>Contributors</h2>

    <ul id="contributors">
        <li>
            <a href="http://www.webpirates.nl/robin-van-der-knaap">
                <img src="http://www.gravatar.com/avatar/1a348e78f5b686f2f109cf733ea8bec7?s=80" alt="Robin van der Knaap" />
                <span>Robin van der Knaap</span>
            </a>
        </li>
        <li>
            <a href="http://www.webpirates.nl/daan-le-duc">
                <img src="http://www.gravatar.com/avatar/df56bcdb988c537e2852b95ffc0a164d?s=80" alt="Daan le Duc" />
                <span>Daan le Duc</span>
            </a>
        </li>
    </ul>
</asp:Content>

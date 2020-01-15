<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminSite._Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1><%=GetLocalResourceObject("administrationApplication") %></h1>
        <br />
        <h2><%=GetLocalResourceObject("adminInfoMessage") %></h2>
        <br />
        <br />
    </div>

</asp:Content>

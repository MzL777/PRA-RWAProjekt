<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AdminSite.Error" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <br />
    <br />
    <br />
    <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="20px" ForeColor="Red" CssClass="center" meta:resourcekey="lblInfoResource1" />
    <br />
</asp:Content>

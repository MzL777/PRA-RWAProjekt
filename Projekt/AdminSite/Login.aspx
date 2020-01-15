<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AdminSite.Login" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <br />
    <hr />
    <br />

    <div class="outer">
        <div class="container container-center">
            <asp:Label ID="Label1" runat="server" Text="Korisničko ime:" meta:resourcekey="Label1Resource1"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control input-md" meta:resourcekey="txtUsernameResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ErrorMessage="Korisničko ime je obavezno" Display="None" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>

            <br />
            <asp:Label ID="Label3" runat="server" Text="Lozinka:" meta:resourcekey="Label3Resource1"></asp:Label>
            <br />

            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control input-md" meta:resourcekey="txtPassResource1" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass" ErrorMessage="Lozinka je obavezna" Display="None" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" meta:resourcekey="ValidationSummary1Resource1" />
            <br />
            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" CssClass="btn btn-danger button-fill" meta:resourcekey="btnLoginResource1" />
        </div>
        <br />
        <br />
    </div>
</asp:Content>

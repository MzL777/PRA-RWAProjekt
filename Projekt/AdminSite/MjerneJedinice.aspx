<%@ Page Title="Mjerne Jedinice" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MjerneJedinice.aspx.cs" Inherits="AdminSite.MjerneJedinice" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <br />
    <hr />
    <br />

    <asp:GridView ID="gvMjerneJedinice" runat="server" ForeColor="DarkRed" GridLines="None" AutoGenerateColumns="False" DataKeyNames="IDMjerneJedinice" ShowFooter="True" ShowHeaderWhenEmpty="True"
        OnRowEditing="gvMjerneJedinice_RowEditing" OnRowCancelingEdit="gvMjerneJedinice_RowCancelingEdit" OnRowUpdating="gvMjerneJedinice_RowUpdating" OnRowDeleting="gvMjerneJedinice_RowDeleting" meta:resourcekey="gvMjerneJediniceResource1">
        <Columns>
            <asp:TemplateField HeaderText="NAZIV" meta:resourcekey="TemplateFieldResource1">
                <EditItemTemplate>
                    <asp:TextBox ID="txtNaziv" runat="server" Text='<%# Bind("Naziv") %>' meta:resourcekey="txtNazivResource1"></asp:TextBox>

                <asp:RequiredFieldValidator ValidationGroup="vgMjerneJediniceEdit" ControlToValidate="txtNaziv" runat="server" ErrorMessage="Naziv mjerne jedinice ne može biti prazan!" Display="None" meta:resourcekey="RequiredFieldValidatorResource1"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbNaziv" runat="server" Text='<%# Bind("Naziv") %>' meta:resourcekey="lbNazivResource1"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNazivNove" runat="server" CssClass="footer-textbox" meta:resourcekey="txtNazivNoveResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ValidationGroup="vgMjerneJediniceAdd" ControlToValidate="txtNazivNove" runat="server" ErrorMessage="Naziv nove mjerne jedinice ne može biti prazan!" Display="None" meta:resourcekey="RequiredFieldValidatorResource2"></asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                <ItemTemplate>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnDodaj" Text="Dodaj" runat="server" OnClick="btnDodaj_Click" ValidationGroup="vgMjerneJediniceAdd" CssClass="btn btn-sm btn-danger" meta:resourcekey="btnDodajResource1" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:CommandField CancelText="Odustani" EditText="Izmijeni" UpdateText="Spremi" ShowEditButton="true" DeleteText="Obriši" ShowDeleteButton="true" ValidationGroup="vgMjerneJediniceEdit" meta:resourcekey="CommandFieldResource1" />
        </Columns>

        <AlternatingRowStyle BackColor="#ffdddd" />
        <EditRowStyle BackColor="Red" ForeColor="Black" />
        <FooterStyle BackColor="#550000" Font-Bold="True" ForeColor="Black" />
        <HeaderStyle BackColor="#550000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" />
        <SelectedRowStyle BackColor="#ccaaaa" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>

    <br />
    <asp:ValidationSummary ValidationGroup="vgMjerneJediniceAdd" runat="server" ForeColor="Red" meta:resourcekey="ValidationSummaryResource1" />
    <asp:ValidationSummary ValidationGroup="vgMjerneJediniceEdit" runat="server" ForeColor="Red" meta:resourcekey="ValidationSummaryResource2" />


    <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Red" meta:resourcekey="lblInfoResource1"></asp:Label>
</asp:Content>

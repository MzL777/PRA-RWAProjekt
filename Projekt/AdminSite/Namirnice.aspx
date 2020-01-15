<%@ Page Title="Namirnice" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Namirnice.aspx.cs" Inherits="AdminSite.Namirnice" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <br />
    <hr />
    <br />

    <asp:GridView ID="gvNamirnice" runat="server" ForeColor="DarkRed" GridLines="None" AutoGenerateColumns="False" DataKeyNames="IDNamirnice" ShowFooter="True" ShowHeaderWhenEmpty="True"
        OnRowEditing="gvNamirnice_RowEditing" OnRowCancelingEdit="gvNamirnice_RowCancelingEdit" OnRowUpdating="gvNamirnice_RowUpdating" OnRowDeleting="gvNamirnice_RowDeleting" OnSelectedIndexChanged="gvNamirnice_SelectedIndexChanged" meta:resourcekey="gvNamirniceResource1">
        <Columns>
            <asp:TemplateField HeaderText="NAZIV" meta:resourcekey="TemplateFieldResource1">
                <EditItemTemplate>
                    <asp:TextBox ID="txtNaziv" runat="server" Text='<%# Bind("Naziv") %>' meta:resourcekey="txtNazivResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ValidationGroup="vgNamirniceEdit" ControlToValidate="txtNaziv" runat="server" ErrorMessage="Naziv namirnice ne može biti prazan!" Display="None" meta:resourcekey="RequiredFieldValidatorResource1"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbNaziv" runat="server" Text='<%# Bind("Naziv") %>' meta:resourcekey="lbNazivResource1"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNazivNove" runat="server" CssClass="footer-textbox" meta:resourcekey="txtNazivNoveResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ValidationGroup="vgNamirniceAdd" ControlToValidate="txtNazivNove" runat="server" ErrorMessage="Naziv nove namirnice ne može biti prazan!" Display="None" meta:resourcekey="RequiredFieldValidatorResource2"></asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="TIP NAMIRNICE" meta:resourcekey="TemplateFieldResource2">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlTipoviNamirnica" runat="server" CssClass="dropdown" meta:resourcekey="ddlTipoviNamirnicaResource1"></asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbTipNamirnice" runat="server" Text='<%# GetTipNamirnice(Eval("TipNamirniceID").ToString()) %>' meta:resourcekey="lbTipNamirniceResource1"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList runat="server" ID="ddlTipoviNamirniceNove" CssClass="dropdown" meta:resourcekey="ddlTipoviNamirniceNoveResource1"></asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="KCAL/GRAM" meta:resourcekey="TemplateFieldResource3">
                <EditItemTemplate>
                    <asp:TextBox ID="txtKcalPoGramu" runat="server" Text='<%# Bind("EnergetskaVrijednostKcalPoGramu") %>' meta:resourcekey="txtKcalPoGramuResource1"></asp:TextBox>

                    <asp:RegularExpressionValidator ValidationGroup="vgNamirniceEdit" ControlToValidate="txtKcalPoGramu" runat="server" ErrorMessage="Iznos Kcal/g nove namirnice mora biti brojčana vrijednost!" Display="None" ValidationExpression="^\d+([,\.]\d+)?$" meta:resourcekey="RegularExpressionValidatorResource1"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ValidationGroup="vgNamirniceEdit" Display="None" ControlToValidate="txtKcalPoGramu" runat="server" ErrorMessage="Iznos Kcal/g ne može biti prazan!" meta:resourcekey="RequiredFieldValidatorResource3"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbKcalPoGramu" CssClass="center" runat="server" Text='<%# Bind("EnergetskaVrijednostKcalPoGramu") %>' meta:resourcekey="lbKcalPoGramuResource1"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtKcalPoGramuNove" runat="server" CssClass="number" placeholder="kcal/g" meta:resourcekey="txtKcalPoGramuNoveResource1"></asp:TextBox>

                    <asp:RegularExpressionValidator ValidationGroup="vgNamirniceAdd" ControlToValidate="txtKcalPoGramuNove" runat="server" ErrorMessage="Iznos Kcal/g nove namirnice mora biti brojčana vrijednost!" Display="None" ValidationExpression="^\d+([,\.]\d+)?$" meta:resourcekey="RegularExpressionValidatorResource2"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ValidationGroup="vgNamirniceAdd" Display="None" ControlToValidate="txtKcalPoGramuNove" runat="server" ErrorMessage="Iznos Kcal/g ne može biti prazan!" meta:resourcekey="RequiredFieldValidatorResource4"></asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="KJ/GRAM" meta:resourcekey="TemplateFieldResource4">
                <EditItemTemplate>
                    <asp:TextBox ID="txtKJPoGramu" runat="server" Text='<%# Bind("EnergetskaVrijednostKJPoGramu") %>' meta:resourcekey="txtKJPoGramuResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ValidationGroup="vgNamirniceEdit" Display="None" ControlToValidate="txtKJPoGramu" runat="server" ErrorMessage="Iznos KJ/g ne može biti prazan!" meta:resourcekey="RequiredFieldValidatorResource5"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="vgNamirniceEdit" ControlToValidate="txtKJPoGramu" runat="server"
                        ErrorMessage="Iznos KJ/g nove namirnice mora biti brojčana vrijednost!" Display="None" ValidationExpression="^\d+([,\.]\d{1,2})?$" meta:resourcekey="RegularExpressionValidatorResource3"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbKJPoGramu" CssClass="center" runat="server" Text='<%# Bind("EnergetskaVrijednostKJPoGramu") %>' meta:resourcekey="lbKJPoGramuResource1"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtKJPoGramuNove" runat="server" CssClass="number" placeholder="kJ/g" meta:resourcekey="txtKJPoGramuNoveResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ValidationGroup="vgNamirniceAdd" Display="None" ControlToValidate="txtKJPoGramuNove" runat="server" ErrorMessage="Iznos KJ/g ne može biti prazan!" meta:resourcekey="RequiredFieldValidatorResource6"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="vgNamirniceAdd" ControlToValidate="txtKJPoGramuNove" runat="server"
                        ErrorMessage="Iznos KJ/g nove namirnice mora biti brojčana vrijednost!" Display="None" ValidationExpression="^\d+([,\.]\d{1,2})?$" meta:resourcekey="RegularExpressionValidatorResource4"></asp:RegularExpressionValidator>
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                <ItemTemplate>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnDodaj" Text="Dodaj" runat="server" OnClick="btnDodaj_Click" CssClass="btn btn-sm btn-danger" ValidationGroup="vgNamirniceAdd" meta:resourcekey="btnDodajResource1" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:CommandField CancelText="Odustani" EditText="Izmijeni" UpdateText="Spremi" ShowEditButton="true" DeleteText="Obriši" ShowDeleteButton="true" ShowSelectButton="true" SelectText="Označi" ValidationGroup="vgNamirniceEdit" meta:resourcekey="CommandFieldResource1" />
        </Columns>

        <AlternatingRowStyle BackColor="#ffdddd" />
        <EditRowStyle BackColor="Red" Font-Bold="true" ForeColor="Black" />
        <FooterStyle BackColor="#550000" Font-Bold="True" ForeColor="Black" />
        <HeaderStyle BackColor="#550000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" />
        <SelectedRowStyle BackColor="#ccaaaa" Font-Bold="True" ForeColor="Red" />
    </asp:GridView>

    <asp:ValidationSummary ValidationGroup="vgNamirniceEdit" runat="server" ForeColor="Red" meta:resourcekey="ValidationSummaryResource1" />
    <asp:ValidationSummary ValidationGroup="vgNamirniceAdd" runat="server" ForeColor="Red" meta:resourcekey="ValidationSummaryResource2" />

    <br />
    <br />
    <hr />



    <asp:Label ID="lblNamirnica" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Red" CssClass="padding-20 inner" meta:resourcekey="lblNamirnicaResource1"></asp:Label>
    <asp:GridView ID="gvMjerneJediniceZaNamirnicu" name="MjerneJedinice" runat="server" ForeColor="DarkRed" GridLines="None" AutoGenerateColumns="False" DataKeyNames="IDMjerneJedinice" ShowFooter="True" ShowHeaderWhenEmpty="True" OnRowDeleting="gvMjerneJediniceZaNamirnicu_RowDeleting" OnRowEditing="gvMjerneJediniceZaNamirnicu_RowEditing" OnRowCancelingEdit="gvMjerneJediniceZaNamirnicu_RowCancelingEdit" OnRowUpdating="gvMjerneJediniceZaNamirnicu_RowUpdating" meta:resourcekey="gvMjerneJediniceZaNamirnicuResource1">
        <Columns>
            <asp:TemplateField HeaderText="MJERNA JEDINICA" meta:resourcekey="TemplateFieldResource6">
                <EditItemTemplate>
                    <asp:Label ID="lbMjernaJedinica" runat="server" Text='<%# Bind("Naziv") %>' meta:resourcekey="lbMjernaJedinicaResource1"></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbMjernaJedinica" runat="server" Text='<%# Bind("Naziv") %>' meta:resourcekey="lbMjernaJedinicaResource2"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList runat="server" ID="ddlMjernaJedinicaNove" CssClass="dropdown" meta:resourcekey="ddlMjernaJedinicaNoveResource1"></asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="TEŽINA (GRAMA)" meta:resourcekey="TemplateFieldResource7">
                <EditItemTemplate>
                    <asp:TextBox ID="txtTezinaGrama" runat="server" CssClass="number" Text='<%# Bind("TezinaUGramima") %>' meta:resourcekey="txtTezinaGramaResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ValidationGroup="vgMjerneJediniceEdit" Display="None" ControlToValidate="txtTezinaGrama" runat="server" ErrorMessage="Težina u gramima ne može biti prazna!" meta:resourcekey="RequiredFieldValidatorResource7"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="vgMjerneJediniceEdit" ControlToValidate="txtTezinaGrama" runat="server" ErrorMessage="Težina u gramima mora biti cijelobrojna vrijednost!" Display="None" ValidationExpression="^\d+$" meta:resourcekey="RegularExpressionValidatorResource5"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTezinaGrama" CssClass="center" runat="server" Text='<%# Bind("TezinaUGramima") %>' meta:resourcekey="lblTezinaGramaResource1"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTezinaGramaNove" runat="server" CssClass="number" meta:resourcekey="txtTezinaGramaNoveResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ValidationGroup="vgMjerneJediniceAdd" Display="None" ControlToValidate="txtTezinaGramaNove" runat="server" ErrorMessage="Težina u gramima ne može biti prazna!" meta:resourcekey="RequiredFieldValidatorResource8"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="vgMjerneJediniceAdd" ControlToValidate="txtTezinaGramaNove" runat="server"
                        ErrorMessage="Težina u gramima mora biti cijelobrojna vrijednost!" Display="None" ValidationExpression="^\d+$" meta:resourcekey="RegularExpressionValidatorResource6"></asp:RegularExpressionValidator>
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                <ItemTemplate>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnDodajMjernuJedinicu" Text="Dodaj" runat="server" OnClick="btnDodajMjernuJedinicu_Click" ValidationGroup="vgMjerneJediniceAdd" CssClass="btn btn-sm btn-danger" meta:resourcekey="btnDodajMjernuJedinicuResource1" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:CommandField CancelText="Odustani" UpdateText="Spremi" DeleteText="Obriši" ShowDeleteButton="true" ShowEditButton="true" EditText="Izmijeni" ValidationGroup="vgMjerneJediniceEdit" meta:resourcekey="CommandFieldResource2" />
        </Columns>

        <AlternatingRowStyle BackColor="#ffdddd" />
        <EditRowStyle BackColor="Red" ForeColor="Black" Font-Bold="true" />
        <FooterStyle BackColor="#550000" Font-Bold="True" ForeColor="Black" />
        <HeaderStyle BackColor="#550000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" />
        <SelectedRowStyle BackColor="#ccaaaa" Font-Bold="True" ForeColor="Red" />
    </asp:GridView>

    <br />
    <asp:ValidationSummary runat="server" ForeColor="Red" ValidationGroup="vgMjerneJediniceAdd" meta:resourcekey="ValidationSummaryResource3" />
    <asp:ValidationSummary runat="server" ForeColor="Red" ValidationGroup="vgMjerneJediniceEdit" meta:resourcekey="ValidationSummaryResource4" />


    <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Red" CssClass="inner" meta:resourcekey="lblInfoResource1"></asp:Label>
</asp:Content>

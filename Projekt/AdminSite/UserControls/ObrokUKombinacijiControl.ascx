<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObrokUKombinacijiControl.ascx.cs" Inherits="AdminSite.UserControls.ObrokUKombinacijiControl" %>

<div class="obrokDiv">
    <table class="obrokTable">
        <tr>
            <td>
                <asp:TextBox ID="txtNazivObroka" runat="server" meta:resourcekey="txtNazivObrokaResource1" />
                <asp:DropDownList ID="ddlNazivObroka" runat="server" CssClass="dropdown" Visible="False" meta:resourcekey="ddlNazivObrokaResource1">
                </asp:DropDownList>
            </td>
            <td class="number">
                <asp:TextBox ID="txtUdioBjelancevina" runat="server" meta:resourcekey="txtUdioBjelancevinaResource1" />

                <asp:RangeValidator ID="rangeval1"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioBjelancevina"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio bjelančevina mora biti vrijednost između 0 i 100!"
                    MaximumValue="100" MinimumValue="0" Type="Integer" meta:resourcekey="rangeval1Resource1"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="reqval1"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioBjelancevina"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio bjelančevina ne može biti prazan!" meta:resourcekey="reqval1Resource1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regexval1"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioBjelancevina"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio bjelančevina mora biti cijelobrojna vrijednost!"
                    ValidationExpression="^\d+$" meta:resourcekey="regexval1Resource1"></asp:RegularExpressionValidator>
            </td>
            <td class="number">
                <asp:TextBox ID="txtUdioMasti" runat="server" meta:resourcekey="txtUdioMastiResource1" />

                <asp:RangeValidator ID="rangeval2"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioMasti"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio masti mora biti vrijednost između 0 i 100!"
                    MaximumValue="100" MinimumValue="0" Type="Integer" meta:resourcekey="rangeval2Resource1"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="reqval2"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioMasti"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio masti ne može biti prazan!" meta:resourcekey="reqval2Resource1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regexval2"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioMasti"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio masti mora biti cijelobrojna vrijednost!"
                    ValidationExpression="^\d+$" meta:resourcekey="regexval2Resource1"></asp:RegularExpressionValidator>
            </td>
            <td class="number">
                <asp:TextBox ID="txtUdioUgljikohidrata" runat="server" meta:resourcekey="txtUdioUgljikohidrataResource1" />

                <asp:RangeValidator ID="rangeval3"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioUgljikohidrata"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio ugljikohidrata mora biti vrijednost između 0 i 100!"
                    MaximumValue="100" MinimumValue="0" Type="Integer" meta:resourcekey="rangeval3Resource1"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="reqval3"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioUgljikohidrata"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio ugljikohidrata ne može biti prazan!" meta:resourcekey="reqval3Resource1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regexval3"
                    runat="server" Display="None"
                    ControlToValidate="txtUdioUgljikohidrata"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Udio ugljikohidrata mora biti cijelobrojna vrijednost!"
                    ValidationExpression="^\d+$" meta:resourcekey="regexval3Resource1"></asp:RegularExpressionValidator>
            </td>
            <td class="number">
                <asp:TextBox ID="txtDnevniUdio" runat="server" meta:resourcekey="txtDnevniUdioResource1" />

                <asp:RangeValidator ID="rangeval4"
                    runat="server" Display="None"
                    ControlToValidate="txtDnevniUdio"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Dnevni udio energije mora biti vrijednost između 0 i 100!"
                    MaximumValue="100" MinimumValue="0" Type="Integer" meta:resourcekey="rangeval4Resource1"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="reqval4"
                    runat="server" Display="None"
                    ControlToValidate="txtDnevniUdio"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Dnevni udio energije ne može biti prazan!" meta:resourcekey="reqval4Resource1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regexval4"
                    runat="server" Display="None"
                    ControlToValidate="txtDnevniUdio"
                    ValidationGroup="vgObroci"
                    ErrorMessage="Dnevni udio energije mora biti cijelobrojna vrijednost!"
                    ValidationExpression="^\d+$" meta:resourcekey="regexval4Resource1"></asp:RegularExpressionValidator>
            </td>
        </tr>
    </table>

    <asp:CustomValidator ID="CustomValidator1"
        runat="server" Display="None"
        ValidationGroup="vgObroci"
        ErrorMessage="Suma vrijednosti udjela bjelančevina, masti i ugljikohidrata mora biti jednaka 100!"
        ClientValidationFunction="ValidateSum" meta:resourcekey="CustomValidator1Resource1"></asp:CustomValidator>
</div>
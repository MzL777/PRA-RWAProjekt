<%@ Page Title="Kombinacije obroka" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kombinacije.aspx.cs" Inherits="AdminSite.Kombinacije" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Title %></h2>
    <br />
    <hr />
    <br />

    <div class="kombinacijeContainer outer">
        <div class="inner">
            <asp:Label Text="Broj obroka:" runat="server" CssClass="move-right" meta:resourcekey="LabelResource1" />
            <asp:DropDownList CssClass="dropdown number" ID="ddlBrojObroka" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrojObroka_SelectedIndexChanged" meta:resourcekey="ddlBrojObrokaResource1">
                <asp:ListItem Text="2" meta:resourcekey="ListItemResource1" />
                <asp:ListItem Text="3" Selected="True" meta:resourcekey="ListItemResource2" />
                <asp:ListItem Text="4" meta:resourcekey="ListItemResource3" />
                <asp:ListItem Text="5" meta:resourcekey="ListItemResource4" />
                <asp:ListItem Text="6" meta:resourcekey="ListItemResource5" />
                <asp:ListItem Text="7" meta:resourcekey="ListItemResource6" />
                <asp:ListItem Text="8" meta:resourcekey="ListItemResource7" />
                <asp:ListItem Text="9" meta:resourcekey="ListItemResource8" />
            </asp:DropDownList>

            <br />
            <br />
            <asp:HiddenField ID="kombinacijaIDhidden" runat="server" Value="0" />

            <asp:Panel ID="phObroci" runat="server" meta:resourcekey="phObrociResource1"></asp:Panel>

            <asp:CustomValidator ID="CustomValidator2"
                runat="server" Display="None"
                ValidationGroup="vgObroci"
                ErrorMessage="Suma vrijednosti dnevnih udjela mora biti jednaka 100!"
                ClientValidationFunction="ValidateSumDnevno" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
            <br />
            <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Red" CssClass="move-right" meta:resourcekey="lblInfoResource1"></asp:Label>
            <br />
            <br />
            <p class="move-right"><%=GetLocalResourceObject("validFrom") %></p>
            <asp:TextBox runat="server" ID="txtDateValidFrom" ReadOnly="True" CssClass="dropdown move-right" meta:resourcekey="txtDateValidFromResource1" />
            <asp:RequiredFieldValidator ID="reqvaldate1"
                runat="server" Display="None"
                ControlToValidate="txtDateValidFrom"
                ValidationGroup="vgObroci"
                ErrorMessage="Datum od kad kombinacija vrijedi ne može biti prazan!" meta:resourcekey="reqvaldate1Resource1"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="dateValidator1"
                runat="server" Display="None"
                Type="Date"
                Operator="DataTypeCheck"
                ControlToValidate="txtDateValidFrom"
                ValidationGroup="vgObroci"
                ErrorMessage="Datum od kad kombinacija vrijedi nije u ispravnom formatu!" meta:resourcekey="dateValidator1Resource1"></asp:CompareValidator>

            <p class="move-right"><%=GetLocalResourceObject("validTo") %></p>
            <asp:TextBox runat="server" ID="txtDateValidTo" ReadOnly="True" CssClass="dropdown move-right" meta:resourcekey="txtDateValidToResource1" />
            <asp:CompareValidator ID="dateValidator2"
                runat="server" Display="None"
                Type="Date"
                Operator="DataTypeCheck"
                ControlToValidate="txtDateValidTo"
                ValidationGroup="vgObroci"
                ErrorMessage="Datum do kad kombinacija vrijedi nije u ispravnom formatu!" meta:resourcekey="dateValidator2Resource1"></asp:CompareValidator>

            <asp:Button ID="btnDisable" Enabled="False" Text="Onemogući" runat="server" OnClick="btnDisable_Click" CssClass="btn btn-sm btn-danger dropdown" meta:resourcekey="btnDisableResource1" />
            <asp:Button ID="btnCancel" Text="Odustani" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-sm btn-danger dropdown" meta:resourcekey="btnCancelResource1" />
            <asp:Button ID="btnSave" Enabled="False" Text="Spremi" runat="server" OnClick="btnSave_Click" CssClass="btn btn-sm btn-danger dropdown" ValidationGroup="vgObroci" UseSubmitBehavior="False" meta:resourcekey="btnSaveResource1" />

            <br />
            <br />

            <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="vgObroci" runat="server" ForeColor="Red" meta:resourcekey="ValidationSummary1Resource1" />
        </div>
    </div>

    <script>

        $(function () {
            $("[id*=txtDateValidFrom]").datepicker({
                closeText: "Zatvori",
                prevText: "&#x3C;",
                nextText: "&#x3E;",
                currentText: "Danas",
                monthNames: ["Siječanj", "Veljača", "Ožujak", "Travanj", "Svibanj", "Lipanj",
                    "Srpanj", "Kolovoz", "Rujan", "Listopad", "Studeni", "Prosinac"],
                monthNamesShort: ["Sij", "Velj", "Ožu", "Tra", "Svi", "Lip",
                    "Srp", "Kol", "Ruj", "Lis", "Stu", "Pro"],
                dayNames: ["Nedjelja", "Ponedjeljak", "Utorak", "Srijeda", "Četvrtak", "Petak", "Subota"],
                dayNamesShort: ["Ned", "Pon", "Uto", "Sri", "Čet", "Pet", "Sub"],
                dayNamesMin: ["Ne", "Po", "Ut", "Sr", "Če", "Pe", "Su"],
                weekHeader: "Tje",
                dateFormat: "<%=System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "hr" ? "mm.dd.yy." : "mm/dd/yy" %>",
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: "",
                defaultDate: "+0d",
                changeMonth: true,
                changeYear: true,
                minDate: "+0d",
                nextText: "Sljedeći >",
                prevText: "< Prethodni",
                onClose: function (selectedDate) {
                    $("[id*=txtDateValidTo]").datepicker("option", "minDate", selectedDate);
                }
            });
        });

    </script>
</asp:Content>

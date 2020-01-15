<%@ Page Title="Posjetitelji" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Posjetitelji.aspx.cs" Inherits="AdminSite.Posjetitelji" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <br />
    <hr />
    <br />

    <div class="outer">
        <div class="inner">
            <asp:Button ID="btnSaveToCsv" Enabled="False" Text="Ispiši u datoteku" runat="server" OnClick="btnSaveToCsv_Click" CssClass="btn btn-sm btn-danger dropdown" meta:resourcekey="btnSaveToCsvResource1" />
            <br />
            <br />

            <asp:Repeater ID="rpPosjetitelji" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th><%=GetLocalResourceObject("titleName") %></th>
                            <th><%=GetLocalResourceObject("titleSurname") %></th>
                            <th><%=GetLocalResourceObject("titleEmail") %></th>
                            <th><%=GetLocalResourceObject("titleHeight") %></th>
                            <th><%=GetLocalResourceObject("titleWeight") %></th>
                            <th><%=GetLocalResourceObject("titleDob") %></th>
                            <th><%=GetLocalResourceObject("titleGender") %></th>
                            <th><%=GetLocalResourceObject("titleDiabetesType") %></th>
                            <th><%=GetLocalResourceObject("titlePhysicalActivity") %></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="center">
                        <td><%# Eval("Ime") %></td>
                        <td><%# Eval("Prezime") %></td>
                        <td><a href="mailto:<%# Eval("Email") %>"><%# Eval("Email") %></a></td>
                        <td><%# Eval("Visina").ToString() %></td>
                        <td><%# Eval("Tezina").ToString() %></td>
                        <td><%# (Eval("DatumRodjenja") as DateTime?).Value.ToShortDateString() %></td>
                        <td><%# GetSpol((int)Eval("SpolID")) %></td>
                        <td><%# GetTipDijabetesa((int)Eval("TipDijabetesaID")) %></td>
                        <td><%# GetRazinaFizickeAktivnosti((int)Eval("RazinaFizickeAktivnostiID")) %></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="center" style="background-color: #ffdddd;">
                        <td><%# Eval("Ime") %></td>
                        <td><%# Eval("Prezime") %></td>
                        <td><a href="mailto:<%# Eval("Email") %>"><%# Eval("Email") %></a></td>
                        <td><%# Eval("Visina") %></td>
                        <td><%# Eval("Tezina") %></td>
                        <td><%# (Eval("DatumRodjenja") as DateTime?).Value.ToShortDateString() %></td>
                        <td><%# GetSpol((int)Eval("SpolID")) %></td>
                        <td><%# GetTipDijabetesa((int)Eval("TipDijabetesaID")) %></td>
                        <td><%# GetRazinaFizickeAktivnosti((int)Eval("RazinaFizickeAktivnostiID")) %></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>


            <br />
            <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Red" meta:resourcekey="lblInfoResource1"></asp:Label>
        </div>
    </div>

</asp:Content>

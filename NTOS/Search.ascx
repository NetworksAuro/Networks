<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="NTOS.Search1" %>
<style type="text/css">
    #ucsearch_gvrep {
        border: none !important;
        width: auto !important;
    }
</style>

<div id="divsearch" runat="server" style="display: none">
    <table width="100%">
        <tr>
            <td align="right">Show</td>
            <td align="left">
                <asp:DropDownList ID="drp_showsearch" runat="server" Width="160px">
                </asp:DropDownList></td>
            <td align="right">Settlement Date</td>
            <td align="left">
                <asp:TextBox ID="txtcreatedatesearch" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="cedate1" runat="server" TargetControlID="txtcreatedatesearch" PopupButtonID="txtcreatedatesearch"></ajaxToolkit:CalendarExtender>
            </td>
            <td align="right">Presenter</td>
            <td align="left">
                <asp:DropDownList ID="drp_presentersearch" runat="server"
                    Width="160px">
                </asp:DropDownList></td>
            <td align="right">Venue</td>
            <td align="left">
                <asp:DropDownList ID="drp_venuesearch" runat="server" Width="160px">
                </asp:DropDownList></td>
            <td align="right">Metro City/State</td>
            <td align="left">
                <asp:DropDownList ID="drp_metrosearch" runat="server" Width="160px">
                </asp:DropDownList></td>
            <td align="left">&nbsp;&nbsp;<asp:Button ID="btnsearcheng" runat="server" CausesValidation="false" OnClick="btnsearcheng_Click" Text="Search" CssClass="butt" />

            </td>

        </tr>

    </table>

</div>

<div align="left">
    <asp:Label runat="server" ID="lblsearchhead" Text="" CssClass="lblsearch"></asp:Label>
</div>
<div class="clear" align="center">
    <asp:Label runat="server" ID="lblresults" Text="Please enter criteria and click search button"></asp:Label>
</div>



<asp:GridView ID="gvrep" runat="server" AutoGenerateColumns="true" CssClass="search" GridLines="None" OnRowDataBound="gvrep_RowDataBound">
    <Columns>

        <asp:TemplateField HeaderText="###" HeaderStyle-CssClass="gvHeadersearch" ItemStyle-CssClass="gvContent">
            <ItemTemplate>
                <p><a class="a_link" href="<%#Eval("Link")%>" title="Edit"><%#Eval("[Name]")%></a> </p>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

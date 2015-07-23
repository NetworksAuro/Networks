<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PresenterSearch.aspx.cs" Inherits="NTOS.PresenterSearch" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <%@ Register Src="~/City.ascx" TagName="City" TagPrefix="UC" %>
<%@ Register Src="~/Docx.ascx" TagName="Docx" TagPrefix="UC" %>
<%@ Register Src="~/PresenterContactPerson.ascx" TagName="ConPerson" TagPrefix="UC" %>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <script type="text/javascript">
           function SetPresenterId(source, eventargs) {
               $('#<%=hdnpresenterid.ClientID %>').val(eventargs.get_value());
        }
        function ClearPresenterID() {
            $('#<%=hdnpresenterid.ClientID %>').val("0");
        }
    </script>


    <asp:HiddenField ID="hdnpresenterid" runat="server" />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlpresenter" runat="server">
                <table  cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="contentpadding">
                            <table width="1850px"  cellpadding="0" cellspacing="0" align="left" border="0">
                                <%--<tr>
                                    <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33" colspan="8">
                                        <a href="Presenter.aspx" id="lnknew" runat="server" visible="false" class="txtnew">New</a>
                                        <asp:Label ID="lblhead" runat="server">Create Presenter</asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="8">
                                        <div class="alerttd">
                                            <asp:ValidationSummary ID="vslist" runat="server" DisplayMode="BulletList" HeaderText="Field validations failed! Please check and correct!" CssClass="valsummary" />
                                            <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                                            <div id="divmsg" runat="server" class="msgbox">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="padding_leftright10" valign="top">Presenter Name</td>
                                    <td valign="top">&nbsp;<asp:TextBox ID="txtpresentername" MaxLength="100" Columns="60" onkeyup="ClearPresenterID();" CssClass="txtmixcaps" runat="server" OnTextChanged="txtpresentername_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="acepresentername" runat="server" TargetControlID="txtpresentername"
                                            CompletionListElementID="divacewidth" DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ShowOnlyCurrentWordInCompletionListItem="true"
                                            MinimumPrefixLength="1" EnableCaching="true" OnClientItemSelected="SetPresenterId" CompletionSetCount="1" CompletionInterval="100"
                                            ServiceMethod="Getpresentername"  FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                     
                                    </td>
                                    <td style="text-align: right">Street Address</td>
                                    <td colspan="3" valign="top" rowspan="6">
                                        <UC:City ID="uccity" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                    <td ali>City</td>
                                </tr>
                                <tr>
                                    <td class="padding_leftright10" rowspan="2" valign="bottom">Notes</td>
                                    <td rowspan="2" valign="middle" style="width:100px">&nbsp;
                                        <asp:TextBox ID="txtnotes" runat="server" TextMode="MultiLine" Rows="6" Columns="50"></asp:TextBox>
                                    </td>
                                    <td valign="top" rowspan="3" align="right">Zip Code
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="8">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="contentpadding">
                            <table cellpadding="0" cellspacing="0" align="left" border="0">
                                <%-- <tr>
                                    <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33" colspan="9">
                                        <a href="Venue.aspx" id="lnknew" runat="server" visible="false" class="txtnew">New</a>
                                        <asp:Label ID="lblhead" runat="server">Create Venue</asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <div class="linetop">
                                        </div>
                                        <asp:Panel ID="pnluc" runat="server" CssClass="ucpnl" Enabled="true">
                                            <table >
                                                <tr>
                                                    <div class="t-border">
                                                        <td width="1000px">
                                                            <UC:ConPerson ID="uccontactperson" runat="server" />
                                                        </td>
                                                    </div>
                                                    <div class="t-border">
                                                        <td width="450px">
                                                            <UC:Docx ID="ucdocx" runat="server" />
                                                        </td>
                                                    </div>
                                                    <div class="t-border">
                                                        <td width="450px"></td>
                                                    </div>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

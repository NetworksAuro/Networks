<%@ Page Title="Metro" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Metro.aspx.cs" Inherits="NTOS.Metro" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/jscript" src="Scripts/autoNumeric 1.9.15.js"></script>
    <script type="text/javascript">
        function SetMetroCityID(source, eventargs) {
            $('#<%=hdnmetrocityid.ClientID%>').val(eventargs.get_value());
        }
        function SetNearbyCityID(source, eventargs) {
            $('#<%=hdnnearbycityid.ClientID%>').val(eventargs.get_value());
        }
        function clearcityid()
        { }
        function SetStateID(source, eventargs) {
            $('#<%=hdnStateID.ClientID%>').val(eventargs.get_value());
        }
    </script>
    <script type="text/javascript">
        jQuery(function ($) { $('.percentage').autoNumeric({ aSign: '%', pSign: 's', vMax: '100' }); });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) { $('.percentage').autoNumeric({ aSign: '%', pSign: 's', vMax: '100' }); }
    </script>

    <asp:UpdatePanel ID="upnlmetro" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdncountry" runat="server" />
            <asp:HiddenField ID="hdnStateID" runat="server" />
            <asp:HiddenField ID="hdnmetrocityid" runat="server" />
            <asp:HiddenField ID="hdnstateidnc" runat="server" />
            <asp:HiddenField ID="hdncountryidnc" runat="server" />
            <asp:HiddenField ID="hdnnearbycityid" runat="server" />

            <asp:Panel ID="pnlmetro" runat="server" Enabled="true">
                <table width="1900px" cellspacing="0" cellpadding="0" style="border-left: 1px solid #ccc;">
                    <tbody>
                        <tr>
                            <td align="center" colspan="10">
                                <table width="1900">
                                    <tbody>
                                        <tr>
                                            <td colspan="10">
                                                <div class="alerttd">
                                                    <asp:ValidationSummary ID="vslist" runat="server" CssClass="valsummary" DisplayMode="BulletList" EnableClientScript="true" HeaderText="Field validations failed! Please check and correct!" />
                                                    <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                                                    <div id="divmsg" runat="server" class="msgbox">
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">

                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <label class="label-blue">Metro City :</label></td>
                                                                        <td style="text-align: left">
                                                                            <asp:TextBox ID="txtmetrocityname" runat="server" AutoPostBack="true" CssClass="txtmixcaps" onblur="clearcityid();" OnTextChanged="txtmetrocityname_TextChanged" TabIndex="1"></asp:TextBox>
                                                                            <ajaxToolkit:AutoCompleteExtender ID="acemetrocityname" runat="server" CompletionInterval="100" CompletionListCssClass="AutoExtender" CompletionListElementID="divacewidth" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="1" EnableCaching="true" FirstRowSelected="true" MinimumPrefixLength="1" OnClientItemSelected="SetMetroCityID" ServiceMethod="Getcityname" TargetControlID="txtmetrocityname">
                                                                            </ajaxToolkit:AutoCompleteExtender>
                                                                            <asp:RequiredFieldValidator ID="rfvmetrocityname" runat="server" ControlToValidate="txtmetrocityname" CssClass="asterisk" ErrorMessage="*" ToolTip="Enter metro city name!"></asp:RequiredFieldValidator></td>

                                                                        <td align="right">
                                                                            <label>Time Zone :</label></td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddltimezone" runat="server" SkinID="ddlmedium" TabIndex="4">
                                                                            </asp:DropDownList></td>

                                                                        <td align="right">
                                                                            <label>State:</label></td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" SkinID="ddlmedium" TabIndex="2">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvstate" runat="server" ControlToValidate="ddlstate" CssClass="asterisk" ErrorMessage="*" InitialValue="0" ToolTip="Select State!"></asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="txtstate" runat="server" Visible="false"></asp:TextBox>
                                                                            <ajaxToolkit:AutoCompleteExtender ID="acestate" runat="server" CompletionInterval="100" CompletionSetCount="1" EnableCaching="true" FirstRowSelected="true" MinimumPrefixLength="1" OnClientItemSelected="SetStateID" ServiceMethod="GetStateName" TargetControlID="txtstate">
                                                                            </ajaxToolkit:AutoCompleteExtender>
                                                                            <asp:RequiredFieldValidator ID="rfv_txtstate" runat="server" ControlToValidate="txtstate" CssClass="asterisk" Enabled="false" ErrorMessage="
                                            "
                                                                                ToolTip="Enter state!"></asp:RequiredFieldValidator></td>

                                                                        <td align="right">
                                                                            <label>Country:</label></td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" SkinID="ddlmedium" TabIndex="5">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvcountry" runat="server" ControlToValidate="ddlcountry" CssClass="asterisk" ErrorMessage="*" InitialValue="0" ToolTip="Select Country!"></asp:RequiredFieldValidator></td>

                                                                        <td align="right">
                                                                            <label>Zipcode :</label></td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtzipcode" runat="server" TabIndex="3"></asp:TextBox></td>
                                                                        <td align="right">
                                                                            <label>Tax :</label></td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txttax" runat="server" CssClass="percentage" TabIndex="6"></asp:TextBox></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="t2-border">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="1000px" style="border-right: 1px solid #ccc; border-top: 1px solid #ccc;">
                                                                            <div class="t2-border">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Panel ID="pnlrep" runat="server" Enabled="true" CssClass="ucpnl">
                                                                                                <table width="1000px" cellspacing="0">
                                                                                                    <tr>
                                                                                                        <td class="txt_contentheader" valign="middle">  <label class="heading">Near By Metro</label> &nbsp;
                                                                                                            <%--<asp:ImageButton ID="imgbtnAddnbmetro" runat="server" ImageUrl="~/Images/add-gdv.png" CommandName="AddNew"
                                                    ToolTip="Add" ValidationGroup="peradd" OnClick="imgbtnAddnbmetro_Click" />--%>
                                                                                                            <%--<asp:ImageButton ID="imgbtnDeletenbmetro" runat="server" ImageUrl="~/Images/cancel-gdv.png" OnClick="imgbtnDeletenbmetro_Click"
                                                    ToolTip="Delete" CausesValidation="false" />--%>
                                                                                                        </td>
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3"></td>
                                                                                                        <td></td>
                                                                                                        <td>
                                                                                                            <asp:ImageButton ID="lnkbtnAddbmetro" runat="server" ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px"      OnClick="lnkbtnAddmetro_Click" TabIndex="7"  ValidationGroup="vgmetro"></asp:ImageButton>
                                                                                                            <asp:ImageButton ID="lnkbtnDeletebmetro" runat="server" ImageUrl="~/Images/minus.png"   Height ="15px" Width ="20px" CausesValidation="false"  OnClick="lnkbtnDeletemetro_Click" TabIndex="8"></asp:ImageButton>
                                                                                                            <ajaxToolkit:ConfirmButtonExtender ID="cbedelete" runat="server" ConfirmOnFormSubmit="true" ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtnDeletebmetro">
                                                                                                            </ajaxToolkit:ConfirmButtonExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <asp:Repeater ID="Repnearbymetro" runat="server" OnItemDataBound="Repnearbymetro_ItemDataBound">
                                                                                                        <HeaderTemplate>

                                                                                                            <tr class="gridviewheader">
                                                                                                                <th>City</th>
                                                                                                                <th>State</th>
                                                                                                                <th>Country</th>
                                                                                                                <th>Zip Code</th>
                                                                                                                <th align="left">Option</th>
                                                                                                            </tr>
                                                                                                        </HeaderTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>'>
                                                                                                                <asp:HiddenField ID="hdnmetroid" Value='<%# Bind("metro_id")%>' runat="server" />
                                                                                                                <asp:HiddenField ID="hdntempid" Value='<%# Bind("tempid")%>' runat="server" />
                                                                                                                <td><%# Eval("cityname")%></td>
                                                                                                                <td><%# Eval("statename")%></td>
                                                                                                                <td><%# Eval("CountryName")%></td>
                                                                                                                <td style="word-wrap: break-word"><%# Eval("Zip")%></td>
                                                                                                                <td align="left">
                                                                                                                    <asp:CheckBox ID="chkdelete" runat="server" /></td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                        <FooterTemplate>

                                                                                                            <asp:Panel ID="pnlfooter" runat="server">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList SkinID="ddlmedium" TabIndex="9" ID="ddlcitynearby" runat="server" OnSelectedIndexChanged="ddlcitynearby_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                                                        <asp:RequiredFieldValidator ID="rfv_ddlnearbycity" runat="server" ErrorMessage="*" ToolTip="Select City!" InitialValue="0" ControlToValidate="ddlcitynearby"
                                                                                                                            CssClass="asterisk" ValidationGroup="vgmetro"></asp:RequiredFieldValidator>
                                                                                                                        <asp:TextBox ID="txtcitynearby" runat="server" Visible="false" OnTextChanged="txtcitynearby_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                                                        <ajaxToolkit:AutoCompleteExtender ID="aceresidcity" runat="server" TargetControlID="txtcitynearby"
                                                                                                                            FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                                                                                                                            CompletionInterval="100" OnClientItemSelected="SetNearbyCityID"
                                                                                                                            ServiceMethod="Getcityname">
                                                                                                                        </ajaxToolkit:AutoCompleteExtender>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtstatenc" runat="server" Visible="false"></asp:TextBox>
                                                                                                                        <asp:DropDownList SkinID="ddlmedium" ID="ddlstatenc" runat="server" Visible="false"></asp:DropDownList>
                                                                                                                        <asp:Label ID="lblnearbystate" runat="server"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblcountrync" runat="server"></asp:Label>
                                                                                                                        <asp:DropDownList SkinID="ddlmedium" ID="ddlcountrync" OnSelectedIndexChanged="ddlcountrync_SelectedIndexChanged" AutoPostBack="true" Visible="false" runat="server"></asp:DropDownList>

                                                                                                                    </td>

                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblzipcodenc" runat="server"></asp:Label>
                                                                                                                        <asp:TextBox ID="txtZipCodenc" runat="server" Visible="false"></asp:TextBox></td>
                                                                                                                </tr>

                                                                                                            </asp:Panel>

                                                                                                            <tr>
                                                                                                                <td>&nbsp;</td>
                                                                                                            </tr>

                                                                                                        </FooterTemplate>

                                                                                                    </asp:Repeater>
                                                                                                </table>
                                                                                            </asp:Panel>

                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                        <td width="450px" style="border-top: 1px solid #ccc;">
                                                                            <div class="t2-border">
                                                                                <table width="400px" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <th height="40" align="left" scope="col" style="padding-left: 12px;">
                                                                                            <label class="heading">Theatres / Venues</label>
                                                                                            <div align="right" style="float: right; padding-right: 12px;">

                                                                                                <asp:LinkButton ID="lnknewvenue"   Width ="60px"  runat="server" CommandArgument="0" OnClick="lnknewvenue_Click" PostBackUrl="~/Venue.aspx" TabIndex="-1" Text="New"></asp:LinkButton>
                                                                                                &nbsp;
                                                                                            </div>
                                                                                        </th>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Repeater ID="Repvenue" runat="server">
                                                                                                <HeaderTemplate>
                                                                                                    <table width="100%">
                                                                                                        <tr class="gridviewheader">
                                                                                                            <th>Venue Name</th>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>'>
                                                                                                        <td><%# Eval("Venue_name")%></td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    </table>
                                                                                                </FooterTemplate>
                                                                                            </asp:Repeater>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="t2-border"></div>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </asp:Panel>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

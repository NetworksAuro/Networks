<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="City.ascx.cs" Inherits="NTOS.City" %>
<script type="text/javascript">

    function setcityID_<%=this.ID %>(source, eventargs) {
        $('#<%=hdncityid.ClientID %>').val(eventargs.get_value());
    }
    function clearcityid() {
        $('#<%=hdncityid.ClientID %>').val("0");
    }
    function validateaddress() {
        if ($('#<%=txtaddress.ClientID %>').val().len == 0)
            ValidatorEnable(document.getElementById('<%=rfvcity.ClientID %>'), false);
        else
            ValidatorEnable(document.getElementById('<%=rfvcity.ClientID %>'), true);
    }
</script>
<asp:UpdatePanel ID="upcity" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdncityid" runat="server" />
        <asp:HiddenField ID="hdncountry" Value="0" runat="server" />
        <asp:HiddenField ID="hdnzipcode" Value="0" runat="server" />
        <asp:HiddenField ID="hdnstate" Value="0" runat="server" />
        <table width="100%" border="0" cellpadding="5" cellspacing="0" align="left">
            <tr>
                <td colspan="3">&nbsp;&nbsp;<asp:TextBox ID="txtaddress" CssClass="txtmixcaps" OnTextChanged="txtaddress_TextChanged" AutoPostBack="true" runat="server" MaxLength="200" Width="370px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" Enabled="false" ErrorMessage="*" ToolTip="Enter Address!" ControlToValidate="txtaddress"
                        CssClass="asterisk"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="padding:0px 10px;">&nbsp;
                    <asp:DropDownList ID="ddlcity" runat="server" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged" SkinID="ddlmedium1" AutoPostBack="true"></asp:DropDownList>
                    <asp:TextBox ID="txtcity" runat="server" MaxLength="30" OnTextChanged="txtcity_TextChanged" onkeyup="clearcityid();"
                        AutoPostBack="true" Visible="false"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="aceresidcity" runat="server" TargetControlID="txtcity"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="Getcityname">
                    </ajaxToolkit:AutoCompleteExtender>
                    <asp:RequiredFieldValidator ID="rfv_ddlcity" runat="server" Enabled="false" ErrorMessage="*" InitialValue="0" ToolTip="Select city!" ControlToValidate="ddlcity"
                        CssClass="asterisk"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvcity" runat="server" Enabled="false" ErrorMessage="*" ToolTip="Enter city!" ControlToValidate="txtcity"
                        CssClass="asterisk"></asp:RequiredFieldValidator>
                </td>
                <td style="padding:0px 10px; width:auto;" align="right">State
                </td>
                <td style="padding:0px 10px; width:auto;">&nbsp;
                    <asp:Label ID="lblstate" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="dropdown" Visible="false"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvstate" runat="server" Enabled="false" ErrorMessage="*" ToolTip="Select state!" InitialValue="0" ControlToValidate="ddlstate"
                        CssClass="asterisk"></asp:RequiredFieldValidator>

                    <asp:TextBox ID="txtstate" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtstate" runat="server" Enabled="false" ErrorMessage="*" ToolTip="Enter state!"
                        ControlToValidate="txtstate"
                        CssClass="asterisk"></asp:RequiredFieldValidator>
                </td>

            </tr>

            <tr>
                <td>&nbsp;
                    <asp:Label ID="lblzipcode" runat="server" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtzipcode" Visible="false" runat="server" OnTextChanged="txtzipcode_TextChanged" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="aceZipcode" runat="server" TargetControlID="txtzipcode"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="GetZipcode">
                    </ajaxToolkit:AutoCompleteExtender>
                    <asp:DropDownList ID="ddlzip" runat="server"><asp:ListItem Text="--Select--" Value="0"></asp:ListItem></asp:DropDownList>

                </td>
                <td align="right">Country
                </td>
                <td>&nbsp;
                    <asp:Label ID="lblcountry" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlcountry" CssClass="dropdown" Visible="false" runat="server" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvcountry" runat="server" InitialValue="0" Enabled="false" ErrorMessage="*" ToolTip="Select country!" ControlToValidate="ddlcountry"
                        CssClass="asterisk"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

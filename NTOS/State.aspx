<%@ Page Title="Manage States" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="State.aspx.cs" Inherits="NTOS.State" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" align="left">

                <tr>
                    <td class="contentpadding">
                        <table width="100%" cellpadding="0" cellspacing="0" align="left">
                           <%-- <tr>
                                <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33"
                                    colspan="8">State List
                                    
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <div class="alerttd">
                                        <asp:ValidationSummary ValidationGroup="add" ID="vslist" runat="server" DisplayMode="BulletList" HeaderText="Field validations failed! Please check and correct!" CssClass="valsummary" />
                                        <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                                        <div id="divmsg" runat="server" class="msgbox">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:GridView PageSize="20" AllowPaging="false" ID="grdState" runat="server" AutoGenerateColumns="False" ShowFooter="true" CellPadding="4" GridLines="None" OnRowCancelingEdit="grdState_RowCancelingEdit" OnRowCommand="grdState_RowCommand" OnRowDeleting="grdState_RowDeleting" OnRowEditing="grdState_RowEditing" OnRowUpdating="grdState_RowUpdating" OnPageIndexChanging="grdState_PageIndexChanging" OnRowDataBound="grdState_RowDataBound">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <AlternatingRowStyle CssClass="gdvaltrowstyle" />
                                        <FooterStyle CssClass="gridviewheader" />
                                        <RowStyle CssClass="gdvrowstyle" />
                                        <EditRowStyle CssClass="gdveditrowstyle" />
                                        <PagerStyle CssClass="gdvpagingstyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Country">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdfCountryid" runat="server" Value='<%#Bind("country_id") %>' />
                                                    <asp:Label ID="lblCountry" Text='<%#Bind("country_name") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hdfCountryid" runat="server" Value='<%#Bind("country_id") %>' />
                                                    <asp:DropDownList SkinID="ddlmedium" ID="ddlCountryE" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="add" CssClass="asterisk" InitialValue="-Select-" ID="rqEdt" runat="server" ControlToValidate="ddlCountryE" ToolTip="Please select country" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <%--<ajaxToolkit:ListSearchExtender ID="lsSearchCountryEdt" runat="server" TargetControlID="ddlCountryE" PromptText="Search here.."></ajaxToolkit:ListSearchExtender>--%>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList SkinID="ddlmedium" ID="ddlCountry" onchange="highlight();" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="add" CssClass="asterisk" InitialValue="-Select-" ID="rq1" runat="server" ControlToValidate="ddlCountry" ToolTip="Please select country" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <%--<ajaxToolkit:ListSearchExtender ID="lsSearchCountry" runat="server" TargetControlID="ddlCountry" PromptText="Search here.."></ajaxToolkit:ListSearchExtender>--%>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdfStateid" runat="server" Value='<%#Bind("State_id") %>' />
                                                    <asp:Label ID="lblState" Text='<%#Bind("State_name") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hdfStateid" runat="server" Value='<%#Bind("State_id") %>' />
                                                    <asp:TextBox MaxLength="50" ID="txtStateEdit" CssClass="txtmixcaps" Text='<%#Bind("State_name") %>' runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator CssClass="asterisk" ValidationGroup="add" ID="rqstEdt" runat="server" ControlToValidate="txtStateEdit" ToolTip="Please enter state" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox MaxLength="50" ID="txtStateName" CssClass="txtmixcaps" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="add" CssClass="asterisk" ID="rq" runat="server" ControlToValidate="txtStateName" ToolTip="Please enter state" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" Text='<%#Bind("state_active_flag") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hdfStatus" runat="server" Value='<%#Bind("state_active_flag") %>' />
                                                    <asp:CheckBox ID="chkStatus" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:ImageButton CausesValidation="false" ImageUrl="~/Images/edit.png" ToolTip="Edit" ID="lnkEdit" runat="server" CommandName="Edit" />
                                                    <asp:ImageButton CausesValidation="false"  ID="lnkDelete" runat="server" CommandName="Delete" />
                                                    <ajaxToolkit:ConfirmButtonExtender ID="cnfm" runat="server" TargetControlID="lnkDelete"></ajaxToolkit:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ImageUrl="~/Images/save.png" ValidationGroup="add" ID="lnkUpdate" ToolTip="Update" runat="server" CommandName="Update" />
                                                    <asp:ImageButton ImageUrl="~/Images/back.png" CausesValidation="false" ID="lnkCancel" ToolTip="Cancel" runat="server" CommandName="Cancel" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnSave" CommandName="Save" ValidationGroup="add" runat="server" ImageUrl="~/Images/add-btn.png" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

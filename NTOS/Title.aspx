<%@ Page Title="Manage Titles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Title.aspx.cs" Inherits="NTOS.Title" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" align="left">

                <tr>
                    <td class="contentpadding">

                        <table style="padding-bottom: 25px" width="100%" cellpadding="0" cellspacing="0" align="left" >
                          <%--  <tr>
                                <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33"
                                    colspan="8">Title List
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
                                    <asp:GridView PageSize="20" AllowPaging="true" CellPadding="4" ID="grdTitle" runat="server" AutoGenerateColumns="False" ShowFooter="true" GridLines="None" OnPageIndexChanging="grdTitle_PageIndexChanging" OnRowCancelingEdit="grdTitle_RowCancelingEdit" OnRowCommand="grdTitle_RowCommand" OnRowDeleting="grdTitle_RowDeleting" OnRowEditing="grdTitle_RowEditing" OnRowUpdating="grdTitle_RowUpdating" OnRowDataBound="grdTitle_RowDataBound">
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
                                            <asp:TemplateField HeaderText="Title">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle  HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdftitle_id" runat="server" Value='<%#Bind("title_id") %>' />
                                                    <asp:Label ID="lblTitlename" Text='<%#Bind("title_name") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hdftitle_id" runat="server" Value='<%#Bind("title_id") %>' />
                                                    <%--<asp:Label ID="lblCountryid" Visible="false" Text='<%#Bind("country_id") %>' runat="server"></asp:Label>--%>
                                                    <asp:TextBox ID="txtTitleNameEdit" CssClass="txtmixcaps" MaxLength="30" Text='<%#Bind("title_name") %>' runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="add" CssClass="asterisk" ID="rqEdit" runat="server" ControlToValidate="txtTitleNameEdit" ToolTip="Please enter title" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTitleName" CssClass="txtmixcaps" MaxLength="30" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="add" ID="rq" CssClass="asterisk" runat="server" ControlToValidate="txtTitleName" ToolTip="Please enter title" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle  HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" Text='<%#Bind("title_active_flag") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hdfStatus" runat="server" Value='<%#Bind("title_active_flag") %>' />
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

<%@ Page Title="Manage Users" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="NTOS.Users" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" align="left">

                <tr>
                    <td class="contentpadding">
                        <table width="100%" cellpadding="0" cellspacing="0" align="left" >
                          <%--  <tr>
                                <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33"
                                    colspan="8">User List
                                    
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
                                    <asp:GridView PageSize="20" AllowPaging="false" ID="grdUsers" runat="server" AutoGenerateColumns="False" ShowFooter="true" CellPadding="4" GridLines="None" OnRowCancelingEdit="grdUsers_RowCancelingEdit" OnRowCommand="grdUsers_RowCommand" OnRowDeleting="grdUsers_RowDeleting" OnRowEditing="grdUsers_RowEditing" OnRowUpdating="grdUsers_RowUpdating"
                                         OnPageIndexChanging="grdUsers_PageIndexChanging" OnRowDataBound="grdUsers_RowDataBound">
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
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdfusersid" runat="server" Value='<%#Bind("users_id") %>' />
                                                    <asp:Label ID="lblUser" Text='<%#Bind("users_login") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hdfusersid" runat="server" Value='<%#Bind("users_id") %>' />
                                                     <asp:TextBox MaxLength="20" ID="txtUserE" CssClass="txtmixcaps" Text='<%#Bind("users_login") %>' runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator CssClass="asterisk" ValidationGroup="add" ID="rqstEdt" runat="server" ControlToValidate="txtUserE" ToolTip="Please enter UserName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                       <asp:TextBox MaxLength="20" ID="txtUserF" CssClass="txtmixcaps" Text="" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator CssClass="asterisk" ValidationGroup="add" ID="rqstF" runat="server" ControlToValidate="txtUserF" ToolTip="Please enter UserName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="User Role">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUserRole" Text='<%#Bind("users_role") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:HiddenField ID="hdfUserRole" runat="server" Value='<%#Bind("users_role") %>' />
                                                    <asp:DropDownList ID="ddlUserRole" runat="server">
                                                        <asp:ListItem Selected="True" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Text="Super Admin" Value="superadmin"></asp:ListItem>
                                                        <asp:ListItem Text="Reader" Value="reader"></asp:ListItem>
                                                        <asp:ListItem Text="Comp Manager" Value="compmanager"></asp:ListItem>
                                                        <asp:ListItem Text="Office Staff" Value="officestaff"></asp:ListItem>
                                                        <asp:ListItem Text="Admin" Value="admin"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="add" CssClass="asterisk" InitialValue="-Select-" ID="rq1" runat="server" ControlToValidate="ddlUserRole" ToolTip="Please select User Role" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                       <asp:DropDownList ID="ddlUserRoleF" runat="server"  onchange="highlight();">
                                                        <asp:ListItem Selected="True" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Text="Super Admin" Value="superadmin"></asp:ListItem>
                                                        <asp:ListItem Text="Reader" Value="reader"></asp:ListItem>
                                                        <asp:ListItem Text="Comp Manager" Value="compmanager"></asp:ListItem>
                                                        <asp:ListItem Text="Office Staff" Value="officestaff"></asp:ListItem>
                                                        <asp:ListItem Text="Admin" Value="admin"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="add" CssClass="asterisk" InitialValue="-Select-" ID="rqF" runat="server" ControlToValidate="ddlUserRoleF" ToolTip="Please select User Role" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" Text='<%#Bind("users_active_flag") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hdfStatus" runat="server" Value='<%#Bind("users_active_flag") %>' />
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

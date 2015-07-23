<%@ Control EnableTheming="false" Language="C#" AutoEventWireup="true" CodeBehind="Lookuplist.ascx.cs" Inherits="NTOS.Lookuplist" %>
<style type="text/css">
    .dp {
        border-style: none;
        min-height: 20px;
        border-bottom: solid 1px #aaa;
    }
</style>
<table width="100%">

    <tr>
        <td>
            <div class="alerttd">
                <asp:ValidationSummary ID="vslist" runat="server" DisplayMode="BulletList" HeaderText="Field validations failed! Please check and correct!" CssClass="valsummary" />
                <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                <div id="divmsg" runat="server" class="msgbox">
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:GridView PageSize="20" AllowPaging="false" ID="grdLookup" runat="server" AutoGenerateColumns="False" ShowFooter="true" CellPadding="4" GridLines="None" OnPageIndexChanging="grdLookup_PageIndexChanging" OnRowCancelingEdit="grdLookup_RowCancelingEdit" OnRowCommand="grdLookup_RowCommand" OnRowDeleting="grdLookup_RowDeleting" OnRowEditing="grdLookup_RowEditing" OnRowUpdating="grdLookup_RowUpdating" OnRowDataBound="grdLookup_RowDataBound">
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
                    <asp:TemplateField HeaderText="Lookup Group" Visible="false">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lbllkup_group" Text='<%#Bind("lkup_group_desc") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="hdflkup_group" runat="server" Value='<%#Bind("lkup_group_desc") %>' />
                            <asp:DropDownList OnSelectedIndexChanged="ddllkup_groupE_SelectedIndexChanged" CssClass="dp" AutoPostBack="true" ID="ddllkup_groupE" runat="server">
                                <asp:ListItem Value="-SELECT-" Text="-SELECT-"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ValidationGroup='<%#this.ID %>' CssClass="asterisk" InitialValue="-Select-" ID="rqEdt" runat="server" ControlToValidate="ddllkup_groupE" ToolTip="Please select group" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <%--<ajaxToolkit:ListSearchExtender ID="lsSearchCountryEdt" runat="server" TargetControlID="ddllkup_groupE" PromptText="Search here.."></ajaxToolkit:ListSearchExtender>--%>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList OnSelectedIndexChanged="ddllkup_group_SelectedIndexChanged" AutoPostBack="true" CssClass="dp" ID="ddllkup_group" runat="server">
                                <asp:ListItem Value="-Select-" Text="-Select-"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ValidationGroup='<%#this.ID %>' CssClass="asterisk" InitialValue="-Select-" ID="rq1" runat="server" ControlToValidate="ddllkup_group" ToolTip="Please select group" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <%--<ajaxToolkit:ListSearchExtender ID="lsSearchCountry" runat="server" TargetControlID="ddllkup_group" PromptText="Search here.."></ajaxToolkit:ListSearchExtender>--%>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lookup Item">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:HiddenField ID="hdfLookupid" runat="server" Value='<%#Bind("lkup_id") %>' />
                            <asp:Label ID="lbllkup_desc" Text='<%#Bind("lkup_desc") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="hdflkup_id" runat="server" Value='<%#Bind("lkup_id") %>' />
                            <asp:TextBox MaxLength="50" CssClass="txtmixcaps" ID="txtlkup_descEdit" Text='<%#Bind("lkup_desc") %>' runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="asterisk" ValidationGroup='<%#this.ID %>' ID="rqstEdt" runat="server" ControlToValidate="txtlkup_descEdit" ToolTip="Please enter lookup" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox MaxLength="50" CssClass="txtmixcaps" ID="txtlkup_desc" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup='<%#this.ID %>' CssClass="asterisk" ID="rq" runat="server" ControlToValidate="txtlkup_desc" ToolTip="Please enter lookup" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" Text='<%#Bind("lkup_active_flag") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="hdfStatus" runat="server" Value='<%#Bind("lkup_active_flag") %>' />
                            <asp:CheckBox ID="chkStatus" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:ImageButton CausesValidation="false" ImageUrl="~/Images/edit.png" ToolTip="Edit" ID="lnkEdit" runat="server" CommandName="Edit" />
                            <asp:ImageButton CausesValidation="false" ID="lnkDelete" runat="server" CommandName="Delete" />
                            <ajaxToolkit:ConfirmButtonExtender ID="cnfm" runat="server" TargetControlID="lnkDelete"></ajaxToolkit:ConfirmButtonExtender>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" ValidationGroup='<%#this.ID %>' ID="lnkUpdate" ToolTip="Update" runat="server" CommandName="Update" />
                            <asp:ImageButton ImageUrl="~/Images/back.png" CausesValidation="false" ID="lnkCancel" ToolTip="Cancel" runat="server" CommandName="Cancel" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="btnSave" CommandName="Save" ValidationGroup='<%#this.ID %>' runat="server" ImageUrl="~/Images/add-btn.png" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

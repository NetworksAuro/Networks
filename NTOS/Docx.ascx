<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Docx.ascx.cs" Inherits="NTOS.Docx" %>
<div class="txt_contentheader uchead">
    Attach Documents
    <asp:LinkButton ID="lnkbtnAdd" runat="server" Text="[+]" CssClass="lnkadd" OnClick="lnkbtnAdd_Click" ToolTip="Attach" ValidationGroup="validaiton"></asp:LinkButton>
    <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="[x]" CssClass="lnkdelete" OnClick="lnkbtnDelete_Click" ToolTip="Delete" CausesValidation="false"></asp:LinkButton>
    <ajaxToolkit:ConfirmButtonExtender ID="cbedelete" runat="server" ConfirmOnFormSubmit="true" ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtnDelete"></ajaxToolkit:ConfirmButtonExtender>
</div>
<asp:HiddenField ID="hdntablename" runat="server" />
<asp:HiddenField ID="hdntableid" runat="server" />
<asp:UpdatePanel ID="updocx1" runat="server">
    <ContentTemplate>
        <table width="50%">
            <asp:Repeater ID="RepDetails" runat="server">
                <HeaderTemplate>

                    <tr class="gridviewheader">
                        <th>Document</th>
                        <th style="width: 100px;">Option</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>'>
                        <td align="left">

                            <asp:LinkButton Visible="false" ID="lnkbtndocxname" OnClick="lnkbtndocxname_Click" CausesValidation="false"
                                runat="server" Text='<%#Eval("DocFilename") %>' Font-Bold="true" ToolTip="Open the document!"></asp:LinkButton>
                            <a id="A2" href='<%# DataBinder.Eval(Container, "DataItem.DocFilepath")%>' target="_blank"><%#Eval("DocFilename") %></a>
                            <asp:HiddenField ID="hdntempid" runat="server" Value='<%#Eval("tempid") %>' />
                            <asp:HiddenField ID="hdnpath" runat="server" Value='<%#Eval("DocFilepath") %>' />
                            <asp:HiddenField ID="hdndocxid" runat="server" Value='<%#Eval("docx_id") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkdelete" runat="server" />

                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Panel ID="pnlfooter" runat="server">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="updocx" runat="server">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="fupdocx" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvfileup" runat="server" ControlToValidate="fupdocx" CssClass="asterisk" ValidationGroup="fileup" ErrorMessage="*" ToolTip="Browse the document!"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="imgbtnAttach" />

                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="imgbtnAttach" runat="server" ImageUrl="~/Images/attach-btn.png" CommandName="AddNew" ValidationGroup="fileup"
                                    ToolTip="Save" OnClick="imgbtnAttach_Click" /></td>
                        </tr>
                    </asp:Panel>
                </FooterTemplate>
            </asp:Repeater>
        </table>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="lnkbtnAdd" />
    </Triggers>
</asp:UpdatePanel>



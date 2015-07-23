<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PresenterContactPerson.ascx.cs" Inherits="NTOS.PresenterContactPerson" %>
<script type="text/javascript">
    function setPersonalID(source, eventargs) {
        $('#<%=hdnfirstnameid.ClientID %>').val(eventargs.get_value());
        $('#<%=hdnfirstname.ClientID %>').val(eventargs.get_text());
    }
    function clearPersonalID(thisid) {
        $('#<%=hdnfirstnameid.ClientID %>').val("0");
    }
    function goto_personal() {
        //debugger;
        var msg = "Saving " + document.title + " Data?";
        return confirm(msg);
    }
</script>
<asp:HiddenField ID="hdntype" runat="server" Value="" />
<asp:HiddenField ID="hdnpresenterid" runat="server" />
<asp:HiddenField ID="hdnvenueid" runat="server" />
<asp:HiddenField ID="hdnpresenteridlist" runat="server" />

<div style="float:left; width:900px;">
<table>
    <tr>
        <td>
<div class="txt_contentheader uchead">
  <div style="float: left;"> <label class="heading">Contact Persons</label> </div> 
               <%-- <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/add-gdv.png" OnClick="imgbtnAdd_Click" CommandName="AddNew"
                    ToolTip="Assign new show" ValidationGroup="peradd" />
    <asp:ImageButton ID="imgbtnDelete" runat="server" OnClick="imgbtnDelete_Click" ImageUrl="~/Images/cancel-gdv.png"
        ToolTip="Delete" CausesValidation="false" />--%>
    <div style="float: left ;">
    <asp:ImageButton ID="lnkbtnAdd" runat="server"  ValidationGroup="peradd"  ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px" OnClick="lnkbtnAdd_Click" ToolTip="Add"></asp:ImageButton>
    <asp:ImageButton ID="lnkbtnDelete" runat="server"  OnClick="lnkbtnDelete_Click" ToolTip="Delete"  ImageUrl="~/Images/minus.png"   Height ="15px" Width ="20px" CausesValidation="false"></asp:ImageButton>


    <ajaxToolkit:ConfirmButtonExtender ID="cbedelete" runat="server" ConfirmOnFormSubmit="true" ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtnDelete"></ajaxToolkit:ConfirmButtonExtender>
        </div> 
</div>
        </td>
    </tr>
    <tr>
        <td>
<asp:UpdatePanel ID="upperson" runat="server">
    <ContentTemplate>
       
        <table width="100%" cellpadding="0" cellspacing="0">
            <asp:Repeater ID="RepDetails" runat="server" OnItemDataBound="RepDetails_ItemDataBound" OnItemCommand="RepDetails_ItemCommand">
                <HeaderTemplate>
                    <tr class="gridviewheader">
                        <th style="width:auto; padding: 0px 15px;">Title</th>
                        <th style="width:auto; padding: 0px 15px;">Last Name</th>
                        <th style="width:auto; padding: 0px 15px;">First Name</th>
                        <th style="width:auto; padding: 0px 15px;">Phone&nbsp;(Direct) &nbsp;</th>
                        <th style="width:auto; padding: 0px 15px;"> Fax &nbsp;</th>
                        <th style="width:auto; padding: 0px 15px;"> Email &nbsp; </th>
                        <th style="width:auto; padding: 0px 15px;"> Option </th>

                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>'>
                        <td align="left" width:auto; padding: 0px 15px;"><%# Eval("title")%>
                            <asp:HiddenField ID="hdntempid" Value='<%# Bind("tempid") %>' runat="server" />
                            <asp:HiddenField ID="htncontactpersonid" Value='<%# Bind("ContactPersonID") %>' runat="server" />
                        </td>
                        <td style="width:auto; padding: 0px 15px;"><%# Eval("firstname")%></td>
                        <td style="width:auto; padding: 0px 15px;"><%# Eval("lastname")%></td>
                        <td style="width:auto; padding: 0px 15px;"><%# Eval("phone")%></td>
                        <td style="width:auto; padding: 0px 15px;"><%# Eval("fax")%></td>
                        <td style="width:auto; padding: 0px 15px;"><%# Eval("email")%></td>
                        <td style="width:auto; padding: 0px 15px; text-align:center;">
                            <asp:CheckBox ID="chkdelete" runat="server" /></td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>

                    <asp:Panel ID="pnlfooter" runat="server">
                        <tr>
                            <td style="width:auto; padding: 0px 15px;">
                                <%--  <asp:DropDownList ID="ddltitle" runat="server" OnSelectedIndexChanged="ddltitle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </td>
                            <td style="width:auto; padding: 0px 15px;">
                                <asp:DropDownList ID="ddlfirstname" runat="server" SkinID="ddlmedium" OnSelectedIndexChanged="ddlfirstname_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <asp:TextBox ID="txtfirstname" runat="server" Visible="false" onchange="clearPersonalID(this);" OnTextChanged="txtfirstname_TextChanged" AutoPostBack="true" />
                                <ajaxToolkit:AutoCompleteExtender ID="acefirstname" runat="server" TargetControlID="txtfirstname"
                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                    ServiceMethod="Getpersonalname" OnClientItemSelected="setPersonalID" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="rfvfirstname" runat="server" ErrorMessage="*" ValidationGroup="peradd"
                                    ControlToValidate="txtfirstname" CssClass="asterisk"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width:auto; padding: 0px 15px;">
                                <asp:DropDownList ID="ddllastname" SkinID="ddlmedium" runat="server" OnSelectedIndexChanged="ddllastname_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvlastname" runat="server" ValidationGroup="peradd" InitialValue="0" ErrorMessage="*" ControlToValidate="ddllastname"
                                    CssClass="asterisk"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width:auto; padding: 0px 15px;">
                                <asp:Label ID="lblphone" runat="server"></asp:Label>
                            </td>
                            <td style="width:auto; padding: 0px 15px;">
                                <asp:Label ID="lblfax" runat="server"></asp:Label>
                            </td>
                            <td style="width:auto; padding: 0px 15px;">
                                <asp:Label ID="lblemail" runat="server"></asp:Label>
                            </td>
                            <td style="width:auto; padding: 0px 15px;"><%--<asp:LinkButton ID="lnkgoto" runat="server" OnClick="lnkgoto_Click" Text="New1" CausesValidation="false"></asp:LinkButton>--%>
                                <%--<a href="#" id="lnkeditper" runat="server" onclick="return goto_personal();" visible="false">Edit</a>
                                <a href="Personal.aspx" id="lnknewper" runat="server" onclick="return confirm('Do you want to create new record?');">Add</a>--%>
                                <asp:LinkButton ID="lnkEditperson" runat="server"  Width ="60px" style="text-decoration :underline" Text="Edit" OnClientClick="return confirm('Do you want to edit this record?');" Visible="false" OnClick="lnkEditperson_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lnkAddnewperson" runat="server"  Width ="60px" style="text-decoration :underline" Text="Add" OnClientClick="return goto_personal();" OnClick="lnkAddnewperson_Click"></asp:LinkButton>
                            </td>

                        </tr>

                    </asp:Panel>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                </FooterTemplate>

            </asp:Repeater>
        </table>
         <asp:HiddenField ID="hdnfirstname" runat="server" />
        <asp:HiddenField ID="hdnfirstnameid" runat="server" />
        <asp:HiddenField ID="hdnlastname" runat="server" />
        <asp:HiddenField ID="hdnlastnameid" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
        </td>
    </tr>
</table>

<div>

</div>
    </div>
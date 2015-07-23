<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalShow.ascx.cs"
    Inherits="NTOS.PersonalShow" %>
<script type="text/javascript">
    function pageLoad() {
        function enablevalidation() {

        }
    }

</script>
<table>
    <tr>
        <td>
            <div class="txt_contentheader uchead">
     <div style="float: left;margin-left: 0px;"><label class ="heading">Show Assignment</label> </div> 
    <div style="float: left; margin-right: 5px;">
        <asp:ImageButton ID="lnkbtnAdd" runat="server" ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px" OnClick="lnkbtnAdd_Click" ToolTip="Add"></asp:ImageButton>
        <asp:ImageButton ID="lnkbtnDelete" runat="server"  OnClick="lnkbtnDelete_Click" ToolTip="Delete" ImageUrl="~/Images/minus.png"   Height ="15px" Width ="20px" CausesValidation="false"></asp:ImageButton>
     
        
        


        
        
        
           <ajaxToolkit:ConfirmButtonExtender ID="cbedelete" runat="server" ConfirmOnFormSubmit="true" ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtnDelete"></ajaxToolkit:ConfirmButtonExtender>
    </div>
</div>
        </td>
    </tr>
    <tr>
        <td>
<div style="float:left; width:990px;">
<table width="900px" cellpadding="0" cellspacing="0">
    <asp:Repeater ID="RepDetails" runat="server" OnItemDataBound="RepDetails_ItemDataBound" OnItemCommand="RepDetails_ItemCommand">
        <HeaderTemplate>
            <tr class="gridviewheader">
                <th>Show Name</th>
                <th>Assignment Date</th>
                <th>End Date</th>
                <th>Status</th>
                <th>Option
                </th>
                <th>Delete</th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>' runat="server" id="trcrow">
                <td align="left" style="width: 200px;">
                    <asp:Label ID="lblshowname" runat="server" Text='<%# Bind("showname") %>' />
                    <asp:HiddenField ID="hdntempid" runat="server" Value='<%# Bind("tempid") %>' />
                    <asp:HiddenField ID="hdnpersonalshowid" runat="server" Value='<%# Bind("per_assign_id") %>' />
                    <asp:HiddenField ID="hdnshowid" runat="server" Value='<%# Bind("showid") %>' />
                </td>
                <td style="width: 150px;">
                    <asp:Label ID="lblassignmentdate" runat="server" Text='<%# Bind("assigndate") %>' />

                </td>
                <td style="width: 150px;">
                    <asp:Label ID="lblenddate" runat="server" Text='<%# Bind("enddate") %>' />
                </td>
                <td style="width: 150px;">
                    <asp:Label ID="ccc00" runat="server" Text='<%# Bind("assignflagtext") %>' /></td>
                <td align="center" style="width: 150px;">
                    <asp:LinkButton ID="lnkEditShow" runat="server" Text="Edit" CommandName="edit" CausesValidation="false"></asp:LinkButton>
                </td>
                <td align="center" style="width: 150px;">
                    <asp:CheckBox ID="chkdelete" runat="server" /></td>
            </tr>
            <tr runat="server" id="tredit" visible="false">
                <td>
                    <asp:DropDownList ID="ddlshownameE" OnSelectedIndexChanged="ddlshownameE_SelectedIndexChanged" AutoPostBack="true" Width="200px" runat="server"></asp:DropDownList>

                </td>
                <td>
                    <asp:TextBox ID="txtassignmentdateE" Width="100px" runat="server" Text='<%# Bind("assigndate") %>' />
                    <asp:RequiredFieldValidator Enabled="false" ID="rfvassignmentdateE" runat="server" CssClass="asterisk"
                        ToolTip="Enter assignment date!" ControlToValidate="txtassignmentdateE">*</asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="ceassignmentdateE" runat="server" TargetControlID="txtassignmentdateF">
                    </ajaxToolkit:CalendarExtender>

                </td>
                <td>
                    <asp:TextBox ID="txtenddateE" runat="server" Width="100px" Text='<%# Bind("enddate") %>' />
                    <ajaxToolkit:CalendarExtender ID="ceenddateE" runat="server" TargetControlID="txtenddateE">
                    </ajaxToolkit:CalendarExtender>


                </td>
                <td>
                    <asp:DropDownList ID="ddlcurrentshowE" runat="server" ValidationGroup="Add" SelectedValue='<%# Eval("assignflag")%>'>
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvcurrentflagE" Enabled="false" runat="server" CssClass="asterisk" InitialValue="0"
                        ToolTip="Select current show status!" ControlToValidate="ddlcurrentshowE">*</asp:RequiredFieldValidator>
                </td>
                <td colspan="2" align="center">
                    <asp:LinkButton ID="lnkUpdateDoc" runat="server" Text="Update" CommandName="update"></asp:LinkButton>
                    <asp:LinkButton ID="lnkCancelDoc" runat="server" Text="Cancel" CommandName="cancel"></asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>

    </asp:Repeater>
    <tr runat="server" id="trfooter">
        <td>
            <asp:DropDownList ID="ddlshownameF" OnSelectedIndexChanged="ddlshownameF_SelectedIndexChanged" AutoPostBack="true" Width="200px" runat="server"></asp:DropDownList>

        </td>
        <td>
            <asp:TextBox ID="txtassignmentdateF" Width="100px" runat="server" />
            <asp:RequiredFieldValidator Enabled="false" ID="rfvassignmentdate" runat="server" CssClass="asterisk"
                ToolTip="Enter assignment date!" ControlToValidate="txtassignmentdateF">*</asp:RequiredFieldValidator>
            <ajaxToolkit:CalendarExtender ID="ceassignmentdateF" runat="server" TargetControlID="txtassignmentdateF">
            </ajaxToolkit:CalendarExtender>

        </td>
        <td>
            <asp:TextBox ID="txtenddateF" runat="server" Width="100px" />
            <ajaxToolkit:CalendarExtender ID="ceenddateF" runat="server" TargetControlID="txtenddateF">
            </ajaxToolkit:CalendarExtender>


        </td>
        <td>
            <asp:DropDownList ID="ddlcurrentshowF" runat="server" ValidationGroup="Add">
                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                <asp:ListItem Text="NO" Value="N"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvcurrentflag" Enabled="false" runat="server" CssClass="asterisk" InitialValue="0"
                ToolTip="Select current show status!" ControlToValidate="ddlcurrentshowF">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/add-btn.png" CommandName="AddNew" Visible="false"
                ToolTip="Add new User" /></td>
    </tr>
</table>
</div>
        </td>
    </tr>
</table>

<asp:HiddenField ID="hdnucpersonalid" runat="server" />



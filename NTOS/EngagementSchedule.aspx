<%@ Page Title="Engagement Schedule" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Engagement.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="EngagementSchedule.aspx.cs" EnableEventValidation="false"
    Inherits="NTOS.EngagementSchedule" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .notes
        {
            width: 30em;
            word-wrap: break-word;
            padding: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    a<script type="text/jscript" src="Scripts/autoNumeric 1.9.15.js"></script><script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            $('.Dollar').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99' });
        }
        function CheckAll(cb) {
            var flg = cb.checked;
            var t = $('#tblrep')[0];
            var rep = $('#<%=RepDetails.ClientID%>');
            var ctrls = document.getElementsByTagName('input');
            for (var i = 1; i < t.rows.length; i++) {
                $(rep.selector + '_chkdelete_' + (i - 1)).attr("checked", flg);
            }
        }
    </script><asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="left">
                <asp:Label ID="lbl_schedule" Visible="false" runat="server" Text="Please Create Engagement First"
                ForeColor="Red"></asp:Label>
            </div>
            <br />
            <div id="div_schedule" runat="server" visible="false">
                <div >
                    <div class ="clear"  >
                    </div>
                    <div style="padding-left: 20px; margin-top:20px;">
                        <label class="heading">
                        Schedule</label >&nbsp;<asp:ImageButton ID="lnkbtnAdd" runat="server" ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px" OnClick="lnkbtnAdd_Click"
                        ToolTip="Add">
                        </asp:ImageButton>
                        <asp:ImageButton ID="lnkbtnDelete" runat="server" ImageUrl="~/Images/minus.png"   Height ="15px" Width ="20px"
                        OnClick="lnkbtnDelete_Click" ToolTip="Delete" CausesValidation="false">
                        </asp:ImageButton>
                        <ajaxToolkit:ConfirmButtonExtender ID="cbedelete" runat="server" ConfirmOnFormSubmit="true"
                        ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtnDelete">
                        </ajaxToolkit:ConfirmButtonExtender>
                    </div>
                </div>
                <div style="float:left; width:1300px;">
                    <table width="100%" id="tblrep" cellpadding ="0" cellspacing ="0" style="padding-left:50px">
                        <asp:HiddenField ID="hdfPerf" runat="server" />
                        <asp:HiddenField ID="hdn_schnextdate" runat="server" />
                        <asp:Repeater ID="RepDetails" runat="server" OnItemCommand="RepDetails_ItemCommand"
                        OnItemDataBound="RepDetails_ItemDataBound">
                            <HeaderTemplate>
                                <tr class="gridviewheader">
                                    <th width="19%">Type</th>
                                    <th width="10%">Date</th>
                                    <th width="10%">Day</th>
                                    <th width="10%">Start Time</th>
                                    <th width="10%">End Time</th>
                                    <th width="5%">Notes</th>
                                    <th width="5%">
                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="CheckAll(this);" />
                                    </th>
                                    <th width="1%"></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="tr_crow" runat="server" class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>'>
                                    <td align="left" width="19%"><%# Eval("schedule_type") %></td>
                                    <td align="left" width="10%"><%# Eval("schedule_date") %></td>
                                    <td align="left" width="10%"><%# Eval("schedule_day") %></td>
                                    <td align="left" width="10%"><%# Eval("schedule_st_time") %></td>
                                    <td align="left" width="10%"><%# Eval("schedule_end_time") %></td>
                                    <td align="left" valign="top" width="35%">
                                        <p class="notes">
                                            <%# Eval("schedule_notes") %>
                                        </p>
                                    </td>
                                    <td align="left" width="5%">
                                        <asp:CheckBox ID="chkdelete" runat="server" />
                                    </td>
                                    <td align="left" width="1%">
                                        <asp:LinkButton style="text-decoration:underline " ID="lnkEditSchedule" runat="server" Text="Edit" CommandName="edit"
                                        CausesValidation="false"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnscheduleid" Value='<%# Bind("schedule_id") %>' runat="server" />
                                    </td>
                                </tr>
                                <tr id="tredit" runat="server" visible="false">
                                    <td>
                                        <asp:HiddenField ID="hdnscheduletypeE" Value='<%# Bind("schedule_type") %>' runat="server" />
                                        <asp:DropDownList ID="ddlscheduletypeE" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvscheduletypeE" ToolTip="Select schedule type!"
                                        InitialValue="0" runat="server" ControlToValidate="ddlscheduletypeE"
                                        CssClass="asterisk">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtscheduedateE" Text='<%# Bind("schedule_date") %>' runat="server"
                                        ToolTip="Press A/P for AM/PM"
                                        OnTextChanged="txtscheduedate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="cescheduledateE" runat="server" TargetControlID="txtscheduedateE">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="revscheduledateE" runat="server"
                                        ControlToValidate="txtscheduedateE" ForeColor="Red" ToolTip="Enter valid date format(mm/dd/yyyy)"
                                        ValidationExpression="^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$">#
                                    </asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvpresenternameE" ToolTip="Select schedule date!"
                                        runat="server" ControlToValidate="txtscheduedateE"
                                        CssClass="asterisk">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblscheduledayE" Text='<%# Bind("schedule_day") %>' runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtschedule_sttimeE" Text='<%# Bind("schedule_st_time") %>' OnTextChanged="txtschedule_sttimeE_TextChanged"
                                        AutoPostBack="true" runat="server" ToolTip="Press A/P for AM/PM" />
                                        <ajaxToolkit:MaskedEditExtender ID="medsttimeE" runat="server" MaskType="Time" ErrorTooltipEnabled="true"
                                        UserTimeFormat="None" AcceptAMPM="true"
                                        TargetControlID="txtschedule_sttimeE" Mask="99:99" MessageValidatorTip="true"
                                        AutoComplete="true">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:RegularExpressionValidator ID="revsttimeE" runat="server"
                                        ControlToValidate="txtschedule_sttimeE" ForeColor="Red" ToolTip="Enter valid time(HH:MM AM or PM)"
                                        ValidationExpression="^(1[0-2]|0[1-9]):[0-5][0-9]\040(AM|am|PM|pm)$">#
                                    </asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtschedule_endtimeE" Text='<%# Bind("schedule_end_time") %>' runat="server" ToolTip="Press A/P for AM/PM" />
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Time"
                                        ErrorTooltipEnabled="true" UserTimeFormat="None" AcceptAMPM="true"
                                        TargetControlID="txtschedule_endtimeE" Mask="99:99" MessageValidatorTip="true"
                                        AutoComplete="true">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtschedule_endtimeE" ForeColor="Red" ToolTip="Enter valid time(HH:MM AM or PM)"
                                        ValidationExpression="^(1[0-2]|0[1-9]):[0-5][0-9]\040(AM|am|PM|pm)$">#
                                    </asp:RegularExpressionValidator>
                                    </td>
                                    <td class="notes">
                                        <asp:TextBox ID="txtschedulenotesE" Rows="3" CssClass="notes" runat="server" Text='<%# Bind("schedule_notes") %>'
                                        TextMode="MultiLine" />
                                    </td>
                                    <td colspan="2">
                                        <asp:LinkButton ID="lnkUpdateSchedule" runat="server" Text="Update" CommandName="update"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancelupdate" runat="server" Text="Cancel" CausesValidation="false"
                                        CommandName="cancel"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                        <tr id="trfooter" runat="server">
                            <td valign="top">
                                <asp:DropDownList ID="ddlscheduletype" SkinID="ddlmedium1" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvscheduletype" ToolTip="Select schedule type!"
                                InitialValue="0" runat="server" ControlToValidate="ddlscheduletype"
                                CssClass="asterisk">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtscheduedate" runat="server"  style="text-align: right" OnTextChanged="txtscheduedate_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="cescheduledate" runat="server" TargetControlID="txtscheduedate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RegularExpressionValidator ID="revscheduledate" runat="server"
                                ControlToValidate="txtscheduedate" ForeColor="Red" ToolTip="Enter valid date format(mm/dd/yyyy)"
                                ValidationExpression="^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$">#
                            </asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvpresentername" ToolTip="Select schedule date!"
                                runat="server" ControlToValidate="txtscheduedate"
                                CssClass="asterisk">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblscheduleday"  style="text-align: right" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtschedule_sttime"  style="text-align: right"  OnTextChanged="txtschedule_sttime_TextChanged"
                                ToolTip="Press A/P for AM/PM"
                                AutoPostBack="true" Text="08:00 PM" runat="server" />
                                <ajaxToolkit:MaskedEditExtender ID="medsttime" runat="server" MaskType="Time" ErrorTooltipEnabled="true"
                                UserTimeFormat="None" AcceptAMPM="true"
                                TargetControlID="txtschedule_sttime" Mask="99:99" MessageValidatorTip="true"
                                AutoComplete="true">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:RegularExpressionValidator ID="revsttime" runat="server"
                                ControlToValidate="txtschedule_sttime" ForeColor="Red" ToolTip="Enter valid time(HH:MM AM or PM)"
                                ValidationExpression="^(1[0-2]|0[1-9]):[0-5][0-9]\040(AM|am|PM|pm)$">#
                            </asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtschedule_endtime" Text="11:00 PM" runat="server"  style="text-align: right" ToolTip="Press A/P for AM/PM" />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Time"
                                ErrorTooltipEnabled="true" UserTimeFormat="None" AcceptAMPM="true"
                                TargetControlID="txtschedule_endtime" Mask="99:99" MessageValidatorTip="true"
                                AutoComplete="true" AutoCompleteValue="00:00 AM">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtschedule_endtime" ForeColor="Red" ToolTip="Enter valid time(HH:MM AM or PM)"
                                ValidationExpression="^(1[0-2]|0[1-9]):[0-5][0-9]\040(AM|am|PM|pm)$">#
                            </asp:RegularExpressionValidator>
                            </td>
                            <td class="notes">
                                <asp:TextBox ID="txtschedulenotes" Rows="3" CssClass="notes" runat="server" TextMode="MultiLine" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
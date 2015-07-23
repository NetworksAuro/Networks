<%@ Page Title="Market History Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarketHistoryReport.aspx.cs" Inherits="NTOS.Reports.MarketHistoryReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">      
        function init() {
            var checkboxes = $('#<%=chklstshow.ClientID %>').find('input:checkbox');
            var chkpresenter = $('#<%=chklstpresenter.ClientID %>').find('input:checkbox');
            var chkvenue = $('#<%=chklistvenue.ClientID %>').find('input:checkbox');
            var chkmetro = $('#<%=chklstmetro.ClientID %>').find('input:checkbox');
            var chkcolumns = $('#<%=chklstcolumn.ClientID %>').find('input:checkbox');
            checkboxes.click(function () {
                //debugger;
                var chk = '#' + $('#<%= chklstshow.ClientID %>')[0].id;
                sel(checkboxes.index($(this)), chk, checkboxes[0], $('#<%=txtshow.ClientID%>'));
            });
            chkvenue.click(function () {
                var chk = '#' + $('#<%= chklistvenue.ClientID %>')[0].id;
                sel(chkvenue.index($(this)), chk, chkvenue[0], $('#<%=txtvenue.ClientID%>'));
            });
            chkcolumns.click(function () {
                var chk = '#' + $('#<%= chklstcolumn.ClientID %>')[0].id;
                sel(chkcolumns.index($(this)), chk, chkcolumns[0], $('#<%=txtcolumn.ClientID%>'));
            });
            chkpresenter.click(function () {
                var chk = '#' + $('#<%= chklstpresenter.ClientID %>')[0].id;
                sel(chkpresenter.index($(this)), chk, chkpresenter[0], $('#<%=txtpresenter.ClientID%>'));
            });
            chkmetro.click(function () {
                var chk = '#' + $('#<%= chklstmetro.ClientID %>')[0].id;
                sel(chkmetro.index($(this)), chk, chkmetro[0], $('#<%=txtmetro.ClientID%>'));
            });
        }

        $(document).ready(function cityselect() {
            init();
        });


        function sel(index, chk, thisid, txtbox) {
            var list = "";
            var delimeter = "";
            var all = "";
            var all1 = "";
            var thisall = "";
            var selectedIndex = index;// chk.index($(this));
            //    debugger;
            if (selectedIndex <= 0) {
                if (thisid.checked == true) {
                    var i = 0;

                    $(chk + " input:checkbox").each(function () {

                        if ($(chk).attr('value') != 0) {
                            var cityname = $(chk)[0].rows[i].outerText;
                            this.checked = true;
                            list = "All";
                            i++;
                        }
                    });
                }
                else {
                    $(chk + " input:checkbox").each(function () {
                        this.checked = false;
                    });
                }
            }
            else if (selectedIndex > 0) {
                var j = 0;
                var allflg = 0;

                $(chk + " input:checkbox").each(function () {
                    if ($(chk).attr('value') != 0) {
                        //debugger;
                        var cityname = $(chk)[0].rows[j].outerText;
                        if (j != 0) {

                            if (this.checked == false) {
                                thisall.checked = false;
                                allflg = 1;
                            }
                            else {
                                list = list + delimeter + cityname;
                                delimeter = ",";
                            }
                        }
                        else { thisall = thisid; }
                        j++;
                    }
                });
                if (allflg == 0) { list = "All"; thisall.checked = true; }
            }
            $(txtbox).val(list);
        }



        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            init();
        }
    </script>
    <script type="text/javascript">
        var status = 0;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //debugger;
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null && status == 1) {
                popup.show();

            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
                status = 0;
            }
        }
        function showpop() {
            var isValid = Page_ClientValidate("");
            if (isValid == true)
                status = 1;
        }
    </script>
    <asp:UpdatePanel ID="upmktrpt" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                    <ProgressTemplate>
                        <asp:Image ID="Image1" Width="100px" Height="100px" ImageUrl="~/Images/process1.gif"
                            AlternateText="Processing" runat="server" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                    PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
            <table width="100%" cellpadding="0" cellspacing="0" align="left">

                <tr>
                    <td class="contentpadding">
                        <table width="100%" cellpadding="0" cellspacing="0" align="left" class="tbl_border">
                            <tr>
                                <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33">Market History Report
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table border="0" cellpadding="5">
                                        <tr>
                                            <td align="left">Show&nbsp;:&nbsp;</td>
                                            <td align="left">
                                                <div>
                                                    <asp:TextBox ID="txtshow" runat="server"></asp:TextBox>
                                                    <asp:Panel BorderWidth="1" BackColor="#cccccc" ID="pnlshow" runat="server" Height="95px" ScrollBars="Auto"
                                                        Width="200px">
                                                        <asp:CheckBoxList runat="server" ID="chklstshow" OnSelectedIndexChanged="chklstshow_SelectedIndexChanged" AutoPostBack="true" TextAlign="Right" RepeatDirection="Vertical">
                                                            <asp:ListItem Text="S1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                    <ajaxToolkit:HoverMenuExtender ID="hmeshow"
                                                        runat="server"
                                                        TargetControlID="txtshow"
                                                        PopupControlID="pnlshow"
                                                        PopupPosition="bottom"
                                                        OffsetX="6"
                                                        PopDelay="25">
                                                    </ajaxToolkit:HoverMenuExtender>
                                                </div>
                                            </td>
                                            <td align="left">Venue&nbsp;:&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtvenue" runat="server"></asp:TextBox>
                                                <asp:Panel BackColor="#cccccc" ID="pnlvenue" runat="server" Height="95px" ScrollBars="Auto"
                                                    Width="200px">
                                                    <asp:CheckBoxList runat="server" ID="chklistvenue" TextAlign="Right" RepeatDirection="Vertical">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                                <ajaxToolkit:HoverMenuExtender ID="hmevenue"
                                                    runat="server"
                                                    TargetControlID="txtvenue"
                                                    PopupControlID="pnlvenue"
                                                    PopupPosition="bottom"
                                                    OffsetX="6"
                                                    PopDelay="25">
                                                </ajaxToolkit:HoverMenuExtender>
                                            </td>
                                            <td align="left">Presenter&nbsp;:&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtpresenter" runat="server"></asp:TextBox>
                                                <asp:Panel BackColor="#cccccc" ID="pnlpresenter" runat="server" Height="95px" ScrollBars="Auto"
                                                    Width="200px">
                                                    <asp:CheckBoxList runat="server" ID="chklstpresenter" TextAlign="Right" RepeatDirection="Vertical">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                                <ajaxToolkit:HoverMenuExtender ID="hmepresenter"
                                                    runat="server"
                                                    TargetControlID="txtpresenter"
                                                    PopupControlID="pnlpresenter"
                                                    PopupPosition="bottom"
                                                    OffsetX="6"
                                                    PopDelay="25">
                                                </ajaxToolkit:HoverMenuExtender>
                                            </td>
                                            <td align="left">City&nbsp;:&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtmetro" runat="server"></asp:TextBox>
                                                <asp:Panel BackColor="#cccccc" ID="pnlmetro" runat="server" Height="95px" ScrollBars="Auto"
                                                    Width="200px">
                                                    <asp:CheckBoxList runat="server" ID="chklstmetro" TextAlign="Right" RepeatDirection="Vertical">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                                <ajaxToolkit:HoverMenuExtender ID="hmemetro"
                                                    runat="server"
                                                    TargetControlID="txtmetro"
                                                    PopupControlID="pnlmetro"
                                                    PopupPosition="bottom"
                                                    OffsetX="6"
                                                    PopDelay="25">
                                                </ajaxToolkit:HoverMenuExtender>
                                            </td>
                                            <td align="left">Column&nbsp;:&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtcolumn" runat="server"></asp:TextBox>
                                                <asp:Panel BackColor="#cccccc" ID="pnlcolumn" runat="server" Height="395px" ScrollBars="Auto"
                                                    Width="200px">
                                                    <asp:CheckBoxList runat="server" ID="chklstcolumn" TextAlign="Right" RepeatDirection="Vertical">
                                                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Advertising(Gross)" Value="exp_d_ad_gross_act"></asp:ListItem>
                                                        <asp:ListItem Text="StageHands (Loan-In)" Value="exp_d_stghand_loadin_act"></asp:ListItem>
                                                        <asp:ListItem Text="StageHands (Loan-Out)" Value="exp_d_stghand_loadout_act"></asp:ListItem>
                                                        <asp:ListItem Text="StageHands (Running)" Value="exp_d_stghand_running_act"></asp:ListItem>
                                                        <asp:ListItem Text="Wardrobe and Hair (Load-In)" Value="exp_d_wardrobe_loadin_act"></asp:ListItem>
                                                        <asp:ListItem Text="Wardrobe and Hair (Load-Out)" Value="exp_d_wardrobe_loadout_act"></asp:ListItem>
                                                        <asp:ListItem Text="Wardrobe and Hair (Running)" Value="exp_d_wardrobe_running_act"></asp:ListItem>

                                                        <asp:ListItem Text="Labour Catering" Value="exp_d_labor_catering_act"></asp:ListItem>
                                                        <asp:ListItem Text="Musicians" Value="exp_d_musician_act"></asp:ListItem>
                                                        <asp:ListItem Text="Insurance (On Drop Count)" Value="exp_d_insurance_act"></asp:ListItem>
                                                        <asp:ListItem Text="Ticket Printing Document" Value="exp_d_ticket_print_act"></asp:ListItem>
                                                        <asp:ListItem Text="ADA Expense" Value="exp_l_ada_expense_act"></asp:ListItem>
                                                        <asp:ListItem Text="Box Office" Value="exp_l_bo_act"></asp:ListItem>
                                                        <asp:ListItem Text="Catering" Value="exp_l_catering_act"></asp:ListItem>
                                                        <asp:ListItem Text="Equipment Rental" Value="exp_l_equip_rental_act"></asp:ListItem>
                                                        <asp:ListItem Text="Group Sales Expenses" Value="exp_l_grp_sales_act"></asp:ListItem>
                                                        <asp:ListItem Text="House Staff" Value="exp_l_house_staff_act"></asp:ListItem>
                                                        <asp:ListItem Text="League Fees" Value="exp_l_league_fee_act"></asp:ListItem>
                                                        <asp:ListItem Text="Licenses/Permits" Value="exp_l_license_act"></asp:ListItem>
                                                        <asp:ListItem Text="Limos/Auto" Value="exp_l_limo_act"></asp:ListItem>
                                                        <asp:ListItem Text="Orchestra Shell Removal" Value="exp_l_orchestra_sh_remove_act"></asp:ListItem>
                                                        <asp:ListItem Text="Presenter Profit" Value="exp_l_presenter_profit_act"></asp:ListItem>
                                                        <asp:ListItem Text="Police/Security/Marshall" Value="exp_l_police_act"></asp:ListItem>
                                                        <asp:ListItem Text="Program" Value="exp_l_program_act"></asp:ListItem>
                                                        <asp:ListItem Text="Rent" Value="exp_l_rent_act"></asp:ListItem>
                                                        <asp:ListItem Text="Sound/Lights" Value="exp_l_sound_act"></asp:ListItem>
                                                        <asp:ListItem Text="Ticket Printing Local" Value="exp_l_ticket_print_act"></asp:ListItem>
                                                        <asp:ListItem Text="Telephones/Internet" Value="exp_l_phone_act"></asp:ListItem>
                                                        <asp:ListItem Text="Dry ICE/C02" Value="exp_l_dryice_act"></asp:ListItem>
                                                        <asp:ListItem Text="Local Fixed" Value="exp_l_local_fixed_act"></asp:ListItem>
                                                        <asp:ListItem Text="Other 1" Value="exp_d_other_1_act"></asp:ListItem>
                                                        <asp:ListItem Text="Other 2" Value="exp_d_other_2_act"></asp:ListItem>
                                                        <asp:ListItem Text="Other 3" Value="exp_d_other_3_act"></asp:ListItem>
                                                        <asp:ListItem Text="Other 4" Value="exp_d_other_4_act"></asp:ListItem>
                                                        <asp:ListItem Text="Other 5" Value="exp_d_other_5_act"></asp:ListItem>
                                                        <asp:ListItem Text="Subscription Sales" Value="bo_sub_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Phone Sales" Value="bo_ph_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Internet Sales" Value="bo_web_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Credit card Sales" Value="bo_cc_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Remote/Outlet Sales" Value="bo_outlet_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Single Tix Sales" Value="bo_single_tix_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Group (<$50,000) Sales" Value="bo_small_group_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Group (>$50,000) Sales" Value="bo_large_group_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Other %" Value="bo_other_per_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Other $" Value="bo_other_usd_gross_rcpt"></asp:ListItem>
                                                        <asp:ListItem Text="Tax 1" Value="deal_tax_ptg"></asp:ListItem>
                                                        <asp:ListItem Text="Tax Amount Over" Value="deal_tax_amt_over"></asp:ListItem>
                                                        <asp:ListItem Text="Gross Sales" Value="grosssale"></asp:ListItem>
                                                        <asp:ListItem Text="Drop Count" Value="dropcount"></asp:ListItem>
                                                        <asp:ListItem Text="Paid Attendance" Value="paidattendance"></asp:ListItem>
                                                        <asp:ListItem Text="Comps" Value="COMPS"></asp:ListItem>
                                                        <asp:ListItem Text="Facility Fee Each Ticket" Value="deal_facility_fee_amt"></asp:ListItem>
                                                        <asp:ListItem Text="Tax/Facility Fee Commission" Value="deal_facility_fee_unit"></asp:ListItem>
                                                        <asp:ListItem Text="Royalty" Value="deal_royalty_income"></asp:ListItem>
                                                        <asp:ListItem Text="Guarantee" Value="deal_guarantee_income"></asp:ListItem>
                                                        <asp:ListItem Text="Company Middle Monies" Value="deal_cmpny_mid_monies_ptg"></asp:ListItem>
                                                        <asp:ListItem Text="Presenter Middle Monies" Value="deal_presenter_mid_monies_ptg"></asp:ListItem>
                                                        <asp:ListItem Text="Producer Share of Split" Value="deal_producer_share_split_ptg"></asp:ListItem>
                                                        <asp:ListItem Text="Presenter Share of Split" Value="deal_presenter_share_split_ptg"></asp:ListItem>
                                                        <asp:ListItem Text="Income With Holiday Tax" Value="deal_incm_wthd_tax_act_amt"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                                <ajaxToolkit:HoverMenuExtender ID="hmecolumn"
                                                    runat="server"
                                                    TargetControlID="txtcolumn"
                                                    PopupControlID="pnlcolumn"
                                                    PopupPosition="bottom"
                                                    OffsetX="6"
                                                    PopDelay="25">
                                                </ajaxToolkit:HoverMenuExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">From&nbsp;:&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtfromdate" runat="server" onchange="highlight();"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvfromdate" runat="server" ErrorMessage="*" ToolTip="Select from date!" ControlToValidate="txtfromdate"
                                                    CssClass="asterisk"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:CalendarExtender ID="cefromdate" runat="server" TargetControlID="txtfromdate">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:CompareValidator runat="server" ID="cvfromdate" Type="Date" ControlToValidate="txtfromdate"
                                                    Operator="DataTypeCheck"
                                                    ErrorMessage="#" ForeColor="Red" ToolTip="Enter valid date (mm/dd/yyyy)"></asp:CompareValidator>
                                            </td>
                                            <td align="left">To&nbsp;:&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtToDate" runat="server" onchange="highlight();"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtodate" runat="server" ErrorMessage="*" ToolTip="Select to date!" ControlToValidate="txtToDate"
                                                    CssClass="asterisk"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:CalendarExtender ID="cetodate" runat="server" TargetControlID="txtToDate">
                                                </ajaxToolkit:CalendarExtender>
                                                 <asp:CompareValidator runat="server" ID="cvtodate" Type="Date" ControlToValidate="txtToDate"
                                                    Operator="DataTypeCheck"
                                                    ErrorMessage="#" ForeColor="Red" ToolTip="Enter valid date (mm/dd/yyyy)"></asp:CompareValidator>
                                            </td>
                                            <td align="right" colspan="6">
                                                <asp:Button ID="btnExtract" runat="server" Text="Extract" OnClientClick="showpop();" OnClick="btnExtract_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="padding_leftright10" align="center">
                                    <rsweb:ReportViewer ID="rptviewer" Width="100%" runat="server" WaitControlDisplayAfter="0"
                                        AsyncRendering="False">
                                    </rsweb:ReportViewer>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>













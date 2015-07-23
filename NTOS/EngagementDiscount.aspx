<%@ Page Title="Engagement Discounts" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Engagement.Master"
    Language="C#"
    AutoEventWireup="true" CodeBehind="EngagementDiscount.aspx.cs" Inherits="NTOS.EngagementDiscount" %>

<%@ Register Src="~/Docx.ascx" TagName="Docx" TagPrefix="UC" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <style  type="text/css">

         .BlueSpan label
{
    background-color:#B8B8B8;
    color:#ffffff;
    display:inline-block;
    width:300px;
}

.YellowSpan label
{
    background-color:	#A0A0A0 ;
    color:#000000;
    width:300px;
     display:inline-block;
}
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/jscript" src="Scripts/autoNumeric 1.9.15.js">
</script>
            <script type="text/javascript">

                jQuery(function ($) {
                    $('.Numeric').autoNumeric({ vMax: '999999999999999', vMin: '-999999999999999' });
                    $('.smallint').autoNumeric({ aSep: '', vMax: '32767', vMin: '0' });
                    $('.Dollar').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99' });
                    $('.Percentage').autoNumeric({ aSign: '%', pSign: 's', vMax: '100' });
                    $('.Percentage1').autoNumeric({ aSign: '%', pSign: 's', vMax: '100.000000' });

                });

                function sysmbols() {
                    $('.Numeric').autoNumeric({ vMax: '999999999999999', vMin: '-999999999999999' });
                    $('.smallint').autoNumeric({ aSep: '', vMax: '32767', vMin: '0' });
                    $('.Dollar').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99' });
                    $('.Percentage').autoNumeric({ aSign: '%', pSign: 's', vMax: '100' });
                    $('.Percentage1').autoNumeric({ aSign: '%', pSign: 's', vMax: '100.000000' });
                }
            </script>
            <script type="text/javascript">

                var ddlperindex = 0;
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

                function EndRequestHandler(sender, args) {
                    init();
                    sysmbols();
                   
                }
                function chk_changes(thisid) {
                    //debugger;

                    if ($('#hdn_modify_status').val() == 1) {
                        if (confirm('Data not Saved! Want to Exit?') == true) {
                            $('#hdn_modify_status').val("0");
                            ddlperindex = thisid.selectedIndex;
                            return true;
                        }
                        else {
                            thisid.selectedIndex = ddlperindex;
                            return false;
                        }
                    }
                    ddlperindex = thisid.selectedIndex;
                    return true;
                }
                $("#Form1").on("change", ":input", function () {
                    //debugger;
                    if ($('#<%=ddlPerformance.ClientID%>')[0] != undefined) {
                        if (this.id != $('#<%=ddlPerformance.ClientID%>')[0].id)
                            $('#hdn_modify_status').val("1");
                        else
                            $('#hdn_modify_status').val("0");
                    }
                });
            </script>
            <script type="text/javascript">

                $(document).ready(function () {
                    init();
                });
                function init() {
                    var chkcolumns = $('#<%=chklstPerformance.ClientID %>').find('input:checkbox');
                    chkcolumns.click(function () {
                        var chk = '#' + $('#<%= chklstPerformance.ClientID %>')[0].id;
                        sel(chkcolumns.index($(this)), chk, chkcolumns[0], $('#<%=txtPerformance.ClientID%>'));
                    });
                }
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

            </script>
            <div align="center">
                <asp:Label ID="lbl_bo" runat="server" Text="Please Enter Engagement Schedule First"
                ForeColor="Red"></asp:Label>
            </div>
            <center>
                <%--  <div runat="server" id="div_bo" visible="false">
                //under development
            </div>--%>
                <asp:HiddenField ID="hdn_engagementid" runat="server" />
                <asp:HiddenField ID="hdn_schedulecount" runat="server" />
            </center>
            <div id="divDiscount" runat="server">
                <table cellpadding="2" cellspacing="0"  width ="1500px">
                    
                    <tr>
                        <td colspan="9" align="left" style="padding-left:0px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPerformance" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList SkinID="ddlmedium1" ID="ddlPerformance" OnChange="if(!chk_changes(this)) return false;"
                                            AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPerformance_SelectedIndexChanged">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Panel  ID="pnlcolumn" BorderStyle="Solid" runat="server" Height="250px" ScrollBars="Auto"
                                            Width="320px ">
                                            <asp:CheckBoxList runat="server" ID="chklstPerformance" TextAlign="Right" OnDataBound="chklstPerformance_DataBound" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <ajaxToolkit:HoverMenuExtender ID="hmecolumn"
                                            runat="server"
                                            TargetControlID="txtPerformance"
                                            PopupControlID="pnlcolumn"
                                            PopupPosition="bottom"
                                            OffsetX="6"
                                            PopDelay="25">
                                        </ajaxToolkit:HoverMenuExtender>
                                    </td>
                                    <td align="left" runat="server" id="tdcopyfrom" style="font-weight: normal; font-size: initial"><a href="#" class="lnkcopyfrom" id="lnkcopy" runat="server">
                                        <asp:Label ID="lblcopyfrom" runat="server" Text="Copy From"></asp:Label>
                                        </a>
                                        <asp:Panel BackColor="#cccccc" ID="pnlCity" runat="server" Height="95px" ScrollBars="Auto"
                                            HorizontalAlign="Left"
                                            Width="200px">
                                            <asp:RadioButtonList ID="rbtnlstperformance" runat="server" OnSelectedIndexChanged="rbtnlstperformance_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:RadioButtonList>
                                        </asp:Panel>
                                        <ajaxToolkit:HoverMenuExtender ID="hvrmnucopyfrom"
                                            runat="server"
                                            TargetControlID="lblcopyfrom"
                                            PopupControlID="pnlCity"
                                            PopupPosition="Right"
                                            OffsetX="6"
                                            PopDelay="25">
                                        </ajaxToolkit:HoverMenuExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                        <td colspan="3"></td>
                        <td colspan="3"></td>
                    </tr>
                       <tr>
                        <td colspan="3"></td>
                        <td colspan="3"></td>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left" style="padding-left:105px; padding-bottom:20px;">
                            <asp:Label ID="lblDemandPricing" runat="server" Text="Demand Pricing"></asp:Label>
                            &nbsp;<asp:TextBox ID="txtDemandPricing" runat="server" CssClass="Dollar"
                                TabIndex="62" Width="100px"></asp:TextBox>
                        </td>
                       
                        <td colspan="3" align="center"></td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="3">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <label class="heading">
                                        Subscription</label></td>
                                    <td colspan="2" width="160px" class="lineright">&nbsp;
                                        <asp:ImageButton ID="lnkbtnSubsAdd0" runat="server" ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px" OnClick="lnkbtnSubsAdd_Click" ToolTip="Add">
                                        </asp:ImageButton>
                                        <asp:ImageButton ID="lnkbtnSubsDelete0" runat="server" CausesValidation="false" ImageUrl="~/Images/minus.png"  OnClick="lnkbtnSubsDelete_Click"  ToolTip="Delete" Height="15px" Width="20px">
                                        </asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <th width="100px" style="padding-left: 45px"></th>
                                    <th align="right" width="60px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; %</th>
                                    <th align="right" width="100px" class="lineright" style="padding-left:34px;">Tickets&nbsp;<asp:CheckBox ID="chkbxSubsTkAll"
                                            runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkbxSubsTkAll_CheckedChanged" />
                                    </th>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label ID="lblsubs1" runat="server" Text="1"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox CssClass="Percentage" Width="50px" ID="txtSubs1Perc" runat="server"
                                                TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs1Tickets" runat="server"
                                                TabIndex="2"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fltx" TargetControlID="txtSubs1Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs2" runat="server" Text="2"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs2Perc"
                                                TabIndex="3" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs2Tickets"
                                                runat="server"
                                                TabIndex="4"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtSubs2Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk2" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs3" runat="server" Text="3"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs3Perc"
                                                TabIndex="5" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs3Tickets"
                                                runat="server"
                                                TabIndex="6"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtSubs3Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk3" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs4" runat="server" Text="4"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs4Perc"
                                                TabIndex="7" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs4Tickets"
                                                runat="server"
                                                TabIndex="8"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtSubs4Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk4" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs5" runat="server" Text="5"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs5Perc"
                                                TabIndex="9" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs5Tickets"
                                                runat="server"
                                                TabIndex="10"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="txtSubs5Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk5" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs6" runat="server" Text="6"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs6Perc"
                                                TabIndex="11" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs6Tickets"
                                                runat="server"
                                                TabIndex="12"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtSubs6Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk6" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs7" runat="server" Text="7"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs7Perc"
                                                TabIndex="13" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs7Tickets"
                                                runat="server"
                                                TabIndex="14"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" TargetControlID="txtSubs7Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk7" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs8" runat="server" Text="8"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs8Perc"
                                                TabIndex="15" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs8Tickets"
                                                runat="server"
                                                TabIndex="16"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtSubs8Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk8" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs9" runat="server" Text="9"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs9Perc"
                                                TabIndex="17" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs9Tickets"
                                                runat="server"
                                                TabIndex="18"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" TargetControlID="txtSubs9Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk9" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="100px" style="padding-left: 45px">
                                        <asp:Label Visible="false" ID="lblsubs10" runat="server" Text="10"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" CssClass="Percentage" Width="50px" ID="txtSubs10Perc"
                                                TabIndex="19" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtSubs10Tickets"
                                                runat="server"
                                                TabIndex="20"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" TargetControlID="txtSubs10Tickets"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxSubsTk10" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="3" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right" width="100px">
                                        <label class="heading">
                                        Group</label> </td>
                                    <td colspan="2" width="160px" class="lineright">&nbsp;
                                        <asp:ImageButton ID="lnkGroupAdd0" runat="server" ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px" OnClick="lnkGroupAdd_Click"  ToolTip="Add">
                                        </asp:ImageButton>
                                        <asp:ImageButton ID="lnkGroupDelete0" runat="server" CausesValidation="false"  OnClick="lnkGroupDelete_Click" ImageUrl="~/Images/minus.png"   Height ="15px" Width ="20px" ToolTip="Delete">
                                        </asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <th align="center" width="100px"></th>
                                    <th align="center" width="60px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; %</th>
                                    <th align="center" width="100px" class="lineright" style="padding-left:24px;">Tickets &nbsp;&nbsp;<asp:CheckBox ID="chkbxGrouplessTkAll"
                                            runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkbxGrouplessTkAll_CheckedChanged" />
                                    </th>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="true" ID="lblSale1" runat="server" Text=" 1"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Width="50px" CssClass="Percentage" TabIndex="22" ID="txtgrouplessPerc1"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Width="50px" CssClass="smallint"  style="text-align: right" ID="txtGrouplessTickets1" runat="server"
                                                TabIndex="23"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtGrouplessTickets1"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale2" runat="server" Text=" 2"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" ID="txtgrouplessPerc2"
                                                TabIndex="24"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtGrouplessTickets2"
                                                TabIndex="25"
                                                runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtGrouplessTickets2"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk2" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale3" runat="server" Text=" 3"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" TabIndex="26" ID="txtgrouplessPerc3"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right"  CssClass="smallint" ID="txtGrouplessTickets3"
                                                runat="server"
                                                TabIndex="27"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" TargetControlID="txtGrouplessTickets3"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk3" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale4" runat="server" Text=" 4"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" TabIndex="28" ID="txtgrouplessPerc4"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtGrouplessTickets4"
                                                runat="server"
                                                TabIndex="29"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" TargetControlID="txtGrouplessTickets4"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk4" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale5" runat="server" Text=" 5"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" TabIndex="30" ID="txtgrouplessPerc5"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtGrouplessTickets5"
                                                runat="server"
                                                TabIndex="31"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" TargetControlID="txtGrouplessTickets5"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk5" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale6" runat="server" Text=" 6"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" TabIndex="32" ID="txtgrouplessPerc6"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtGrouplessTickets6"
                                                runat="server"
                                                TabIndex="33"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" TargetControlID="txtGrouplessTickets6"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk6" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale7" runat="server" Text=" 7"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" TabIndex="34" ID="txtgrouplessPerc7"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtGrouplessTickets7"
                                                runat="server"
                                                TabIndex="35"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" TargetControlID="txtGrouplessTickets7"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk7" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale8" runat="server" Text=" 8"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" TabIndex="36" ID="txtgrouplessPerc8"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtGrouplessTickets8"
                                                runat="server"
                                                TabIndex="37"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" TargetControlID="txtGrouplessTickets8"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk8" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale9" runat="server" Text=" 9"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" TabIndex="38" ID="txtgrouplessPerc9"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtGrouplessTickets9"
                                                runat="server"
                                                TabIndex="39"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" TargetControlID="txtGrouplessTickets9"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk9" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px;" align="right" width="100px">
                                        <asp:Label Visible="false" ID="lblSale10" runat="server" Text=" 10"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox Visible="false" Width="50px" CssClass="Percentage" TabIndex="40" ID="txtgrouplessPerc10"
                                                runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 150px; padding-right: 10px;" align="center" width="100px" class="lineright">
                                        <asp:TextBox Visible="false" Width="50px"  style="text-align: right" CssClass="smallint" ID="txtGrouplessTickets10"
                                                runat="server"
                                                TabIndex="41"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" TargetControlID="txtGrouplessTickets10"
                                                runat="server" FilterType="Custom,Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxGrouplessTk10" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="3" valign="top">
                            <table width="100%">
                                <tr>
                                    <td align="right" width="100px">
                                        <label class="heading">
                                        Miscellaneous</label> </td>
                                    <td colspan="2" width="160px">&nbsp;
                                                     <asp:ImageButton ID="lnkbtnMiscAdd" runat="server" Height="15px" ImageUrl="~/Images/plus.png" OnClick="lnkbtnMiscAdd_Click" ToolTip="Add" Width="20px" />
                                        <asp:ImageButton ID="lnkbtnMiscDelete" runat="server" CausesValidation="false" Height="15px" ImageUrl="~/Images/minus.png" OnClick="lnkbtnMiscDelete_Click" ToolTip="Delete" Width="20px" />
                                    </td>
                                </tr>
                                <tr>
                                    <th style="padding-left: 10px; width: 50px" width="100px"></th>
                                    <th align="center" width="60px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;%</th>
                                    <th align="center" width="150px" style="padding-left:58px;">Tickets
                                                <asp:CheckBox ID="chkbxMiscPercAll" runat="server" AutoPostBack="true" Checked="false" OnCheckedChanged="chkbxMiscPercAll_CheckedChanged" />
                                    </th>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous1" runat="server" Text="1" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc1" runat="server" CssClass="Percentage" TabIndex="42" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets1" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="43" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets1">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc1" runat="server" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous2" runat="server" Text="2" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc2" runat="server" CssClass="Percentage" TabIndex="44" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets2" runat="server" CssClass="smallint" TabIndex="45"  style="text-align: right" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets2">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc2" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous3" runat="server" Text="3" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc3" runat="server" CssClass="Percentage" TabIndex="46" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets3" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="47" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets3">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc3" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous4" runat="server" Text="4" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc4" runat="server" CssClass="Percentage" TabIndex="48" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets4" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="49" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets4">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc4" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous5" runat="server" Text="5" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc5" runat="server" CssClass="Percentage" TabIndex="50" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets5" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="51" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets5">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc5" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous6" runat="server" Text="6" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc6" runat="server" CssClass="Percentage" TabIndex="52" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets6" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="53" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets6">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc6" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous7" runat="server" Text="7" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc7" runat="server" CssClass="Percentage" TabIndex="54" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets7" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="55" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets7">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc7" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous8" runat="server" Text="8" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc8" runat="server" CssClass="Percentage" TabIndex="56" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets8" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="57" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets8">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc8" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous9" runat="server" Text="9" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc9" runat="server" CssClass="Percentage" TabIndex="58" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets9" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="59" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets9">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc9" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 30px">
                                    <td align="right" style="padding-left: 10px" width="100px">
                                        <asp:Label ID="lblMiscellaneous10" runat="server" Text="10" Visible="false" Width="50px"></asp:Label>
                                    </td>
                                    <td align="center" width="60px">
                                        <asp:TextBox ID="txtMiscellaneousPerc10" runat="server" CssClass="Percentage" TabIndex="60" Visible="false" Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center" width="150px">
                                        <asp:TextBox ID="txtMiscellaneousTickets10" runat="server" CssClass="smallint"  style="text-align: right" TabIndex="61" Visible="false" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" FilterType="Custom,Numbers" TargetControlID="txtMiscellaneousTickets10">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:CheckBox ID="chkbxMiscPerc10" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                    </tr>
                </table>
                </td>
                <td>
                    <table>
                        <tr>
                            <td valign="top" style="padding-left:45px">Notes:&nbsp;</td>
                            <td align="left" valign="bottom">
                                <asp:TextBox ID="txtnotes" runat="server" TextMode="MultiLine" Rows="6" Columns="50"
                                TabIndex="62"></asp:TextBox>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <UC:Docx ID="ucdocx" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr>
                        <td colspan="9" align="center">
                            <asp:ValidationSummary ID="val_summary_deal" runat="server" Font-Bold="true" ForeColor="Orange"
                                HeaderText="Field validations failed. Please check and correct" DisplayMode="SingleParagraph" />
                            &nbsp;
                                <asp:Label ID="lbl_staticmsg" runat="server" ForeColor="Orange" Font-Bold="true" />
                            <asp:Label ID="lbl_message" runat="server" ForeColor="Orange" Font-Bold="true" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

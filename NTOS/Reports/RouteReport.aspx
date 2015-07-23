<%@ Page Title="Route Report" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" 
    CodeBehind="RouteReport.aspx.cs" Inherits="NTOS.Reports.RouteReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function init() {
            var chkcolumns = $('#<%=chklstcolumn.ClientID %>').find('input:checkbox');
            chkcolumns.click(function () {
                var chk = '#' + $('#<%= chklstcolumn.ClientID %>')[0].id;
                sel(chkcolumns.index($(this)), chk, chkcolumns[0], $('#<%=txtcolumn.ClientID%>'));
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



        function ValidateCityList(source, args) {
            var chkListModules = document.getElementById('<%= chkCity.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
    <script type="text/javascript">
        function comparedate(date1, date2, msg) {
            var d1 = $('#' + date1).val();
            var d2 = $('#' + date2).val();
            dateN1 = new Date(d1.split("/")[2], d1.split("/")[0] - 1, d1.split("/")[1]);
            dateN2 = new Date(d2.split("/")[2], d2.split("/")[0] - 1, d2.split("/")[1]);
            if (dateN1 > dateN2) {
                alert(msg);
                $('#' + date1).val("");
                $('#' + date2).val("");
                return false;
            }
            return true;
        }
        function checkdatemax(date1, msg) {
            var txt1 = $('#' + date1);
            var d1 = txt1.val();
            date1 = new Date(d1.split("/")[2], d1.split("/")[0] - 1, d1.split("/")[1]);
            date2 = new Date();
            if (date1 > date2) {
                alert(msg);
                txt1.val("");
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            chkCityselected();
            init();

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function cityselect() {
            //function chkCityselected() {
            init();

            var checkboxes = $('#<%=chkCity.ClientID %>').find('input:checkbox');
            checkboxes.click(function () {
                var list = "";
                var delimeter = "";
                var all = "";
                var all1 = "";
                var thisall;
                var selectedIndex = checkboxes.index($(this));

                if (selectedIndex <= 0) {
                    if (this.checked == true) {
                        var i = 0;

                        $('#<%= chkCity.ClientID %>' + " input:checkbox").each(function () {

                            if ($('#<%= chkCity.ClientID %>').attr('value') != 0) {
                                var cityname = $('#<%= chkCity.ClientID %>')[0].rows[i].outerText;
                                this.checked = true;
                                list = "All";
                                i++;
                            }
                        });
                    }
                    else {
                        $('#<%= chkCity.ClientID %>' + " input:checkbox").each(function () {
                            this.checked = false;
                        });
                    }
                }
                else if (selectedIndex > 0) {
                    var j = 0;
                    var allflg = 0;

                    $('#<%= chkCity.ClientID %>' + " input:checkbox").each(function () {
                        if ($('#<%= chkCity.ClientID %>').attr('value') != 0) {
                            //debugger;
                            var cityname = $('#<%= chkCity.ClientID %>')[0].rows[j].outerText;
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
                            else { thisall = this; }

                            j++;
                        }
                    });
                    if (allflg == 0) { list = "All"; thisall.checked = true; }
                }
                $('#<%=txtCity.ClientID%>').val(list);


            });

        });




        function chkCityselected() {


            var checkboxes = $('#<%=chkCity.ClientID %>').find('input:checkbox');
            checkboxes.click(function () {
                var list = "";
                var delimeter = "";
                var all = "";
                var all1 = "";
                var thisall;
                var selectedIndex = checkboxes.index($(this));

                if (selectedIndex <= 0) {
                    if (this.checked == true) {
                        var i = 0;

                        $('#<%= chkCity.ClientID %>' + " input:checkbox").each(function () {

                            if ($('#<%= chkCity.ClientID %>').attr('value') != 0) {
                                var cityname = $('#<%= chkCity.ClientID %>')[0].rows[i].outerText;
                                this.checked = true;
                                list = "All";
                                i++;
                            }
                        });
                    }
                    else {
                        $('#<%= chkCity.ClientID %>' + " input:checkbox").each(function () {
                            this.checked = false;
                        });
                    }
                }
                else if (selectedIndex > 0) {
                    var j = 0;
                    var allflg = 0;

                    $('#<%= chkCity.ClientID %>' + " input:checkbox").each(function () {
                        if ($('#<%= chkCity.ClientID %>').attr('value') != 0) {
                            //debugger;
                            var cityname = $('#<%= chkCity.ClientID %>')[0].rows[j].outerText;
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
                            else { thisall = this; }
                            j++;
                        }
                    });
                    if (allflg == 0) { list = "All"; thisall.checked = true; }
                }
                $('#<%=txtCity.ClientID%>').val(list);
            });
        }
    </script>



    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <asp:UpdatePanel ID="upl1" runat="server" RenderMode="Block">
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
                                <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33">Route Report
                                </td>
                            </tr>
                            <tr>
                                <td align="center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td class="padding_leftright10">Show &nbsp;:

                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlShow" runat="server" onchange="highlight();" Width="150px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvShow" runat="server" ControlToValidate="ddlShow"
                                                    InitialValue="-Select-"
                                                    CssClass="asterisk" ToolTip="Select Show!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="padding_leftright10">From</td>
                                            <td valign="top">:&nbsp;<asp:TextBox ID="txtAsofDate" runat="server" onchange="highlight();"></asp:TextBox>
                                                <asp:CompareValidator runat="server" ID="cvfromdate" Type="Date" ControlToValidate="txtAsofDate"
                                                    Operator="DataTypeCheck"
                                                    ErrorMessage="#" ForeColor="Red" ToolTip="Enter valid date (mm/dd/yyyy)"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="rfvAsofDate" runat="server" ControlToValidate="txtAsofDate"
                                                    CssClass="asterisk" ToolTip="Enter As of date!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:CalendarExtender ID="cldrAsofDate" runat="server" TargetControlID="txtAsofDate">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fltrFromdate" TargetControlID="txtAsofDate"
                                                    runat="server" FilterType="Numbers,Custom" ValidChars="[ /]">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td class="padding_leftright10">To</td>
                                            <td valign="top">:&nbsp;<asp:TextBox ID="txtTodate" onchange="highlight();" runat="server"></asp:TextBox>
                                                <asp:CompareValidator runat="server" ID="cvtodate" Type="Date" ControlToValidate="txtTodate"
                                                    Operator="DataTypeCheck"
                                                    ErrorMessage="#" ForeColor="Red" ToolTip="Enter valid date (mm/dd/yyyy)"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="rfvTodate" runat="server" ControlToValidate="txtTodate"
                                                    CssClass="asterisk" ToolTip="Enter To date!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:CalendarExtender ID="cldr" runat="server" TargetControlID="txtTodate">
                                                </ajaxToolkit:CalendarExtender>

                                            </td>
                                            <td class="padding_leftright10">City/State&nbsp;:</td>
                                            <td align="left" valign="top">
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                            <asp:Panel BackColor="#cccccc" ID="pnlCity" runat="server" Height="95px" ScrollBars="Auto"
                                                                Width="200px">
                                                                <asp:CheckBoxList runat="server" ID="chkCity" TextAlign="Right" RepeatDirection="Vertical">
                                                                </asp:CheckBoxList>
                                                            </asp:Panel>
                                                            <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1"
                                                                runat="server"
                                                                TargetControlID="txtCity"
                                                                PopupControlID="pnlCity"
                                                                PopupPosition="bottom"
                                                                OffsetX="6"
                                                                PopDelay="25">
                                                            </ajaxToolkit:HoverMenuExtender>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:CustomValidator CssClass="asterisk" runat="server" ID="cvmodulelist"
                                                                ClientValidationFunction="ValidateCityList"
                                                                ErrorMessage="*" ToolTip="Please Select Atleast one City"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left">Column&nbsp;:&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtcolumn" runat="server"></asp:TextBox>
                                                <asp:Panel BackColor="#cccccc" ID="pnlcolumn" runat="server" Height="395px" ScrollBars="Auto"
                                                    Width="200px">
                                                    <asp:CheckBoxList runat="server" ID="chklstcolumn" TextAlign="Right" RepeatDirection="Vertical">
                                                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Mileage" Value="mileage"></asp:ListItem>
                                                        <asp:ListItem Text="Timezone" Value="timezone"></asp:ListItem>
                                                        <asp:ListItem Text="Show-Times" Value="showtimes"></asp:ListItem>
                                                        <asp:ListItem Text="No Of Performance" Value="noofperformance"></asp:ListItem>
                                                        <asp:ListItem Text="Venue" Value="venue"></asp:ListItem>
                                                        <asp:ListItem Text="Presenter" Value="presenter"></asp:ListItem>
                                                        <asp:ListItem Text="Fixed Guarantee" Value="fixedguarantee"></asp:ListItem>
                                                        <asp:ListItem Text="Royalty" Value="royalty"></asp:ListItem>
                                                        <asp:ListItem Text="Backend" Value="backend"></asp:ListItem>
                                                        <asp:ListItem Text="Notes" Value="notes"></asp:ListItem>
                                                        <asp:ListItem Text="Est Royalty" Value="estroyalty"></asp:ListItem>
                                                        <asp:ListItem Text="On Subcription" Value="onsubcription"></asp:ListItem>
                                                        <asp:ListItem Text="Date Confirmed" Value="dateconfirmed"></asp:ListItem>
                                                        <asp:ListItem Text="Offer" Value="offer"></asp:ListItem>
                                                        <asp:ListItem Text="Price Scales" Value="pricescales"></asp:ListItem>
                                                        <asp:ListItem Text="Expense" Value="expense"></asp:ListItem>
                                                        <asp:ListItem Text="Deal Memo " Value="dealmemo "></asp:ListItem>
                                                        <asp:ListItem Text="Contract" Value="contract"></asp:ListItem>
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
                                            <td valign="top"></td>
                                            <td valign="top" align="right">
                                                <asp:Button ID="btnExtract" runat="server" Text="Extract" OnClick="btnExtract_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Orange" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td align="center">
                                    <table style="width: 100%">
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
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

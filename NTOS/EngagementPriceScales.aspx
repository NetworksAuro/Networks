<%@ Page Title="Engagement Price Scales" MasterPageFile="~/Engagement.Master" Language="C#"
    AutoEventWireup="true" CodeBehind="EngagementPriceScales.aspx.cs" Inherits="NTOS.EngagementPriceScales" %>

<%@ Register Src="~/PriceScale.ascx" TagPrefix="UC" TagName="PS" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .divsec {
            margin: 5px 0 0 0;
           
            width: 100%;
        }

        .btnsection {
            cursor: pointer;
            border: solid 1px;
        }

        .tog {
            border: solid 1px;
            padding: 0 0 20px 0;
            margin: 0px;
        }

            .tog p {
                cursor: pointer;
                color: green;
                font-weight: bold;
                font-size: 10px;
                border: solid 1px;
                width: 10px;
                padding: 2px;
                text-align: center;
                vertical-align: middle;
                margin: 0px 0 0 0;
            }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript" language="javascript">
        var tot_gross_per_wk = "0";
        function cal() {
            tot_gross_per_wk = "0";
            var ps_selected_count = 0;
            set_class();
            var ucid1 = $('#<%= uc_ps1.ClientID %>');
            var dindex = ucid1.selector.slice(ucid1.selector.length - 2, ucid1.selector.length).replace('s', '');
            var content_id = ucid1.selector.replace('uc_ps' + dindex, '');
            //debugger;

            for (var j = 1; j <= 20; j++) {
                var chklstps = $(content_id + 'chk_ps' + j);
                ps_selected_count += $(chklstps[0]).find("input:checked").length;
                var gv1 = $(content_id + 'uc_ps' + j + '_gdv_ps1');
                var ucid = $(content_id + 'uc_ps' + j);
                calgrid(gv1, ucid);
            }

            var lbltotgrperwk = $('#<%= lbl_pstotal.ClientID %>');
            var lbltottax = $('#<%= lbl_pssubtotal.ClientID %>');
            var lbltaxper =parseFloat($('#<%= lbl_pstax.ClientID %>').html());
            var lblgrossafttax = $('#<%= lblgrossafttax.ClientID %>');
            lbltotgrperwk.html(tot_gross_per_wk);
            lbltotgrperwk.autoNumeric('set', (parseFloat(tot_gross_per_wk)));
            lbltottax.autoNumeric('set', (parseFloat(tot_gross_per_wk) * parseFloat(lbltaxper) / 100));
            lblgrossafttax.autoNumeric('set', (parseFloat($(lbltotgrperwk).autoNumeric('get')) - parseFloat($(lbltottax).autoNumeric('get'))));
        }
        function calgrid(gv, ucid) {
            // debugger;
            var seattotal = 0, saleamttotal = 0;
            for (var i = 0; i < gv[0].rows.length - 1; i++) {
                var seat = 0, tktprice = 0, salesamt = 0, sdiscount = 0, gdiscount = 0, sprice = 0, gprice = 0;
                var txt_ps1_seats = $('#' + gv[0].id + "_txt_ps1_seats_" + i);
                var txt_ps1_ticketprice = $('#' + gv[0].id + "_txt_ps1_ticketprice_" + i);
                var lbl_ps1_saleamount = $('#' + gv[0].id + "_lbl_ps1_saleamount_" + i);
                var drp_ps1_sunit = $('#' + gv[0].id + "_drp_ps1_sunit_" + i);
                var txt_ps1_sdiscount = $('#' + gv[0].id + "_txt_ps1_sdiscount_" + i);
                var lbl_ps1_sprice = $('#' + gv[0].id + "_lbl_ps1_sprice_" + i);
                var drp_ps1_gunit = $('#' + gv[0].id + "_drp_ps1_gunit_" + i);
                var txt_ps1_gdiscount = $('#' + gv[0].id + "_txt_ps1_gdiscount_" + i);
                var lbl_ps1_gprice = $('#' + gv[0].id + "_lbl_ps1_gprice_" + i);
                seat = (!isNaN(parseFloat(txt_ps1_seats.val()))) ? txt_ps1_seats.val() : seat;
                tktprice = (txt_ps1_ticketprice.autoNumeric('get')) ? txt_ps1_ticketprice.autoNumeric('get') : tktprice;
                tktprice = (!isNaN(tktprice)) ? tktprice : "0";
                salesamt = (parseFloat(seat) * parseFloat(tktprice));
                lbl_ps1_saleamount.autoNumeric('set', salesamt);
                sdiscount = (!isNaN(parseFloat(txt_ps1_sdiscount.val()))) ? txt_ps1_sdiscount.val() : sdiscount;
                gdiscount = (!isNaN(parseFloat(txt_ps1_gdiscount.val()))) ? txt_ps1_gdiscount.val() : gdiscount;
                if (drp_ps1_sunit[0].selectedIndex == 0) { sprice = (1 - sdiscount / 100) * tktprice; } else { sprice = (parseFloat(sdiscount) > 0) ? tktprice - sdiscount : "0"; }
                if (drp_ps1_gunit[0].selectedIndex == 0) { gprice =  (1- gdiscount / 100) * tktprice; } else { gprice = (parseFloat(gdiscount) > 0) ? tktprice - gdiscount : "0"; }
                lbl_ps1_sprice.autoNumeric('set', sprice);
                lbl_ps1_gprice.autoNumeric('set', gprice);
                seattotal += parseFloat(seat);
                saleamttotal += parseFloat(salesamt);
                tot_gross_per_wk = parseFloat(tot_gross_per_wk) + parseFloat(salesamt);
            }

            var lbl_seattotal = $(ucid.selector + "_lbl_seattotal");
            var lbl_ps1total = $(ucid.selector + "_lbl_ps1total");
            var lbl_ps1subtotal = $(ucid.selector + "_lbl_ps1subtotal");
            lbl_seattotal.html(seattotal);

            var divindex = ucid.selector.slice(ucid.selector.length - 2, ucid.selector.length).replace('s', '');
            var contentid = ucid.selector.replace('uc_ps' + divindex, '');
            var chk = $(contentid + "chk_ps" + divindex);
            var perCount = $(chk).find("input:checked").length;
            lbl_ps1subtotal.autoNumeric('set', (parseFloat(saleamttotal) * parseFloat(perCount)));


        }
       
    </script>

    <script src="Scripts/autoNumeric 1.9.15.js" type="text/javascript"></script>
    <script type="text/javascript">
        function checkweek(thisid) {
            var wk = $('#' + thisid.id.replace("chk_ps", "chklistweek"));
            if ($(wk).find("input:checked").length == 0) {
                alert('Select week!');
                return false;
            }
            return true;
        }
        function checkps(thisid) {
            var ps = $('#' + thisid.id.replace("chklistweek", "chk_ps"));
            var wkcount = $(thisid).find("input:checked").length;
            if ($(ps).find("input:checked").length > 0 && parseInt(wkcount) == 0) {
                alert('Performance is selected!');
                return false;
            }
        }
        function set_class() {
            $('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99', nBracket: '(,)' });
            $('.decimalonly10').autoNumeric({ vMax: '99999999.99', vMin: '-99999999.99', nBracket: '(,)' });
            $('.seatsint').autoNumeric({ aSep: '', vMin: '0', vMax: '2147483647' });
            $('.Percentage').autoNumeric({ aSign: '%', pSign: 's', vMax: '100' });
        }
        jQuery(function ($) {
            set_class();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            cal(thisid);
            set_class();
        };
        function pageLoad() {

            set_class();
        }

       

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center></center>
            <asp:HiddenField ID="hdnintialseat_total" runat="server" />
            <asp:Label ID="lbl_ps" runat="server" Text="Please Enter Engagement Schedule First"
                ForeColor="Red"></asp:Label>

            <table cellspacing="0" cellpadding="0" style="padding: 0px 0px 0px 0px;width:100%">
                <tr>
                    <td>
                             <div id="divmainpnl" width="100%" runat="server" visible="false">
                <div runat="server" id="div_ps" visible="false">
                    
                    <div runat="server" id="div_ps1" class="divsec" style="margin-top:20px; clear:both;width:100%">
                        <asp:Button ID="btnshowhidesec1" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                            CssClass="btnsection" CommandArgument="tab1" />
                        <table  id="tab1" runat="server" style="width:100%">
                            <tr>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="chklistweek1" onclick="return checkps(this);" runat="server"
                                        RepeatDirection="Horizontal" Enabled="true">
                                    </asp:CheckBoxList></td>
                            </tr>
                            <tr>
                                <td colspan="3" ><span style ="font-weight:bold" >Price Scales&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label
                                    ID="lbl_ps1" runat="server" Text=""></asp:Label>
                                </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                    ID="chk_ps1" runat="server" OnSelectedIndexChanged="chk_SelectedIndexChanged"
                                    ToolTip="1" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true">
                                </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" >
                                    <UC:PS runat="server" ID="uc_ps1" />
                                </td>
                            </tr>
                        </table>
                    </div>

               
                <div runat="server" id="div_ps2" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec2" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab2" />
                    <table width="100%" runat="server" id="tab2">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek2" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps2" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps2" runat="server" ToolTip="2"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps2" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps3" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec3" runat="server" Text= " - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab3" />
                    <table width="100%" runat="server" id="tab3">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek3" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps3" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps3" runat="server" ToolTip="3"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps3" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps4" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec4" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab4" />
                    <table width="100%" runat="server" id="tab4">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek4" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold" >&nbsp;<asp:Label
                                ID="lbl_ps4" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps4" runat="server" ToolTip="4"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps4" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps5" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec5" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab5" />
                    <table width="100%" runat="server" id="tab5">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek5" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps5" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps5" runat="server" ToolTip="5"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps5" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps6" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec6" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab6" />
                    <table width="100%" runat="server" id="tab6">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek6" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps6" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps6" runat="server" ToolTip="6"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps6" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps7" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec7" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab7" />
                    <table width="100%" runat="server" id="tab7">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek7" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps7" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps7" runat="server" ToolTip="7"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps7" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps8" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec8" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab8" />
                    <table width="100%" runat="server" id="tab8">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek8" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps8" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps8" runat="server" ToolTip="8"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps8" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps9" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec9" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab9" />
                    <table width="100%" runat="server" id="tab9">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek9" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps9" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps9" runat="server" ToolTip="9"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps9" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps10" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec10" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab10" />
                    <table width="100%" runat="server" id="tab10">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek10" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps10" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps10" runat="server" ToolTip="10"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps10" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps11" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec11" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab11" />
                    <table width="100%" runat="server" id="tab11">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek11" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps11" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps11" runat="server" ToolTip="11"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps11" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps12" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec12" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab12" />
                    <table width="100%" runat="server" id="tab12">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek12" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps12" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps12" runat="server" ToolTip="12"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps12" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps13" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec13" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab13" />
                    <table width="100%" runat="server" id="tab13">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek13" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps13" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps13" runat="server" ToolTip="13"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps13" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps14" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec14" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab14" />
                    <table width="100%" runat="server" id="tab14">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek14" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps14" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps14" runat="server" ToolTip="14"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps14" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps15" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec15" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab15" />
                    <table width="100%" runat="server" id="tab15">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek15" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps15" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps15" runat="server" ToolTip="15"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps15" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps16" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec16" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab16" />
                    <table width="100%" runat="server" id="tab16">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek16" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps16" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps16" runat="server" ToolTip="16"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps16" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps17" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec17" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab17" />
                    <table width="100%" runat="server" id="tab17">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek17" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps17" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps17" runat="server" ToolTip="17"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps17" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps18" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec18" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab18" />
                    <table width="100%" runat="server" id="tab18">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek18" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps18" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps18" runat="server" ToolTip="18"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps18" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps19" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec19" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab19" />
                    <table width="100%" runat="server" id="tab19">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek19" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps19" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps19" runat="server" ToolTip="19"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps19" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="div_ps20" class="divsec" style="margin-top:20px; clear:both;">
                    <asp:Button ID="btnshowhidesec20" runat="server" Text=" - " OnClick="btnshowhidesec_Click"
                        CssClass="btnsection" CommandArgument="tab20" />
                    <table width="100%" runat="server" id="tab20">
                        <tr>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chklistweek20" onclick="return checkps(this);" runat="server"
                                    RepeatDirection="Horizontal" Enabled="true">
                                </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="3" ><span style ="font-weight:bold">&nbsp;<asp:Label
                                ID="lbl_ps20" runat="server" Text=""></asp:Label>
                            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBoxList onclick="return checkweek(this);"
                                ID="chk_ps20" runat="server" ToolTip="20"
                                OnSelectedIndexChanged="chk_SelectedIndexChanged" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true">
                            </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="width: 100%">
                                <UC:PS runat="server" ID="uc_ps20" />
                            </td>
                        </tr>
                    </table>
                </div>
                </div>
                <div runat="server" id="div_pstotal" style="width:1300px; clear:both; margin-top:20px;">
                     <table width="100%">
                        <tr>
                            <td class="padding_leftright10">
                                <table width="100%">
                                    <tr>
                                        <td colspan="14" align="right">
                                            <asp:Button ID="btnNewPriceScale" CssClass="bluebutt" runat="server" OnClick="btnNewPriceScale_Click"
                                                Text="New Price Scale" /></td>
                                    </tr>
                                    <tr >
                                        <td style="width:100%">
                                            <hr />
                                        </td>
                                        </tr>

                                    <tr>
                                        <td class="linetop" align="left" style="width: 250px;" colspan="10">Total&nbsp; Gross/Week
                                       &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_pstotal" CssClass="txt_bold dollor" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp; Shows &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbl_psshows" runat="server" CssClass="txt_bold" Text=""></asp:Label>
                                            &nbsp;&nbsp;&nbsp; Tax&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbl_pssubtotal" runat="server" Text="" CssClass="txt_bold dollor"></asp:Label>&nbsp;&nbsp;&nbsp;Tax % &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbl_pstax" runat="server" CssClass="txt_bold" Text=""></asp:Label>&nbsp;&nbsp;&nbsp; Gross
                                        &nbsp;&nbsp;&nbsp; Tax/Week  &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblgrossafttax" runat="server" CssClass="txt_bold dollor" Text=""></asp:Label>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                    </td>
                </tr>
            

            <asp:HiddenField ID="hdn_engagementid" runat="server" />
            <asp:HiddenField ID="hdn_schedulecount" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


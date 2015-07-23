<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EngagementDeal.ascx.cs"
    Inherits="NTOS.EngagementDeal1" EnableTheming="true" %>
<style type="text/css">
    .auto-style1 {
        text-align: right;
    }
</style>
<script type="text/javascript">
    function showhide_ff() {
        //debugger

        if ($('#<%= drp_tax.ClientID%>').val() == "I") {
            $('#spntaxff').show();
            $('#<%= ddltaxptg_ff.ClientID+"_chosen"%>').width(40);
        }
        else {
            $('#spntaxff').hide();
        }
    }
    function setclass(thisid) {
        // debugger;
        var amt = thisid.value;
        //amt = (parseFloat(amt) <= 100) ? amt : "";
        //$('#' + thisid.id).autoNumeric('init');
        //$('#' + thisid.id).autoNumeric('set', amt);       
        if (parseFloat(amt) <= 100) {
            $('#' + thisid.id).autoNumeric('init', { aSign: '%', vMax: '100.00', pSign: 's' });
            $('#' + thisid.id).autoNumeric('set', { aSign: '%', vMax: '100.00', pSign: 's' });
        }
        else {
            thisid.value = "";
            $('#' + thisid.id).autoNumeric('init', { aSign: '%', vMax: '100.00', pSign: 's' })
        }

    }
    function pageLoad() {
        $(function selectdate() {
            $('#<%=hdfdealdate.ClientID%>').datePicker({
                startDate: '01/01/1996'
            }).bind(
                'dateSelected',

                function (e, selectedDate, $td) {
                    var d = new Date();
                    d = selectedDate.localeFormat("MM/dd/yyyy");
                    $("#<%=hdfdealdate.ClientID %>").val(d);
                    $("#<%=btnclickevent.ClientID %>")[0].click();
                }
            );
        });
    }
</script>
<script src="Scripts/autoNumeric 1.9.15.js" type="text/javascript"></script>
<script type="text/javascript">
    jQuery(function ($) {
        $('.dollor17').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99', nBracket: '(,)' });
        $('.decimal17').autoNumeric({ vMax: '999999999999999.99' });
        $('.decimal18').autoNumeric({ vMax: '999999999999999.99999999' });
        $('.percentage').autoNumeric({ aSign: '%', vMax: '100.00', pSign: 's' });
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        $('.dollor17').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99', nBracket: '(,)' });
        $('.decimal17').autoNumeric({ vMax: '999999999999999.99' });
        $('.percentage').autoNumeric({ aSign: '%', vMax: '100.00', pSign: 's' });
        highlight_this('r');
        showhide_ff();
    };

    function hlight(varid, i, control) {
        // debugger;
        var ss1 = document.createElement('style');
        var def = '';
        var cid = '';
        if (control == 'ddl') {
            cid = varid + '_chosen a ';
        }
        else if (control == 'txt') {
            cid = varid;
        }

        if (parseInt(i) == 0) {
            def = '#' + cid + ' {border-bottom-color:#FF0000;}';
        } else {
            def = '#' + cid + ' { border-bottom-color:#aaa;}';
            // def = '#' + varid + '_chosen a {background-color: #fff;}';
        }
        ss1.setAttribute("type", "text/css");
        var hh1 = document.getElementsByTagName('head')[0];
        hh1.appendChild(ss1);
        if (ss1.styleSheet) {   // IE
            ss1.styleSheet.cssText = def;
        } else {                // the world
            var tt1 = document.createTextNode(def);
            ss1.appendChild(tt1);
        }
    }

    //function highlight() {
    //    var reqlist = $('#hdnreqlist').val();
    //    if (reqlist.length > 0) {
    //        for (var i = 0; i < reqlist.split(',').length; i++) {
    //            var thisid = $('#' + reqlist.split(',')[i]);
    //            if (thisid[0] != undefined) {
    //                if (thisid[0].type == "select-one") {
    //                    // if (thisid[0].selectedIndex == 0) thisid[0].style.backgroundColor = '#FFD3A8'; else thisid[0].style.backgroundColor = '#ffffff';
    //                    if (thisid[0].selectedIndex == 0) hlight(thisid[0].id, 0, 'ddl'); else hlight(thisid[0].id, 1, 'ddl');
    //                    var keyup = (thisid[0].getAttribute("onchange") != null) ? thisid[0].getAttribute("onchange") + ";highlight();" : "highlight();";
    //                    if (thisid[0].hasAttributes("onchange") == false)
    //                        thisid[0].setAttribute("onchange", keyup);
    //                }
    //                if (thisid[0].type != "select-one") {
    //                    //if (thisid[0].value == '') thisid[0].style.backgroundColor = '#FFD3A8'; else thisid[0].style.backgroundColor = '#ffffff';
    //                    if (thisid[0].value == '') hlight(thisid[0].id, 0, 'txt'); else hlight(thisid[0].id, 1, 'txt');
    //                    var keyup = (thisid[0].getAttribute("onkeyup") != null && thisid[0].getAttribute("onkeyup").search("highlight()") == -1) ? thisid[0].getAttribute("onkeyup") + "highlight();" : "highlight();";
    //                    thisid[0].setAttribute("onkeyup", keyup);
    //                }
    //            }

    //        }
    //    }

    //}


    function highlight_this(type) {
        var reqlist = "drp_show,drp_dealtype,txt_createdate", ctrl = "";
        for (var i = 0; i < reqlist.split(',').length; i++) {
            ctrl = $("#<%=hdn_ucdealid.ClientID %>");
            if (ctrl[0] != undefined) {
                ctrl = ctrl[0].id.replace("hdn_ucdealid", reqlist.split(',')[i]);
                if ($('#' + ctrl)[0] != undefined) {
                    var flg = ($('#' + ctrl).val() == "" || $('#' + ctrl).val() == "0" || $('#' + ctrl).val() == "--Select--") ? false : true;
                    //$('#' + ctrl)[0].style.backgroundColor = (flg == false) ? '#FFD3A8' : '#ffffff';
                    var ctrltype = ($('#' + ctrl)[0].type == "select-one") ? "ddl" : "txt";
                    if (flg == false) hlight($('#' + ctrl)[0].id, 0, ctrltype); else hlight($('#' + ctrl)[0].id, 1, ctrltype);

                    if (reqlist.split(',')[i] == 'drp_dealtype') {
                        var hf = $('#' + ctrl)[0].id + '_drp_dealtype_HiddenField';
                        var txt = $('#' + ctrl)[0].id + '_drp_dealtype_TextBox';
                        flg = ($('#' + hf).val() == "0") ? false : true;
                        $('#' + txt)[0].style.borderBottomColor = (flg == false) ? '#FF0000' : '#aaa';
                    }
                }
            }
        }
    }
    function changemodifystatus() {
        $('#hdn_modify_status').val("1");
    }
    window.onload = highlight_this;
</script>
<asp:UpdatePanel ID="updpanel1" runat="server">
    <ContentTemplate>
       

        <table width="1800px" cellpadding="0" cellspacing="0" border="0" style="margin-top:-20px;">
            <tr>
                <td colspan="10" align="center">
                    <asp:ValidationSummary ID="val_summary_deal" runat="server" Font-Bold="true" ForeColor="Orange"
                        HeaderText="Field validations failed. Please check and correct" DisplayMode="SingleParagraph" />
                    &nbsp;
         <asp:Label ID="lbl_message" runat="server" Font-Bold="true" />
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    <asp:Label ID="lbl_show" runat="server" Text="Show"></asp:Label>
                </td>
                <td>
                  &nbsp;&nbsp;<asp:DropDownList ID="drp_show" runat="server" onchange="highlight_this('s');" TabIndex="2" Width="200px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_show" runat="server" ControlToValidate="drp_show" CssClass="asterisk" InitialValue="0" ToolTip="Required Field">*</asp:RequiredFieldValidator>
                </td>
                <td align="right">

                    <label>
                    Deal Type</label>
                </td>

                <td>
                    &nbsp;&nbsp;<ajaxToolkit:ComboBox ID="drp_dealtype" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" CssClass="ajxcmb" DropDownStyle="DropDown" ItemInsertLocation="Append" MaxLength="30" onchange="highlight_this('t');" OnSelectedIndexChanged="drp_dealtype_SelectedIndexChanged" RenderMode="Inline" TabIndex="17" Width="200px">
                    </ajaxToolkit:ComboBox><asp:RequiredFieldValidator ID="rfv_dealtype" runat="server" ControlToValidate="drp_dealtype" CssClass="asterisk" InitialValue="--Select OR Enter--" ToolTip="Required Field">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfv_dealtype1" runat="server" ControlToValidate="drp_dealtype" CssClass="asterisk" InitialValue="--Select--" ToolTip="Required Field">*</asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lbl_createdate" runat="server" Text="Create Date"></asp:Label>
                </td>
                <td>
                    &nbsp;&nbsp;<asp:TextBox ID="txt_createdate" runat="server" onchange="highlight_this('d');" TabIndex="26" Width="120px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_createdate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfv_createdate" runat="server" ControlToValidate="txt_createdate" CssClass="asterisk" ToolTip="Required Field">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="comp_createdate" runat="server" ControlToValidate="txt_createdate" ForeColor="Red" Operator="DataTypeCheck" ToolTip="Enter valid date format(mm/dd/yyyy)" Type="Date">#</asp:CompareValidator>
                </td>
                  <td style="visibility: hidden">Update Date
                            </td>
                            <td style="visibility: hidden">&nbsp;&nbsp;&nbsp;  
                                <asp:DropDownList ID="drp_updatedate" CssClass="my_select_box" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_updatedate_SelectedIndexChanged"
                                    Width="120px" DataTextFormatString="{0:MM/dd/yyyy}" TabIndex="4">
                                </asp:DropDownList>
                              <%--  <asp:RequiredFieldValidator ID="rfv_updatedate" runat="server" CssClass="asterisk"
                                    ToolTip="Required Field" ControlToValidate="drp_updatedate" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                            </td>
                            <td style="visibility: hidden">
                                <asp:HiddenField ID="hdfdealdate" runat="server" />
                                <div style="display: none">
                                    <asp:Button CausesValidation="false" ID="btnclickevent" runat="server"
                                        OnClick="cal_updatedate_SelectionChanged" />
                                </div>

                            </td>

            </tr>
            
             <tr>
                <td colspan="10"  class="heading"></td>
                
            </tr>
            <tr>
                <td colspan="4" class="heading lineright">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Income</td>
                <td class="heading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td colspan="4" class="heading">&nbsp; Commissions</td>

                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>

            </tr>

            <tr>
                <td colspan="2">&nbsp;</td>
                <td colspan="2" class="lineright"><strong>Middle Monies</strong></td>
                <td>&nbsp;</td>
                <td align="right"><strong>Taxes</strong></td>
                <td>&nbsp;</td>
                <td align="right"><strong>Facility Fee</strong></td>
                <td>&nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: right">Royalty </td>
                <td>&nbsp;&nbsp;<asp:TextBox ID="txt_royalty" runat="server"  style="text-align: right" CssClass="percentage" TabIndex="3"></asp:TextBox>
                    &nbsp;</td>

                <td class="auto-style1">Company</td>
                <td class="lineright">&nbsp;&nbsp;<asp:TextBox ID="txt_companymonies"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="18"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="auto-style1">Tax 1</td>
                <td>
                   &nbsp;&nbsp;<asp:DropDownList ID="drp_tax" runat="server" onchange="showhide_ff();" SkinID="ddlsmaller" TabIndex="27" ToolTip="I-Include,O-On the Top" Width="50px">
                        <asp:ListItem Value="I">I</asp:ListItem>
                        <asp:ListItem Value="O">O</asp:ListItem>
                    </asp:DropDownList>
                   
                    <span id="spntaxff0" style="width: 40px; position: relative;">
                        <asp:DropDownList ID="ddltaxptg_ff" runat="server" SkinID="ddlsmaller" TabIndex="28">
                            <asp:ListItem Text="After FF" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Before FF" Value="B"></asp:ListItem>
                             <asp:ListItem Text="No FF" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Not Applicable" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                     <asp:TextBox ID="txt_tax" runat="server" CssClass="percentage"  style="text-align: right" TabIndex="29" Width="50px"></asp:TextBox>
                </td>
                <td class="auto-style1">On Each Ticket</td>
                <td>&nbsp;&nbsp;<asp:DropDownList ID="drp_facilityfee1" runat="server" SkinID="ddlsmaller" TabIndex="37" ToolTip="I-Include,O-On the Top" Width="50px">
                    <asp:ListItem Value="I">I</asp:ListItem>
                    <asp:ListItem Value="O">O</asp:ListItem>
                </asp:DropDownList>
                     <asp:DropDownList ID="drp_facilityfee2" runat="server" SkinID="ddlsmaller" TabIndex="38" Width="50px">
                        <asp:ListItem Value="$">$</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txt_facilityfee" runat="server" CssClass="decimal17" TabIndex="39" Width="120px"></asp:TextBox>
                   
                    &nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: right">Guarantee</td>
                <td>&nbsp;&nbsp;<asp:TextBox ID="txt_guarantee"  style="text-align: right" runat="server" CssClass="dollor17" TabIndex="4"></asp:TextBox>
                </td>
                <td class="auto-style1">Presenter</td>
                <td class="lineright">&nbsp;&nbsp;<asp:TextBox ID="txt_presentermonies"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="19"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="auto-style1">Tax 2</td>
                <td>
                   &nbsp;&nbsp;<asp:TextBox ID="txt_tax2" runat="server"  style="text-align: right" CssClass="percentage" TabIndex="30"></asp:TextBox>
                </td>
                <td class="auto-style1">Tax/Facility Fee</td>
                <td>&nbsp;&nbsp;<asp:DropDownList ID="drp_boxoffice1" runat="server" SkinID="ddlsmaller" TabIndex="40" ToolTip="I-Include,O-On the Top" Width="50px">
                    <asp:ListItem Value="I">I</asp:ListItem>
                    <asp:ListItem Value="O">O</asp:ListItem>
                </asp:DropDownList>
                    <asp:TextBox ID="txt_boxoffice" runat="server" CssClass="percentage" TabIndex="41" Width="120px"></asp:TextBox>
                    &nbsp;<%--<asp:DropDownList ID="drp_boxoffice2" runat="server" TabIndex="36">
                        <asp:ListItem Value="$">$</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                    </asp:DropDownList>--%></td>
                <td></td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;Cap</td>
                <td class="lineright">&nbsp;&nbsp;<asp:TextBox ID="txt_middlecap"  style="text-align: right" runat="server" CssClass="dollor17" TabIndex="20"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="chosen-rtl" style="text-align: right">Tax Amount Over</td>
                <td>
                    &nbsp;&nbsp;<asp:TextBox ID="txt_taxover" runat="server"  style="text-align: right" CssClass="dollor17" TabIndex="31"></asp:TextBox>
                </td>
            </tr>

<%--            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>--%>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2"><strong>Share Split </strong></td>
                <td colspan="2" class="lineright"><strong>Income Withholding Tax</strong></td>
                <td>&nbsp;</td>
                <td align="right"><strong>Sales</strong></td>
                <td>&nbsp;</td>
                <td align="right"><strong>Group Sales</strong></td>
                <td>&nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style1">Producer </td>

                <td>&nbsp;&nbsp;<asp:TextBox ID="txt_producershare"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="5"></asp:TextBox>
                </td>
                <td class="auto-style1">&nbsp;Budget</td>
                <td class="lineright">&nbsp;&nbsp;<asp:DropDownList ID="drp_taxbudget"  style="text-align: right" runat="server" SkinID="ddlsmaller" TabIndex="21" Width="50px">
                    <asp:ListItem Value="$">$</asp:ListItem>
                    <asp:ListItem Value="%">%</asp:ListItem>
                </asp:DropDownList>
                    <asp:TextBox ID="txt_taxbudget" runat="server"  style="text-align: right" CssClass="decimal17" TabIndex="22" Width="110px"></asp:TextBox>
                    &nbsp;&nbsp;</td>
               <td >&nbsp;</td>
                <td class="auto-style1">Subscription </td>
                <td>&nbsp;&nbsp;<asp:TextBox ID="txt_subscriptionsale" runat="server"  style="text-align: right" CssClass="percentage" TabIndex="32"></asp:TextBox>
                    &nbsp;&nbsp;</td>
                <td class="auto-style1">Single Tickets</td>
                <td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt_singleticket" runat="server"  style="text-align: right" CssClass="percentage" TabIndex="42"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style1">Presenter</td>
                <td>&nbsp;&nbsp;<asp:TextBox ID="txt_presentershare"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="10"></asp:TextBox>
                    &nbsp; 
                </td>
                <td class="auto-style1">&nbsp;Actual </td>

                <td class="lineright">&nbsp;&nbsp;<asp:DropDownList ID="drp_taxactual" runat="server" SkinID="ddlsmaller" TabIndex="23" Width="50px">
                    <asp:ListItem Value="$">$</asp:ListItem>
                    <asp:ListItem Value="%">%</asp:ListItem>
                </asp:DropDownList>
                    <asp:TextBox ID="txt_taxactual" runat="server"  style="text-align: right" CssClass="decimal18" TabIndex="24" Width="110px"></asp:TextBox>
                </td>

               <td >&nbsp;</td>
                <td class="auto-style1">Phone</td>
               
                <td>&nbsp;&nbsp;<asp:TextBox ID="txt_phonecommission"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="33"></asp:TextBox>
                    &nbsp;&nbsp;</td>

                <td class="auto-style1">Group 1</td>
                <td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt_groupsale1"  style="text-align: right"  runat="server" CssClass="percentage" TabIndex="43"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
              
                <td class="auto-style1">Star Royalty</td>
                <td>
                    &nbsp;&nbsp;<asp:TextBox ID="txt_starroyalty"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="15"></asp:TextBox>
                   </td>
                <td></td>
                
                <td class="lineright">&nbsp;</td>
               <td></td>
                <td class="auto-style1">Internet</td>
                <td>&nbsp;&nbsp;<asp:TextBox ID="txt_internetsale"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="34"></asp:TextBox>
                </td>
                <td class="auto-style1">Group 2</td>
                <td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt_groupsale2"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="44"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
               
                <td>&nbsp;</td>
                <td >&nbsp;</td>
                <td>&nbsp;</td>
               
                 <td class="lineright">&nbsp;</td>
                 <td></td>
                <td class="auto-style1">Credit Card</td>
                <td>&nbsp;&nbsp;<asp:TextBox ID="txt_cardsale"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="35"></asp:TextBox>
                </td>

                   
                        <td align="right"><strong>Miscellaneous</strong></td>
<td>
                </td>
            </tr>
            <tr>
               
                     <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
               
                    <td class="lineright">&nbsp;</td>
                     <td></td>
                    <td class="auto-style1">Remote </td>

                    <td>&nbsp;&nbsp;<asp:TextBox ID="txt_remotesale"  style="text-align: right" runat="server" CssClass="percentage" TabIndex="36"></asp:TextBox>
                    </td>
                 <td class="auto-style1">&nbsp;Other 1</td>

                <td>&nbsp;&nbsp;<asp:DropDownList ID="ddlMisOthers1" runat="server" SkinID="ddlsmall" TabIndex="6">
                </asp:DropDownList>
                    <asp:DropDownList ID="drp_miscellaneous11" runat="server" SkinID="ddlsmaller" TabIndex="7" ToolTip="I-Include,O-On the Top" Width="50px">
                        <asp:ListItem Value="I">I</asp:ListItem>
                        <asp:ListItem Value="O">O</asp:ListItem>
                    </asp:DropDownList>  <asp:DropDownList ID="drp_miscellaneous12" runat="server" SkinID="ddlsmaller" TabIndex="8" Width="50px">
                        <asp:ListItem Value="$">$</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txt_miscellaneous1" runat="server" CssClass="decimal17" TabIndex="9" Width="120px"></asp:TextBox>
                  
                </td>
                    <td>
                        <td>&nbsp;</td>
            </tr>
            


            <tr>
                <td style="text-align: right" class="linebottom">&nbsp;</td>
                <td class="linebottom">&nbsp;<td class="linebottom">&nbsp;</td>
                    <td class="linebottom lineright">&nbsp;</td>
                    <td class="linebottom">&nbsp;</td>
                    <td class="chosen-rtl linebottom">&nbsp;</td>
                    <td class="linebottom">&nbsp;</td>
                    <td class="linebottom auto-style1" >&nbsp;Other 2</td>
                <td class="linebottom">&nbsp;&nbsp;<asp:DropDownList ID="ddlMisOthers2" runat="server" SkinID="ddlsmall" TabIndex="11"></asp:DropDownList>
                    <asp:DropDownList ID="drp_miscellaneous21" runat="server" SkinID="ddlsmaller" TabIndex="12" ToolTip="I-Include,O-On the Top" Width="50px">
                        <asp:ListItem Value="I">I</asp:ListItem>
                        <asp:ListItem Value="O">O</asp:ListItem>
                    </asp:DropDownList><asp:DropDownList ID="drp_miscellaneous22" runat="server" SkinID="ddlsmaller" TabIndex="13" Width="50px">
                        <asp:ListItem Value="$">$</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txt_miscellaneous2" runat="server"  style="text-align: right" CssClass="decimal17" TabIndex="14" Width="120px"></asp:TextBox>
                    
                    &nbsp;&nbsp;&nbsp;</td>
            </tr>
              <tr>
                <td style="text-align: right" class="linebottom">&nbsp;</td>
                <td class="linebottom">&nbsp;<td class="linebottom">&nbsp;</td>
                    <td class="linebottom lineright">&nbsp;</td>
                    <td class="linebottom">&nbsp;</td>
                    <td class="chosen-rtl linebottom">&nbsp;</td>
                    <td class="linebottom">&nbsp;</td>
                    <td class="linebottom auto-style1" >&nbsp;Other 3</td>
                <td class="linebottom">&nbsp;&nbsp;<asp:DropDownList ID="ddlMisOthers3" runat="server" SkinID="ddlsmall" TabIndex="11"></asp:DropDownList>
                    <asp:DropDownList ID="drp_miscellaneous31" runat="server" SkinID="ddlsmaller" TabIndex="12" ToolTip="I-Include,O-On the Top" Width="50px">
                        <asp:ListItem Value="I">I</asp:ListItem>
                        <asp:ListItem Value="O">O</asp:ListItem>
                    </asp:DropDownList><asp:DropDownList ID="drp_miscellaneous32" runat="server" SkinID="ddlsmaller" TabIndex="13" Width="50px">
                        <asp:ListItem Value="$">$</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txt_miscellaneous3" runat="server"  style="text-align: right" CssClass="decimal17" TabIndex="14" Width="120px"></asp:TextBox>
                    
                    &nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="linebottom">&nbsp;</td>
                <td class="linebottom">&nbsp;<td class="linebottom">&nbsp;</td>
                    <td class="linebottom lineright">&nbsp;</td>
                    <td class="linebottom">&nbsp;</td>
                    <td class="chosen-rtl linebottom">&nbsp;</td>
                    <td class="linebottom">&nbsp;</td>
                    <td class="linebottom auto-style1" >&nbsp;Other 4</td>
                <td class="linebottom">&nbsp;&nbsp;<asp:DropDownList ID="ddlMisOthers4" runat="server" SkinID="ddlsmall" TabIndex="11" ></asp:DropDownList>
                    <asp:DropDownList ID="drp_miscellaneous41" runat="server" SkinID="ddlsmaller" TabIndex="12" ToolTip="I-Include,O-On the Top" Width="50px">
                        <asp:ListItem Value="I">I</asp:ListItem>
                        <asp:ListItem Value="O">O</asp:ListItem>
                    </asp:DropDownList><asp:DropDownList ID="drp_miscellaneous42" runat="server" SkinID="ddlsmaller" TabIndex="13" Width="50px">
                        <asp:ListItem Value="$">$</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txt_miscellaneous4" runat="server"  style="text-align: right" CssClass="decimal17" TabIndex="14" Width="120px"></asp:TextBox>
                    
                    &nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="linebottom">&nbsp;</td>
                <td class="linebottom">&nbsp;<td class="linebottom">&nbsp;</td>
                    <td class="linebottom lineright">&nbsp;</td>
                    <td class="linebottom">&nbsp;</td>
                    <td class="chosen-rtl linebottom">&nbsp;</td>
                    <td class="linebottom">&nbsp;</td>
                    <td class="linebottom auto-style1" >&nbsp;Other 5</td>
                <td class="linebottom">&nbsp;&nbsp;<asp:DropDownList ID="ddlMisOthers5" runat="server" SkinID="ddlsmall" TabIndex="11"></asp:DropDownList>
                    <asp:DropDownList ID="drp_miscellaneous51" runat="server" SkinID="ddlsmaller" TabIndex="12" ToolTip="I-Include,O-On the Top" Width="50px">
                        <asp:ListItem Value="I">I</asp:ListItem>
                        <asp:ListItem Value="O">O</asp:ListItem>
                    </asp:DropDownList><asp:DropDownList ID="drp_miscellaneous52" runat="server" SkinID="ddlsmaller" TabIndex="13" Width="50px">
                        <asp:ListItem Value="$">$</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txt_miscellaneous5" runat="server"  style="text-align: right" CssClass="decimal17" TabIndex="14" Width="120px"></asp:TextBox>
                    
                    &nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr><td colspan="10">
        <table>
            <tr>
                <td align="right">
                    <label id="lblcontract" runat="server">Contract</label></td>
                <td>&nbsp;&nbsp;<asp:DropDownList ID="drp_contract" runat="server" Width="160px" TabIndex="1">
                </asp:DropDownList>
                </td>


                <td class="auto-style1">
                    <label id="lbldealdemo" runat="server">
                        Deal Memo</label></td>
                <td>&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="drp_dealmemo" runat="server" Width="160px" TabIndex="16">
                </asp:DropDownList>
                </td>


                <td align="right">
                    <label id="lblExchangerate" runat="server">Exchange Rate</label></td>
                <td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt_exchange"  style="text-align: right" runat="server" CssClass="decimal6" TabIndex="25"></asp:TextBox>
                </td>
                <td></td>
            </tr>
        </table>
        </td></tr>
            
        
            <tr>
                <td colspan="8" align="center">
                    <asp:HiddenField ID="hdn_ucdealid" runat="server" />
                    <asp:HiddenField ID="hdn_ucengagementid" runat="server" />
                    <asp:HiddenField ID="hdn_ucshowid" runat="server" />
                </td>
            </tr>
        </table>


    </ContentTemplate>
</asp:UpdatePanel>

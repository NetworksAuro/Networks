<%@ Page Language="C#" Title="Search" AutoEventWireup="true" MasterPageFile="~/Engagement.Master" CodeBehind="Search.aspx.cs" Inherits="NTOS.Search2" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/jscript" src="Scripts/autoNumeric 1.9.15.js"></script>
    <script type="text/javascript">

        jQuery(function ($) {
            $('.dollor17').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99', nBracket: '(,)' });
            $('.decimal17').autoNumeric({ vMax: '999999999999999.99' });
            $('.percentage').autoNumeric({ aSign: '%', vMax: '100.00', pSign: 's' });
            $('.dollor4dec').autoNumeric({ aSign: '$', vMax: '999999999999999.9999', vMin: '-999999999999999.9999', nBracket: '(,)' });
        });

        $("#Form1").one("change", ":input", function () {
            var user_role = '<%= Session["userrole"].ToString() %>';
             var role_status = (user_role == 'reader') ? 0 : 1;
             if (role_status != 0)
                 $('#hdn_modify_status').val("0");

         });

    </script>
    <style type="text/css">


        #gvrep {
            border: none !important;
            width: auto !important;
        }

        .auto-style1 {
            font-size: 14px;
        }
         
    
        .auto-style4 {
              text-align: right;
            width: 311px;
        }
   
    </style>
   <%--  <div class="clear" align="left">
        <asp:Label runat="server" ID="lblresults" Text="Please enter criteria and click search button" Font-Bold="true"></asp:Label>
    </div>--%>
    <div style="height: 30px">
    </div>
    <div>
    </div>
    <table>
        <tr>
            <td>

           <div id="Div_eng_schedule" runat="server" visible="false" width="100%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                    <tr>
                        <td class="chosen-rtl">
                            <label>
                            Shows</label> </td>
                        <td align="left"  class="lineright" style="width:auto; padding: 0px 10px 0px 5px;">&nbsp;
                          
                            <asp:TextBox ID="txt_showsearch" runat="server" Width="150px" TabIndex="1"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="aceshowname" runat="server" TargetControlID="txt_showsearch" DelimiterCharacters=","
                                CompletionListElementID="divacewidth" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="60"
                                ServiceMethod="Getshownames"  FirstRowSelected="true">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td class="chosen-rtl" width="100">
                            <label>
                            Presenter</label> </td>
                        <td  align="left"  class="lineright" style="width:auto; padding: 0px 10px 0px 5px;">&nbsp;
                            <asp:TextBox ID="txt_presentersearch"  runat="server"
                                Width="150px" TabIndex="2"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="acepresentername" runat="server" TargetControlID="txt_presentersearch"
                                           CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ShowOnlyCurrentWordInCompletionListItem="true"
                                            MinimumPrefixLength="1" EnableCaching="true"  CompletionSetCount="1" CompletionInterval="100"
                                            ServiceMethod="Getpresentername"  FirstRowSelected="true">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td class="chosen-rtl">
                            <label>
                            Status</label> </td>
                        <td align="left" class="lineright" style="width:auto; padding: 0px 10px 0px 5px;">&nbsp; 
                            <asp:TextBox ID="txt_status" runat="server" Width="150px" TabIndex="3">
                            </asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_status"
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="GetStatus">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td rowspan="3">
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <label>
                            City/State</label> </td>
                        <td align="left"   class="lineright" style="width:auto; padding: 0px 10px 0px 5px;">&nbsp; 
                            <asp:TextBox ID="txt_metrosearch" runat="server" OnTextChanged="txt_metrosearch_TextChanged"
                                AutoPostBack="True" Width="150px" TabIndex="4">
                            </asp:TextBox>
                            &nbsp;
                             <asp:TextBox ID="txtState" runat="server" Width="150px" AutoPostBack="true" TabIndex="8">
                            </asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txt_metrosearch"
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="Getcityname">
                                </ajaxToolkit:AutoCompleteExtender>
                                 <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtState"
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="GetstATEname">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td class="chosen-rtl">
                            <label>
                            Venue</label> </td>
                        <td align="left"  class="lineright" style="width:auto; padding: 0px 10px 0px 5px;">&nbsp;
                            <asp:TextBox ID="txt_venuesearch" runat="server" Width="150px" AutoPostBack="true" TabIndex="6">
                            </asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="aceVenuename" runat="server" TargetControlID="txt_venuesearch" DelimiterCharacters=","
                                CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                                CompletionInterval="60" 
                                ServiceMethod="GetVenueName">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td class="chosen-rtl" width="150px">
                            <label>
                            Subscription 
                            </label>
                        </td>
                        <td align="left" class="lineright" style="width:auto; padding: 0px 10px 0px 5px;">
                            <asp:DropDownList ID="drp_subscription" EnableTheming="false" CssClass="my_select_box_small"
                                                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="drp_subscription_SelectedIndexChanged">
                                 <asp:ListItem Value="">--</asp:ListItem>
                                <asp:ListItem Value="N">N</asp:ListItem>
                                <asp:ListItem Value="Y">Y</asp:ListItem>
                            </asp:DropDownList>
                                                    &nbsp;&nbsp
                                                      <asp:TextBox ID="txt_subscription" runat="server" Visible="false"  Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="chosen-rtl">
                            <label>
                            Performance Date</label> </td>
                        <td align="left"   class="lineright" style="width:auto; padding: 0px 10px 0px 5px;">&nbsp; 
                            <asp:TextBox ID="txtcreatedatesearch" Width="150px" runat="server" TabIndex="5"></asp:TextBox>
                         <ajaxToolkit:CalendarExtender ID="CalendarEsxtender1" runat="server" TargetControlID="txtcreatedatesearch" PopupButtonID="txtcreatedatesearch"></ajaxToolkit:CalendarExtender></td>
                       
                        <td align="right" valign="bottom">Repeat
                        </td>
                        <td align="left" class="lineright" style="width:auto; padding: 0px 10px 0px 5px;">
                            <asp:DropDownList ID="drp_repeat" runat="server" EnableTheming="false"
                                                    CssClass="my_select_box_small">
                                 <asp:ListItem Value="">--</asp:ListItem>
                                <asp:ListItem Value="N">N</asp:ListItem>
                                <asp:ListItem Value="Y">Y</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divDeal" runat="server" visible="false">
        <table width="1800px" cellpadding="5" cellspacing="0" border="0">
            <tr>
                <td colspan="4" style="border-right-style: solid; border-right-width: 1px" class="heading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Income</td>
                <td class="heading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td colspan="4" class="heading">&nbsp; Commision</td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
                <td colspan="2" style="border-right-style: solid; border-right-width: 1px"><strong>Middle Monies</strong></td>
                <td>&nbsp;</td>
                <td colspan="2"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Taxs</strong></td>
                <td colspan="2"><strong>Facility Fee</strong></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: right">Royalty </td>
                <td>&nbsp;
                                        <asp:TextBox ID="txt_royalty" runat="server"  TabIndex="9"></asp:TextBox>
                </td>
                <td class="chosen-rtl">Company</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;
                                <asp:TextBox ID="txt_companymonies" runat="server"  TabIndex="23"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">Tax 1</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_tax" runat="server"  TabIndex="33" Width="50px"></asp:TextBox>
                </td>
                <td class="chosen-rtl">On Each Ticket</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_facilityfee" runat="server"  TabIndex="49" Width="120px"></asp:TextBox>
                    &nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: right">Guarantee</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_guarantee" runat="server"  TabIndex="11"></asp:TextBox>
                </td>
                <td class="chosen-rtl">Presenter</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;
                                <asp:TextBox ID="txt_presentermonies" runat="server"  TabIndex="25"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">Tax 2</td>
                <td>&nbsp;
                </td>
                <td class="chosen-rtl">Tax/Facility Fee</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_boxoffice" runat="server"  TabIndex="51" Width="120px"></asp:TextBox>
                    &nbsp;<%--<asp:DropDownList ID="drp_boxoffice2" runat="server" TabIndex="36">
                        <asp:ListItem Value="$">$</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                    </asp:DropDownList>--%></td>
                <td></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;Cap</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;
                                <asp:TextBox ID="txt_middlecap" runat="server"  TabIndex="27"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">Tax Amount Over</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_taxover" runat="server"  TabIndex="37"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2"><strong>Share Split Miscellaneous</strong></td>
                <td colspan="2" style="border-right-style: solid; border-right-width: 1px"><strong>Income Withholding Tax</strong></td>
                <td>&nbsp;</td>
                <td colspan="2"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sales</strong></td>
                <td colspan="2"><strong>Group Sales</strong></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: right">Producer </td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_producershare" runat="server"  TabIndex="13"></asp:TextBox>
                </td>
                <td class="chosen-rtl">&nbsp;Budget</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;
                                <asp:TextBox ID="txt_taxbudget" runat="server"  TabIndex="29" Width="110px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">Subscription </td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_subscriptionsale" runat="server"  TabIndex="39"></asp:TextBox>
                </td>
                <td class="chosen-rtl">Single Tickets</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_singleticket" runat="server"  TabIndex="53"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="chosen-rtl">&nbsp;Other 1</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_miscellaneous1" runat="server"  TabIndex="15" ></asp:TextBox>
                </td>
                <td class="chosen-rtl">&nbsp;Actual </td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;
                                <asp:TextBox ID="txt_taxactual" runat="server"  TabIndex="31" Width="110px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">Phone</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_phonecommission" runat="server"  TabIndex="41"></asp:TextBox>
                </td>
                <td class="chosen-rtl">Group 1</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_groupsale1" runat="server"  TabIndex="55"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="chosen-rtl">Presenter</td>
                <td>&nbsp;
                    <asp:TextBox ID="txt_presentershare2" runat="server"  TabIndex="17"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">Internet</td>
                <td>&nbsp;
                                <asp:TextBox ID="txt_internetsale" runat="server"  TabIndex="43"></asp:TextBox>
                </td>
                <td class="chosen-rtl">Group 2</td>
                <td>&nbsp;
                    <asp:TextBox ID="txt_groupsale3" runat="server"  TabIndex="57"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="chosen-rtl">&nbsp;Other 2</td>
                <td>&nbsp;
                                        <asp:TextBox ID="txt_miscellaneous5" runat="server"  TabIndex="19" ></asp:TextBox>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">Credit Card</td>
                <td>&nbsp;
                                        <asp:TextBox ID="txt_cardsale" runat="server"  TabIndex="45"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Star Royalty</td>
                <td>&nbsp;
                    <asp:TextBox ID="txt_starroyalty1" runat="server"  TabIndex="21"></asp:TextBox>
                <td>&nbsp;</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">Remote </td>
                <td>&nbsp;
                                        <asp:TextBox ID="txt_remotesale1" runat="server"  TabIndex="47"></asp:TextBox>
                </td>
                <td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td>&nbsp;<td>&nbsp;</td>
                <td style="border-right-style: solid; border-right-width: 1px">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;<td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div id="DivPricescale" runat="server" visible="false">
        <table cellpadding="0" cellspacing="0" border="0" width="1000px">
            <tr>
                <td align="left" width="25%"><b>Price Scale</b></td>
                <td align="left"><b>Ticket Price</b></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td width="5%" align="right">
                                <label>
                                Seats</label>&nbsp;</td>
                            <td width="20%" style="border-right-style: solid; border-right-width: 1px">
                                <asp:TextBox ID="txtbxSeatFrom" runat="server" Width="150px" TabIndex="1"></asp:TextBox>
                            </td>
                            <td width="10%" align="right">
                                <label>
                                Single</label>&nbsp;</td>
                            <td width="50%">
                                <asp:TextBox ID="txtbxSinglefrom" onchange="highlight();" runat="server" Width="150px" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="border-right-style: solid; border-right-width: 1px"></td>
                            <td align="right">
                                <label>
                                &nbsp;Group</label>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txttbxgroupfrom" onchange="highlight();" runat="server" Width="150px" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="border-right-style: solid; border-right-width: 1px"></td>                       
                            <td align="right">
                                <label>
                                &nbsp;Subscription</label>&nbsp;</td>
                            <td><asp:TextBox ID="txtbxsubscriptionfrom" onchange="highlight();" runat="server"
                                Width="150px" TabIndex="2"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
    </div>
    <div id="DivExpense" runat="server" visible="false">
        <table cellspacing="0" cellpadding="0" width="1900px">
            <tr>
              <%--  <td align="center" class="auto-style4">
                    <label>
                    Expense</label>&nbsp;</td>--%>
                <td colspan="9"></td>
            </tr>
            <tr>
                <td class="heading"  align="right"><strong>Documented</strong></td>
                <td colspan="4" class="lineright"></td>
                <td align="right" class="heading"><strong>Local</strong></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <th height="40" class="auto-style4" align="center"></th>
                <th align="left"></th>
                <th align="left">BUDGETED</th>
                <th align="left">ACTUAL</th>
                <th align="left" class="lineright">&nbsp;</th>
                <th align="center" class="heading"></th>
                <th align="center">&nbsp;</th>
                <th align="left">BUDGETED</th>
                <th align="left">ACTUAL</th>
                <th align="left">&nbsp;</th>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Advertising(Gross)</label></td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtadvt_bud" 
                        runat="server" TabIndex="3"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtadvt_act"  runat="server"
                        TabIndex="32"></asp:TextBox>
                </td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    ADA Expense</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtadaexp_bud" 
                        runat="server" TabIndex="60"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtadaexp_act"  runat="server"
                        TabIndex="108"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Labor Catering</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtlabourcatering_bud" runat="server"  TabIndex="5"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtlabourcatering_act" runat="server"  TabIndex="34"></asp:TextBox>
                </td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    Box Office</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtboxoff_bud" 
                        runat="server" TabIndex="62"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtboxoff_act"  runat="server"
                        TabIndex="110"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Musicians</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtmusicians_bud" runat="server"  TabIndex="7"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtmusicians_act" runat="server"  TabIndex="36"></asp:TextBox>
                </td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    Catering</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtcatering_bud" runat="server"  TabIndex="64"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtcatering_act" runat="server"  TabIndex="112"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    Equipment Rental</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txteqiprental_bud" 
                        runat="server" TabIndex="66"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txteqiprental_act"  runat="server"
                        TabIndex="114"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4"><b>Stage Hands</b></td>
                <td></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    Group Sales Expenses</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtgrpsaleexp_bud" 
                        runat="server" TabIndex="68"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtgrpsaleexp_act"  runat="server"
                        TabIndex="116"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Load In</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtstatehandin_bud" runat="server"  TabIndex="9"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtstatehandin_act" runat="server"  TabIndex="38"></asp:TextBox>
                </td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    House Staff</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txthousestaff_bud" 
                        runat="server" TabIndex="70"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txthousestaff_act"  runat="server"
                        TabIndex="118"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Load Out</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtstatehandout_bud" runat="server"  TabIndex="11"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtstatehandout_act" runat="server"  TabIndex="40"></asp:TextBox>
                </td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    League Fees</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtleaguefee_bud" 
                        runat="server" TabIndex="72"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtleaguefee_act"  runat="server"
                        TabIndex="120"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Running</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtstatehandsrun_bud" runat="server"  TabIndex="13"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtstatehandsrun_act" runat="server"  TabIndex="42"></asp:TextBox>
                </td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    Licenses/Permits</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtlicpermits_bud" 
                        runat="server" TabIndex="74"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtlicpermits_act"  runat="server"
                        TabIndex="122"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td  class="lineright">&nbsp;</td>
                <td style="text-align: right">
                    <label style="text-align: right">
                    Limos/Auto</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtlimosauto_bud" runat="server"  TabIndex="76"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtlimosauto_act" runat="server"  TabIndex="124"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4"><b>Wardrobe</b></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    Orchestra Shell Removal</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtorchestrashellrml_bud" runat="server"  TabIndex="78"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtorchestrashellrml_act" runat="server"  TabIndex="126"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Load In</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtwardrobehairin_bud" runat="server"  TabIndex="15"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtwardrobehairin_act" runat="server"  TabIndex="44"></asp:TextBox>
                </td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    Presenter Profit</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtpresenterprofit_bud" runat="server"  TabIndex="80"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtpresenterprofit_act" runat="server"  TabIndex="127"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Load Out</label></td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtwardrobehairout_bud" runat="server"  TabIndex="17"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtwardrobehairout_act" runat="server"  TabIndex="46"></asp:TextBox>
                </td>
                <td class="lineright"></td>
                <td class="chosen-rtl">
                    <label>
                    Police/Security/Fire Marshall</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtpol_sec_fire_mar_bud" runat="server"  TabIndex="82"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtpol_sec_fire_mar_act" runat="server"  TabIndex="129"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Running</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtwardrobehairrun_bud" runat="server"  TabIndex="19"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtwardrobehairrun_act" runat="server"  TabIndex="48"></asp:TextBox>
                </td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">
                    <label>
                    Program</label></td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtprogram_bud" 
                        runat="server" TabIndex="84"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtprogram_act"  runat="server"
                        TabIndex="131"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright"></td>
                <td class="chosen-rtl">Rent</td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtrent_bud" 
                        runat="server" TabIndex="86"></asp:TextBox>
                </td>
                <td align="center" style="text-align: left">
                    <asp:TextBox ID="txtrent_act"  runat="server"
                        TabIndex="133"></asp:TextBox>
                </td>
                <td align="center" style="text-align: left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Insurance (On Drop Count)
                    </label>
                &nbsp;
                                        <asp:TextBox ID="txtinsurnace_de_unit" runat="server"  TabIndex="21" Width="40px"></asp:TextBox>
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtinsurance_bud" runat="server" CssClass="dollor4dec" TabIndex="23"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtinsurance_act" runat="server" CssClass="dollor4dec" TabIndex="50"></asp:TextBox>
                </td>
                <td class="lineright"></td>
                <td class="chosen-rtl">Sound/Lights</td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtsoundlignt_bud" 
                        runat="server" TabIndex="88"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtsoundlignt_act"  runat="server"
                        TabIndex="135"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    &nbsp;Ticket Printing
                    </label>
                &nbsp;
                    <asp:TextBox ID="txtticketprint_de_unit" runat="server"  TabIndex="25" Width="40px"></asp:TextBox>
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtticketprint_de_bud" runat="server"  TabIndex="26"></asp:TextBox>
                </td>
                <td>&nbsp;
                    <asp:TextBox ID="txtticketprint_de_act" runat="server"  TabIndex="52"></asp:TextBox>
                </td>
                <td class="lineright"></td>
                <td class="chosen-rtl">Ticket Printing</td>
                <td align="center">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtticketprint_le_bud" 
                        runat="server" TabIndex="90"></asp:TextBox>
                </td>
                <td align="center" style="text-align: left">
                    <asp:TextBox ID="txtticketprint_le_act" 
                        runat="server" TabIndex="137"></asp:TextBox>
                </td>
                <td align="center" style="text-align: left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">Telephones/Internet</td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="txttel_internet_bud" 
                        runat="server" TabIndex="92"></asp:TextBox>
                </td>
                <td align="center" style="text-align: left">
                    <asp:TextBox ID="txttel_internet_act"  runat="server"
                        TabIndex="139"></asp:TextBox>
                </td>
                <td align="center" style="text-align: left">&nbsp;</td>
            </tr>
            <tr>
                <td align="right" class="auto-style4"><b>Other</b></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">Dry Ice/C02</td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtdryice_bud" 
                        runat="server" TabIndex="94"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtdryice_act"  runat="server"
                        TabIndex="141"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Other 1</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtother1_de_bud" 
                        runat="server" TabIndex="28"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtother1_de_act"  runat="server"
                        TabIndex="54"></asp:TextBox>
                </td>
                <td align="left" class="lineright">&nbsp;</td>
                <td class="chosen-rtl">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <label>
                    Other 2</label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtother2_de_bud" 
                        runat="server" TabIndex="30"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtother2_de_act"  runat="server"
                        TabIndex="57"></asp:TextBox>
                </td>
                <td align="left" class="lineright">&nbsp;</td>
                <td class="chosen-rtl"><b>Other</b></td>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">Other 1</td>
                <td></td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtother1_le_bud" runat="server"  TabIndex="96"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtother1_le_act" runat="server"  TabIndex="143"></asp:TextBox>
                </td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">Other 2</td>
                <td></td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtother2_le_bud" 
                        runat="server" TabIndex="98"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtother2_le_act"  runat="server"
                        TabIndex="146"></asp:TextBox>
                </td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">Other 3</td>
                <td></td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtother3_le_bud" 
                        runat="server" TabIndex="100"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtother3_le_act"  runat="server"
                        TabIndex="149"></asp:TextBox>
                </td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">Other 4</td>
                <td></td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtother4_le_bud" 
                        runat="server" TabIndex="102"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtother4_le_act"  runat="server"
                        TabIndex="152"></asp:TextBox>
                </td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">Other 5</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtother5_le_bud" 
                        runat="server" TabIndex="104"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtother5_le_act"  runat="server"
                        TabIndex="155"></asp:TextBox>
                </td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="lineright">&nbsp;</td>
                <td class="chosen-rtl">Local Fixed</td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtlocalfixed_bud" 
                        runat="server" TabIndex="106"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtlocalfixed_act"  runat="server"
                        TabIndex="158"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div id="Divbox" runat="server" visible="false">
        &nbsp;<table width="1000px" cellpadding="5" cellspacing="0" border="0">
            <tr>
                <td colspan="9"><strong><span class="auto-style1">Box Office</span></strong>&nbsp;&nbsp;                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td width="151" style="text-align: right">Gross Sales </td>
                <td width="281">&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtbxgrpSalesfrm" runat="server" Width="150px"  TabIndex="9"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Drop Count</td>
                <td width="281">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxdrpcountfrom" Width="150px" runat="server"  TabIndex="9"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Paid Attendance</td>
                <td width="281">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxPAfrom" Width="150px" runat="server"  TabIndex="9"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Comp</td>
                <td width="281">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxCompFrom" Width="150px" runat="server"  TabIndex="9"></asp:TextBox>
                </td>
            <tr>
                <td class="chosen-rtl">&nbsp;</td>
                <td >&nbsp;</td>
            </tr>
            <tr>
                <td colspan="1">&nbsp;</td>
                <td colspan="1" style="text-align:left"  ><label ><strong >Ticket Sales</strong></label>
                <td colspan="1" style="text-align:left"><strong>Gross Receipts</strong></td>
            </tr>
            <tr>
                <td style="text-align: right">Subscription </td>
                <td width="281">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxtsSubFrom" runat="server" Width="150px" 
                         TabIndex="9"></asp:TextBox>
                </td>
                <td width="180">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxgrSubFrom" runat="server" Width="150px"
                         TabIndex="9"></asp:TextBox>
                </td>
                <td>&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Phone</td>
                <td width="180">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxtsPhoFrom" Width="150px" runat="server" 
                         TabIndex="9" ></asp:TextBox>
                </td>
                <td>&nbsp;
                    <asp:TextBox ID="txtbxgrPhoFrom" runat="server" Width="150px"  
                        TabIndex="9"></asp:TextBox>
                    
                  &nbsp;&nbsp;
                </td>
            <tr>
                <td style="text-align: right">Internet</td>
                <td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxtsIntFrom" runat="server"   Width="150px"
                        TabIndex="9"></asp:TextBox>
                   
                    &nbsp;&nbsp;</td>
                <td>&nbsp;
                    <asp:TextBox ID="txtbxgrIntFrom" runat="server" Width="150px" 
                        TabIndex="9"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Credit Card</td>
                <td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxtsCreFrom" runat="server"  Width="150px"
                        TabIndex="9"></asp:TextBox>
                </td>
                <td>&nbsp;
                    <asp:TextBox ID="txtbxgrCreFrom" runat="server"   Width="150px"
                        TabIndex="9"></asp:TextBox>
                </td>
            <tr>
                <td style="text-align: right">Remote/Outlet</td>
                <td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxtsRemFrom" runat="server"  Width="150px"
                        TabIndex="9"></asp:TextBox>
                  
&nbsp;<td>&nbsp;
                    <asp:TextBox ID="txtbxgrRemFrom" runat="server"  Width="150px"
                        TabIndex="9"></asp:TextBox>
                    
                &nbsp;&nbsp;
              </td>
              <%--  <asp:DropDownList ID="DropDownList12" runat="server" TabIndex="20">
                </asp:DropDownList>
                &nbsp;&nbsp;
                    <asp:TextBox ID="TextBox22" runat="server" CssClass="" TabIndex="21"></asp:TextBox>--%>
            </tr>
            <tr>
                <td style="text-align: right">Single Tickets</td>
                <td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxtsSinFrom" runat="server"  Width="150px"
                        TabIndex="9"></asp:TextBox>
                    
                    &nbsp;&nbsp;<td>&nbsp;
                    <asp:TextBox ID="txtbxgrSinFrom" runat="server"  Width="150px"
                        TabIndex="9"></asp:TextBox>

                  &nbsp;&nbsp;
                </td>
            <tr>
                <td width="151" style="text-align: right">Group1</td>
                <td width="281">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxtsGrp1From" runat="server" Width="150px"
                       TabIndex="9"></asp:TextBox>
                    
                  &nbsp;<td>&nbsp;
                    <asp:TextBox ID="txtbxgrGrp1From" runat="server" Width="150px"  
                        TabIndex="9"></asp:TextBox>

                &nbsp;&nbsp;
                </td>
            <tr>
                <td width="151" style="text-align: right">Group2</td>
                <td width="281">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbxtsGrp2From" runat="server" Width="150px"
                       TabIndex="9"></asp:TextBox>
                    
                  &nbsp;<td>&nbsp;
                    <asp:TextBox ID="txtbxgrGrp2From" runat="server" Width="150px" 
                        TabIndex="9"></asp:TextBox>
                    
                &nbsp;&nbsp;   
            </tr>
        </table>
    </div>
    <div id="DivDiscount" runat="server" visible="false">
<strong><span class="auto-style1">Discount</span></strong>        <table cellpadding="0" cellspacing="0" border="0" width="800px">
            <tr>
                <td></td>
                <td  >
                    <label>&nbsp;
                    %</label> </td>
                <td>
                    <label>
                    Ticket</label> </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    <label>
                    Subscription</label> </td>
                <td>&nbsp;
                                <asp:TextBox ID="txtbxDSubscription" onchange="highlight();" runat="server"
                                Width="150px" TabIndex="2"></asp:TextBox>
                    &nbsp;
                            </td>
                <td valign="bottom" align="left" style="border-right-style: solid; border-right-width: 1px">
                               
                                    <asp:TextBox ID="txtxsubTktto" onchange="highlight();" runat="server"
                                Width="150px" TabIndex="2"></asp:TextBox>
                    &nbsp;            
                            
                             
    
                            </td>
            </tr>
            <tr>
                <td style="text-align:right" >
                    <label>
                   Group</label> </td>
                <td>&nbsp;<asp:TextBox ID="txtbxGSubFrom" onchange="highlight();" runat="server"
                                Width="150px" TabIndex="2"></asp:TextBox>
                    &nbsp;
                                
                            </td>
                <td  align="left" style="border-right-style: solid; border-right-width: 1px">
       
                            <asp:TextBox ID="txtbxGtktFrom" onchange="highlight();" runat="server"
                                Width="150px" TabIndex="2"></asp:TextBox>
                    &nbsp;
                                
    
                            </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    <label>
                    Miscellaneous</label> </td>
                <td>&nbsp;
                                <asp:TextBox ID="txtbxmis" onchange="highlight();" runat="server"
                                Width="150px" TabIndex="2"></asp:TextBox>
                    &nbsp;
                            </td>
                <td valign="bottom" align="left" style="border-right-style: solid; border-right-width: 1px">
                                 
                                    <asp:TextBox ID="txtbxmisTfrom" onchange="highlight();" runat="server"
                                Width="150px" TabIndex="2"></asp:TextBox>
                    &nbsp;            
                            

    
                            </td>
            </tr>
        </table>
    </div>
    <div id="DivCoversheet" runat="server" visible="false">
        Covertsheet



    </div>
                </td>
            
        
            <td  valign="top" style="padding: 0px 0px 0px 80px" >
                <asp:Panel ID="pnlLastSearch" runat ="server">
                                <table>
                                    <tr>
                                        <td>Last Search 
                    </td>
                                      
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtLastSearch" Rows="3" TextMode="MultiLine" runat="server" Width="600px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                          <td align="left">
                                            <asp:Button ID="btnLastSearch" CssClass="buttn" OnClick="btnLastSearch_Click" runat="server" Text="Last Search" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
            </td>
        </tr>
        </table>

    <div style="height: 300px">
    </div>


</asp:Content>

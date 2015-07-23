<%@ Page Title="Engagement Coversheet" MasterPageFile="~/Engagement.Master" MaintainScrollPositionOnPostback="true" Language="C#" AutoEventWireup="true" CodeBehind="EngagementCoversheet.aspx.cs" Inherits="NTOS.EngagementCoversheet" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .tabpadding {
            padding-top: 10px;
        }

        .bdrfoo {
            border-width: 2px;
            padding: 5px 0 5px 0;
        }

        .divinsdel {
            padding: 0 5px 5px 0;
        }
        
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/jscript" src="Scripts/autoNumeric%201.9.15.js"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            $('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99' });
        });
        function cal_dcc_total(thisid, amt) {
            //debugger;
            //$('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99' });
            var dcc_total = parseFloat($('#<%=hdn_dcc_total.ClientID%>').val());
            var advt_act = $(thisid).autoNumeric('get');
            if (advt_act == "") advt_act = 0;
            advt_act = advt_act - amt;
            $('#<%=lbl_dcc_total.ClientID%>').autoNumeric('set', parseFloat(dcc_total) + parseFloat(advt_act));
        }
        function cal_ocr_total(thisid, amt) {
            // debugger;
            //$('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99' });
            var ocr_total = parseFloat($('#<%=hdn_ocr_total.ClientID%>').val());
            var advt_act = $(thisid).autoNumeric('get');
            if (advt_act == "") advt_act = 0;
            advt_act = advt_act - amt;
            $('#<%=lbl_ocr_total.ClientID%>').autoNumeric('set', parseFloat(ocr_total) + parseFloat(advt_act));
        }
        function setMultiTxtBoxLen(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            $('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99' });
        }
        function CheckAll(cb, tblid, chkid) {
            // debugger
            var flg = cb.checked;
            var t = $('#' + tblid)[0];

            var rep = (tblid == "tblcc") ? $('#<%=rep_dir_com_chg.ClientID%>') : $('#<%=rep_os_com_receivables.ClientID%>');
            var ctrls = document.getElementsByTagName('input');

            for (var i = 1; i < t.rows.length; i++) {
                $(rep.selector + chkid + (i - 1)).attr("checked", flg);
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdncoversheetid" runat="server" />
          <div align="center"><asp:Label ID="lbl_cs" runat="server" Text="Please Create Engagement First" ForeColor="Red"></asp:Label></div>  
            <div runat="server" id="div_cs" visible="false">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="550px">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>

                                        <table cellpadding="0" cellspacing="0">
                                            <tr style="text-transform: capitalize">
                                                <th style="text-align:right">Settlement Document List</th>
                                                <th align="right" width="10px">&nbsp;</th>
                                                <th align="right">Received</th>
                                                <th align="right" width="20px">&nbsp;</th>
                                                <th style="text-align:right">Notes</th>
                                                <th align="right" class="lineright" width="20px">&nbsp;</th>
                                            </tr>
                                            <tr>

                                                <td class="padding_leftright10">Settlement cover page (1 copy)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chksetcvrpage" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtsetcvrpagenotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Guarantee check (2 copies)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkguarantee" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtguaranteenotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Royalty check (2 copies)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkroyalty" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtroyaltynotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Overage check (2 copies)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkoverage" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtoveragenotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Settlement summary (2 originals)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chksetsummary" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtsettlementsumnotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Venue settlement (if subbmited)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkvenuesettlement" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtvenuesettlementnotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Box office sheet</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkboxoffice" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtboxofficenotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Box office statement (in reverse order)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkboxoffsettlement" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtboxoffsettlementnotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Labor bills (Signed off by TD)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chklabourbills" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtlabourbillsnotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Musicians bills (if applicable)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkmusicians" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtmusiciansnotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Local Documented expensive invoice</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chklocaldocexp" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtlocaldocexpnotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Advertising (summary, invoices, tear sheets, etc.,)</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkadvertising" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtadvertisingnotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="padding_leftright10">Contract copy</td>
                                                <td align="center" width="10px">
                                                    &nbsp;</td>
                                                <td align="right">
                                                    <asp:CheckBox ID="chkcontractcopy" runat="server" />
                                                </td>
                                                <td align="center" width="20px">&nbsp;</td>
                                                <td align="right" class="tabpadding">
                                                    <asp:TextBox ID="txtcontractcopynotes" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Rows="2" Columns="30" runat="server"></asp:TextBox></td>
                                                <td align="right" class="lineright" width="20px">&nbsp;</td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px;padding-left:50px">
                                        <label class="padding_leftright10 gridviewheader" style="font-weight: bold;">Distribution List: Email</label>
                                        <asp:Panel ID="pnl" runat="server" ScrollBars="Auto">
                                            <asp:TextBox ID="txtemailidlist" runat="server" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'8000');" TextMode="MultiLine" Columns="70" Rows="8"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="weemail" runat="server" TargetControlID="txtemailidlist" WatermarkText="Enter Email ids with (,) separator!"></ajaxToolkit:TextBoxWatermarkExtender>
                                            <table class="tbl_border" cellpadding="0" cellspacing="0" align="left" border="0" style="display: none">
                                                <tr class="gridviewheader">
                                                    <td align="left">
                                                        <asp:Repeater ID="repdistribution" runat="server">
                                                            <ItemTemplate>
                                                                <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>'>

                                                                    <td class="padding_leftright10"><%#Eval("SlNo") %>.</td>
                                                                    <td class="tabpadding">

                                                                        <asp:TextBox ID="txtemail" runat="server"
                                                                            Width="300px" MaxLength="50" Text='<%#Eval("Email") %>'>
                                                                        </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:HiddenField ID="hdnemaillist" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    
                                </tr>
                            </table>
                        </td>
                        <td width="1000px" align="left" valign="top" style="padding-left: 10px">
                            <table cellpadding="0" cellspacing="0" align="left" border="0" id="tblcc">
                                
                                <tr class="gridviewheader">
                                    <th style="padding-bottom: 0px">
                                        <div>
                                            <asp:ImageButton ID="lnkbtn_dccAdd0" OnClick="lnkbtn_dccAdd_Click" runat="server"  ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px" ToolTip="Add" CausesValidation="false"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkbtn_dccDelete0" OnClick="lnkbtn_dccDelete_Click" runat="server" ImageUrl="~/Images/minus.png"   Height ="15px" Width ="20px" ToolTip="Delete" CausesValidation="false"></asp:ImageButton>
                                            <ajaxToolkit:ConfirmButtonExtender ID="cbedelete" runat="server" ConfirmOnFormSubmit="true" ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtn_dccDelete0"></ajaxToolkit:ConfirmButtonExtender>
                                    </div>
                                    </th>
                                    <th style="width: 350px;">
                                        &nbsp;Direct Charges (Checks written)
                                    </th>
                                    <th style="width: 80px">Charge
                                    </th>
                                    <th style="width: 100px">Check #
                                    </th>
                                    <th style="width: 300px">Notes
                                    </th>
                                    <th style="width: 70px">&nbsp;</th>
                                    <th style="width: 25px" align="left" >
                                        <asp:CheckBox ID="chkSelectAll" runat="server" Visible="false" onclick="CheckAll(this,'tblcc','_chk_dcc_delete_');" />
                                    </th>
                                </tr>
                                <asp:Repeater ID="rep_dir_com_chg" runat="server" OnItemCommand="rep_dir_com_chg_ItemCommand">
                                    <ItemTemplate>
                                        <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>' runat="server" id="tr_dcc_crow">
                                            <td class="padding_leftright10" colspan="2">
                                                <asp:Label ID="lbl_dcc_desc" runat="server" Text='<%#Eval("cvr_chgs_desc") %>'>
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_dcc_charges" CssClass="dollor" Width="120px" runat="server" Text='<%#Eval("cvr_chgs_amt") %>'>
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_dcc_check" runat="server" Width="160px" Text='<%#Eval("cvr_chgs_check") %>'>
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_dcc_notes" runat="server" Width="170px" Text='<%#Eval("cvr_chgs_notes") %>'>
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkEdit_dcc" runat="server" Text="Edit" CommandName="edit" style="text-decoration :underline" CausesValidation="false"></asp:LinkButton>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chk_dcc_delete" runat="server" />
                                                <asp:HiddenField ID="hdnchargesid" runat="server" Value='<%# Bind("cvr_chgs_id") %>' />
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr_dcc_edit" visible="false">
                                            <td class="padding_leftright10" colspan="2">
                                                <asp:TextBox ID="txt_dcc_descE"  style="text-align: right" runat="server" CssClass="txtmixcaps"
                                                    Width="300px" MaxLength="50" Text='<%#Eval("cvr_chgs_desc") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_dcc_chargesE"   style="text-align: right" onkeyup=<%# "javascript:cal_dcc_total(this,'" + Eval("cvr_chgs_amt") + "')" %> Text='<%#Eval("cvr_chgs_amt") %>' Width="150px" CssClass="dollor" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;<asp:TextBox ID="txt_dcc_checkE" Width="150px"  style="text-align: right" runat="server" Text='<%#Eval("cvr_chgs_check") %>'
                                                    MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td style="padding-top: 10px">
                                                <asp:TextBox ID="txt_dcc_notesE"  style="text-align: right" Text='<%#Eval("cvr_chgs_notes") %>' onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Width="170px" runat="server"
                                                    MaxLength="300"></asp:TextBox>
                                            </td>
                                            <td colspan="2">
                                                <asp:LinkButton ID="lnkUpdateDoc" runat="server" Text="Update" CommandName="update"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkCancelDoc" runat="server" Text="Cancel" CommandName="cancel"></asp:LinkButton>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr runat="server" id="tr_dcc_footer">
                                    <td  colspan="2">
                                        <asp:TextBox ID="txt_dcc_desc" runat="server" CssClass="txtmixcaps"
                                            Width="300px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_dcc_charges"  style="text-align: right" onkeyup="cal_dcc_total(this,0);" Width="150px" CssClass="dollor" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                      <asp:TextBox ID="txt_dcc_check" Width="150px" runat="server"  style="text-align: right"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td style="padding-top: 10px">
                                        <asp:TextBox ID="txt_dcc_notes" onpaste="return true"  style="text-align: right" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" Width="170px" runat="server"
                                            MaxLength="300"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 30px; font-weight: bold;" colspan="2">TOTAL&nbsp;:</td>
                                    <td>
                                        <asp:HiddenField ID="hdn_dcc_total" runat="server" />
                                        <asp:Label ID="lbl_dcc_total" Font-Bold="true" runat="server" CssClass="dollor"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="7" class="linebottom"></td>
                                </tr>
                                <tr>
                                    <td colspan="7">&nbsp;</td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" align="left" id="tblcr">
                                
                                <tr class="gridviewheader">
                                    <th style="padding-bottom: 0px;">
                                        <div class="divinsdel">
                                            <asp:ImageButton ID="lnkbtn_ocradd0" OnClick="lnkbtn_ocradd_Click" runat="server"  ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px" ToolTip="Add"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkbtn_ocrdelete" OnClick="lnkbtn_ocrdelete_Click" runat="server"  ImageUrl="~/Images/minus.png"   Height ="15px" Width ="20px"  ToolTip="Delete" CausesValidation="false"></asp:ImageButton>
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="true" ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtn_ocrdelete"></ajaxToolkit:ConfirmButtonExtender>
                                        </div>
                                        </th>

                                    <th style="width: 350px">&nbsp;Outstanding Receivables
                                    </th>
                                    <th style="width: 80px">Charge
                                    </th>

                                    <th style="width: 270px">Notes
                                    </th>
                                    <th style="width: 70px">&nbsp;</th>
                                    <th style="width: 50px"  align="left" >
                                        <asp:CheckBox ID="CheckBox1" Visible="false" runat="server" onclick="CheckAll(this,'tblcr','_chk_ocr_delete_');" />
                                    </th>
                                </tr>
                                <asp:Repeater ID="rep_os_com_receivables" runat="server" OnItemCommand="rep_os_com_receivables_ItemCommand">
                                    <ItemTemplate>
                                        <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle" %>' runat="server" id="tr_ocr_crow">
                                            <td class="padding_leftright10" colspan="2">
                                                <asp:Label ID="lbl_ocr_desc" runat="server" Text='<%#Eval("cvr_cvabls_desc") %>'>
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_ocr_charges" CssClass="dollor" runat="server" Text='<%#Eval("cvr_cvabls_charge") %>'>
                                                </asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_ocr_notes" runat="server" Text='<%#Eval("cvr_cvabls_notes") %>'>
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkEdit_ocr" runat="server" Text="Edit" style="text-decoration :underline"  CommandName="edit" CausesValidation="false"></asp:LinkButton>
                                            </td>
                                            <td align="left">
                                                <asp:HiddenField ID="hdnreceivablesid" runat="server" Value='<%# Bind("cvr_rcvabls_id") %>' />
                                                <asp:CheckBox ID="chk_ocr_delete" runat="server" /></td>
                                        </tr>
                                        </tr>
                                        <tr runat="server" id="tr_ocr_edit" visible="false">
                                            <td colspan="2">
                                                <asp:TextBox ID="txt_ocr_descE"  style="text-align: right"  runat="server" CssClass="txtmixcaps" Text='<%#Eval("cvr_cvabls_desc") %>'
                                                    Width="300px" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_ocr_chargesE"  style="text-align: right" onkeyup=<%# "javascript:cal_ocr_total(this,'" + Eval("cvr_cvabls_charge") + "')" %> CssClass="dollor" runat="server" Text='<%#Eval("cvr_cvabls_charge") %>'
                                                    Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="padding-top: 10px">
                                                &nbsp;<asp:TextBox ID="txt_ocr_notesE" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" runat="server"
                                                    Width="170px" MaxLength="300" Text='<%#Eval("cvr_cvabls_notes") %>'></asp:TextBox>
                                            </td>
                                            <td colspan="2">
                                                <asp:LinkButton ID="lnkUpdateOcr" runat="server" Text="Update" CommandName="update"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkCancelOcr" runat="server" Text="Cancel" CommandName="cancel"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr runat="server" id="tr_ocr_footer">
                                    <td colspan="2">
                                        <asp:TextBox ID="txt_ocr_desc"  style="text-align: right" runat="server" CssClass="txtmixcaps"
                                            Width="300px" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_ocr_charges"  style="text-align: right" onkeyup="cal_ocr_total(this,0);" CssClass="dollor" runat="server"
                                            Width="150px"></asp:TextBox>
                                    </td>
                                    <td style="padding-top: 10px">
                                        &nbsp;<asp:TextBox ID="txt_ocr_notes"  style="text-align: right" onpaste="return true" onkeyDown="setMultiTxtBoxLen(this,event,'300');" TextMode="MultiLine" runat="server"
                                            Width="170px" MaxLength="300"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 30px; font-weight: bold" align="right" colspan="2">TOTAL&nbsp;:</td>
                                    <td>
                                        <asp:HiddenField ID="hdn_ocr_total" runat="server" />
                                        <asp:Label ID="lbl_ocr_total" Font-Bold="true" CssClass="dollor" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="550px">&nbsp;</td>
                        <td align="left" style="padding-left: 10px" valign="top" width="1000px">&nbsp;</td>
                    </tr>
                </table>
                <asp:HiddenField ID="hdn_engagementid" runat="server" />
                <asp:HiddenField ID="hdn_schedulecount" runat="server" />




            </div>








        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

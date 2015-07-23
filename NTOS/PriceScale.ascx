<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PriceScale.ascx.cs" Inherits="NTOS.PriceScale" %>
<script type="text/javascript">
    var thisid = "setfocusid_" + this.ClientID + "('txt_ps1_ticketprice')";
    
    function AddCount_<%=this.ID %>(thisid) {
        var k = $('#<%=hdndelcount.ClientID %>').val();
        if (thisid.checked == true)
            k++;
        else
            k--;
        $('#<%=hdndelcount.ClientID %>').val(k);


    }

    function changemodifystatus() {
        $('#hdn_modify_status').val("1");
    }


    function checkSelRows_<%=this.ID %>() {
        // var k = $('#<%=hdndelcount.ClientID %>').val();
        // if (parseInt(k) == 0) {
        //     alert('select record!');
        //     return false;
        // }
        return confirm('Are you sure to delete the selected record(s)?');
    }
    function setfocus(focid, val) {
        //alert('s');
        //  debugger;
        //$('#<%=hdnfocusid.ClientID %>').val(focid);
        var hdnid = '#' + focid.id.slice(0, 21).replace('g', '').replace('d', '') + "hdnfocusid";
        var hdnindex = '#' + focid.id.slice(0, 21).replace('g', '').replace('d', '') + "hdnfocusindex";
        $(hdnid).val(val);
        $(hdnindex).val(focid.id.split('_')[focid.id.split('_').length - 1]);

    }
    function setfocus1(focid, val) {
        //debugger;
        //var hdnid = focid.id.indexOf('_gdv_ps') + 1;
        //var hdnindex = focid.id.substring(0, 19) + "gdv_ps1_" + val + "_" + focid.id.split('_')[focid.id.split('_').length - 1];
        //$(hdnindex).focus();
        focid.focus();
        //var hdnid = '#' + focid.id.slice(0, 21).replace('g', '').replace('d', '') + "hdnfocusid";
        //var hdnindex = '#' + focid.id.slice(0, 21).replace('g', '').replace('d', '') + "hdnfocusindex";
        //$(hdnid).val(val);
        //$(hdnindex).val(focid.id.split('_')[focid.id.split('_').length - 1]);
    }

    

</script>
<style type="text/css">
    td.column_style_left
    {
        border-left: 1px solid black;
    }    
    td.column_style_right
    {
        border-right: 1px solid black;
    }    
</style>

<asp:HiddenField ID="hdnfocusindex" runat="server" />
<asp:HiddenField ID="hdnfocusid" runat="server" />
<asp:HiddenField ID="hdnps_scale" runat="server" />
<asp:HiddenField ID="hdndelcount" runat="server" Value="0" />
<table style="width:100%" >
    <tr>
        <td colspan="3" align="right">
            <div class="txt_contentheader uchead" style="margin-top: -30px;width:100%">
               
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
                <asp:LinkButton ID="lnkbtnAdd" runat="server" Text="[+]" OnClick="lnkbtnAdd_Click" CssClass="lnkadd" ValidationGroup="peradd" ToolTip="Add"></asp:LinkButton>
                <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="[-]" CssClass="lnkdelete" OnClick="lnkbtnDelete_Click" ToolTip="Delete" CausesValidation="false"></asp:LinkButton>
                <ajaxToolkit:ConfirmButtonExtender ID="cbedelrow" runat="server" ConfirmOnFormSubmit="true" ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtnDelete"></ajaxToolkit:ConfirmButtonExtender>
                <ajaxToolkit:ConfirmButtonExtender ID="cbedelete" runat="server" ConfirmOnFormSubmit="true" ConfirmText="Please save all the data before deleting! Are you sure to delete this price scale?" TargetControlID="btnpricescalehide"></ajaxToolkit:ConfirmButtonExtender>
                <asp:Button ID="btnpricescalehide" CssClass="bluebutt" runat="server" OnClick="btnpricescalehide_Click" Text="Delete Price Scale" />
            </div>
        </td>

    </tr>
    <tr>
        <td class="txt_contentheader linebottom" align="left" style="width:800px;"><label class="heading" style ="padding-right: 400px;">Singles</label> </td>
        <td class="txt_contentheader linebottom" align="left" style="width: 100px;"> <label class="heading" style ="padding-right: 200px;">Subscriptions</label> </td>
        <td class="txt_contentheader linebottom" align="left" style="width: 250px;"> <label class="heading" style ="padding-right: 200px;">Group</label></td>
    </tr>
    <tr>
        <td colspan="3" style="width: 100%">
            <asp:GridView Width="100%" ID="gdv_ps1" OnRowCommand="gdv_ps1_RowCommand" runat="server" AutoGenerateColumns="False" DataKeyNames="ps_id" HeaderStyle-CssClass="gridviewheader" RowStyle-CssClass="gdvrowstyle" AlternatingRowStyle-CssClass="gdvaltrowstyle" GridLines="None" OnRowDataBound="gdv_ps1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Price Level">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("ps_price_level") %>' ID="lbl_ps1_pricelevel"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seats">
                        <ItemTemplate>
                            <asp:TextBox runat="server"  style="text-align: right" CssClass="seatsint" onkeyup="cal(this);" EnableViewState="false" Text='<%# Bind("ps_seats_single") %>' ID="txt_ps1_seats" OnTextChanged="txt_ps1_seats_TextChanged" AutoPostBack="false"></asp:TextBox>
                            <%--<asp:RangeValidator ID="rgv_seats" runat="server" ControlToValidate="txt_ps1_seats" ForeColor="Red" ToolTip="Numeric Field, maximum limit is 32767" MinimumValue="0" MaximumValue="32767" Type="Integer">#</asp:RangeValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ticket Price">
                        <ItemTemplate>
                            <asp:TextBox runat="server"  style="text-align: right" CssClass="dollor" onkeyup="cal(this);" Text='<%# Bind("ps_t_price_single", "{0:C2}") %>' ID="txt_ps1_ticketprice"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sale Amount">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("ps_sale_amount", "{0:C2}") %>' ID="lbl_ps1_saleamount" CssClass="dollor"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seating Detail">
                        <ItemTemplate>
                            <asp:TextBox MaxLength="100" runat="server" Text='<%# Bind("ps_seat_detail_single") %>' ID="txt_ps1_seatingdetail"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  >
                                                 <ItemStyle CssClass="column_style_left" />
                        <ItemTemplate>
                            <asp:DropDownList ID="drp_ps1_sunit" onchange="cal();" Width="50px" SkinID="ddlsmaller" runat="server" SelectedValue='<%# Bind("ps_discount_unit_sub") %>'>

                                <asp:ListItem Value="%">%</asp:ListItem>
                                <asp:ListItem Value="$">$</asp:ListItem>
                                <asp:ListItem Value="" Enabled="false"></asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount">

                        <ItemTemplate >
                            <asp:TextBox runat="server"  style="text-align: right" CssClass="decimalonly10" onchange="cal();" Text='<%# Bind("ps_discount_sub") %>' ID="txt_ps1_sdiscount"></asp:TextBox>
                            <%--<asp:RangeValidator ID="rgv_sdiscount" runat="server" ControlToValidate="txt_ps1_sdiscount" ForeColor="Red" ToolTip="Decimal Field, maximum limit is xxxxxxxx.xx" MaximumValue="99999999.99" Type="Double">#</asp:RangeValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ticket Price">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("ps_t_price_sub", "{0:C2}") %>' CssClass="dollor" ID="lbl_ps1_sprice"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField >
                         <ItemStyle CssClass="column_style_left" />
                        <ItemTemplate >
                            <asp:DropDownList ID="drp_ps1_gunit" runat="server" Width="50px" onkeyup="setfocus(this,'drp_ps1_gunit');" SkinID="ddlsmaller" onchange="cal(this);" SelectedValue='<%# Bind("ps_discount_unit_grp") %>'>
                                <asp:ListItem Value="%">%</asp:ListItem>
                                <asp:ListItem Value="$">$</asp:ListItem>
                                <asp:ListItem Value="" Enabled="false"></asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount">
                        
                        <ItemTemplate>
                            <asp:TextBox runat="server"  style="text-align: right" CssClass="decimalonly10"  onchange="cal();"  Text='<%# Bind("ps_discount_grp") %>' ID="txt_ps1_gdiscount"></asp:TextBox>
                            <%--<asp:RangeValidator ID="rgv_gdiscount" runat="server" ControlToValidate="txt_ps1_gdiscount" ForeColor="Red" ToolTip="Decimal Field, maximum limit is xxxxxxxx.xx" MaximumValue="99999999.99" Type="Double">#</asp:RangeValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ticket Price">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("ps_t_price_grp", "{0:C2}") %>' CssClass="dollor" ID="lbl_ps1_gprice"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkdelete" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="txt_contentsection" colspan="2" align="center" style="width: 650px;">Sub Total &nbsp;&nbsp;Seat  :&nbsp;<asp:Label ID="lbl_seattotal"  CssClass="txt_bold" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Schedule  :&nbsp;<asp:Label ID="lbl_ps1total" runat="server" CssClass="txt_bold dollor" Text=""></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;of Shows :&nbsp;<asp:Label ID="lbl_ps1shows"  CssClass="txt_bold" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sale :&nbsp;<asp:Label ID="lbl_ps1subtotal" runat="server" CssClass="txt_bold dollor" Text=""></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>

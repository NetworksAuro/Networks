<%@ Page Title="Engagement Expenses" MasterPageFile="~/Engagement.Master" Language="C#"
    AutoEventWireup="true" CodeBehind="EngagementExpenses.aspx.cs" Inherits="NTOS.EngagementExpenses" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .rowspace {
            padding-bottom: 15px;
        }

        .bdrwidth {
            border-width: 2px;
            font-weight: bold;
            text-align: center;
        }

        #MainContent_lblsubtotal_le_bud3 {
            text-align: center;
        }

        #MainContent_lblsubtotal_le_bud4 {
            text-align: center;
        }

        #MainContent_lblsubtotal_le_bud {
            text-align: center;
        }

        #MainContent_lblsubtotal_le_bud5 {
            text-align: center;
        }

        #MainContent_lblsubtotal_le_bud8 {
            text-align: center;
        }

        #MainContent_lblsubtotal_le_bud7 {
            text-align: center;
        }

        #MainContent_lblsubtotal_le_bud7 {
            text-align: center;
        }

        .auto-style2 {
            text-align: left;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">

    </script>
    <script type="text/jscript" src="Scripts/autoNumeric%201.9.15.js"></script>
    <%--<script type="text/jscript" src="Scripts/autoNumeric-1.7.5.js"></script>--%>

    <script type="text/javascript">
        jQuery(function ($) {
            $('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99', nBracket: '(,)' });
            $('.dollor4dec').autoNumeric({ aSign: '$', vMax: '999999999999999.9999', vMin: '-999999999999999.9999', nBracket: '(,)' });
        });
    </script>

    <script type="text/javascript">
        function copytoact(thisid) {
            $('#<%=txtlocalfixed_act.ClientID%>').val(thisid.value);
        }
        function Cal_ins(flg) {
            // debugger
            $('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99', nBracket: '(,)' });
            $('.dollor4dec').autoNumeric({ aSign: '$', vMax: '999999999999999.9999', vMin: '-999999999999999.9999', nBracket: '(,)' });
            var insurnace_de_unit = parseFloat($('#<%=txtinsurnace_de_unit.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(insurnace_de_unit)) insurnace_de_unit = 0;
            var price_scl_cap = $('#<%=hdn_pricescale_cap.ClientID%>').val();
            var bo_drop_count = $('#<%=hdn_bodropcount.ClientID%>').val();
            var attendance = $('#<%=hdn_attendance.ClientID%>').val();
            var NoofShows = $('#<%=hdnnoofshows.ClientID%>').val();
            var ins_bud_amt = insurnace_de_unit * price_scl_cap * NoofShows;
            var ins_act_amt = insurnace_de_unit * bo_drop_count;
            if (flg == "insurance") {
                if ($('#<%=txtinsurnace_de_unit.ClientID%>').val() != "" && price_scl_cap != "")
                   $('#<%=txtinsurance_bud.ClientID%>').autoNumeric('set', ins_bud_amt);
               if ($('#<%=txtinsurnace_de_unit.ClientID%>').val() != "" && bo_drop_count != "")
                   $('#<%=txtinsurance_act.ClientID%>').autoNumeric('set', ins_act_amt);
            }
            if (flg == "ticketprinting") {
                var ticketprint_de_unit = parseFloat($('#<%=txtticketprint_de_unit.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
                if (isNaN(ticketprint_de_unit)) ticketprint_de_unit = 0;
                var tkt_bud_amt = 0;
                var tkt_act_amt = ticketprint_de_unit * attendance;
                tkt_bud_amt = ticketprint_de_unit * price_scl_cap * NoofShows;
                if ($('#<%=txtticketprint_de_unit.ClientID%>').val() != "" && attendance != "")
                     $('#<%=txtticketprint_de_act.ClientID%>').autoNumeric('set', tkt_act_amt);
                 if ($('#<%=txtticketprint_de_unit.ClientID%>').val() != "" && price_scl_cap != "")
                    $('#<%=txtticketprint_de_bud.ClientID%>').autoNumeric('set', tkt_bud_amt);
             }
         }
         function cal_total() {
             $('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99', nBracket: '(,)' });
             var advt_bud = parseFloat($('#<%=txtadvt_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
             if (isNaN(advt_bud)) advt_bud = 0;
             var advt_act = parseFloat($('#<%=txtadvt_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
             if (isNaN(advt_act)) advt_act = 0;
             var statehandin_bud = parseFloat($('#<%=txtstatehandin_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(statehandin_bud)) statehandin_bud = 0;
            var statehandin_act = parseFloat($('#<%=txtstatehandin_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(statehandin_act)) statehandin_act = 0;
            var statehandout_bud = parseFloat($('#<%=txtstatehandout_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(statehandout_bud)) statehandout_bud = 0;
            var statehandout_act = parseFloat($('#<%=txtstatehandout_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(statehandout_act)) statehandout_act = 0;
            var statehandsrun_bud = parseFloat($('#<%=txtstatehandsrun_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(statehandsrun_bud)) statehandsrun_bud = 0;
            var statehandsrun_act = parseFloat($('#<%=txtstatehandsrun_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(statehandsrun_act)) statehandsrun_act = 0;
            var wardrobehairin_bud = parseFloat($('#<%=txtwardrobehairin_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(wardrobehairin_bud)) wardrobehairin_bud = 0;
            var wardrobehairin_act = parseFloat($('#<%=txtwardrobehairin_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(wardrobehairin_act)) wardrobehairin_act = 0;
            var wardrobehairout_bud = parseFloat($('#<%=txtwardrobehairout_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(wardrobehairout_bud)) wardrobehairout_bud = 0;
            var wardrobehairout_act = parseFloat($('#<%=txtwardrobehairout_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(wardrobehairout_act)) wardrobehairout_act = 0;
            var wardrobehairrun_bud = parseFloat($('#<%=txtwardrobehairrun_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(wardrobehairrun_bud)) wardrobehairrun_bud = 0;
            var wardrobehairrun_act = parseFloat($('#<%=txtwardrobehairrun_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(wardrobehairrun_act)) wardrobehairrun_act = 0;
            var labourcatering_bud = parseFloat($('#<%=txtlabourcatering_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(labourcatering_bud)) labourcatering_bud = 0;
            var labourcatering_act = parseFloat($('#<%=txtlabourcatering_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(labourcatering_act)) labourcatering_act = 0;
            var musicians_bud = parseFloat($('#<%=txtmusicians_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(musicians_bud)) musicians_bud = 0;
            var musicians_act = parseFloat($('#<%=txtmusicians_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(musicians_act)) musicians_act = 0;
            var insurnace_de_unit = parseFloat($('#<%=txtinsurnace_de_unit.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(insurnace_de_unit)) insurnace_de_unit = 0;

            var price_scl_cap = $('#<%=hdn_pricescale_cap.ClientID%>').val();
            var NoofShows = $('#<%=hdnnoofshows.ClientID%>').val();
             var bo_drop_count = $('#<%=hdn_bodropcount.ClientID%>').val();
             var attendance = $('#<%=hdn_attendance.ClientID%>').val();
             var ins_bud_amt = insurnace_de_unit * price_scl_cap * NoofShows;
             var ins_act_amt = insurnace_de_unit * bo_drop_count;
             //if ($('#<%=txtinsurnace_de_unit.ClientID%>').val() != "")
             //    $('#<%=txtinsurance_bud.ClientID%>').autoNumeric('set', ins_bud_amt);
             //if ($('#<%=txtinsurnace_de_unit.ClientID%>').val() != "")
             //    $('#<%=txtinsurance_act.ClientID%>').autoNumeric('set', ins_act_amt);
             var insurance_bud = parseFloat($('#<%=txtinsurance_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
             if (isNaN(insurance_bud)) insurance_bud = 0;
             var insurance_act = parseFloat($('#<%=txtinsurance_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
             if (isNaN(insurance_act)) insurance_act = 0;
             var ticketprint_de_unit = parseFloat($('#<%=txtticketprint_de_unit.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(ticketprint_de_unit)) ticketprint_de_unit = 0;

            var tkt_act_amt = ticketprint_de_unit * attendance;
             //if ($('#<%=txtticketprint_de_unit.ClientID%>').val() != "")
             //    $('#<%=txtticketprint_de_act.ClientID%>').autoNumeric('set', tkt_act_amt);
             var ticketprint_de_bud = parseFloat($('#<%=txtticketprint_de_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
             if (isNaN(ticketprint_de_bud)) ticketprint_de_bud = 0;
             var ticketprint_de_act = parseFloat($('#<%=txtticketprint_de_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
             if (isNaN(ticketprint_de_act)) ticketprint_de_act = 0;
             var other1_de_bud = parseFloat($('#<%=txtother1_de_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other1_de_bud)) other1_de_bud = 0;
            var other1_de_act = parseFloat($('#<%=txtother1_de_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other1_de_act)) other1_de_act = 0;
            var other2_de_bud = parseFloat($('#<%=txtother2_de_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other2_de_bud)) other2_de_bud = 0;
            var other2_de_act = parseFloat($('#<%=txtother2_de_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other2_de_act)) other2_de_act = 0;

            var subtotal_de_bud = advt_bud + statehandin_bud + statehandout_bud + statehandsrun_bud + wardrobehairin_bud + wardrobehairrun_bud + labourcatering_bud + musicians_bud + insurance_bud + ticketprint_de_bud + other1_de_bud + other2_de_bud;
            var subtotal_de_act = advt_act + statehandin_act + statehandout_act + statehandsrun_act + wardrobehairin_act + wardrobehairout_act + wardrobehairrun_act + labourcatering_act + musicians_act + insurance_act + ticketprint_de_act + other1_de_act + other2_de_act;

            $('#<%=lblsubtotal_de_bud.ClientID%>').autoNumeric('set', subtotal_de_bud);
            $('#<%=lblsubtotal_de_act.ClientID%>').autoNumeric('set', subtotal_de_act);

             var adaexp_bud = parseFloat($('#<%=txtadaexp_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
             if (isNaN(adaexp_bud)) adaexp_bud = 0;
             var adaexp_act = parseFloat($('#<%=txtadaexp_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
             if (isNaN(adaexp_act)) adaexp_act = 0;
             var boxoff_bud = parseFloat($('#<%=txtboxoff_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(boxoff_bud)) boxoff_bud = 0;
            var boxoff_act = parseFloat($('#<%=txtboxoff_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(boxoff_act)) boxoff_act = 0;
            var catering_bud = parseFloat($('#<%=txtcatering_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(catering_bud)) catering_bud = 0;
            var catering_act = parseFloat($('#<%=txtcatering_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(catering_act)) catering_act = 0;
            var eqiprental_bud = parseFloat($('#<%=txteqiprental_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(eqiprental_bud)) eqiprental_bud = 0;
            var eqiprental_act = parseFloat($('#<%=txteqiprental_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(eqiprental_act)) eqiprental_act = 0;
            var grpsaleexp_bud = parseFloat($('#<%=txtgrpsaleexp_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(grpsaleexp_bud)) grpsaleexp_bud = 0;
            var grpsaleexp_act = parseFloat($('#<%=txtgrpsaleexp_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(grpsaleexp_act)) grpsaleexp_act = 0;
            var thousestaff_bud = parseFloat($('#<%=txthousestaff_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(thousestaff_bud)) thousestaff_bud = 0;
            var thousestaff_act = parseFloat($('#<%=txthousestaff_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(thousestaff_act)) thousestaff_act = 0;
            var leaguefee_bud = parseFloat($('#<%=txtleaguefee_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(leaguefee_bud)) leaguefee_bud = 0;
            var leaguefee_act = parseFloat($('#<%=txtleaguefee_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(leaguefee_act)) leaguefee_act = 0;
            var licpermits_bud = parseFloat($('#<%=txtlicpermits_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(licpermits_bud)) licpermits_bud = 0;
            var licpermits_act = parseFloat($('#<%=txtlicpermits_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(licpermits_act)) licpermits_act = 0;
            var limosauto_bud = parseFloat($('#<%=txtlimosauto_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(limosauto_bud)) limosauto_bud = 0;
            var limosauto_act = parseFloat($('#<%=txtlimosauto_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(limosauto_act)) limosauto_act = 0;
            var orchestrashellrml_bud = parseFloat($('#<%=txtorchestrashellrml_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(orchestrashellrml_bud)) orchestrashellrml_bud = 0;
            var orchestrashellrml_act = parseFloat($('#<%=txtorchestrashellrml_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(orchestrashellrml_act)) orchestrashellrml_act = 0;
            var presenterprofit_bud = parseFloat($('#<%=txtpresenterprofit_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(presenterprofit_bud)) presenterprofit_bud = 0;
            var presenterprofit_act = parseFloat($('#<%=txtpresenterprofit_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(presenterprofit_act)) presenterprofit_act = 0;
            var pol_sec_fire_mar_bud = parseFloat($('#<%=txtpol_sec_fire_mar_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(pol_sec_fire_mar_bud)) pol_sec_fire_mar_bud = 0;
            var pol_sec_fire_mar_act = parseFloat($('#<%=txtpol_sec_fire_mar_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(pol_sec_fire_mar_act)) pol_sec_fire_mar_act = 0;
            var program_bud = parseFloat($('#<%=txtprogram_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(program_bud)) program_bud = 0;
            var program_act = parseFloat($('#<%=txtprogram_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(program_act)) program_act = 0;
            var rent_bud = parseFloat($('#<%=txtrent_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(rent_bud)) rent_bud = 0;
            var rent_act = parseFloat($('#<%=txtrent_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(rent_act)) rent_act = 0;
            var soundlignt_bud = parseFloat($('#<%=txtsoundlignt_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(soundlignt_bud)) soundlignt_bud = 0;
            var soundlignt_act = parseFloat($('#<%=txtsoundlignt_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(soundlignt_act)) soundlignt_act = 0;
            var ticketprint_le_bud = parseFloat($('#<%=txtticketprint_le_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(ticketprint_le_bud)) ticketprint_le_bud = 0;
            var ticketprint_le_act = parseFloat($('#<%=txtticketprint_le_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(ticketprint_le_act)) ticketprint_le_act = 0;
            var tel_internet_bud = parseFloat($('#<%=txttel_internet_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(tel_internet_bud)) tel_internet_bud = 0;
            var tel_internet_act = parseFloat($('#<%=txttel_internet_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(tel_internet_act)) tel_internet_act = 0;
            var dryice_bud = parseFloat($('#<%=txtdryice_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(dryice_bud)) dryice_bud = 0;
            var dryice_act = parseFloat($('#<%=txtdryice_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(dryice_act)) dryice_act = 0;
            var other1_le_bud = parseFloat($('#<%=txtother1_le_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other1_le_bud)) other1_le_bud = 0;
            var other1_le_act = parseFloat($('#<%=txtother1_le_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other1_le_act)) other1_le_act = 0;
            var other2_le_bud = parseFloat($('#<%=txtother2_le_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other2_le_bud)) other2_le_bud = 0;
            var other2_le_act = parseFloat($('#<%=txtother2_le_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other2_le_act)) other2_le_act = 0;
            var other3_le_bud = parseFloat($('#<%=txtother3_le_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other3_le_bud)) other3_le_bud = 0;
            var other3_le_act = parseFloat($('#<%=txtother3_le_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other3_le_act)) other3_le_act = 0;
            var other4_le_bud = parseFloat($('#<%=txtother4_le_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other4_le_bud)) other4_le_bud = 0;
            var other4_le_act = parseFloat($('#<%=txtother4_le_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other4_le_act)) other4_le_act = 0;
            var other5_le_bud = parseFloat($('#<%=txtother5_le_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other5_le_bud)) other5_le_bud = 0;
            var other5_le_act = parseFloat($('#<%=txtother5_le_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(other5_le_act)) other5_le_act = 0;
            var localfixed_bud = parseFloat($('#<%=txtlocalfixed_bud.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(localfixed_bud)) localfixed_bud = 0;
            var localfixed_act = parseFloat($('#<%=txtlocalfixed_act.ClientID%>').val().replace(/[^0-9.\s]/gi, ''));
            if (isNaN(localfixed_act)) localfixed_act = 0;
            var subtotal_le_bud = adaexp_bud + boxoff_bud + catering_bud + eqiprental_bud + grpsaleexp_bud + thousestaff_bud + leaguefee_bud + licpermits_bud + limosauto_bud + orchestrashellrml_bud + presenterprofit_bud + pol_sec_fire_mar_bud + program_bud + rent_bud + soundlignt_bud + ticketprint_le_bud + tel_internet_bud + dryice_bud + other1_le_bud + other2_le_bud + other3_le_bud + other4_le_bud + other5_le_bud + localfixed_bud;
            var subtotal_le_act = adaexp_act + boxoff_act + catering_act + eqiprental_act + grpsaleexp_act + thousestaff_act + leaguefee_act + licpermits_act + limosauto_act + orchestrashellrml_act + presenterprofit_act + pol_sec_fire_mar_act + program_act + rent_act + soundlignt_act + ticketprint_le_act + tel_internet_act + dryice_act + other1_le_act + other2_le_act + other3_le_act + other4_le_act + other5_le_act + localfixed_act;


            $('#<%=lblsubtotal_le_bud.ClientID%>').autoNumeric('set', subtotal_le_bud);
            $('#<%=lblsubtotal_le_act.ClientID%>').autoNumeric('set', subtotal_le_act);

             $('#<%=lbltotal_eng_bud.ClientID%>').autoNumeric('set', (subtotal_de_bud + subtotal_le_bud));
             $('#<%=lbltotal_eng_act.ClientID%>').autoNumeric('set', (subtotal_de_act + subtotal_le_act));

         }
    </script>

    <asp:UpdatePanel ID="updpanel1" runat="server">
        <ContentTemplate>
            <%-- <asp:Button CausesValidation="false" runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />--%>
           <div align="center"><asp:Label ID="lbl_ex" runat="server" Text="Please Create Engagement First"
                ForeColor="Red"></asp:Label></div> 
            <div runat="server" id="div_ex" visible="false">
                  
                <table cellspacing="0" cellpadding="0">
                        
                        <tr>
                            <td colspan="10">
                                <div class="t5-border">
                                    <table width="1550" cellspacing="0" cellpadding="0">
                                        <!--<tr>
                                            <td height="20" colspan="4" align="left" class="heading">&nbsp;</td>
                                            <td align="left" class="heading" height="20">&nbsp;</td>
                                            <td rowspan="29" align="left"></td>
                                            <td height="20" colspan="4" align="left" class="heading">&nbsp;</td>
                                            <td align="left" class="heading" height="20">&nbsp;</td>
                                        </tr>-->
                                       
                                        <tr>
                                            <td class="heading" align="right"><strong>Documented</strong></td>
                                            <td colspan="4" class="lineright"></td>
                                            <td align="right" class="heading"><strong>Local</strong></td>
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <th height="40" class="heading" align="center"></th>
                                            <th align="left"></th>
                                            <th align="left">Budgeted</th>
                                            <th align="left">Actual</th>
                                            <th align="left" class="lineright">&nbsp;</th>
                                            <th align="center" class="heading"></th>
                                            <th align="center">&nbsp;</th>
                                            <th align="left">Budgeted</th>
                                            <th align="left">Actual</th>
                                            <th align="left">&nbsp;</th>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>Advertising(Gross)</label></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtadvt_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="1"></asp:TextBox></td>
                                            <td><span class="Dollar" id="MainContent_lblSubscriptionAmusementtax"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txtadvt_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="16"></asp:TextBox>
                                            </span></td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>ADA Expense</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtadaexp_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="31"></asp:TextBox></td>
                                            <td><span class="Dollar" id="MainContent_lblSubsSalesNetcommission"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txtadaexp_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="55"></asp:TextBox>
                                            </span></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Labor Catering</label></td>
                                            <td>
                                                </td>
                                            <td>
                                                <asp:TextBox ID="txtlabourcatering_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="2"></asp:TextBox>
                                            </td>
                                            <td><span id="Span5" class="Dollar" style="display: inline-block;">
                                                <asp:TextBox ID="txtlabourcatering_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="17"></asp:TextBox>
                                                </span></td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>Box Office</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtboxoff_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="32"></asp:TextBox></td>
                                            <td><span class="Dollar" id="MainContent_lblPhoneNetCommsn"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txtboxoff_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="56"></asp:TextBox>
                                            </span></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Musicians</label</td>
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="txtmusicians_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="3"></asp:TextBox>
                                                </td>
                                                <td><span id="Span7" class="Dollar" style="display: inline-block;">
                                                    <asp:TextBox ID="txtmusicians_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="18"></asp:TextBox>
                                                    </span></td>
                                                <td class="lineright">&nbsp;</td>
                                                <td class="chosen-rtl">
                                                    <label>
                                                    Catering</label></td>
                                                <td align="center">&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtcatering_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="33"></asp:TextBox>
                                                </td>
                                                <td><span id="MainContent_lblInternetNetCommsn" class="Dollar" style="display: inline-block;">
                                                    <asp:TextBox ID="txtcatering_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="57"></asp:TextBox>
                                                    </span></td>
                                                <td>&nbsp;</td>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                &nbsp;</td>
                                            <td></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>Equipment Rental</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txteqiprental_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="34"></asp:TextBox></td>
                                            <td><span class="Dollar" id="MainContent_lblCreditNetCommsn"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txteqiprental_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="58"></asp:TextBox>
                                            </span></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <b>Stage Hands</b></td>
                                            <td>
                                                </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>Group Sales Expenses</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtgrpsaleexp_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="35"></asp:TextBox></td>
                                            <td><span class="Dollar" id="MainContent_lbltax2Netcommsn"
                                                style="display: inline-block;">
                                               <asp:TextBox ID="txtgrpsaleexp_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                TabIndex="59"></asp:TextBox>
                                            </span></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Load In</label></td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtstatehandin_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="4"></asp:TextBox>
                                                </td>
                                            <td><span id="MainContent_lblPhoneAmusementtax" class="Dollar" style="display: inline-block;">
                                                <asp:TextBox ID="txtstatehandin_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="19"></asp:TextBox>
                                                </span></td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>House Staff</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td><asp:TextBox ID="txthousestaff_bud" CssClass="dollor" onkeyup="cal_total();"
                                                runat="server" TabIndex="36"></asp:TextBox></td>
                                            <td><span class="Dollar" id="Span2"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txthousestaff_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="60"></asp:TextBox>
                                            </span></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Load Out</label></td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtstatehandout_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="5"></asp:TextBox>
                                                </td>
                                            <td><span id="MainContent_lblInternetAmusemntTax" class="Dollar" style="display: inline-block;">
                                                <asp:TextBox ID="txtstatehandout_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="20"></asp:TextBox>
                                                </span></td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>League Fees</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtleaguefee_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="37"></asp:TextBox></td>
                                            <td><span class="Dollar" id="Span4"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txtleaguefee_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="61"></asp:TextBox>
                                            </span></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Running</label></td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtstatehandsrun_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="6"></asp:TextBox>
                                                </td>
                                            <td>
                                                <span id="MainContent_lblCreditAmusementtax" class="Dollar" style="display: inline-block;">
                                                <asp:TextBox ID="txtstatehandsrun_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="21"></asp:TextBox>
                                                </span>
                                            </td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>Licenses/Permits</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtlicpermits_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="38"></asp:TextBox></td>
                                            <td><span class="Dollar" id="Span6"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txtlicpermits_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="62"></asp:TextBox>
                                            </span></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                &nbsp;<td></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>
                                                Limos/Auto</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtlimosauto_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="39"></asp:TextBox>
                                            </td>
                                            <td><span id="Span8" class="Dollar" style="display: inline-block;">
                                                <asp:TextBox ID="txtlimosauto_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="63"></asp:TextBox>
                                                </span></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <b>Wardrobe</b><td></td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="lineright">&nbsp;</td>
                                                <td class="chosen-rtl">
                                                    <label>
                                                    Orchestra Shell Removal</label></td>
                                                <td align="center">&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtorchestrashellrml_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="40"></asp:TextBox>
                                                </td>
                                                <td><span id="Span9" class="Dollar" style="display: inline-block;">
                                                    <asp:TextBox ID="txtorchestrashellrml_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="64"></asp:TextBox>
                                                    </span></td>
                                                <td>&nbsp;</td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Load In</label><td></td>
                                                <td>
                                                    <asp:TextBox ID="txtwardrobehairin_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="7"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtwardrobehairin_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="22"></asp:TextBox>
                                                </td>
                                                <td class="lineright">&nbsp;</td>
                                                <td class="chosen-rtl">
                                                    <label>
                                                    Presenter Profit</label></td>
                                                <td align="center">&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtpresenterprofit_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="41"></asp:TextBox>
                                                </td>
                                                <td><span id="Span11" class="Dollar" style="display: inline-block;">
                                                    <asp:TextBox ID="txtpresenterprofit_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="65"></asp:TextBox>
                                                    </span></td>
                                                <td>&nbsp;</td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Load Out</label><td>&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtwardrobehairout_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="8"></asp:TextBox>
                                                </td>
                                                <td><span id="Span1" class="Dollar" style="display: inline-block;">
                                                    <asp:TextBox ID="txtwardrobehairout_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="23"></asp:TextBox>
                                                    </span></td>
                                                <td class="lineright"></td>
                                                <td class="chosen-rtl">
                                                    <label>
                                                    Police/Security/Fire Marshall</label></td>
                                                <td align="center">&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtpol_sec_fire_mar_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="42"></asp:TextBox>
                                                </td>
                                                <td><span id="Span13" class="Dollar" style="display: inline-block;">
                                                    <asp:TextBox ID="txtpol_sec_fire_mar_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="66"></asp:TextBox>
                                                    </span></td>
                                                <td>&nbsp;</td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Running</label></td>                                            
                                            <td>
                                                
                                                </td>
                                            <td>
                                                <asp:TextBox ID="txtwardrobehairrun_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="9"></asp:TextBox>
                                            </td>
                                            <td>
                                                <span id="Span3" class="Dollar" style="display: inline-block;">
                                                <asp:TextBox ID="txtwardrobehairrun_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="24"></asp:TextBox>
                                                </span>
                                            </td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">
                                                <label>Program</label></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtprogram_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="43"></asp:TextBox></td>
                                            <td><span class="Dollar" id="Span15"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txtprogram_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="67"></asp:TextBox>
                                            </span></td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td class="chosen-rtl">&nbsp;</td>
                                            <td>
                                                
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="lineright"></td>
                                            <td class="chosen-rtl">Rent</span></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtrent_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="44"></asp:TextBox></td>
                                            <td align="center" style="text-align: left">
                                                <asp:TextBox ID="txtrent_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="68"></asp:TextBox></td>
                                            <td align="center" style="text-align: left">&nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Insurance (On Drop Count)
                                                </label>&nbsp;<asp:TextBox ID="txtinsurnace_de_unit" runat="server" CssClass="dollor" onkeyup="Cal_ins('insurance');cal_total();" TabIndex="10" Width="40px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                
                                                &nbsp;<asp:TextBox ID="txtinsurance_bud" runat="server" CssClass="dollor4dec" onkeyup="cal_total();" TabIndex="11"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;<asp:TextBox ID="txtinsurance_act" runat="server" CssClass="dollor4dec" onkeyup="cal_total();" TabIndex="25"></asp:TextBox>
                                            </td>
                                            <td class="lineright"></td>
                                            <td class="chosen-rtl">Sound/Lights</span></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtsoundlignt_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="45"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtsoundlignt_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="69"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                &nbsp;Ticket Printing
                                                </label>
                                                <asp:TextBox ID="txtticketprint_de_unit" runat="server" CssClass="dollor" onkeyup="Cal_ins('ticketprinting');cal_total();" TabIndex="12" Width="40px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>&nbsp;<asp:TextBox ID="txtticketprint_de_bud" runat="server" CssClass="dollor4dec" onkeyup="cal_total();" TabIndex="13"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;<span id="Span10" class="Dollar" style="display: inline-block;"><asp:TextBox ID="txtticketprint_de_act" runat="server" CssClass="dollor4dec" onkeyup="cal_total();" TabIndex="26"></asp:TextBox>
                                                </span></td>
                                            <td class="lineright"></td>
                                            <td class="chosen-rtl">Ticket Printing</span></td>
                                            <td align="center">&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtticketprint_le_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="46"></asp:TextBox></td>
                                            <td align="center" style="text-align: left">
                                                <asp:TextBox ID="txtticketprint_le_act" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="70"></asp:TextBox></td>
                                            <td align="center" style="text-align: left">&nbsp;</td>
                                        </tr>



                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">Telephones/Internet</span></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txttel_internet_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="47"></asp:TextBox></td>
                                            <td align="center" style="text-align: left">
                                                <asp:TextBox ID="txttel_internet_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="71"></asp:TextBox></td>
                                            <td align="center" style="text-align: left">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right"><b>Other</b></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl"><span style="color: rgb(153, 153, 162); font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 25px; orphans: auto; text-align: -webkit-right; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">Dry Ice/C02</span></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtdryice_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="48"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtdryice_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="72"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Other 1</label></td>
                                            <td>
                                                </td>
                                            <td>
                                                <asp:TextBox ID="txtother1_de_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="14"></asp:TextBox></td>
                                            <td><span class="Dollar" id="Span12"
                                                style="display: inline-block;">
                                             <asp:TextBox ID="txtother1_de_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                TabIndex="27"></asp:TextBox>
                                            </span></td>
                                            <td align="left" class="lineright">
                                                <asp:DropDownList ID="ddlDocumentother1" runat="server" SkinID="ddlmedium" TabIndex="28">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="chosen-rtl">Other 1</span></td>
                                            <td></td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtother1_le_bud" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="49"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtother1_le_act" runat="server" CssClass="dollor" onkeyup="cal_total();" TabIndex="73"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlLocalOther1" runat="server" SkinID="ddlmedium" TabIndex="74">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="chosen-rtl">
                                                <label>
                                                Other 2</label></td>
                                            <td>
                                                </td>
                                            <td>
                                                <asp:TextBox ID="txtother2_de_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="15"></asp:TextBox></td>
                                            <td><span class="Dollar" id="Span14"
                                                style="display: inline-block;">
                                                <asp:TextBox ID="txtother2_de_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="29"></asp:TextBox>
                                            </span></td>
                                            <td align="left" class="lineright">
                                                <asp:DropDownList ID="ddlDocumentOther2" runat="server" SkinID="ddlmedium" TabIndex="30">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="chosen-rtl">Other 2</span></td>
                                            <td>
                                                </td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtother2_le_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="50"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtother2_le_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="75"></asp:TextBox></td>
                                            <td align="left"><asp:DropDownList ID="ddlLocalOther2" SkinID="ddlmedium" runat="server" TabIndex="76"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">Other 3</span></td>
                                            <td>
                                                </td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtother3_le_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="51"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtother3_le_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="77"></asp:TextBox></td>
                                            <td align="left"><asp:DropDownList ID="ddlLocalOther3" SkinID="ddlmedium" runat="server" TabIndex="78"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">Other 4</span></td>
                                            <td>
                                                </td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtother4_le_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="52"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtother4_le_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="79"></asp:TextBox></td>
                                            <td align="left"><asp:DropDownList ID="ddlLocalOther4" SkinID="ddlmedium" runat="server" TabIndex="80"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="lineright">&nbsp;</td>
                                            <td class="chosen-rtl">Other 5</span></td>
                                            <td>
                                                </td>
                                            <td>
                                                <asp:TextBox ID="txtother5_le_bud" CssClass="dollor" onkeyup="cal_total();"
                                                    runat="server" TabIndex="53"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtother5_le_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="81"></asp:TextBox></td>
                                            <td align="left"><asp:DropDownList ID="ddlLocalOther5" SkinID="ddlmedium" runat="server" TabIndex="82"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="chosen-rtl">Local Fixed</span></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtlocalfixed_bud" CssClass="dollor" onkeyup="copytoact(this);cal_total();"
                                                    runat="server" TabIndex="54"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtlocalfixed_act" CssClass="dollor" onkeyup="cal_total();" runat="server"
                                                    TabIndex="83"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; border-top: 1px solid #ccc;" colspan="4">&nbsp;</td>
                                            <td style="font-weight: bold; border-top: 1px solid #ccc;">&nbsp;</td>
                                            <td style="font-weight: bold; border-top: 1px solid #ccc;" colspan="4" align="center">&nbsp;</td>
                                            <td align="center" style="font-weight: bold; border-top: 1px solid #ccc;">&nbsp;</td>
                                        </tr>
                                        <tr class="linetop">
                                            <td align="right" ><span id="MainContent_lblTotal">
                                                <label style="font-weight :bold" >Sub Total Variable Expenses</label></span></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label  ID="lblsubtotal_de_bud" CssClass="txt_bold dollor" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblsubtotal_de_act" CssClass="txt_bold dollor" runat="server"></asp:Label></td>
                                            <td>&nbsp;</td>
                                            <td align="center"><span id="Span19">
                                                <label style="font-weight :bold" >Sub Total Local Expenses</label></span></td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblsubtotal_le_bud" CssClass="txt_bold dollor" runat="server" TabIndex="84"></asp:Label></td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblsubtotal_le_act" CssClass="txt_bold dollor" runat="server" TabIndex="85"></asp:Label></td>
                                            <td class="auto-style2">&nbsp;</td>
                                        </tr>
                                        <tr class ="linetop">
                                            <td align="right"><label>Expense</label>
                                            <td>&nbsp;</td>
                                            <td>
                                             <asp:DropDownList ID="drp_expense" runat="server" Width="160px">
                                                </asp:DropDownList></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td align="center"><span id="Span20">
                                                <label style ="font-weight :bold  ">
                                                    Total Engagement Expenses</label></span></td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbltotal_eng_bud" CssClass="txt_bold dollor" runat="server" TabIndex="86"></asp:Label></td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbltotal_eng_act" CssClass="txt_bold dollor" runat="server" TabIndex="87"></asp:Label></td>
                                            <td class="auto-style2">&nbsp;</td>
                                        </tr>
                                         
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:HiddenField ID="hdnnoofshows" runat="server" />
                <asp:HiddenField ID="hdn_engagementid" runat="server" />
                <asp:HiddenField ID="hdn_schedulecount" runat="server" />
                <asp:HiddenField ID="hdn_pricescale_cap" runat="server" />
                <asp:HiddenField ID="hdn_bodropcount" runat="server" />
                <asp:HiddenField ID="hdn_attendance" runat="server" />
                <asp:HiddenField ID="hdn_expenseid" runat="server" />   

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

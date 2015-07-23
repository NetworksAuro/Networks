<%@ Page Title="Break Even Report" MaintainScrollPositionOnPostback="true" Language="C#" EnableTheming="true"
    MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BreakEvenReport.aspx.cs"
    Inherits="NTOS.Reports.BreakEvenReport" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .Numeric1
        {
            font-size: 10px;
        }

        .Percentage
        {
            font-size: 10px;
        }

        .alignleft
        {
            text-align: left;
        }

        .textboxlabel
        {
            background-color: transparent !important;
            border-color: transparent !important;
            /*text-align: right;*/
            width: 70px;border:solid 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <script type="text/javascript" src="../Scripts/autoNumeric 1.9.15.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            autonumeric();
        });
    </script>
    <script type="text/javascript">
        function autonumeric() {
            $('.Percentage').autoNumeric({ aSign: '%', pSign: 's', vMax: '100' });
            $('.Percentage1').autoNumeric({ aSign: '%', pSign: 's', vMax: '100' });
            $('.PercentageBE').autoNumeric({ aSign: '%', pSign: 's', vMax: '999999999999999999999999999999.00', vMin: '-999999999999999999999999999999' });
            $('.smallint').autoNumeric({ aSep: '', vMax: '32767', vMin: '0' });
            $('.Numeric').autoNumeric({ vMax: '999999999999999999999999999999', vMin: '-999999999999999999999999999999', nBracket: '(,)' });
            $('.Numeric1').autoNumeric({ aSign: '$', vMax: '999999999999999999999999999999.99', vMin: '-999999999999999999999999999999.99', nBracket: '(,)' });
            //$('.Numeric1').autoNumeric({ vMax: '999999999999999', vMin: '-999999999999999' });
            $('.Dollar').autoNumeric({ aSign: '$', vMax: '999999999999999999999999999999.99', vMin: '-999999999999999999999999999999.99', nBracket: '(,)' });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            autonumeric();

        }
        function anc() {
            $('#<%= lnkexcel.ClientID %>')[0].click();
            // window.open(url, 'Download');
        }
    </script>

    <script type="text/javascript">

        function cancel() {
            window.parent.document.getElementById('btnCancel').click();
        }
        function clearall(flag) {
            $('#<%=chkAddlHouseEquipment.ClientID%>')[0].checked = flag;
            $('#<%=chkAdvertising.ClientID%>')[0].checked = flag;
            $('#<%=chkCatering.ClientID%>')[0].checked = flag;
            $('#<%=chkFireWatch.ClientID%>')[0].checked = flag;
            $('#<%=chkFixedCosts.ClientID%>')[0].checked = flag;
            $('#<%=chkGuarantee.ClientID%>')[0].checked = flag;
            $('#<%=chkInsurance.ClientID%>')[0].checked = flag;
            $('#<%=chkMusicians.ClientID%>')[0].checked = flag;
            $('#<%=chkOther.ClientID%>')[0].checked = flag;
            $('#<%=chkPresenterProfit.ClientID%>')[0].checked = flag;
            $('#<%=chkRent.ClientID%>')[0].checked = flag;
            $('#<%=chkRoyalty.ClientID%>')[0].checked = flag;
            $('#<%=chkStagehandsLoadInOut.ClientID%>')[0].checked = flag;
            $('#<%=chkStagehandsRunning.ClientID%>')[0].checked = flag;
            $('#<%=chkTicketPrinting.ClientID%>')[0].checked = flag;
            $('#<%=chkWardrobeHairLoadInOut.ClientID%>')[0].checked = flag;
            $('#<%=chkWardrobeHairRunning.ClientID%>')[0].checked = flag;
            calculate();
            return false;
        }
        function calculate1() {
            calculate();
        }
        function setclass(thisid) {
            // debugger;
            var amt = thisid.value;
            if (parseFloat(amt) <= 100) {
                $('#' + thisid.id).autoNumeric('init', { aSign: '%', vMax: '100.00', pSign: 's' });
                $('#' + thisid.id).autoNumeric('set', { aSign: '%', vMax: '100.00', pSign: 's' });
            }
            else {
                thisid.value = "";
                $('#' + thisid.id).autoNumeric('init', { aSign: '%', vMax: '100.00', pSign: 's' })
            }

        }


        function calculate() {
            try {
                autonumeric();
                $('#<%=txtPerformanceCapacityBrevn.ClientID%>').autoNumeric('set', 100);
                var txtNoofshowsperweek = $('#<%=txtNoofshowsperweek.ClientID%>').autoNumeric('get');
                if (isNaN(txtNoofshowsperweek)) txtNoofshowsperweek = 0
                var txtSeatspershow = $('#<%=txtSeatspershow.ClientID%>').autoNumeric('get');
                if (isNaN(txtSeatspershow)) txtSeatspershow = 0
                var Housecapacity = txtNoofshowsperweek * txtSeatspershow;
                if (isNaN(Housecapacity)) Housecapacity = 0
                var txtNoofweeks = $('#<%=txtNoofweeks.ClientID%>').autoNumeric('get');
                if (isNaN(txtNoofweeks)) txtNoofweeks = 0

                $('#<%=lblHousecapacity.ClientID%>').autoNumeric('set', Housecapacity);
                $('#<%=lblHousecapacity1.ClientID%>').autoNumeric('set', Housecapacity);
                $('#<%=lblHousecapacity2.ClientID%>').autoNumeric('set', Housecapacity);
                $('#<%=lblHousecapacity3.ClientID%>').autoNumeric('set', Housecapacity);
                $('#<%=lblHousecapacity4.ClientID%>').autoNumeric('set', Housecapacity);
                $('#<%=lblHousecapacity5.ClientID%>').autoNumeric('set', Housecapacity);
                $('#<%=lblHousecapacity6.ClientID%>').autoNumeric('set', Housecapacity);
                $('#<%=lblHousecapacity7.ClientID%>').autoNumeric('set', Housecapacity);
                $('#<%=lblHousecapacity8.ClientID%>').autoNumeric('set', Housecapacity);


                //$('.Percentage').autoNumeric('init');
                var txtPerformanceCapacity1 = parseFloat($('#<%=txtPerformanceCapacity1.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacity1)) txtPerformanceCapacity1 = 0
                var txtPerformanceCapacity2 = parseFloat($('#<%=txtPerformanceCapacity2.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacity2)) txtPerformanceCapacity2 = 0
                var txtPerformanceCapacity3 = parseFloat($('#<%=txtPerformanceCapacity3.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacity3)) txtPerformanceCapacity3 = 0
                var txtPerformanceCapacity4 = parseFloat($('#<%=txtPerformanceCapacity4.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacity4)) txtPerformanceCapacity4 = 0
                var txtPerformanceCapacity5 = parseFloat($('#<%=txtPerformanceCapacity5.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacity5)) txtPerformanceCapacity5 = 0
                var txtPerformanceCapacity6 = parseFloat($('#<%=txtPerformanceCapacity6.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacity6)) txtPerformanceCapacity6 = 0
                var txtPerformanceCapacity7 = parseFloat($('#<%=txtPerformanceCapacity7.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacity7)) txtPerformanceCapacity7 = 0
                var txtPerformanceCapacity8 = parseFloat($('#<%=txtPerformanceCapacity8.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacity8)) txtPerformanceCapacity8 = 0

                var txtPerformanceCapacityBrevn = parseFloat($('#<%=txtPerformanceCapacityBrevn.ClientID%>').autoNumeric('get'));
                if (isNaN(txtPerformanceCapacityBrevn)) txtPerformanceCapacityBrevn = 0


                var ticketsoldweek1value = ((Housecapacity * txtPerformanceCapacity1) / 100).toFixed(0)
                if (isNaN(ticketsoldweek1value)) ticketsoldweek1value = 0
                var ticketsoldweek2value = ((Housecapacity * txtPerformanceCapacity2) / 100).toFixed(0)
                if (isNaN(ticketsoldweek2value)) ticketsoldweek2value = 0
                var ticketsoldweek3value = ((Housecapacity * txtPerformanceCapacity3) / 100).toFixed(0)
                if (isNaN(ticketsoldweek3value)) ticketsoldweek3value = 0
                var ticketsoldweek4value = ((Housecapacity * txtPerformanceCapacity4) / 100).toFixed(0)
                if (isNaN(ticketsoldweek4value)) ticketsoldweek4value = 0
                var ticketsoldRun1value = ((Housecapacity * txtPerformanceCapacity5) / 100).toFixed(0)
                if (isNaN(ticketsoldRun1value)) ticketsoldRun1value = 0
                var ticketsoldRun2value = ((Housecapacity * txtPerformanceCapacity6) / 100).toFixed(0)
                if (isNaN(ticketsoldRun2value)) ticketsoldRun2value = 0
                var ticketsoldRun3value = ((Housecapacity * txtPerformanceCapacity7) / 100).toFixed(0)
                if (isNaN(ticketsoldRun3value)) ticketsoldRun3value = 0
                var ticketsoldRun4value = ((Housecapacity * txtPerformanceCapacity8) / 100).toFixed(0)
                if (isNaN(ticketsoldRun4value)) ticketsoldRun4value = 0

                $('#<%=lblTicketsoldweek1.ClientID%>').autoNumeric('set', ticketsoldweek1value);
                $('#<%=lblTicketsoldweek2.ClientID%>').autoNumeric('set', ticketsoldweek2value);
                $('#<%=lblTicketsoldweek3.ClientID%>').autoNumeric('set', ticketsoldweek3value);
                $('#<%=lblTicketsoldweek4.ClientID%>').autoNumeric('set', ticketsoldweek4value);
                $('#<%=lblTicketsoldRun1.ClientID%>').autoNumeric('set', ticketsoldRun1value);
                $('#<%=lblTicketsoldRun2.ClientID%>').autoNumeric('set', ticketsoldRun2value);
                $('#<%=lblTicketsoldRun3.ClientID%>').autoNumeric('set', ticketsoldRun3value);
                $('#<%=lblTicketsoldRun4.ClientID%>').autoNumeric('set', ticketsoldRun4value);


                var breakevenperc = parseFloat($('#<%=txtPerformanceCapacityBrevn.ClientID%>').autoNumeric('get'));
                if (isNaN(breakevenperc)) breakevenperc = 0.00
                //var breakevenperc = (1 * perc).toFixed(2);
                for (var i = 0; i < 100 ; i++) {

                    var Ticketsoldbreakeven = ((Housecapacity * breakevenperc) / 100).toFixed(3);
                    if (isNaN(Ticketsoldbreakeven)) Ticketsoldbreakeven = 0.00

                    $('#<%=lblTicketsoldBreakeven.ClientID%>').autoNumeric('set', Ticketsoldbreakeven);


                    var lblBoxofcgrosssale = $('#<%=lblBoxofcgrosssale.ClientID%>').autoNumeric('get');
                    if (isNaN(lblBoxofcgrosssale)) lblBoxofcgrosssale = 0.00

                    var Boxofcgrosssaleweekvalue = ((lblBoxofcgrosssale * txtPerformanceCapacity1) / 100).toFixed(0)
                    if (isNaN(Boxofcgrosssaleweek1value)) Boxofcgrosssaleweek1value = 0

                    var Boxofcgrosssaleweek1value = ((lblBoxofcgrosssale * txtPerformanceCapacity2) / 100).toFixed(0)
                    if (isNaN(Boxofcgrosssaleweek1value)) Boxofcgrosssaleweek1value = 0
                    var Boxofcgrosssaleweek2value = ((lblBoxofcgrosssale * txtPerformanceCapacity3) / 100).toFixed(0)
                    if (isNaN(Boxofcgrosssaleweek2value)) Boxofcgrosssaleweek2value = 0
                    var Boxofcgrosssaleweek3value = ((lblBoxofcgrosssale * txtPerformanceCapacity4) / 100).toFixed(0)
                    if (isNaN(Boxofcgrosssaleweek3value)) Boxofcgrosssaleweek3value = 0

                    var BoxofcgrosssaleRun4value = (Boxofcgrosssaleweekvalue * txtNoofweeks).toFixed(0)
                    if (isNaN(BoxofcgrosssaleRun4value)) BoxofcgrosssaleRun4value = 0
                    var BoxofcgrosssaleRun5value = (Boxofcgrosssaleweek1value * txtNoofweeks).toFixed(0)
                    if (isNaN(BoxofcgrosssaleRun5value)) BoxofcgrosssaleRun5value = 0
                    var BoxofcgrosssaleRun6value = (Boxofcgrosssaleweek2value * txtNoofweeks).toFixed(0)
                    if (isNaN(BoxofcgrosssaleRun6value)) BoxofcgrosssaleRun6value = 0
                    var BoxofcgrosssaleRun7value = (Boxofcgrosssaleweek3value * txtNoofweeks).toFixed(0)
                    if (isNaN(BoxofcgrosssaleRun7value)) BoxofcgrosssaleRun7value = 0
                    var boxofcegrossaleBreakeven = ((Boxofcgrosssaleweekvalue * breakevenperc) / 100).toFixed(3)
                    if (isNaN(boxofcegrossaleBreakeven)) boxofcegrossaleBreakeven = 0

                    $('#<%=lblBoxofcgrosssale1.ClientID%>').autoNumeric('set', Boxofcgrosssaleweek1value);
                    $('#<%=lblBoxofcgrosssale2.ClientID%>').autoNumeric('set', Boxofcgrosssaleweek2value);
                    $('#<%=lblBoxofcgrosssale3.ClientID%>').autoNumeric('set', Boxofcgrosssaleweek3value);
                    $('#<%=lblBoxofcgrosssale4.ClientID%>').autoNumeric('set', BoxofcgrosssaleRun4value);
                    $('#<%=lblBoxofcgrosssale5.ClientID%>').autoNumeric('set', BoxofcgrosssaleRun5value);
                    $('#<%=lblBoxofcgrosssale6.ClientID%>').autoNumeric('set', BoxofcgrosssaleRun6value);
                    $('#<%=lblBoxofcgrosssale7.ClientID%>').autoNumeric('set', BoxofcgrosssaleRun7value);
                    $('#<%=lblBoxofcgrosssalebreakeven.ClientID%>').autoNumeric('set', boxofcegrossaleBreakeven);


                    var lessdiscount = parseFloat($('#<%=txtLessDiscounts.ClientID%>').autoNumeric('get'));
                    if (isNaN(lessdiscount)) lessdiscount = 0

                    var lblLessdiscountWeek1 = ((-lblBoxofcgrosssale * lessdiscount) / 100).toFixed(0);
                    var lblLessdiscountWeek2 = ((-Boxofcgrosssaleweek1value * lessdiscount) / 100).toFixed(0);
                    var lblLessdiscountWeek3 = ((-Boxofcgrosssaleweek2value * lessdiscount) / 100).toFixed(0);
                    var lblLessdiscountWeek4 = ((-Boxofcgrosssaleweek3value * lessdiscount) / 100).toFixed(0);
                    var lblLessdiscountWeek5 = ((-BoxofcgrosssaleRun4value * lessdiscount) / 100).toFixed(0);
                    var lblLessdiscountWeek6 = ((-BoxofcgrosssaleRun5value * lessdiscount) / 100).toFixed(0);
                    var lblLessdiscountWeek7 = ((-BoxofcgrosssaleRun6value * lessdiscount) / 100).toFixed(0);
                    var lblLessdiscountWeek8 = ((-BoxofcgrosssaleRun7value * lessdiscount) / 100).toFixed(0);
                    var lblLessdiscountBreakeven = ((-boxofcegrossaleBreakeven * lessdiscount) / 100).toFixed(3);

                    $('#<%=lblLessdiscountWeek1.ClientID%>').autoNumeric('set', lblLessdiscountWeek1);
                    $('#<%=lblLessdiscountWeek2.ClientID%>').autoNumeric('set', lblLessdiscountWeek2);
                    $('#<%=lblLessdiscountWeek3.ClientID%>').autoNumeric('set', lblLessdiscountWeek3);
                    $('#<%=lblLessdiscountWeek4.ClientID%>').autoNumeric('set', lblLessdiscountWeek4);
                    $('#<%=lblLessdiscountWeek5.ClientID%>').autoNumeric('set', lblLessdiscountWeek5);
                    $('#<%=lblLessdiscountWeek6.ClientID%>').autoNumeric('set', lblLessdiscountWeek6);
                    $('#<%=lblLessdiscountWeek7.ClientID%>').autoNumeric('set', lblLessdiscountWeek7);
                    $('#<%=lblLessdiscountWeek8.ClientID%>').autoNumeric('set', lblLessdiscountWeek8);
                    $('#<%=lblLessdiscountBreakeven.ClientID%>').autoNumeric('set', lblLessdiscountBreakeven);


                    var lblAdjustedgrossWeekly1 = parseFloat(lblBoxofcgrosssale) + parseFloat(lblLessdiscountWeek1)
                    var lblAdjustedgrossWeekly2 = parseFloat(Boxofcgrosssaleweek1value) + parseFloat(lblLessdiscountWeek2)
                    var lblAdjustedgrossWeekly3 = parseFloat(Boxofcgrosssaleweek2value) + parseFloat(lblLessdiscountWeek3)

                    var lblAdjustedgrossWeekly4 = parseFloat(Boxofcgrosssaleweek3value) + parseFloat(lblLessdiscountWeek4)
                    var lblAdjustedgrossWeekly5 = parseFloat(BoxofcgrosssaleRun4value) + parseFloat(lblLessdiscountWeek5)
                    var lblAdjustedgrossWeekly6 = parseFloat(BoxofcgrosssaleRun5value) + parseFloat(lblLessdiscountWeek6)
                    var lblAdjustedgrossWeekly7 = parseFloat(BoxofcgrosssaleRun6value) + parseFloat(lblLessdiscountWeek7)
                    var lblAdjustedgrossWeekly8 = parseFloat(BoxofcgrosssaleRun7value) + parseFloat(lblLessdiscountWeek8)

                    var lblAdjustedgrossBreakeven = (parseFloat(boxofcegrossaleBreakeven) + parseFloat(lblLessdiscountBreakeven))

                    $('#<%=lblAdjustedgrossWeekly1.ClientID%>').autoNumeric('set', lblAdjustedgrossWeekly1);
                    $('#<%=lblAdjustedgrossWeekly2.ClientID%>').autoNumeric('set', lblAdjustedgrossWeekly2);
                    $('#<%=lblAdjustedgrossWeekly3.ClientID%>').autoNumeric('set', lblAdjustedgrossWeekly3);
                    $('#<%=lblAdjustedgrossWeekly4.ClientID%>').autoNumeric('set', lblAdjustedgrossWeekly4);
                    $('#<%=lblAdjustedgrossWeekly5.ClientID%>').autoNumeric('set', lblAdjustedgrossWeekly5);
                    $('#<%=lblAdjustedgrossWeekly6.ClientID%>').autoNumeric('set', lblAdjustedgrossWeekly6);
                    $('#<%=lblAdjustedgrossWeekly7.ClientID%>').autoNumeric('set', lblAdjustedgrossWeekly7);
                    $('#<%=lblAdjustedgrossWeekly8.ClientID%>').autoNumeric('set', lblAdjustedgrossWeekly8);
                    $('#<%=lblAdjustedgrossBreakeven.ClientID%>').autoNumeric('set', lblAdjustedgrossBreakeven);

                    var txtTax = parseFloat($('#<%=txtTax.ClientID%>').autoNumeric('get'));
                    if (isNaN(txtTax)) txtTax = 0
                    var lblTax1 = ((parseFloat(-lblAdjustedgrossWeekly1) * parseFloat(txtTax)) / 100).toFixed(0);
                    var lblTax2 = ((parseFloat(-lblAdjustedgrossWeekly2) * parseFloat(txtTax)) / 100).toFixed(0);
                    var lblTax3 = ((parseFloat(-lblAdjustedgrossWeekly3) * parseFloat(txtTax)) / 100).toFixed(0);
                    var lblTax4 = ((parseFloat(-lblAdjustedgrossWeekly4) * parseFloat(txtTax)) / 100).toFixed(0);
                    var lblTax5 = ((parseFloat(-lblAdjustedgrossWeekly5) * parseFloat(txtTax)) / 100).toFixed(0);
                    var lblTax6 = ((parseFloat(-lblAdjustedgrossWeekly6) * parseFloat(txtTax)) / 100).toFixed(0);
                    var lblTax7 = ((parseFloat(-lblAdjustedgrossWeekly7) * parseFloat(txtTax)) / 100).toFixed(0);
                    var lblTax8 = ((parseFloat(-lblAdjustedgrossWeekly8) * parseFloat(txtTax)) / 100).toFixed(0);
                    var lblTaxBeakeven = ((parseFloat(-lblAdjustedgrossBreakeven) * parseFloat(txtTax)) / 100).toFixed(3);

                    $('#<%=lblTax1.ClientID%>').autoNumeric('set', lblTax1);
                    $('#<%=lblTax2.ClientID%>').autoNumeric('set', lblTax2);
                    $('#<%=lblTax3.ClientID%>').autoNumeric('set', lblTax3);
                    $('#<%=lblTax4.ClientID%>').autoNumeric('set', lblTax4);
                    $('#<%=lblTax5.ClientID%>').autoNumeric('set', lblTax5);
                    $('#<%=lblTax6.ClientID%>').autoNumeric('set', lblTax6);
                    $('#<%=lblTax7.ClientID%>').autoNumeric('set', lblTax7);
                    $('#<%=lblTax8.ClientID%>').autoNumeric('set', lblTax8);
                    $('#<%=lblTaxBeakeven.ClientID%>').autoNumeric('set', lblTaxBeakeven);

                    var txtRestoration = $('#<%=txtRestoration.ClientID%>').autoNumeric('get');
                    if (isNaN(txtRestoration)) txtRestoration = 0;
                    var lblRestoration1 = -ticketsoldweek1value * txtRestoration;
                    var lblRestoration2 = -ticketsoldweek2value * txtRestoration;
                    var lblRestoration3 = -ticketsoldweek3value * txtRestoration;
                    var lblRestoration4 = -ticketsoldweek4value * txtRestoration;

                    var lblRestoration5 = lblRestoration1 * txtNoofweeks;
                    var lblRestoration6 = lblRestoration2 * txtNoofweeks;
                    var lblRestoration7 = lblRestoration3 * txtNoofweeks;
                    var lblRestoration8 = lblRestoration4 * txtNoofweeks;
                    var lblRestorationBreakeven = (-Ticketsoldbreakeven * txtRestoration).toFixed(3);

                    $('#<%=lblRestoration1.ClientID%>').autoNumeric('set', lblRestoration1);
                    $('#<%=lblRestoration2.ClientID%>').autoNumeric('set', lblRestoration2);
                    $('#<%=lblRestoration3.ClientID%>').autoNumeric('set', lblRestoration3);
                    $('#<%=lblRestoration4.ClientID%>').autoNumeric('set', lblRestoration4);
                    $('#<%=lblRestoration5.ClientID%>').autoNumeric('set', lblRestoration5);
                    $('#<%=lblRestoration6.ClientID%>').autoNumeric('set', lblRestoration6);
                    $('#<%=lblRestoration7.ClientID%>').autoNumeric('set', lblRestoration7);
                    $('#<%=lblRestoration8.ClientID%>').autoNumeric('set', lblRestoration8);
                    $('#<%=lblRestorationBreakeven.ClientID%>').autoNumeric('set', lblRestorationBreakeven);

                    var txtSubloadin = $('#<%=txtSubloadin.ClientID%>').autoNumeric('get');
                    if (isNaN(txtSubloadin)) txtSubloadin = 0
                    subloadinchang();

                    var txtSubscriptioncharge = parseFloat($('#<%=txtSubscriptioncharge.ClientID%>').autoNumeric('get'));
                    if (isNaN(txtSubscriptioncharge)) txtSubscriptioncharge = 0

                    var lblSubscriptioncharge1 = ((-txtSubloadin * txtSubscriptioncharge) / 100).toFixed(0);

                    $('#<%=lblSubscriptioncharge1.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)
                    $('#<%=lblSubscriptioncharge2.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)
                    $('#<%=lblSubscriptioncharge3.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)
                    $('#<%=lblSubscriptioncharge4.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)
                    $('#<%=lblSubscriptioncharge5.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)
                    $('#<%=lblSubscriptioncharge6.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)
                    $('#<%=lblSubscriptioncharge7.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)
                    $('#<%=lblSubscriptioncharge8.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)
                    $('#<%=lblSubscriptionchargeBreakeven.ClientID%>').autoNumeric('set', lblSubscriptioncharge1)


                    var txtCCothercommissions = parseFloat($('#<%=txtCCothercommissions.ClientID%>').autoNumeric('get'));
                    if (isNaN(txtCCothercommissions)) txtCCothercommissions = 0

                    lblCCothercommssns1 = ((-(parseFloat(lblAdjustedgrossWeekly1) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(0);
                    lblCCothercommssns2 = ((-(parseFloat(lblAdjustedgrossWeekly2) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(0);
                    lblCCothercommssns3 = ((-(parseFloat(lblAdjustedgrossWeekly3) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(0);
                    lblCCothercommssns4 = ((-(parseFloat(lblAdjustedgrossWeekly4) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(0);
                    lblCCothercommssns5 = ((-(parseFloat(lblAdjustedgrossWeekly5) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(0);
                    lblCCothercommssns6 = ((-(parseFloat(lblAdjustedgrossWeekly6) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(0);
                    lblCCothercommssns7 = ((-(parseFloat(lblAdjustedgrossWeekly7) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(0);
                    lblCCothercommssns8 = ((-(parseFloat(lblAdjustedgrossWeekly8) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(0);
                    lblCCothercommssnsBreakeven = (-((parseFloat(lblAdjustedgrossBreakeven) - parseFloat(txtSubloadin)) * txtCCothercommissions) / 100).toFixed(3);

                    $('#<%=lblCCothercommssns1.ClientID%>').autoNumeric('set', lblCCothercommssns1)
                    $('#<%=lblCCothercommssns2.ClientID%>').autoNumeric('set', lblCCothercommssns2)
                    $('#<%=lblCCothercommssns3.ClientID%>').autoNumeric('set', lblCCothercommssns3)
                    $('#<%=lblCCothercommssns4.ClientID%>').autoNumeric('set', lblCCothercommssns4)
                    $('#<%=lblCCothercommssns5.ClientID%>').autoNumeric('set', lblCCothercommssns5)
                    $('#<%=lblCCothercommssns6.ClientID%>').autoNumeric('set', lblCCothercommssns6)
                    $('#<%=lblCCothercommssns7.ClientID%>').autoNumeric('set', lblCCothercommssns7)
                    $('#<%=lblCCothercommssns8.ClientID%>').autoNumeric('set', lblCCothercommssns8)
                    $('#<%=lblCCothercommssnsBreakeven.ClientID%>').autoNumeric('set', lblCCothercommssnsBreakeven)

                    var lblNetAR1 = parseFloat((lblAdjustedgrossWeekly1) + (parseFloat(lblTax1) + parseFloat(lblRestoration1) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssns1)));
                    var lblNetAR2 = parseFloat(lblAdjustedgrossWeekly2) + (parseFloat(lblTax2) + (parseFloat(lblRestoration2) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssns2)));
                    var lblNetAR3 = parseFloat(lblAdjustedgrossWeekly3) + (parseFloat(lblTax3) + (parseFloat(lblRestoration3) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssns3)));
                    var lblNetAR4 = parseFloat(lblAdjustedgrossWeekly4) + (parseFloat(lblTax4) + (parseFloat(lblRestoration4) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssns4)));
                    var lblNetAR5 = parseFloat(lblAdjustedgrossWeekly5) + (parseFloat(lblTax5) + (parseFloat(lblRestoration5) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssns5)));
                    var lblNetAR6 = parseFloat(lblAdjustedgrossWeekly6) + (parseFloat(lblTax6) + (parseFloat(lblRestoration6) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssns6)));
                    var lblNetAR7 = parseFloat(lblAdjustedgrossWeekly7) + (parseFloat(lblTax7) + (parseFloat(lblRestoration7) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssns7)));
                    var lblNetAR8 = parseFloat(lblAdjustedgrossWeekly8) + (parseFloat(lblTax8) + (parseFloat(lblRestoration8) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssns8)));
                    var lblNetARBEvn = parseFloat(lblAdjustedgrossBreakeven) + ((parseFloat(lblTaxBeakeven) + parseFloat(lblRestorationBreakeven) + parseFloat(lblSubscriptioncharge1) + parseFloat(lblCCothercommssnsBreakeven)));

                    $('#<%=lblNetAR1.ClientID%>').autoNumeric('set', lblNetAR1);
                    $('#<%=lblNetAR2.ClientID%>').autoNumeric('set', lblNetAR2);
                    $('#<%=lblNetAR3.ClientID%>').autoNumeric('set', lblNetAR3);
                    $('#<%=lblNetAR4.ClientID%>').autoNumeric('set', lblNetAR4);
                    $('#<%=lblNetAR5.ClientID%>').autoNumeric('set', lblNetAR5);
                    $('#<%=lblNetAR6.ClientID%>').autoNumeric('set', lblNetAR6);
                    $('#<%=lblNetAR7.ClientID%>').autoNumeric('set', lblNetAR7);
                    $('#<%=lblNetAR8.ClientID%>').autoNumeric('set', lblNetAR8);
                    $('#<%=lblNetARBEvn.ClientID%>').autoNumeric('set', lblNetARBEvn);

                    var exchangerate = $('#<%=txtExchangerate.ClientID%>').autoNumeric('get');
                    if (isNaN(exchangerate)) exchangerate = 0;

                    var txtGuarantee = $('#<%=txtGuarantee.ClientID%>').autoNumeric('get');
                    if (isNaN(txtGuarantee)) txtGuarantee = 0;

                    //debugger;
                    var lblGuarantee1 = (txtGuarantee * exchangerate).toFixed(0)
                    var lblGuarantee2 = (txtGuarantee * exchangerate).toFixed(0)
                    var lblGuarantee3 = (txtGuarantee * exchangerate).toFixed(0)
                    var lblGuarantee4 = (txtGuarantee * exchangerate).toFixed(0)
                    var lblGuarantee5 = (lblGuarantee1 * txtNoofweeks).toFixed(0)
                    var lblGuarantee6 = (lblGuarantee2 * txtNoofweeks).toFixed(0)
                    var lblGuarantee7 = (lblGuarantee3 * txtNoofweeks).toFixed(0)
                    var lblGuarantee8 = (lblGuarantee4 * txtNoofweeks).toFixed(0)
                    var lblGuaranteeBEvn = parseFloat(1 * txtGuarantee).toFixed(3)

                    $('#<%=lblGuarantee1.ClientID%>').autoNumeric('set', lblGuarantee1);
                    $('#<%=lblGuarantee2.ClientID%>').autoNumeric('set', lblGuarantee2);
                    $('#<%=lblGuarantee3.ClientID%>').autoNumeric('set', lblGuarantee3);
                    $('#<%=lblGuarantee4.ClientID%>').autoNumeric('set', lblGuarantee4);
                    $('#<%=lblGuarantee5.ClientID%>').autoNumeric('set', lblGuarantee5);
                    $('#<%=lblGuarantee6.ClientID%>').autoNumeric('set', lblGuarantee6);
                    $('#<%=lblGuarantee7.ClientID%>').autoNumeric('set', lblGuarantee7);
                    $('#<%=lblGuarantee8.ClientID%>').autoNumeric('set', lblGuarantee8);
                    $('#<%=lblGuaranteeBEvn.ClientID%>').autoNumeric('set', lblGuaranteeBEvn);


                    var txtRoyalty = parseFloat($('#<%=txtRoyalty.ClientID%>').autoNumeric('get'));
                    if (isNaN(txtRoyalty)) txtRoyalty = 0;
                    var lblRoyalty1 = ((lblNetAR1 * txtRoyalty) / 100).toFixed(0)
                    var lblRoyalty2 = ((lblNetAR2 * txtRoyalty) / 100).toFixed(0)
                    var lblRoyalty3 = ((lblNetAR3 * txtRoyalty) / 100).toFixed(0)
                    var lblRoyalty4 = ((lblNetAR4 * txtRoyalty) / 100).toFixed(0)
                    var lblRoyalty5 = ((lblNetAR5 * txtRoyalty) / 100).toFixed(0)
                    var lblRoyalty6 = ((lblNetAR6 * txtRoyalty) / 100).toFixed(0)
                    var lblRoyalty7 = ((lblNetAR7 * txtRoyalty) / 100).toFixed(0)
                    var lblRoyalty8 = ((lblNetAR8 * txtRoyalty) / 100).toFixed(0)
                    var lblRoyaltyBEvn = ((lblNetARBEvn * txtRoyalty) / 100).toFixed(3)

                    $('#<%=lblRoyalty1.ClientID%>').autoNumeric('set', lblRoyalty1);
                    $('#<%=lblRoyalty2.ClientID%>').autoNumeric('set', lblRoyalty2);
                    $('#<%=lblRoyalty3.ClientID%>').autoNumeric('set', lblRoyalty3);
                    $('#<%=lblRoyalty4.ClientID%>').autoNumeric('set', lblRoyalty4);
                    $('#<%=lblRoyalty5.ClientID%>').autoNumeric('set', lblRoyalty5);
                    $('#<%=lblRoyalty6.ClientID%>').autoNumeric('set', lblRoyalty6);
                    $('#<%=lblRoyalty7.ClientID%>').autoNumeric('set', lblRoyalty7);
                    $('#<%=lblRoyalty8.ClientID%>').autoNumeric('set', lblRoyalty8);
                    $('#<%=lblRoyaltyBEvn.ClientID%>').autoNumeric('set', lblRoyaltyBEvn);

                    var txtFixedCosts = $('#<%=txtFixedCosts.ClientID%>').autoNumeric('get');
                    if (isNaN(txtFixedCosts)) txtFixedCosts = 0;

                    var lblFixedcosts1 = (1 * txtFixedCosts).toFixed(0);
                    var lblFixedcosts2 = (1 * txtFixedCosts).toFixed(0);
                    var lblFixedcosts3 = (1 * txtFixedCosts).toFixed(0);
                    var lblFixedcosts4 = (1 * txtFixedCosts).toFixed(0);
                    var lblFixedcosts5 = (txtFixedCosts * txtNoofweeks).toFixed(0);
                    var lblFixedcosts6 = (txtFixedCosts * txtNoofweeks).toFixed(0);
                    var lblFixedcosts7 = (txtFixedCosts * txtNoofweeks).toFixed(0);
                    var lblFixedcosts8 = (txtFixedCosts * txtNoofweeks).toFixed(0);
                    var lblFixedcostsBEvn = (1 * txtFixedCosts).toFixed(3);
                    $('#<%=lblFixedcosts1.ClientID%>').autoNumeric('set', lblFixedcosts1);
                    $('#<%=lblFixedcosts2.ClientID%>').autoNumeric('set', lblFixedcosts2);
                    $('#<%=lblFixedcosts3.ClientID%>').autoNumeric('set', lblFixedcosts3);
                    $('#<%=lblFixedcosts4.ClientID%>').autoNumeric('set', lblFixedcosts4);
                    $('#<%=lblFixedcosts5.ClientID%>').autoNumeric('set', lblFixedcosts5);
                    $('#<%=lblFixedcosts6.ClientID%>').autoNumeric('set', lblFixedcosts6);
                    $('#<%=lblFixedcosts7.ClientID%>').autoNumeric('set', lblFixedcosts7);
                    $('#<%=lblFixedcosts8.ClientID%>').autoNumeric('set', lblFixedcosts8);
                    $('#<%=lblFixedcostsBEvn.ClientID%>').autoNumeric('set', lblFixedcostsBEvn);

                    var txtAdHouseEquipment = $('#<%=txtAdHouseEquipment.ClientID%>').autoNumeric('get');
                    if (isNaN(txtAdHouseEquipment)) txtAdHouseEquipment = 0
                    var lblAdHouseEquipment1 = (1 * txtAdHouseEquipment).toFixed(0)
                    var lblAdHouseEquipment2 = (1 * txtAdHouseEquipment).toFixed(0)
                    var lblAdHouseEquipment3 = (1 * txtAdHouseEquipment).toFixed(0)
                    var lblAdHouseEquipment4 = (1 * txtAdHouseEquipment).toFixed(0)
                    var lblAdHouseEquipment5 = (txtAdHouseEquipment * txtNoofweeks).toFixed(0)
                    var lblAdHouseEquipment6 = (txtAdHouseEquipment * txtNoofweeks).toFixed(0)
                    var lblAdHouseEquipment7 = (txtAdHouseEquipment * txtNoofweeks).toFixed(0)
                    var lblAdHouseEquipment8 = (txtAdHouseEquipment * txtNoofweeks).toFixed(0)
                    var lblAdHouseEquipmentBEvn = (1 * txtAdHouseEquipment).toFixed(3)


                    $('#<%=lblAdHouseEquipment1.ClientID%>').autoNumeric('set', lblAdHouseEquipment1);
                    $('#<%=lblAdHouseEquipment2.ClientID%>').autoNumeric('set', lblAdHouseEquipment2);
                    $('#<%=lblAdHouseEquipment3.ClientID%>').autoNumeric('set', lblAdHouseEquipment3);
                    $('#<%=lblAdHouseEquipment4.ClientID%>').autoNumeric('set', lblAdHouseEquipment4);
                    $('#<%=lblAdHouseEquipment5.ClientID%>').autoNumeric('set', lblAdHouseEquipment5);
                    $('#<%=lblAdHouseEquipment6.ClientID%>').autoNumeric('set', lblAdHouseEquipment6);
                    $('#<%=lblAdHouseEquipment7.ClientID%>').autoNumeric('set', lblAdHouseEquipment7);
                    $('#<%=lblAdHouseEquipment8.ClientID%>').autoNumeric('set', lblAdHouseEquipment8);
                    $('#<%=lblAdHouseEquipmentBEvn.ClientID%>').autoNumeric('set', lblAdHouseEquipmentBEvn);
                    //change1

                    var txtAdvertising = $('#<%=txtAdvertising.ClientID%>').autoNumeric('get');
                    if (isNaN(txtAdHouseEquipment)) txtAdHouseEquipment = 0;

                    var lblAdvertising1 = (1 * txtAdvertising).toFixed(0);
                    var lblAdvertising2 = (1 * txtAdvertising).toFixed(0);
                    var lblAdvertising3 = (1 * txtAdvertising).toFixed(0);
                    var lblAdvertising4 = (1 * txtAdvertising).toFixed(0);
                    var lblAdvertising5 = (txtAdvertising * txtNoofweeks).toFixed(0);
                    var lblAdvertising6 = (txtAdvertising * txtNoofweeks).toFixed(0);
                    var lblAdvertising7 = (txtAdvertising * txtNoofweeks).toFixed(0);
                    var lblAdvertising8 = (txtAdvertising * txtNoofweeks).toFixed(0);
                    var lblAdvertisingBEvn = (1 * txtAdvertising).toFixed(3);

                    $('#<%=lblAdvertising1.ClientID%>').autoNumeric('set', lblAdvertising1);
                    $('#<%=lblAdvertising2.ClientID%>').autoNumeric('set', lblAdvertising2);
                    $('#<%=lblAdvertising3.ClientID%>').autoNumeric('set', lblAdvertising3);
                    $('#<%=lblAdvertising4.ClientID%>').autoNumeric('set', lblAdvertising4);
                    $('#<%=lblAdvertising5.ClientID%>').autoNumeric('set', lblAdvertising5);
                    $('#<%=lblAdvertising6.ClientID%>').autoNumeric('set', lblAdvertising6);
                    $('#<%=lblAdvertising7.ClientID%>').autoNumeric('set', lblAdvertising7);
                    $('#<%=lblAdvertising8.ClientID%>').autoNumeric('set', lblAdvertising8);
                    $('#<%=lblAdvertisingBEvn.ClientID%>').autoNumeric('set', lblAdvertisingBEvn);

                    var txtCatering = $('#<%=txtCatering.ClientID%>').autoNumeric('get');
                    if (isNaN(txtCatering)) txtCatering = 0;
                    var lblCatering1 = (1 * txtCatering).toFixed(0)
                    var lblCatering2 = (1 * txtCatering).toFixed(0)
                    var lblCatering3 = (1 * txtCatering).toFixed(0)
                    var lblCatering4 = (1 * txtCatering).toFixed(0)
                    var lblCatering5 = (txtCatering * txtNoofweeks).toFixed(0);
                    var lblCatering6 = (txtCatering * txtNoofweeks).toFixed(0);
                    var lblCatering7 = (txtCatering * txtNoofweeks).toFixed(0);
                    var lblCatering8 = (txtCatering * txtNoofweeks).toFixed(0);
                    var lblCateringBEvn = (1 * txtCatering).toFixed(3)

                    $('#<%=lblCatering1.ClientID%>').autoNumeric('set', lblCatering1);
                    $('#<%=lblCatering2.ClientID%>').autoNumeric('set', lblCatering2);
                    $('#<%=lblCatering3.ClientID%>').autoNumeric('set', lblCatering3);
                    $('#<%=lblCatering4.ClientID%>').autoNumeric('set', lblCatering4);
                    $('#<%=lblCatering5.ClientID%>').autoNumeric('set', lblCatering5);
                    $('#<%=lblCatering6.ClientID%>').autoNumeric('set', lblCatering6);
                    $('#<%=lblCatering7.ClientID%>').autoNumeric('set', lblCatering7);
                    $('#<%=lblCatering8.ClientID%>').autoNumeric('set', lblCatering8);
                    $('#<%=lblCateringBEvn.ClientID%>').autoNumeric('set', lblCateringBEvn);

                    var txtInsurance = $('#<%=txtInsurance.ClientID%>').autoNumeric('get');
                    if (isNaN(txtInsurance)) txtInsurance = 0;

                    var lblInsurance1 = (txtInsurance * ticketsoldweek1value).toFixed(0)
                    var lblInsurance2 = (txtInsurance * ticketsoldweek2value).toFixed(0)
                    var lblInsurance3 = (txtInsurance * ticketsoldweek3value).toFixed(0)
                    var lblInsurance4 = (txtInsurance * ticketsoldweek4value).toFixed(0)
                    var lblInsurance5 = ((txtInsurance * ticketsoldRun1value) * txtNoofweeks).toFixed(0)
                    var lblInsurance6 = ((txtInsurance * ticketsoldRun2value) * txtNoofweeks).toFixed(0)
                    var lblInsurance7 = ((txtInsurance * ticketsoldRun3value) * txtNoofweeks).toFixed(0)
                    var lblInsurance8 = ((txtInsurance * ticketsoldRun4value) * txtNoofweeks).toFixed(0)
                    var lblInsuranceBEvn = (txtInsurance * Ticketsoldbreakeven).toFixed(3)

                    $('#<%=lblInsurance1.ClientID%>').autoNumeric('set', lblInsurance1);
                    $('#<%=lblInsurance2.ClientID%>').autoNumeric('set', lblInsurance2);
                    $('#<%=lblInsurance3.ClientID%>').autoNumeric('set', lblInsurance3);
                    $('#<%=lblInsurance4.ClientID%>').autoNumeric('set', lblInsurance4);
                    $('#<%=lblInsurance5.ClientID%>').autoNumeric('set', lblInsurance5);
                    $('#<%=lblInsurance6.ClientID%>').autoNumeric('set', lblInsurance6);
                    $('#<%=lblInsurance7.ClientID%>').autoNumeric('set', lblInsurance7);
                    $('#<%=lblInsurance8.ClientID%>').autoNumeric('set', lblInsurance8);
                    $('#<%=lblInsuranceBEvn.ClientID%>').autoNumeric('set', lblInsuranceBEvn);

                    var txtFireWatch = $('#<%=txtFireWatch.ClientID%>').autoNumeric('get');
                    if (isNaN(txtFireWatch)) txtFireWatch = 0;
                    var lblFirewatch1 = (1 * txtFireWatch).toFixed(0)
                    var lblFirewatch2 = (1 * txtFireWatch).toFixed(0)
                    var lblFirewatch3 = (1 * txtFireWatch).toFixed(0)
                    var lblFirewatch4 = (1 * txtFireWatch).toFixed(0)
                    var lblFirewatch5 = (txtFireWatch * txtNoofweeks).toFixed(0)
                    var lblFirewatch6 = (txtFireWatch * txtNoofweeks).toFixed(0)
                    var lblFirewatch7 = (txtFireWatch * txtNoofweeks).toFixed(0)
                    var lblFirewatch8 = (txtFireWatch * txtNoofweeks).toFixed(0)
                    var lblFirewatchBEvn = (1 * txtFireWatch).toFixed(3)

                    $('#<%=lblFirewatch1.ClientID%>').autoNumeric('set', lblFirewatch1);
                    $('#<%=lblFirewatch2.ClientID%>').autoNumeric('set', lblFirewatch2);
                    $('#<%=lblFirewatch3.ClientID%>').autoNumeric('set', lblFirewatch3);
                    $('#<%=lblFirewatch4.ClientID%>').autoNumeric('set', lblFirewatch4);
                    $('#<%=lblFirewatch5.ClientID%>').autoNumeric('set', lblFirewatch5);
                    $('#<%=lblFirewatch6.ClientID%>').autoNumeric('set', lblFirewatch6);
                    $('#<%=lblFirewatch7.ClientID%>').autoNumeric('set', lblFirewatch7);
                    $('#<%=lblFirewatch8.ClientID%>').autoNumeric('set', lblFirewatch8);
                    $('#<%=lblFirewatchBEvn.ClientID%>').autoNumeric('set', lblFirewatchBEvn);
                    //change2

                    var txtMusicians = $('#<%=txtMusicians.ClientID%>').autoNumeric('get');
                    if (isNaN(txtMusicians)) txtMusicians = 0;

                    var lblMusicians1 = (1 * txtMusicians).toFixed(0)
                    var lblMusicians2 = (1 * txtMusicians).toFixed(0)
                    var lblMusicians3 = (1 * txtMusicians).toFixed(0)
                    var lblMusicians4 = (1 * txtMusicians).toFixed(0)
                    var lblMusicians5 = (txtMusicians * txtNoofweeks).toFixed(0)
                    var lblMusicians6 = (txtMusicians * txtNoofweeks).toFixed(0)
                    var lblMusicians7 = (txtMusicians * txtNoofweeks).toFixed(0)
                    var lblMusicians8 = (txtMusicians * txtNoofweeks).toFixed(0)
                    var lblMusiciansBEvn = (1 * txtMusicians).toFixed(3)

                    $('#<%=lblMusicians1.ClientID%>').autoNumeric('set', lblMusicians1);
                    $('#<%=lblMusicians2.ClientID%>').autoNumeric('set', lblMusicians2);
                    $('#<%=lblMusicians3.ClientID%>').autoNumeric('set', lblMusicians3);
                    $('#<%=lblMusicians4.ClientID%>').autoNumeric('set', lblMusicians4);
                    $('#<%=lblMusicians5.ClientID%>').autoNumeric('set', lblMusicians5);
                    $('#<%=lblMusicians6.ClientID%>').autoNumeric('set', lblMusicians6);
                    $('#<%=lblMusicians7.ClientID%>').autoNumeric('set', lblMusicians7);
                    $('#<%=lblMusicians8.ClientID%>').autoNumeric('set', lblMusicians8);
                    $('#<%=lblMusiciansBEvn.ClientID%>').autoNumeric('set', lblMusiciansBEvn);

                    var txtRent = parseFloat($('#<%=txtRent.ClientID%>').autoNumeric('get'));
                    if (isNaN(txtRent)) txtRent = 0;

                    var lblRent1 = ((txtRent * txtPerformanceCapacity1) / 100).toFixed(0);
                    var lblRent2 = ((txtRent * txtPerformanceCapacity2) / 100).toFixed(0);
                    var lblRent3 = ((txtRent * txtPerformanceCapacity3) / 100).toFixed(0);
                    var lblRent4 = ((txtRent * txtPerformanceCapacity4) / 100).toFixed(0);
                    var lblRent5 = ((txtRent * txtPerformanceCapacity5) / 100).toFixed(0);
                    var lblRent6 = ((txtRent * txtPerformanceCapacity6) / 100).toFixed(0);
                    var lblRent7 = ((txtRent * txtPerformanceCapacity7) / 100).toFixed(0);
                    var lblRent8 = ((txtRent * txtPerformanceCapacity8) / 100).toFixed(0);
                    var lblRentBEvn = (1 * txtRent).toFixed(3);
                    $('#<%=lblRent1.ClientID%>').autoNumeric('set', lblRent1);
                    $('#<%=lblRent2.ClientID%>').autoNumeric('set', lblRent2);
                    $('#<%=lblRent3.ClientID%>').autoNumeric('set', lblRent3);
                    $('#<%=lblRent4.ClientID%>').autoNumeric('set', lblRent4);
                    $('#<%=lblRent5.ClientID%>').autoNumeric('set', lblRent5);
                    $('#<%=lblRent6.ClientID%>').autoNumeric('set', lblRent6);
                    $('#<%=lblRent7.ClientID%>').autoNumeric('set', lblRent7);
                    $('#<%=lblRent8.ClientID%>').autoNumeric('set', lblRent8);
                    $('#<%=lblRentBEvn.ClientID%>').autoNumeric('set', lblRentBEvn);

                    var txtStagehandsloadin = $('#<%=txtStagehandsloadin.ClientID%>').autoNumeric('get');
                    if (isNaN(txtStagehandsloadin)) txtStagehandsloadin = 0;
                    var lblStagehandslodin1 = (1 * txtStagehandsloadin).toFixed(0);
                    var lblStagehandslodin2 = (1 * txtStagehandsloadin).toFixed(0);
                    var lblStagehandslodin3 = (1 * txtStagehandsloadin).toFixed(0);
                    var lblStagehandslodin4 = (1 * txtStagehandsloadin).toFixed(0);
                    var lblStagehandslodin5 = (1 * txtStagehandsloadin).toFixed(0);
                    var lblStagehandslodin6 = (1 * txtStagehandsloadin).toFixed(0);
                    var lblStagehandslodin7 = (1 * txtStagehandsloadin).toFixed(0);
                    var lblStagehandslodin8 = (1 * txtStagehandsloadin).toFixed(0);
                    var lblStagehandslodinBEvn = (1 * txtStagehandsloadin).toFixed(3);
                    $('#<%=lblStagehandslodin1.ClientID%>').autoNumeric('set', lblStagehandslodin1);
                    $('#<%=lblStagehandslodin2.ClientID%>').autoNumeric('set', lblStagehandslodin2);
                    $('#<%=lblStagehandslodin3.ClientID%>').autoNumeric('set', lblStagehandslodin3);
                    $('#<%=lblStagehandslodin4.ClientID%>').autoNumeric('set', lblStagehandslodin4);
                    $('#<%=lblStagehandslodin5.ClientID%>').autoNumeric('set', lblStagehandslodin5);
                    $('#<%=lblStagehandslodin6.ClientID%>').autoNumeric('set', lblStagehandslodin6);
                    $('#<%=lblStagehandslodin7.ClientID%>').autoNumeric('set', lblStagehandslodin7);
                    $('#<%=lblStagehandslodin8.ClientID%>').autoNumeric('set', lblStagehandslodin8);
                    $('#<%=lblStagehandslodinBEvn.ClientID%>').autoNumeric('set', lblStagehandslodinBEvn);

                    var txtStagehandsrunning = $('#<%=txtStagehandsrunning.ClientID%>').autoNumeric('get');
                    if (isNaN(txtStagehandsrunning)) txtStagehandsrunning = 0;
                    var lblStagehandsrunning1 = (1 * txtStagehandsrunning).toFixed(0);
                    var lblStagehandsrunning2 = (1 * txtStagehandsrunning).toFixed(0);
                    var lblStagehandsrunning3 = (1 * txtStagehandsrunning).toFixed(0);
                    var lblStagehandsrunning4 = (1 * txtStagehandsrunning).toFixed(0);
                    var lblStagehandsrunning5 = (txtStagehandsrunning * txtNoofweeks).toFixed(0);
                    var lblStagehandsrunning6 = (txtStagehandsrunning * txtNoofweeks).toFixed(0);
                    var lblStagehandsrunning7 = (txtStagehandsrunning * txtNoofweeks).toFixed(0);
                    var lblStagehandsrunning8 = (txtStagehandsrunning * txtNoofweeks).toFixed(0);
                    var lblStagehandsrunningBEvn = (1 * txtStagehandsrunning).toFixed(3);
                    $('#<%=lblStagehandsrunning1.ClientID%>').autoNumeric('set', lblStagehandsrunning1);
                    $('#<%=lblStagehandsrunning2.ClientID%>').autoNumeric('set', lblStagehandsrunning2);
                    $('#<%=lblStagehandsrunning3.ClientID%>').autoNumeric('set', lblStagehandsrunning3);
                    $('#<%=lblStagehandsrunning4.ClientID%>').autoNumeric('set', lblStagehandsrunning4);
                    $('#<%=lblStagehandsrunning5.ClientID%>').autoNumeric('set', lblStagehandsrunning5);
                    $('#<%=lblStagehandsrunning6.ClientID%>').autoNumeric('set', lblStagehandsrunning6);
                    $('#<%=lblStagehandsrunning7.ClientID%>').autoNumeric('set', lblStagehandsrunning7);
                    $('#<%=lblStagehandsrunning8.ClientID%>').autoNumeric('set', lblStagehandsrunning8);
                    $('#<%=lblStagehandsrunningBEvn.ClientID%>').autoNumeric('set', lblStagehandsrunningBEvn);

                    //change3

                    var txtTicketprinting = $('#<%=txtTicketprinting.ClientID%>').autoNumeric('get');
                    if (isNaN(txtTicketprinting)) txtTicketprinting = 0;
                    var lblTicketPrinting1 = (txtTicketprinting * ticketsoldweek1value).toFixed(0);
                    var lblTicketPrinting2 = (txtTicketprinting * ticketsoldweek2value).toFixed(0);
                    var lblTicketPrinting3 = (txtTicketprinting * ticketsoldweek3value).toFixed(0);
                    var lblTicketPrinting4 = (txtTicketprinting * ticketsoldweek4value).toFixed(0);
                    var lblTicketPrinting5 = ((txtTicketprinting * ticketsoldRun1value) * txtNoofweeks).toFixed(0);
                    var lblTicketPrinting6 = ((txtTicketprinting * ticketsoldRun2value) * txtNoofweeks).toFixed(0);
                    var lblTicketPrinting7 = ((txtTicketprinting * ticketsoldRun3value) * txtNoofweeks).toFixed(0);
                    var lblTicketPrinting8 = ((txtTicketprinting * ticketsoldRun4value) * txtNoofweeks).toFixed(0);
                    var lblTicketPrintingBEvn = (txtTicketprinting * Ticketsoldbreakeven).toFixed(3);

                    $('#<%=lblTicketPrinting1.ClientID%>').autoNumeric('set', lblTicketPrinting1);
                    $('#<%=lblTicketPrinting2.ClientID%>').autoNumeric('set', lblTicketPrinting2);
                    $('#<%=lblTicketPrinting3.ClientID%>').autoNumeric('set', lblTicketPrinting3);
                    $('#<%=lblTicketPrinting4.ClientID%>').autoNumeric('set', lblTicketPrinting4);
                    $('#<%=lblTicketPrinting5.ClientID%>').autoNumeric('set', lblTicketPrinting5);
                    $('#<%=lblTicketPrinting6.ClientID%>').autoNumeric('set', lblTicketPrinting6);
                    $('#<%=lblTicketPrinting7.ClientID%>').autoNumeric('set', lblTicketPrinting7);
                    $('#<%=lblTicketPrinting8.ClientID%>').autoNumeric('set', lblTicketPrinting8);
                    $('#<%=lblTicketPrintingBEvn.ClientID%>').autoNumeric('set', lblTicketPrintingBEvn);

                    var txtWardrobehairloadin = $('#<%=txtWardrobehairloadin.ClientID%>').autoNumeric('get');
                    if (isNaN(txtWardrobehairloadin)) txtWardrobehairloadin = 0;
                    var lblWardrobehairloadin1 = (1 * txtWardrobehairloadin).toFixed(0);
                    var lblWardrobehairloadin2 = (1 * txtWardrobehairloadin).toFixed(0);
                    var lblWardrobehairloadin3 = (1 * txtWardrobehairloadin).toFixed(0);
                    var lblWardrobehairloadin4 = (1 * txtWardrobehairloadin).toFixed(0);

                    var lblWardrobehairloadin5 = (txtWardrobehairloadin * txtNoofweeks).toFixed(0);
                    var lblWardrobehairloadin6 = (txtWardrobehairloadin * txtNoofweeks).toFixed(0);
                    var lblWardrobehairloadin7 = (txtWardrobehairloadin * txtNoofweeks).toFixed(0);
                    var lblWardrobehairloadin8 = (txtWardrobehairloadin * txtNoofweeks).toFixed(0);
                    var lblWardrobehairloadinBEvn = (1 * txtWardrobehairloadin).toFixed(3);

                    $('#<%=lblWardrobehairloadin1.ClientID%>').autoNumeric('set', lblWardrobehairloadin1);
                    $('#<%=lblWardrobehairloadin2.ClientID%>').autoNumeric('set', lblWardrobehairloadin2);
                    $('#<%=lblWardrobehairloadin3.ClientID%>').autoNumeric('set', lblWardrobehairloadin3);
                    $('#<%=lblWardrobehairloadin4.ClientID%>').autoNumeric('set', lblWardrobehairloadin4);
                    $('#<%=lblWardrobehairloadin5.ClientID%>').autoNumeric('set', lblWardrobehairloadin5);
                    $('#<%=lblWardrobehairloadin6.ClientID%>').autoNumeric('set', lblWardrobehairloadin6);
                    $('#<%=lblWardrobehairloadin7.ClientID%>').autoNumeric('set', lblWardrobehairloadin7);
                    $('#<%=lblWardrobehairloadin8.ClientID%>').autoNumeric('set', lblWardrobehairloadin8);

                    $('#<%=lblWardrobehairloadinBEvn.ClientID%>').autoNumeric('set', lblWardrobehairloadinBEvn);

                    var txtWardrobehairrunning = $('#<%=txtWardrobehairrunning.ClientID%>').autoNumeric('get');
                    if (isNaN(txtWardrobehairrunning)) txtWardrobehairrunning = 0;
                    var lblWardrobehairrunning1 = (1 * txtWardrobehairrunning).toFixed(0);
                    var lblWardrobehairrunning2 = (1 * txtWardrobehairrunning).toFixed(0);
                    var lblWardrobehairrunning3 = (1 * txtWardrobehairrunning).toFixed(0);
                    var lblWardrobehairrunning4 = (1 * txtWardrobehairrunning).toFixed(0);
                    var lblWardrobehairrunning5 = (txtWardrobehairrunning * txtNoofweeks).toFixed(0);
                    var lblWardrobehairrunning6 = (txtWardrobehairrunning * txtNoofweeks).toFixed(0);
                    var lblWardrobehairrunning7 = (txtWardrobehairrunning * txtNoofweeks).toFixed(0);
                    var lblWardrobehairrunning8 = (txtWardrobehairrunning * txtNoofweeks).toFixed(0);
                    var lblWardrobehairrunningBEvn = (1 * txtWardrobehairrunning).toFixed(3);

                    $('#<%=lblWardrobehairrunning1.ClientID%>').autoNumeric('set', lblWardrobehairrunning1);
                    $('#<%=lblWardrobehairrunning2.ClientID%>').autoNumeric('set', lblWardrobehairrunning2);
                    $('#<%=lblWardrobehairrunning3.ClientID%>').autoNumeric('set', lblWardrobehairrunning3);
                    $('#<%=lblWardrobehairrunning4.ClientID%>').autoNumeric('set', lblWardrobehairrunning4);
                    $('#<%=lblWardrobehairrunning5.ClientID%>').autoNumeric('set', lblWardrobehairrunning5);
                    $('#<%=lblWardrobehairrunning6.ClientID%>').autoNumeric('set', lblWardrobehairrunning6);
                    $('#<%=lblWardrobehairrunning7.ClientID%>').autoNumeric('set', lblWardrobehairrunning7);
                    $('#<%=lblWardrobehairrunning8.ClientID%>').autoNumeric('set', lblWardrobehairrunning8);
                    $('#<%=lblWardrobehairrunningBEvn.ClientID%>').autoNumeric('set', lblWardrobehairrunningBEvn);

                    var txtPF = $('#<%=txtPF.ClientID%>').autoNumeric('get');
                    if (isNaN(txtPF)) txtPF = 0;
                    var lblPF1 = (1 * txtPF).toFixed(0);
                    var lblPF2 = (1 * txtPF).toFixed(0);
                    var lblPF3 = (1 * txtPF).toFixed(0);
                    var lblPF4 = (1 * txtPF).toFixed(0);
                    var lblPF5 = (txtPF * txtNoofweeks).toFixed(0);
                    var lblPF6 = (txtPF * txtNoofweeks).toFixed(0);
                    var lblPF7 = (txtPF * txtNoofweeks).toFixed(0);
                    var lblPF8 = (txtPF * txtNoofweeks).toFixed(0);
                    var lblPFBEvn = (1 * txtPF).toFixed(3);

                    $('#<%=lblPF1.ClientID%>').autoNumeric('set', lblPF1);
                    $('#<%=lblPF2.ClientID%>').autoNumeric('set', lblPF2);
                    $('#<%=lblPF3.ClientID%>').autoNumeric('set', lblPF3);
                    $('#<%=lblPF4.ClientID%>').autoNumeric('set', lblPF4);
                    $('#<%=lblPF5.ClientID%>').autoNumeric('set', lblPF5);
                    $('#<%=lblPF6.ClientID%>').autoNumeric('set', lblPF6);
                    $('#<%=lblPF7.ClientID%>').autoNumeric('set', lblPF7);
                    $('#<%=lblPF8.ClientID%>').autoNumeric('set', lblPF8);
                    $('#<%=lblPFBEvn.ClientID%>').autoNumeric('set', lblPFBEvn);

                    var txtOther = $('#<%=txtOther.ClientID%>').autoNumeric('get');
                    if (isNaN(txtOther)) txtOther = 0;
                    var lblOther1 = (1 * txtOther).toFixed(0);
                    var lblOther2 = (1 * txtOther).toFixed(0);
                    var lblOther3 = (1 * txtOther).toFixed(0);
                    var lblOther4 = (1 * txtOther).toFixed(0);
                    var lblOther5 = (txtOther * txtNoofweeks).toFixed(0);
                    var lblOther6 = (txtOther * txtNoofweeks).toFixed(0);
                    var lblOther7 = (txtOther * txtNoofweeks).toFixed(0);
                    var lblOther8 = (txtOther * txtNoofweeks).toFixed(0);
                    var lblOtherBEvn = (1 * txtOther).toFixed(3);
                    $('#<%=lblOther1.ClientID%>').autoNumeric('set', lblOther1);
                    $('#<%=lblOther2.ClientID%>').autoNumeric('set', lblOther2);
                    $('#<%=lblOther3.ClientID%>').autoNumeric('set', lblOther3);
                    $('#<%=lblOther4.ClientID%>').autoNumeric('set', lblOther4);
                    $('#<%=lblOther5.ClientID%>').autoNumeric('set', lblOther5);
                    $('#<%=lblOther6.ClientID%>').autoNumeric('set', lblOther6);
                    $('#<%=lblOther7.ClientID%>').autoNumeric('set', lblOther7);
                    $('#<%=lblOther8.ClientID%>').autoNumeric('set', lblOther8);
                    $('#<%=lblOtherBEvn.ClientID%>').autoNumeric('set', lblOtherBEvn);


                    lGuaranteeBEvn = 0;

                    if (!$("#<%=chkRoyalty.ClientID%>").is(":checked")) {

                        lblRoyalty1 = 0;
                        lblRoyalty2 = 0;
                        lblRoyalty3 = 0;
                        lblRoyalty4 = 0;
                        lblRoyalty5 = 0;
                        lblRoyalty6 = 0;
                        lblRoyalty7 = 0;
                        lblRoyalty8 = 0;
                        lblRoyaltyBEvn = 0;
                    }
                    if (!$("#<%=chkFixedCosts.ClientID%>").is(":checked")) {
                        lblFixedcosts1 = 0;
                        lblFixedcosts2 = 0;
                        lblFixedcosts3 = 0;
                        lblFixedcosts4 = 0;
                        lblFixedcosts5 = 0;
                        lblFixedcosts6 = 0;
                        lblFixedcosts7 = 0;
                        lblFixedcosts8 = 0;
                        lblFixedcostsBEvn = 0;
                    }
                    if (!$("#<%=chkAddlHouseEquipment.ClientID%>").is(":checked")) {
                        lblAdHouseEquipment1 = 0;
                        lblAdHouseEquipment2 = 0;
                        lblAdHouseEquipment3 = 0;
                        lblAdHouseEquipment4 = 0;
                        lblAdHouseEquipment5 = 0;
                        lblAdHouseEquipment6 = 0;
                        lblAdHouseEquipment7 = 0;
                        lblAdHouseEquipment8 = 0;
                        lblAdHouseEquipmentBEvn = 0;
                    }
                    if (!$("#<%=chkAdvertising.ClientID%>").is(":checked")) {
                        lblAdvertising1 = 0;
                        lblAdvertising2 = 0;
                        lblAdvertising3 = 0;
                        lblAdvertising4 = 0;
                        lblAdvertising5 = 0;
                        lblAdvertising6 = 0;
                        lblAdvertising7 = 0;
                        lblAdvertising8 = 0;
                        lblAdvertisingBEvn = 0;
                    }

                    if (!$("#<%=chkCatering.ClientID%>").is(":checked")) {
                        lblCatering1 = 0;
                        lblCatering2 = 0;
                        lblCatering3 = 0;
                        lblCatering4 = 0;
                        lblCatering5 = 0;
                        lblCatering6 = 0;
                        lblCatering7 = 0;
                        lblCatering8 = 0;
                        lblCateringBEvn = 0;
                    }
                    if (!$("#<%=chkInsurance.ClientID%>").is(":checked")) {
                        lblInsurance1 = 0;
                        lblInsurance2 = 0;
                        lblInsurance3 = 0;
                        lblInsurance4 = 0;
                        lblInsurance5 = 0;
                        lblInsurance6 = 0;
                        lblInsurance7 = 0;
                        lblInsurance8 = 0;
                        lblInsuranceBEvn = 0;
                    }
                    if (!$("#<%=chkFireWatch.ClientID%>").is(":checked")) {
                        lblFirewatch1 = 0;
                        lblFirewatch2 = 0;
                        lblFirewatch3 = 0;
                        lblFirewatch4 = 0;
                        lblFirewatch5 = 0;
                        lblFirewatch6 = 0;
                        lblFirewatch7 = 0;
                        lblFirewatch8 = 0;
                        lblFirewatchBEvn = 0;
                    }
                    if (!$("#<%=chkMusicians.ClientID%>").is(":checked")) {
                        lblMusicians1 = 0;
                        lblMusicians2 = 0;
                        lblMusicians3 = 0;
                        lblMusicians4 = 0;
                        lblMusicians5 = 0;
                        lblMusicians6 = 0;
                        lblMusicians7 = 0;
                        lblMusicians8 = 0;
                        lblMusiciansBEvn = 0;
                    }
                    if (!$("#<%=chkRent.ClientID%>").is(":checked")) {
                        lblRent1 = 0;
                        lblRent2 = 0;
                        lblRent3 = 0;
                        lblRent4 = 0;
                        lblRent5 = 0;
                        lblRent6 = 0;
                        lblRent7 = 0;
                        lblRent8 = 0;
                        lblRentBEvn = 0;
                    }
                    if (!$("#<%=chkStagehandsLoadInOut.ClientID%>").is(":checked")) {
                        lblStagehandslodin1 = 0;
                        lblStagehandslodin2 = 0;
                        lblStagehandslodin3 = 0;
                        lblStagehandslodin4 = 0;
                        lblStagehandslodin5 = 0;
                        lblStagehandslodin6 = 0;
                        lblStagehandslodin7 = 0;
                        lblStagehandslodin8 = 0;
                        lblStagehandslodinBEvn = 0;
                    }
                    if (!$("#<%=chkStagehandsRunning.ClientID%>").is(":checked")) {
                        lblStagehandsrunning1 = 0;
                        lblStagehandsrunning2 = 0;
                        lblStagehandsrunning3 = 0;
                        lblStagehandsrunning4 = 0;
                        lblStagehandsrunning5 = 0;
                        lblStagehandsrunning6 = 0;
                        lblStagehandsrunning7 = 0;
                        lblStagehandsrunning8 = 0;
                        lblStagehandslodinBEvn = 0;
                    }
                    if (!$("#<%=chkTicketPrinting.ClientID%>").is(":checked")) {
                        lblTicketPrinting1 = 0;
                        lblTicketPrinting2 = 0;
                        lblTicketPrinting3 = 0;
                        lblTicketPrinting4 = 0;
                        lblTicketPrinting5 = 0;
                        lblTicketPrinting6 = 0;
                        lblTicketPrinting7 = 0;
                        lblTicketPrinting8 = 0;
                        lblTicketPrintingBEvn = 0;
                    }
                    if (!$("#<%=chkWardrobeHairLoadInOut.ClientID%>").is(":checked")) {
                        lblWardrobehairloadin1 = 0;
                        lblWardrobehairloadin2 = 0;
                        lblWardrobehairloadin3 = 0;
                        lblWardrobehairloadin4 = 0;
                        lblWardrobehairloadin5 = 0;
                        lblWardrobehairloadin6 = 0;
                        lblWardrobehairloadin7 = 0;
                        lblWardrobehairloadin8 = 0;
                        lblWardrobehairloadinBEvn = 0;
                    }
                    if (!$("#<%=chkWardrobeHairRunning.ClientID%>").is(":checked")) {
                        lblWardrobehairrunning1 = 0;
                        lblWardrobehairrunning2 = 0;
                        lblWardrobehairrunning3 = 0;
                        lblWardrobehairrunning4 = 0;
                        lblWardrobehairrunning5 = 0;
                        lblWardrobehairrunning6 = 0;
                        lblWardrobehairrunning7 = 0;
                        lblWardrobehairrunning8 = 0;
                        lblWardrobehairrunningBEvn = 0;
                    }
                    if (!$("#<%=chkPresenterProfit.ClientID%>").is(":checked")) {
                        lblPF1 = 0;
                        lblPF2 = 0;
                        lblPF3 = 0;
                        lblPF4 = 0;
                        lblPF5 = 0;
                        lblPF6 = 0;
                        lblPF7 = 0;
                        lblPF8 = 0;
                        lblPFBEvn = 0;
                    }
                    if (!$("#<%=chkOther.ClientID%>").is(":checked")) {
                        lblOther1 = 0;
                        lblOther2 = 0;
                        lblOther3 = 0;
                        lblOther4 = 0;
                        lblOther5 = 0;
                        lblOther6 = 0;
                        lblOther7 = 0;
                        lblOther8 = 0;
                        lblOtherBEvn = 0;
                    }

                    var lblTotalLExp1 = (parseFloat(lblGuarantee1) + parseFloat(lblRoyalty1) + parseFloat(lblFixedcosts1) + parseFloat(lblAdHouseEquipment1) + parseFloat(lblAdvertising1) + parseFloat(lblCatering1) + parseFloat(lblInsurance1) + parseFloat(lblFirewatch1) + parseFloat(lblMusicians1) + parseFloat(lblRent1) + parseFloat(lblStagehandslodin1) + parseFloat(lblStagehandsrunning1) + parseFloat(lblTicketPrinting1) + parseFloat(lblWardrobehairloadin1) + parseFloat(lblWardrobehairrunning1) + parseFloat(lblPF1) + parseFloat(lblOther1)).toFixed(0)
                    var lblTotalLExp2 = (parseFloat(lblGuarantee2) + parseFloat(lblRoyalty2) + parseFloat(lblFixedcosts2) + parseFloat(lblAdHouseEquipment2) + parseFloat(lblAdvertising2) + parseFloat(lblCatering2) + parseFloat(lblInsurance2) + parseFloat(lblFirewatch2) + parseFloat(lblMusicians2) + parseFloat(lblRent2) + parseFloat(lblStagehandslodin2) + parseFloat(lblStagehandsrunning2) + parseFloat(lblTicketPrinting2) + parseFloat(lblWardrobehairloadin2) + parseFloat(lblWardrobehairrunning2) + parseFloat(lblPF2) + parseFloat(lblOther2)).toFixed(0)
                    var lblTotalLExp3 = (parseFloat(lblGuarantee3) + parseFloat(lblRoyalty3) + parseFloat(lblFixedcosts3) + parseFloat(lblAdHouseEquipment3) + parseFloat(lblAdvertising3) + parseFloat(lblCatering3) + parseFloat(lblInsurance3) + parseFloat(lblFirewatch3) + parseFloat(lblMusicians3) + parseFloat(lblRent3) + parseFloat(lblStagehandslodin3) + parseFloat(lblStagehandsrunning3) + parseFloat(lblTicketPrinting3) + parseFloat(lblWardrobehairloadin3) + parseFloat(lblWardrobehairrunning3) + parseFloat(lblPF3) + parseFloat(lblOther3)).toFixed(0)
                    var lblTotalLExp4 = (parseFloat(lblGuarantee4) + parseFloat(lblRoyalty4) + parseFloat(lblFixedcosts4) + parseFloat(lblAdHouseEquipment4) + parseFloat(lblAdvertising4) + parseFloat(lblCatering4) + parseFloat(lblInsurance4) + parseFloat(lblFirewatch4) + parseFloat(lblMusicians4) + parseFloat(lblRent4) + parseFloat(lblStagehandslodin4) + parseFloat(lblStagehandsrunning4) + parseFloat(lblTicketPrinting4) + parseFloat(lblWardrobehairloadin4) + parseFloat(lblWardrobehairrunning4) + parseFloat(lblPF4) + parseFloat(lblOther4)).toFixed(0)
                    var lblTotalLExp5 = (parseFloat(lblGuarantee5) + parseFloat(lblRoyalty5) + parseFloat(lblFixedcosts5) + parseFloat(lblAdHouseEquipment5) + parseFloat(lblAdvertising5) + parseFloat(lblCatering5) + parseFloat(lblInsurance5) + parseFloat(lblFirewatch5) + parseFloat(lblMusicians5) + parseFloat(lblRent5) + parseFloat(lblStagehandslodin5) + parseFloat(lblStagehandsrunning5) + parseFloat(lblTicketPrinting5) + parseFloat(lblWardrobehairloadin5) + parseFloat(lblWardrobehairrunning5) + parseFloat(lblPF5) + parseFloat(lblOther5)).toFixed(0)
                    var lblTotalLExp6 = (parseFloat(lblGuarantee6) + parseFloat(lblRoyalty6) + parseFloat(lblFixedcosts6) + parseFloat(lblAdHouseEquipment6) + parseFloat(lblAdvertising6) + parseFloat(lblCatering6) + parseFloat(lblInsurance6) + parseFloat(lblFirewatch6) + parseFloat(lblMusicians6) + parseFloat(lblRent6) + parseFloat(lblStagehandslodin6) + parseFloat(lblStagehandsrunning6) + parseFloat(lblTicketPrinting6) + parseFloat(lblWardrobehairloadin6) + parseFloat(lblWardrobehairrunning6) + parseFloat(lblPF6) + parseFloat(lblOther6)).toFixed(0)
                    var lblTotalLExp7 = (parseFloat(lblGuarantee7) + parseFloat(lblRoyalty7) + parseFloat(lblFixedcosts7) + parseFloat(lblAdHouseEquipment7) + parseFloat(lblAdvertising7) + parseFloat(lblCatering7) + parseFloat(lblInsurance7) + parseFloat(lblFirewatch7) + parseFloat(lblMusicians7) + parseFloat(lblRent7) + parseFloat(lblStagehandslodin7) + parseFloat(lblStagehandsrunning7) + parseFloat(lblTicketPrinting7) + parseFloat(lblWardrobehairloadin7) + parseFloat(lblWardrobehairrunning7) + parseFloat(lblPF7) + parseFloat(lblOther7)).toFixed(0)
                    var lblTotalLExp8 = (parseFloat(lblGuarantee8) + parseFloat(lblRoyalty8) + parseFloat(lblFixedcosts8) + parseFloat(lblAdHouseEquipment8) + parseFloat(lblAdvertising8) + parseFloat(lblCatering8) + parseFloat(lblInsurance8) + parseFloat(lblFirewatch8) + parseFloat(lblMusicians8) + parseFloat(lblRent8) + parseFloat(lblStagehandslodin8) + parseFloat(lblStagehandsrunning8) + parseFloat(lblTicketPrinting8) + parseFloat(lblWardrobehairloadin8) + parseFloat(lblWardrobehairrunning8) + parseFloat(lblPF8) + parseFloat(lblOther8)).toFixed(0)
                    var lblTotalLExpBEvn = (parseFloat(lblGuaranteeBEvn) + parseFloat(lblRoyaltyBEvn) + parseFloat(lblFixedcostsBEvn) + parseFloat(lblAdHouseEquipmentBEvn) + parseFloat(lblAdvertisingBEvn) + parseFloat(lblCateringBEvn) + parseFloat(lblInsuranceBEvn) + parseFloat(lblFirewatchBEvn) + parseFloat(lblMusiciansBEvn) + parseFloat(lblRentBEvn) + parseFloat(lblStagehandslodinBEvn) + parseFloat(lblStagehandsrunningBEvn) + parseFloat(lblTicketPrintingBEvn) + parseFloat(lblWardrobehairloadinBEvn) + parseFloat(lblWardrobehairrunningBEvn) + parseFloat(lblPFBEvn) + parseFloat(lblOtherBEvn)).toFixed(3)

                    $('#<%=lblTotalLExp1.ClientID%>').autoNumeric('set', lblTotalLExp1);
                    $('#<%=lblTotalLExp2.ClientID%>').autoNumeric('set', lblTotalLExp2);
                    $('#<%=lblTotalLExp3.ClientID%>').autoNumeric('set', lblTotalLExp3);
                    $('#<%=lblTotalLExp4.ClientID%>').autoNumeric('set', lblTotalLExp4);
                    $('#<%=lblTotalLExp5.ClientID%>').autoNumeric('set', lblTotalLExp5);
                    $('#<%=lblTotalLExp6.ClientID%>').autoNumeric('set', lblTotalLExp6);
                    $('#<%=lblTotalLExp7.ClientID%>').autoNumeric('set', lblTotalLExp7);
                    $('#<%=lblTotalLExp8.ClientID%>').autoNumeric('set', lblTotalLExp8);
                    $('#<%=lblTotalLExpBEvn.ClientID%>').autoNumeric('set', lblTotalLExpBEvn);

                    var lblMoneyRemaining1 = (parseFloat(lblNetAR1) - parseFloat(lblTotalLExp1));
                    var lblMoneyRemaining2 = (parseFloat(lblNetAR2) - parseFloat(lblTotalLExp2));
                    var lblMoneyRemaining3 = (parseFloat(lblNetAR3) - parseFloat(lblTotalLExp3));
                    var lblMoneyRemaining4 = (parseFloat(lblNetAR4) - parseFloat(lblTotalLExp4));

                    var lblMoneyRemaining5 = (lblMoneyRemaining1 * txtNoofweeks);
                    var lblMoneyRemaining6 = (lblMoneyRemaining2 * txtNoofweeks);
                    var lblMoneyRemaining7 = (lblMoneyRemaining3 * txtNoofweeks);
                    var lblMoneyRemaining8 = (lblMoneyRemaining4 * txtNoofweeks);
                    var lblMoneyRemainingBEvn = (parseFloat(lblNetARBEvn) - parseFloat(lblTotalLExpBEvn));

                    $('#<%=lblMoneyRemaining1.ClientID%>').autoNumeric('set', lblMoneyRemaining1);
                    $('#<%=lblMoneyRemaining2.ClientID%>').autoNumeric('set', lblMoneyRemaining2);
                    $('#<%=lblMoneyRemaining3.ClientID%>').autoNumeric('set', lblMoneyRemaining3);
                    $('#<%=lblMoneyRemaining4.ClientID%>').autoNumeric('set', lblMoneyRemaining4);
                    $('#<%=lblMoneyRemaining5.ClientID%>').autoNumeric('set', lblMoneyRemaining5);
                    $('#<%=lblMoneyRemaining6.ClientID%>').autoNumeric('set', lblMoneyRemaining6);
                    $('#<%=lblMoneyRemaining7.ClientID%>').autoNumeric('set', lblMoneyRemaining7);
                    $('#<%=lblMoneyRemaining8.ClientID%>').autoNumeric('set', lblMoneyRemaining8);
                    $('#<%=lblMoneyRemainingBEvn.ClientID%>').autoNumeric('set', lblMoneyRemainingBEvn);

                    var txtNMTP = $('#<%=txtNMTP.ClientID%>').autoNumeric('get');
                    if (isNaN(txtNMTP)) txtNMTP = 0;
                    var lblNMTP1 = (1 * txtNMTP).toFixed(0);
                    var lblNMTP2 = (1 * txtNMTP).toFixed(0);
                    var lblNMTP3 = (1 * txtNMTP).toFixed(0);
                    var lblNMTP4 = (1 * txtNMTP).toFixed(0);
                    var lblNMTP5 = (txtNMTP * txtNoofweeks);
                    var lblNMTP6 = (txtNMTP * txtNoofweeks);
                    var lblNMTP7 = (txtNMTP * txtNoofweeks);
                    var lblNMTP8 = (txtNMTP * txtNoofweeks);
                    var lblNMTPBEvn = (1 * txtNMTP).toFixed(3);

                    $('#<%=lblNMTP1.ClientID%>').autoNumeric('set', lblNMTP1);
                    $('#<%=lblNMTP2.ClientID%>').autoNumeric('set', lblNMTP2);
                    $('#<%=lblNMTP3.ClientID%>').autoNumeric('set', lblNMTP3);
                    $('#<%=lblNMTP4.ClientID%>').autoNumeric('set', lblNMTP4);
                    $('#<%=lblNMTP5.ClientID%>').autoNumeric('set', lblNMTP5);
                    $('#<%=lblNMTP6.ClientID%>').autoNumeric('set', lblNMTP6);
                    $('#<%=lblNMTP7.ClientID%>').autoNumeric('set', lblNMTP7);
                    $('#<%=lblNMTP8.ClientID%>').autoNumeric('set', lblNMTP8);
                    $('#<%=lblNMTPBEvn.ClientID%>').autoNumeric('set', lblNMTPBEvn);
                    //change4

                    var txtNMTPTR = $('#<%=txtNMTPTR.ClientID%>').autoNumeric('get');
                    if (isNaN(txtNMTPTR)) txtNMTP = 0;
                    var lblNMTPTR1 = (1 * txtNMTPTR).toFixed(0);
                    var lblNMTPTR2 = (1 * txtNMTPTR).toFixed(0);
                    var lblNMTPTR3 = (1 * txtNMTPTR).toFixed(0);
                    var lblNMTPTR4 = (1 * txtNMTPTR).toFixed(0);
                    var lblNMTPTR5 = (txtNMTPTR * txtNoofweeks).toFixed(0);
                    var lblNMTPTR6 = (txtNMTPTR * txtNoofweeks).toFixed(0);
                    var lblNMTPTR7 = (txtNMTPTR * txtNoofweeks).toFixed(0);
                    var lblNMTPTR8 = (txtNMTPTR * txtNoofweeks).toFixed(0);
                    var lblNMTPTRBEvn = (1 * txtNMTPTR).toFixed(3);
                    $('#<%=lblNMTPTR1.ClientID%>').autoNumeric('set', lblNMTPTR1);
                    $('#<%=lblNMTPTR2.ClientID%>').autoNumeric('set', lblNMTPTR2);
                    $('#<%=lblNMTPTR3.ClientID%>').autoNumeric('set', lblNMTPTR3);
                    $('#<%=lblNMTPTR4.ClientID%>').autoNumeric('set', lblNMTPTR4);
                    $('#<%=lblNMTPTR5.ClientID%>').autoNumeric('set', lblNMTPTR5);
                    $('#<%=lblNMTPTR6.ClientID%>').autoNumeric('set', lblNMTPTR6);
                    $('#<%=lblNMTPTR7.ClientID%>').autoNumeric('set', lblNMTPTR7);
                    $('#<%=lblNMTPTR8.ClientID%>').autoNumeric('set', lblNMTPTR8);
                    $('#<%=lblNMTPTRBEvn.ClientID%>').autoNumeric('set', lblNMTPTRBEvn);

                    var lblTEP1 = (parseFloat(lblMoneyRemaining1) - parseFloat(lblNMTP1) - parseFloat(lblNMTPTR1)).toFixed(0);
                    var lblTEP2 = (parseFloat(lblMoneyRemaining2) - parseFloat(lblNMTP2) - parseFloat(lblNMTPTR2)).toFixed(0);
                    var lblTEP3 = (parseFloat(lblMoneyRemaining3) - parseFloat(lblNMTP3) - parseFloat(lblNMTPTR3)).toFixed(0);
                    var lblTEP4 = (parseFloat(lblMoneyRemaining4) - parseFloat(lblNMTP4) - parseFloat(lblNMTPTR4)).toFixed(0);
                    var lblTEP5 = (parseFloat(lblMoneyRemaining5) - parseFloat(lblNMTP5) - parseFloat(lblNMTPTR5)).toFixed(0);
                    var lblTEP6 = (parseFloat(lblMoneyRemaining6) - parseFloat(lblNMTP6) - parseFloat(lblNMTPTR6)).toFixed(0);
                    var lblTEP7 = (parseFloat(lblMoneyRemaining7) - parseFloat(lblNMTP7) - parseFloat(lblNMTPTR7)).toFixed(0);
                    var lblTEP8 = (parseFloat(lblMoneyRemaining8) - parseFloat(lblNMTP8) - parseFloat(lblNMTPTR8)).toFixed(0);
                    var lblTEPBEvn = (parseFloat(lblMoneyRemainingBEvn) - parseFloat(lblNMTPBEvn) - parseFloat(lblNMTPTRBEvn)).toFixed(3);
                    $('#<%=lblTEP1   .ClientID%>').autoNumeric('set', lblTEP1);
                    $('#<%=lblTEP2   .ClientID%>').autoNumeric('set', lblTEP2);
                    $('#<%=lblTEP3   .ClientID%>').autoNumeric('set', lblTEP3);
                    $('#<%=lblTEP4   .ClientID%>').autoNumeric('set', lblTEP4);
                    $('#<%=lblTEP5   .ClientID%>').autoNumeric('set', lblTEP5);
                    $('#<%=lblTEP6   .ClientID%>').autoNumeric('set', lblTEP6);
                    $('#<%=lblTEP7   .ClientID%>').autoNumeric('set', lblTEP7);
                    $('#<%=lblTEP8   .ClientID%>').autoNumeric('set', lblTEP8);
                    $('#<%=lblTEPBEvn.ClientID%>').autoNumeric('set', lblTEPBEvn);

                    var txtPShare = parseFloat($('#<%=txtPShare.ClientID%>').autoNumeric('get'));
                    if (isNaN(txtPShare)) txtPShare = 0;
                    var lblPShare1 = parseFloat(lblTEP1) > 0 ? ((lblTEP1 * txtPShare) / 100).toFixed(0) : 0;
                    var lblPShare2 = parseFloat(lblTEP2) > 0 ? ((lblTEP2 * txtPShare) / 100).toFixed(0) : 0;
                    var lblPShare3 = parseFloat(lblTEP3) > 0 ? ((lblTEP3 * txtPShare) / 100).toFixed(0) : 0;
                    var lblPShare4 = parseFloat(lblTEP4) > 0 ? ((lblTEP4 * txtPShare) / 100).toFixed(0) : 0;
                    var lblPShare5 = parseFloat(lblTEP5) > 0 ? ((lblTEP5 * txtPShare) / 100).toFixed(0) : 0;
                    var lblPShare6 = parseFloat(lblTEP6) > 0 ? ((lblTEP6 * txtPShare) / 100).toFixed(0) : 0;
                    var lblPShare7 = parseFloat(lblTEP7) > 0 ? ((lblTEP7 * txtPShare) / 100).toFixed(0) : 0;
                    var lblPShare8 = parseFloat(lblTEP8) > 0 ? ((lblTEP8 * txtPShare) / 100).toFixed(0) : 0;
                    var lblPShareBEvn = parseFloat(lblTEPBEvn) > 0 ? ((lblTEPBEvn * txtPShare) / 100).toFixed(3) : 0;

                    $('#<%=lblPShare1   .ClientID%>').autoNumeric('set', lblPShare1);
                    $('#<%=lblPShare2   .ClientID%>').autoNumeric('set', lblPShare2);
                    $('#<%=lblPShare3   .ClientID%>').autoNumeric('set', lblPShare3);
                    $('#<%=lblPShare4   .ClientID%>').autoNumeric('set', lblPShare4);
                    $('#<%=lblPShare5   .ClientID%>').autoNumeric('set', lblPShare5);
                    $('#<%=lblPShare6   .ClientID%>').autoNumeric('set', lblPShare6);
                    $('#<%=lblPShare7   .ClientID%>').autoNumeric('set', lblPShare7);
                    $('#<%=lblPShare8   .ClientID%>').autoNumeric('set', lblPShare8);
                    $('#<%=lblPShareBEvn   .ClientID%>').autoNumeric('set', lblPShareBEvn);

                    var txtPShareofsplit = parseFloat($('#<%=txtPShareofsplit.ClientID%>').autoNumeric('get'));
                    if (isNaN(txtPShareofsplit)) txtPShareofsplit = 0;
                    var lblPSharesplit1 = parseFloat(lblTEP1) > 0 ? ((lblTEP1 * txtPShareofsplit) / 100).toFixed(0) : 0;
                    var lblPSharesplit2 = parseFloat(lblTEP2) > 0 ? ((lblTEP2 * txtPShareofsplit) / 100).toFixed(0) : 0;
                    var lblPSharesplit3 = parseFloat(lblTEP3) > 0 ? ((lblTEP3 * txtPShareofsplit) / 100).toFixed(0) : 0;
                    var lblPSharesplit4 = parseFloat(lblTEP4) > 0 ? ((lblTEP4 * txtPShareofsplit) / 100).toFixed(0) : 0;
                    var lblPSharesplit5 = parseFloat(lblTEP5) > 0 ? ((lblTEP5 * txtPShareofsplit) / 100).toFixed(0) : 0;
                    var lblPSharesplit6 = parseFloat(lblTEP6) > 0 ? ((lblTEP6 * txtPShareofsplit) / 100).toFixed(0) : 0;
                    var lblPSharesplit7 = parseFloat(lblTEP7) > 0 ? ((lblTEP7 * txtPShareofsplit) / 100).toFixed(0) : 0;
                    var lblPSharesplit8 = parseFloat(lblTEP8) > 0 ? ((lblTEP8 * txtPShareofsplit) / 100).toFixed(0) : 0;
                    var lblPSharesplitBEvn = parseFloat(lblTEPBEvn) > 0 ? ((lblTEPBEvn * txtPShareofsplit) / 100).toFixed(3) : 0;

                    $('#<%=lblPSharesplit1.ClientID%>').autoNumeric('set', lblPSharesplit1);
                    $('#<%=lblPSharesplit2.ClientID%>').autoNumeric('set', lblPSharesplit2);
                    $('#<%=lblPSharesplit3.ClientID%>').autoNumeric('set', lblPSharesplit3);
                    $('#<%=lblPSharesplit4.ClientID%>').autoNumeric('set', lblPSharesplit4);
                    $('#<%=lblPSharesplit5.ClientID%>').autoNumeric('set', lblPSharesplit5);
                    $('#<%=lblPSharesplit6.ClientID%>').autoNumeric('set', lblPSharesplit6);
                    $('#<%=lblPSharesplit7.ClientID%>').autoNumeric('set', lblPSharesplit7);
                    $('#<%=lblPSharesplit8.ClientID%>').autoNumeric('set', lblPSharesplit8);
                    $('#<%=lblPSharesplitBEvn.ClientID%>').autoNumeric('set', lblPSharesplitBEvn);


                    var lblMiddlemonies1 = lblNMTP1
                    var lblMiddlemonies2 = lblNMTP2
                    var lblMiddlemonies3 = lblNMTP3
                    var lblMiddlemonies4 = lblNMTP4
                    var lblMiddlemonies5 = lblNMTP5
                    var lblMiddlemonies6 = lblNMTP6
                    var lblMiddlemonies7 = lblNMTP7
                    var lblMiddlemonies8 = lblNMTP8
                    var lblMiddlemoniesBEvn = lblNMTPBEvn

                    $('#<%=lblMiddlemonies1   .ClientID%>').autoNumeric('set', lblNMTP1);
                    $('#<%=lblMiddlemonies2   .ClientID%>').autoNumeric('set', lblNMTP2);
                    $('#<%=lblMiddlemonies3   .ClientID%>').autoNumeric('set', lblNMTP3);
                    $('#<%=lblMiddlemonies4   .ClientID%>').autoNumeric('set', lblNMTP4);
                    $('#<%=lblMiddlemonies5   .ClientID%>').autoNumeric('set', lblNMTP5);
                    $('#<%=lblMiddlemonies6   .ClientID%>').autoNumeric('set', lblNMTP6);
                    $('#<%=lblMiddlemonies7   .ClientID%>').autoNumeric('set', lblNMTP7);
                    $('#<%=lblMiddlemonies8   .ClientID%>').autoNumeric('set', lblNMTP8);
                    $('#<%=lblMiddlemoniesBEvn.ClientID%>').autoNumeric('set', lblNMTPBEvn);


                    var ddlTerm = $('#<%=ddlTerm.ClientID%>')[0].options[$('#<%=ddlTerm.ClientID%>')[0].selectedIndex].value;

                    var lblRoyal1 = (ddlTerm == 1) ? 0 : lblRoyalty1;
                    var lblRoyal2 = (ddlTerm == 1) ? 0 : lblRoyalty2;
                    var lblRoyal3 = (ddlTerm == 1) ? 0 : lblRoyalty3;
                    var lblRoyal4 = (ddlTerm == 1) ? 0 : lblRoyalty4;

                    var lblRoyal5 = (ddlTerm == 1) ? 0 : lblRoyal1 * txtNoofweeks;
                    var lblRoyal6 = (ddlTerm == 1) ? 0 : lblRoyal2 * txtNoofweeks;
                    var lblRoyal7 = (ddlTerm == 1) ? 0 : lblRoyal3 * txtNoofweeks;
                    var lblRoyal8 = (ddlTerm == 1) ? 0 : lblRoyal4 * txtNoofweeks;
                    var lblRoyalBEv = (ddlTerm == 1) ? 0 : lblRoyaltyBEvn;

                    $('#<%=lblRoyal1  .ClientID%>').autoNumeric('set', lblRoyal1);
                    $('#<%=lblRoyal2  .ClientID%>').autoNumeric('set', lblRoyal2);
                    $('#<%=lblRoyal3  .ClientID%>').autoNumeric('set', lblRoyal3);
                    $('#<%=lblRoyal4  .ClientID%>').autoNumeric('set', lblRoyal4);
                    $('#<%=lblRoyal5  .ClientID%>').autoNumeric('set', lblRoyal5);
                    $('#<%=lblRoyal6  .ClientID%>').autoNumeric('set', lblRoyal6);
                    $('#<%=lblRoyal7  .ClientID%>').autoNumeric('set', lblRoyal7);
                    $('#<%=lblRoyal8  .ClientID%>').autoNumeric('set', lblRoyal8);
                    $('#<%=lblRoyalBEv.ClientID%>').autoNumeric('set', lblRoyalBEv);


                    var lblGuarante1 = (ddlTerm == 1) ? 0 : lblGuarantee1;
                    var lblGuarante2 = (ddlTerm == 1) ? 0 : lblGuarantee2;
                    var lblGuarante3 = (ddlTerm == 1) ? 0 : lblGuarantee3;
                    var lblGuarante4 = (ddlTerm == 1) ? 0 : lblGuarantee4;
                    var lblGuarante5 = (ddlTerm == 1) ? 0 : lblGuarantee5;
                    var lblGuarante6 = (ddlTerm == 1) ? 0 : lblGuarantee6;
                    var lblGuarante7 = (ddlTerm == 1) ? 0 : lblGuarantee7;
                    var lblGuarante8 = (ddlTerm == 1) ? 0 : lblGuarantee8;
                    var lblGuaranteBEvn = (ddlTerm == 1) ? 0 : lblGuaranteeBEvn;

                    $('#<%=lblGuarante1   .ClientID%>').autoNumeric('set', lblGuarante1);
                    $('#<%=lblGuarante2   .ClientID%>').autoNumeric('set', lblGuarante2);
                    $('#<%=lblGuarante3   .ClientID%>').autoNumeric('set', lblGuarante3);
                    $('#<%=lblGuarante4   .ClientID%>').autoNumeric('set', lblGuarante4);
                    $('#<%=lblGuarante5   .ClientID%>').autoNumeric('set', lblGuarante5);
                    $('#<%=lblGuarante6   .ClientID%>').autoNumeric('set', lblGuarante6);
                    $('#<%=lblGuarante7   .ClientID%>').autoNumeric('set', lblGuarante7);
                    $('#<%=lblGuarante8   .ClientID%>').autoNumeric('set', lblGuarante8);
                    $('#<%=lblGuaranteBEvn.ClientID%>').autoNumeric('set', lblGuaranteBEvn);


                    var lblTotalPR1 = parseFloat(lblPSharesplit1) + parseFloat(lblMiddlemonies1) + parseFloat(lblRoyal1) + parseFloat(lblGuarante1);
                    var lblTotalPR2 = parseFloat(lblPSharesplit2) + parseFloat(lblMiddlemonies2) + parseFloat(lblRoyal2) + parseFloat(lblGuarante2);
                    var lblTotalPR3 = parseFloat(lblPSharesplit3) + parseFloat(lblMiddlemonies3) + parseFloat(lblRoyal3) + parseFloat(lblGuarante3);
                    var lblTotalPR4 = parseFloat(lblPSharesplit4) + parseFloat(lblMiddlemonies4) + parseFloat(lblRoyal4) + parseFloat(lblGuarante4);
                    var lblTotalPR5 = parseFloat(lblPSharesplit5) + parseFloat(lblMiddlemonies5) + parseFloat(lblRoyal5) + parseFloat(lblGuarante5);
                    var lblTotalPR6 = parseFloat(lblPSharesplit6) + parseFloat(lblMiddlemonies6) + parseFloat(lblRoyal6) + parseFloat(lblGuarante6);
                    var lblTotalPR7 = parseFloat(lblPSharesplit7) + parseFloat(lblMiddlemonies7) + parseFloat(lblRoyal7) + parseFloat(lblGuarante7);
                    var lblTotalPR8 = parseFloat(lblPSharesplit8) + parseFloat(lblMiddlemonies8) + parseFloat(lblRoyal8) + parseFloat(lblGuarante8);
                    var lblTotalPRBEVn = parseFloat(lblPSharesplitBEvn) + parseFloat(lblMiddlemoniesBEvn) + parseFloat(lblRoyalBEv) + parseFloat(lblGuaranteBEvn);

                    $('#<%=lblTotalPR1   .ClientID%>').autoNumeric('set', lblTotalPR1);
                    $('#<%=lblTotalPR2   .ClientID%>').autoNumeric('set', lblTotalPR2);
                    $('#<%=lblTotalPR3   .ClientID%>').autoNumeric('set', lblTotalPR3);
                    $('#<%=lblTotalPR4   .ClientID%>').autoNumeric('set', lblTotalPR4);
                    $('#<%=lblTotalPR5   .ClientID%>').autoNumeric('set', lblTotalPR5);
                    $('#<%=lblTotalPR6   .ClientID%>').autoNumeric('set', lblTotalPR6);
                    $('#<%=lblTotalPR7   .ClientID%>').autoNumeric('set', lblTotalPR7);
                    $('#<%=lblTotalPR8   .ClientID%>').autoNumeric('set', lblTotalPR8);
                    $('#<%=lblTotalPRBEVn.ClientID%>').autoNumeric('set', lblTotalPRBEVn);

                    var txtLessTWS = parseFloat($('#<%=txtLessTWS.ClientID%>').autoNumeric('get'));
                    if (isNaN(txtLessTWS)) txtLessTWS = 0;
                    var lbllestaxwithsouce1 = (((parseFloat(lblTotalPR1) * txtLessTWS) / 100)).toFixed(0);
                    var lbllestaxwithsouce2 = (((parseFloat(lblTotalPR2) * txtLessTWS) / 100)).toFixed(0);
                    var lbllestaxwithsouce3 = (((parseFloat(lblTotalPR3) * txtLessTWS) / 100)).toFixed(0);
                    var lbllestaxwithsouce4 = (((parseFloat(lblTotalPR4) * txtLessTWS) / 100)).toFixed(0);
                    var lbllestaxwithsouce5 = (((parseFloat(lblTotalPR5) * txtLessTWS) / 100)).toFixed(0);
                    var lbllestaxwithsouce6 = (((parseFloat(lblTotalPR6) * txtLessTWS) / 100)).toFixed(0);
                    var lbllestaxwithsouce7 = (((parseFloat(lblTotalPR7) * txtLessTWS) / 100)).toFixed(0);
                    var lbllestaxwithsouce8 = (((parseFloat(lblTotalPR8) * txtLessTWS) / 100)).toFixed(0);
                    var lbllestaxwithsouceBEvn = (((parseFloat(lblTotalPRBEVn) * txtLessTWS) / 100)).toFixed(3);

                    $('#<%=lbllestaxwithsouce1   .ClientID%>').autoNumeric('set', lbllestaxwithsouce1);
                    $('#<%=lbllestaxwithsouce2   .ClientID%>').autoNumeric('set', lbllestaxwithsouce2);
                    $('#<%=lbllestaxwithsouce3   .ClientID%>').autoNumeric('set', lbllestaxwithsouce3);
                    $('#<%=lbllestaxwithsouce4   .ClientID%>').autoNumeric('set', lbllestaxwithsouce4);
                    $('#<%=lbllestaxwithsouce5   .ClientID%>').autoNumeric('set', lbllestaxwithsouce5);
                    $('#<%=lbllestaxwithsouce6   .ClientID%>').autoNumeric('set', lbllestaxwithsouce6);
                    $('#<%=lbllestaxwithsouce7   .ClientID%>').autoNumeric('set', lbllestaxwithsouce7);
                    $('#<%=lbllestaxwithsouce8   .ClientID%>').autoNumeric('set', lbllestaxwithsouce8);
                    $('#<%=lbllestaxwithsouceBEvn.ClientID%>').autoNumeric('set', lbllestaxwithsouceBEvn);

                    var txtLessTWamt = $('#<%=txtLessTWamt.ClientID%>').autoNumeric('get');
                    if (isNaN(txtLessTWamt)) txtLessTWamt = 0;
                    var lblLessTWamt1 = txtLessTWamt
                    var lblLessTWamt2 = txtLessTWamt
                    var lblLessTWamt3 = txtLessTWamt
                    var lblLessTWamt4 = txtLessTWamt
                    var lblLessTWamt5 = txtLessTWamt * txtNoofweeks
                    var lblLessTWamt6 = txtLessTWamt * txtNoofweeks
                    var lblLessTWamt7 = txtLessTWamt * txtNoofweeks
                    var lblLessTWamt8 = txtLessTWamt * txtNoofweeks
                    var lblLessTWamtBEvn = txtLessTWamt * txtNoofweeks

                    $('#<%=lblLessTWamt1   .ClientID%>').autoNumeric('set', lblLessTWamt1);
                    $('#<%=lblLessTWamt2   .ClientID%>').autoNumeric('set', lblLessTWamt2);
                    $('#<%=lblLessTWamt3   .ClientID%>').autoNumeric('set', lblLessTWamt3);
                    $('#<%=lblLessTWamt4   .ClientID%>').autoNumeric('set', lblLessTWamt4);
                    $('#<%=lblLessTWamt5   .ClientID%>').autoNumeric('set', lblLessTWamt5);
                    $('#<%=lblLessTWamt6   .ClientID%>').autoNumeric('set', lblLessTWamt6);
                    $('#<%=lblLessTWamt7   .ClientID%>').autoNumeric('set', lblLessTWamt7);
                    $('#<%=lblLessTWamt8   .ClientID%>').autoNumeric('set', lblLessTWamt8);
                    $('#<%=lblLessTWamtBEvn.ClientID%>').autoNumeric('set', lblLessTWamtBEvn);

                    var lblNetincomePr1 = parseFloat(lblTotalPR1) - (parseFloat(lbllestaxwithsouce1) + parseFloat(lblLessTWamt1));
                    var lblNetincomePr2 = parseFloat(lblTotalPR2) - (parseFloat(lbllestaxwithsouce2) + parseFloat(lblLessTWamt2));
                    var lblNetincomePr3 = parseFloat(lblTotalPR3) - (parseFloat(lbllestaxwithsouce3) + parseFloat(lblLessTWamt3));
                    var lblNetincomePr4 = parseFloat(lblTotalPR4) - (parseFloat(lbllestaxwithsouce4) + parseFloat(lblLessTWamt4));
                    var lblNetincomePr5 = parseFloat(lblTotalPR5) - (parseFloat(lbllestaxwithsouce5) + parseFloat(lblLessTWamt5));
                    var lblNetincomePr6 = parseFloat(lblTotalPR6) - (parseFloat(lbllestaxwithsouce6) + parseFloat(lblLessTWamt6));
                    var lblNetincomePr7 = parseFloat(lblTotalPR7) - (parseFloat(lbllestaxwithsouce7) + parseFloat(lblLessTWamt7));
                    var lblNetincomePr8 = parseFloat(lblTotalPR8) - (parseFloat(lbllestaxwithsouce8) + parseFloat(lblLessTWamt8));
                    var lblNetincomePrBEvn = parseFloat(lblTotalPRBEVn) - (parseFloat(lbllestaxwithsouceBEvn) + parseFloat(lblLessTWamtBEvn));

                    $('#<%=lblNetincomePr1   .ClientID%>').autoNumeric('set', lblNetincomePr1);
                    $('#<%=lblNetincomePr2   .ClientID%>').autoNumeric('set', lblNetincomePr2);
                    $('#<%=lblNetincomePr3   .ClientID%>').autoNumeric('set', lblNetincomePr3);
                    $('#<%=lblNetincomePr4   .ClientID%>').autoNumeric('set', lblNetincomePr4);
                    $('#<%=lblNetincomePr5   .ClientID%>').autoNumeric('set', lblNetincomePr5);
                    $('#<%=lblNetincomePr6   .ClientID%>').autoNumeric('set', lblNetincomePr6);
                    $('#<%=lblNetincomePr7   .ClientID%>').autoNumeric('set', lblNetincomePr7);
                    $('#<%=lblNetincomePr8   .ClientID%>').autoNumeric('set', lblNetincomePr8);
                    $('#<%=lblNetincomePrBEvn.ClientID%>').autoNumeric('set', lblNetincomePrBEvn);

                    var txtWOE = $('#<%=txtWOE.ClientID%>').autoNumeric('get');
                    if (isNaN(txtWOE)) txtWOE = 0;
                    var lblWOE1 = (1 * txtWOE).toFixed(0);
                    var lblWOE2 = (1 * txtWOE).toFixed(0);
                    var lblWOE3 = (1 * txtWOE).toFixed(0);
                    var lblWOE4 = (1 * txtWOE).toFixed(0);
                    var lblWOE5 = (txtWOE * txtNoofweeks).toFixed(0);
                    var lblWOE6 = (txtWOE * txtNoofweeks).toFixed(0);
                    var lblWOE7 = (txtWOE * txtNoofweeks).toFixed(0);
                    var lblWOE8 = (txtWOE * txtNoofweeks).toFixed(0);
                    var lblWOEBEvn = (1 * txtWOE).toFixed(3);
                    $('#<%=lblWOE1   .ClientID%>').autoNumeric('set', lblWOE1);
                    $('#<%=lblWOE2   .ClientID%>').autoNumeric('set', lblWOE2);
                    $('#<%=lblWOE3   .ClientID%>').autoNumeric('set', lblWOE3);
                    $('#<%=lblWOE4   .ClientID%>').autoNumeric('set', lblWOE4);
                    $('#<%=lblWOE5   .ClientID%>').autoNumeric('set', lblWOE5);
                    $('#<%=lblWOE6   .ClientID%>').autoNumeric('set', lblWOE6);
                    $('#<%=lblWOE7   .ClientID%>').autoNumeric('set', lblWOE7);
                    $('#<%=lblWOE8   .ClientID%>').autoNumeric('set', lblWOE8);
                    $('#<%=lblWOEBEvn.ClientID%>').autoNumeric('set', lblWOEBEvn);

                    var txtVR = $('#<%=txtVR.ClientID%>').autoNumeric('get');
                    if (isNaN(txtVR)) txtVR = 0;
                    var lblVR1 = txtVR
                    var lblVR2 = txtVR
                    var lblVR3 = txtVR
                    var lblVR4 = txtVR
                    var lblVR5 = txtVR * txtNoofweeks
                    var lblVR6 = txtVR * txtNoofweeks
                    var lblVR7 = txtVR * txtNoofweeks
                    var lblVR8 = txtVR * txtNoofweeks
                    var lblVRBEvn = txtVR * txtNoofweeks
                    $('#<%=lblVR1   .ClientID%>').autoNumeric('set', lblVR1);
                    $('#<%=lblVR2   .ClientID%>').autoNumeric('set', lblVR2);
                    $('#<%=lblVR3   .ClientID%>').autoNumeric('set', lblVR3);
                    $('#<%=lblVR4   .ClientID%>').autoNumeric('set', lblVR4);
                    $('#<%=lblVR5   .ClientID%>').autoNumeric('set', lblVR5);
                    $('#<%=lblVR6   .ClientID%>').autoNumeric('set', lblVR6);
                    $('#<%=lblVR7   .ClientID%>').autoNumeric('set', lblVR7);
                    $('#<%=lblVR8   .ClientID%>').autoNumeric('set', lblVR8);
                    $('#<%=lblVRBEvn.ClientID%>').autoNumeric('set', lblVRBEvn);

                    var txtTotalSHOW1 = parseFloat(lblNetincomePr1) - (parseFloat(lblWOE1) + parseFloat(lblVR1));
                    var txtTotalSHOW2 = parseFloat(lblNetincomePr2) - (parseFloat(lblWOE2) + parseFloat(lblVR2));
                    var txtTotalSHOW3 = parseFloat(lblNetincomePr3) - (parseFloat(lblWOE3) + parseFloat(lblVR3));
                    var txtTotalSHOW4 = parseFloat(lblNetincomePr4) - (parseFloat(lblWOE4) + parseFloat(lblVR4));
                    var txtTotalSHOW5 = parseFloat(lblNetincomePr5) - (parseFloat(lblWOE5) + parseFloat(lblVR5));
                    var txtTotalSHOW6 = parseFloat(lblNetincomePr6) - (parseFloat(lblWOE6) + parseFloat(lblVR6));
                    var txtTotalSHOW7 = parseFloat(lblNetincomePr7) - (parseFloat(lblWOE7) + parseFloat(lblVR7));
                    var txtTotalSHOW8 = parseFloat(lblNetincomePr8) - (parseFloat(lblWOE8) + parseFloat(lblVR8));
                    var txtTotalSHOWBEvn = parseFloat(lblNetincomePrBEvn) - (parseFloat(lblWOEBEvn) + parseFloat(lblVRBEvn));

                    $('#<%=txtTotalSHOW1   .ClientID%>').autoNumeric('set', txtTotalSHOW1);
                    $('#<%=txtTotalSHOW2   .ClientID%>').autoNumeric('set', txtTotalSHOW2);
                    $('#<%=txtTotalSHOW3   .ClientID%>').autoNumeric('set', txtTotalSHOW3);
                    $('#<%=txtTotalSHOW4   .ClientID%>').autoNumeric('set', txtTotalSHOW4);
                    $('#<%=txtTotalSHOW5   .ClientID%>').autoNumeric('set', txtTotalSHOW5);
                    $('#<%=txtTotalSHOW6   .ClientID%>').autoNumeric('set', txtTotalSHOW6);
                    $('#<%=txtTotalSHOW7   .ClientID%>').autoNumeric('set', txtTotalSHOW7);
                    $('#<%=txtTotalSHOW8   .ClientID%>').autoNumeric('set', txtTotalSHOW8);
                    $('#<%=txtTotalSHOWBEvn.ClientID%>').autoNumeric('set', txtTotalSHOWBEvn);

                    if ((1 * lblMoneyRemainingBEvn).toFixed(4) == 0.0000) {
                        break;
                    }
                    if (parseFloat(breakevenperc) == 100) {
                        var firstbevnper = 1 - (lblMoneyRemainingBEvn / lblNetARBEvn);
                        breakevenperc = firstbevnper * 100;
                    } else {

                        var dynamicper = (breakevenperc / 100);
                        var bevnper = (dynamicper - (lblMoneyRemainingBEvn / (lblNetARBEvn / dynamicper)));

                        breakevenperc = bevnper * 100;
                    }
                    $('#<%=txtPerformanceCapacityBrevn.ClientID%>').autoNumeric('set', breakevenperc);

                    $('#<%=hfBevnval.ClientID%>').val(breakevenperc);

                }                
            }
            catch (ex) {
                $("#diverr")[0].innerHTML = ex.message;
            }
            finally {
                if ($('#<%=hfstatus.ClientID%>').val() == "true") {
                    $('#<%= btncalculation.ClientID%>')[0].click();
                }
            }
        }

        function subloadinchang() {
            var txtSubloadin = $('#<%=txtSubloadin.ClientID%>').autoNumeric('get');
            if (isNaN(txtSubloadin)) txtSubloadin = 0

            $('#<%=lblSubloadin.ClientID%>').autoNumeric('set', txtSubloadin);
            $('#<%=lblSubloadin1.ClientID%>').autoNumeric('set', txtSubloadin);
            $('#<%=lblSubloadin2.ClientID%>').autoNumeric('set', txtSubloadin);
            $('#<%=lblSubloadin3.ClientID%>').autoNumeric('set', txtSubloadin);
            $('#<%=lblSubloadin4.ClientID%>').autoNumeric('set', txtSubloadin);
            $('#<%=lblSubloadin5.ClientID%>').autoNumeric('set', txtSubloadin);
            $('#<%=lblSubloadin6.ClientID%>').autoNumeric('set', txtSubloadin);
            $('#<%=lblSubloadin7.ClientID%>').autoNumeric('set', txtSubloadin);
            $('#<%=lblSubloadinBreak.ClientID%>').autoNumeric('set', txtSubloadin);

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
                <div id="diverr" style="display:none;"></div>
            <table width="100%" cellpadding="0" cellspacing="0" align="left">

                <tr>
                    <td class="contentpadding">

                        <table width="100%" cellpadding="0" cellspacing="0" align="left" class="tbl_border">
                            <tr>
                                <td align="center" valign="middle" class="bg_tbl_header txt_contentheader">Break Even
                                    Report</td>
                            </tr>
                            <tr>
                                <td align="center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" border="0">
                                        <tr>
                                            <td style="width: 100%;" align="center" valign="middle" class="contentpadding">Show&nbsp;:&nbsp;
                                                <asp:DropDownList Width="100px" ID="ddlShow" CssClass="my_select_box_auto" runat="server" OnSelectedIndexChanged="ddlShow_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvShow" runat="server" ControlToValidate="ddlShow"
                                                    InitialValue="0"
                                                    CssClass="asterisk" ToolTip="Select Show!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;City/State&nbsp;:&nbsp;
                    <asp:DropDownList AutoPostBack="true" ID="ddlCity" runat="server" Width="100px" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCity"
                                                    InitialValue="0"
                                                    CssClass="asterisk" ToolTip="Select City!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Venue&nbsp;:&nbsp;
                                                <asp:DropDownList Width="100px" ID="ddlVenue" AutoPostBack="true" OnSelectedIndexChanged="ddlVenue_SelectedIndexChanged"
                                                    runat="server">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Start Date&nbsp;:&nbsp;
                                                <asp:DropDownList Width="80px" ID="ddlCreateddate" runat="server" OnSelectedIndexChanged="ddlCreateddate_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCreateddate"
                                                    InitialValue="0"
                                                    CssClass="asterisk" ToolTip="Select Start Date!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;End Date:
                                        <asp:Label ID="lblEngtEndDate" runat="server" Width="80px"></asp:Label>
                                                &nbsp;Discount cap&nbsp;:&nbsp;&nbsp;<asp:TextBox CssClass="Percentage" Width="50px"
                                                    ID="txtDiscountcap" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDiscountcap"
                                                    CssClass="asterisk" ToolTip="Enter Discount Cap" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <%-- <ajaxToolkit:FilteredTextBoxExtender ID="fl" runat="server" TargetControlID="txtDiscountcap" FilterType="Numbers,Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                                                &nbsp;&nbsp;
                                        <asp:Button ID="btnShowRpt" runat="server" Text="Extract" OnClick="btnShowRpt_Click"
                                            OnClientClick="showpop();" />
                                                &nbsp;
                                                <asp:Button ID="btnCalculate" runat="server" Text="Edit Parameters" OnClick="btnCalculate_Click"
                                                    OnClientClick="showpop();" />&nbsp;&nbsp;
                                                <div id="divh" style="display: none">
                                                    <asp:Button ID="btncalculation" OnClick="btncalculation_Click" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr runat="server" id="imgExcel">
                                <td align="center">
                                    <a href="#" class="excel" runat="server" id="lnkexcel" style="border: 0">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Excel.png" Width="80" Height="80"
                                            BorderWidth="0" />
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="padding_leftright10" align="center">
                                                <rsweb:ReportViewer ID="rptBEvn" runat="server" Height="300px" Width="100%" SizeToReportContent="false"
                                                    WaitControlDisplayAfter="0" AsyncRendering="false">
                                                </rsweb:ReportViewer>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div style="display: none">
                                        <asp:Button ID="btnAddNew" runat="server" Text="New Expanse" CausesValidation="false" />
                                          <input id="btnCancel" style="height: 30px; width: 60px" value="Cancel" type="button"
                                                    onclick="cancel();" />
                                    </div>
                                    <ajaxToolkit:ModalPopupExtender ID="modpop" BackgroundCssClass="ModalPopupBG"
                                        runat="server"  TargetControlID="btnAddNew"
                                        PopupControlID="Panel1" Drag="true" PopupDragHandleControlID="PopupHeader">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <div id="Panel1" style="display: none;" class="popupConfirmation">
                                        <div class="popup_Container">
                                            <div class="popup_Titlebar" id="PopupHeader">
                                                <div class="TitlebarLeft">
                                                    Edit BreakEven Parameters
                                                </div>
                                                <div class="TitlebarRight">
                                                </div>
                                            </div>
                                            <div class="popup_Body">

                                                <table style="padding-bottom: 20px;">
                                                    <tr>
                                                        <td align="left" valign="top">No of Shows Per Week 
                                                        </td>
                                                        <td align="left" valign="top">:&nbsp;
                                              <asp:TextBox TabIndex="10" CssClass="Numeric" onchange="return calculate();" ID="txtNoofshowsperweek"
                                                  runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top">No of Weeks 
                                                        </td>
                                                        <td align="left" valign="top">:&nbsp;
                                              <asp:TextBox TabIndex="11" CssClass="Numeric" onchange="return calculate();" ID="txtNoofweeks"
                                                  runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top">Seats Per Show 
                                                        </td>
                                                        <td align="left" valign="top">:&nbsp;
                                              <asp:TextBox TabIndex="12" CssClass="Numeric1" onchange="return calculate();" ID="txtSeatspershow"
                                                  runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top">Weekly Gross Potential 
                                                        </td>
                                                        <td align="left" valign="top">:&nbsp;
                                              <asp:TextBox TabIndex="13" CssClass="Numeric1" onchange="return calculate();" ID="txtWeeklygrospotential"
                                                  runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top">Net Avg Per Tix 
                                                        </td>
                                                        <td align="left" valign="top">:&nbsp;
                                             <asp:Label ID="lblNetavgpertix" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top">Exchange Rate 
                                                        </td>
                                                        <td align="left" valign="top">:&nbsp;
                                             <asp:TextBox TabIndex="14" CssClass="Numeric1" onchange="return calculate();" ID="txtExchangerate"
                                                 runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top">Deal Type	  
                                                        </td>
                                                        <td align="left" valign="top">:&nbsp;
                                           <asp:DropDownList onchange="calculate();" ID="ddlTerm" runat="server">
                                               <asp:ListItem Text="Guarantee Deal" Value="0" Selected="True"></asp:ListItem>
                                               <asp:ListItem Text="Terms Deal" Value="1"></asp:ListItem>
                                           </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <table style="width: 90%; border-top: 2px solid black;" cellpadding="1" border="0">
                                                    <tr>
                                                        <th style="width: 20px; text-align: right"></th>
                                                        <th style="width: 20px; text-align: right"></th>
                                                        <th style="width: 20px; text-align: right">Weekly</th>
                                                        <th style="width: 20px; text-align: right">Weekly</th>
                                                        <th style="width: 20px; text-align: right">Weekly</th>
                                                        <th style="width: 20px; text-align: right">Weekly</th>
                                                        <th style="width: 20px; text-align: right">RUN</th>
                                                        <th style="width: 20px; text-align: right">RUN</th>
                                                        <th style="width: 20px; text-align: right">RUN</th>
                                                        <th style="width: 20px; text-align: right">RUN</th>
                                                        <th style="width: 20px; text-align: right">BREAK</th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="alignleft">House Capacity</td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity1" runat="server"
                                                                Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity2" runat="server"
                                                                Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity3" runat="server"
                                                                Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity4" runat="server"
                                                                Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity5" runat="server"
                                                                Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity6" runat="server"
                                                                Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity7" runat="server"
                                                                Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblHousecapacity8" runat="server"
                                                                Text=""></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="alignleft">Performance Capacity</td>

                                                        <td>
                                                            <%--   <asp:TextBox ID="lblhundredper" runat="server" Text="100"></asp:TextBox>--%>
                                                            <asp:TextBox CssClass="Percentage" onchange="return calculate();" ID="txtPerformanceCapacity1"
                                                                Text="100" Width="50px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="Percentage" onchange="return calculate();" ID="txtPerformanceCapacity2"
                                                                Width="50px" Text="90" runat="server"></asp:TextBox>
                                                            <%--<asp:Label ID="lblhundredper1" runat="server" Text="90"></asp:Label>%--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="Percentage" onchange="return calculate();" ID="txtPerformanceCapacity3"
                                                                Width="50px" Text="80" runat="server"></asp:TextBox>
                                                            <%-- <asp:Label ID="lblhundredper2" runat="server" Text="80"></asp:Label>%--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="Percentage" onchange="return calculate();" ID="txtPerformanceCapacity4"
                                                                Width="50px" Text="70" runat="server"></asp:TextBox>
                                                            <%--<asp:Label ID="lblhundredper3" runat="server" Text="70"></asp:Label>%--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="Percentage" onchange="return calculate();" ID="txtPerformanceCapacity5"
                                                                Width="50px" Text="100" runat="server"></asp:TextBox>
                                                            <%--<asp:Label ID="lblhundredper4" runat="server" Text="100"></asp:Label>%--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="Percentage" onchange="return calculate();" ID="txtPerformanceCapacity6"
                                                                Width="50px" Text="90" runat="server"></asp:TextBox>
                                                            <%--<asp:Label ID="lblhundredper5" runat="server" Text="90"></asp:Label>%--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="Percentage" onchange="return calculate();" ID="txtPerformanceCapacity7"
                                                                Width="50px" Text="80" runat="server"></asp:TextBox>
                                                            <%--<asp:Label ID="lblhundredper6" runat="server" Text="80"></asp:Label>%--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="Percentage" onchange="return calculate();" ID="txtPerformanceCapacity8"
                                                                Width="50px" Text="70" runat="server"></asp:TextBox><%--<asp:Label ID="lblhundredper7" runat="server" Text="70"></asp:Label>%--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel PercentageBE" ID="txtPerformanceCapacityBrevn"
                                                                Width="100px" Text="100" runat="server"></asp:TextBox>
                                                            <%--<asp:Label ID="lblBreakPercentage" runat="server" Text="0.00"></asp:Label>%--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="alignleft">Tickets Sold </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldweek1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldweek2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldweek3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldweek4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldRun1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldRun2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldRun3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldRun4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric" ID="lblTicketsoldBreakeven" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="alignleft">Sub Load - in</td>
                                                        <td>
                                                            <asp:TextBox TabIndex="15" CssClass="Numeric1" onchange="return calculate();" ID="txtSubloadin"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadin" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadin1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadin2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadin3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadin4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadin5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadin6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadin7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubloadinBreak" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="2" class="alignleft">Box Office Gross </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssale" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssale1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssale2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssale3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssale4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssale5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssale6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssale7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblBoxofcgrosssalebreakeven" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="alignleft">Less Discounts </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="16" CssClass="Percentage" onchange="return calculate();" ID="txtLessDiscounts"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountWeek1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountWeek2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountWeek3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountWeek4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountWeek5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountWeek6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountWeek7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountWeek8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessdiscountBreakeven" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="alignleft">Adjusted Gross  </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossWeekly1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossWeekly2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossWeekly3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossWeekly4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossWeekly5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossWeekly6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossWeekly7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossWeekly8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdjustedgrossBreakeven" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td class="alignleft">Tax  
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="17" CssClass="Percentage" onchange="return calculate();" ID="txtTax"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTax1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTax2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTax3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTax4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTax5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTax6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTax7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTax8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTaxBeakeven" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="alignleft">Restoration 
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="18" CssClass="Numeric1" onchange="return calculate();" ID="txtRestoration"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestoration1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestoration2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestoration3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestoration4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestoration5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestoration6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestoration7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestoration8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRestorationBreakeven" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="alignleft">Subscription Charge 
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="19" CssClass="Percentage" onchange="return calculate();" ID="txtSubscriptioncharge"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptioncharge1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptioncharge2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptioncharge3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptioncharge4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptioncharge5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptioncharge6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptioncharge7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptioncharge8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblSubscriptionchargeBreakeven"
                                                                runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="alignleft">Credit Card & Other Commissions  
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="20" CssClass="Percentage" onchange="return calculate();" ID="txtCCothercommissions"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssns1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssns2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssns3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssns4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssns5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssns6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssns7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssns8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCCothercommssnsBreakeven" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-weight: bold; text-align: left">Net Adjusted B. O. Receipts
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetAR1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetAR2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetAR3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetAR4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetAR5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetAR6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetAR7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetAR8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetARBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="11" style="text-align: left"></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="btnClearall" OnClientClick="return clearall(false);" runat="server"
                                                                Text="Clear All" />&nbsp;&nbsp;<asp:Button ID="btnSelectall" OnClientClick="return clearall(true);"
                                                                    runat="server" Text="Select All" /></td>
                                                        <td>@ US $ </td>
                                                        <td>@ Can $</td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkGuarantee" Checked="true" Text="Guarantee"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <%--<asp:CheckBox ID="chkGuarantee" Checked="true" runat="server" />&nbsp;&nbsp;--%>
                                                            <asp:TextBox TabIndex="21" CssClass="Numeric1" onchange="return calculate();" ID="txtGuarantee"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarantee1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarantee2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarantee3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarantee4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarantee5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarantee6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarantee7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarantee8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuaranteeBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkRoyalty" Checked="true" Text="Royalty"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="22" CssClass="Percentage" ID="txtRoyalty" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalty1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalty2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalty3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalty4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalty5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalty6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalty7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalty8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyaltyBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkFixedCosts" Checked="true" Text="Fixed Costs"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="23" CssClass="Numeric1" onchange="return calculate();" ID="txtFixedCosts"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcosts1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcosts2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcosts3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcosts4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcosts5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcosts6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcosts7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcosts8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFixedcostsBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkAddlHouseEquipment" Checked="true"
                                                                Text="Add'l House Equipment" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="24" CssClass="Numeric1" onchange="return calculate();" ID="txtAdHouseEquipment"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipment1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipment2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipment3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipment4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipment5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipment6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipment7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipment8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdHouseEquipmentBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkAdvertising" Checked="true" Text="Advertising"
                                                                runat="server" /></td>
                                                        <td>
                                                            <asp:TextBox TabIndex="25" CssClass="Numeric1" onchange="return calculate();" ID="txtAdvertising"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertising1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertising2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertising3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertising4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertising5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertising6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertising7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertising8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblAdvertisingBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkCatering" Checked="true" Text="Catering"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="26" CssClass="Numeric1" onchange="return calculate();" ID="txtCatering"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCatering1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCatering2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCatering3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCatering4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCatering5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCatering6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCatering7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCatering8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblCateringBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkInsurance" Checked="true" Text="Insurance"
                                                                runat="server" />
                                                        </td>

                                                        <td>
                                                            <asp:TextBox TabIndex="27" CssClass="Numeric1" onchange="return calculate();" ID="txtInsurance"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsurance1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsurance2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsurance3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsurance4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsurance5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsurance6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsurance7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsurance8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblInsuranceBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkFireWatch" Checked="true" Text="Fire Watch"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="28" CssClass="Numeric1" onchange="return calculate();" ID="txtFireWatch"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatch1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatch2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatch3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatch4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatch5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatch6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatch7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatch8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblFirewatchBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkMusicians" Checked="true" Text="Musicians"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="29" CssClass="Numeric1" onchange="return calculate();" ID="txtMusicians"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusicians1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusicians2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusicians3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusicians4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusicians5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusicians6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusicians7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusicians8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMusiciansBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkRent" Checked="true" Text="Rent"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="30" CssClass="Numeric1" onchange="return calculate();" ID="txtRent"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRent1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRent2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRent3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRent4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRent5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRent6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRent7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRent8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRentBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkStagehandsLoadInOut" Checked="true"
                                                                Text="Stagehands - Load In/Out" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="31" CssClass="Numeric1" onchange="return calculate();" ID="txtStagehandsloadin"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodin1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodin2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodin3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodin4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodin5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodin6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodin7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodin8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandslodinBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkStagehandsRunning" Checked="true"
                                                                Text="Stagehands - Running" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="32" CssClass="Numeric1" onchange="return calculate();" ID="txtStagehandsrunning"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunning1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunning2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunning3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunning4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunning5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunning6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunning7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunning8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblStagehandsrunningBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkTicketPrinting" Checked="true"
                                                                Text="Ticket Printing" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="33" CssClass="Numeric1" onchange="return calculate();" ID="txtTicketprinting"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrinting1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrinting2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrinting3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrinting4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrinting5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrinting6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrinting7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrinting8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTicketPrintingBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; white-space: nowrap">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkWardrobeHairLoadInOut" Checked="true"
                                                                Text="Wardrobe/Hair-Load In/Out" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="34" CssClass="Numeric1" onchange="return calculate();" ID="txtWardrobehairloadin"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadin1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadin2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadin3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadin4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadin5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadin6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadin7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadin8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairloadinBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkWardrobeHairRunning" Checked="true"
                                                                Text="Wardrobe/Hair - Running" runat="server" /></td>
                                                        <td>
                                                            <asp:TextBox TabIndex="35" CssClass="Numeric1" onchange="return calculate();" ID="txtWardrobehairrunning"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunning1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunning2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunning3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunning4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunning5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunning6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunning7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunning8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWardrobehairrunningBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkPresenterProfit" Checked="true"
                                                                Text="Presenter Profit" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="36" CssClass="Numeric1" onchange="return calculate();" ID="txtPF"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPF1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPF2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPF3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPF4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPF5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPF6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPF7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPF8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPFBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox onclick="return calculate();" ID="chkOther" Checked="true" Text="Other"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="37" CssClass="Numeric1" onchange="return calculate();" ID="txtOther"
                                                                runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOther1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOther2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOther3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOther4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOther5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOther6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOther7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOther8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblOtherBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-weight: bold; white-space: nowrap; text-align: left">TOTAL
                                                            LOCAL EXPENSE
                                                        </td>

                                                        <td>
                                                            <asp:TextBox TabIndex="38" CssClass="textboxlabel Numeric1" ID="lblTotalLExp1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalLExp2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalLExp3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalLExp4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalLExp5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalLExp6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalLExp7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalLExp8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalLExpBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; white-space: nowrap; text-align: left"></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; white-space: nowrap; text-align: left">FORMULA CHECK
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-weight: bold; white-space: nowrap; text-align: left">Money
                                                            Remaining
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemaining1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemaining2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemaining3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemaining4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemaining5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemaining6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemaining7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemaining8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMoneyRemainingBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; text-align: left">Next Monies - To Producer </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="39" CssClass="Numeric1" onchange="return calculate();" ID="txtNMTP"
                                                                runat="server" Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTP1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTP2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTP3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTP4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTP5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTP6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTP7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTP8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; text-align: left">Next Monies - To Presenter</td>
                                                        <td>
                                                            <asp:TextBox TabIndex="40" CssClass="Numeric1" onchange="return calculate();" ID="txtNMTPTR"
                                                                runat="server" Text=""></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTR1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTR2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTR3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTR4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTR5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTR6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTR7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTR8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNMTPTRBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-weight: bold; white-space: nowrap; text-align: left">Total
                                                            Engagement
                                                                        Profit </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEP1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEP2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEP3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEP4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEP5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEP6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEP7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEP8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTEPBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; white-space: nowrap; text-align: left">Presenter Share
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="41" CssClass="Percentage" onchange="return calculate();" ID="txtPShare"
                                                                Text="" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShare1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShare2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShare3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShare4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShare5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShare6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShare7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShare8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPShareBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; white-space: nowrap; text-align: left">Producer Share
                                                            of split </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="42" CssClass="Percentage" onchange="return calculate();" ID="txtPShareofsplit"
                                                                Text="" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplit1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplit2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplit3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplit4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplit5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplit6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplit7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplit8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblPSharesplitBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="white-space: nowrap; text-align: left">Middle Monies</td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemonies1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemonies2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemonies3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemonies4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemonies5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemonies6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemonies7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemonies8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblMiddlemoniesBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: left">Royalty 
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyal1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyal2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyal3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyal4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyal5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyal6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyal7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyal8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblRoyalBEv" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: left">Guarantee 
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarante1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarante2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarante3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarante4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarante5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarante6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarante7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuarante8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblGuaranteBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: left; font-weight: bold;">TOTAL TO PRODUCER 
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPR1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPR2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPR3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPR4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPR5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPR6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPR7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPR8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblTotalPRBEVn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">Less Taxes Witheld at Source
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="43" CssClass="Percentage" onchange="return calculate();" ID="txtLessTWS"
                                                                runat="server" Text=""></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouce1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouce2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouce3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouce4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouce5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouce6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouce7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouce8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lbllestaxwithsouceBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">Less Taxes Witheld at Source
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="44" CssClass="Numeric1" onchange="return calculate();" ID="txtLessTWamt"
                                                                runat="server" Text=""></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamt1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamt2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamt3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamt4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamt5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamt6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamt7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamt8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblLessTWamtBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: left; font-weight: bold">Net Income to Producer
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePr1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePr2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePr3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePr4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePr5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePr6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePr7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePr8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblNetincomePrBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">Weekly Operating Expenses
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="45" CssClass="Numeric1" onchange="return calculate();" ID="txtWOE"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOE1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOE2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOE3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOE4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOE5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOE6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOE7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOE8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblWOEBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">Variable Royalties
                                                        </td>
                                                        <td>
                                                            <asp:TextBox TabIndex="46" CssClass="Numeric1" onchange="return calculate();" ID="txtVR"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVR1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVR2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVR3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVR4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVR5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVR6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVR7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVR8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="lblVRBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: left; font-weight: bold">Total Show Profit 
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOW1" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOW2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOW3" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOW4" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOW5" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOW6" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOW7" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOW8" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="textboxlabel Numeric1" ID="txtTotalSHOWBEvn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>


                                            </div>
                                            <div class="popup_Buttons">

                                              <%--  <asp:Button ID="btnOkay" Height="30px" Width="40px" Text="OK" runat="server" OnClick="btnPrintReport_Click" />
                                                <input id="btnCancel" style="height: 30px; width: 60px" value="Cancel" type="button"
                                                    onclick="cancel();" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfBevnval" runat="server" />
                                    <asp:HiddenField ID="hfstatus" runat="server" />
                                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Orange" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <%-- <div style="display: none">
        <asp:Button ID="btnPrintReport" runat="server" OnClick="btnPrintReport_Click" />
    </div>--%>
        </ContentTemplate>

    </asp:UpdatePanel>
    

</asp:Content>

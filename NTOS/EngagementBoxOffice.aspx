<%@ Page Title="Engagement Box Ofice" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Engagement.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="EngagementBoxOffice.aspx.cs"
    Inherits="NTOS.EngagementBoxOffice" %>

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
    <script type="text/jscript" src="Scripts/autoNumeric 1.9.15.js"></script>

 <script type="text/javascript">


     function ShowDiv()
     {
        
         alert('m in sdfhid div');
        // document.getElementById('trother2').style.display = 'block';
         $('.trother2').show();
     }


     function HideDiv() {

         alert('m in hid div');
         alert(document.getElementById('trother2'));
        // document.getElementById('trother2').style.display = 'none';
         $('.trother2').hide();
         
     }

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


     var temp_flag = "";
     function Get_taxamt(tax_ff_type, GrossRecAmt) {
         var TaxAmt = 0;
         var tax1 = 0;
      
         tax1 = parseFloat($('#<%=txtTax1.ClientID%>').autoNumeric('get'));

            if (tax_ff_type == "IO") { TaxAmt = GrossRecAmt * tax1; } else { TaxAmt = GrossRecAmt - (GrossRecAmt / (1 + tax1)); }
            return parseFloat(TaxAmt).toFixed(2);
        }

        function GetSalesLessTax_FF(GrossRecAmt, FacFee, Tax) {
            var SalesLessTaxFFAmt = 0, tax1 = 0;
            tax1 = parseFloat($('#<%=txtTax1.ClientID%>').autoNumeric('get'));
            switch (temp_flag) {
                case ("IBI"):
                    SalesLessTaxFFAmt = parseFloat(GrossRecAmt) - parseFloat(FacFee) - parseFloat(Tax);
                    break;
                case ("I0O"):
                    SalesLessTaxFFAmt = GrossRecAmt / (1 + tax1);
                    break;
                case ("OO"): case ("INO"): default:
                    SalesLessTaxFFAmt = GrossRecAmt;
                    break;
                case ("OI"):
                    SalesLessTaxFFAmt = GrossRecAmt - FacFee;
                    break;
            }
            return SalesLessTaxFFAmt;
        }
     function Subscriptioncalc() {
       // debugger;
         var deal_ff_IO = "", deal_tax_ff = "", deal_ff_unit = "", deal_tax_IO = "";
         deal_tax_ff = $('#<%=hdn_dealtaxptg_ff.ClientID%>').val();
         deal_ff_unit = $('#<%=hdn_deal_ff_unit.ClientID%>').val();
         deal_tax_IO = $('#<%=hdn_deal_tax_ptg_include.ClientID%>').val();
         var deal_ff_IO = "", deal_tax_ff = $('#<%=hdn_dealtaxptg_ff.ClientID%>').val();
         deal_ff_IO = $('#<%=hdn_deal_ff_IO.ClientID%>').val();
         init();
         $('.int').autoNumeric({ aSep: '', vMax: '2147483647', vMin: '0' });
         $('.smallint').autoNumeric({ aSep: '', vMax: '32767', vMin: '0' });
         $('.Numeric').autoNumeric({ vMax: '999999999999999', vMin: '-999999999999999', nBracket: '(,)' });
         $('.Dollar').autoNumeric({ aSign: '$', vMax: '999999999999999.99', vMin: '-999999999999999.99', nBracket: '(,)' });
         $('.Percentage').autoNumeric({ aSign: '%', pSign: 's', vMax: '100' });
         $('.Percentage1').autoNumeric({ vMax: '1.00000' });



            var chk_override = $('#<%=hdn_checkoverride.ClientID%>').val();


         $('#<%=hidfval.ClientID%>').val("");
         parseFloat($('#<%=txtFacilityfeeoneachticket.ClientID%>').autoNumeric('init'));

         var txtFacilityfeeoneachticket = parseFloat($('#<%=txtFacilityfeeoneachticket.ClientID%>').autoNumeric('get'));
         negative(txtFacilityfeeoneachticket)
         if (isNaN(txtFacilityfeeoneachticket)) txtFacilityfeeoneachticket = 0.00

         //***************************Ticket Sold***************************************************
         var txtSubscriptionsold = parseFloat($('#<%=txtSubscriptionsold.ClientID%>').val()).toFixed(2);

            negative(txtSubscriptionsold)

            if (isNaN(txtSubscriptionsold)) txtSubscriptionsold = 0.00
            var txtPhoneTcktSold = parseFloat($('#<%=txtPhoneTcktSold.ClientID%>').val()).toFixed(2);

            negative(txtPhoneTcktSold)

            if (isNaN(txtPhoneTcktSold)) txtPhoneTcktSold = 0.00
            var txtInternetTcktSold = parseFloat($('#<%=txtInternetTcktSold.ClientID%>').val()).toFixed(2);

            negative(txtInternetTcktSold)

            if (isNaN(txtInternetTcktSold)) txtInternetTcktSold = 0.00
            var txtCreditcrdTcktSold = parseFloat($('#<%=txtCreditcrdTcktSold.ClientID%>').val()).toFixed(2);

            negative(txtCreditcrdTcktSold)

            if (isNaN(txtCreditcrdTcktSold)) txtCreditcrdTcktSold = 0.00
            var txtRemoteoutletTcktSold = parseFloat($('#<%=txtRemoteoutletTcktSold.ClientID%>').val()).toFixed(2);

            negative(txtRemoteoutletTcktSold)

            if (isNaN(txtRemoteoutletTcktSold)) txtRemoteoutletTcktSold = 0.00
            var txtSingletixtcktSold = parseFloat($('#<%=txtSingletixtcktSold.ClientID%>').val()).toFixed(2);

            negative(txtSingletixtcktSold)

            if (isNaN(txtSingletixtcktSold)) txtSingletixtcktSold = 0.00
            var txtGroupTicktSold = parseFloat($('#<%=txtGroupTicktSold.ClientID%>').val()).toFixed(2);

            negative(txtGroupTicktSold)

            if (isNaN(txtGroupTicktSold)) txtGroupTicktSold = 0.00
            var txtGroup1TicktSold = parseFloat($('#<%=txtGroup1TicktSold.ClientID%>').val()).toFixed(2);

            negative(txtGroup1TicktSold)


            if (isNaN(txtGroup1TicktSold)) txtGroup1TicktSold = 0.00

          
            var txtOtherPercTcktSold = parseFloat($('#<%=txtOtherPercTcktSold.ClientID%>').val()).toFixed(2);

            negative(txtOtherPercTcktSold)

            if (isNaN(txtOtherPercTcktSold)) txtOtherPercTcktSold = 0.00
            var txtOtherDollTcktSold = parseFloat($('#<%=txtOtherDollTcktSold.ClientID%>').val()).toFixed(2);

            negative(txtOtherDollTcktSold)

            if (isNaN(txtOtherDollTcktSold)) txtOtherDollTcktSold = 0.00
            
          //  debugger;

            var txtOther3TcktSold = parseFloat($('#<%=txtOther3TcktSold.ClientID%>').val()).toFixed(2);
            
            negative(txtOther3TcktSold)

            if (isNaN(txtOther4TcktSold)) txtOther4TcktSold = 0.00

            var txtOther4TcktSold = parseFloat($('#<%=txtOther4TcktSold.ClientID%>').val()).toFixed(2);

            negative(txtOther4TcktSold)

            if (isNaN(txtOther4TcktSold)) txtOther4TcktSold = 0.00

            var txtOther5TcktSold = parseFloat($('#<%=txtOther5TcktSold.ClientID%>').val()).toFixed(2);

            negative(txtOther5TcktSold)

            if (isNaN(txtOther5TcktSold)) txtOther5TcktSold = 0.00



         //****************************Gross Receipts***********************************************

            var txtSubscriptionreceipts = parseFloat($('#<%=txtSubscriptionreceipts.ClientID%>').autoNumeric('get')).toFixed(2);

            negative(txtSubscriptionreceipts)

            if (isNaN(txtSubscriptionreceipts)) txtSubscriptionreceipts = 0.00
            var txtPhoneGrossReceipts = parseFloat($('#<%=txtPhoneGrossReceipts.ClientID%>').autoNumeric('get')).toFixed(2);

            negative(txtPhoneGrossReceipts)

            if (isNaN(txtPhoneGrossReceipts)) txtPhoneGrossReceipts = 0.00
            var txtInternetGrossRecpt = parseFloat($('#<%=txtInternetGrossRecpt.ClientID%>').autoNumeric('get')).toFixed(2);

            negative(txtInternetGrossRecpt)

            if (isNaN(txtInternetGrossRecpt)) txtInternetGrossRecpt = 0.00
            var txtCreditGrossReceipts = parseFloat($('#<%=txtCreditGrossReceipts.ClientID%>').autoNumeric('get')).toFixed(2);

            negative(txtCreditGrossReceipts)

            if (isNaN(txtCreditGrossReceipts)) txtCreditGrossReceipts = 0.00
            var txtRemoteGrossReceipts = parseFloat($('#<%=txtRemoteGrossReceipts.ClientID%>').autoNumeric('get')).toFixed(2);

            negative(txtRemoteGrossReceipts)

            if (isNaN(txtRemoteGrossReceipts)) txtRemoteGrossReceipts = 0.00
            var txtSingletixtGrossReceipts = parseFloat($('#<%=txtSingletixtGrossReceipts.ClientID%>').autoNumeric('get')).toFixed(2);

            negative(txtSingletixtGrossReceipts)

            if (isNaN(txtSingletixtGrossReceipts)) txtSingletixtGrossReceipts = 0.00
            var txtGroupGrossReceipts = parseFloat($('#<%=txtGroupGrossReceipts.ClientID%>').autoNumeric('get')).toFixed(2);

            negative(txtGroupGrossReceipts)

            if (isNaN(txtGroupGrossReceipts)) txtGroupGrossReceipts = 0.00
            var txtGroup1grossReceipts = parseFloat($('#<%=txtGroup1grossReceipts.ClientID%>').autoNumeric('get')).toFixed(2);

            negative(txtGroup1grossReceipts)

            if (isNaN(txtGroup1grossReceipts)) txtGroup1grossReceipts = 0.00
          //  var txtOtherPercGrossReceipts = parseFloat($('#<%=txtOtherPercGrossReceipts.ClientID%>').autoNumeric('get')).toFixed(2);
              var txtOtherPercGrossReceipts=parseFloat($('#<%=txtOtherPercGrossReceipts.ClientID%>').val()).toFixed(2);
              negative(txtOtherPercGrossReceipts)

            if (isNaN(txtOtherPercGrossReceipts)) txtOtherPercGrossReceipts = 0.00
            //var txtOtherDollGrossReceipts = parseFloat($('#<%=txtOtherDollGrossReceipts.ClientID%>').autoNumeric('get')).toFixed(2);
             var txtOtherDollGrossReceipts = parseFloat($('#<%=txtOtherDollGrossReceipts.ClientID%>').val()).toFixed(2);
            negative(txtOtherDollGrossReceipts)

            if (isNaN(txtOtherDollGrossReceipts)) txtOtherDollGrossReceipts = 0.00

            var txtOther3GrossReceipts = parseFloat($('#<%=txtOther3GrossReceipts.ClientID%>').val()).toFixed(2);
            negative(txtOther3GrossReceipts)

            if (isNaN(txtOther3GrossReceipts)) txtOther3GrossReceipts = 0.00

            var txtOther4GrossReceipts = parseFloat($('#<%=txtOther4GrossReceipts.ClientID%>').val()).toFixed(2);
            negative(txtOther4GrossReceipts)

            if (isNaN(txtOther4GrossReceipts)) txtOther4GrossReceipts = 0.00


            var txtOther5GrossReceipts = parseFloat($('#<%=txtOther5GrossReceipts.ClientID%>').val()).toFixed(2);
            negative(txtOther5GrossReceipts)

            if (isNaN(txtOther5GrossReceipts)) txtOther5GrossReceipts = 0.00


         //**********************Total-GrossReceipts>************************************
         //debugger;
            var totalGrossReceipts = parseFloat((parseFloat(txtSubscriptionreceipts) + parseFloat(txtPhoneGrossReceipts) + parseFloat(txtInternetGrossRecpt)
                 + parseFloat(txtCreditGrossReceipts) + parseFloat(txtRemoteGrossReceipts) + parseFloat(txtSingletixtGrossReceipts)
                 + parseFloat(txtGroupGrossReceipts) + parseFloat(txtGroup1grossReceipts))).toFixed(2);
         //+ parseFloat(txtGroupGrossReceipts) + parseFloat(txtGroup1grossReceipts) + parseFloat(txtOtherPercGrossReceipts) + parseFloat(txtOtherDollGrossReceipts))).toFixed(2);
            if (isNaN(totalGrossReceipts)) totalGrossReceipts = 0.00
         // $('#<%=txtTotalGrossReceipts.ClientID%>').val(totalGrossReceipts);
         $('#<%=txtTotalGrossReceipts.ClientID%>').autoNumeric('set', totalGrossReceipts);
         //****************************************************************************


         //*****************************************************************************************
         var hidFdollar = $('#<%=hidFdollar.ClientID%>').val();
         var hidFPerc = $('#<%=hidFPerc.ClientID%>').val();
         if (hidFdollar.length > 0) {

             var susfacltyfee1 = parseFloat(txtSubscriptionsold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblSubscriptionfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee1;

             if(chk_override !='y')
             $('#<%=lblSubscriptionfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee1);

                negative(susfacltyfee1)

                //$('.demoSet').autoNumeric('set', someValue)
                //**********************FacilityfeePhone************************************
                var susfacltyfee2 = parseFloat(txtPhoneTcktSold * txtFacilityfeeoneachticket).toFixed(2);
             //  $('#<%=lblPhoneFacilityfee.ClientID%>')[0].innerHTML = susfacltyfee2;

                if (chk_override != 'y')
                $('#<%=lblPhoneFacilityfee.ClientID%>').autoNumeric('set', susfacltyfee2);

                negative(susfacltyfee2)

                //**********************FacilityfeeInternet************************************
                var susfacltyfee3 = parseFloat(txtInternetTcktSold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblInternetFacltyFee.ClientID%>')[0].innerHTML = susfacltyfee3;

             if (chk_override != 'y')
             $('#<%=lblInternetFacltyFee.ClientID%>').autoNumeric('set', susfacltyfee3);


                negative(susfacltyfee3)

                //**********************FacilityfeeCreditcrdTckt************************************
                var susfacltyfee4 = parseFloat(txtCreditcrdTcktSold * txtFacilityfeeoneachticket).toFixed(2);
                //$('#<%=lblCreditfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee4;
             if (chk_override != 'y')
             $('#<%=lblCreditfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee4);


                negative(susfacltyfee4)

                //**********************FacilityfeeInternet************************************
                var susfacltyfee5 = parseFloat(txtRemoteoutletTcktSold * txtFacilityfeeoneachticket).toFixed(2);
                //$('#<%=lblRemotefacilityfee.ClientID%>')[0].innerHTML = susfacltyfee5;
             if (chk_override != 'y')
             $('#<%=lblRemotefacilityfee.ClientID%>').autoNumeric('set', susfacltyfee5);


                negative(susfacltyfee5)

                //**********************FacilityfeeSingletixt************************************
                var susfacltyfee6 = parseFloat(txtSingletixtcktSold * txtFacilityfeeoneachticket).toFixed(2);
                //$('#<%=lblSingletixfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee6;
             if (chk_override != 'y')
             $('#<%=lblSingletixfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee6);


                negative(susfacltyfee6)

                //**********************FacilityfeeGroup<************************************
                var susfacltyfee7 = parseFloat(txtGroupTicktSold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblGroupsfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee7;

             if (chk_override != 'y')
                $('#<%=lblGroupsfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee7);


                negative(susfacltyfee7)

                //**********************FacilityfeeGroup>************************************
                var susfacltyfee8 = parseFloat(txtGroup1TicktSold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblGroup1facilityfee.ClientID%>')[0].innerHTML = susfacltyfee8;

             if (chk_override != 'y')
                $('#<%=lblGroup1facilityfee.ClientID%>').autoNumeric('set', susfacltyfee8);


                negative(susfacltyfee8)

                //**********************FacilityfeeOtherPerc>************************************
                var susfacltyfee9 = parseFloat(txtOtherPercTcktSold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblOtherPercfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee9;

             if (chk_override != 'y')
                $('#<%=lblOtherPercfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee9);


                negative(susfacltyfee9)

                //**********************FacilityfeeOtherDoll>************************************
                var susfacltyfee10 = parseFloat(txtOtherDollTcktSold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblOtherDollfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee10;

             if (chk_override != 'y')
                $('#<%=lblOtherDollfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee10);


             negative(susfacltyfee10)

             //**********************FacilityfeeOther3>************************************
             var susfacltyfee11 = parseFloat(txtOther3TcktSold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblOther3facilityfee.ClientID%>')[0].innerHTML = susfacltyfee10;

             if (chk_override != 'y')
                 $('#<%=lblOther3facilityfee.ClientID%>').autoNumeric('set', susfacltyfee11);


             negative(susfacltyfee11)



             //**********************FacilityfeeOther4>************************************
             var susfacltyfee12 = parseFloat(txtOther4TcktSold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblOther4facilityfee.ClientID%>')[0].innerHTML = susfacltyfee10;

             if (chk_override != 'y')
                 $('#<%=lblOther4facilityfee.ClientID%>').autoNumeric('set', susfacltyfee12);


             negative(susfacltyfee12)


             //**********************FacilityfeeOther5>************************************
             var susfacltyfee13 = parseFloat(txtOther5TcktSold * txtFacilityfeeoneachticket).toFixed(2);
             //$('#<%=lblOther5facilityfee.ClientID%>')[0].innerHTML = susfacltyfee10;

             if (chk_override != 'y')
                 $('#<%=lblOther5facilityfee.ClientID%>').autoNumeric('set', susfacltyfee13);


             negative(susfacltyfee13)


            }
            else if (hidFPerc.length > 0) {
                var susfacltyfee1 = parseFloat((parseFloat(txtSubscriptionreceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblSubscriptionfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee1;

                if (chk_override != 'y')
                $('#<%=lblSubscriptionfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee1);

                negative(susfacltyfee1)

                //$('.demoSet').autoNumeric('set', someValue)
                //**********************FacilityfeePhone************************************
                var susfacltyfee2 = parseFloat((parseFloat(txtPhoneGrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //  $('#<%=lblPhoneFacilityfee.ClientID%>')[0].innerHTML = susfacltyfee2;

                if (chk_override != 'y')
                $('#<%=lblPhoneFacilityfee.ClientID%>').autoNumeric('set', susfacltyfee2);

                negative(susfacltyfee2)

                //**********************FacilityfeeInternet************************************
                var susfacltyfee3 = parseFloat((parseFloat(txtInternetGrossRecpt) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblInternetFacltyFee.ClientID%>')[0].innerHTML = susfacltyfee3;

                if (chk_override != 'y')
                $('#<%=lblInternetFacltyFee.ClientID%>').autoNumeric('set', susfacltyfee3);


                negative(susfacltyfee3)

                //**********************FacilityfeeCreditcrdTckt************************************
                var susfacltyfee4 = parseFloat((parseFloat(txtCreditGrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblCreditfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee4;

                if (chk_override != 'y')
                $('#<%=lblCreditfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee4);


                negative(susfacltyfee4)

                //**********************FacilityfeeInternet************************************
                var susfacltyfee5 = parseFloat((parseFloat(txtRemoteGrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblRemotefacilityfee.ClientID%>')[0].innerHTML = susfacltyfee5;

                if (chk_override != 'y')
                $('#<%=lblRemotefacilityfee.ClientID%>').autoNumeric('set', susfacltyfee5);


                negative(susfacltyfee5)

                //**********************FacilityfeeSingletixt************************************
                var susfacltyfee6 = parseFloat((parseFloat(txtSingletixtGrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblSingletixfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee6;

                if (chk_override != 'y')
                $('#<%=lblSingletixfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee6);


                negative(susfacltyfee6)

                //**********************FacilityfeeGroup<************************************
                var susfacltyfee7 = parseFloat((parseFloat(txtGroupGrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblGroupsfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee7;

                if (chk_override != 'y')
                $('#<%=lblGroupsfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee7);


                negative(susfacltyfee7)

                //**********************FacilityfeeGroup>************************************
                var susfacltyfee8 = parseFloat((parseFloat(txtGroup1grossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblGroup1facilityfee.ClientID%>')[0].innerHTML = susfacltyfee8;
                if (chk_override != 'y')
                $('#<%=lblGroup1facilityfee.ClientID%>').autoNumeric('set', susfacltyfee8);


                negative(susfacltyfee8)

                //**********************FacilityfeeOtherPerc>************************************
                var susfacltyfee9 = parseFloat((parseFloat(txtOtherPercGrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblOtherPercfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee9;

                if (chk_override != 'y')
                $('#<%=lblOtherPercfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee9);


                negative(susfacltyfee9)

                //**********************FacilityfeeOtherDoll>************************************
                var susfacltyfee10 = parseFloat((parseFloat(txtOtherDollGrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblOtherDollfacilityfee.ClientID%>')[0].innerHTML = susfacltyfee10;

                if (chk_override != 'y')
                $('#<%=lblOtherDollfacilityfee.ClientID%>').autoNumeric('set', susfacltyfee10);


                negative(susfacltyfee10)



                //**********************FacilityfeeOther3>************************************
                var susfacltyfee11 = parseFloat((parseFloat(txtOther3GrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblOther3facilityfee.ClientID%>')[0].innerHTML = susfacltyfee10;

                if (chk_override != 'y')
                    $('#<%=lblOther3facilityfee.ClientID%>').autoNumeric('set', susfacltyfee11);



                negative(susfacltyfee11)


                //**********************FacilityfeeOther4>************************************
                var susfacltyfee12 = parseFloat((parseFloat(txtOther4GrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblOther4facilityfee.ClientID%>')[0].innerHTML = susfacltyfee10;

                if (chk_override != 'y')
                    $('#<%=lblOther4facilityfee.ClientID%>').autoNumeric('set', susfacltyfee12);


                negative(susfacltyfee12)



                //**********************FacilityfeeOther5>************************************
                var susfacltyfee13 = parseFloat((parseFloat(txtOther5GrossReceipts) * parseFloat(txtFacilityfeeoneachticket)) / 100).toFixed(2);
                //$('#<%=lblOther5facilityfee.ClientID%>')[0].innerHTML = susfacltyfee10;

                if (chk_override != 'y')
                    $('#<%=lblOther5facilityfee.ClientID%>').autoNumeric('set', susfacltyfee13);


                negative(susfacltyfee13)




            }
         //***********************************Totalfacilityfees*************************************************
        var totalfacilityfee = parseFloat((parseFloat(susfacltyfee1) + parseFloat(susfacltyfee2) + parseFloat(susfacltyfee3)
            + parseFloat(susfacltyfee4) + parseFloat(susfacltyfee5) + parseFloat(susfacltyfee6) + parseFloat(susfacltyfee7) +
            parseFloat(susfacltyfee8) + parseFloat(susfacltyfee9) + parseFloat(susfacltyfee10) + parseFloat(susfacltyfee11) + parseFloat(susfacltyfee12) + parseFloat(susfacltyfee13))).toFixed(2);
        if (isNaN(totalfacilityfee)) totalfacilityfee = 0.00
         //$('#<%=lblTotalfacilityfee.ClientID%>')[0].innerHTML = totalfacilityfee;
         $('#<%=lblTotalfacilityfee.ClientID%>').autoNumeric('set', totalfacilityfee);

         negative(totalfacilityfee)
         //**********************Total-TicketSold>************************************
         var totalTicketSold = parseFloat((parseFloat(txtSubscriptionsold) + parseFloat(txtPhoneTcktSold) + parseFloat(txtInternetTcktSold)
              + parseFloat(txtCreditcrdTcktSold) + parseFloat(txtRemoteoutletTcktSold) + parseFloat(txtSingletixtcktSold)
              + parseFloat(txtGroupTicktSold) + parseFloat(txtGroup1TicktSold)));
         //+ parseFloat(txtGroupTicktSold) + parseFloat(txtGroup1TicktSold) + parseFloat(txtOtherPercTcktSold) + parseFloat(txtOtherDollTcktSold)));
         if (isNaN(totalTicketSold)) totalTicketSold = 0.00
         $('#<%=txtTotalTcktSold.ClientID%>').val(totalTicketSold);


            negative(totalTicketSold)

         //$('#<%=txtTotalTcktSold.ClientID%>').autoNumeric('set', totalTicketSold);

         //***********************************Sales less amusement tax & facilityfees*************************************************

         var lblSubscriptionfacilityfee = parseFloat($('#<%=lblSubscriptionfacilityfee.ClientID%>').autoNumeric('get'));
         if (isNaN(lblSubscriptionfacilityfee)) lblSubscriptionfacilityfee = 0.00


         negative(lblSubscriptionfacilityfee)

         var lblPhoneFacilityfee = parseFloat($('#<%=lblPhoneFacilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblPhoneFacilityfee)) lblPhoneFacilityfee = 0.00
            negative(lblPhoneFacilityfee)

            var lblInternetFacltyFee = parseFloat($('#<%=lblInternetFacltyFee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblInternetFacltyFee)) lblInternetFacltyFee = 0.00
            negative(lblInternetFacltyFee)

            var lblCreditfacilityfee = parseFloat($('#<%=lblCreditfacilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblCreditfacilityfee)) lblCreditfacilityfee = 0.00
            negative(lblCreditfacilityfee)

            var lblRemotefacilityfee = parseFloat($('#<%=lblRemotefacilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblRemotefacilityfee)) lblRemotefacilityfee = 0.00
            negative(lblRemotefacilityfee)

            var lblSingletixfacilityfee = parseFloat($('#<%=lblSingletixfacilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblSingletixfacilityfee)) lblSingletixfacilityfee = 0.00
            negative(lblSingletixfacilityfee)

            var lblGroupsfacilityfee = parseFloat($('#<%=lblGroupsfacilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblGroupsfacilityfee)) lblGroupsfacilityfee = 0.00
            negative(lblGroupsfacilityfee)

            var lblGroup1facilityfee = parseFloat($('#<%=lblGroup1facilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblGroup1facilityfee)) lblGroup1facilityfee = 0.00
            negative(lblGroup1facilityfee)
            var tot_other_ff = "0";
            var lblOtherPercfacilityfee = parseFloat($('#<%=lblOtherPercfacilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblOtherPercfacilityfee)) lblOtherPercfacilityfee = 0.00
            negative(lblOtherPercfacilityfee)

            var lblOtherDollfacilityfee = parseFloat($('#<%=lblOtherDollfacilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblOtherDollfacilityfee)) lblOtherDollfacilityfee = 0.00
            negative(lblOtherDollfacilityfee)

            var lblOther3facilityfee = parseFloat($('#<%=lblOther3facilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblOther3facilityfee)) lblOther3facilityfee = 0.00
            negative(lblOther3facilityfee)

            var lblOther4facilityfee = parseFloat($('#<%=lblOther4facilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblOther4facilityfee)) lblOther4facilityfee = 0.00
            negative(lblOther4facilityfee)

            var lblOther5facilityfee = parseFloat($('#<%=lblOther5facilityfee.ClientID%>').autoNumeric('get'));
            if (isNaN(lblOther5facilityfee)) lblOther5facilityfee = 0.00
            negative(lblOther5facilityfee)
           
            tot_other_ff = parseFloat(lblOtherPercfacilityfee) + parseFloat(lblOtherDollfacilityfee) + parseFloat(lblOther3facilityfee) + parseFloat(lblOther4facilityfee) + parseFloat(lblOther5facilityfee);
         //**********************************************sales less amusement tax & facility fee***************

            var tax = parseFloat($('#<%=txtTax1.ClientID%>').autoNumeric('get'));
            negative(tax)
            var less_ff_tax_for_nagbour = 0, less_ff_tax_flag = 'n';
            var subsamusementtaxfee = 0, phoneamusementtaxfee = 0, Internetamusementtaxfee = 0, CreditGrossRecptsamusementtaxfee = 0, RemoteGrossRecptsamusementtaxfee = 0,
                SingletixGrossRecptsamusementtaxfee = 0, GroupGrossRecptsamusementtaxfee = 0, Group1GrossRecptsamusementtaxfee = 0, OtherPerGrossRecptsamusementtaxfee = 0,
                OtherDollGrossRecptsamusementtaxfee = 0, Other3GrossRecptsamusementtaxfee = 0, Other4GrossRecptsamusementtaxfee = 0, Other5GrossRecptsamusementtaxfee = 0;
            var subsamusementtax = 0, Phoneamusementtax = 0, Internetamusementtax = 0, Creditamusementtax = 0, Remoteamusementtax = 0, SingleTixamusementtax = 0, Groupamusementtax = 0,
                  Group1amusementtax = 0, OtherPercamusementtax = 0, OtherDollcamusementtax = 0,Other3amusementtax = 0,Other4amusementtax = 0,Other5amusementtax = 0, totalamusementtax = 0;
            temp_flag = deal_tax_IO + deal_tax_ff.trim() + deal_ff_IO;

            if (deal_tax_IO == "I" && deal_tax_ff == "A" && deal_ff_IO == "I") {
                //Sales Less Fac fee and Tax Calculation
                subsamusementtaxfee = parseFloat((parseFloat(txtSubscriptionreceipts) - parseFloat(susfacltyfee1)) / (1 + parseFloat(tax))).toFixed(2);
                phoneamusementtaxfee = parseFloat((parseFloat(txtPhoneGrossReceipts) - parseFloat(susfacltyfee2)) / (1 + parseFloat(tax))).toFixed(2);
                Internetamusementtaxfee = parseFloat((parseFloat(txtInternetGrossRecpt) - parseFloat(susfacltyfee3)) / (1 + parseFloat(tax))).toFixed(2);
                CreditGrossRecptsamusementtaxfee = parseFloat((parseFloat(txtCreditGrossReceipts) - parseFloat(susfacltyfee4)) / (1 + parseFloat(tax))).toFixed(2);
                RemoteGrossRecptsamusementtaxfee = parseFloat((parseFloat(txtRemoteGrossReceipts) - parseFloat(susfacltyfee5)) / (1 + parseFloat(tax))).toFixed(2);
                SingletixGrossRecptsamusementtaxfee = parseFloat((parseFloat(txtSingletixtGrossReceipts) - parseFloat(susfacltyfee6)) / (1 + parseFloat(tax))).toFixed(2);
                GroupGrossRecptsamusementtaxfee = parseFloat((parseFloat(txtGroupGrossReceipts) - parseFloat(susfacltyfee7)) / (1 + parseFloat(tax))).toFixed(2);
                Group1GrossRecptsamusementtaxfee = parseFloat((parseFloat(txtGroup1grossReceipts) - parseFloat(susfacltyfee8)) / (1 + parseFloat(tax))).toFixed(2);
                OtherPerGrossRecptsamusementtaxfee = parseFloat((parseFloat(txtOtherPercGrossReceipts) - parseFloat(susfacltyfee9)) / (1 + parseFloat(tax))).toFixed(2);
                OtherDollGrossRecptsamusementtaxfee = parseFloat((parseFloat(txtOtherDollGrossReceipts) - parseFloat(susfacltyfee10)) / (1 + parseFloat(tax))).toFixed(2);
                Other3GrossRecptsamusementtaxfee = parseFloat((parseFloat(txtOther3GrossReceipts) - parseFloat(susfacltyfee11)) / (1 + parseFloat(tax))).toFixed(2);
                Other4GrossRecptsamusementtaxfee = parseFloat((parseFloat(txtOther4GrossReceipts) - parseFloat(susfacltyfee12)) / (1 + parseFloat(tax))).toFixed(2);
                Other5GrossRecptsamusementtaxfee = parseFloat((parseFloat(txtOther5GrossReceipts) - parseFloat(susfacltyfee13)) / (1 + parseFloat(tax))).toFixed(2);

                //Tax Calculation
                subsamusementtax = ((parseFloat(txtSubscriptionreceipts) - parseFloat(susfacltyfee1)) - parseFloat(subsamusementtaxfee)).toFixed(2);
                Phoneamusementtax = ((parseFloat(txtPhoneGrossReceipts) - parseFloat(susfacltyfee2)) - parseFloat(phoneamusementtaxfee)).toFixed(2);
                Internetamusementtax = ((parseFloat(txtInternetGrossRecpt) - parseFloat(susfacltyfee3)) - parseFloat(Internetamusementtaxfee)).toFixed(2);
                Creditamusementtax = ((parseFloat(txtCreditGrossReceipts) - parseFloat(susfacltyfee4)) - parseFloat(CreditGrossRecptsamusementtaxfee)).toFixed(2);
                Remoteamusementtax = ((parseFloat(txtRemoteGrossReceipts) - parseFloat(susfacltyfee5)) - parseFloat(RemoteGrossRecptsamusementtaxfee)).toFixed(2);
                SingleTixamusementtax = ((parseFloat(txtSingletixtGrossReceipts) - parseFloat(susfacltyfee6)) - parseFloat(SingletixGrossRecptsamusementtaxfee)).toFixed(2);
                Groupamusementtax = ((parseFloat(txtGroupGrossReceipts) - parseFloat(susfacltyfee7)) - parseFloat(GroupGrossRecptsamusementtaxfee)).toFixed(2);
                Group1amusementtax = ((parseFloat(txtGroup1grossReceipts) - parseFloat(susfacltyfee8)) - parseFloat(Group1GrossRecptsamusementtaxfee)).toFixed(2);
                OtherPercamusementtax = ((parseFloat(txtOtherPercGrossReceipts) - parseFloat(susfacltyfee9)) - parseFloat(OtherPerGrossRecptsamusementtaxfee)).toFixed(2);
                OtherDollcamusementtax = ((parseFloat(txtOtherDollGrossReceipts) - parseFloat(susfacltyfee10)) - parseFloat(OtherDollGrossRecptsamusementtaxfee)).toFixed(2);
                Other3amusementtax = ((parseFloat(txtOther3GrossReceipts) - parseFloat(susfacltyfee11)) - parseFloat(Other3GrossRecptsamusementtaxfee)).toFixed(2);
                Other4amusementtax = ((parseFloat(txtOther4GrossReceipts) - parseFloat(susfacltyfee12)) - parseFloat(Other4GrossRecptsamusementtaxfee)).toFixed(2);
                Other5amusementtax = ((parseFloat(txtOther5GrossReceipts) - parseFloat(susfacltyfee13)) - parseFloat(Other5GrossRecptsamusementtaxfee)).toFixed(2);
            }
            else {
                if ((deal_tax_IO == "I" && deal_tax_ff == "B" && deal_ff_IO == "I") || (temp_flag == "I0O" || temp_flag == "INO")) {
                    var tax_ff_type = "IB";
                }
                else {
                    var tax_ff_type = "IO";
                }
                subsamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtSubscriptionreceipts));
                Phoneamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtPhoneGrossReceipts));
                Internetamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtInternetGrossRecpt));
                Creditamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtCreditGrossReceipts));
                Remoteamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtRemoteGrossReceipts));
                SingleTixamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtSingletixtGrossReceipts));
                Groupamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtGroupGrossReceipts));
                Group1amusementtax = Get_taxamt(tax_ff_type, parseFloat(txtGroup1grossReceipts));
                OtherPercamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtOtherPercGrossReceipts));
                OtherDollcamusementtax = Get_taxamt(tax_ff_type, parseFloat(txtOtherDollGrossReceipts));
                Other3amusementtax = Get_taxamt(tax_ff_type, parseFloat(txtOther3GrossReceipts));
                Other4amusementtax = Get_taxamt(tax_ff_type, parseFloat(txtOther4GrossReceipts));
                Other5amusementtax = Get_taxamt(tax_ff_type, parseFloat(txtOther5GrossReceipts));

                if (temp_flag == "IBI" || temp_flag == "I0O") {//Sales Less Fac fee and Tax Calculation
                    subsamusementtaxfee = GetSalesLessTax_FF(txtSubscriptionreceipts, susfacltyfee1, subsamusementtax);
                    phoneamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtPhoneGrossReceipts), parseFloat(susfacltyfee2), parseFloat(Phoneamusementtax));
                    Internetamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtInternetGrossRecpt), parseFloat(susfacltyfee3), parseFloat(Internetamusementtax));
                    CreditGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtCreditGrossReceipts), parseFloat(susfacltyfee4), parseFloat(Creditamusementtax));
                    RemoteGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtRemoteGrossReceipts), parseFloat(susfacltyfee5), parseFloat(Remoteamusementtax));
                    SingletixGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtSingletixtGrossReceipts), parseFloat(susfacltyfee6), parseFloat(SingleTixamusementtax));
                    GroupGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtGroupGrossReceipts), parseFloat(susfacltyfee7), parseFloat(Groupamusementtax));
                    Group1GrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtGroup1grossReceipts), parseFloat(susfacltyfee8), parseFloat(Group1amusementtax));
                    OtherPerGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOtherPercGrossReceipts), parseFloat(susfacltyfee9), parseFloat(OtherPercamusementtax));
                    OtherDollGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOtherDollGrossReceipts), parseFloat(susfacltyfee10), parseFloat(OtherDollcamusementtax));
                    Other3GrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOther3GrossReceipts), parseFloat(susfacltyfee11), parseFloat(Other3amusementtax));
                    Other4GrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOther4GrossReceipts), parseFloat(susfacltyfee12), parseFloat(Other4amusementtax));
                    Other5GrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOther5GrossReceipts), parseFloat(susfacltyfee13), parseFloat(Other5amusementtax));
                }
                else {
                    subsamusementtaxfee = GetSalesLessTax_FF(txtSubscriptionreceipts, susfacltyfee1, subsamusementtax);
                    phoneamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtPhoneGrossReceipts), parseFloat(susfacltyfee2), parseFloat(Phoneamusementtax));
                    Internetamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtInternetGrossRecpt), parseFloat(susfacltyfee3), parseFloat(Internetamusementtax));
                    CreditGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtCreditGrossReceipts), parseFloat(susfacltyfee4), parseFloat(Creditamusementtax));
                    RemoteGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtRemoteGrossReceipts), parseFloat(susfacltyfee5), parseFloat(Remoteamusementtax));
                    SingletixGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtSingletixtGrossReceipts), parseFloat(susfacltyfee6), parseFloat(SingleTixamusementtax));
                    GroupGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtGroupGrossReceipts), parseFloat(susfacltyfee7), parseFloat(Groupamusementtax));
                    Group1GrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtGroup1grossReceipts), parseFloat(susfacltyfee8), parseFloat(Group1amusementtax));
                    OtherPerGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOtherPercGrossReceipts), parseFloat(susfacltyfee9), parseFloat(OtherPercamusementtax));
                    OtherDollGrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOtherDollGrossReceipts), parseFloat(susfacltyfee10), parseFloat(OtherDollcamusementtax));
                    Other3GrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOther3GrossReceipts), parseFloat(susfacltyfee11), parseFloat(Other3amusementtax));
                    Other4GrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOther4GrossReceipts), parseFloat(susfacltyfee12), parseFloat(Other4amusementtax));
                    Other5GrossRecptsamusementtaxfee = GetSalesLessTax_FF(parseFloat(txtOther5GrossReceipts), parseFloat(susfacltyfee13), parseFloat(Other5amusementtax));

                    less_ff_tax_flag = 'y';
                }
                less_ff_tax_flag = (temp_flag == "I0O") ? "lessff" : less_ff_tax_flag;
                less_ff_tax_flag = (temp_flag == "OI") ? "lesstax" : less_ff_tax_flag;
            }

         //========================================================Sales Less Tax1 & Facility Fee Begin========================================================
            if (isNaN(subsamusementtaxfee)) subsamusementtaxfee = 0.00;
            $('#<%=lblSubsSalslessamusementtax.ClientID%>').autoNumeric('set', subsamusementtaxfee);
            negative(subsamusementtaxfee)
            negative(phoneamusementtaxfee)
            if (isNaN(phoneamusementtaxfee)) phoneamusementtaxfee = 0.00
            $('#<%=lblPhoneSaleslessamusemntfee.ClientID%>').autoNumeric('set', phoneamusementtaxfee);

            if (isNaN(Internetamusementtaxfee)) Internetamusementtaxfee = 0.00
            $('#<%=lblInternetSaleslesAmusmntfee.ClientID%>').autoNumeric('set', Internetamusementtaxfee);
            negative(Internetamusementtaxfee)

            if (isNaN(CreditGrossRecptsamusementtaxfee)) CreditGrossRecptsamusementtaxfee = 0.00
            $('#<%=lblCreditSaleslesAmusementfee.ClientID%>').autoNumeric('set', CreditGrossRecptsamusementtaxfee);
            negative(CreditGrossRecptsamusementtaxfee)

            if (isNaN(RemoteGrossRecptsamusementtaxfee)) RemoteGrossRecptsamusementtaxfee = 0.00
            $('#<%=lblRemoteSaleslesAmusementfee.ClientID%>').autoNumeric('set', RemoteGrossRecptsamusementtaxfee);
            negative(RemoteGrossRecptsamusementtaxfee)

            if (isNaN(SingletixGrossRecptsamusementtaxfee)) SingletixGrossRecptsamusementtaxfee = 0.00
            $('#<%=lblSingletixAmusementfee.ClientID%>').autoNumeric('set', SingletixGrossRecptsamusementtaxfee);
            negative(SingletixGrossRecptsamusementtaxfee)

            if (isNaN(GroupGrossRecptsamusementtaxfee)) GroupGrossRecptsamusementtaxfee = 0.00
            $('#<%=lblGroupAmusementfee.ClientID%>').autoNumeric('set', GroupGrossRecptsamusementtaxfee);
            negative(GroupGrossRecptsamusementtaxfee)

            if (isNaN(Group1GrossRecptsamusementtaxfee)) Group1GrossRecptsamusementtaxfee = 0.00
            $('#<%=lblGroup1Amusementfee.ClientID%>').autoNumeric('set', Group1GrossRecptsamusementtaxfee);
            negative(Group1GrossRecptsamusementtaxfee)

            if (isNaN(OtherPerGrossRecptsamusementtaxfee)) OtherPerGrossRecptsamusementtaxfee = 0.00
         //exclued other1 and other2 changed by siva
            var excludeothers_saleslesstax = parseFloat(OtherPerGrossRecptsamusementtaxfee);
            $('#<%=lblOtherPercAmusementfee.ClientID%>').autoNumeric('set', OtherPerGrossRecptsamusementtaxfee);
            negative(OtherPerGrossRecptsamusementtaxfee)

            excludeothers_saleslesstax = parseFloat(excludeothers_saleslesstax) + parseFloat(OtherDollGrossRecptsamusementtaxfee);
            if (isNaN(OtherDollGrossRecptsamusementtaxfee)) OtherDollGrossRecptsamusementtaxfee = 0.00
            $('#<%=lblOtherDollAmusementfee.ClientID%>').autoNumeric('set', OtherDollGrossRecptsamusementtaxfee);
            negative(OtherDollGrossRecptsamusementtaxfee)


            excludeothers_saleslesstax = parseFloat(excludeothers_saleslesstax) + parseFloat(Other3GrossRecptsamusementtaxfee);
            if (isNaN(Other3GrossRecptsamusementtaxfee)) Other3GrossRecptsamusementtaxfee = 0.00
            $('#<%=lblOther3Amusementfee.ClientID%>').autoNumeric('set', Other3GrossRecptsamusementtaxfee);
            negative(Other3GrossRecptsamusementtaxfee)

            excludeothers_saleslesstax = parseFloat(excludeothers_saleslesstax) + parseFloat(Other4GrossRecptsamusementtaxfee);
            if (isNaN(Other4GrossRecptsamusementtaxfee)) Other4GrossRecptsamusementtaxfee = 0.00
            $('#<%=lblOther4Amusementfee.ClientID%>').autoNumeric('set', Other4GrossRecptsamusementtaxfee);
         negative(Other4GrossRecptsamusementtaxfee)

         excludeothers_saleslesstax = parseFloat(excludeothers_saleslesstax) + parseFloat(Other5GrossRecptsamusementtaxfee);
         if (isNaN(Other5GrossRecptsamusementtaxfee)) Other5GrossRecptsamusementtaxfee = 0.00
         $('#<%=lblOther5Amusementfee.ClientID%>').autoNumeric('set', Other5GrossRecptsamusementtaxfee);
            negative(Other5GrossRecptsamusementtaxfee)

         //*************************************************total sales less amusement tax & fac fee***********************
            var TotalAmusementfee = parseFloat((parseFloat(subsamusementtaxfee) + parseFloat(phoneamusementtaxfee) + parseFloat(Internetamusementtaxfee) +
                parseFloat(CreditGrossRecptsamusementtaxfee) + parseFloat(RemoteGrossRecptsamusementtaxfee) + parseFloat(SingletixGrossRecptsamusementtaxfee) +
                parseFloat(GroupGrossRecptsamusementtaxfee) + parseFloat(Group1GrossRecptsamusementtaxfee) + parseFloat(OtherPerGrossRecptsamusementtaxfee) +
                parseFloat(OtherDollGrossRecptsamusementtaxfee) + parseFloat(Other3GrossRecptsamusementtaxfee) + parseFloat(Other4GrossRecptsamusementtaxfee) + parseFloat(Other5GrossRecptsamusementtaxfee))).toFixed(2);
            if (isNaN(TotalAmusementfee)) TotalAmusementfee = 0.00;
            $('#<%=lblTotalAmusementfee.ClientID%>').autoNumeric('set', TotalAmusementfee);
            negative(TotalAmusementfee)

         //========================================================Sales Less Tax1 & Facility Fee End========================================================

           if(chk_override !='y')
               {
            if (isNaN(subsamusementtax)) subsamusementtax = 0.00;
            $('#<%=lblSubscriptionAmusementtax.ClientID%>').autoNumeric('set', subsamusementtax);
            if (isNaN(Phoneamusementtax)) Phoneamusementtax = 0.00;
            $('#<%=lblPhoneAmusementtax.ClientID%>').autoNumeric('set', Phoneamusementtax);
            if (isNaN(Internetamusementtax)) Internetamusementtax = 0.00;
            $('#<%=lblInternetAmusemntTax.ClientID%>').autoNumeric('set', Internetamusementtax);
            if (isNaN(Creditamusementtax)) Creditamusementtax = 0.00;
            $('#<%=lblCreditAmusementtax.ClientID%>').autoNumeric('set', Creditamusementtax);
            if (isNaN(Remoteamusementtax)) Remoteamusementtax = 0.00;
            $('#<%=lblRemoteAmusementtax.ClientID%>').autoNumeric('set', Remoteamusementtax);
            if (isNaN(SingleTixamusementtax)) SingleTixamusementtax = 0.00;
            $('#<%=lblSingletixAmusementtax.ClientID%>').autoNumeric('set', SingleTixamusementtax);
            if (isNaN(Groupamusementtax)) Groupamusementtax = 0.00;
            $('#<%=lblGroupAmusementtax.ClientID%>').autoNumeric('set', Groupamusementtax);
            if (isNaN(Group1amusementtax)) Group1amusementtax = 0.00;
            $('#<%=lblGroup1Amusementtax.ClientID%>').autoNumeric('set', Group1amusementtax);
            if (isNaN(OtherPercamusementtax)) OtherPercamusementtax = 0.00
            var exclude_Others_tax1 = OtherPercamusementtax;
            $('#<%=lblOtherPercAmusementtax.ClientID%>').autoNumeric('set', OtherPercamusementtax);
            if (isNaN(OtherDollcamusementtax)) OtherDollcamusementtax = 0.00
            exclude_Others_tax1 = parseFloat(exclude_Others_tax1) + parseFloat(OtherDollcamusementtax);
            $('#<%=lblOtherDollAmusementtax.ClientID%>').autoNumeric('set', OtherDollcamusementtax);

            if (isNaN(Other3amusementtax)) Other3amusementtax = 0.00
            exclude_Others_tax1 = parseFloat(exclude_Others_tax1) + parseFloat(Other3amusementtax);
            $('#<%=lblOther3Amusementtax.ClientID%>').autoNumeric('set', Other3amusementtax);
           
             if (isNaN(Other4amusementtax)) Other4amusementtax = 0.00
             exclude_Others_tax1 = parseFloat(exclude_Others_tax1) + parseFloat(Other4amusementtax);
             $('#<%=lblOther4Amusementtax.ClientID%>').autoNumeric('set', Other4amusementtax);

            if (isNaN(Other5amusementtax)) Other5amusementtax = 0.00
            exclude_Others_tax1 = parseFloat(exclude_Others_tax1) + parseFloat(Other5amusementtax);
            $('#<%=lblOther5Amusementtax.ClientID%>').autoNumeric('set', Other5amusementtax);
               }

            totalamusementtax = parseFloat((parseFloat(subsamusementtax) + parseFloat(Phoneamusementtax) + parseFloat(Internetamusementtax) + parseFloat(Creditamusementtax) + parseFloat(Remoteamusementtax) +
                parseFloat(SingleTixamusementtax) + parseFloat(Groupamusementtax) + parseFloat(Group1amusementtax) + parseFloat(OtherPercamusementtax) + parseFloat(OtherDollcamusementtax) + parseFloat(Other3amusementtax) + parseFloat(Other4amusementtax) + parseFloat(Other5amusementtax))).toFixed(2);
            $('#<%=lblTotalAmusementtax.ClientID%>').autoNumeric('set', totalamusementtax);

            if (less_ff_tax_flag == 'y') { less_ff_tax_for_nagbour = parseFloat(totalamusementtax) + parseFloat(totalfacilityfee); }
            if (less_ff_tax_flag == 'lessff') { less_ff_tax_for_nagbour = parseFloat(totalfacilityfee); }
            if (less_ff_tax_flag == 'lesstax') { less_ff_tax_for_nagbour = parseFloat(totalamusementtax); }
         //**************************************************Net Commission********************************************



         //*******calculate tax2 done by siva            
            var txtgrossales_fortax2 = parseFloat($('#<%=txtgrossales.ClientID%>').autoNumeric('get'));
            var tax2per = parseFloat($('#<%=lbltax2DollPerc.ClientID%>').autoNumeric('get')).toFixed(2);
         var excludeothers = parseFloat(OtherDollcamusementtax) + parseFloat(OtherPercamusementtax)+parseFloat(Other3amusementtax)+parseFloat(Other4amusementtax)+parseFloat(Other5amusementtax);
         var tax2netcomm1 = parseFloat(txtgrossales_fortax2) - (parseFloat(totalfacilityfee) + (parseFloat(totalamusementtax) - parseFloat(excludeothers)));
         var tax2netcomm2 = parseFloat(tax2netcomm1) * parseFloat(tax2per) / 100;
         if (isNaN(tax2netcomm2)) tax2netcomm2 = 0.00
         $('#<%=lbltax2Netcommsn.ClientID%>').autoNumeric('set', tax2netcomm2);
         negative(tax2netcomm2);

         var txtSubsSalesPerc = parseFloat($('#<%=txtSubsSalesPerc.ClientID%>').autoNumeric('get'));
            if (isNaN(txtSubsSalesPerc)) txtSubsSalesPerc = 0.00
            negative(txtSubsSalesPerc)
            var subsNetCommsn = parseFloat((parseFloat(subsamusementtaxfee) * parseFloat(txtSubsSalesPerc)) / 100).toFixed(2);
         // $('#<%=lblSubsSalesNetcommission.ClientID%>').html(subsNetCommsn);

         if(chk_override != 'y')
         $('#<%=lblSubsSalesNetcommission.ClientID%>').autoNumeric('set', subsNetCommsn);
         negative(subsNetCommsn)

         var txtPhonePerc = parseFloat($('#<%=txtPhonePerc.ClientID%>').autoNumeric('get'));
         if (isNaN(txtPhonePerc)) txtPhonePerc = 0.00
         negative(txtPhonePerc)
         var PhoneNetCommsn = parseFloat((parseFloat(phoneamusementtaxfee) * parseFloat(txtPhonePerc)) / 100).toFixed(2);
         //$('#<%=lblPhoneNetCommsn.ClientID%>').html(PhoneNetCommsn);
         if (chk_override != 'y')
         $('#<%=lblPhoneNetCommsn.ClientID%>').autoNumeric('set', PhoneNetCommsn);
         negative(PhoneNetCommsn)

         var txtInternetPerc = parseFloat($('#<%=txtInternetPerc.ClientID%>').autoNumeric('get'));
         if (isNaN(txtInternetPerc)) txtInternetPerc = 0.00
         negative(txtInternetPerc)
         var InternetCommsn = parseFloat((parseFloat(Internetamusementtaxfee) * parseFloat(txtInternetPerc)) / 100).toFixed(2);
         //$('#<%=lblInternetNetCommsn.ClientID%>').html(InternetCommsn);
         if (chk_override != 'y')
         $('#<%=lblInternetNetCommsn.ClientID%>').autoNumeric('set', InternetCommsn);
         negative(InternetCommsn)

         var txtCreditPerc = parseFloat($('#<%=txtCreditPerc.ClientID%>').autoNumeric('get'));
         if (isNaN(txtCreditPerc)) txtCreditPerc = 0.00
         negative(txtCreditPerc)
         var CreditCommsn = parseFloat((parseFloat(CreditGrossRecptsamusementtaxfee) * parseFloat(txtCreditPerc)) / 100).toFixed(2);
         // $('#<%=lblCreditNetCommsn.ClientID%>').html(CreditCommsn);

         if (chk_override != 'y')
         $('#<%=lblCreditNetCommsn.ClientID%>').autoNumeric('set', CreditCommsn);
         negative(CreditCommsn)

         var txtRemotePerc = parseFloat($('#<%=txtRemotePerc.ClientID%>').autoNumeric('get'));
         if (isNaN(txtRemotePerc)) txtRemotePerc = 0.00
         negative(txtRemotePerc)
         var RemoteCommsn = parseFloat((parseFloat(RemoteGrossRecptsamusementtaxfee) * parseFloat(txtRemotePerc)) / 100).toFixed(2);
         // $('#<%=lblRemoteNetCommsn.ClientID%>').html(RemoteCommsn);

         if (chk_override != 'y')
         $('#<%=lblRemoteNetCommsn.ClientID%>').autoNumeric('set', RemoteCommsn);
         negative(RemoteCommsn)

         var txtSingletixPerc = parseFloat($('#<%=txtSingletixPerc.ClientID%>').autoNumeric('get'));
         if (isNaN(txtSingletixPerc)) txtSingletixPerc = 0.00
         negative(txtSingletixPerc)
         var SingletixCommsn = parseFloat((parseFloat(SingletixGrossRecptsamusementtaxfee) * parseFloat(txtSingletixPerc)) / 100).toFixed(2);
         // $('#<%=lblSingletixNetCommsn.ClientID%>').html(SingletixCommsn);

         if (chk_override != 'y')
         $('#<%=lblSingletixNetCommsn.ClientID%>').autoNumeric('set', SingletixCommsn);
         negative(SingletixCommsn)

         var txtGroupPerc = parseFloat($('#<%=txtGroupPerc.ClientID%>').autoNumeric('get'));
         if (isNaN(txtGroupPerc)) txtGroupPerc = 0.00
         negative(txtGroupPerc)
         var GroupCommsn = parseFloat((parseFloat(GroupGrossRecptsamusementtaxfee) * parseFloat(txtGroupPerc)) / 100).toFixed(2);
         //$('#<%=lblNetCommsn.ClientID%>').html(GroupCommsn);

         if (chk_override != 'y')
         $('#<%=lblNetCommsn.ClientID%>').autoNumeric('set', GroupCommsn);
         negative(GroupCommsn)

         var txtGroup1Perc = parseFloat($('#<%=txtGroup1Perc.ClientID%>').autoNumeric('get'));
         if (isNaN(txtGroup1Perc)) txtGroup1Perc = 0.00
         negative(txtGroup1Perc)
         var Group1Commsn = parseFloat((parseFloat(Group1GrossRecptsamusementtaxfee) * parseFloat(txtGroup1Perc)) / 100).toFixed(2);
         //$('#<%=lblGroup1NetCommsn.ClientID%>').html(Group1Commsn);

         if (chk_override != 'y')
         $('#<%=lblGroup1NetCommsn.ClientID%>').autoNumeric('set', Group1Commsn);
         negative(Group1Commsn)

         //Other1 Net Commission Based on $ or %

         var hidOther1Perc = $('#<%=hidOther1Perc.ClientID%>').val();
         var hidOther1Doll = $('#<%=hidOther1Doll.ClientID%>').val();
         parseFloat($('#<%=txtOtherPercPerc.ClientID%>').autoNumeric('init'));
         var txtOtherPercPerc = parseFloat($('#<%=txtOtherPercPerc.ClientID%>').autoNumeric('get'));
         if (isNaN(txtOtherPercPerc)) txtOtherPercPerc = 0.00
         negative(txtOtherPercPerc);
         var OtherCommsn = "0";
         if (hidOther1Perc.length > 0) {
             OtherCommsn = parseFloat((parseFloat(OtherPerGrossRecptsamusementtaxfee) * parseFloat(txtOtherPercPerc)) / 100).toFixed(2);
             if (isNaN(OtherCommsn)) OtherCommsn = 0.00
             //$('#<%=lblOtherPercNetCommsn.ClientID%>').html(OtherCommsn);
             $('#<%=lblOtherPercNetCommsn.ClientID%>').autoNumeric('set', OtherCommsn);
             negative(OtherCommsn)
         }
         else if (hidOther1Doll.length > 0) {
             var OtherCommsn = parseFloat(parseFloat(txtOtherPercTcktSold) * parseFloat(txtOtherPercPerc)).toFixed(2);
             if (isNaN(OtherCommsn)) OtherCommsn = 0.00
             //$('#<%=lblOtherPercNetCommsn.ClientID%>').html(OtherCommsn);
                $('#<%=lblOtherPercNetCommsn.ClientID%>').autoNumeric('set', OtherCommsn);
                negative(OtherCommsn)
            }
         //*****************************************************************
        parseFloat($('#<%=txtOtherDollPerc.ClientID%>').autoNumeric('init'));
         var txtOtherDollPerc = parseFloat($('#<%=txtOtherDollPerc.ClientID%>').autoNumeric('get'));

         if (isNaN(txtOtherDollPerc)) txtOtherDollPerc = 0.00
         negative(txtOtherDollPerc)
         var hidOther2Perc = $('#<%=hidOther2Perc.ClientID%>').val();
         var hidOther2Doll = $('#<%=hidOther2Doll.ClientID%>').val();

         var OtherDollCommsn = "0";
         if (hidOther2Perc.length > 0) {
             OtherDollCommsn = parseFloat((parseFloat(OtherDollGrossRecptsamusementtaxfee) * parseFloat(txtOtherDollPerc)) / 100).toFixed(2);
             if (isNaN(OtherDollCommsn)) OtherDollCommsn = 0.00
             // $('#<%=lblOOtherDollNetCommsn.ClientID%>').html(OtherDollCommsn);
             $('#<%=lblOOtherDollNetCommsn.ClientID%>').autoNumeric('set', OtherDollCommsn);
             negative(OtherDollCommsn)
         }
         else if (hidOther2Doll.length > 0) {
             OtherDollCommsn = parseFloat(parseFloat(txtOtherDollTcktSold) * parseFloat(txtOtherDollPerc)).toFixed(2);
             if (isNaN(OtherDollCommsn)) OtherDollCommsn = 0.00
             //$('#<%=lblOtherPercNetCommsn.ClientID%>').html(OtherCommsn);
                $('#<%=lblOOtherDollNetCommsn.ClientID%>').autoNumeric('set', OtherDollCommsn);
                negative(OtherDollCommsn)
            }
         //*****************************************************************sil
         //debugger;
         parseFloat($('#<%=txtOther3Perc.ClientID%>').autoNumeric('init'));
         var txtOther3Perc = parseFloat($('#<%=txtOther3Perc.ClientID%>').autoNumeric('get'));

         if (isNaN(txtOther3Perc)) txtOther3Perc = 0.00
         negative(txtOther3Perc)
         var hidOther3Perc = $('#<%=hidOther3Perc.ClientID%>').val();
         var hidOther3Doll = $('#<%=hidOther3Doll.ClientID%>').val();

         var Other3Commsn = "0";
         if (hidOther3Perc.length > 0) {
             Other3Commsn = parseFloat((parseFloat(Other3GrossRecptsamusementtaxfee) * parseFloat(txtOther3Perc)) / 100).toFixed(2);
             if (isNaN(Other3Commsn)) Other3Commsn = 0.00
             $('#<%=lblOOther3NetCommsn.ClientID%>').autoNumeric('set', Other3Commsn);
             negative(Other3Commsn)
         }
         else if (hidOther3Doll.length > 0) {
             Other3Commsn = parseFloat(parseFloat(txtOther3TcktSold) * parseFloat(txtOther3Perc)).toFixed(2);
             if (isNaN(Other3Commsn)) Other3Commsn = 0.00
             $('#<%=lblOOther3NetCommsn.ClientID%>').autoNumeric('set', Other3Commsn);
             negative(Other3Commsn)
         }

         //*****************************************************************sil
         parseFloat($('#<%=txtOther4Perc.ClientID%>').autoNumeric('init'));
         var txtOther4Perc = parseFloat($('#<%=txtOther4Perc.ClientID%>').autoNumeric('get'));

         if (isNaN(txtOther4Perc)) txtOther4Perc = 0.00
         negative(txtOther4Perc)
         var hidOther4Perc = $('#<%=hidOther4Perc.ClientID%>').val();
         var hidOther4Doll = $('#<%=hidOther4Doll.ClientID%>').val();

         var Other4Commsn = "0";
         if (hidOther4Perc.length > 0) {
             Other4Commsn = parseFloat((parseFloat(Other4GrossRecptsamusementtaxfee) * parseFloat(txtOther4Perc)) / 100).toFixed(2);
             if (isNaN(Other4Commsn)) Other4Commsn = 0.00
             $('#<%=lblOOther4NetCommsn.ClientID%>').autoNumeric('set', Other4Commsn);
             negative(Other4Commsn)
         }
         else if (hidOther4Doll.length > 0) {
             Other4Commsn = parseFloat(parseFloat(txtOther4TcktSold) * parseFloat(txtOther4Perc)).toFixed(2);
             if (isNaN(Other4Commsn)) Other4Commsn = 0.00
             $('#<%=lblOOther4NetCommsn.ClientID%>').autoNumeric('set', Other4Commsn);
             negative(Other4Commsn)
         }

         //*****************************************************************sil
         parseFloat($('#<%=txtOther5Perc.ClientID%>').autoNumeric('init'));
         var txtOther5Perc = parseFloat($('#<%=txtOther5Perc.ClientID%>').autoNumeric('get'));

         if (isNaN(txtOther5Perc)) txtOther5Perc = 0.00
         negative(txtOther5Perc)
         var hidOther5Perc = $('#<%=hidOther5Perc.ClientID%>').val();
         var hidOther5Doll = $('#<%=hidOther5Doll.ClientID%>').val();

         var Other5Commsn = "0";
         if (hidOther5Perc.length > 0) {
             Other5Commsn = parseFloat((parseFloat(Other5GrossRecptsamusementtaxfee) * parseFloat(txtOther5Perc)) / 100).toFixed(2);
             if (isNaN(Other5Commsn)) Other4Commsn = 0.00
             $('#<%=lblOOther5NetCommsn.ClientID%>').autoNumeric('set', Other5Commsn);
             negative(Other5Commsn)
         }
         else if (hidOther5Doll.length > 0) {
             Other5Commsn = parseFloat(parseFloat(txtOther5TcktSold) * parseFloat(txtOther5Perc)).toFixed(2);
             if (isNaN(Other5Commsn)) Other5Commsn = 0.00
             $('#<%=lblOOther5NetCommsn.ClientID%>').autoNumeric('set', Other5Commsn);
             negative(Other5Commsn)
         }
         var other_netcomm_total = parseFloat(OtherCommsn) + parseFloat(OtherDollCommsn) + parseFloat(Other3Commsn) + parseFloat(Other4Commsn) + parseFloat(Other5Commsn);
         //**************************************************Net Commission Total**************************************************************
        var totalNetCommission = parseFloat((parseFloat(subsNetCommsn) + parseFloat(PhoneNetCommsn) + parseFloat(InternetCommsn)
            + parseFloat(CreditCommsn) + parseFloat(RemoteCommsn) + parseFloat(SingletixCommsn) + parseFloat(GroupCommsn) + parseFloat(Group1Commsn) + parseFloat(OtherCommsn) + parseFloat(OtherDollCommsn) + parseFloat(Other3Commsn) + parseFloat(Other4Commsn) + parseFloat(Other5Commsn) + parseFloat(tax2netcomm2))).toFixed(2);

        if (isNaN(totalNetCommission)) totalNetCommission = 0.00
         // $('#<%=lblTotalNetCommsn.ClientID%>').html(totalNetCommission);
         $('#<%=lblTotalNetCommsn.ClientID%>').autoNumeric('set', totalNetCommission);
         negative(totalNetCommission)
         //**********************************************Tax facility fee Commisn**************************************************************
         parseFloat($('#<%=txtTaxFacilityfeecoms.ClientID%>').autoNumeric('init'));
         var txtTaxFacilityfeecoms = parseFloat($('#<%=txtTaxFacilityfeecoms.ClientID%>').autoNumeric('get'));
         if (isNaN(txtTaxFacilityfeecoms)) txtTaxFacilityfeecoms = 0.00
         negative(txtTaxFacilityfeecoms)

         var substaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee1) + parseFloat(subsamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(substaxfacltyfeecomssn)) substaxfacltyfeecomssn = 0.00
         // $('#<%=lblSubsTaxfacltycomsn.ClientID%>').html(substaxfacltyfeecomssn);
         $('#<%=lblSubsTaxfacltycomsn.ClientID%>').autoNumeric('set', substaxfacltyfeecomssn);
         negative(substaxfacltyfeecomssn)

         var Phonetaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee2) + parseFloat(Phoneamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Phonetaxfacltyfeecomssn)) Phonetaxfacltyfeecomssn = 0.00
         //$('#<%=lblTaxfacltyfeecmssn.ClientID%>').html(Phonetaxfacltyfeecomssn);
         $('#<%=lblTaxfacltyfeecmssn.ClientID%>').autoNumeric('set', Phonetaxfacltyfeecomssn);

         var Internettaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee3) + parseFloat(Internetamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Internettaxfacltyfeecomssn)) Internettaxfacltyfeecomssn = 0.00
         // $('#<%=lblInternetFacltyfeecmssn.ClientID%>').html(Internettaxfacltyfeecomssn);
         $('#<%=lblInternetFacltyfeecmssn.ClientID%>').autoNumeric('set', Internettaxfacltyfeecomssn);
         negative(Internettaxfacltyfeecomssn)

         var Credittaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee4) + parseFloat(Creditamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Credittaxfacltyfeecomssn)) Credittaxfacltyfeecomssn = 0.00
         //$('#<%=lblCreditTaxFacltyfeecmssn.ClientID%>').html(Credittaxfacltyfeecomssn);
         $('#<%=lblCreditTaxFacltyfeecmssn.ClientID%>').autoNumeric('set', Credittaxfacltyfeecomssn);
         negative(Credittaxfacltyfeecomssn)

         var Remotetaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee5) + parseFloat(Remoteamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Remotetaxfacltyfeecomssn)) Remotetaxfacltyfeecomssn = 0.00
         //$('#<%=lblRemoteTaxFacltyfeecmssn.ClientID%>').html(Remotetaxfacltyfeecomssn);
         $('#<%=lblRemoteTaxFacltyfeecmssn.ClientID%>').autoNumeric('set', Remotetaxfacltyfeecomssn);
         negative(Remotetaxfacltyfeecomssn)

         var Singletaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee6) + parseFloat(SingleTixamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Singletaxfacltyfeecomssn)) Singletaxfacltyfeecomssn = 0.00
         //$('#<%=lblSingletixtaxfacltyfeecmssn.ClientID%>').html(Singletaxfacltyfeecomssn);
         $('#<%=lblSingletixtaxfacltyfeecmssn.ClientID%>').autoNumeric('set', Singletaxfacltyfeecomssn);
         negative(Singletaxfacltyfeecomssn)

         var Grouptaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee7) + parseFloat(Groupamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Grouptaxfacltyfeecomssn)) Grouptaxfacltyfeecomssn = 0.00
         // $('#<%=lblGroupfeecmssn.ClientID%>').html(Grouptaxfacltyfeecomssn);
         $('#<%=lblGroupfeecmssn.ClientID%>').autoNumeric('set', Grouptaxfacltyfeecomssn);
         negative(Grouptaxfacltyfeecomssn)

         var Group1taxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee8) + parseFloat(Group1amusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Group1taxfacltyfeecomssn)) Group1taxfacltyfeecomssn = 0.00
         //$('#<%=lblGroup1facltyfeecmssn.ClientID%>').html(Group1taxfacltyfeecomssn);
         $('#<%=lblGroup1facltyfeecmssn.ClientID%>').autoNumeric('set', Group1taxfacltyfeecomssn);
         negative(Group1taxfacltyfeecomssn)

         var OtherPertaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee9) + parseFloat(OtherPercamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(OtherPertaxfacltyfeecomssn)) OtherPertaxfacltyfeecomssn = 0.00
         //$('#<%=lblOtherPercfacltyfeecmssn.ClientID%>').html(OtherPertaxfacltyfeecomssn);
         $('#<%=lblOtherPercfacltyfeecmssn.ClientID%>').autoNumeric('set', OtherPertaxfacltyfeecomssn);
         negative(OtherPertaxfacltyfeecomssn)

         var OtherDolltaxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee10) + parseFloat(OtherDollcamusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(OtherDolltaxfacltyfeecomssn)) OtherDolltaxfacltyfeecomssn = 0.00
         // $('#<%=lblOtherDollfacltyfeecmssn.ClientID%>').html(OtherDolltaxfacltyfeecomssn);
         $('#<%=lblOtherDollfacltyfeecmssn.ClientID%>').autoNumeric('set', OtherDolltaxfacltyfeecomssn);
         negative(OtherDolltaxfacltyfeecomssn)


         var Other3taxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee11) + parseFloat(Other3amusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Other3taxfacltyfeecomssn)) Other3taxfacltyfeecomssn = 0.00
         // $('#<%=lblOtherDollfacltyfeecmssn.ClientID%>').html(OtherDolltaxfacltyfeecomssn);
         $('#<%=lblOther3facltyfeecmssn.ClientID%>').autoNumeric('set', Other3taxfacltyfeecomssn);
         negative(Other3taxfacltyfeecomssn)

         var Other4taxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee12) + parseFloat(Other4amusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Other4taxfacltyfeecomssn)) Other4taxfacltyfeecomssn = 0.00
         // $('#<%=lblOtherDollfacltyfeecmssn.ClientID%>').html(OtherDolltaxfacltyfeecomssn);
         $('#<%=lblOther4facltyfeecmssn.ClientID%>').autoNumeric('set', Other4taxfacltyfeecomssn);
         negative(Other4taxfacltyfeecomssn)


         var Other5taxfacltyfeecomssn = parseFloat(((parseFloat(susfacltyfee13) + parseFloat(Other5amusementtax)) * txtTaxFacilityfeecoms) / 100).toFixed(2);
         if (isNaN(Other5taxfacltyfeecomssn)) Other5taxfacltyfeecomssn = 0.00
         // $('#<%=lblOtherDollfacltyfeecmssn.ClientID%>').html(OtherDolltaxfacltyfeecomssn);
         $('#<%=lblOther5facltyfeecmssn.ClientID%>').autoNumeric('set', Other5taxfacltyfeecomssn);
         negative(Other5taxfacltyfeecomssn)

         var other_ffcomm_tot = parseFloat(OtherPertaxfacltyfeecomssn) + parseFloat(OtherDolltaxfacltyfeecomssn) + +parseFloat(Other3taxfacltyfeecomssn) + +parseFloat(Other4taxfacltyfeecomssn) + +parseFloat(Other5taxfacltyfeecomssn);
         //******************************************Total tax facility fee commsn*******************************
         var totaltaxfacltyfeecomsn = parseFloat((parseFloat(substaxfacltyfeecomssn) + parseFloat(Phonetaxfacltyfeecomssn) + parseFloat(Internettaxfacltyfeecomssn) +
             parseFloat(Credittaxfacltyfeecomssn) + parseFloat(Remotetaxfacltyfeecomssn) + parseFloat(Singletaxfacltyfeecomssn) + parseFloat(Grouptaxfacltyfeecomssn) + parseFloat(Group1taxfacltyfeecomssn) +
             parseFloat(OtherPertaxfacltyfeecomssn) + parseFloat(OtherDolltaxfacltyfeecomssn) + parseFloat(Other3taxfacltyfeecomssn) + parseFloat(Other4taxfacltyfeecomssn) + parseFloat(Other5taxfacltyfeecomssn))).toFixed(2);

         //$('#<%=lblTotalfacltyfeecmssn.ClientID%>').html(totaltaxfacltyfeecomsn);
         $('#<%=lblTotalfacltyfeecmssn.ClientID%>').autoNumeric('set', totaltaxfacltyfeecomsn);
         negative(totaltaxfacltyfeecomsn)

         var total = parseFloat(parseFloat(totalNetCommission) + parseFloat(totaltaxfacltyfeecomsn)).toFixed(2);
         if (isNaN(total)) total = 0.00
         // $('#<%=lblTotalcms.ClientID%>').html(total);
         $('#<%=lblTotalcms.ClientID%>').autoNumeric('set', total);
         negative(total)

         //************************************************Cross check*****************************************************************

         var txtPaidAttendance = parseFloat($('#<%=txtPaidAttendance.ClientID%>').val());
         if (isNaN(txtPaidAttendance)) txtPaidAttendance = 0.00
         negative(txtPaidAttendance)

         var txtComps = parseFloat($('#<%=txtComps.ClientID%>').val());
            if (isNaN(txtComps)) txtComps = 0.00
            negative(txtComps)

            var Crosschecksold = parseFloat(parseFloat(totalTicketSold) - parseFloat(txtPaidAttendance)).toFixed(2);
            if (isNaN(Crosschecksold)) Crosschecksold = 0.00
         // $('#<%=lblCrossCheckSold.ClientID%>').html(Crosschecksold);
         $('#<%=lblCrossCheckSold.ClientID%>').autoNumeric('set', Crosschecksold);
         negative(Crosschecksold)

         parseFloat($('#<%=txtgrossales.ClientID%>').autoNumeric('init'));
         var txtgrossales = parseFloat($('#<%=txtgrossales.ClientID%>').autoNumeric('get'));
         if (isNaN(txtgrossales)) txtgrossales = 0.00
         negative(txtgrossales)

         var CrosscheckReceipts = parseFloat(parseFloat(totalGrossReceipts) - parseFloat(txtgrossales)).toFixed(2);
         if (isNaN(CrosscheckReceipts)) CrosscheckReceipts = 0.00
         // $('#<%=lblCrossCheckreceipts.ClientID%>').html(CrosscheckReceipts);
         $('#<%=lblCrossCheckreceipts.ClientID%>').autoNumeric('set', CrosscheckReceipts);
         negative(CrosscheckReceipts);

         var a = $("#dialog-confirm");
         if (parseFloat(Crosschecksold) != 0 || parseFloat(CrosscheckReceipts) != 0) {
             a[0].innerHTML = "<P><SPAN style='MARGIN: 0px 7px 20px 0px; FLOAT: left' class='ui-icon ui-icon-alert'></SPAN>Cross Check not correct!</P>";
             btn1txt = "Ignore"; btn2txt = "Rectify";
         }
         else {
             a[0].innerHTML = "<P><SPAN style='MARGIN: 0px 7px 20px 0px; FLOAT: left' class='ui-icon ui-icon-alert'></SPAN>Do you want to submit the data? </P>";
             btn1txt = "Yes"; btn2txt = "No";
         }

         //************************************************************NAGBOR******************************************************
         //var exclude_nagbour_others = parseFloat(exclude_Others_tax1) + parseFloat(excludeothers_saleslesstax) + parseFloat(tot_other_ff) + parseFloat(other_netcomm_total) + parseFloat(other_ffcomm_tot);
         //var exclude_nagbour_others = parseFloat(exclude_Others_tax1) + parseFloat(excludeothers_saleslesstax) + parseFloat(other_netcomm_total);
         var exclude_nagbour_others = parseFloat(excludeothers_saleslesstax) + parseFloat(other_netcomm_total) + parseFloat(other_ffcomm_tot);
         totalfacilityfee = parseFloat(totalfacilityfee) - parseFloat(tot_other_ff);
         TotalAmusementfee = parseFloat(TotalAmusementfee) - parseFloat(excludeothers_saleslesstax);
         totalamusementtax = parseFloat(totalamusementtax) - parseFloat(exclude_Others_tax1);
         // totalNetCommission = parseFloat(totalNetCommission) - parseFloat(other_netcomm_total);
         totalNetCommission = parseFloat(totalNetCommission);
         totaltaxfacltyfeecomsn = parseFloat(totaltaxfacltyfeecomsn) - parseFloat(other_ffcomm_tot);
         // var nagboramt = parseFloat(parseFloat(TotalAmusementfee) - parseFloat(totalNetCommission) - parseFloat(exclude_nagbour_others) - parseFloat(less_ff_tax_for_nagbour)).toFixed(2);
         //var nagboramt = parseFloat(parseFloat(TotalAmusementfee) - parseFloat(totalNetCommission) - parseFloat(exclude_nagbour_others)).toFixed(2);
         var nagboramt = parseFloat(parseFloat(totalGrossReceipts) - parseFloat(totalfacilityfee) - parseFloat(totalamusementtax) - parseFloat(totalNetCommission) - parseFloat(totaltaxfacltyfeecomsn)).toFixed(2);
         if (isNaN(nagboramt)) nagboramt = 0.00
         //$('#<%=lblNagbor.ClientID%>').html(nagboramt);
         $('#<%=lblNagbor.ClientID%>').autoNumeric('set', nagboramt);
         negative(nagboramt)




     }
        function negative(val) {
            if (parseFloat(val) < 0) {
                $('#<%=hidfval.ClientID%>').val(val);
            }
        }
    </script>
    <script type="text/javascript">
        var ddlperindex = 0;
        
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
           
            init()
            Subscriptioncalc();
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
            if ($('#<%=ddlPerformance.ClientID%>')[0] != undefined) {
                if (this.id != $('#<%=ddlPerformance.ClientID%>')[0].id)
                    $('#hdn_modify_status').val("1");
                else
                    $('#hdn_modify_status').val("0");
            }
        });
    </script>
    <script type="text/javascript">

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


   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div align="center"> <asp:Label ID="lbl_bo" runat="server" Visible="false" Text="Please Enter Engagement Schedule First"
                ForeColor="Red"></asp:Label></div> 
            <center>
                <%--  <div runat="server" id="div_bo" visible="false">
                //under development
            </div>--%>
                <asp:HiddenField ID="hdn_engagementid" runat="server" />
                <asp:HiddenField ID="hdn_schedulecount" runat="server" />
                <asp:HiddenField ID="hdnboscheduleidlist" runat="server" />
                        <asp:HiddenField ID="hdninitial_dropcount" runat="server" />
                <asp:HiddenField ID="hdninitial_paidattn" runat="server" />
                <asp:HiddenField ID="hdn_deal_ff_IO" runat="server" />
            <asp:HiddenField ID="hdn_deal_ff_unit" runat="server" />
            <asp:HiddenField ID="hdn_deal_ff_amt" runat="server" />
                 <asp:HiddenField ID="hdn_checkoverride" runat="server" />
                
                 <asp:HiddenField ID="hdn_deal_tax_ptg_include" runat="server" />
            <asp:HiddenField ID="hdn_dealtaxptg_ff" runat="server" />

            </center>

            <div id="diveboxofc" runat="server">

                <table width="100%" cellpadding="2" cellspacing="0">

                   
                    <tr>
                        <td colspan="10" align="left">
                            <table width ="1400px">
                                <tr>
                                    <td style="width:auto; padding-right:2px;">
                                                    <asp:TextBox ID="txtPerformance" runat="server" TabIndex="1"></asp:TextBox>
                                                </td>
                                </tr>
                                <tr>

                                    <td width="200px" style="padding-left:45px">
                                        <table>
                                            <tr>
                                                
                                                <td>
                                                    <asp:DropDownList ID="ddlPerformance" SkinID="ddlmedium1" AutoPostBack="true" runat="server"
                                                        OnChange="if(!chk_changes(this)) return false;"
                                                        OnSelectedIndexChanged="ddlPerformance_SelectedIndexChanged" TabIndex="1">
                                                        <asp:ListItem Text="-Select-" Value="0">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Panel ID="pnlcolumn" BackColor="#F0F0F0" BorderStyle="Solid" runat="server"  Height="250px" ScrollBars="Auto"
                                                        Width="320px">
                                                        <asp:CheckBoxList runat="server" ID="chklstPerformance" TextAlign="Right" OnDataBound="chklstPerformance_DataBound"  RepeatDirection="Vertical">
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
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align:top;text-align:left;font-weight:bold">
                                        Override 
                                        &nbsp;
                                        <asp:CheckBox ID="chkboxOverride" AutoPostBack="true" OnCheckedChanged="chkboxOverride_CheckedChanged" runat="server" />

                                    </td>

                                    <td align="left" runat="server" id="tdcopyfrom" style="font-weight: normal; font-size: initial;" width="150px">
                                        <a href="#" class="lnkcopyfrom">
                                            <asp:Label ID="lblcopyfrom" runat="server" Text="Copy From" TabIndex="2"></asp:Label></a>
                                        <asp:Panel BackColor="#cccccc" ID="pnlperf" runat="server" Height="95px" ScrollBars="Auto"
                                            Width="200px">
                                            <asp:RadioButtonList ID="rbtnlstperformance" runat="server" OnSelectedIndexChanged="rbtnlstperformance_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:RadioButtonList>
                                        </asp:Panel>

                                        <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1"
                                            runat="server"
                                            TargetControlID="lblcopyfrom"
                                            PopupControlID="pnlperf"
                                            PopupPosition="Right"
                                            OffsetX="6"
                                            PopDelay="25">
                                        </ajaxToolkit:HoverMenuExtender>

                                    </td>
                                    
                                    <td class="chosen-rtl" width="150px">
                                        <asp:Label ID="lblgrosssales" runat="server" Text="Gross Sales"></asp:Label>
                                    </td>
                                      <td style="text-align: left" valign="middle" width="150px">&nbsp;&nbsp;<asp:TextBox onchange="Subscriptioncalc();"
                                        CssClass="Dollar" Width="113px" ID="txtgrossales"  style="text-align: right" runat="server" TabIndex="3"></asp:TextBox>
                                    </td>
                                    <td class="chosen-rtl" width="150px">
                                        <asp:Label ID="lblTax1" runat="server" Text="Tax 1"></asp:Label>
                                    </td>
                                    <td style="text-align: left">&nbsp;&nbsp;<asp:Label Width="80px" CssClass="Percentage1 txt_bold"
                                        ID="txtTax1" runat="server"></asp:Label></td>
                                </tr>
                                <tr>

                                    <td colspan="1" style="padding-left:45px; padding-right:20px;"><asp:Label ID="lblScheduleDate" runat="server" CssClass="txt_bold"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblScheduleTime" runat="server" CssClass="txt_bold"></asp:Label></td>
                                    
                                    <td class="chosen-rtl" width="150px">
                                        <asp:Label ID="lblDropcount" runat="server" style="text-align: right" Text="Drop Count  "></asp:Label>
                                    </td>
                                    <td style="text-align: left" width="150px">&nbsp;
                                        <asp:TextBox ID="txtDropcount" runat="server"  style="text-align: right" CssClass="int" Width="113px" TabIndex="4"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fltx" runat="server" FilterType="Custom,Numbers" TargetControlID="txtDropcount">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                    <td class="chosen-rtl" width="150px">
                                        <asp:Label ID="lblTaxamountover" runat="server" Text="Tax Amount Over  "></asp:Label>
                                    </td>
                                    <td style="text-align: left" align="left">&nbsp;
                                        <asp:Label ID="txtTaxAmountOver" runat="server" CssClass="Dollar txt_bold" Width="80px"></asp:Label>
                                    </td>
                                </tr>
                               
                                <tr>

                                    <td colspan="1" style="padding-left:45px; padding-right:20px;">
                                        <asp:Label ID="lblCapacity" runat="server" Text="Capacity"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblCapacityValue" runat="server" CssClass="txt_bold"></asp:Label></td>
                                    
                                                                        
                                    <td class="chosen-rtl" width="150px">
                                        <asp:Label ID="lblPaidAttendance" runat="server" Text="Paid Attendance"></asp:Label>
                                    </td>
                                    <td style="text-align: left" width="150px">&nbsp;&nbsp;<asp:TextBox ID="txtPaidAttendance"  style="text-align: right" runat="server" CssClass="int" onchange="Subscriptioncalc();" Width="113px" TabIndex="5"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtPaidAttendance_FilteredTextBoxExtender"   runat="server" FilterType="Custom,Numbers" TargetControlID="txtPaidAttendance">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                    <td class="chosen-rtl" width="150px">Tax/FF Commission</td>
                                    <td style="text-align: left">&nbsp;&nbsp;<asp:Label CssClass="Percentage txt_bold" Width="50px"
                                        ID="txtTaxFacilityfeecoms" runat="server"></asp:Label></td>
                                </tr>
                                 <tr>

                                    <td class="chosen-rtl" width="250px">
                                        &nbsp;</td>
                                   
<%--                                     <td class="chosen-rtl" width="150px">&nbsp;</td>--%>
                                   
                                    <td class="chosen-rtl" width="150px">
                                        <asp:Label ID="lblComps" runat="server" Text="Comps"></asp:Label>
                                    </td>
                                    <td style="text-align: left" width="150px">&nbsp;&nbsp;<asp:TextBox ID="txtComps" runat="server" CssClass="int" Width="113px" TabIndex="6"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtComps_FilteredTextBoxExtender" runat="server" FilterType="Custom,Numbers" TargetControlID="txtComps">
                                        </ajaxToolkit:FilteredTextBoxExtender>

                                    </td>
                                    <td class="chosen-rtl" width="150px">
                                        FF on Each Ticket
                                    </td>
                                    <td style="text-align: left;color: #000">&nbsp;&nbsp;<b><asp:Label ID="txtFacilityfeeoneachticket" runat="server" Width="50px"></asp:Label></b>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="chosen-rtl" width="250px">&nbsp;</td>
                                    <td class="chosen-rtl" width="150px">&nbsp;</td>
                                    <td class="chosen-rtl" width="150px">&nbsp;</td>
                                    <td style="text-align: left" width="150px">&nbsp;</td>
                                    <td class="chosen-rtl" width="150px">&nbsp;</td>
                                    <td style="text-align: left;color: #000">&nbsp;</td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" class ="heading" >Sales</td>
                        <td colspan="5" class="lineright" ></td>
                        <td align="left" style="padding-left: 10px;" class ="heading" >Commissions</td>
                        <td colspan="3" ></td>
                    </tr>
                    
                    <tr class="gridviewheader" >
                        <th align="center"></th>
                        <th align="center">Tickets Sold</th>
                        <th align="center">Gross Receipts</th>
                        <th align="center">Facility Fee</th>
                        <th align="center">Tax1</th>
                        <th align="center"  style="width: 150px; padding-right: 10px;" class="lineright">Sales
                            Less Tax1 & Facility Fee</th>
                        <th align="left" style="padding-left: 10px; width: 100px"></th>
                        <th align="center">Net Commission</th>
                        <th align="center" style="width: 70px">Tax/Facility Fee Commission</th>
                        <th align="center">Total</th>
                    </tr>
                    <tr style="margin-bottom: 30px">
                        <td class="chosen-rtl">
                            <asp:Label ID="lblSubscription" runat="server" Text="Subscription"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;<asp:TextBox Width="100px"  style="text-align: right" CssClass="int" onchange="Subscriptioncalc();"
                            ID="txtSubscriptionsold" runat="server" TabIndex="7"></asp:TextBox></td>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtSubscriptionsold"
                            runat="server" FilterType="Custom,Numbers">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" onchange="Subscriptioncalc();" ID="txtSubscriptionreceipts"
                                runat="server" TabIndex="8"></asp:TextBox></td>
                        <td>
                            
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblSubscriptionfacilityfee"
                                runat="server" TabIndex="8"></asp:TextBox></td>


                           <%-- <asp:Label Width="100px" CssClass="Dollar" data-a-sign="$ " ID="lblSubscriptionfacilityfee"
                                runat="server"></asp:Label></td>--%>
                       
                        <td>
                                                        
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblSubscriptionAmusementtax"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblSubscriptionAmusementtax" runat="server"></asp:Label>--%>

                        </td>
                        <td  style="width: 150px; padding-right: 10px;" class="lineright">
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblSubsSalslessamusementtax" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label Width="100px" CssClass="Percentage" ID="txtSubsSalesPerc" runat="server"></asp:Label>

                        </td>
                        <td style="padding-left: 10px;">
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblSubsSalesNetcommission"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                            <%--<asp:Label Width="100px" CssClass="Dollar" ID="lblSubsSalesNetcommission" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblSubsTaxfacltycomsn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblSubsTaxfacltycomsn" runat="server"></asp:Label>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                        <td>&nbsp;&nbsp;<asp:TextBox Width="100px"  style="text-align: right" CssClass="int" onchange="Subscriptioncalc();"
                            ID="txtPhoneTcktSold" runat="server" TabIndex="9"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox Width="100px"   style="text-align: right" CssClass="Dollar" onchange="Subscriptioncalc();" ID="txtPhoneGrossReceipts"
                                runat="server" TabIndex="10"></asp:TextBox></td>
                        <td>
                             <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblPhoneFacilityfee"
                                runat="server" TabIndex="8"></asp:TextBox>
<%--                            <asp:Label Width="100px" CssClass="Dollar" ID="lblPhoneFacilityfee" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblPhoneAmusementtax"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblPhoneAmusementtax" runat="server"></asp:Label>--%>
                        </td>
                        <td  style="width: 150px; padding-right: 10px;" class="lineright">
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblPhoneSaleslessamusemntfee" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label Width="100px" CssClass="Percentage" ID="txtPhonePerc" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px;">
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblPhoneNetCommsn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                            <%--<asp:Label Width="100px" CssClass="Dollar" ID="lblPhoneNetCommsn" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblTaxfacltyfeecmssn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
<%--                            <asp:Label Width="100px" CssClass="Dollar" ID="lblTaxfacltyfeecmssn" runat="server"></asp:Label>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <asp:Label ID="lblInternet" runat="server" Text="Internet"></asp:Label></td>
                        <td>&nbsp;&nbsp;<asp:TextBox Width="100px"  style="text-align: right" CssClass="int" onchange="Subscriptioncalc();"
                            ID="txtInternetTcktSold" runat="server" TabIndex="11"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtInternetTcktSold"
                                runat="server" FilterType="Custom,Numbers">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td>
                            <asp:TextBox Width="100px" CssClass="Dollar"  style="text-align: right" onchange="Subscriptioncalc();" ID="txtInternetGrossRecpt"
                                runat="server" TabIndex="12"></asp:TextBox></td>
                        <td>
                                  <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblInternetFacltyFee"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblInternetFacltyFee" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblInternetAmusemntTax"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblInternetAmusemntTax" runat="server"></asp:Label>--%>
                        </td>
                        <td  style="width: 150px; padding-right: 10px;" class="lineright">
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblInternetSaleslesAmusmntfee" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label Width="100px" CssClass="Percentage" ID="txtInternetPerc" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px;">
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblInternetNetCommsn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblInternetNetCommsn" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblInternetFacltyfeecmssn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblInternetFacltyfeecmssn" runat="server"></asp:Label>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <asp:Label ID="lblCreditcard" runat="server" Text="Credit Card"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;<asp:TextBox Width="100px"  style="text-align: right" CssClass="int" onchange="Subscriptioncalc();"
                            ID="txtCreditcrdTcktSold" runat="server" TabIndex="13"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox Width="100px" CssClass="Dollar"  style="text-align: right" onchange="Subscriptioncalc();" ID="txtCreditGrossReceipts"
                                runat="server" TabIndex="14"></asp:TextBox></td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblCreditfacilityfee"
                                runat="server" TabIndex="8"></asp:TextBox>
                            <%--<asp:Label Width="100px" CssClass="Dollar" ID="lblCreditfacilityfee" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                            
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblCreditAmusementtax"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblCreditAmusementtax" runat="server"></asp:Label>--%>
                        </td>
                        <td  style="width: 150px; padding-right: 10px;" class="lineright">
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblCreditSaleslesAmusementfee" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label Width="100px" CssClass="Percentage" ID="txtCreditPerc" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px;">
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblCreditNetCommsn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                          <%--  <asp:Label Width="100px" CssClass="Dollar" ID="lblCreditNetCommsn" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblCreditTaxFacltyfeecmssn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                         <%--   <asp:Label Width="100px" CssClass="Dollar" ID="lblCreditTaxFacltyfeecmssn" runat="server"></asp:Label>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <asp:Label ID="lblRemoteoutlet" runat="server" Text="Remote/Outlet"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;<asp:TextBox Width="100px"  style="text-align: right" CssClass="int" onchange="Subscriptioncalc();"
                            ID="txtRemoteoutletTcktSold" runat="server" TabIndex="15"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox Width="100px" CssClass="Dollar"  style="text-align: right" onchange="Subscriptioncalc();" ID="txtRemoteGrossReceipts"
                                runat="server" TabIndex="16"></asp:TextBox></td>
                        <td>
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblRemotefacilityfee"
                                runat="server" TabIndex="8"></asp:TextBox>
                            
                            <%--<asp:Label Width="100px" CssClass="Dollar" ID="lblRemotefacilityfee" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblRemoteAmusementtax"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblRemoteAmusementtax" runat="server"></asp:Label>--%>
                        </td>
                        <td  style="width: 150px; padding-right: 10px;" class="lineright">
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblRemoteSaleslesAmusementfee" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label Width="100px" CssClass="Percentage" ID="txtRemotePerc" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px;">
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblRemoteNetCommsn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                         <%--   <asp:Label Width="100px" CssClass="Dollar" ID="lblRemoteNetCommsn" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblRemoteTaxFacltyfeecmssn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblRemoteTaxFacltyfeecmssn" runat="server"></asp:Label>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <asp:Label ID="lblSingletix" runat="server" Text="Single Tix"></asp:Label></td>
                        <td>&nbsp;&nbsp;<asp:TextBox CssClass="int"  style="text-align: right" Width="100px" onchange="Subscriptioncalc();"
                            ID="txtSingletixtcktSold" runat="server" TabIndex="17"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox Width="100px" CssClass="Dollar"  style="text-align: right" onchange="Subscriptioncalc();" ID="txtSingletixtGrossReceipts"
                                runat="server" TabIndex="18"></asp:TextBox></td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblSingletixfacilityfee"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblSingletixfacilityfee" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblSingletixAmusementtax"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblSingletixAmusementtax" runat="server"></asp:Label>--%>
                        </td>
                        <td  style="width: 150px; padding-right: 10px;" class="lineright">
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblSingletixAmusementfee" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label Width="100px" CssClass="Percentage" ID="txtSingletixPerc" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px;">
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblSingletixNetCommsn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                            <%--<asp:Label Width="100px" CssClass="Dollar" ID="lblSingletixNetCommsn" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblSingletixtaxfacltyfeecmssn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblSingletixtaxfacltyfeecmssn" runat="server"></asp:Label>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <asp:Label ID="lblGroup" runat="server" Text="Group 1"></asp:Label></td>
                        <td>&nbsp;&nbsp;<asp:TextBox Width="100px" CssClass="int"  style="text-align: right" onchange="Subscriptioncalc();"
                            ID="txtGroupTicktSold" runat="server" TabIndex="19"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox Width="100px" CssClass="Dollar"  style="text-align: right" onchange="Subscriptioncalc();" ID="txtGroupGrossReceipts"
                                runat="server" TabIndex="20"></asp:TextBox></td>
                        <td>
                             <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblGroupsfacilityfee"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblGroupsfacilityfee" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblGroupAmusementtax"
                                runat="server" TabIndex="8"></asp:TextBox>
                            <%--<asp:Label Width="100px" CssClass="Dollar" ID="lblGroupAmusementtax" runat="server"></asp:Label>--%>
                        </td>
                        <td  style="width: 150px; padding-right: 10px;" class="lineright">
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblGroupAmusementfee" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label Width="100px" CssClass="Percentage" ID="txtGroupPerc" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px;">
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblNetCommsn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                         <%--   <asp:Label Width="100px" CssClass="Dollar" ID="lblNetCommsn" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblGroupfeecmssn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblGroupfeecmssn" runat="server"></asp:Label>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <asp:Label ID="lblGroupgreatrthan" runat="server" Text="Group 2"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;<asp:TextBox Width="100px"  style="text-align: right" CssClass="int" onchange="Subscriptioncalc();"
                            ID="txtGroup1TicktSold" runat="server" TabIndex="21"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" onchange="Subscriptioncalc();" ID="txtGroup1grossReceipts"
                                runat="server" TabIndex="22"></asp:TextBox></td>
                        <td>
                             <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblGroup1facilityfee"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblGroup1facilityfee" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblGroup1Amusementtax"
                                runat="server" TabIndex="8"></asp:TextBox>
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblGroup1Amusementtax" runat="server"></asp:Label>--%>
                        </td>
                        <td  style="width: 150px; padding-right: 10px;" class="lineright">
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblGroup1Amusementfee" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label Width="100px" CssClass="Percentage" ID="txtGroup1Perc" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px;">
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblGroup1NetCommsn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblGroup1NetCommsn" runat="server"></asp:Label>--%>
                        </td>
                        <td>
                                                                                    
                              <asp:TextBox Width="100px"  style="text-align: right" CssClass="Dollar" ReadOnly="true" ID="lblGroup1facltyfeecmssn"
                                runat="server" TabIndex="8"></asp:TextBox>
                           
                           <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblGroup1facltyfeecmssn" runat="server"></asp:Label>--%>
                        </td>
                        <td></td>
                    </tr>
                    
                   <%-- <tr  >
                        <td colspan="12"  >
                               <table >
                                   <tr>
                                       <td colspan="8">
                                            <label for="header1" style="margin-left:40px">Other</label>
                                           &nbsp;<asp:ImageButton ID="lnkbtnAdd" runat="server" ImageUrl="~/Images/plus.png"   Height ="15px" Width ="20px" OnClientClick="ShowDiv();"
                        ToolTip="Add">
                        </asp:ImageButton>
                        <asp:ImageButton ID="lnkbtnDelete" runat="server" ImageUrl="~/Images/minus.png"   Height ="15px" Width ="20px"
                           OnClientClick="HideDiv();" ToolTip="Delete" CausesValidation="false">
                        </asp:ImageButton>
                      <%--  <ajaxToolkit:ConfirmButtonExtender ID="cbedelete" runat="server" ConfirmOnFormSubmit="true"
                        ConfirmText="Are you sure to delete the selected record(s)?" TargetControlID="lnkbtnDelete">
                        </ajaxToolkit:ConfirmButtonExtender>--%>
                           
                                       </td>
                                   </tr>
                    <caption>
                        --%&gt;
                        <tr >
                            <td class="chosen-rtl">
                                <asp:Label ID="lblOtherPerc" runat="server" Text="Other 1"></asp:Label>
                            </td>
                            <td>&nbsp;&nbsp;<asp:TextBox ID="txtOtherPercTcktSold" runat="server" CssClass="int" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="23" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOtherPercGrossReceipts" runat="server" CssClass="Dollar" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="24" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="lblOtherPercfacilityfee" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherPercfacilityfee" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOtherPercAmusementtax" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherPercAmusementtax" runat="server"></asp:Label>--%></td>
                            <td class="lineright" style="width: 150px; padding-right: 10px;">
                                <asp:Label ID="lblOtherPercAmusementfee" runat="server" CssClass="Dollar" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px">
                                <asp:Label ID="txtOtherPercPerc" runat="server" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="lblOtherPercNetCommsn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%--  <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherPercNetCommsn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOtherPercfacltyfeecmssn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%--     
                            <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherPercfacltyfeecmssn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:CheckBox ID="cbother1" runat="server" />
                            </td>
                        </tr>
                        <tr >
                            <td class="chosen-rtl">
                                <asp:Label ID="lblOtherDoll" runat="server" Text="Other 2"></asp:Label>
                            </td>
                            <td>&nbsp;&nbsp;<asp:TextBox ID="txtOtherDollTcktSold" runat="server" CssClass="int" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="25" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOtherDollGrossReceipts" runat="server" CssClass="Dollar" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="26" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="lblOtherDollfacilityfee" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar"  style="text-align: right" ID="lblOtherDollfacilityfee" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOtherDollAmusementtax" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherDollAmusementtax" runat="server"></asp:Label>--%></td>
                            <td class="lineright" style="width: 150px; padding-right: 10px;">
                                <asp:Label ID="lblOtherDollAmusementfee" runat="server" CssClass="Dollar" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px">
                                <asp:Label ID="txtOtherDollPerc" runat="server" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="lblOOtherDollNetCommsn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOOtherDollNetCommsn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOtherDollfacltyfeecmssn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%--   <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherDollfacltyfeecmssn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:CheckBox ID="cbother2" runat="server" />
                            </td>
                        </tr>
                        <tr id="trother3">
                            <td class="chosen-rtl">
                                <asp:Label ID="Label1" runat="server" Text="Other 3"></asp:Label>
                            </td>
                            <td>&nbsp;&nbsp;<asp:TextBox ID="txtOther3TcktSold" runat="server" CssClass="int" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="25" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOther3GrossReceipts" runat="server" CssClass="Dollar" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="26" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="lblOther3facilityfee" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar"  style="text-align: right" ID="lblOtherDollfacilityfee" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOther3Amusementtax" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherDollAmusementtax" runat="server"></asp:Label>--%></td>
                            <td class="lineright" style="width: 150px; padding-right: 10px;">
                                <asp:Label ID="lblOther3Amusementfee" runat="server" CssClass="Dollar" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px">
                                <asp:Label ID="txtOther3Perc" runat="server" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="lblOOther3NetCommsn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOOtherDollNetCommsn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOther3facltyfeecmssn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%--   <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherDollfacltyfeecmssn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:CheckBox ID="cbother3" runat="server" />
                            </td>
                        </tr>
                        <tr id="trother4">
                            <td class="chosen-rtl">
                                <asp:Label ID="Label2" runat="server" Text="Other 4"></asp:Label>
                            </td>
                            <td>&nbsp;&nbsp;<asp:TextBox ID="txtOther4TcktSold" runat="server" CssClass="int" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="25" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOther4GrossReceipts" runat="server" CssClass="Dollar" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="26" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="lblOther4facilityfee" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar"  style="text-align: right" ID="lblOtherDollfacilityfee" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOther4Amusementtax" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherDollAmusementtax" runat="server"></asp:Label>--%></td>
                            <td class="lineright" style="width: 150px; padding-right: 10px;">
                                <asp:Label ID="lblOther4Amusementfee" runat="server" CssClass="Dollar" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px">
                                <asp:Label ID="txtOther4Perc" runat="server" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="lblOOther4NetCommsn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOOtherDollNetCommsn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOther4facltyfeecmssn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%--   <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherDollfacltyfeecmssn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:CheckBox ID="cbother4" runat="server" />
                            </td>
                        </tr>
                        <tr id="trother5">
                            <td class="chosen-rtl">
                                <asp:Label ID="Label3" runat="server" Text="Other 5"></asp:Label>
                            </td>
                            <td>&nbsp;&nbsp;<asp:TextBox ID="txtOther5TcktSold" runat="server" CssClass="int" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="25" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOther5GrossReceipts" runat="server" CssClass="Dollar" onchange="Subscriptioncalc();" style="text-align: right" TabIndex="26" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="lblOther5facilityfee" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar"  style="text-align: right" ID="lblOtherDollfacilityfee" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOther5Amusementtax" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherDollAmusementtax" runat="server"></asp:Label>--%></td>
                            <td class="lineright" style="width: 150px; padding-right: 10px;">
                                <asp:Label ID="lblOther5Amusementfee" runat="server" CssClass="Dollar" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px">
                                <asp:Label ID="txtOther5Perc" runat="server" Width="100px"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="lblOOther5NetCommsn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%-- <asp:Label Width="100px" CssClass="Dollar" ID="lblOOtherDollNetCommsn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:TextBox ID="lblOther5facltyfeecmssn" runat="server" CssClass="Dollar" ReadOnly="true" style="text-align: right" TabIndex="8" Width="100px"></asp:TextBox>
                                <%--   <asp:Label Width="100px" CssClass="Dollar" ID="lblOtherDollfacltyfeecmssn" runat="server"></asp:Label>--%></td>
                            <td>
                                <asp:CheckBox ID="cbother5" runat="server" />
                            </td>
                        </tr>
                    </caption>
                            
                                  
                               
                           <%--</table>--%>




                        
                            <%--</td>
                    </tr>--%>
                   
                    <tr>
                        <td class="chosen-rtl">Tax 2</td>
                        <td  colspan="5" class="lineright">&nbsp;&nbsp;</td>

                        <td>&nbsp;&nbsp;<asp:Label Width="100px" CssClass="Percentage" ID="lbltax2DollPerc"
                            runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;<asp:Label Width="100px" CssClass="Dollar" ID="lbltax2Netcommsn"
                            runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="linebottom" >&nbsp;</td>
                        <td colspan="4" class="linebottom">&nbsp;</td>
                    </tr>
                    <tr style="font-weight: bold;">
                        <td style="text-align: right">
                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label></td>
                        <td>&nbsp;&nbsp;<asp:TextBox Width="100px" ReadOnly="true"  style="text-align: right" CssClass="smallint" ID="txtTotalTcktSold"
                            runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox Width="100px" ReadOnly="true"  style="text-align: right" CssClass="Dollar" ID="txtTotalGrossReceipts"
                                runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Label Width="100px" CssClass="txt_bold Dollar" ID="lblTotalfacilityfee" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label Width="100px" CssClass="txt_bold Dollar" ID="lblTotalAmusementtax" runat="server"></asp:Label>
                        </td>
                        <td  style="width: 150px; padding-right: 10px;">
                            <asp:Label Width="100px" CssClass="txt_bold Dollar" ID="lblTotalAmusementfee" runat="server"></asp:Label>
                        </td>
                        <td style="padding-left: 10px">
                            <%--<asp:TextBox Width="50px" ID="TextBox3" runat="server"></asp:TextBox>--%>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Label Width="100px" CssClass="txt_bold Dollar" ID="lblTotalNetCommsn" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label Width="100px" CssClass="txt_bold Dollar" ID="lblTotalfacltyfeecmssn" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label Width="100px" CssClass="txt_bold Dollar" ID="lblTotalcms" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCrosscheck" runat="server" Text="Cross Check"></asp:Label>
                        </td>
                        <td>&nbsp;<asp:Label ID="lblCrossCheckSold" CssClass="Dollar" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCrossCheckreceipts" CssClass="Dollar" runat="server" Text=""></asp:Label>
                        </td>
                        <td></td>
                        <td></td>
                        <td  style="width: 150px; padding-right: 10px;"></td>
                        <td style="padding-left: 10px"></td>
                        <td style="padding-left: 10px;"></td>
                        <td></td>
                        <td></td>
                    </tr>
                     <tr>
                        <td colspan="10" align="center">

                            <asp:ValidationSummary ID="val_summary_deal" runat="server" Font-Bold="true" ForeColor="Orange"
                                HeaderText="Field validations failed. Please check and correct" DisplayMode="SingleParagraph" />
                            &nbsp;
         <asp:Label ID="lbl_message" runat="server" ForeColor="Orange" Font-Bold="true" />
                            <asp:Label ID="lbl_staticmsg" runat="server" ForeColor="Orange" Font-Bold="true" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="6" >&nbsp;</td>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td colspan="4" style="padding-left:45px">
                            <asp:Label ID="lblNetadjustednagbor" Font-Bold="true" runat="server" Text="Net Adjusted Gross Box Office Receipt(NAGBOR)"></asp:Label>&nbsp;<asp:Label
                                CssClass="txt_bold Dollar" ID="lblNagbor" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </td>
                        <td align="left"></td>
                        <td  style="width: 150px; padding-right: 10px;"></td>
                        <td style="padding-left: 10px"></td>
                        <td style="padding-left: 10px;"></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="6" >&nbsp;</td>
                        <td colspan="4">&nbsp;
                            <asp:HiddenField ID="hidfval" runat="server" />
                            <asp:HiddenField ID="hidFdollar" runat="server" />
                            <asp:HiddenField ID="hidFPerc" runat="server" />
                            <asp:HiddenField ID="hidOther1Doll" runat="server" />
                            <asp:HiddenField ID="hidOther1Perc" runat="server" />
                            <asp:HiddenField ID="hidOther2Doll" runat="server" />
                            <asp:HiddenField ID="hidOther2Perc" runat="server" />
                            <asp:HiddenField ID="hidOther3Doll" runat="server" />
                            <asp:HiddenField ID="hidOther3Perc" runat="server" />
                            <asp:HiddenField ID="hidOther4Doll" runat="server" />
                            <asp:HiddenField ID="hidOther4Perc" runat="server" />
                            <asp:HiddenField ID="hidOther5Doll" runat="server" />
                            <asp:HiddenField ID="hidOther5Perc" runat="server" />
                        </td>
                    </tr>
                   
                </table>

            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

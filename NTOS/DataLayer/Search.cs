using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTOS.DataLayer
{
    public class Search
    {
    }

    public class Schedule
    {
        public string Show { get; set; }
        public string Presenter { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string SettlementDate { get; set; }
        public string Venue { get; set; }
    }

    public class PriceScaleDetail
    {
        public string Seats { get; set; }
        public string Single { get; set; }
        public string Group { get; set; }
        public string Subscription { get; set; }
    }

    public class BoxOfficeDetail
    {
        public string GroupSales { get; set; }
        public string DropCount { get; set; }
        public string PaidAttendance { get; set; }
        public string Comp { get; set; }
        public string TSSubscription { get; set; }
        public string TSPhone { get; set; }
        public string TSInternet { get; set; }
        public string TSCreditCard { get; set; }
        public string TSRemoteOutlet { get; set; }
        public string TSSingleTickets { get; set; }
        public string TSGroup1 { get; set; }
        public string TSGroup2 { get; set; }
        public string GRSubscription { get; set; }
        public string GRPhone { get; set; }
        public string GRInternet { get; set; }
        public string GRCreditCard { get; set; }
        public string GRRemoteOutlet { get; set; }
        public string GRSingleTickets { get; set; }
        public string GRGroup1 { get; set; }
        public string GRGroup2 { get; set; }
    }

    public class DealDetail
    {
        public string Royalty {get ; set ;}
        public string Company {get ; set ;}
        public string Guarantee {get ; set ;}
        public string Presenter {get ; set ;}
        public string Cap {get ; set ;}
        public string Tax1 {get ; set ;}
        public string OnEachTicket {get ; set ;}
        public string Tax2 {get ; set ;}
        public string TaxFacilityFee {get ; set ;}
        public string TaxAmountOver {get ; set ;}
        public string Producer {get ; set ;}
        public string Other1 {get ; set ;}
        //public string Presenter {get ; set ;}
        public string Other2 {get ; set ;}
        public string StarRoyalty {get ; set ;}
        public string Budget {get ; set ;}
        public string Actual {get ; set ;}
        public string Subscription {get ; set ;}
        public string Phone {get ; set ;}
        public string Internet {get ; set ;}
        public string CreditCard {get ; set ;}
        public string Remote {get ; set ;}
        public string SingleTickets {get ; set ;}
        public string Group1 {get ; set ;}
        public string Group2 {get ; set ;}
    }

    public class Discount
    {
        public string Sub { get; set; }
        public string SubTkt { get; set; }
        public string Group { get; set; }
        public string Grptkt { get; set; }
        public string Misc { get; set; }
        public string MiscTkt { get; set; }
    }

    public class ExpenseDetails
    {
        public string BUDAdvertisingGross { get; set; }
        public string BUDLaborCatering { get; set; }
        public string BUDMusicians { get; set; }
        public string BUDshLoadIn { get; set; }
        public string BUDshLoadOut { get; set; }
        public string BUDshRunning { get; set; }
        public string BUDwrLoadIn { get; set; }
        public string BUDwrLoadOut { get; set; }
        public string BUDwrRunning { get; set; }
        public string BUDInsuranceOnDropCount { get; set; }
        public string BUDTicketPrinting { get; set; }
        public string BUDDOCOther1 { get; set; }
        public string BUDDOCOther2 { get; set; }
        public string BUD { get; set; }
        public string BUDADAExpense { get; set; }
        public string BUDBoxOffice { get; set; }
        public string BUDCatering { get; set; }
        public string BUDEquipmentRental { get; set; }
        public string BUDGroupSalesExpenses { get; set; }
        public string BUDHouseStaff { get; set; }
        public string BUDLeagueFees { get; set; }
        public string BUDLicensesPermits { get; set; }
        public string BUDLimosAuto { get; set; }
        public string BUDOrchestraShellRemoval { get; set; }
        public string BUDPresenterProfit { get; set; }
        public string BUDPoliceSecurityFireMarshall { get; set; }
        public string BUDProgram { get; set; }
        public string BUDRent { get; set; }
        public string BUDSoundLights { get; set; }
       
        public string BUDTelephonesInternet { get; set; }
        public string BUDDryIceC02 { get; set; }
        public string BUDLOCOther1 { get; set; }
        public string BUDLOCOther2 { get; set; }
        public string BUDLOCOther3 { get; set; }
        public string BUDLOCOther4 { get; set; }
        public string BUDLOCOther5 { get; set; }
        public string BUDLocalFixed { get; set; }
        public string ACTAdvertisingGross { get; set; }
        public string ACTLaborCatering { get; set; }
        public string ACTMusicians { get; set; }
        public string ACTshLoadIn { get; set; }
        public string ACTshLoadOut { get; set; }
        public string ACTshRunning { get; set; }
        public string ACTwrLoadIn { get; set; }
        public string ACTwrLoadOut { get; set; }
        public string ACTwrRunning { get; set; }
        public string ACTInsuranceOnDropCount { get; set; }
        public string ACTTicketPrinting { get; set; }
        public string ACTDOCOther1 { get; set; }
        public string ACTDOCOther2 { get; set; }
        public string ACT { get; set; }
        public string ACTADAExpense { get; set; }
        public string ACTBoxOffice { get; set; }
        public string ACTCatering { get; set; }
        public string ACTEquipmentRental { get; set; }
        public string ACTGroupSalesExpenses { get; set; }
        public string ACTHouseStaff { get; set; }
        public string ACTLeagueFees { get; set; }
        public string ACTLicensesPermits { get; set; }
        public string ACTLimosAuto { get; set; }
        public string ACTOrchestraShellRemoval { get; set; }
        public string ACTPresenterProfit { get; set; }
        public string ACTPoliceSecurityFireMarshall { get; set; }
        public string ACTProgram { get; set; }
        public string ACTRent { get; set; }
        public string ACTSoundLights { get; set; }
       
        public string ACTTelephonesInternet { get; set; }
        public string ACTDryIceC02 { get; set; }
        public string ACTLOCOther1 { get; set; }
        public string ACTLOCOther2 { get; set; }
        public string ACTLOCOther3 { get; set; }
        public string ACTLOCOther4 { get; set; }
        public string ACTLOCOther5 { get; set; }
        public string ACTLocalFixed { get; set; }
    }
}
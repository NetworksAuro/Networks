using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTOS.DataLayer
{
    public class VenuePresenter
    {
        public string VenuePresenterName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string capacity { get; set; }
    }

    public class ShowDetail
    {
        public string Show { get; set; }
        public string ShowBegindate { get; set; }
        public string CorporateName { get; set; }
        public string CompanyManager { get; set; }
        public string OverheadNut { get; set; }
        public string VariableRoylaties { get; set; }
        public string WeeklyOperatingExpense { get; set; } 

    }
}
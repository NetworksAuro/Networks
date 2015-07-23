using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTOS
{
    public interface MasterPageSaveInterface
    {
        void SaveData();
        void DeleteData(string flag);
        void Reset();
    }
    public interface ISearch
    {
       void GetSearchData(bool ismaster);
       void GetSearchEngmtDealData(bool ismaster);
       void GetSearchEngmtPriceData(bool ismaster);
       void GetSearchEngmtExpensesData(bool ismaster);
       void GetSearchEngmtBoxOFfice(bool ismaster);
       void GetSearchEngtDiscount(bool ismaster);
      
    }

    public interface ISearchAll
    {
        void GetSearchPresentationData(bool ismaster);
        void GetSearchVenueData(bool ismaster);
        void GetSearchShowData(bool ismaster);
        void GetSearchPersonalData(bool ismaster);

    }

}

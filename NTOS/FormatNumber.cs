using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NTOS
{
    public static class ExtensionMethods
    {
        //public static Nullable<Int32> AutoformatInt(this string value)
        //{
        //    Nullable<Int32> val = null;
        //    char[] chDlr = { ',', ' ', ')' };
        //    value = value.Trim(chDlr).Replace("$", "");
        //    val = (value.Length > 0) ? Convert.ToInt32(value.Replace('(', '-')) : val;
        //    return val;
        //}
        //public static Nullable<decimal> AutoformatDecimal(this string value)
        //{
        //    Nullable<decimal> val = null;
        //    char[] chDlr = { ',', ' ', ')' };
        //    value = value.Trim(chDlr).Replace("$", "");
        //    val = (value.Length > 0) ? Convert.ToDecimal(value.Replace("(", "-")) : val;
        //    return val;
        //}
       
        public static Nullable<Int32> AutoformatInt(this string value)
        {
            Nullable<Int32> val = null;
            char[] chDlr = { ',', ' ', ')', '%' };
            value = value.Trim(chDlr).Replace("$", "").Replace(",", "").Replace("-", "").Replace(" ", string.Empty);
            val = (value.Length > 0) ? Convert.ToInt32(Convert.ToDecimal(value.Replace('(', '-'))) : val;
            return val;
        }
        public static Nullable<decimal> AutoformatDecimal(this string value)
        {
            Nullable<decimal> val = null;
            char[] chDlr = { ',', ' ', ')', '%' };
            value = value.Trim(chDlr).Replace("$", "").Replace(",", "").Replace("-", "").Replace(" ", string.Empty);
            val = (value.Length > 0) ? Convert.ToDecimal(value.Replace("(", "-"), CultureInfo.CurrentUICulture.NumberFormat) : val;
            return val;
        }
    }
}
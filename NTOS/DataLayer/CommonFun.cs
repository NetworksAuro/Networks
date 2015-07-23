using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using System.Text;
using System.Globalization;
namespace CommonFunction
{
    public class CommonFun
    {

        public decimal ToDecimal(string Value)
        {
            if (Value.Length == 0)
                return 0;
            else
                return Decimal.Parse(Value.Replace(" ", ""), NumberStyles.AllowThousands| NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol);
        }


        public void FillDropDownList(DropDownList ddl, DataTable mydt, string textField, string valueFeild)
        {
            ddl.DataSource = mydt;
            ddl.DataValueField = valueFeild;
            ddl.DataTextField = textField;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
        }
        public void Fillddl_noselect(DropDownList ddl, DataTable mydt, string textField, string valueFeild)
        {
            ddl.DataSource = mydt;
            ddl.DataValueField = valueFeild;
            ddl.DataTextField = textField;
            ddl.DataBind();
        }
        public void HideControls(ContentPlaceHolder cnt, System.Web.UI.Page pg)
        {
            string pagename = HttpContext.Current.Request.Url.AbsolutePath.ToLower().Replace("/", "").Replace(".aspx", "").ToString();
            string controllist = "";

            switch (pagename)
            {
                case "show":
                    {
                        controllist = "pnlshow";
                        break;
                    }
                case "personal":
                    {
                        controllist = "pnlpersonal";
                        break;
                    }
                case "presenter":
                    {
                        controllist = "pnlpresenter";
                        break;
                    }
                case "metro":
                    {
                        controllist = "pnlmetro";
                        break;
                    }
                case "venue":
                    {
                        controllist = "pnlvenue";
                        break;
                    }
            }
            Panel ctl = (Panel)cnt.FindControl(controllist);
            if (ctl != null)
            {
                ctl.Enabled = false;
            }
            //for (int i = 0; i < controllist.Split(',').Length; i++)
            //{
            //    //System.Web.UI.Control ctl =(System.Web.UI.Control) cnt.FindControl(controllist.Split(',')[i]);
            //    Panel ctl = (Panel)cnt.FindControl(controllist.Split(',')[i]);
            //    if (ctl != null)
            //    {
            //        ctl.Enabled = false;
            //    }
            //}
        }
        public void usercontrollist(string ucname, ContentPlaceHolder uc_cnt, string uc_list)
        {
            string[] arr = uc_list.Split(',');
            System.Web.UI.Control uc = (System.Web.UI.Control)uc_cnt.FindControl(ucname);
            for (int i = 0; i < arr.Length; i++)
            {
                uc.FindControl(arr[i]).Visible = false;
            }
        }

        public void HideDBData_XLSheets(Excel.Sheets currsheets, string sheetlist)
        {
            Excel.Worksheet _xlNewSheet = null;
            string[] arr = sheetlist.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                _xlNewSheet = (Excel.Worksheet)currsheets[Convert.ToInt32(arr[i])];
                _xlNewSheet.Visible = Excel.XlSheetVisibility.xlSheetHidden;
            }
        }
        public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
        {
            System.Reflection.FieldInfo info;
            foreach (RenderingExtension extension in ReportViewerID.ServerReport.ListRenderingExtensions())
            {
                if (extension.Name.ToLower() == strFormatName.ToLower())
                {
                    info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }
            }
        }

        public string SplitStringForComma(string str)
        {
            string finalstring = "";
            if (str.Contains(","))
            {

                string[] strArray = str.Split(',');
                finalstring = strArray[0].Trim();
                for (int i = 1; i < strArray.Length; i++)
                {
                    finalstring += "," + strArray[i].Trim();
                }
            }
            else
            {
                finalstring = str;
            }

            return finalstring;
        }

        public string SplitstringForDates(string str)
        {
            StringBuilder sb = new StringBuilder();
            string finalstring = "";
            if (str.Contains('>'))
            {

                str = str.Replace(">", "> '");
                str += "'";
                finalstring = str;

            }
            else if (str.Contains('<'))
            {
                str = str.Replace("<", "< '");
                str += "'";
                finalstring = str;

            }
            else
            {
                finalstring = str;
            }
            return finalstring;

        }


        public string SplitStringForSql(string str)
        {
            string finalString = "";
            if (str.Contains("..."))
            {
                if ((str.Contains('>')) || (str.Contains('<')))
                {
                    finalString = str.Replace("...", " and ");

                }
                else
                {
                    str = str.Replace("...", " and ");
                    finalString = "between" + " " + str + " ";
                }

            }
            else if ((str.Contains('>')) || (str.Contains('<')))
            {
                finalString = str;
            }

            else { finalString = "=" + " " + str; }

            return finalString;
        }



        public string SplitForDates(string str)
        {
            string finalstr = "";

            if (str.Contains("..."))
            {
                string[] res = str.Split(new string[] { "..." }, StringSplitOptions.None);
                string[] firstdatesplit = res[0].Split('/');
                string[] finaldatesplit = res[1].Split('/');
                if (firstdatesplit.Length < 3)
                    res[0] = res[0] + '/' + finaldatesplit[2];

                finalstr = "'" + res[0] + "'" + "..." + "'" + res[1] + "'";
            }
            else
                finalstr = str;

            return finalstr;
        }



        public string SpiltSqlQuery(string str, string firststr, out string finalstring)
        {
            string returnstring = "";
            bool iscons = false;
            string inString = firststr + " " + "in";
            int posin = str.IndexOf(inString);
            bool check = str.Contains(inString);
            int posunion = str.IndexOf("union");
            if (posunion < 0)
                posunion = Int32.MaxValue;
            if (posin < 0)
                posin = Int32.MaxValue;


            if (str.Contains("union") || check)
            {


                if (posunion < posin)
                {
                    string substr = "";
                    int positionOfunion = str.IndexOf("union");
                    substr = str.Substring(0, positionOfunion);
                    if (substr.Contains("where"))
                    {
                        int possubstr1 = substr.IndexOf("where");
                        int posssubstr2 = possubstr1 + 14;
                        int len = substr.Length;

                        returnstring = "Extended---" + substr.Substring(posssubstr2, substr.Length - posssubstr2);
                        finalstring = str.Substring(positionOfunion + 5, str.Length - (positionOfunion + 5));
                        return returnstring;
                    }


                }
                else
                {
                    string substr = "";

                    substr = str.Substring(0, posin);

                    if (substr.Contains("where"))
                    {
                        int possubstr1 = substr.IndexOf("where");
                        int posssubstr2 = possubstr1 + 19;

                        returnstring = "Constraint---" + substr.Substring(possubstr1 + 14, substr.Length - posssubstr2);

                        finalstring = str.Substring(posin + inString.Length, str.Length - (posin + inString.Length));
                        return returnstring;
                    }
                }




            }
            else
            {
                if (str.Contains("where"))
                {
                    int possubstr1 = str.IndexOf("where");
                    int posssubstr2 = possubstr1 + 14;

                    returnstring = "New---" + str.Substring(possubstr1 + 16, str.Length - posssubstr2);
                    returnstring.Replace("(", "");
                    finalstring = "";
                    return returnstring;

                }
            }
            finalstring = "";
            return returnstring;


        }


        public List<string> spiltQuery(string str, string firststr)
        {

            string outstr;
            List<string> newstring = new List<string>(0);

            string st = SpiltSqlQuery(str, firststr, out outstr);
            newstring.Add(st);
            while (outstr != "")
            {
                str = outstr;
                string st1 = SpiltSqlQuery(str, firststr, out outstr);
                newstring.Add(st1);

            }



            return newstring;
        }

        public string getCityAfterSplit(string st)
        {
            string finalstring = "";
            string[] namesArray = st.Split(',');
            foreach (string st1 in namesArray)
            {
                if (finalstring == "")
                    finalstring = st1.Split('/')[0].Trim();
                else
                    finalstring = finalstring + "," + (st1.Split('/')[0]).Trim();
            }

            return finalstring;
        }


        private bool isConstraint(int posin, string str, string firststring)
        {
            bool isCon = false;
            if (posin > 1)
            {

                int index = str.IndexOf("where");
                string substring = str.Substring(index, str.Length - index);
                int len = substring.IndexOf(firststring);
                int pos = firststring.Length + 1;
                string text = substring.Substring(len + pos, 2);
                if (substring.Substring(len + pos, 2) == "in")
                {

                    if (substring.Substring(len + pos + 3, 1) == "(")
                        isCon = true;
                }

            }


            return isCon;

        }


    }






}
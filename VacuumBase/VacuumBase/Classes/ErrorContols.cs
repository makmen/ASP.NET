using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Web.UI.WebControls;


namespace VacuumBase.Classes
{
    static class ErrorControls
    {
        static private int count = 0;

        public static int Count
        {
            get { return ErrorControls.count; }
            set { ErrorControls.count = value; }
        }

        static public bool NotEmptyTextBox(TextBox tb)
        {
            bool check = true;
            if (tb.Text == "")
            {
                check = false;
                if (tb.CssClass.IndexOf(" err") == -1)
                {
                    tb.CssClass += " err";
                }
            }
            else
            {
                tb.CssClass = tb.CssClass.Replace(" err", "");
            }
            CountErrors(check);

            return check;
        }

        static public bool RegexTextBox(TextBox tb, string pattern)
        {
            bool check = true;
            if (!Regex.IsMatch(tb.Text, pattern))
            {
                check = false;
                if (tb.CssClass.IndexOf(" err") == -1)
                {
                    tb.CssClass += " err";
                }
            }
            else
            {
                tb.CssClass = tb.CssClass.Replace(" err", "");
            }
            CountErrors(check);

            return check;
        }

/*
 


        static public bool EmptyComboBox(ComboBox cmb)
        {
            bool check = true;
            if (cmb.Text == "")
            {
                check = false;
                cmb.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                cmb.Background = System.Windows.Media.Brushes.White;
            }
            CountErrors(check);

            return check;
        }

        static public bool TextBoxToDouble(TextBox tb)
        {
            bool check = true;
            double res = 0;
            try
            {
                res = double.Parse(tb.Text);
                tb.Background = (LinearGradientBrush)tb.FindResource("LightBrush");
            }
            catch(Exception)
            {
                check = false;
                tb.Background = System.Windows.Media.Brushes.Red;
            }
            CountErrors(check);

            return check;
        }

        static public bool TextBoxToInt(TextBox tb, int min = 0, int max = 0)
        {
            bool check = true;
            double res = 0;
            try
            {
                res = int.Parse(tb.Text);
                tb.Background = (LinearGradientBrush)tb.FindResource("LightBrush");
                if (min == 0 && min != max) // если заданы параметры min и max
                {
                    if (res < min || res > max)
                    {
                        tb.Background = System.Windows.Media.Brushes.Red;
                        check = false;
                    }
                }
            }
            catch (Exception)
            {
                check = false;
                tb.Background = System.Windows.Media.Brushes.Red;
            }
            CountErrors(check);

            return check;
        }
 * */
        static public int GetCount()
        {
            int i = count;
            count = 0;

            return i;
        }

        static public void CountErrors(bool check)
        {
            if (!check)
            {
                ++count;
            }
        }

    }
}

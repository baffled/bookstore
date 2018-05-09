using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books.shared
{
    public static class Utils
    {
        public static Int32 DCount(String text, String delim) {
            int ct = 1;
            int ix = 0;
            int nix = 0;
            String temp = String.Empty;
            if (String.IsNullOrEmpty(text)) {
                return 0;
            }
            do {
                nix = text.IndexOf(delim, ix);
                if (nix >= 0) {
                    ct++;
                    ix = nix + delim.Length;
                }
            } while (nix >= 0);
            return ct;
        }
        public static Int32 DCount(String text, Char delim) {
            int ct = 1;
            int ix = 0;
            int nix = 0;
            String temp = String.Empty;
            if (String.IsNullOrEmpty(text)) {
                return 0;
            }
            do {
                nix = text.IndexOf(delim, ix);
                if (nix >= 0) {
                    ct++;
                    ix = nix + 1;
                }
            } while (nix >= 0);
            return ct;
        }
        public static Boolean safeBool(String value) {
            if (String.IsNullOrEmpty(value)) {
                return false;
            }
            if (value.ToUpper().StartsWith("Y")) {
                return true;
            }
            if (value.ToUpper().StartsWith("T")) {
                return true;
            }
            return (safeInt(value) > 0);
        }

        public static int safeInt(String value){
            int tempInt = 0;
            if(Int32.TryParse(value, out tempInt)){
                return tempInt;
            } else{
                return 0;
            }
        }

        public static double safeDouble(String value){
            double temp = 0.0;
            if(Double.TryParse(value, out temp)){
                return temp;
            } else{
                return 0.0;
            }
        }

        public static DateTime safeDate(String value) {
            DateTime temp;
            if (DateTime.TryParse(value, out temp)) {
                return temp;
            } else {
                return DateTime.Parse("1900-01-01");
            }
        }

        public static String safeString(String value) {
            if (value == null) value = String.Empty;
            return value;
        }

        public static DateTime fromU2Date(int value) {
            return DateTime.Parse("31 Dec 1967").AddDays(value);
        }

        public static int toU2Date(DateTime date) {
            return (Int32)date.Subtract(DateTime.Parse("31 Dec 1967")).TotalDays;
        }

        public static String nextField(String text, ref Int32 ix, String delim) {
            if (ix > text.Length) {
                return null;
            }
            String t = String.Empty;
            Int32 nix = text.IndexOf(delim, ix);
            if (nix < 0) {
                t = text.Substring(ix);
                ix = text.Length + 1;
            } else {
                t = text.Substring(ix, nix - ix);
                ix = nix + delim.Length;
            }
            return t;
        }

        public static String field(String text, string delim, int start, int take) {            
            if (String.IsNullOrEmpty(text)) {
                return String.Empty;
            }

            // find the start
            int ct = 1;
            int ix = 0;
            int nix = 0;
            String temp = String.Empty;
            if (start > 1) {
                do {
                    nix = text.IndexOf(delim, ix);
                    if (nix >= 0) {
                        ct++;
                        ix = nix + delim.Length;
                    }
                } while ((nix >= 0) && (ct < start));
            }

            if (ct < start) {
                return String.Empty;
            }

            // see what to take
            // would be nice to use -1 to take everything
            if (take == 1 || take == 0) {
                nix = text.IndexOf(delim, ix);
                if (nix >= 0) {
                    return text.Substring(ix, (nix - ix));                    
                }
                return String.Empty; 
            }
            if (take < 0) {
                return text.Substring(ix);                
            }

            int six = ix;

            do {
                nix = text.IndexOf(delim, ix);
                if (nix >= 0) {
                    ct++;
                    ix = nix + delim.Length;
                }
            } while ((nix >= 0) && (--take > 0));

            if (nix < 0) {
                return text.Substring(six);
            } else {
                return text.Substring(six, nix - six);
            }
            
        }

        /// <summary>
        /// formatCurrency: U2 holds data in whole pence.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string formatCurrency(int value)
        {
            return String.Format("{0:N2}", (value / 100));
        }
    }

    
}

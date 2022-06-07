using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Extensions
{
    public static class StructHelper
    {
        public static string StringNormalize(this string value)
        {
            string returnStr = null;
            try
            {
                if (value.Length != 0)
                {
                    int spaceCount = value.ToCharArray().Count(((w) => w == ' '));
                    if (spaceCount != 0)
                    {
                        bool nextStr = false;
                        foreach (string s in value.Split(' '))
                        {
                            if (s.Length != 0)
                            {
                                if (nextStr == true) { returnStr = returnStr + " "; }; nextStr = false;
                                int x = 0;
                                foreach (char c in s)
                                {
                                    if (x == 0)
                                    {
                                        if (c.ToString() == "i")
                                        {
                                            returnStr = returnStr + "İ"; x++;
                                        }
                                        else
                                        {
                                            returnStr = returnStr + char.ToUpper(c).ToString(); x++;
                                        }
                                    }
                                    else
                                    {
                                        if (c.ToString() == "I")
                                        {
                                            returnStr = returnStr + "ı"; x++;
                                        }
                                        else
                                        {
                                            returnStr = returnStr + char.ToLower(c).ToString();
                                        }
                                    }
                                }
                            }
                            nextStr = true;
                        }
                    }
                    else
                    {
                        int x = 0;
                        foreach (char c in value)
                        {
                            if (x == 0)
                            {
                                if (c.ToString() == "i")
                                {
                                    returnStr = returnStr + "İ"; x++;
                                }
                                else
                                {
                                    returnStr = returnStr + char.ToUpper(c).ToString(); x++;
                                }
                            }
                            else
                            {
                                if (c.ToString() == "I")
                                {
                                    returnStr = returnStr + "ı"; x++;
                                }
                                else
                                {
                                    returnStr = returnStr + char.ToLower(c).ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                returnStr = value.Trim(); // write to log..
            }
            return returnStr;
        }

        public static string SQLTextUpper(this string value)
        {
            string target = string.Empty;

            /*
             *ğ,ü,i,ş,ç,ö
             */
            if (value != null && !string.IsNullOrEmpty(value))
            {
                target = value.ToUpper();

                target = target.Replace('Ğ', 'G');
                target = target.Replace('Ü', 'U');
                target = target.Replace('İ', 'I');
                target = target.Replace('Ş', 'S');
                target = target.Replace('Ç', 'C');
                target = target.Replace('Ö', 'O');
            }
            return target;
        }

        public static string Nullable(this string value)
        {
            if (value == null)
                return null;
            else if (string.IsNullOrEmpty(value.Trim()))
                return null;
            else
                return value.Trim();
        }

       
        public static string FormatN0(this decimal value)
        {
            return value.ToString("N0");
        }

        public static string FormatCurrency(this decimal value)
        {
            return value.ToString("#,0.##");
        }

        public static Decimal ToDecimal(this object value)
        {
            decimal returnValue = 0;

            if (value == null || value is DateTime)
                return returnValue;

            try
            {


                if (value is string && !string.IsNullOrEmpty(value.ToString()))
                {
                    //returnValue = Decimal.Parse(value as string);
                    decimal.TryParse(value.ToString(), out returnValue);
                }
                else
                {
                    //returnValue = Decimal.Parse(value.ToString());
                    decimal.TryParse(value.ToString(), out returnValue);
                }
            }
            catch (Exception ex)
            {
                ex.ConsolePrint();
            }



            return returnValue;
        }

        public static Byte ToByte(this object value)
        {
            byte returnValue = 0;

            if (value == null || value is DateTime)
                return returnValue;

            try
            {
                if (value is string)
                    returnValue = Byte.Parse(value as string);
                else
                    returnValue = Byte.Parse(value.ToString());
            }
            catch { }

            return returnValue;
        }

        public static Int32 ToInt32(this object value)
        {
            Int32 returnValue = 0;

            if (value == null || value is DateTime)
                return returnValue;

            try
            {
                if (value is string)
                    returnValue = Int32.Parse(value as string);
                else
                    returnValue = Int32.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                string.Format("ToInt32 : {0} {1}", ex.Message, ex.Source).ConsolePrint();
            }

            return returnValue;
        }

        public static Int16 ToInt16(this object value)
        {
            Int16 returnValue = 0;

            if (value == null || value is DateTime)
                return returnValue;

            try
            {
                if (value is string)
                    returnValue = Int16.Parse(value as string);
                else
                    returnValue = Int16.Parse(value.ToString());
            }
            catch { }

            return returnValue;
        }

        public static DateTime ToDateTime(this object value)
        {
            DateTime returnValue = new DateTime();

            if (value is DateTime)
                return (DateTime)value;

            try
            {

                if (value is string)
                {
                    if (((string)value).Contains("+"))
                    {
                        var dateValue = ((string)value).Split("+").First();
                        returnValue = DateTime.Parse(dateValue);
                    }
                    else
                    {
                        returnValue = DateTime.Parse(value as string);
                    }
                }
                else
                {
                    returnValue = DateTime.Parse(value.ToString());
                }
            }
            catch (Exception ex)
            {
                string.Format("ToDateTime : {0} {1}", ex.Message, ex.Source).ConsolePrint();
            }


            return returnValue;
        }

        public static bool ToBool(this object value)
        {
            bool returnValue = new bool();

            if (value is Boolean)
                return (Boolean)value;

            try
            {

                if (value is string)
                    returnValue = bool.Parse(value as string);
                else
                    returnValue = bool.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                string.Format("ToBool : {0} {1}", ex.Message, ex.Source).ConsolePrint();
            }


            return returnValue;
        }


        public static bool IsNumeric(this Object Source)
        {
            if (Source == null || Source is DateTime)
                return false;

            if (Source is Int16 || Source is Int32 || Source is Int64 || Source is Decimal || Source is Single || Source is Double || Source is Boolean)
                return true;

            try
            {
                if (Source is string)
                    Double.Parse(Source as string);
                else
                    Double.Parse(Source.ToString());
                return true;
            }
            catch { }
            return false;
        }

        public static bool IsNull(this string text)
        {
            return string.IsNullOrEmpty(text.Trim());
        }
        public static void ConsolePrint(this object value)
        {
            Console.WriteLine(value);
        }

        public static string ToString<T>(this T instance, string format, params string[] proprtyNames)
        {
            T sample = (T)instance;

            string[] values = new string[proprtyNames.Length];

            for (int i = 0; i < proprtyNames.Length; i++)
            {
                if (sample.GetType().GetProperty(proprtyNames[i]) != null)
                {
                    values[i] = sample.GetType().GetProperty(proprtyNames[i]).GetValue(instance, null).ToString();
                }
            }
            return string.Format(format, values);
        }


        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
        

    }
}

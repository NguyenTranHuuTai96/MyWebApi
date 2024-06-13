using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Optimize
{
    public static class MethodConvert
    {
        public static  string ConvertDateToStringEight(DateTime? iDate)
        {
            return iDate == null ? "" : iDate?.ToString("yyyy-MM-dd");
        }
        public static  DateTime? ConvertStingEightToDate(string iDate112)
        {
            CultureInfo enUS = new CultureInfo("en-US");
            var resultDate = new DateTime();
            if (DateTime.TryParseExact(iDate112, "yyyyMMdd", enUS,
                                 DateTimeStyles.None, out resultDate))
                return resultDate;
            else
                return null;
        }
        public static Dictionary<string, object> ToDictionary(object model)
        {
            try
            {
                var dict = model.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(model));
                return dict;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }
    }
}

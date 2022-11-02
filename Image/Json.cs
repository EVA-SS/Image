using Newtonsoft.Json.Linq;
using System.Data;

namespace Image
{
    public static class Json
    {
        public static string ByteUnit(this long Size, int d = 1, string nultxt = "0B")
        {
            return ByteUnit(Size * 1.0, d, nultxt);
        }

        public static string ByteUnit(this double Size, int d = 1, string nultxt = "0B")
        {
            if (Size == 0)
            {
                return nultxt;
            }
            var val = Size;
            int unit = 0;
            while (val > 1024)
            {
                val = val / 1024;
                unit++;
                if (unit > 5)
                {
                    break;
                }
            }
            return Math.Round(val, d) + CountSizeUnit(unit);
        }

        static string CountSizeUnit(this int val)
        {
            switch (val)
            {
                case 4: return "T";
                case 3: return "G";
                case 2: return "M";
                case 1: return "K";
                case 5: return "P";
                case 6: return "E";
                //case 7: return "Z";
                //case 8: return "Y";
                default: return "B";
            }
        }

        static Newtonsoft.Json.JsonSerializerSettings mJsonSettings = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            else if (obj is DataTable)
            {
                return (obj as DataTable).ToJson();
            }
            else if (obj is DataRow)
            {
                return (obj as DataRow).ToJson();
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, mJsonSettings);
            }
        }
        public static T ToJson<T>(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
                }
                catch
                {
                }
            }
            return default(T);
        }
        public static dynamic ToJson(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return JObject.Parse(value);
            }
            return null;
        }
    }
}

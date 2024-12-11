namespace Image
{
    public static class Api
    {
        public static string ByteUnit(this long Size, int d = 1, string nultxt = "0 B") => ByteUnit(Size * 1.0, d, nultxt);

        public static string ByteUnit(this double Size, int d = 1, string nultxt = "0 B")
        {
            if (Size == 0) return nultxt;
            var val = Size;
            int unit = 0;
            while (val > 1024)
            {
                val = val / 1024;
                unit++;
                if (unit > 5) break;
            }
            return Math.Round(val, d) + CountSizeUnit(unit);
        }

        static string CountSizeUnit(this int val)
        {
            switch (val)
            {
                case 4: return "TB";
                case 3: return "GB";
                case 2: return "MB";
                case 1: return "KB";
                case 5: return "PB";
                case 6: return "EB";
                default: return "B";
            }
        }
    }
}
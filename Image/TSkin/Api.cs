namespace TSkin
{
    public static class Api
    {
        #region 颜色

        public static int HexToInt(this string str)
        {
            return int.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        /// <summary>
        /// [颜色：16进制转成RGB]
        /// </summary>
        /// <param name="strColor">设置16进制颜色 [返回RGB]</param>
        /// <returns></returns>
        public static Color HexToColor(this string strHxColor)
        {
            try
            {
                if (!string.IsNullOrEmpty(strHxColor) && strHxColor.Length > 5)
                {
                    if (strHxColor.StartsWith("#"))
                    {
                        strHxColor = strHxColor.Substring(1);
                    }

                    if (strHxColor.Length == 6)
                    {
                        return Color.FromArgb(strHxColor.Substring(0, 2).HexToInt(), strHxColor.Substring(2, 2).HexToInt(), strHxColor.Substring(4, 2).HexToInt());
                    }
                    else if (strHxColor.Length == 8)
                    {
                        return Color.FromArgb(strHxColor.Substring(6, 2).HexToInt(), strHxColor.Substring(0, 2).HexToInt(), strHxColor.Substring(2, 2).HexToInt(), strHxColor.Substring(4, 2).HexToInt());
                    }
                }
            }
            catch
            {
            }
            return System.Drawing.Color.FromArgb(0, 0, 0);//设为黑色
        }

        /// <summary>
        /// [颜色：RGB转成16进制]
        /// </summary>
        /// <returns></returns>
        public static string ColorToHex(this Color color)
        {
            if (color.A == 255)
            {
                return string.Format("{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
            }
            else
            {
                return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", color.R, color.G, color.B, color.A);
            }
        }

        #endregion

        public static Size GetSize(this SizeF sizeF, int fz = 0)
        {
            return new Size((int)Math.Ceiling(sizeF.Width) + fz, (int)Math.Ceiling(sizeF.Height) + fz);
        }
        public static Size GetSize(this SizeF sizeF, int w, int h)
        {
            return new Size((int)Math.Ceiling(sizeF.Width) + w, (int)Math.Ceiling(sizeF.Height) + h);
        }
    }
}

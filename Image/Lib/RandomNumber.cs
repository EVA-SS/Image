﻿namespace Image
{
    public class RandomNumber
    {
        static object _lock = new object();
        static int count = 1;
        public static string GetID(string num)
        {
            lock (_lock)
            {
                if (count > 999) count = 1;
                var number = num + DateTime.Now.ToString("yyyyMMddHHmmss") + count.ToString().PadLeft(3, '0');
                count++;
                return number;
            }
        }
    }
}
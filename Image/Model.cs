namespace Image
{
    public class HttpPaths : HttpBase
    {
        public List<HttpInfo> data { get; set; }
    }
    public class HttpInfo
    {
        /// <summary>
        /// 配置的存储路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 总空间大小（字节）
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 可用空间大小（字节）
        /// </summary>
        public long free { get; set; }
    }


    public class TFileInfo
    {
        public string name { get; set; }
        public string type { get; set; }
        public long size { get; set; }
    }
    public class HttpData : HttpBase
    {
        public List<string> data { get; set; }
    }
    public class HttpBase
    {
        public int errno { get; set; }
        public string errmsg { get; set; }
    }
}
namespace Image
{
    /// <summary>
    /// 系统设置。保存用户设置的数据。
    /// </summary>

    public static class Settings
    {
        public static List<FileIndex> ImagePath
        {
            get
            {
                var json = core["path"];
                if (json != null)
                {
                    try
                    {
                        var data = System.Text.Json.JsonSerializer.Deserialize(json, typeof(List<FileIndex>), SGC.Default);
                        if (data is List<FileIndex> list) return list;
                    }
                    catch { }
                }
                return new List<FileIndex>(0);
            }
            set => core["path"] = System.Text.Json.JsonSerializer.Serialize(value, typeof(List<FileIndex>), SGC.Default);
        }

        public static int ImagePathIndex
        {
            get => core.Int("i");
            set => core.IntSet("i", value);
        }

        public static string TempPath
        {
            get => core["temp"] ?? Program.TempPath;
            set => core["temp"] = value;
        }

        /// <summary>
        /// 最大上传字节
        /// </summary>
        public static long LimitSize
        {
            get => core.Long("limit", "size", 1073741824);
            set => core.LongSet("limit", "size", value);
        }

        /// <summary>
        /// 保留字节
        /// </summary>
        public static long LimitRetain
        {
            get => core.Long("limit", "retain", 1024);
            set => core.LongSet("limit", "retain", value);
        }

        /// <summary>
        /// 是否写入日志
        /// </summary>
        public static bool Log
        {
            get => core.Bool("log");
            set => core.BoolSet("log", value);
        }

        public static int HttpPort
        {
            get => core.Int("port", 6088);
            set => core.IntSet("port", value);
        }

        public static int HttpsPort
        {
            get => core.Int("ports", 6089);
            set => core.IntSet("ports", value);
        }

        #region 证书

        public static string CertPfx
        {
            get => core["cert", "pfx"] ?? "cert.pfx";
            set => core["cert", "pfx"] = value;
        }
        public static string CertPfxPass
        {
            get => core["cert", "pfxpass"] ?? "pass.txt";
            set => core["cert", "pfxpass"] = value;
        }

        public static string CertPem
        {
            get => core["cert", "pem"] ?? "cert.pem";
            set => core["cert", "pem"] = value;
        }

        public static string CertPemPass
        {
            get => core["cert", "pempass"] ?? "key.pem";
            set => core["cert", "pempass"] = value;
        }

        #endregion

        #region Core

        /// <summary>
        /// 恢复默认
        /// </summary>
        public static void Resume() => core.Resume();
        /// <summary>
        /// 保存
        /// </summary>
        public static void Save() => core.Save();

        static string FilePath = Program.BasePath + "setting.ini";

        static IniParser core;
        static Settings()
        {
            core = new IniParser(FilePath, true);
        }

        #endregion
    }

    public class FileIndex
    {
        public string path { get; set; }
        public string root { get; set; }
        public int i { get; set; }
    }
}
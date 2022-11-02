using IniParser;
using IniParser.Model;
using System.Text;

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
                return ReadString("Path", "Image").ToJson<List<FileIndex>>();
            }
            set
            {
                Write("Path", "Image", value.ToJson());
            }
        }

        public static int ImagePathIndex
        {
            get
            {
                return ReadInt("Path", "Index");
            }
            set
            {
                Write("Path", "Index", value);
            }
        }

        public static string TempPath
        {
            get
            {
                var val = ReadString("Path", "Temp");
                if (val == null)
                {
                    return Program.TempPath;
                }
                else { return val; }
            }
            set
            {
                Write("Path", "Temp", value);
            }
        }

        /// <summary>
        /// 最大上传字节
        /// </summary>
        public static long LimitSize
        {
            get
            {
                //默认1G
                return ReadLong("Limit", "Size", 1073741824);
            }
            set
            {
                Write("Limit", "Size", value);
            }
        }

        /// <summary>
        /// 保留字节
        /// </summary>
        public static long LimitRetain
        {
            get
            {
                return ReadLong("Limit", "Retain", 1024);
            }
            set
            {
                Write("Limit", "Retain", value);
            }
        }

        /// <summary>
        /// 是否写入日志
        /// </summary>
        public static bool Log
        {
            get
            {
                return ReadBool("System", "Log");
            }
            set
            {
                Write("System", "Log", value);
            }
        }

        #region 端口

        public static int HttpPortDefault = 6088;
        public static int HttpPort
        {
            get
            {
                return ReadInt("Port", "Http", HttpPortDefault);
            }
            set
            {
                Write("Port", "Http", value);
            }
        }

        #endregion

        #region Core

        #region 恢复默认

        public static void Resume()
        {
            if (File.Exists(SystemSettingsFilePath))
            {
                File.WriteAllText(SystemSettingsFilePath, null, Encoding.UTF8);
            }
            data = parser.ReadFile(SystemSettingsFilePath);
        }

        #endregion

        static string SystemSettingsFilePath = Program.BasePath + "setting.ini";

        static FileIniDataParser parser = new FileIniDataParser();
        static IniData data;
        static FileSystemWatcher watcher;
        static Settings()
        {
            if (!File.Exists(SystemSettingsFilePath))
            {
                File.WriteAllText(SystemSettingsFilePath, null, Encoding.UTF8);
            }
            try
            {
                data = parser.ReadFile(SystemSettingsFilePath, Encoding.UTF8);
            }
            catch
            {
                File.WriteAllText(SystemSettingsFilePath, null, Encoding.UTF8);
                data = parser.ReadFile(SystemSettingsFilePath, Encoding.UTF8);
            }
            watcher = new FileSystemWatcher
            {
                Filter = "setting.ini",
                Path = Program.BasePath,
                NotifyFilter = NotifyFilters.LastWrite,//设置监视文件的哪些修改行为
                EnableRaisingEvents = true
            };
            watcher.Changed += Watcher_Changed;
        }
        public delegate void VEventHandler();
        public static event VEventHandler Update;

        static int watcher_count = 0;
        static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (watcher_count == 0)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            data = parser.ReadFile(SystemSettingsFilePath, Encoding.UTF8);
                            watcher_count = 0;
                            if (Update != null) { Update(); }
                            return;
                        }
                        catch { }
                    }
                });
            }
            watcher_count++;
        }

        #region 读取

        static string ReadString(string section, string key, string devaue = null)
        {
            string value = _Read(section, key);
            if (string.IsNullOrEmpty(value))
            {
                return devaue;
            }
            return value;
        }
        static bool ReadBool(string section, string key, bool devaue = false)
        {
            string _value = _Read(section, key);
            if (string.IsNullOrEmpty(_value))
            {
                return devaue;
            }
            else if (_value == "1")
            {
                return true;
            }
            else if (_value == "0")
            {
                return false;
            }
            else
            {
                bool value;
                if (bool.TryParse(_value, out value))
                {
                    return value;
                }
                return devaue;
            }
        }
        static int ReadInt(string section, string key, int devaue = 0)
        {
            int value;
            if (int.TryParse(_Read(section, key), out value))
            {
                return value;
            }
            return devaue;
        }
        static float ReadFloat(string section, string key, float devaue = 0)
        {
            float value;
            if (float.TryParse(_Read(section, key), out value))
            {
                return value;
            }
            return devaue;
        }
        static double ReadDouble(string section, string key, double devaue = 0)
        {
            double value;
            if (double.TryParse(_Read(section, key), out value))
            {
                return value;
            }
            return devaue;
        }
        static long ReadLong(string section, string key, long devaue = 0)
        {
            long value;
            if (long.TryParse(_Read(section, key), out value))
            {
                return value;
            }
            return devaue;
        }
        static DateTime ReadDateTime(string section, string key)
        {
            DateTime value;
            if (DateTime.TryParse(_Read(section, key), out value))
            {
                return value;
            }
            return new DateTime();
        }
        static string _Read(string section, string key)
        {
            return data[section][key];
        }

        #endregion

        #region 写入

        static bool Write(string section, string key, string Value)
        {
            return _Write(section, key, Value);
        }
        static bool Write(string section, string key, int Value)
        {
            return _Write(section, key, Value.ToString());
        }
        static bool Write(string section, string key, long Value)
        {
            return _Write(section, key, Value.ToString());
        }
        static bool Write(string section, string key, double Value)
        {
            return _Write(section, key, Value.ToString());
        }
        static bool Write(string section, string key, float Value)
        {
            return _Write(section, key, Value.ToString());
        }
        static bool Write(string section, string key, bool Value)
        {
            return _Write(section, key, Value ? "1" : "0");
        }

        static int writ_count = 0;
        static bool _Write(string section, string key, object Value)
        {
            try
            {
                if (Value == null)
                {
                    if (data[section][key] == null)
                    {
                        return false;
                    }
                    data[section][key] = null;
                }
                else
                {
                    string val = Value.ToString();
                    if (string.IsNullOrEmpty(val))
                    {
                        if (data[section][key] == null)
                        {
                            return false;
                        }
                        data[section][key] = null;
                    }
                    else
                    {
                        data[section][key] = val;
                    }
                }
                if (writ_count == 0)
                {
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            Thread.Sleep(1000);
                            try
                            {
                                watcher.EnableRaisingEvents = false;
                                parser.WriteFile(SystemSettingsFilePath, data, Encoding.UTF8);
                                writ_count = 0;
                                watcher.EnableRaisingEvents = true;
                                return;
                            }
                            catch { }
                        }
                    });
                }
                writ_count++;
                return true;
            }
            catch { }
            return false;
        }

        #endregion

        #endregion
    }
    public class FileIndex
    {
        public string path { get; set; }
        public string root { get; set; }
        public int i { get; set; }
    }
}

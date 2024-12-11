using System.Text;

namespace Image
{
    public class IniParser : IDisposable
    {
        string FilePath;
        Encoding Encoding;

        bool watcherEnable = false;
        FileSystemWatcher? watcher;

        public IniParser(string filePath) : this(filePath, Encoding.UTF8)
        {
        }

        public IniParser(string filePath, bool enable) : this(filePath, Encoding.UTF8)
        {
            if (enable) EnableWatcher();
        }

        public IniParser(string filePath, Encoding encoding)
        {
            FilePath = filePath;
            Encoding = encoding;
            data = Read();
        }

        public IniParserRoot Read()
        {
            if (File.Exists(FilePath))
            {
                string? title = null;
                var TMP_V = new List<string?>();
                var TMP_D = new List<IniParserData>(1);
                Dictionary<string, int> TMP_V_Index = new Dictionary<string, int>(),
                    TMP_D_Index = new Dictionary<string, int>();
                int TMP_V_I = 0, TMP_D_I = 0;
                var section = new Dictionary<string, string?>();
                foreach (var text in File.ReadAllLines(FilePath, Encoding))
                {
                    var line = text.Trim();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        if (section.Count > 0)
                        {
                            if (title == null)
                            {
                                foreach (var item in section)
                                {
                                    TMP_V.Add(item.Value);
                                    TMP_V_Index.Add(item.Key, TMP_V_I);
                                    TMP_V_I++;
                                }
                            }
                            else
                            {
                                var tmp = new List<string?>(section.Count);
                                var tmpIndex = new Dictionary<string, int>(section.Count);
                                int tmpI = 0;
                                foreach (var item in section)
                                {
                                    tmp.Add(item.Value);
                                    tmpIndex.Add(item.Key, tmpI);
                                    tmpI++;
                                }

                                TMP_D.Add(new IniParserData(tmp.ToArray(), tmpIndex));
                                TMP_D_Index.Add(title, TMP_D_I);
                                TMP_D_I++;
                            }
                        }

                        title = line.Substring(1, line.Length - 2).Trim();
                        section.Clear();
                        continue;
                    }

                    var idx = line.IndexOf("=");
                    if (idx > -1)
                    {
                        string key = line.Substring(0, idx).Trim();
                        string? value = line.Substring(idx + 1).Trim();
                        if (string.IsNullOrWhiteSpace(value)) value = null;
                        section.TryAdd(key, value);
                    }
                }

                if (section.Count > 0)
                {
                    if (title == null)
                    {
                        foreach (var item in section)
                        {
                            TMP_V.Add(item.Value);
                            TMP_V_Index.Add(item.Key, TMP_V_I);
                            TMP_V_I++;
                        }
                    }
                    else
                    {
                        var tmp = new List<string?>(section.Count);
                        var tmpIndex = new Dictionary<string, int>(section.Count);
                        int tmpI = 0;
                        foreach (var item in section)
                        {
                            tmp.Add(item.Value);
                            tmpIndex.Add(item.Key, tmpI);
                            tmpI++;
                        }

                        TMP_D.Add(new IniParserData(tmp.ToArray(), tmpIndex));
                        TMP_D_Index.Add(title, TMP_D_I);
                        TMP_D_I++;
                    }
                }

                return new IniParserRoot(TMP_V.ToArray(), TMP_V_Index, TMP_D.ToArray(), TMP_D_Index);
            }

            return new IniParserRoot();
        }

        public void EnableWatcher(bool enable = true)
        {
            watcherEnable = enable;
            if (enable)
            {
                if (watcher == null)
                {
                    if (File.Exists(FilePath))
                    {
                        var file = new FileInfo(FilePath);
                        watcher = new FileSystemWatcher
                        {
                            Filter = file.Name,
                            Path = file.DirectoryName ?? "",
                            NotifyFilter = NotifyFilters.LastWrite, //设置监视文件的哪些修改行为
                            EnableRaisingEvents = true
                        };
                        watcher.Changed += Watcher_Changed;
                    }
                }
                else watcher.EnableRaisingEvents = true;
            }
            else Dispose();
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            data = Read();
        }

        #region 交互

        public string? this[string key]
        {
            get
            {
                var value = data[key];
                if (OnEncrypt != null && !string.IsNullOrWhiteSpace(value))
                    value = OnEncrypt(new IniParserEncrypt(key, value, false));
                return value;
            }
            set
            {
                if (OnEncrypt != null && !string.IsNullOrWhiteSpace(value))
                    value = OnEncrypt(new IniParserEncrypt(key, value, true));
                data[key] = value;
            }
        }

        public string? this[string section, string key]
        {
            get
            {
                var value = data[section, key];
                if (OnEncrypt != null && !string.IsNullOrWhiteSpace(value))
                    value = OnEncrypt(new IniParserEncrypt(section + "-" + key, value, false));
                return value;
            }
            set
            {
                if (OnEncrypt != null && !string.IsNullOrWhiteSpace(value))
                    value = OnEncrypt(new IniParserEncrypt(section + "-" + key, value, true));
                data[section, key] = value;
            }
        }

        #region Int

        public int Int(string key, int defaultValue = 0)
        {
            if (int.TryParse(this[key], out var value)) return value;
            return defaultValue;
        }

        public int Int(string section, string key, int defaultValue = 0)
        {
            if (int.TryParse(this[section, key], out var value)) return value;
            return defaultValue;
        }

        public void IntSet(string key, int value) => this[key] = value.ToString();
        public void IntSet(string section, string key, int value) => this[section, key] = value.ToString();

        #endregion

        #region Float

        public float Float(string key, float defaultValue = 0F)
        {
            if (float.TryParse(this[key], out var value)) return value;
            return defaultValue;
        }

        public float Float(string section, string key, float defaultValue = 0F)
        {
            if (float.TryParse(this[section, key], out var value)) return value;
            return defaultValue;
        }

        public void FloatSet(string key, float value) => this[key] = value.ToString();
        public void FloatSet(string section, string key, float value) => this[section, key] = value.ToString();

        #endregion

        #region Long

        public long Long(string key, long defaultValue = 0)
        {
            if (long.TryParse(this[key], out var value)) return value;
            return defaultValue;
        }

        public long Long(string section, string key, long defaultValue = 0)
        {
            if (long.TryParse(this[section, key], out var value)) return value;
            return defaultValue;
        }

        public void LongSet(string key, long value) => this[key] = value.ToString();
        public void LongSet(string section, string key, long value) => this[section, key] = value.ToString();

        #endregion

        #region Double

        public double Double(string key, double defaultValue = 0)
        {
            if (double.TryParse(this[key], out var value)) return value;
            return defaultValue;
        }

        public double Double(string section, string key, double defaultValue = 0)
        {
            if (double.TryParse(this[section, key], out var value)) return value;
            return defaultValue;
        }

        public void DoubleSet(string key, double value) => this[key] = value.ToString();
        public void DoubleSet(string section, string key, double value) => this[section, key] = value.ToString();

        #endregion

        #region Bool

        public bool Bool(string key, bool defaultValue = false) => BoolCore(this[key], defaultValue);

        public bool Bool(string section, string key, bool defaultValue = false) =>
            BoolCore(this[section, key], defaultValue);

        bool BoolCore(string? value, bool defaultValue = false)
        {
            if (value == null) return defaultValue;
            else if (value == "1") return true;
            else if (value == "0") return false;
            else if (bool.TryParse(value, out var r)) return r;
            return defaultValue;
        }

        public void BoolSet(string key, bool value) => this[key] = value ? "1" : "0";
        public void BoolSet(string section, string key, bool value) => this[section, key] = value ? "1" : "0";

        #endregion

        public Func<IniParserEncrypt, string>? OnEncrypt;

        #endregion

        IniParserRoot data;

        #region 方法

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            if (watcher == null) File.WriteAllLines(FilePath, data.Save(), Encoding);
            else
            {
                watcher.EnableRaisingEvents = false;
                File.WriteAllLines(FilePath, data.Save(), Encoding);
                watcher.EnableRaisingEvents = true;
            }

            if (watcherEnable && watcher == null) EnableWatcher();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Resume()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                data = Read();
            }
        }

        #endregion

        public void Dispose()
        {
            watcher?.Dispose();
            watcher = null;
        }

        public class IniParserRoot : IniParserData
        {
            public IniParserRoot() : base(new string?[0], new Dictionary<string, int>(0))
            {
                D = [];
                DIndex = [];
            }

            public IniParserRoot(string?[] v, Dictionary<string, int> v_Index, IniParserData[] d,
                Dictionary<string, int> d_Index) : base(v, v_Index)
            {
                D = d;
                DIndex = d_Index;
            }

            #region Data

            public IniParserData[] D { get; set; }
            public Dictionary<string, int> DIndex { get; set; }

            #endregion

            public string? this[string key]
            {
                get
                {
                    if (VIndex.TryGetValue(key, out var i)) return V[i];
                    return null;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value)) value = null;
                    if (VIndex.TryGetValue(key, out var i)) V[i] = value;
                    else
                    {
                        var tmp = new List<string?>(V.Length + 1);
                        tmp.AddRange(V);
                        VIndex.Add(key, tmp.Count);
                        tmp.Add(value);
                        V = tmp.ToArray();
                    }
                }
            }

            public string? this[string section, string key]
            {
                get
                {
                    if (DIndex.TryGetValue(section, out var i))
                    {
                        var find_section = D[i];
                        if (find_section.VIndex.TryGetValue(key, out var i_key)) return find_section.V[i_key];
                    }

                    return null;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value)) value = null;
                    if (DIndex.TryGetValue(section, out var i))
                    {
                        var find_section = D[i];
                        if (find_section.VIndex.TryGetValue(key, out var i_key)) find_section.V[i_key] = value;
                        else
                        {
                            var tmp = new List<string?>(find_section.V.Length + 1);
                            tmp.AddRange(find_section.V);
                            find_section.VIndex.Add(key, tmp.Count);
                            tmp.Add(value);
                            find_section.V = tmp.ToArray();
                        }
                    }
                    else
                    {
                        var tmp = new List<IniParserData>(D.Length + 1);
                        tmp.AddRange(D);
                        DIndex.Add(section, tmp.Count);
                        tmp.Add(new IniParserData([value], new Dictionary<string, int> { { key, 0 } }));
                        D = tmp.ToArray();
                    }
                }
            }

            public override string ToString() => string.Join(Environment.NewLine, Save());

            public List<string> Save()
            {
                int dcount = D.Length;
                foreach (var it in DIndex) dcount += D[it.Value].V.Length + 1;
                var line = new List<string>(V.Length + dcount);
                if (V.Length > 0)
                {
                    foreach (var it in VIndex) line.Add(it.Key + " = " + V[it.Value]);
                    line.Add("");
                }

                foreach (var it in DIndex)
                {
                    var value = D[it.Value];
                    line.Add("[" + it.Key + "]");
                    foreach (var item in value.VIndex) line.Add(item.Key + " = " + value.V[item.Value]);
                    line.Add("");
                }

                return line;
            }
        }

        public class IniParserData
        {
            public IniParserData(string?[] v, Dictionary<string, int> index)
            {
                V = v;
                VIndex = index;
            }

            public string?[] V { get; set; }
            public Dictionary<string, int> VIndex { get; set; }
        }
    }

    public class IniParserEncrypt
    {
        public IniParserEncrypt(string ID, string Val, bool Write)
        {
            id = ID;
            value = Val;
            write = Write;
        }

        /// <summary>
        /// 标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 是否写入
        /// </summary>
        public bool write { get; set; }
    }
}
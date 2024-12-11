namespace Image
{
    /// <summary>
    /// 多线程存图管理中心
    /// </summary>
    public static class centreImage
    {
        static centreImage()
        {
            foreach (var item in Settings.ImagePath) Paths.Add(item.i.ToString(), item);
            Assess();
            new Thread(LongTask)
            {
                IsBackground = true
            }.Start();
        }

        public static FileIndex? Sel = null;
        public static Dictionary<string, FileIndex> Paths = new Dictionary<string, FileIndex>();
        static long totol = 0, totol_free = 0;

        static void LongTask()
        {
            while (true)
            {
                Thread.Sleep(10000);
                Assess();
            }
        }
        public static void Hand(long save_size)
        {
            totol_free -= save_size;
            if (totol_free > Settings.LimitRetain)
            {
            }
            else Assess();
        }

        /// <summary>
        /// 评估空间
        /// </summary>
        static void Assess()
        {
            Sel = AssessCore(out totol, out totol_free);
        }
        static FileIndex? AssessCore(out long totol, out long totol_free)
        {
            var drives = DriveInfo.GetDrives().ToList();
            foreach (var item in Paths)
            {
                var drive = drives.Find(a => a.Name == item.Value.root);
                if (drive != null)
                {
                    if (drive.TotalFreeSpace > Settings.LimitRetain)
                    {
                        totol = drive.TotalSize;
                        totol_free = drive.TotalFreeSpace;
                        return item.Value;
                    }
                }
            }
            totol = totol_free = 0;
            return null;
        }
    }
}
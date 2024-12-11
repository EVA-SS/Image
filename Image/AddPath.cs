namespace Image
{
    public partial class AddPath : AntdUI.Window
    {
        public AddPath()
        {
            InitializeComponent();
        }

        public string? path, root;
        private void btn_save_Click(object sender, EventArgs e)
        {
            path = txt_path.Text.TrimEnd('\\') + Path.DirectorySeparatorChar;
            DialogResult = DialogResult.OK;
        }

        private void txt_path_TextChanged(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            label1.Text = null;
            if (string.IsNullOrEmpty(txt_path.Text)) return;
            var dirinfo = new DirectoryInfo(txt_path.Text);
            if (dirinfo.Exists)
            {
                root = dirinfo.Root.Name;
                bool isok = false;
                Task.Run(() =>
                {
                    var drive = System.IO.DriveInfo.GetDrives().ToList().Find(a => a.Name == dirinfo.Root.Name);
                    if (drive != null)
                    {
                        if (drive.TotalFreeSpace > 1073741824) isok = true; //大于1G
                        label1.Text = $"总 {drive.TotalSize.ByteUnit()} | 已使用 {(drive.TotalSize - drive.TotalFreeSpace).ByteUnit()} | 剩余 {drive.TotalFreeSpace.ByteUnit()}";
                    }
                }).ContinueWith((action =>
                {
                    if (isok) btn_save.Enabled = true;
                }));
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog { Description = "选择挂载路径", InitialDirectory = txt_path.Text, UseDescriptionForTitle = true })
            {
                if (dialog.ShowDialog() == DialogResult.OK) txt_path.Text = dialog.SelectedPath;
            }
        }

        private void winBar_BackClick(object sender, EventArgs e) => DialogResult = DialogResult.No;
    }
}
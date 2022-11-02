using System.Runtime.InteropServices;

namespace Image
{
    public partial class AddPath : Form
    {
        #region ���÷��йܵĶ�̬���ӿ����ô�������϶�

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();
        public void FrmMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x0112, 61456 | 2, 0);
        }

        #endregion

        public AddPath()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public string path, root;
        private void btn_save_Click(object sender, EventArgs e)
        {
            path = txt_path.Text.TrimEnd('\\') + Path.DirectorySeparatorChar;
            this.DialogResult = DialogResult.OK;
        }

        private void txt_path_TextChanged(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            label1.Text = null;
            if (string.IsNullOrEmpty(txt_path.Text)) { return; }
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
                        if (drive.TotalFreeSpace > 1073741824)
                        {
                            //����1G
                            isok = true;
                        }
                        Invoke(new Action(() =>
                        {
                            label1.Text = $"�� {drive.TotalSize.ByteUnit()} | ��ʹ�� {(drive.TotalSize - drive.TotalFreeSpace).ByteUnit()} | ʣ�� {drive.TotalFreeSpace.ByteUnit()}";
                        }));
                    }
                }).ContinueWith((action =>
                {
                    Invoke(new Action(() =>
                    {
                        if (isok)
                        {
                            btn_save.Enabled = true;
                        }
                    }));
                }));
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog { Description = "ѡ�����·��", InitialDirectory = txt_path.Text, UseDescriptionForTitle = true })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txt_path.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
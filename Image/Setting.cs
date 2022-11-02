using System.Runtime.InteropServices;

namespace Image
{
    public partial class Setting : Form
    {
        #region 调用非托管的动态链接库来让窗体可以拖动

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

        Main main;
        public Setting(Main _main)
        {
            main = _main;
            InitializeComponent();
            txt_port.Text = Settings.HttpPort.ToString();
            check_log.Checked = Settings.Log;
            var val = Settings.LimitSize * 1.0;
            if (val >= 1024)
            {
                val = val / 1024.0;
                if (val >= 1024)
                {
                    val = val / 1024.0;
                    if (val >= 1024)
                    {
                        val = val / 1024.0;
                        if (val >= 1024)
                        {
                            val = val / 1024.0;
                            txt_limitSize.Text = val + "T";
                        }
                        else
                        {
                            txt_limitSize.Text = val + "G";
                        }
                    }
                    else
                    {
                        txt_limitSize.Text = val + "M";
                    }
                }
                else
                {
                    txt_limitSize.Text = val + "K";
                }
            }
            else
            {
                txt_limitSize.Text = val + "B";
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Log = check_log.Checked;
                Settings.HttpPort = Convert.ToInt32(txt_port.Text);
                var val = txt_limitSize.Text.ToUpper();
                if (val.EndsWith("T"))
                {
                    Settings.LimitSize = Convert.ToInt64(txt_limitSize.Text.Substring(0, txt_limitSize.Text.Length - 1)) * 1024 * 1024 * 1024 * 1024;
                }
                else if (val.EndsWith("G"))
                {
                    Settings.LimitSize = Convert.ToInt64(txt_limitSize.Text.Substring(0, txt_limitSize.Text.Length - 1)) * 1024 * 1024 * 1024;
                }
                else if (val.EndsWith("M"))
                {
                    Settings.LimitSize = Convert.ToInt64(txt_limitSize.Text.Substring(0, txt_limitSize.Text.Length - 1)) * 1024 * 1024;
                }
                else if (val.EndsWith("K"))
                {
                    Settings.LimitSize = Convert.ToInt64(txt_limitSize.Text.Substring(0, txt_limitSize.Text.Length - 1)) * 1024;
                }
                else
                {
                    Settings.LimitSize = Convert.ToInt64(txt_limitSize.Text);
                }
                this.DialogResult = DialogResult.OK;
            }
            catch { }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            using (var frm = new AddPath())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var it = new FileIndex { i = Settings.ImagePathIndex, path = frm.path, root = frm.root };
                    Settings.ImagePathIndex = Settings.ImagePathIndex + 1;
                    var list = Settings.ImagePath;
                    if (list == null)
                    {
                        Settings.ImagePath = new List<FileIndex> { it };
                    }
                    else
                    {
                        var find_root = list.Find(a => a.root == frm.root);
                        if (find_root != null)
                        {
                            Settings.ImagePathIndex = Settings.ImagePathIndex - 1;
                            MessageBox.Show("重复磁盘");
                            return;
                        }
                        list.Add(it);
                        Settings.ImagePath = list;
                    }
                    main.relodList();
                }
            }
        }
    }
}
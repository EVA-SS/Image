using System.Collections;
using System.Configuration.Install;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace Image
{
    public partial class Main : Form
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

        public Main()
        {
            InitializeComponent();
        }

        ServiceController service = null;
        protected override void OnLoad(EventArgs e)
        {
            relodList();
            service = ServiceController.GetServices().ToList().Find(a => a.ServiceName == "Image");
            if (service == null)
            {
                btn_install.Visible = true;
                label_state.Text = "δ��װ";
                label_state.ForeColor = Color.Red;
            }
            else
            {
                btn_uninstall.Visible = true;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    btn_stop.Visible = true;
                    label_state.Text = "������";
                    label_state.ForeColor = Color.FromArgb(24, 144, 255);
                }
                else
                {
                    btn_start.Visible = true;
                    label_state.Text = "δ����";
                    label_state.ForeColor = Color.Black;
                }
            }
            base.OnLoad(e);
            new Thread(LongTask)
            {
                IsBackground = true
            }.Start();
        }

        TransactedInstaller TransactedInstaller()
        {
            var ti = new TransactedInstaller();
            ti.Installers.Add(new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            });
            ti.Installers.Add(new ServiceInstaller
            {
                DisplayName = "�ǻ�Ѳ�쾵���",
                ServiceName = "Image",
                Description = "�ǻ�Ѳ�����һ����ӻ�����ϵͳ",
                //ServicesDependedOn = new string[] { "MSSQLSERVER" },//ǰ�÷���
                StartType = ServiceStartMode.Automatic//���з�ʽ
            });
            ti.Context = new InstallContext();
            ti.Context.Parameters["assemblypath"] = "\"" + Program.ExePath + "\" service";
            return ti;
        }

        private void btn_uninstall_Click(object sender, EventArgs e)
        {
            btn_start.Visible = btn_stop.Visible = false;
            btn_uninstall.Enabled = false;
            bool isok = false;
            Task.Run(() =>
            {
                using (var ti = TransactedInstaller())
                {
                    ti.Uninstall(null);
                }
                isok = true;
            }).ContinueWith((action =>
            {
                this.Invoke(new Action(() =>
                {
                    btn_uninstall.Enabled = true;
                    if (isok)
                    {
                        btn_uninstall.Visible = false;
                        label_state.Text = "δ��װ";
                        label_state.ForeColor = Color.Red;
                        btn_install.Visible = true;
                    }
                    else
                    {
                        btn_start.Visible = btn_stop.Visible = btn_uninstall.Visible = true;
                    }
                }));
            }));
        }

        private void btn_install_Click(object sender, EventArgs e)
        {
            if (Settings.ImagePath == null || Settings.ImagePath.Count == 0)
            {
                MessageBox.Show("���ȹ��ش���");
                return;
            }
            btn_install.Enabled = false;
            bool isok = false;
            Task.Run(() =>
            {
                using (var ti = TransactedInstaller())
                {
                    ti.Install(new Hashtable());
                }
                isok = true;
            }).ContinueWith((action =>
            {
                this.Invoke(new Action(() =>
                {
                    btn_install.Enabled = true;
                    if (isok)
                    {
                        label_state.Text = "δ����";
                        label_state.ForeColor = Color.Black;
                        btn_install.Visible = false;
                        btn_start.Visible = btn_uninstall.Visible = true;
                    }
                }));
            }));
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                relod();
            }
            if (service == null) { return; }
            btn_start.Enabled = false;
            bool isok = false;
            Task.Run(() =>
            {
                service.Start();
                isok = true;
            }).ContinueWith((action =>
            {
                this.Invoke(new Action(() =>
                {
                    btn_start.Enabled = true;
                    if (isok)
                    {
                        label_state.Text = "������";
                        label_state.ForeColor = Color.FromArgb(24, 144, 255);
                        btn_start.Visible = false;
                        btn_stop.Visible = true;
                    }
                }));
            }));
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                relod();
            }
            if (service == null) { return; }
            btn_stop.Enabled = false;
            bool isok = false;
            Task.Run(() =>
            {
                service.Stop();
                isok = true;
            }).ContinueWith((action =>
            {
                this.Invoke(new Action(() =>
                {
                    btn_stop.Enabled = true;
                    if (isok)
                    {
                        label_state.Text = "��ֹͣ";
                        label_state.ForeColor = Color.Black;
                        btn_stop.Visible = false;
                        btn_start.Visible = true;
                    }
                }));
            }));
        }

        void relod()
        {
            btn_install.Visible = btn_uninstall.Visible = btn_stop.Visible = btn_start.Visible = false;
            service = ServiceController.GetServices().ToList().Find(a => a.ServiceName == "Image");
            if (service == null)
            {
                btn_install.Visible = true;
                label_state.Text = "δ��װ";
                label_state.ForeColor = Color.Red;
            }
            else
            {
                btn_uninstall.Visible = true;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    btn_stop.Visible = true;
                    label_state.Text = "������";
                    label_state.ForeColor = Color.FromArgb(24, 144, 255);
                }
                else
                {
                    btn_start.Visible = true;
                    label_state.Text = "δ����";
                    label_state.ForeColor = Color.Black;
                }
            }
        }
        public void relodList()
        {
            lock (dataG.Items)
            {
                dataG.Items.Clear();
                var list = Settings.ImagePath;
                if (list != null)
                {
                    var drives = DriveInfo.GetDrives().ToList();
                    foreach (var item in list)
                    {
                        var drive = drives.Find(a => a.Name == item.root);
                        if (drive == null)
                        {
                            dataG.Items.Add(new TSkinList.TitleItem(dataG)
                            {
                                Tag = item.root,
                                Prog = 1,
                                Visible = true,
                                Name = item.path,
                                Desc = "�� - | ��ʹ�� - | ʣ�� -"
                            });
                        }
                        else
                        {
                            dataG.Items.Add(new TSkinList.TitleItem(dataG)
                            {
                                Tag = item.root,
                                Prog = (float)(((drive.TotalSize - drive.TotalFreeSpace) * 1.0) / (drive.TotalSize * 1.0)),
                                Visible = true,
                                Name = item.path,
                                Desc = $"�� {drive.TotalSize.ByteUnit()} | ��ʹ�� {(drive.TotalSize - drive.TotalFreeSpace).ByteUnit()} | ʣ�� {drive.TotalFreeSpace.ByteUnit()}"
                            });
                        }
                    }
                }
            }
            dataG.InPaint();
            dataG.Invalidate();
        }
        void LongTask()
        {
            while (true)
            {
                Thread.Sleep(5000);
                var drives = DriveInfo.GetDrives().ToList();
                lock (dataG.Items)
                {
                    foreach (var item in dataG.Items)
                    {
                        var drive = drives.Find(a => a.Name == item.Tag.ToString());
                        if (drive == null)
                        {
                            item.Prog = 1;
                            item.Desc = "�� - | ��ʹ�� - | ʣ�� -";
                        }
                        else
                        {
                            item.Prog = (float)(((drive.TotalSize - drive.TotalFreeSpace) * 1.0) / (drive.TotalSize * 1.0));
                            item.Desc = $"�� {drive.TotalSize.ByteUnit()} | ��ʹ�� {(drive.TotalSize - drive.TotalFreeSpace).ByteUnit()} | ʣ�� {drive.TotalFreeSpace.ByteUnit()}";
                        }
                    }
                }
            }
        }
        private void btn_setting_Click(object sender, EventArgs e)
        {
            using (var frm = new Setting(this))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }
    }
}
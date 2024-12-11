using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Image
{
    public partial class Main : AntdUI.BaseForm
    {
        public Main()
        {
            InitializeComponent();
        }
        public void FrmMove(object sender, MouseEventArgs e) => DraggableMouseDown();

        ServiceController? service = null;
        protected override void OnLoad(EventArgs e)
        {
            relodList();
            service = ServiceController.GetServices().ToList().Find(a => a.ServiceName == "Image");
            base.OnLoad(e);
            if (service == null)
            {
                btn_install.Visible = true;
                label_state.Text = "δ��װ";
                label_state.State = AntdUI.TState.Error;
            }
            else
            {
                btn_uninstall.Visible = true;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    btn_stop.Visible = true;
                    label_state.Text = "������";
                    label_state.State = AntdUI.TState.Success;
                }
                else
                {
                    btn_start.Visible = true;
                    label_state.Text = "δ����";
                    label_state.State = AntdUI.TState.Default;
                }
            }
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
                DisplayName = "����洢����",
                ServiceName = "Image",
                Description = "����洢��Ⱥ",
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
            btn_uninstall.Loading = true;
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
                btn_uninstall.Loading = false;
                if (isok)
                {
                    btn_uninstall.Visible = false;
                    label_state.Text = "δ��װ";
                    label_state.State = AntdUI.TState.Error;
                    btn_install.Visible = true;
                }
                else btn_start.Visible = btn_stop.Visible = btn_uninstall.Visible = true;
            }));
        }

        private void btn_install_Click(object sender, EventArgs e)
        {
            if (Settings.ImagePath == null || Settings.ImagePath.Count == 0)
            {
                AntdUI.Message.info(this, "���ȹ��ش���");
                return;
            }
            btn_install.Loading = true;
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
                btn_install.Loading = false;
                if (isok)
                {
                    label_state.Text = "δ����";
                    label_state.State = AntdUI.TState.Default;
                    btn_install.Visible = false;
                    btn_start.Visible = btn_uninstall.Visible = true;
                }
            }));
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (service == null) relod();
            if (service == null) return;
            btn_start.Loading = true;
            bool isok = false;
            Task.Run(() =>
            {
                service.Start();
                isok = true;
            }).ContinueWith((action =>
            {
                btn_start.Loading = false;
                if (isok)
                {
                    label_state.Text = "������";
                    label_state.State = AntdUI.TState.Success;
                    btn_start.Visible = false;
                    btn_stop.Visible = true;
                }
            }));
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (service == null) relod();
            if (service == null) return;
            btn_stop.Loading = true;
            bool isok = false;
            Task.Run(() =>
            {
                service.Stop();
                isok = true;
            }).ContinueWith((action =>
            {
                btn_stop.Loading = false;
                if (isok)
                {
                    label_state.Text = "��ֹͣ";
                    label_state.State = AntdUI.TState.Default;
                    btn_stop.Visible = false;
                    btn_start.Visible = true;
                }
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
                label_state.State = AntdUI.TState.Error;
            }
            else
            {
                btn_uninstall.Visible = true;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    btn_stop.Visible = true;
                    label_state.Text = "������";
                    label_state.State = AntdUI.TState.Success;
                }
                else
                {
                    btn_start.Visible = true;
                    label_state.Text = "δ����";
                    label_state.State = AntdUI.TState.Default;
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
                            dataG.Items.Add(new TSkin.TitleItem(dataG)
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
                            dataG.Items.Add(new TSkin.TitleItem(dataG)
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
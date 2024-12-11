namespace Image
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            btn_install = new AntdUI.Button();
            btn_uninstall = new AntdUI.Button();
            btn_setting = new AntdUI.Button();
            btn_start = new AntdUI.Button();
            btn_stop = new AntdUI.Button();
            panel_top = new Panel();
            label_title = new AntdUI.PageHeader();
            label_state = new AntdUI.Badge();
            dataG = new TSkin.TitleList();
            panel_top.SuspendLayout();
            SuspendLayout();
            // 
            // btn_install
            // 
            btn_install.AutoSizeMode = AntdUI.TAutoSize.Width;
            btn_install.Dock = DockStyle.Right;
            btn_install.IconSvg = "AppstoreAddOutlined";
            btn_install.Location = new Point(543, 0);
            btn_install.Name = "btn_install";
            btn_install.Size = new Size(95, 38);
            btn_install.TabIndex = 5;
            btn_install.Text = "安装";
            btn_install.Type = AntdUI.TTypeMini.Primary;
            btn_install.Visible = false;
            btn_install.Click += btn_install_Click;
            // 
            // btn_uninstall
            // 
            btn_uninstall.AutoSizeMode = AntdUI.TAutoSize.Width;
            btn_uninstall.Dock = DockStyle.Right;
            btn_uninstall.IconSvg = "DeleteOutlined";
            btn_uninstall.Location = new Point(448, 0);
            btn_uninstall.Name = "btn_uninstall";
            btn_uninstall.Size = new Size(95, 38);
            btn_uninstall.TabIndex = 4;
            btn_uninstall.Text = "卸载";
            btn_uninstall.Type = AntdUI.TTypeMini.Error;
            btn_uninstall.Visible = false;
            btn_uninstall.Click += btn_uninstall_Click;
            // 
            // btn_setting
            // 
            btn_setting.AutoSizeMode = AntdUI.TAutoSize.Width;
            btn_setting.BorderWidth = 2F;
            btn_setting.Dock = DockStyle.Right;
            btn_setting.IconSvg = "SettingOutlined";
            btn_setting.Location = new Point(163, 0);
            btn_setting.Name = "btn_setting";
            btn_setting.Size = new Size(95, 38);
            btn_setting.TabIndex = 1;
            btn_setting.Text = "设置";
            btn_setting.Click += btn_setting_Click;
            // 
            // btn_start
            // 
            btn_start.AutoSizeMode = AntdUI.TAutoSize.Width;
            btn_start.BorderWidth = 2F;
            btn_start.Dock = DockStyle.Right;
            btn_start.Ghost = true;
            btn_start.IconSvg = "PlaySquareOutlined";
            btn_start.Location = new Point(258, 0);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(95, 38);
            btn_start.TabIndex = 2;
            btn_start.Text = "启动";
            btn_start.Type = AntdUI.TTypeMini.Primary;
            btn_start.Visible = false;
            btn_start.Click += btn_start_Click;
            // 
            // btn_stop
            // 
            btn_stop.AutoSizeMode = AntdUI.TAutoSize.Width;
            btn_stop.BorderWidth = 2F;
            btn_stop.Dock = DockStyle.Right;
            btn_stop.Ghost = true;
            btn_stop.IconSvg = "StopOutlined";
            btn_stop.Location = new Point(353, 0);
            btn_stop.Name = "btn_stop";
            btn_stop.Size = new Size(95, 38);
            btn_stop.TabIndex = 3;
            btn_stop.Text = "停止";
            btn_stop.Type = AntdUI.TTypeMini.Error;
            btn_stop.Visible = false;
            btn_stop.Click += btn_stop_Click;
            // 
            // panel_top
            // 
            panel_top.Controls.Add(label_title);
            panel_top.Controls.Add(label_state);
            panel_top.Controls.Add(btn_setting);
            panel_top.Controls.Add(btn_start);
            panel_top.Controls.Add(btn_stop);
            panel_top.Controls.Add(btn_uninstall);
            panel_top.Controls.Add(btn_install);
            panel_top.Dock = DockStyle.Top;
            panel_top.Location = new Point(0, 0);
            panel_top.Name = "panel_top";
            panel_top.Size = new Size(638, 38);
            panel_top.TabIndex = 0;
            panel_top.MouseDown += FrmMove;
            // 
            // label_title
            // 
            label_title.Dock = DockStyle.Fill;
            label_title.Location = new Point(0, 0);
            label_title.Name = "label_title";
            label_title.Size = new Size(140, 38);
            label_title.SubText = "图片、视频 存储服务";
            label_title.TabIndex = 0;
            label_title.Text = "存储镜像服务";
            label_title.MouseDown += FrmMove;
            // 
            // label_state
            // 
            label_state.AutoSizeMode = AntdUI.TAutoSize.Width;
            label_state.Dock = DockStyle.Right;
            label_state.Location = new Point(140, 0);
            label_state.Name = "label_state";
            label_state.Size = new Size(23, 38);
            label_state.TabIndex = 0;
            label_state.MouseDown += FrmMove;
            // 
            // dataG
            // 
            dataG.Dock = DockStyle.Fill;
            dataG.Location = new Point(0, 38);
            dataG.Name = "dataG";
            dataG.Size = new Size(638, 309);
            dataG.TabIndex = 1;
            // 
            // Main
            // 
            BackColor = Color.FromArgb(250, 250, 250);
            ClientSize = new Size(638, 347);
            Controls.Add(dataG);
            Controls.Add(panel_top);
            Font = new Font("Microsoft YaHei UI", 12F);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(648, 380);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "存储镜像服务";
            MouseDown += FrmMove;
            panel_top.ResumeLayout(false);
            panel_top.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Button btn_install;
        private AntdUI.Button btn_uninstall;
        private AntdUI.Button btn_start;
        private AntdUI.Button btn_stop;
        private AntdUI.Button btn_setting;
        private AntdUI.PageHeader label_title;
        private Panel panel_top;
        private AntdUI.Badge label_state;
        private TSkin.TitleList dataG;
    }
}
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
            this.btn_install = new TSkin.TBut();
            this.btn_uninstall = new TSkin.TBut();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_desc = new System.Windows.Forms.Label();
            this.panel_state = new System.Windows.Forms.Panel();
            this.label_state = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.btn_setting = new TSkin.TBut();
            this.btn_start = new TSkin.TBut();
            this.btn_stop = new TSkin.TBut();
            this.dataG = new TSkinList.TitleList();
            this.panel3.SuspendLayout();
            this.panel_state.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_install
            // 
            this.btn_install.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btn_install.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(105)))), ((int)(((byte)(236)))));
            this.btn_install.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.btn_install.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_install.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_install.ForeColor = System.Drawing.Color.White;
            this.btn_install.Location = new System.Drawing.Point(554, 6);
            this.btn_install.Name = "btn_install";
            this.btn_install.Size = new System.Drawing.Size(72, 25);
            this.btn_install.TabIndex = 13;
            this.btn_install.Text = "▬ 安装";
            this.btn_install.Visible = false;
            this.btn_install.Click += new System.EventHandler(this.btn_install_Click);
            // 
            // btn_uninstall
            // 
            this.btn_uninstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btn_uninstall.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btn_uninstall.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_uninstall.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_uninstall.ForeColor = System.Drawing.Color.Red;
            this.btn_uninstall.Location = new System.Drawing.Point(482, 6);
            this.btn_uninstall.Name = "btn_uninstall";
            this.btn_uninstall.Size = new System.Drawing.Size(72, 25);
            this.btn_uninstall.TabIndex = 12;
            this.btn_uninstall.Text = "▧ 卸载";
            this.btn_uninstall.Visible = false;
            this.btn_uninstall.Click += new System.EventHandler(this.btn_uninstall_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label_desc);
            this.panel3.Controls.Add(this.panel_state);
            this.panel3.Controls.Add(this.label_title);
            this.panel3.Controls.Add(this.btn_setting);
            this.panel3.Controls.Add(this.btn_start);
            this.panel3.Controls.Add(this.btn_stop);
            this.panel3.Controls.Add(this.btn_uninstall);
            this.panel3.Controls.Add(this.btn_install);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(6);
            this.panel3.Size = new System.Drawing.Size(632, 37);
            this.panel3.TabIndex = 0;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label_desc
            // 
            this.label_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_desc.ForeColor = System.Drawing.Color.Gray;
            this.label_desc.Location = new System.Drawing.Point(170, 6);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(0, 25);
            this.label_desc.TabIndex = 0;
            this.label_desc.Text = "图片、视频 存储服务";
            this.label_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_desc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // panel_state
            // 
            this.panel_state.Controls.Add(this.label_state);
            this.panel_state.Controls.Add(this.label6);
            this.panel_state.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_state.Location = new System.Drawing.Point(156, 6);
            this.panel_state.Name = "panel_state";
            this.panel_state.Size = new System.Drawing.Size(130, 25);
            this.panel_state.TabIndex = 0;
            // 
            // label_state
            // 
            this.label_state.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_state.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_state.Location = new System.Drawing.Point(63, 0);
            this.label_state.Name = "label_state";
            this.label_state.Size = new System.Drawing.Size(67, 25);
            this.label_state.TabIndex = 0;
            this.label_state.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_state.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "状态：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_title.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_title.Location = new System.Drawing.Point(6, 6);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(164, 25);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "存储镜像服务";
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // btn_setting
            // 
            this.btn_setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btn_setting.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btn_setting.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_setting.Location = new System.Drawing.Point(286, 6);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(52, 25);
            this.btn_setting.TabIndex = 14;
            this.btn_setting.Text = "设置";
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btn_start.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btn_start.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_start.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(105)))), ((int)(((byte)(236)))));
            this.btn_start.Location = new System.Drawing.Point(338, 6);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(72, 25);
            this.btn_start.TabIndex = 10;
            this.btn_start.Text = "▶ 启动";
            this.btn_start.Visible = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.Color.Red;
            this.btn_stop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_stop.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_stop.ForeColor = System.Drawing.Color.White;
            this.btn_stop.Location = new System.Drawing.Point(410, 6);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(72, 25);
            this.btn_stop.TabIndex = 11;
            this.btn_stop.Text = "■ 停止";
            this.btn_stop.Visible = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // dataG
            // 
            this.dataG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataG.Location = new System.Drawing.Point(0, 37);
            this.dataG.Name = "dataG";
            this.dataG.SelectItem = null;
            this.dataG.Size = new System.Drawing.Size(632, 304);
            this.dataG.TabIndex = 1;
            // 
            // Main
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(632, 341);
            this.Controls.Add(this.dataG);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::Image.Properties.Resources.favicon;
            this.MinimumSize = new System.Drawing.Size(648, 380);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "存储镜像服务";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel_state.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private TSkin.TBut btn_install;
        private TSkin.TBut btn_uninstall;
        private Label label_title;
        private Label label_desc;
        private Panel panel3;
        private Panel panel_state;
        private Label label_state;
        private Label label6;
        private TSkin.TBut btn_start;
        private TSkin.TBut btn_stop;
        private TSkinList.TitleList dataG;
        private TSkin.TBut btn_setting;
    }
}
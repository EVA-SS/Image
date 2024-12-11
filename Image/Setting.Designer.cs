namespace Image
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            winBar = new AntdUI.PageHeader();
            btn_save = new AntdUI.Button();
            label1 = new Label();
            txt_port = new AntdUI.Input();
            check_log = new AntdUI.Checkbox();
            btn_add = new AntdUI.Button();
            label2 = new Label();
            txt_limitSize = new AntdUI.Input();
            label3 = new Label();
            SuspendLayout();
            // 
            // winBar
            // 
            winBar.DividerShow = true;
            winBar.Dock = DockStyle.Top;
            winBar.Location = new Point(0, 0);
            winBar.Name = "winBar";
            winBar.ShowBack = true;
            winBar.Size = new Size(356, 38);
            winBar.SubText = "配置 端口、挂载磁盘";
            winBar.TabIndex = 0;
            winBar.Text = "存储镜像服务";
            winBar.BackClick += winBar_BackClick;
            // 
            // btn_save
            // 
            btn_save.Dock = DockStyle.Bottom;
            btn_save.Location = new Point(0, 183);
            btn_save.Name = "btn_save";
            btn_save.Radius = 0;
            btn_save.Size = new Size(356, 37);
            btn_save.TabIndex = 10;
            btn_save.Text = "保存";
            btn_save.Type = AntdUI.TTypeMini.Primary;
            btn_save.WaveSize = 0;
            btn_save.Click += btn_save_Click;
            // 
            // label1
            // 
            label1.Location = new Point(30, 60);
            label1.Name = "label1";
            label1.Size = new Size(74, 24);
            label1.TabIndex = 0;
            label1.Text = "服务端口";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_port
            // 
            txt_port.Location = new Point(111, 52);
            txt_port.Name = "txt_port";
            txt_port.PlaceholderText = "端口";
            txt_port.Size = new Size(90, 40);
            txt_port.TabIndex = 0;
            // 
            // check_log
            // 
            check_log.AutoSizeMode = AntdUI.TAutoSize.Auto;
            check_log.Location = new Point(224, 51);
            check_log.Name = "check_log";
            check_log.RightToLeft = RightToLeft.Yes;
            check_log.Size = new Size(82, 43);
            check_log.TabIndex = 1;
            check_log.Text = "日志";
            // 
            // btn_add
            // 
            btn_add.BorderWidth = 2F;
            btn_add.Location = new Point(216, 104);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(99, 35);
            btn_add.TabIndex = 3;
            btn_add.Text = "新增挂载";
            btn_add.Click += btn_add_Click;
            // 
            // label2
            // 
            label2.Location = new Point(30, 109);
            label2.Name = "label2";
            label2.Size = new Size(74, 24);
            label2.TabIndex = 0;
            label2.Text = "限制大小";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_limitSize
            // 
            txt_limitSize.Location = new Point(111, 101);
            txt_limitSize.Name = "txt_limitSize";
            txt_limitSize.PlaceholderText = "端口";
            txt_limitSize.Size = new Size(90, 40);
            txt_limitSize.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 9F);
            label3.ForeColor = Color.DimGray;
            label3.Location = new Point(41, 142);
            label3.Name = "label3";
            label3.Size = new Size(184, 17);
            label3.TabIndex = 0;
            label3.Text = "K=KB、M=MB、G=GB、T=TB";
            // 
            // Setting
            // 
            BackColor = Color.FromArgb(250, 250, 250);
            ClientSize = new Size(356, 220);
            Controls.Add(btn_add);
            Controls.Add(btn_save);
            Controls.Add(check_log);
            Controls.Add(txt_limitSize);
            Controls.Add(txt_port);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(winBar);
            Controls.Add(label3);
            Font = new Font("Microsoft YaHei UI", 12F);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(356, 220);
            Name = "Setting";
            StartPosition = FormStartPosition.CenterParent;
            Text = "设置";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private AntdUI.PageHeader winBar;
        private AntdUI.Button btn_save;
        private Label label1;
        private AntdUI.Input txt_port;
        private AntdUI.Checkbox check_log;
        private AntdUI.Button btn_add;
        private Label label2;
        private AntdUI.Input txt_limitSize;
        private Label label3;
    }
}
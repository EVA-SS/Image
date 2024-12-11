namespace Image
{
    partial class AddPath
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPath));
            winBar = new AntdUI.PageHeader();
            btn_open = new AntdUI.Button();
            btn_save = new AntdUI.Button();
            txt_path = new AntdUI.Input();
            label1 = new AntdUI.Label();
            SuspendLayout();
            // 
            // winBar
            // 
            winBar.DividerShow = true;
            winBar.Dock = DockStyle.Top;
            winBar.Location = new Point(0, 0);
            winBar.Name = "winBar";
            winBar.ShowBack = true;
            winBar.Size = new Size(574, 38);
            winBar.SubText = "配置 新挂载磁盘";
            winBar.TabIndex = 0;
            winBar.Text = "存储镜像服务";
            winBar.BackClick += winBar_BackClick;
            // 
            // btn_open
            // 
            btn_open.AutoSizeMode = AntdUI.TAutoSize.Width;
            btn_open.BorderWidth = 2F;
            btn_open.Dock = DockStyle.Right;
            btn_open.JoinLeft = true;
            btn_open.Location = new Point(515, 38);
            btn_open.Name = "btn_open";
            btn_open.Padding = new Padding(0, 4, 0, 4);
            btn_open.Size = new Size(59, 49);
            btn_open.TabIndex = 15;
            btn_open.Text = "打开";
            btn_open.Click += btn_open_Click;
            // 
            // btn_save
            // 
            btn_save.Dock = DockStyle.Bottom;
            btn_save.Enabled = false;
            btn_save.Location = new Point(0, 110);
            btn_save.Name = "btn_save";
            btn_save.Radius = 0;
            btn_save.Size = new Size(574, 36);
            btn_save.TabIndex = 10;
            btn_save.Text = "新增挂载";
            btn_save.Type = AntdUI.TTypeMini.Primary;
            btn_save.WaveSize = 0;
            btn_save.Click += btn_save_Click;
            // 
            // txt_path
            // 
            txt_path.Dock = DockStyle.Fill;
            txt_path.JoinRight = true;
            txt_path.Location = new Point(0, 38);
            txt_path.Name = "txt_path";
            txt_path.Padding = new Padding(0, 4, 0, 4);
            txt_path.PlaceholderText = "挂载路径";
            txt_path.Size = new Size(515, 49);
            txt_path.TabIndex = 2;
            txt_path.TextChanged += txt_path_TextChanged;
            // 
            // label1
            // 
            label1.AutoSizeMode = AntdUI.TAutoSize.Auto;
            label1.Dock = DockStyle.Bottom;
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(0, 87);
            label1.Name = "label1";
            label1.Size = new Size(46, 23);
            label1.TabIndex = 0;
            // 
            // AddPath
            // 
            BackColor = Color.FromArgb(250, 250, 250);
            ClientSize = new Size(574, 146);
            Controls.Add(txt_path);
            Controls.Add(btn_open);
            Controls.Add(winBar);
            Controls.Add(label1);
            Controls.Add(btn_save);
            Font = new Font("Microsoft YaHei UI", 12F);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(500, 146);
            Name = "AddPath";
            StartPosition = FormStartPosition.CenterParent;
            Text = "新增挂载";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private AntdUI.PageHeader winBar;
        private AntdUI.Button btn_save;
        private AntdUI.Input txt_path;
        private AntdUI.Button btn_open;
        private AntdUI.Label label1;
    }
}
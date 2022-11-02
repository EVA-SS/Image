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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_desc = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.btn_save = new TSkin.TBut();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.check_log = new System.Windows.Forms.CheckBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_limitSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label_desc);
            this.panel3.Controls.Add(this.label_title);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(6);
            this.panel3.Size = new System.Drawing.Size(340, 37);
            this.panel3.TabIndex = 0;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label_desc
            // 
            this.label_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_desc.ForeColor = System.Drawing.Color.Gray;
            this.label_desc.Location = new System.Drawing.Point(170, 6);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(164, 25);
            this.label_desc.TabIndex = 0;
            this.label_desc.Text = "配置 端口、挂载磁盘";
            this.label_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_desc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
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
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btn_save.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(105)))), ((int)(((byte)(236)))));
            this.btn_save.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.Location = new System.Drawing.Point(0, 179);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(340, 30);
            this.btn_save.TabIndex = 10;
            this.btn_save.Text = "保存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务端口";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // txt_port
            // 
            this.txt_port.BackColor = System.Drawing.Color.White;
            this.txt_port.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_port.ForeColor = System.Drawing.Color.Black;
            this.txt_port.Location = new System.Drawing.Point(111, 63);
            this.txt_port.Name = "txt_port";
            this.txt_port.PlaceholderText = "端口";
            this.txt_port.Size = new System.Drawing.Size(50, 21);
            this.txt_port.TabIndex = 0;
            // 
            // check_log
            // 
            this.check_log.AutoSize = true;
            this.check_log.Location = new System.Drawing.Point(216, 61);
            this.check_log.Name = "check_log";
            this.check_log.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.check_log.Size = new System.Drawing.Size(61, 25);
            this.check_log.TabIndex = 1;
            this.check_log.Text = "日志";
            this.check_log.UseVisualStyleBackColor = true;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(216, 107);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(99, 28);
            this.btn_add.TabIndex = 3;
            this.btn_add.Text = "新增挂载";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(31, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "限制大小";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // txt_limitSize
            // 
            this.txt_limitSize.BackColor = System.Drawing.Color.White;
            this.txt_limitSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_limitSize.ForeColor = System.Drawing.Color.Black;
            this.txt_limitSize.Location = new System.Drawing.Point(111, 111);
            this.txt_limitSize.Name = "txt_limitSize";
            this.txt_limitSize.PlaceholderText = "端口";
            this.txt_limitSize.Size = new System.Drawing.Size(50, 21);
            this.txt_limitSize.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(41, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "K=KB、M=MB、G=GB、T=TB";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // Setting
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(340, 209);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.check_log);
            this.Controls.Add(this.txt_limitSize);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::Image.Properties.Resources.favicon;
            this.MinimumSize = new System.Drawing.Size(356, 248);
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label_title;
        private Label label_desc;
        private Panel panel3;
        private TSkin.TBut btn_save;
        private Label label1;
        private TextBox txt_port;
        private CheckBox check_log;
        private Button btn_add;
        private Label label2;
        private TextBox txt_limitSize;
        private Label label3;
    }
}
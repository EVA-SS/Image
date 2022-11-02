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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_desc = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.btn_open = new TSkin.TBut();
            this.btn_save = new TSkin.TBut();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label_desc);
            this.panel3.Controls.Add(this.label_title);
            this.panel3.Controls.Add(this.btn_open);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(6);
            this.panel3.Size = new System.Drawing.Size(574, 37);
            this.panel3.TabIndex = 0;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label_desc
            // 
            this.label_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_desc.ForeColor = System.Drawing.Color.Gray;
            this.label_desc.Location = new System.Drawing.Point(170, 6);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(346, 25);
            this.label_desc.TabIndex = 0;
            this.label_desc.Text = "配置 新挂载磁盘";
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
            // btn_open
            // 
            this.btn_open.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btn_open.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btn_open.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_open.Location = new System.Drawing.Point(516, 6);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(52, 25);
            this.btn_open.TabIndex = 15;
            this.btn_open.Text = "打开";
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btn_save.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(105)))), ((int)(((byte)(236)))));
            this.btn_save.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_save.Enabled = false;
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.Location = new System.Drawing.Point(0, 91);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(574, 30);
            this.btn_save.TabIndex = 10;
            this.btn_save.Text = "新增挂载";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_path
            // 
            this.txt_path.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_path.BackColor = System.Drawing.Color.White;
            this.txt_path.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_path.ForeColor = System.Drawing.Color.Black;
            this.txt_path.Location = new System.Drawing.Point(12, 43);
            this.txt_path.Name = "txt_path";
            this.txt_path.PlaceholderText = "挂载路径";
            this.txt_path.Size = new System.Drawing.Size(550, 21);
            this.txt_path.TabIndex = 2;
            this.txt_path.TextChanged += new System.EventHandler(this.txt_path_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 21);
            this.label1.TabIndex = 0;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // AddPath
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(574, 121);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::Image.Properties.Resources.favicon;
            this.MinimumSize = new System.Drawing.Size(500, 140);
            this.Name = "AddPath";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增挂载";
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
        private TextBox txt_path;
        private TSkin.TBut btn_open;
        private Label label1;
    }
}
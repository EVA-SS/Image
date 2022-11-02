using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TSkin
{
    [ToolboxBitmap(typeof(Button))]
    public partial class TBut : Control, IButtonControl
    {
        #region 构造函数

        public TBut()
        {
            _borderFocus.DashStyle = DashStyle.Dash;
            this.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.Selectable |
               ControlStyles.DoubleBuffer |
               ControlStyles.SupportsTransparentBackColor |
               ControlStyles.ContainerControl |
               ControlStyles.UserPaint, true);
            this.UpdateStyles();
            base.BackColor = Color.Transparent;
        }

        #endregion

        #region 属性

        #region 普通状态

        public int PreferredWidth
        {
            get
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    return g.MeasureString(Text, this.Font).GetSize().Width;
                }
            }
        }

        bool _HasFocus = true;
        [Description("是否带激活样式"), Category("TSkin"), DefaultValue(true)]
        public bool HasFocus
        {
            get { return _HasFocus; }
            set
            {
                if (_HasFocus != value)
                {
                    _HasFocus = value;
                }
            }
        }

        [Description("文字"), Category("TSkin"), DefaultValue(null)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public override string Text
        {
            get => base.Text;
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        bool _IsShadow = false;
        [Description("是否显示阴影"), Category("TSkin"), DefaultValue(false)]
        public bool IsShadow
        {
            get { return _IsShadow; }
            set
            {
                if (_IsShadow != value)
                {
                    _IsShadow = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }


        int _ShadowWidth = 8;
        [Description("阴影宽度"), Category("TSkin"), DefaultValue(8)]
        public int ShadowWidth
        {
            get { return _ShadowWidth; }
            set
            {
                if (_ShadowWidth != value)
                {
                    _ShadowWidth = value;
                    if (_IsShadow)
                    {
                        this.OnSizeChanged(null);
                        this.Invalidate();
                    }
                }
            }
        }

        SolidBrush shadowbrush = new SolidBrush(Color.Black);
        [Description("阴影颜色"), Category("TSkin"), DefaultValue(typeof(Color), "Black")]
        public Color ShadowColor
        {
            get => shadowbrush.Color;
            set
            {
                if (shadowbrush.Color != value)
                {
                    shadowbrush.Color = value;
                    if (_IsShadow)
                    {
                        this.OnSizeChanged(null);
                        this.Invalidate();
                    }
                }
            }
        }


        bool _MultiLine = false;
        [Description("是否多行"), Category("TSkin"), DefaultValue(false)]
        public bool MultiLine
        {
            get { return _MultiLine; }
            set
            {
                if (_MultiLine != value)
                {
                    _MultiLine = value;
                    stringFormat.FormatFlags = value ? 0 : StringFormatFlags.NoWrap;
                    this.Invalidate();
                }
            }
        }

        Padding _TextMargin = new Padding(0);
        [Description("文字边距"), Category("TSkin"), DefaultValue(typeof(Padding), "0, 0, 0, 0")]
        public Padding TextMargin
        {
            get { return _TextMargin; }
            set
            {
                if (_TextMargin != value)
                {
                    _TextMargin = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }



        private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;
        [Description("字体位置"), Category("TSkin"), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextAlign
        {
            get => _TextAlign;
            set
            {
                if (_TextAlign != value)
                {
                    _TextAlign = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }


        Color backcolor1 = Color.FromArgb(20, 0, 0, 0);
        Color backcolor2 = Color.FromArgb(20, 0, 0, 0);
        [Description("背景颜色"), Category("TSkin"), DefaultValue(typeof(Color), "20, 0, 0, 0")]
        public new Color BackColor
        {
            get => backcolor1;
            set
            {
                if (backcolor1 != value)
                {
                    backcolor1 = value;
                    if (backcolor1 != Color.Transparent && backcolor2 != Color.Transparent)
                    {
                        if (rectmF.Width > 0 && rectmF.Height > 0)
                        {
                            backbrush = new LinearGradientBrush(rectmF, backcolor1, backcolor2, LinearGradientMode.Horizontal);
                        }
                    }
                    else
                    {
                        backbrush = new SolidBrush(backcolor1);
                    }
                    this.Invalidate();
                }
            }
        }

        [Description("背景渐变色"), Category("TSkin"), DefaultValue(typeof(Color), "20, 0, 0, 0")]
        public Color BackColor2
        {
            get => backcolor2;
            set
            {
                if (backcolor2 != value)
                {
                    backcolor2 = value;
                    SetBrush(ref backbrush, backcolor1, backcolor2);
                    this.Invalidate();
                }
            }
        }

        bool badge = false;
        [Description("是否显示角标"), Category("TSkin"), DefaultValue(false)]
        public bool Badge
        {
            get { return badge; }
            set
            {
                if (badge != value)
                {
                    badge = value;
                    this.Invalidate();
                }
            }
        }

        Pen badgePColor = new Pen(Color.FromArgb(112, 237, 58), 2f);
        SolidBrush badgeColor = new SolidBrush(Color.FromArgb(112, 237, 58));
        [Description("角标颜色"), Category("TSkin"), DefaultValue(typeof(Color), "112, 237, 58")]
        public Color BadgeColor
        {
            get { return badgeColor.Color; }
            set
            {
                if (badgeColor.Color != value)
                {
                    badgePColor.Color = badgeColor.Color = value;
                    if (badge)
                    {
                        this.Invalidate();
                    }
                }
            }
        }
        int badgeWidth = 10;
        [Description("角标大小"), Category("TSkin"), DefaultValue(10)]
        public int BadgeSize
        {
            get { return badgeWidth; }
            set
            {
                if (badgeWidth != value)
                {
                    badgeWidth = value;
                    if (badge)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        [Description("边框颜色"), Category("TSkin"), DefaultValue(typeof(Color), "Transparent")]
        public Color BorderColor
        {
            get => _border.Color;
            set
            {
                if (_border.Color != value)
                {
                    _border.Color = value;
                    this.Invalidate();
                }
            }
        }

        float _BorderWidth = 0;
        [Description("边框宽度"), Category("TSkin"), DefaultValue(0f)]
        public float BorderWidth
        {
            get { return _BorderWidth; }
            set
            {
                if (_BorderWidth != value)
                {
                    _border.Width = _Activeborder.Width = value;
                    _BorderWidth = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        int _radius = 0;
        [Description("圆角"), Category("TSkin"), DefaultValue(0)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value;

                    this.Invalidate();
                }
            }
        }

        float _enabledOpacity = 0.4f;
        [Description("禁用透明度"), Category("TSkin"), DefaultValue(0.4f)]
        public float EnabledOpacity
        {
            get { return _enabledOpacity; }
            set
            {
                if (_enabledOpacity != value)
                {
                    _enabledOpacity = value;
                    if (!base.Enabled)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        #endregion

        #region 移动

        SolidBrush backhovebrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
        [Description("鼠标移入颜色"), Category("TSkin"), DefaultValue(typeof(Color), "20, 0, 0, 0")]
        public Color BackColorHover
        {
            get => backhovebrush.Color;
            set
            {
                if (backhovebrush.Color != value)
                {
                    backhovebrush.Color = value;
                    if (IsHove)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        #endregion

        #region 焦点

        [Description("焦点边框颜色"), Category("TSkin"), DefaultValue(typeof(Color), "200, 0, 0, 0")]
        public Color BorderColorFocus
        {
            get => _borderFocus.Color;
            set
            {
                if (_borderFocus.Color != value)
                {
                    _borderFocus.Color = value;
                    if (IsFocus)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        #endregion

        #region 激活

        private bool _isActive = false;
        [Description("激活状态"), Category("TSkin"), DefaultValue(false)]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;

                    this.Invalidate();
                }
            }
        }

        [Description("激活后字体颜色"), Category("TSkin"), DefaultValue(typeof(Color), "Transparent")]
        public Color ForeColorActive
        {
            get => foreActivebrush;
            set
            {
                if (foreActivebrush != value)
                {
                    foreActivebrush = value;
                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }


        Color backcolorsel1 = Color.FromArgb(100, 0, 0, 0);
        Color backcolorsel2 = Color.Transparent;
        [Description("激活后背景颜色"), Category("TSkin"), DefaultValue(typeof(Color), "100, 0, 0, 0")]
        public Color BackColorActive
        {
            get => backcolorsel1;
            set
            {
                if (backcolorsel1 != value)
                {
                    backcolorsel1 = value;


                    SetBrush(ref backselbrush, backcolorsel1, backcolorsel2);

                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }


        [Description("激活后背景渐变色"), Category("TSkin"), DefaultValue(typeof(Color), "Transparent")]

        public Color BackColorActive2
        {
            get => backcolorsel2;
            set
            {
                if (backcolorsel2 != value)
                {
                    backcolorsel2 = value;

                    SetBrush(ref backselbrush, backcolorsel1, backcolorsel2);

                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        [Description("激活后边框颜色"), Category("TSkin"), DefaultValue(typeof(Color), "Transparent")]
        public Color BorderColorActive
        {
            get => _Activeborder.Color;
            set
            {
                if (_Activeborder.Color != value)
                {
                    _Activeborder.Color = value;
                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        #endregion

        void SetBrush(ref Brush brush, Color color1, Color color2)
        {
            bool isOk = false;
            if (color1 == color2)
            {
                if (brush != null)
                {
                    brush.Dispose();
                }
                brush = new SolidBrush(color1);
            }
            else if (color1 != Color.Transparent || color2 != Color.Transparent)
            {
                if (rectmF.Width > 0 && rectmF.Height > 0)
                {
                    isOk = true;
                    if (brush != null)
                    {
                        brush.Dispose();
                    }
                    brush = new LinearGradientBrush(rectmF, color1, color2, LinearGradientMode.Horizontal);
                }
            }
            if (!isOk)
            {
                if (brush != null)
                {
                    if (brush is SolidBrush && (brush as SolidBrush).Color == color1)
                    {
                        return;
                    }
                    else
                    {
                        brush.Dispose();
                    }
                }
                brush = new SolidBrush(color1);
            }
        }

        #endregion

        #region 坐标布局

        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };

        RectangleF rectmF;
        Rectangle rectByFont;
        protected override void OnSizeChanged(EventArgs e)
        {
            Rectangle rect = ClientRectangle;
            if (_IsShadow)
            {
                var b2 = _ShadowWidth / 2f;
                var _rect = new RectangleF(b2, b2, rect.Width - _ShadowWidth, rect.Height - _ShadowWidth);
                SizeChange(rect, _rect);
            }
            else
            {
                if (_BorderWidth == 0)
                {
                    SizeChange(rect, rect);
                }
                else
                {
                    var b2 = _BorderWidth / 2f;
                    var _rect = new RectangleF(b2, b2, rect.Width - _BorderWidth, rect.Height - _BorderWidth);
                    SizeChange(rect, _rect);
                }
            }

            base.OnSizeChanged(e);
        }

        void SizeChange(Rectangle rect, RectangleF brect)
        {
            //int HasX = _ImageMargin.Left, HasY = _ImageMargin.Top, HasX2 = _ImageMargin.Bottom, HasY2 = _ImageMargin.Right;
            SizeF fontSizeF = TextRenderer.MeasureText(string.IsNullOrEmpty(this.Text) ? "Qq森甩" : this.Text, this.Font);
            Size fontSize = new Size((int)Math.Ceiling(fontSizeF.Width), (int)Math.Ceiling(fontSizeF.Height));
            rectByFont = new Rectangle(rect.X + _TextMargin.Left, rect.Y + _TextMargin.Top, rect.Width - (_TextMargin.Left + _TextMargin.Right), rect.Height - (_TextMargin.Top + _TextMargin.Bottom));

            switch (_TextAlign)
            {
                case ContentAlignment.TopLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    //内容在垂直方向上顶部对齐，在水平方向上左边对齐
                    break;
                case ContentAlignment.TopCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    //内容在垂直方向上顶部对齐，在水平方向上居中对齐

                    break;
                case ContentAlignment.TopRight:
                    //内容在垂直方向上顶部对齐，在水平方向上右边对齐
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    //内容在垂直方向上中间对齐，在水平方向上左边对齐
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    break;
                case ContentAlignment.MiddleCenter:
                    //内容在垂直方向上中间对齐，在水平方向上居中对齐
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    //内容在垂直方向上中间对齐，在水平方向上右边对齐
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    break;
                case ContentAlignment.BottomLeft:
                    //内容在垂直方向上底边对齐，在水平方向上左边对齐
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    //内容在垂直方向上底边对齐，在水平方向上居中对齐
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Far;

                    break;
                case ContentAlignment.BottomRight:
                    //内容在垂直方向上底边对齐，在水平方向上右边对齐
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
            }
            rectmF = brect;

            SetBrush(ref backbrush, backcolor1, backcolor2);
            SetBrush(ref backselbrush, backcolorsel1, backcolorsel2);

        }

        #endregion

        Pen _borderFocus = new Pen(Color.FromArgb(200, 0, 0, 0), 1);
        Pen _border = new Pen(Color.Transparent, 1), _Activeborder = new Pen(Color.Transparent, 1);
        Color foreActivebrush = Color.Transparent;
        Brush backbrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)), backselbrush = new SolidBrush(Color.Transparent);

        #region 模拟点击

        DialogResult dialogResult = DialogResult.None;

        [DefaultValue(typeof(DialogResult), "None")]
        public DialogResult DialogResult
        {
            get { return dialogResult; }
            set
            {
                if (dialogResult != value)
                {
                    dialogResult = value;
                }
            }
        }
        public void PerformClick()
        {
            this.OnClick(EventArgs.Empty);
        }
        public void NotifyDefault(bool value)
        {

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 32)
            {
                //回车
                e.Handled = true;
                OnClick(EventArgs.Empty);
                this.Focus();
                //this.Invalidate();
            }
            base.OnKeyPress(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }


        #endregion

        bool _IsHove = false, _IsFocus = false;

        [Description("是否移入"), Category("TSkin"), DefaultValue(false)]
        public bool IsHove
        {
            get => _IsHove;
            set
            {
                if (_IsHove != value)
                {
                    _IsHove = value;
                    this.Invalidate();
                }
            }
        }

        [Description("是否焦点"), Category("TSkin"), DefaultValue(false)]
        public bool IsFocus
        {
            get => _IsFocus;
            set
            {
                if (_IsFocus != value)
                {
                    _IsFocus = value;
                    this.Invalidate();
                }
            }
        }

        #region 移动

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!IsHove) IsHove = true;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (IsHove) IsHove = false;
            base.OnMouseLeave(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            if (IsHove) IsHove = false;
            base.OnLeave(e);
        }

        #endregion

        #region 焦点

        protected override void OnGotFocus(EventArgs e)
        {
            IsFocus = true;
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            IsFocus = false;
            base.OnLostFocus(e);
        }

        #endregion

        bool _Visible = true;
        [Description("是否显示控件及其所有子控件"), Category("行为"), DefaultValue(true)]
        public new bool Visible
        {
            get => _Visible;
            set
            {
                base.Visible = _Visible = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            if (rect.Width == 0 || rect.Height == 0) { return; }
            Graphics gMain = e.Graphics;
            gMain.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    using (GraphicsPath path = rectmF.CreateRoundedRectanglePath(_radius))
                    {
                        //if (_IsShadow)
                        //{
                        //    g.FillPath(shadowbrush, path);
                        //    bmp.StackBlur(_ShadowWidth / 2);
                        //}

                        g.FillPath(backbrush, path);
                        if (IsActive)
                        {
                            g.FillPath(backselbrush, path);
                        }
                        else if (IsHove)
                        {
                            g.FillPath(backhovebrush, path);
                        }
                        if (_BorderWidth > 0)
                        {
                            if (IsActive)
                            {
                                g.DrawPath(_Activeborder, path);
                            }
                            else
                            {
                                g.DrawPath(_border, path);
                            }
                        }

                        if (_HasFocus && IsFocus)
                        {
                            if (Radius > 0)
                            {
                                using (GraphicsPath path2 = new RectangleF(0.5F, 0.5F, rect.Width - 1, rect.Height - 1).CreateRoundedRectanglePath(Radius))
                                {
                                    g.DrawPath(_borderFocus, path2);
                                }
                            }
                            else
                            {
                                g.DrawPath(_borderFocus, path);
                            }
                        }
                    }

                    if (badge)
                    {
                        g.FillEllipse(badgeColor, new RectangleF((rectByFont.X + rectByFont.Width) - badgeWidth, rectByFont.Y, badgeWidth, badgeWidth));
                    }
                }

                if (base.Enabled || _enabledOpacity >= 1)
                {
                    gMain.DrawImage(bmp, rect);
                }
                else
                {
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        ColorMatrix matrix = new ColorMatrix();
                        matrix.Matrix33 = _enabledOpacity;
                        attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        gMain.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attributes);
                    }
                }
                if (!string.IsNullOrEmpty(Text))
                {
                    gMain.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    int c2 = (int)(255 * ((base.Enabled || _enabledOpacity >= 1) ? 1 : _enabledOpacity));
                    if (IsActive && foreActivebrush != Color.Transparent)
                    {
                        gMain.DrawString(Text, this.Font, new SolidBrush(Color.FromArgb(c2, foreActivebrush)), rectByFont, stringFormat);
                    }
                    else
                    {
                        gMain.DrawString(Text, this.Font, new SolidBrush(Color.FromArgb(c2, ForeColor)), rectByFont, stringFormat);
                    }
                }
            }
            base.OnPaint(e);
        }

    }
}

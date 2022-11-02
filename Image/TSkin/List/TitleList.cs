using System.Drawing.Drawing2D;
using TSkin;

namespace TSkinList
{
    public partial class TitleList : Control
    {
        public TitleList()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            chatVScroll = new TSkin.YScroll(this);
        }

        #region 属性

        public List<TitleItem> Items = new List<TitleItem>();
        public YScroll chatVScroll;    //滚动条

        #endregion

        #region 位置坐标

        int ReadHeight = 0;
        public void InPaint()
        {
            chatVScroll.VirtualWidth = Width;
            if (Items.Count > 0)
            {
                ReadHeight = 0;
                int hehe = 30, heheT = 72;
                for (int i = 0; i < Items.Count; i++)
                {
                    TitleItem it = Items[i];
                    it.Index = i;
                    if (it.Visible)
                    {
                        it.Bounds = new Rectangle(0, ReadHeight, Width, heheT);
                        it.Bounds2 = new Rectangle(1, ReadHeight, Width - 2, heheT);
                        it.TxtBound = new Rectangle(10, ReadHeight, Width - 10, hehe);
                        it.DescBound = new Rectangle(14, ReadHeight + hehe, Width - 28, 18);
                        it.ProgBound = new Rectangle(14, ReadHeight + hehe + 22, Width - 28, 12);
                        ReadHeight += heheT + 4;
                    }
                }
                chatVScroll.VirtualHeight = ReadHeight;
            }
            else
            {
                ReadHeight = 0;
                chatVScroll.VirtualHeight = ReadHeight;
            }
        }

        public int SelectIndex = -1;
        protected override void OnSizeChanged(EventArgs e)
        {
            InPaint();
            Invalidate();
            base.OnSizeChanged(e);
        }
        Color getColor(float val)
        {
            if (val > 0.9)
            {
                return Color.Red;
            }
            else if (val > 0.8)
            {
                return Color.FromArgb(255, 87, 34);
            }
            else if (val > 0.6)
            {
                return Color.FromArgb(252, 212, 55);
            }
            else
            {
                return Color.FromArgb(38, 202, 119);
            }
        }

        #endregion

        SolidBrush fontColor = new SolidBrush(Color.Black);
        SolidBrush solidBrush = new SolidBrush(Color.White);
        SolidBrush solidBrushH = new SolidBrush(Color.FromArgb(10, 0, 0, 0));

        StringFormat _StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
        Pen pen = new Pen(Color.FromArgb(80, 80, 80), 2);
        Font on = new Font("Microsoft YaHei UI", 8);
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(0, -chatVScroll.Value);//根据滚动条的值设置坐标偏移
            g.SmoothingMode = SmoothingMode.AntiAlias;//使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            int toms = chatVScroll.Bounds.Y + chatVScroll.Value;
            int tom = toms - 100;

            List<TitleItem> _Items = Items.FindAll(db => db.Visible && (db.Bounds.Y > tom && db.Bounds.Y - toms < Height));
            try
            {
                foreach (TitleItem it in _Items)
                {
                    g.FillRectangle(solidBrush, it.Bounds);
                    if (it.MouseHover)
                    {
                        g.FillRectangle(solidBrushH, it.Bounds);
                    }
                    if (it.Prog >= 1)
                    {
                        using (var brush = new SolidBrush(getColor(1)))
                        {
                            g.FillRectangle(brush, it.ProgBound);
                        }
                    }
                    else
                    {
                        g.FillRectangle(solidBrushH, it.ProgBound);
                        if (it.Prog > 0)
                        {
                            using (var brush = new SolidBrush(getColor(it.Prog)))
                            {
                                g.FillRectangle(brush, new RectangleF(it.ProgBound.X, it.ProgBound.Y, (it.ProgBound.Width * 1.0F) * it.Prog, it.ProgBound.Height));
                            }
                        }
                    }
                    g.DrawString(it.Name, Font, fontColor, it.TxtBound, _StringFormat);
                    g.DrawString(it.Desc, on, fontColor, it.DescBound, _StringFormat);
                    if (it.Select)
                    {
                        g.DrawRectangle(pen, it.Bounds2);
                    }
                }
                g.ResetTransform();
                if (chatVScroll.IsDraw)
                    chatVScroll.DrawScroll(g);
            }
            catch { }

            base.OnPaint(e);
        }

        public event DownEventHandler DownClick;
        public delegate void DownEventHandler(TitleItem Item);

        Point old;
        public void refresh(TitleItem it)
        {
            int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
            this.Invalidate(new Rectangle(it.Bounds.X - 10, (it.Bounds.Y - 10) - tom, it.Bounds.Width + 20, it.Bounds.Height + 20));
        }

        #region 键盘操作

        public bool Kdown(Keys KeyCode)
        {
            if (KeyCode == Keys.Down)
            {
                int seb = SelectIndex;
                doha(seb);
                return true;
            }
            else if (KeyCode == Keys.Up)
            {
                int seb = SelectIndex;
                upha(seb);
                return true;
            }
            else if (KeyCode == Keys.Enter && SelectIndex > -1)
            {
                TitleItem TitleItem = Items[SelectIndex];
                if (TitleItem != null)
                {
                    SelectItem = TitleItem;
                    if (TitleItem.Visible)
                    {
                        if (DownClick != null)
                        {
                            DownClick(TitleItem);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        void upha(int seb)
        {
            int bbqq = seb -= 1; try
            {
                TitleItem TitleItem = Items[bbqq];
                while (TitleItem != null)
                {
                    if (TitleItem.Visible)
                    {
                        seb = bbqq;
                        SelectIndex = TitleItem.Index;
                        SetSv(TitleItem);


                        foreach (TitleItem TitleItems in Items)
                        {
                            if (TitleItems.Select)
                            {
                                TitleItems.Select = false;
                            }
                        }
                        if (!TitleItem.Select)
                        {
                            TitleItem.Select = true;
                        }
                        break;
                    }
                    bbqq--;
                    TitleItem = Items[bbqq];
                }
            }
            catch { }
        }
        void doha(int seb)
        {
            int bbqq = seb += 1;
            try
            {
                TitleItem TitleItem = Items[bbqq];
                while (TitleItem != null)
                {
                    if (TitleItem.Visible)
                    {
                        seb = bbqq;
                        SelectIndex = TitleItem.Index;

                        SetSv(TitleItem);
                        foreach (TitleItem TitleItems in Items)
                        {
                            TitleItems.Select = false;
                        }
                        TitleItem.Select = true;
                        break;
                    }
                    bbqq++;
                    TitleItem = Items[bbqq];
                }
            }
            catch { }
        }
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            Kdown(e.KeyCode);
            //base.OnPreviewKeyDown(e);return;
            base.OnPreviewKeyDown(e);
        }
        public void SetSv(TitleItem TitleItem)
        {
            if (chatVScroll.IsDraw)
            {
                if (chatVScroll.Value + Height - 50 < TitleItem.Bounds.Y)
                {
                    chatVScroll.Value = TitleItem.Bounds.Y;
                }
                else if (chatVScroll.Value > TitleItem.Bounds.Y)
                {
                    chatVScroll.Value = TitleItem.Bounds.Y - 150;
                }
            }
        }
        public void SetSvs(TitleItem TitleItem)
        {
            if (chatVScroll.IsDraw)
            {
                if (chatVScroll.Value + Height < TitleItem.Bounds.Y + 100)
                {
                    chatVScroll.Value += 5;
                }
                else if (chatVScroll.Value > TitleItem.Bounds.Y - 100)
                {
                    chatVScroll.Value -= 5;
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.Focus();
            if (e.Delta > 0) chatVScroll.Value -= 50;
            if (e.Delta < 0) chatVScroll.Value += 50;
            base.OnMouseWheel(e);
        }

        #endregion

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            Point m_ptMousePos = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                old = m_ptMousePos;

                if (chatVScroll.Bounds.Contains(m_ptMousePos))
                {
                    if (chatVScroll.SliderBounds.Contains(m_ptMousePos))
                    {
                        chatVScroll.IsMouseDown = true;
                        chatVScroll.MouseDownY = e.Y;
                    }
                }
                else
                {
                    if (Items.Count != 0)
                    {
                        SelectItem = null;

                        for (int i = 0; i < Items.Count; i++)
                        {
                            TitleItem it = Items[i];
                            if (it.Visible)
                            {
                                if (it.Bound(chatVScroll).Contains(m_ptMousePos))
                                {
                                    SelectIndex = i;
                                    SelectItem = it;
                                    it.Select = true;
                                    if (DownClick != null)
                                    { DownClick(it); }
                                    //break;
                                }
                                else
                                {
                                    it.Select = false;
                                }
                            }
                        }
                        if (SelectItem == null)
                        {
                            base.OnMouseDown(e);
                        }
                    }
                    else
                    {
                        base.OnMouseDown(e);
                    }
                }
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                chatVScroll.Value += 50;
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                chatVScroll.Value -= 50;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Point m_ptMousePos = e.Location;
            //if (e.Button == MouseButtons.Left)
            //    chatVScroll.IsMouseDown = false;
            //base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                chatVScroll.IsMouseDown = false;
                if (m_ptMousePos == old)
                {
                    if (chatVScroll.IsDraw)
                    {         //如果有滚动条 判断是否在滚动条类点击
                        if (chatVScroll.Bounds.Contains(m_ptMousePos))
                        {        //判断在滚动条那个位置点击
                            if (!chatVScroll.SliderBounds.Contains(m_ptMousePos))
                                chatVScroll.MoveSliderToLocation(m_ptMousePos.Y);
                            return;
                        }
                    }


                    base.OnMouseUp(e); //this.Focus();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point m_ptMousePos = e.Location;
            if (e.Button == MouseButtons.XButton1)
            {
                chatVScroll.Value += 50;
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                chatVScroll.Value -= 50;
            }

            //bool ij = chatVScroll.Bounds.Contains(m_ptMousePos);
            if (chatVScroll.IsMouseDown)
            {
                //如果滚动条的滑块处于被点击 那么移动
                chatVScroll.MoveSliderFromLocation(e.Y);
                return;
            }
            else
            {
                try
                {
                    foreach (TitleItem its in Items)
                    {
                        if (its.Visible)
                        {
                            Rectangle rect = its.Bound(chatVScroll);
                            if (rect.Contains(m_ptMousePos))
                            {
                                its.MouseHover = true;
                            }
                            else
                            {
                                its.MouseHover = false;
                            }
                        }
                    }
                }
                catch { }
            }
            if (chatVScroll.IsDraw)
            {
                if (chatVScroll.Bounds.Contains(m_ptMousePos))
                {
                    //ClearItemMouseOn();
                    if (chatVScroll.SliderBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnSlider = true;
                    else
                        chatVScroll.IsMouseOnSlider = false;
                    return;
                }
                else
                    chatVScroll.ClearAllMouseOn();
            }
            //m_ptMousePos.Y += chatVScroll.Value;//如果不在滚动条范围类 那么根据滚动条当前值计算虚拟的一个坐标
            //ClearItemMouseOn();
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            foreach (TitleItem it in Items)
            {
                if (it.Visible && it.MouseHover)
                {
                    it.MouseHover = false;
                }
            }
            chatVScroll.ClearAllMouseOn();
            base.OnMouseLeave(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            foreach (TitleItem it in Items)
            {
                if (it.Visible && it.MouseHover)
                {
                    it.MouseHover = false;
                }
            }
            chatVScroll.ClearAllMouseOn();
            base.OnLeave(e);
        }

        public TitleItem SelectItem { get; set; }
    }

    public class TitleItem
    {
        public TitleList control;
        public TitleItem(TitleList control)
        {
            this.control = control;
        }
        public TitleItem(TitleList control, string Name) : this(control)
        {
            this.Name = Name;
        }
        public Rectangle TxtBound { set; get; }
        public Rectangle ProgBound { set; get; }
        public float Prog { get; set; }
        public Rectangle DescBound { set; get; }
        public Rectangle Bounds { set; get; }
        public Rectangle Bounds2 { set; get; }

        public Rectangle Bound(int x, int y)
        {
            return new Rectangle(Bounds.X - x, Bounds.Y - y, Bounds.Width, Bounds.Height);
        }
        public Rectangle Bound(YScroll VScroll)
        {
            return new Rectangle(Bounds.X, Bounds.Y - (VScroll.Bounds.Y + VScroll.Value), Bounds.Width, Bounds.Height);
        }

        public string Name { get; set; }
        public string Desc { get; set; }

        bool _MouseHover = false;
        public bool MouseHover
        {
            get { return _MouseHover; }
            set
            {
                if (_MouseHover != value)
                {
                    _MouseHover = value;
                    control.refresh(this);
                }
            }
        }

        public bool Visible { get; set; }

        public bool _Select = false;
        public bool Select
        {
            get { return _Select; }
            set
            {
                if (_Select != value)
                {
                    _Select = value;
                    if (value)
                    {
                        foreach (var item in control.Items)
                        {
                            if (item != this)
                            {
                                item.Select = false;
                            }
                        }
                    }
                    control.refresh(this);
                }
            }
        }

        public object Tag { set; get; }
        public int Index { set; get; }
    }
}

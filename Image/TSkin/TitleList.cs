using System.ComponentModel;

namespace TSkin
{
    public partial class TitleList : AntdUI.IControl
    {
        public TitleList()
        {
            chatVScroll = new YScroll(this);
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
                int g4 = (int)(4 * AntdUI.Config.Dpi), g10 = (int)(10 * AntdUI.Config.Dpi), g12 = (int)(12 * AntdUI.Config.Dpi), g14 = (int)(14 * AntdUI.Config.Dpi), g18 = (int)(18 * AntdUI.Config.Dpi), g22 = (int)(22 * AntdUI.Config.Dpi), g28 = (int)(28 * AntdUI.Config.Dpi),
                    hehe = (int)(30 * AntdUI.Config.Dpi), heheT = (int)(72 * AntdUI.Config.Dpi);
                for (int i = 0; i < Items.Count; i++)
                {
                    TitleItem it = Items[i];
                    it.Index = i;
                    if (it.Visible)
                    {
                        it.Bounds = new Rectangle(0, ReadHeight, Width, heheT);
                        it.TxtBound = new Rectangle(g10, ReadHeight, Width - g10, hehe);
                        it.DescBound = new Rectangle(g14, ReadHeight + hehe, Width - g28, g18);
                        it.ProgBound = new Rectangle(g14, ReadHeight + hehe + g22, Width - g28, g12);
                        ReadHeight += heheT + g4;
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
            if (val > 0.9) return Color.Red;
            else if (val > 0.8) return Color.FromArgb(255, 87, 34);
            else if (val > 0.6) return Color.FromArgb(252, 212, 55);
            else return Color.FromArgb(38, 202, 119);
        }

        #endregion

        StringFormat _StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = AntdUI.Helper.High(e.Graphics);
            g.TranslateTransform(0, -chatVScroll.Value);//根据滚动条的值设置坐标偏移
            using (var on = new Font("Microsoft YaHei UI", 8))
            using (var brush = new SolidBrush(AntdUI.Style.Db.BgContainer))
            using (var brushHover = new SolidBrush(Color.FromArgb(10, 0, 0, 0)))
            {
                foreach (TitleItem it in Items)
                {
                    if (it.Visible)
                    {
                        g.Fill(brush, it.Bounds);
                        if (it.MouseHover) g.Fill(brushHover, it.Bounds);
                        if (it.Prog >= 1) g.Fill(getColor(1), it.ProgBound);
                        else
                        {
                            g.Fill(brushHover, it.ProgBound);
                            if (it.Prog > 0) g.Fill(getColor(it.Prog), new RectangleF(it.ProgBound.X, it.ProgBound.Y, (it.ProgBound.Width * 1.0F) * it.Prog, it.ProgBound.Height));
                        }
                        g.String(it.Name, Font, Brushes.Black, it.TxtBound, _StringFormat);
                        g.String(it.Desc, on, Brushes.Black, it.DescBound, _StringFormat);
                        if (it.Select)
                        {
                            using (var pen = new Pen(Color.FromArgb(80, 80, 80), 2 * AntdUI.Config.Dpi))
                            {
                                g.SetClip(it.Bounds);
                                g.Draw(pen, it.Bounds);
                                g.ResetClip();
                            }
                        }
                    }
                }
            }
            g.ResetTransform();
            if (chatVScroll.IsDraw) chatVScroll.DrawScroll(g);
            base.OnPaint(e);
        }

        Point old;
        public void refresh(TitleItem it)
        {
            int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
            Invalidate(new Rectangle(it.Bounds.X - 10, (it.Bounds.Y - 10) - tom, it.Bounds.Width + 20, it.Bounds.Height + 20));
        }

        #region 键盘操作

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Focus();
            if (e.Delta > 0) chatVScroll.Value -= 50;
            if (e.Delta < 0) chatVScroll.Value += 50;
            base.OnMouseWheel(e);
        }

        #endregion

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            var m_ptMousePos = e.Location;
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


                    base.OnMouseUp(e); //Focus();
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TitleItem? SelectItem { get; private set; }
    }

    public class TitleItem
    {
        public TitleList control;
        public TitleItem(TitleList _control)
        {
            control = _control;
        }
        public TitleItem(TitleList control, string Name) : this(control)
        {
            Name = Name;
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

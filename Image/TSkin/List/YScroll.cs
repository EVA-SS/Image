namespace TSkin
{
    public class YScroll
    {
        #region 区域

        Rectangle bounds;
        /// <summary>
        /// 滚动条自身的区域
        /// </summary>
        public Rectangle Bounds
        {
            get => bounds;
        }

        Rectangle sliderBounds;
        /// <summary>
        /// 滑块区域
        /// </summary>
        public Rectangle SliderBounds
        {
            get => sliderBounds;
        }

        #endregion

        int virtualWidth = 0;
        /// <summary>
        /// 虚拟宽度
        /// </summary>
        public int VirtualWidth
        {
            get => virtualWidth;
            set
            {
                if (virtualWidth != value)
                {
                    virtualWidth = value;
                    bounds.X = value - 8;
                    sliderBounds.Height = size;
                    sliderBounds.Y = value - size;
                }
            }
        }

        /// <summary>
        /// 虚拟的一个高度(控件中内容的高度)
        /// </summary>
        int virtualHeight = 0;
        public int VirtualHeight
        {
            get { return virtualHeight; }
            set
            {
                if (value <= ctrl.Height)
                {
                    Value = 0;
                    IsDraw = false;
                }
                else
                {
                    IsDraw = true;
                    if (value - _value < ctrl.Height)
                    {
                        _value -= ctrl.Height - value + _value;
                        ctrl.Invalidate();
                    }
                }
                virtualHeight = value;
            }
        }

        int size = 8;
        /// <summary>
        /// 滑块宽度
        /// </summary>
        public int Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    sliderBounds.Height = value;
                    sliderBounds.X = virtualWidth - value;
                    ctrl.Invalidate();
                }
            }
        }

        int _value = 0;
        /// <summary>
        /// 当前滚动条位置
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                if (value < 0)
                {
                    //小于0
                    if (_value != 0)
                    {
                        _value = 0;
                        if (IsDraw)
                            ctrl.Invalidate();
                        if (ScrollChange != null)
                        { ScrollChange(0); }
                    }
                }
                else if (virtualHeight > 0 && value > virtualHeight - ctrl.Height)
                {
                    if (IsDraw)
                    {
                        //大于最大值
                        if (_value != virtualHeight - ctrl.Height)
                        {
                            _value = virtualHeight - ctrl.Height;
                            ctrl.Invalidate();
                            if (ScrollChange != null)
                            { ScrollChange(Math.Round((_value * 1.0) / ((virtualHeight - bounds.Height) * 1.0), 2)); }
                        }
                    }
                }
                else if (_value != value)
                {
                    _value = value;
                    if (IsDraw)
                        ctrl.Invalidate();
                    if (ScrollChange != null)
                    { ScrollChange(Math.Round((_value * 1.0) / ((virtualHeight - bounds.Height) * 1.0), 2)); }
                }
            }
        }

        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        bool isMouseDown = false;
        public bool IsMouseDown
        {
            get { return isMouseDown; }
            set
            {
                if (value)
                {
                    m_nLastSliderY = sliderBounds.Y;
                }
                isMouseDown = value;
            }
        }

        bool isMouseOnSlider = false;
        /// <summary>
        /// 鼠标是否按下滑块
        /// </summary>
        public bool IsMouseOnSlider
        {
            get { return isMouseOnSlider; }
            set
            {
                if (isMouseOnSlider != value)
                {
                    isMouseOnSlider = value;
                    ctrl.Invalidate(SliderBounds);
                }
            }
        }

        /// <summary>
        /// 鼠标在滑块点下时候的y坐标
        /// </summary>
        public int MouseDownY = -1;
        //滑块移动前的 滑块的y坐标
        int m_nLastSliderY = -1;

        //绑定的控件
        Control ctrl;
        public YScroll(Control _ctrl)
        {
            ctrl = _ctrl;
            bounds = new Rectangle(0, 0, size, 0);
            sliderBounds = new Rectangle(0, 0, size, 0);
        }

        public void ClearAllMouseOn()
        {
            isMouseOnSlider = false;
            ctrl.Invalidate(this.bounds);
        }
        public void OnScrollChange(double value)
        {
            if (ScrollChange != null)
            { ScrollChange(value); }
        }

        public event ScrollEventHandler ScrollChange;
        public delegate void ScrollEventHandler(double value);

        /// <summary>
        /// 将滑块跳动至一个地方
        /// </summary>
        /// <param name="mouseY">当前鼠标坐标Y</param>
        public void MoveSliderToLocation(int mouseY)
        {
            if (mouseY - sliderBounds.Height / 2 <= 0)
                sliderBounds.Y = 0;
            else if (mouseY + sliderBounds.Height / 2 > ctrl.Height)
                sliderBounds.Y = ctrl.Height - sliderBounds.Height;
            else
                sliderBounds.Y = mouseY - sliderBounds.Height / 2;
            Value = (int)((sliderBounds.Y * 1.0) / (ctrl.Height - SliderBounds.Height) * (virtualHeight - ctrl.Height));
        }
        /// <summary>
        /// 根据鼠标位置移动滑块
        /// </summary>
        /// <param name="mouseY">当前鼠标坐标Y</param>
        public void MoveSliderFromLocation(int mouseY)
        {
            //if (!this.IsMouseDown) return;
            if (m_nLastSliderY + mouseY - MouseDownY <= 0)
            {
                sliderBounds.Y = 0;
            }
            else if (m_nLastSliderY + mouseY - MouseDownY > ctrl.Height - SliderBounds.Height)
            {
                if (sliderBounds.Y == ctrl.Height - sliderBounds.Height)
                    return;
                sliderBounds.Y = ctrl.Height - sliderBounds.Height;
            }
            else
            {
                sliderBounds.Y = m_nLastSliderY + mouseY - MouseDownY;
            }
            Value = (int)((sliderBounds.Y * 1.0) / (ctrl.Height - SliderBounds.Height) * (virtualHeight - ctrl.Height));
        }


        #region 外观

        public Color DefaultColor = Color.FromArgb(60, 0, 0, 0);
        public Color DownColor = Color.FromArgb(200, 36, 251, 243);

        /// <summary>
        /// 是否有必要在控件上绘制滚动条
        /// </summary>
        public bool IsDraw = false;

        /// <summary>
        /// 绘制滚动条
        /// </summary>
        public void DrawScroll(Graphics g)
        {
            if (!IsDraw)
                return;
            bounds.Height = ctrl.Height;
            //计算滑块位置
            sliderBounds.X = bounds.X;
            sliderBounds.Height = (int)(((double)ctrl.Height / virtualHeight) * ctrl.Height);
            if (sliderBounds.Height < 0) sliderBounds.Height = 10;
            sliderBounds.Y = _value == 0 ? 0 : (int)(((double)_value / (virtualHeight - ctrl.Height)) * (ctrl.Height - sliderBounds.Height));

            using (var sb = new SolidBrush((isMouseDown || isMouseOnSlider) ? DownColor : DefaultColor))
            {
                using (var path = sliderBounds.CreateRoundedRectanglePath(size))
                {
                    g.FillPath(sb, path);
                }
            }
        }

        #endregion
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    [DefaultEvent("ValueChanged"), DefaultProperty("Value")]
    public class YoutubeProgressBar : Control
    {

        #region  Declarations 

        private int _value;
        private int _maximum;
        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler(object sender);

        private IStyle _style;
        #endregion

        #region  Constructors 

        public YoutubeProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            _maximum = 100;
            _value = 0;
            _style = IStyle.Red;
        }

        #endregion

        #region  Properties 

        [Category("Custom"), Description("Gets or sets the current position of the progressbar.")]
        public int Value
        {
            get
            {
                if (_value < 0)
                {
                    return 0;
                }
                else
                {
                    return _value;
                }
            }
            set
            {
                if (value > Maximum)
                {
                    value = Maximum;
                }
                _value = value;
                if (ValueChanged != null) ValueChanged.Invoke(this);
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the maximum value of the progressbar.")]
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                if (value < _value)
                {
                    _value = Value;
                }
                _maximum = value;
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the style for the control.")]
        public IStyle Style
        {
            get { return _style; }
            set
            {
                _style = value;
                Invalidate();
            }
        }

        #endregion

        #region  Draw Control 
    
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsPath gp = new GraphicsPath();

            int currentValue = Convert.ToInt32((double)Value / Maximum * Width);
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            g.FillRectangle(Brushes.LightGray, rect);


            if (currentValue != 0)
            {
                g.FillRectangle(Style == IStyle.Red ? Brushes.Red : Brushes.Blue, new Rectangle(rect.X, rect.Y, currentValue, rect.Height));
            }

            gp.Dispose();
        }

        #endregion

        #region  Enumerators 

        public enum IStyle
        {
            Red,
            Blue
        }

        #endregion

    }
}
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    public class YoutubeSeperator : Control
    {

        #region  Variables 

        private Style _seperatorStyle;
        private Color _seperatorColor;

        #endregion

        #region  Enumerators 

        public enum Style
        {
            Horizental,
            Vertiacal
        }

        #endregion

        #region  Properties 

        [Category("Custom"), Description("Gets or sets the style for the control.")]
        public Style SeperatorStyle
        {
            get { return _seperatorStyle; }
            set
            {
                _seperatorStyle = value;
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the color for the control.")]
        public Color SeperatorColor
        {
            get { return _seperatorColor; }
            set
            {
                _seperatorColor = value;
                Invalidate();
            }
        }

        #endregion

        #region  Constructors 

        public YoutubeSeperator()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            _seperatorStyle = Style.Horizental;
            _seperatorColor = Colors.Silver;
        }

        #endregion

        #region  Draw Control 

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Pen p = new Pen(SeperatorColor))
            {
                switch (SeperatorStyle)
                {
                    case Style.Horizental:
                        g.DrawLine(p, 0, 1, Width, 1);
                        break;
                    case Style.Vertiacal:
                        g.DrawLine(p, 1, 0, 1, Height);
                        break;
                }
            }

        }

        #endregion

        #region  Events 

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (SeperatorStyle == Style.Horizental)
            {
                Height = 4;
            }
            else
            {
                Width = 4;
            }
        }

        #endregion

    }
}
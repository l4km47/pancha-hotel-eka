using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    public class YoutubeButton : Control
    {

        #region  Declarations 

        private static readonly HelperMethods H = new HelperMethods();
        private HelperMethods.MouseMode _state;
        private IStyle _style;
        private int _borderRadius;

        #endregion

        #region  Constructors 

        public YoutubeButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 10);
            _borderRadius = 0;
            _style = IStyle.Light;
            _state = HelperMethods.MouseMode.Normal;
        }

        #endregion

        #region  Events 

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _state = HelperMethods.MouseMode.Pushed;
            Cursor = Cursors.Hand;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            _state = HelperMethods.MouseMode.Hovered;
            Cursor = Cursors.Hand;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _state = HelperMethods.MouseMode.Normal;
            Cursor = Cursors.Default;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _state = HelperMethods.MouseMode.Hovered;
            Cursor = Cursors.Hand;
            Invalidate();
        }


        #endregion

        #region  Properties 

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

        [Category("Custom"), Description("Gets or sets the rounded corner degree of the control.")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                Invalidate();
            }
        }

        #endregion

        #region  Draw Control 

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            GraphicsPath gp = new GraphicsPath();

            if (BorderRadius > 1)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                gp = H.RoundRec(rect, BorderRadius);
            }
            else
            {
                gp.AddRectangle(rect);
            }

            switch (_state)
            {
                case HelperMethods.MouseMode.Normal:
                    g.FillPath(Style == IStyle.Light ? Brushes.LightSilver : Brushes.Blue, gp);
                    g.DrawPath(Style == IStyle.Light ? Pens.Silver : Pens.LighterBlue, gp);
                    g.DrawString(Text, Font, Style == IStyle.Light ? Brushes.Gray : Brushes.White, rect, H.SetPosition());
                    break;
                case HelperMethods.MouseMode.Hovered:
                    g.FillPath(Style == IStyle.Light ? Brushes.Silver : Brushes.LighterBlue, gp);
                    g.DrawPath(Style == IStyle.Light ? Pens.Silver : Pens.Blue, gp);
                    g.DrawString(Text, Font, Style == IStyle.Light ? Brushes.Gray : Brushes.White, rect, H.SetPosition());
                    break;
                case HelperMethods.MouseMode.Pushed:
                    g.FillPath(Style == IStyle.Light ? Brushes.LightGray : Brushes.DarkBlue, gp);
                    g.DrawPath(Style == IStyle.Light ? Pens.Silver : Pens.DarkBlue, gp);
                    g.DrawString(Text, Font, Style == IStyle.Light ? Brushes.Gray : Brushes.White, rect, H.SetPosition());
                    break;
            }

        }

        #endregion

        #region  Enumerators 

        public enum IStyle
        {
            Light,
            Blue
        }

        #endregion

    }
}
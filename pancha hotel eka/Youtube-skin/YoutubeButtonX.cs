using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    public class YoutubeButtonX : Control
    {

        #region  Declarations 

        private static readonly HelperMethods H = new HelperMethods();
        private string _leftText;
        private string _rightText;
        private Rectangle _redPart;
        private Rectangle _lightPart;
        private HelperMethods.MouseMode _state;
        private IStyle _style;

        #endregion

        #region  Constructors 

        public YoutubeButtonX()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 10);
            _style = IStyle.Red;
            _state = HelperMethods.MouseMode.Normal;
            _leftText = "12,961,386";
            _rightText = "Subscribe";
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

        [Category("Custom"), Description("Gets or sets the text of the left side of the control.")]
        public string LeftText
        {
            get { return _leftText; }
            set
            {
                _leftText = value;
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the text of the right side of the control.")]
        public string RightText
        {
            get { return _rightText; }
            set
            {
                _rightText = value;
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
            g.SmoothingMode = SmoothingMode.AntiAlias;
            _redPart = new Rectangle(0, 0, Width / 2, Height);
            _lightPart = new Rectangle(Width / 2, 0, Width - Width / 2, Height);

            switch (_state)        {

                case HelperMethods.MouseMode.Normal:
                    g.FillPath(Style == IStyle.Red ? Brushes.Red : Brushes.Blue, H.RoundRec(_redPart, 6, true, false, true, false));
                    g.FillRectangle(Brushes.LightSilver, _lightPart);
                    g.DrawLine(Pens.Silver, Convert.ToInt32(Width / 2), 0, Convert.ToInt32(Width - 1), 0);
                    g.DrawLine(Pens.Silver, Convert.ToInt32(Width / 2), Height - 1, Width, Height - 1);
                    g.DrawLine(Pens.Silver, Width - 1, 0, Width - 1, Height);
                    g.DrawString(RightText, Font, Brushes.White, _redPart, H.SetPosition());
                    g.DrawString(LeftText, Font, Brushes.Gray, _lightPart, H.SetPosition());
                    break;
                case HelperMethods.MouseMode.Hovered:
                    g.FillPath(Style == IStyle.Red ? Brushes.LightRed : Brushes.LighterBlue, H.RoundRec(_redPart, 6, true, false, true, false));
                    g.FillRectangle(Brushes.LightSilver, _lightPart);
                    g.DrawLine(Pens.Silver, Convert.ToInt32(Width / 2), 0, Convert.ToInt32(Width - 1), 0);
                    g.DrawLine(Pens.Silver, Convert.ToInt32(Width / 2), Height - 1, Width, Height - 1);
                    g.DrawLine(Pens.Silver, Width - 1, 0, Width - 1, Height);
                    g.DrawString(RightText, Font, Brushes.White, _redPart, H.SetPosition());
                    g.DrawString(LeftText, Font, Brushes.Gray, _lightPart, H.SetPosition());
                    break;
                case HelperMethods.MouseMode.Pushed:
                    g.FillPath(Style == IStyle.Red ? Brushes.DarkRed : Brushes.DarkBlue, H.RoundRec(_redPart, 6, true, false, true, false));
                    g.FillRectangle(Brushes.LightSilver, _lightPart);
                    g.DrawLine(Pens.Silver, Convert.ToInt32(Width / 2), 0, Convert.ToInt32(Width - 1), 0);
                    g.DrawLine(Pens.Silver, Convert.ToInt32(Width / 2), Height - 1, Width, Height - 1);
                    g.DrawLine(Pens.Silver, Width - 1, 0, Width - 1, Height);
                    g.DrawString(RightText, Font, Brushes.White, _redPart, H.SetPosition());
                    g.DrawString(LeftText, Font, Brushes.Gray, _lightPart, H.SetPosition());
                    break;
            }

        }

        #endregion

        #region  Enumerators 

        public enum IStyle
        {
            Blue,
            Red
        }

        #endregion

    }
}
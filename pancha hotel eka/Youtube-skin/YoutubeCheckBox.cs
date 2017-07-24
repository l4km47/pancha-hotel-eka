using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    [DefaultEvent("CheckedChanged"), DefaultProperty("Checked")]
    public class YoutubeCheckBox : Control
    {

        #region  Declarations 

        private static readonly HelperMethods H = new HelperMethods();
        private bool _checked;
        protected HelperMethods.MouseMode State = HelperMethods.MouseMode.Normal;

        #endregion

        #region  Properties 

        [Category("Custom"), Description("Gets or set a value indicating whether the control is in the checked state.")]
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                if (CheckedChanged != null) CheckedChanged.Invoke(this);
                Invalidate();
            }
        }

        #endregion

        #region  Constructors 

        public YoutubeCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(121, 121, 121);
            Font = new Font("Segoe UI", 9);
            UpdateStyles();
        }

        #endregion

        #region  Draw Control 

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            Rectangle rect = new Rectangle(0, 0, 18, 17);
            using (Font f = new Font("Marlett", 14))
            {
                g.FillRectangle(Brushes.LightSilver, rect);
                if (Checked)
                { g.DrawString("b", f, Brushes.Gray, new Rectangle(-2, 0, Width, Height));}
                g.DrawRectangle(Pens.Silver, new Rectangle(0, 0, 17, 16));
                g.DrawString(Text, Font, Brushes.Gray, new Rectangle(18, 1, Width, Height - 4), H.SetPosition(StringAlignment.Near));
            }

        }

        #endregion

        #region  Events 

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnClick(EventArgs e)
        {
            _checked = !Checked;
            if (CheckedChanged != null) CheckedChanged.Invoke(this);
            base.OnClick(e);
            Invalidate();
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Height = 17;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            State = HelperMethods.MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = HelperMethods.MouseMode.Normal;
            Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

        #endregion

    }
}
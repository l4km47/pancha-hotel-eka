using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    [DefaultEvent("CheckedChanged"), DefaultProperty("Checked")]
    public class YoutubeRadioButton : Control
    {

        #region  Declarations 

        private static readonly HelperMethods H = new HelperMethods();
        private bool _checked;
        protected int _Group;

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

        [Category("Custom")]
        public int Group
        {
            get { return _Group; }
            set
            {
                _Group = value;
                Invalidate();
            }
        }

        #endregion

        #region  Constructors 

        public YoutubeRadioButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            UpdateStyles();
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(121, 121, 121);
            Font = new Font("Segoe UI", 9);
            Group = 1;
        }

        #endregion

        #region  Draw Control 

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (Font f = new Font("Marlett", 14))
            {
                g.FillEllipse(Brushes.LightSilver, new Rectangle(0, 0, 21, 21));
                if (Checked)
                { g.FillEllipse(Brushes.Gray, new Rectangle(5, 5, 10, 10)); }
                g.DrawString(Text, Font, Brushes.Gray, new Rectangle(21, 1, Width, Height - 2), H.SetPosition(StringAlignment.Near));
                g.DrawEllipse(Pens.Silver, new Rectangle(0, 0, 20, 20));
            }

        }

        #endregion

        #region  Events 

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        private void UpdateState()
        {
            if (!IsHandleCreated || !Checked)
                return;
            foreach (Control c in Parent.Controls)
            {
                if (!ReferenceEquals(c, this) && c is YoutubeRadioButton && ((YoutubeRadioButton)c).Group == _Group)
                {
                    ((YoutubeRadioButton)c).Checked = false;
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            _checked = !Checked;
            UpdateState();
            base.OnClick(e);
            Invalidate();
        }

        protected override void OnCreateControl()
        {
            UpdateState();
            base.OnCreateControl();
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Height = 22;
            Invalidate();
        }

        #endregion

    }
}
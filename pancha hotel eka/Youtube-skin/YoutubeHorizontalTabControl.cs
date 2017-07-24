using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    public class YoutubeHorizontalTabControl : TabControl
    {

        #region  Declarations 

        private static readonly HelperMethods H = new HelperMethods();
        private Point _locatedPostion;
        private Rectangle _r;

        #endregion

        #region  Constructors 

        public YoutubeHorizontalTabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            SizeMode = TabSizeMode.Fixed;
            Dock = DockStyle.None;
            ItemSize = new Size(90, 48);
            Alignment = TabAlignment.Top;
            Font = new Font("Myriad Pro", 9);
            _locatedPostion = new Point(-1, -1);
        }

        #endregion

        #region  Events 

        protected override void OnCreateControl()
        {
            foreach (TabPage tab in TabPages)
            {
                tab.BackColor = Colors.White;
                Invalidate();
            }
            base.OnCreateControl();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (DesignMode)
                return;
            _locatedPostion = e.Location;
            Cursor = Cursors.Hand;
            Invalidate();
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _locatedPostion = new Point(-1, -1);
            Cursor = Cursors.Default;
            Invalidate();
            base.OnMouseLeave(e);
        }

        #endregion

        #region  Properties 

        #endregion

        #region  Draw Control 

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.CompositingQuality = CompositingQuality.HighQuality;

            g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, ItemSize.Height));
            g.FillRectangle(Brushes.White, new Rectangle(0, ItemSize.Height + 3, Width, Height));
            using (StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center })
            {

                for (int i = 0; i <= TabCount - 1; i++)
                {
                    _r = GetTabRect(i);

                    if (i == SelectedIndex)
                    {
                        g.FillRectangle(Brushes.Red, new Rectangle(_r.X - 2, _r.Height - 4, _r.Width - 1, 4));

                        g.DrawString(TabPages[i].Text, Font, Brushes.DarkGray, _r, H.SetPosition());

                    }
                    else
                    {

                        if (_r.Contains(_locatedPostion))
                        {
                            g.DrawString(TabPages[i].Text, Font, Brushes.DarkGray, _r, H.SetPosition());
                        }
                        else
                        {
                            g.DrawString(TabPages[i].Text, Font, Brushes.Gray, _r, H.SetPosition());
                        }

                    }
                }

            }

        }

        #endregion

    }
}
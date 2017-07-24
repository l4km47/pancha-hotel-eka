using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    public class YoutubeTabControl : TabControl
    {

        #region  Declarations 

        private static readonly HelperMethods H = new HelperMethods();
        private Rectangle _r;
        private Point _locatedPostion;
        private Point _imageLocation;
        private Point _textLocation;
        private Point _headerTextLocation;

        #endregion

        #region  Constructors 

        public YoutubeTabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            SizeMode = TabSizeMode.Fixed;
            Dock = DockStyle.None;
            ItemSize = new Size(38, 180);
            Alignment = TabAlignment.Left;
            Font = new Font("Myriad Pro", 9);
            _locatedPostion = new Point(-1, -1);
            _imageLocation = new Point(30, 13);
            _textLocation = new Point(50, 2);
            _headerTextLocation = new Point(16, 5);
        }

        #endregion

        #region  Events 

        protected override void OnCreateControl()
        {
            foreach (TabPage tab in TabPages)
            {
                tab.BackColor = Colors.LightSilver;
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

        [Category("Custom"), Description("Gets or sets the tab pages image location.")]
        public Point ImageLocation
        {
            get { return _imageLocation; }
            set
            {
                _imageLocation = value;
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the tab pages text location.")]
        public Point TextLocation
        {
            get { return _textLocation; }
            set
            {
                _textLocation = value;
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the tab pages header text location.")]
        public Point HeaderTextLocation
        {
            get { return _headerTextLocation; }
            set
            {
                _headerTextLocation = value;
                Invalidate();
            }
        }

        #endregion

        #region  Draw Control 

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.FillRectangle(Brushes.White, new Rectangle(0, 0, ItemSize.Height, Height));
            g.FillRectangle(Brushes.LightSilver, new Rectangle(ItemSize.Height, 0, Width, Height));
            using (StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center })
            {
                for (int i = 0; i <= TabCount - 1; i++)
                {
                    _r = GetTabRect(i);

                    if (TabPages[i].Tag != null)
                    {
                        using (Font f = new Font(Font.Name, 9, FontStyle.Bold))
                        {

                            g.DrawString(TabPages[i].Text.ToUpper(), f, System.Drawing.Brushes.Red, new Rectangle(_r.X + HeaderTextLocation.X, _r.Y + HeaderTextLocation.Y, _r.Width - 2, _r.Height), sf);

                        }

                    }
                    else if (i == SelectedIndex)
                    {
                        g.FillRectangle(System.Drawing.Brushes.Red, new Rectangle(_r.X - 2, _r.Y + 2, _r.Width - 1, _r.Height - 2));

                        g.DrawString(TabPages[i].Text, Font, System.Drawing.Brushes.White, new Rectangle(_r.X + TextLocation.X, _r.Y + TextLocation.Y, _r.Width - 2, _r.Height), sf);                    
                    }
                    else
                    {

                        if (_r.Contains(_locatedPostion))
                        {
                            g.FillRectangle(System.Drawing.Brushes.Gray, new Rectangle(_r.X - 2, _r.Y + 2, _r.Width - 1, _r.Height - 2));

                            g.DrawString(TabPages[i].Text, Font, System.Drawing.Brushes.White, new Rectangle(_r.X + TextLocation.X, _r.Y + TextLocation.Y, _r.Width - 2, _r.Height), sf);
                        
                        }
                        else
                        {
                            g.DrawString(TabPages[i].Text, Font, System.Drawing.Brushes.Gray, new Rectangle(_r.X + TextLocation.X, _r.Y + TextLocation.Y, _r.Width - 2, _r.Height), sf);

                        }

                    }

                    if ((ImageList != null) && ImageList.Images[i] != null && TabPages[i].Tag == null)
                    {
                        H.DrawImageWithColor(g, new Rectangle(_r.X + ImageLocation.X, _r.Y + ImageLocation.Y, 13, 13), ImageList.Images[i], i == SelectedIndex || _r.Contains(_locatedPostion) ? Colors.White : Colors.Gray);
                    }

                }

            }

        }

        #endregion

    }
}
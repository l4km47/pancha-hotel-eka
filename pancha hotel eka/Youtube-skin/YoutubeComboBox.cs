using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    [DefaultEvent("SelectedIndexChanged")]
    public class YoutubeComboBox : ComboBox
    {

        #region  Declarations 

        private int _startIndex = 0;
        private static readonly HelperMethods H = new HelperMethods();
        public new event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public delegate void SelectedIndexChangedEventHandler(object sender);

        #endregion

        #region  Constructors 


        public YoutubeComboBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 11);
            DrawMode = DrawMode.OwnerDrawFixed;
            DoubleBuffered = true;
            StartIndex = 0;
            DropDownStyle = ComboBoxStyle.DropDownList;
            UpdateStyles();
        }

        #endregion

        #region  Properties 

        [Category("Custom Properties"), Description("Gets or sets the index specifying the currently selected item.")]
        private int StartIndex
        {
            get { return _startIndex; }
            set
            {
                _startIndex = value;
                try
                {
                    base.SelectedIndex = value;
                    if (SelectedIndexChanged != null) SelectedIndexChanged.Invoke(this);
                }
                catch
                {
                }
                Invalidate();
            }
        }

        #endregion

        #region  Draw Control 

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            try
            {
                using (SolidBrush bg = new SolidBrush((e.State & DrawItemState.Selected) == DrawItemState.Selected ? Colors.Gray : Colors.White))
                using (SolidBrush tc = new SolidBrush((e.State & DrawItemState.Selected) == DrawItemState.Selected ? Colors.White : Colors.Gray))
                using (Font f = new Font(Font.Name, 9))
                {
                    g.FillRectangle(bg, e.Bounds);
                    g.DrawString(GetItemText(Items[e.Index]), f, tc, e.Bounds);
                }
                
            

            }
            catch
            {
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            GraphicsPath gp = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            gp.AddRectangle(rect);
            gp.CloseFigure();
            using (PathGradientBrush b = new PathGradientBrush(gp) { CenterColor = Colors.White, SurroundColors = new Color[] { Colors.LightSilver }, FocusScales = new PointF(0.98f, 0.75f) })
            {
                g.FillPath(b, gp);
                g.DrawPath(Pens.Silver, gp);
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            H.DrawTriangle(g, Colors.Silver, 2, new Point(Width - 20, 12), new Point(Width - 16, 16), new Point(Width - 16, 16), new Point(Width - 12, 12), new Point(Width - 16, 17), new Point(Width - 16, 16));
            g.SmoothingMode = SmoothingMode.None;
            using (Font f = new Font(Font.Name, 10))
            {
                g.DrawString(Text, f, Brushes.Silver, new Rectangle(7, 0, Width - 1, Height - 1), H.SetPosition(StringAlignment.Near));
            }
            gp.Dispose();
        }

        #endregion

        #region  Events 

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        #endregion

    }
}
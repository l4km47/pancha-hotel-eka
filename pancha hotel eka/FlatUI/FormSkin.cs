using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
    public sealed class FormSkin : ContainerControl
    {
        private int _w;
        private int _h;
        private bool _cap;
        private Point _mousePoint = new Point(0, 0);
        private int _moveHeight = 30;

        [Category("Colors")]
        public Color HeaderColor
        {
            get { return _headerColor; }
            set { _headerColor = value; }
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get { return _baseColor; }
            set { _baseColor = value; }
        }

        [Category("Colors")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        [Category("Colors")]
        public Color FlatColor
        {
            // get { return Helpers.FlatColor; }
            // set { Helpers.FlatColor = value; }
            get { return _flatColor; }
            set { _flatColor = value; }
        }

        [Category("Options")]
        public bool HeaderMaximize { get; set; }

        [Category("Icon")]
        public Image Icon { get; set; }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left & new Rectangle(0, 0, Width, _moveHeight).Contains(e.Location))
            {
                _cap = true;
                _mousePoint = e.Location;
            }
        }

        private void FormSkin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HeaderMaximize)
            {
                if (e.Button == MouseButtons.Left & new Rectangle(0, 0, Width, _moveHeight).Contains(e.Location))
                {
                    var findForm = FindForm();
                    if (findForm != null && findForm.WindowState == FormWindowState.Normal)
                    {
                        findForm.WindowState = FormWindowState.Maximized;
                        findForm.Refresh();
                    }
                    else if (findForm != null && findForm.WindowState == FormWindowState.Maximized)
                    {
                       findForm.WindowState = FormWindowState.Normal;
                       findForm.Refresh();
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _cap = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_cap)
            {
                // Parent.Location = MousePosition - MousePoint;
                Parent.Location = new Point(
                    MousePosition.X - _mousePoint.X,
                    MousePosition.Y - _mousePoint.Y
                );
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (ParentForm != null)
            {
                ParentForm.FormBorderStyle = FormBorderStyle.None;
                ParentForm.AllowTransparency = false;
                ParentForm.TransparencyKey = Color.Fuchsia;
                var findForm = ParentForm.FindForm();
                if (findForm != null) findForm.StartPosition = FormStartPosition.CenterScreen;
            }
            Dock = DockStyle.Fill;
            Invalidate();
        }

        public Color _headerColor = Color.FromArgb(45, 47, 49);
        private Color _baseColor = Color.FromArgb(60, 70, 73);
        private Color _borderColor = Color.FromArgb(53, 58, 60);
        private Color _flatColor = Helpers.FlatColor;
        private readonly Color _textColor = Color.FromArgb(234, 234, 234);

        public Color TextLight = Color.FromArgb(45, 47, 49);

        public FormSkin()
        {
            MouseDoubleClick += FormSkin_MouseDoubleClick;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 8.25f);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);
            _w = Width;
            _h = Height;

            Rectangle Base = new Rectangle(0, 0, _w, _h);
            Rectangle header = new Rectangle(0, 0, _w, 30);

            var with2 = g;
            with2.SmoothingMode = SmoothingMode.HighQuality;
            with2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            with2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            with2.Clear(BackColor);

            //-- Base
            with2.FillRectangle(new SolidBrush(_baseColor), Base);

            //-- Header
            with2.FillRectangle(new SolidBrush(_headerColor), header);

            //-- Logo
            //	_with2.FillRectangle(new SolidBrush(Color.FromArgb(243, 243, 243)), new Rectangle(8, 16, 4, 18));
            //	_with2.FillRectangle(new SolidBrush(FlatColor), 16, 16, 4, 18);
            if (Icon != null)
                with2.DrawImageUnscaledAndClipped(
                    Icon.GetThumbnailImage(27,27,null,Handle), 
                    new Rectangle(3,3,27,27));
            with2.DrawString(Text, Font, new SolidBrush(_textColor), 
                new Rectangle(35, Convert.ToInt16(15 - Font.Size), _w, _h), Helpers.NearSf);

            //-- Border
            with2.DrawRectangle(new Pen(_borderColor), Base);

            base.OnPaint(e);
            g.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(b, 0, 0);
            b.Dispose();
        }
    }
}
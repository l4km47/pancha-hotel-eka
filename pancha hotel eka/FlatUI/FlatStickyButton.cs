using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatStickyButton : Control
	{
		private int _w;
		private int _h;
		private MouseState _state = MouseState.None;
		private bool _rounded = false;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			_state = MouseState.Down;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_state = MouseState.Over;
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseState.Over;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_state = MouseState.None;
			Invalidate();
		}

		private bool[] GetConnectedSides()
		{
			bool[] Bool = new bool[4] { false, false, false, false };

			foreach (Control b in Parent.Controls)
			{
				if (b is FlatStickyButton)
				{
					if (ReferenceEquals(b, this) || !Rect.IntersectsWith(Rect))
						continue;
					double a = (Math.Atan2(Left - b.Left, Top - b.Top) * 2 / Math.PI);
					if (a / 1 == a)
						Bool[(int)a + 1] = true;
				}
			}

			return Bool;
		}

		private Rectangle Rect
		{
			get { return new Rectangle(Left, Top, Width, Height); }
		}

		[Category("Colors")]
		public Color BaseColor
		{
			get { return _baseColor; }
			set { _baseColor = value; }
		}

		[Category("Colors")]
		public Color TextColor
		{
			get { return _textColor; }
			set { _textColor = value; }
		}

		[Category("Options")]
		public bool Rounded
		{
			get { return _rounded; }
			set { _rounded = value; }
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			//Height = 32
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			//Size = New Size(112, 32)
		}

		private Color _baseColor = Helpers.FlatColor;
		private Color _textColor = Color.FromArgb(243, 243, 243);

		public FlatStickyButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			Size = new Size(106, 32);
			BackColor = Color.Transparent;
			Font = new Font("Segoe UI", 12);
			Cursor = Cursors.Hand;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width;
			_h = Height;

			GraphicsPath gp = new GraphicsPath();

			bool[] gcs = GetConnectedSides();
			// dynamic RoundedBase = Helpers.RoundRect(0, 0, W, H, ???, !(GCS(2) | GCS(1)), !(GCS(1) | GCS(0)), !(GCS(3) | GCS(0)), !(GCS(3) | GCS(2)));
			GraphicsPath roundedBase = Helpers.RoundRect(0, 0, _w, _h, 0.3, !(gcs[2] || gcs[1]), !(gcs[1] || gcs[0]), !(gcs[3] || gcs[0]), !(gcs[3] || gcs[2]));
			Rectangle Base = new Rectangle(0, 0, _w, _h);

			var with17 = g;
			with17.SmoothingMode = SmoothingMode.HighQuality;
			with17.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with17.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with17.Clear(BackColor);

			switch (_state) {
				case MouseState.None:
					if (Rounded) {
						//-- Base
						gp = roundedBase;
						with17.FillPath(new SolidBrush(_baseColor), gp);

						//-- Text
						with17.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					} else {
						//-- Base
						with17.FillRectangle(new SolidBrush(_baseColor), Base);

						//-- Text
						with17.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					}
					break;
				case MouseState.Over:
					if (Rounded) {
						//-- Base
						gp = roundedBase;
						with17.FillPath(new SolidBrush(_baseColor), gp);
						with17.FillPath(new SolidBrush(Color.FromArgb(20, Color.White)), gp);

						//-- Text
						with17.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					} else {
						//-- Base
						with17.FillRectangle(new SolidBrush(_baseColor), Base);
						with17.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.White)), Base);

						//-- Text
						with17.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					}
					break;
				case MouseState.Down:
					if (Rounded) {
						//-- Base
						gp = roundedBase;
						with17.FillPath(new SolidBrush(_baseColor), gp);
						with17.FillPath(new SolidBrush(Color.FromArgb(20, Color.Black)), gp);

						//-- Text
						with17.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					} else {
						//-- Base
						with17.FillRectangle(new SolidBrush(_baseColor), Base);
						with17.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.Black)), Base);

						//-- Text
						with17.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					}
					break;
			}

			base.OnPaint(e);
			g.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(b, 0, 0);
			b.Dispose();
		}

		private void UpdateColors()
		{
			FlatColors colors = Helpers.GetColors(this);

			_baseColor = colors.Flat;
		}
	}
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatButton : Control
	{
		private int _w;
		private int _h;
		private bool _rounded = false;
		private MouseState _state = MouseState.None;

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

		private Color _baseColor = Helpers.FlatColor;
		private Color _textColor = Color.FromArgb(243, 243, 243);

		public FlatButton()
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
			_w = Width - 1;
			_h = Height - 1;

			GraphicsPath gp = new GraphicsPath();
			Rectangle Base = new Rectangle(0, 0, _w, _h);

			var with8 = g;
			with8.SmoothingMode = SmoothingMode.HighQuality;
			with8.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with8.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with8.Clear(BackColor);

			switch (_state)
			{
				case MouseState.None:
					if (Rounded)
					{
						//-- Base
						gp = Helpers.RoundRec(Base, 6);
						with8.FillPath(new SolidBrush(_baseColor), gp);

						//-- Text
						with8.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					}
					else
					{
						//-- Base
						with8.FillRectangle(new SolidBrush(_baseColor), Base);

						//-- Text
						with8.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					}
					break;
				case MouseState.Over:
					if (Rounded)
					{
						//-- Base
						gp = Helpers.RoundRec(Base, 6);
						with8.FillPath(new SolidBrush(_baseColor), gp);
						with8.FillPath(new SolidBrush(Color.FromArgb(20, Color.White)), gp);

						//-- Text
						with8.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					}
					else
					{
						//-- Base
						with8.FillRectangle(new SolidBrush(_baseColor), Base);
						with8.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.White)), Base);

						//-- Text
						with8.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					}
					break;
				case MouseState.Down:
					if (Rounded)
					{
						//-- Base
						gp = Helpers.RoundRec(Base, 6);
						with8.FillPath(new SolidBrush(_baseColor), gp);
						with8.FillPath(new SolidBrush(Color.FromArgb(20, Color.Black)), gp);

						//-- Text
						with8.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
					}
					else
					{
						//-- Base
						with8.FillRectangle(new SolidBrush(_baseColor), Base);
						with8.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.Black)), Base);

						//-- Text
						with8.DrawString(Text, Font, new SolidBrush(_textColor), Base, Helpers.CenterSf);
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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	[DefaultEvent("CheckedChanged")]
	public class FlatCheckBox : Control
	{
		private int _w;
		private int _h;
		private MouseState _state = MouseState.None;
		private _Options _o;
		private bool _checked;

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		public bool Checked
		{
			get { return _checked; }
			set
			{
				_checked = value;
				Invalidate();
			}
		}

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);
		protected override void OnClick(EventArgs e)
		{
			_checked = !_checked;
			if (CheckedChanged != null)
			{
				CheckedChanged(this);
			}
			base.OnClick(e);
		}

		[Flags()]
		public enum _Options
		{
			Style1,
			Style2
		}

		[Category("Options")]
		public _Options Options
		{
			get { return _o; }
			set { _o = value; }
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 22;
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

		private Color _baseColor = Color.FromArgb(45, 47, 49);
		private Color _textColor = Color.FromArgb(243, 243, 243);
		private Color _borderColor = Helpers.FlatColor;

		public FlatCheckBox()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			BackColor = Color.FromArgb(60, 70, 73);
			Cursor = Cursors.Hand;
			Font = new Font("Segoe UI", 10);
			Size = new Size(112, 22);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width - 1;
			_h = Height - 1;

			Rectangle Base = new Rectangle(0, 2, Height - 5, Height - 5);

			var with11 = g;
			with11.SmoothingMode = SmoothingMode.HighQuality;
			with11.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with11.Clear(BackColor);
			switch (_o)
			{
				case _Options.Style1:
					//-- Style 1
					//-- Base
					with11.FillRectangle(new SolidBrush(_baseColor), Base);

					switch (_state)
					{
						case MouseState.Over:
							//-- Base
							with11.DrawRectangle(new Pen(_borderColor), Base);
							break;
						case MouseState.Down:
							//-- Base
							with11.DrawRectangle(new Pen(_borderColor), Base);
							break;
					}

					//-- If Checked
					if (Checked)
					{
						with11.DrawString("ü", new Font("Wingdings", 18), new SolidBrush(_borderColor), new Rectangle(5, 7, _h - 9, _h - 9), Helpers.CenterSf);
					}

					//-- If Enabled
					if (Enabled == false)
					{
						with11.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), Base);
						with11.DrawString(Text, Font, new SolidBrush(Color.FromArgb(140, 142, 143)), new Rectangle(20, 2, _w, _h), Helpers.NearSf);
					}

					//-- Text
					with11.DrawString(Text, Font, new SolidBrush(_textColor), new Rectangle(20, 2, _w, _h), Helpers.NearSf);
					break;
				case _Options.Style2:
					//-- Style 2
					//-- Base
					with11.FillRectangle(new SolidBrush(_baseColor), Base);

					switch (_state)
					{
						case MouseState.Over:
							//-- Base
							with11.DrawRectangle(new Pen(_borderColor), Base);
							with11.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), Base);
							break;
						case MouseState.Down:
							//-- Base
							with11.DrawRectangle(new Pen(_borderColor), Base);
							with11.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), Base);
							break;
					}

					//-- If Checked
					if (Checked)
					{
						with11.DrawString("ü", new Font("Wingdings", 18), new SolidBrush(_borderColor), new Rectangle(5, 7, _h - 9, _h - 9), Helpers.CenterSf);
					}

					//-- If Enabled
					if (Enabled == false)
					{
						with11.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), Base);
						with11.DrawString(Text, Font, new SolidBrush(Color.FromArgb(48, 119, 91)), new Rectangle(20, 2, _w, _h), Helpers.NearSf);
					}

					//-- Text
					with11.DrawString(Text, Font, new SolidBrush(_textColor), new Rectangle(20, 2, _w, _h), Helpers.NearSf);
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

			_borderColor = colors.Flat;
		}
	}
}
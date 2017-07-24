using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatNumeric : Control
	{
		private int _w;
		private int _h;
		private MouseState _state = MouseState.None;
		private int _x;
		private int _y;
		private long _value;
		private long _min;
		private long _max;
		private bool _bool;

		public long Value
		{
			get { return _value; }
			set
			{
				if (value <= _max & value >= _min)
					_value = value;
				Invalidate();
			}
		}

		public long Maximum
		{
			get { return _max; }
			set
			{
				if (value > _min)
					_max = value;
				if (_value > _max)
					_value = _max;
				Invalidate();
			}
		}

		public long Minimum
		{
			get { return _min; }
			set
			{
				if (value < _max)
					_min = value;
				if (_value < _min)
					_value = Minimum;
				Invalidate();
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			_x = e.Location.X;
			_y = e.Location.Y;
			Invalidate();
			if (e.X < Width - 23)
				Cursor = Cursors.IBeam;
			else
				Cursor = Cursors.Hand;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (_x > Width - 21 && _x < Width - 3)
			{
				if (_y < 15)
				{
					if ((Value + 1) <= _max)
						_value += 1;
				}
				else
				{
					if ((Value - 1) >= _min)
						_value -= 1;
				}
			}
			else
			{
				_bool = !_bool;
				Focus();
			}
			Invalidate();
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			try
			{
				if (_bool)
					_value = Convert.ToInt64(_value.ToString() + e.KeyChar.ToString());
				if (_value > _max)
					_value = _max;
				Invalidate();
			}
			catch
			{
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Back)
			{
				Value = 0;
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 30;
		}

		[Category("Colors")]
		public Color BaseColor
		{
			get { return _baseColor; }
			set { _baseColor = value; }
		}

		[Category("Colors")]
		public Color ButtonColor
		{
			get { return _buttonColor; }
			set { _buttonColor = value; }
		}

		private Color _baseColor = Color.FromArgb(45, 47, 49);
		private Color _buttonColor = Helpers.FlatColor;

		public FlatNumeric()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			Font = new Font("Segoe UI", 10);
			BackColor = Color.FromArgb(60, 70, 73);
			ForeColor = Color.White;
			_min = 0;
			_max = 9999999;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width;
			_h = Height;

			Rectangle Base = new Rectangle(0, 0, _w, _h);

			var with18 = g;
			with18.SmoothingMode = SmoothingMode.HighQuality;
			with18.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with18.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with18.Clear(BackColor);

			//-- Base
			with18.FillRectangle(new SolidBrush(_baseColor), Base);
			with18.FillRectangle(new SolidBrush(_buttonColor), new Rectangle(Width - 24, 0, 24, _h));

			//-- Add
			with18.DrawString("+", new Font("Segoe UI", 12), Brushes.White, new Point(Width - 12, 8), Helpers.CenterSf);
			//-- Subtract
			with18.DrawString("-", new Font("Segoe UI", 10, FontStyle.Bold), Brushes.White, new Point(Width - 12, 22), Helpers.CenterSf);

			//-- Text
			with18.DrawString(Value.ToString(), Font, Brushes.White, new Rectangle(5, 1, _w, _h), new StringFormat { LineAlignment = StringAlignment.Center });

			base.OnPaint(e);
			g.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(b, 0, 0);
			b.Dispose();
		}

		private void UpdateColors()
		{
			FlatColors colors = Helpers.GetColors(this);

			_buttonColor = colors.Flat;
		}
	}
}

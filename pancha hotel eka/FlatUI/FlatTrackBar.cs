using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	[DefaultEvent("Scroll")]
	public class FlatTrackBar : Control
	{
		private int _w;
		private int _h;
		private int _val;
		private bool _bool;
		private Rectangle _track;
		private Rectangle _knob;
		private _Style _style;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				_val = Convert.ToInt32((float)(_value - _minimum) / (float)(_maximum - _minimum) * (float)(Width - 11));
				_track = new Rectangle(_val, 0, 10, 20);

				_bool = _track.Contains(e.Location);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (_bool && e.X > -1 && e.X < (Width + 1))
			{
				Value = _minimum + Convert.ToInt32((float)(_maximum - _minimum) * ((float)e.X / (float)Width));
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_bool = false;
		}

		[Flags()]
		public enum _Style
		{
			Slider,
			Knob
		}

		public _Style Style
		{
			get { return _style; }
			set { _style = value; }
		}

		[Category("Colors")]
		public Color TrackColor
		{
			get { return _trackColor; }
			set { _trackColor = value; }
		}

		[Category("Colors")]
		public Color HatchColor
		{
			get { return _hatchColor; }
			set { _hatchColor = value; }
		}

		public event ScrollEventHandler Scroll;
		public delegate void ScrollEventHandler(object sender);

		private int _minimum;
		public int Minimum
		{
			get
			{
				int functionReturnValue = 0;
				return functionReturnValue;
				return functionReturnValue;
			}
			set
			{
				if (value < 0)
				{
				}

				_minimum = value;

				if (value > _value)
					_value = value;
				if (value > _maximum)
					_maximum = value;
				Invalidate();
			}
		}

		private int _maximum = 10;
		public int Maximum
		{
			get { return _maximum; }
			set
			{
				if (value < 0)
				{
				}

				_maximum = value;
				if (value < _value)
					_value = value;
				if (value < _minimum)
					_minimum = value;
				Invalidate();
			}
		}

		private int _value;
		public int Value
		{
			get { return _value; }
			set
			{
				if (value == _value)
					return;

				if (value > _maximum || value < _minimum)
				{
				}

				_value = value;
				Invalidate();
				if (Scroll != null)
				{
					Scroll(this);
				}
			}
		}

		private bool _showValue = false;
		public bool ShowValue
		{
			get { return _showValue; }
			set { _showValue = value; }
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Subtract)
			{
				if (Value == 0)
					return;
				Value -= 1;
			}
			else if (e.KeyCode == Keys.Add)
			{
				if (Value == _maximum)
					return;
				Value += 1;
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 23;
		}

		private Color _baseColor = Color.FromArgb(45, 47, 49);
		private Color _trackColor = Helpers.FlatColor;
		private Color _sliderColor = Color.FromArgb(25, 27, 29);
		private Color _hatchColor = Color.FromArgb(23, 148, 92);

		public FlatTrackBar()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			Height = 18;

			BackColor = Color.FromArgb(60, 70, 73);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width - 1;
			_h = Height - 1;

			Rectangle Base = new Rectangle(1, 6, _w - 2, 8);
			GraphicsPath gp = new GraphicsPath();
			GraphicsPath gp2 = new GraphicsPath();

			var with20 = g;
			with20.SmoothingMode = SmoothingMode.HighQuality;
			with20.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with20.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with20.Clear(BackColor);

			//-- Value
			_val = Convert.ToInt32((float)(_value - _minimum) / (float)(_maximum - _minimum) * (float)(_w - 10));
			_track = new Rectangle(_val, 0, 10, 20);
			_knob = new Rectangle(_val, 4, 11, 14);

			//-- Base
			gp.AddRectangle(Base);
			with20.SetClip(gp);
			with20.FillRectangle(new SolidBrush(_baseColor), new Rectangle(0, 7, _w, 8));
			with20.FillRectangle(new SolidBrush(_trackColor), new Rectangle(0, 7, _track.X + _track.Width, 8));
			with20.ResetClip();

			//-- Hatch Brush
			HatchBrush hb = new HatchBrush(HatchStyle.Plaid, HatchColor, _trackColor);
			with20.FillRectangle(hb, new Rectangle(-10, 7, _track.X + _track.Width, 8));

			//-- Slider/Knob
			switch (Style)
			{
				case _Style.Slider:
					gp2.AddRectangle(_track);
					with20.FillPath(new SolidBrush(_sliderColor), gp2);
					break;
				case _Style.Knob:
					gp2.AddEllipse(_knob);
					with20.FillPath(new SolidBrush(_sliderColor), gp2);
					break;
			}

			//-- Show the value 
			if (ShowValue)
			{
				with20.DrawString(Value.ToString(), new Font("Segoe UI", 8), Brushes.White, new Rectangle(1, 6, _w, _h), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Far
				});
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

			_trackColor = colors.Flat;
		}
	}
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatProgressBar : Control
	{
		private int _w;
		private int _h;
		private int _value = 0;
		private int _maximum = 100;
		private bool _pattern = true;
		private bool _showBalloon = true;
		private bool _percentSign = false;

		[Category("Control")]
		public int Maximum
		{
			get { return _maximum; }
			set
			{
				if (value < _value)
					_value = value;
				_maximum = value;
				Invalidate();
			}
		}

		[Category("Control")]
		public int Value
		{
			get
			{
				return _value;
				/*
				switch (_Value)
				{
					case 0:
						return 0;
						Invalidate();
						break;
					default:
						return _Value;
						Invalidate();
						break;
				}
				*/
			}
			set
			{
				if (value > _maximum)
				{
					value = _maximum;
					Invalidate();
				}

				_value = value;
				Invalidate();
			}
		}

		public bool Pattern
		{
			get { return _pattern; }
			set { _pattern = value; }
		}

		public bool ShowBalloon
		{
			get { return _showBalloon; }
			set { _showBalloon = value; }
		}

		public bool PercentSign
		{
			get { return _percentSign; }
			set { _percentSign = value; }
		}

		[Category("Colors")]
		public Color ProgressColor
		{
			get { return _progressColor; }
			set { _progressColor = value; }
		}

		[Category("Colors")]
		public Color DarkerProgress
		{
			get { return _darkerProgress; }
			set { _darkerProgress = value; }
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 42;
		}

		protected override void CreateHandle()
		{
			base.CreateHandle();
			Height = 42;
		}

		public void Increment(int amount)
		{
			Value += amount;
		}

		private Color _baseColor = Color.FromArgb(45, 47, 49);
		private Color _progressColor = Helpers.FlatColor;
		private Color _darkerProgress = Color.FromArgb(23, 148, 92);

		public FlatProgressBar()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			BackColor = Color.FromArgb(60, 70, 73);
			Height = 42;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width - 1;
			_h = Height - 1;

			Rectangle Base = new Rectangle(0, 24, _w, _h);
			GraphicsPath gp = new GraphicsPath();
			GraphicsPath gp2 = new GraphicsPath();
			GraphicsPath gp3 = new GraphicsPath();

			var with15 = g;
			with15.SmoothingMode = SmoothingMode.HighQuality;
			with15.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with15.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with15.Clear(BackColor);

			//-- Progress Value
			//int iValue = Convert.ToInt32(((float)_Value) / ((float)(_Maximum * Width)));
			float percent = ((float)_value) / ((float)_maximum);
			int iValue = (int)(percent * ((float)Width));

			switch (Value)
			{
				case 0:
					//-- Base
					with15.FillRectangle(new SolidBrush(_baseColor), Base);
					//--Progress
					with15.FillRectangle(new SolidBrush(_progressColor), new Rectangle(0, 24, iValue - 1, _h - 1));
					break;
				case 100:
					//-- Base
					with15.FillRectangle(new SolidBrush(_baseColor), Base);
					//--Progress
					with15.FillRectangle(new SolidBrush(_progressColor), new Rectangle(0, 24, iValue - 1, _h - 1));
					break;
				default:
					//-- Base
					with15.FillRectangle(new SolidBrush(_baseColor), Base);

					//--Progress
					gp.AddRectangle(new Rectangle(0, 24, iValue - 1, _h - 1));
					with15.FillPath(new SolidBrush(_progressColor), gp);

					if (_pattern)
					{
						//-- Hatch Brush
						HatchBrush hb = new HatchBrush(HatchStyle.Plaid, _darkerProgress, _progressColor);
						with15.FillRectangle(hb, new Rectangle(0, 24, iValue - 1, _h - 1));
					}

					if (_showBalloon)
					{
						//-- Balloon
						Rectangle balloon = new Rectangle(iValue - 18, 0, 34, 16);
						gp2 = Helpers.RoundRec(balloon, 4);
						with15.FillPath(new SolidBrush(_baseColor), gp2);

						//-- Arrow
						gp3 = Helpers.DrawArrow(iValue - 9, 16, true);
						with15.FillPath(new SolidBrush(_baseColor), gp3);

						//-- Value > You can add "%" > value & "%"
						string text = (_percentSign ? Value.ToString() + "%" : Value.ToString());
						int wOffset = (_percentSign ? iValue - 15 : iValue - 11);
						with15.DrawString(text, new Font("Segoe UI", 10), new SolidBrush(_progressColor), new Rectangle(wOffset, -2, _w, _h), Helpers.NearSf);
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

			_progressColor = colors.Flat;
		}
	}
}

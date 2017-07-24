using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatStatusBar : Control
	{
		private int _w;
		private int _h;
		private bool _showTimeDate = false;

		protected override void CreateHandle()
		{
			base.CreateHandle();
			Dock = DockStyle.Bottom;
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
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

		[Category("Colors")]
		public Color RectColor
		{
			get { return _rectColor; }
			set { _rectColor = value; }
		}

		public bool ShowTimeDate
		{
			get { return _showTimeDate; }
			set { _showTimeDate = value; }
		}

		public string GetTimeDate()
		{
			return DateTime.Now.Date + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
		}

		private Color _baseColor = Color.FromArgb(45, 47, 49);
		private Color _textColor = Color.White;
		private Color _rectColor = Helpers.FlatColor;

		public FlatStatusBar()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			Font = new Font("Segoe UI", 8);
			ForeColor = Color.White;
			Size = new Size(Width, 20);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width;
			_h = Height;

			Rectangle Base = new Rectangle(0, 0, _w, _h);

			var with21 = g;
			with21.SmoothingMode = SmoothingMode.HighQuality;
			with21.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with21.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with21.Clear(BaseColor);

			//-- Base
			with21.FillRectangle(new SolidBrush(BaseColor), Base);

			//-- Text
			with21.DrawString(Text, Font, Brushes.White, new Rectangle(10, 4, _w, _h), Helpers.NearSf);

			//-- Rectangle
			with21.FillRectangle(new SolidBrush(_rectColor), new Rectangle(4, 4, 4, 14));

			//-- TimeDate
			if (ShowTimeDate)
			{
				with21.DrawString(GetTimeDate(), Font, new SolidBrush(_textColor), new Rectangle(-4, 2, _w, _h), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Center
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

			_rectColor = colors.Flat;
		}
	}
}

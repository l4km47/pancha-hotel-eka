using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatColorPalette : Control
	{
		private int _w;
		private int _h;

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Width = 180;
			Height = 80;
		}

		[Category("Colors")]
		public Color Red
		{
			get { return _red; }
			set { _red = value; }
		}

		[Category("Colors")]
		public Color Cyan
		{
			get { return _cyan; }
			set { _cyan = value; }
		}

		[Category("Colors")]
		public Color Blue
		{
			get { return _blue; }
			set { _blue = value; }
		}

		[Category("Colors")]
		public Color LimeGreen
		{
			get { return _limeGreen; }
			set { _limeGreen = value; }
		}

		[Category("Colors")]
		public Color Orange
		{
			get { return _orange; }
			set { _orange = value; }
		}

		[Category("Colors")]
		public Color Purple
		{
			get { return _purple; }
			set { _purple = value; }
		}

		[Category("Colors")]
		public Color Black
		{
			get { return _black; }
			set { _black = value; }
		}

		[Category("Colors")]
		public Color Gray
		{
			get { return _gray; }
			set { _gray = value; }
		}

		[Category("Colors")]
		public Color White
		{
			get { return _white; }
			set { _white = value; }
		}

		private Color _red = Color.FromArgb(220, 85, 96);
		private Color _cyan = Color.FromArgb(10, 154, 157);
		private Color _blue = Color.FromArgb(0, 128, 255);
		private Color _limeGreen = Color.FromArgb(35, 168, 109);
		private Color _orange = Color.FromArgb(253, 181, 63);
		private Color _purple = Color.FromArgb(155, 88, 181);
		private Color _black = Color.FromArgb(45, 47, 49);
		private Color _gray = Color.FromArgb(63, 70, 73);
		private Color _white = Color.FromArgb(243, 243, 243);

		public FlatColorPalette()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			BackColor = Color.FromArgb(60, 70, 73);
			Size = new Size(160, 80);
			Font = new Font("Segoe UI", 12);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width - 1;
			_h = Height - 1;

			var with6 = g;
			with6.SmoothingMode = SmoothingMode.HighQuality;
			with6.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with6.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with6.Clear(BackColor);

			//-- Colors 
			with6.FillRectangle(new SolidBrush(_red), new Rectangle(0, 0, 20, 40));
			with6.FillRectangle(new SolidBrush(_cyan), new Rectangle(20, 0, 20, 40));
			with6.FillRectangle(new SolidBrush(_blue), new Rectangle(40, 0, 20, 40));
			with6.FillRectangle(new SolidBrush(_limeGreen), new Rectangle(60, 0, 20, 40));
			with6.FillRectangle(new SolidBrush(_orange), new Rectangle(80, 0, 20, 40));
			with6.FillRectangle(new SolidBrush(_purple), new Rectangle(100, 0, 20, 40));
			with6.FillRectangle(new SolidBrush(_black), new Rectangle(120, 0, 20, 40));
			with6.FillRectangle(new SolidBrush(_gray), new Rectangle(140, 0, 20, 40));
			with6.FillRectangle(new SolidBrush(_white), new Rectangle(160, 0, 20, 40));

			//-- Text
			with6.DrawString("Color Palette", Font, new SolidBrush(_white), new Rectangle(0, 22, _w, _h), Helpers.CenterSf);

			base.OnPaint(e);
			g.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(b, 0, 0);
			b.Dispose();
		}
	}
}

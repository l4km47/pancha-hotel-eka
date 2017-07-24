using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatGroupBox : ContainerControl
	{
		private int _w;
		private int _h;
		private bool _showText = true;

		[Category("Colors")]
		public Color BaseColor
		{
			get { return _baseColor; }
			set { _baseColor = value; }
		}

		public bool ShowText
		{
			get { return _showText; }
			set { _showText = value; }
		}

		private Color _baseColor = Color.FromArgb(60, 70, 73);
		private Color _textColor = Helpers.FlatColor;

		public FlatGroupBox()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			BackColor = Color.Transparent;
			Size = new Size(240, 180);
			Font = new Font("Segoe ui", 10);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width - 1;
			_h = Height - 1;

			GraphicsPath gp = new GraphicsPath();
			GraphicsPath gp2 = new GraphicsPath();
			GraphicsPath gp3 = new GraphicsPath();
			Rectangle Base = new Rectangle(8, 8, _w - 16, _h - 16);

			var with7 = g;
			with7.SmoothingMode = SmoothingMode.HighQuality;
			with7.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with7.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with7.Clear(BackColor);

			//-- Base
			gp = Helpers.RoundRec(Base, 8);
			with7.FillPath(new SolidBrush(_baseColor), gp);

			//-- Arrows
			gp2 = Helpers.DrawArrow(28, 2, false);
			with7.FillPath(new SolidBrush(_baseColor), gp2);
			gp3 = Helpers.DrawArrow(28, 8, true);
			with7.FillPath(new SolidBrush(Color.FromArgb(60, 70, 73)), gp3);

			//-- if ShowText
			if (ShowText)
			{
				with7.DrawString(Text, Font, new SolidBrush(_textColor), new Rectangle(16, 16, _w, _h), Helpers.NearSf);
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

			_textColor = colors.Flat;
		}
	}
}

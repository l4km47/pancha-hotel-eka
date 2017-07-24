using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	[DefaultEvent("CheckedChanged")]
	public class FlatToggle : Control
	{
		private int _w;
		private int _h;
		private _Options _o;
		private bool _checked = false;
		private MouseState _state = MouseState.None;

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		[Flags()]
		public enum _Options
		{
			Style1,
			Style2,
			Style3,
			Style4,
			//-- TODO: New Style
			Style5
			//-- TODO: New Style
		}

		[Category("Options")]
		public _Options Options
		{
			get { return _o; }
			set { _o = value; }
		}

		[Category("Options")]
		public bool Checked
		{
			get { return _checked; }
			set { _checked = value; }
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Width = 76;
			Height = 33;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseState.Over;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			_state = MouseState.Down;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_state = MouseState.None;
			Invalidate();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_state = MouseState.Over;
			Invalidate();
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			_checked = !_checked;
			if (CheckedChanged != null)
			{
				CheckedChanged(this);
			}
		}

		private Color _baseColor = Helpers.FlatColor;
		private Color _baseColorRed = Color.FromArgb(220, 85, 96);
		private Color _bgColor = Color.FromArgb(84, 85, 86);
		private Color _toggleColor = Color.FromArgb(45, 47, 49);
		private Color _textColor = Color.FromArgb(243, 243, 243);

		public FlatToggle()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			BackColor = Color.Transparent;
			Size = new Size(44, Height + 1);
			Cursor = Cursors.Hand;
			Font = new Font("Segoe UI", 10);
			Size = new Size(76, 33);
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
			Rectangle Base = new Rectangle(0, 0, _w, _h);
			Rectangle toggle = new Rectangle(Convert.ToInt32(_w / 2), 0, 38, _h);

			var with9 = g;
			with9.SmoothingMode = SmoothingMode.HighQuality;
			with9.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with9.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with9.Clear(BackColor);

			switch (_o)
			{
				case _Options.Style1:
					//-- Style 1
					//-- Base
					gp = Helpers.RoundRec(Base, 6);
					gp2 = Helpers.RoundRec(toggle, 6);
					with9.FillPath(new SolidBrush(_bgColor), gp);
					with9.FillPath(new SolidBrush(_toggleColor), gp2);

					//-- Text
					with9.DrawString("OFF", Font, new SolidBrush(_bgColor), new Rectangle(19, 1, _w, _h), Helpers.CenterSf);

					if (Checked)
					{
						//-- Base
						gp = Helpers.RoundRec(Base, 6);
						gp2 = Helpers.RoundRec(new Rectangle(Convert.ToInt32(_w / 2), 0, 38, _h), 6);
						with9.FillPath(new SolidBrush(_toggleColor), gp);
						with9.FillPath(new SolidBrush(_baseColor), gp2);

						//-- Text
						with9.DrawString("ON", Font, new SolidBrush(_baseColor), new Rectangle(8, 7, _w, _h), Helpers.NearSf);
					}
					break;
				case _Options.Style2:
					//-- Style 2
					//-- Base
					gp = Helpers.RoundRec(Base, 6);
					toggle = new Rectangle(4, 4, 36, _h - 8);
					gp2 = Helpers.RoundRec(toggle, 4);
					with9.FillPath(new SolidBrush(_baseColorRed), gp);
					with9.FillPath(new SolidBrush(_toggleColor), gp2);

					//-- Lines
					with9.DrawLine(new Pen(_bgColor), 18, 20, 18, 12);
					with9.DrawLine(new Pen(_bgColor), 22, 20, 22, 12);
					with9.DrawLine(new Pen(_bgColor), 26, 20, 26, 12);

					//-- Text
					with9.DrawString("r", new Font("Marlett", 8), new SolidBrush(_textColor), new Rectangle(19, 2, Width, Height), Helpers.CenterSf);

					if (Checked)
					{
						gp = Helpers.RoundRec(Base, 6);
						toggle = new Rectangle(Convert.ToInt32(_w / 2) - 2, 4, 36, _h - 8);
						gp2 = Helpers.RoundRec(toggle, 4);
						with9.FillPath(new SolidBrush(_baseColor), gp);
						with9.FillPath(new SolidBrush(_toggleColor), gp2);

						//-- Lines
						with9.DrawLine(new Pen(_bgColor), Convert.ToInt32(_w / 2) + 12, 20, Convert.ToInt32(_w / 2) + 12, 12);
						with9.DrawLine(new Pen(_bgColor), Convert.ToInt32(_w / 2) + 16, 20, Convert.ToInt32(_w / 2) + 16, 12);
						with9.DrawLine(new Pen(_bgColor), Convert.ToInt32(_w / 2) + 20, 20, Convert.ToInt32(_w / 2) + 20, 12);

						//-- Text
						with9.DrawString("ü", new Font("Wingdings", 14), new SolidBrush(_textColor), new Rectangle(8, 7, Width, Height), Helpers.NearSf);
					}
					break;
				case _Options.Style3:
					//-- Style 3
					//-- Base
					gp = Helpers.RoundRec(Base, 16);
					toggle = new Rectangle(_w - 28, 4, 22, _h - 8);
					gp2.AddEllipse(toggle);
					with9.FillPath(new SolidBrush(_toggleColor), gp);
					with9.FillPath(new SolidBrush(_baseColorRed), gp2);

					//-- Text
					with9.DrawString("OFF", Font, new SolidBrush(_baseColorRed), new Rectangle(-12, 2, _w, _h), Helpers.CenterSf);

					if (Checked)
					{
						//-- Base
						gp = Helpers.RoundRec(Base, 16);
						toggle = new Rectangle(6, 4, 22, _h - 8);
						gp2.Reset();
						gp2.AddEllipse(toggle);
						with9.FillPath(new SolidBrush(_toggleColor), gp);
						with9.FillPath(new SolidBrush(_baseColor), gp2);

						//-- Text
						with9.DrawString("ON", Font, new SolidBrush(_baseColor), new Rectangle(12, 2, _w, _h), Helpers.CenterSf);
					}
					break;
				case _Options.Style4:
					//-- TODO: New Styles
					if (Checked)
					{
						//--
					}
					break;
				case _Options.Style5:
					//-- TODO: New Styles
					if (Checked)
					{
						//--
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

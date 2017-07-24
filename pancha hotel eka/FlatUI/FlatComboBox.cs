using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatComboBox : ComboBox
	{
		private int _w;
		private int _h;
		private int _startIndex = 0;
		private int _x;
		private int _y;

		private MouseState _state = MouseState.None;
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

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			_x = e.Location.X;
			_y = e.Location.Y;
			Invalidate();
			if (e.X < Width - 41)
				Cursor = Cursors.IBeam;
			else
				Cursor = Cursors.Hand;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			base.OnDrawItem(e);
			Invalidate();
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				Invalidate();
			}
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			Invalidate();
		}

		[Category("Colors")]
		public Color HoverColor
		{
			get { return _hoverColor; }
			set { _hoverColor = value; }
		}

		private int StartIndex
		{
			get { return _startIndex; }
			set
			{
				_startIndex = value;
				try
				{
					SelectedIndex = value;
				}
				catch
				{
				}
				Invalidate();
			}
		}

		public void DrawItem_(Object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0)
				return;
			e.DrawBackground();
			e.DrawFocusRectangle();

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				//-- Selected item
				e.Graphics.FillRectangle(new SolidBrush(_hoverColor), e.Bounds);
			}
			else
			{
				//-- Not Selected
				e.Graphics.FillRectangle(new SolidBrush(_baseColor), e.Bounds);
			}

			//-- Text
			e.Graphics.DrawString(GetItemText(Items[e.Index]), new Font("Segoe UI", 8), Brushes.White, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height));


			e.Graphics.Dispose();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 18;
		}

		private Color _baseColor = Color.FromArgb(25, 27, 29);
		private Color _bgColor = Color.FromArgb(45, 47, 49);
		private Color _hoverColor = Color.FromArgb(35, 168, 109);

		public FlatComboBox()
		{
			DrawItem += DrawItem_;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;

			DrawMode = DrawMode.OwnerDrawFixed;
			BackColor = Color.FromArgb(45, 45, 48);
			ForeColor = Color.White;
			DropDownStyle = ComboBoxStyle.DropDownList;
			Cursor = Cursors.Hand;
			StartIndex = 0;
			ItemHeight = 18;
			Font = new Font("Segoe UI", 8, FontStyle.Regular);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width;
			_h = Height;

			Rectangle Base = new Rectangle(0, 0, _w, _h);
			Rectangle button = new Rectangle(Convert.ToInt32(_w - 40), 0, _w, _h);
			GraphicsPath gp = new GraphicsPath();
			GraphicsPath gp2 = new GraphicsPath();

			var with16 = g;
			with16.Clear(Color.FromArgb(45, 45, 48));
			with16.SmoothingMode = SmoothingMode.HighQuality;
			with16.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with16.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			//-- Base
			with16.FillRectangle(new SolidBrush(_bgColor), Base);

			//-- Button
			gp.Reset();
			gp.AddRectangle(button);
			with16.SetClip(gp);
			with16.FillRectangle(new SolidBrush(_baseColor), button);
			with16.ResetClip();

			//-- Lines
			with16.DrawLine(Pens.White, _w - 10, 6, _w - 30, 6);
			with16.DrawLine(Pens.White, _w - 10, 12, _w - 30, 12);
			with16.DrawLine(Pens.White, _w - 10, 18, _w - 30, 18);

			//-- Text
			with16.DrawString(Text, Font, Brushes.White, new Point(4, 6), Helpers.NearSf);

			g.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(b, 0, 0);
			b.Dispose();
		}
	}
}

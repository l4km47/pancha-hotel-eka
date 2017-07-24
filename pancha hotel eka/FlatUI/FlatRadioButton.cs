using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	[DefaultEvent("CheckedChanged")]
	public class FlatRadioButton : Control
	{
		private MouseState _state = MouseState.None;
		private int _w;
		private int _h;
		private _Options _o;

		private bool _checked;
		public bool Checked
		{
			get { return _checked; }
			set
			{
				_checked = value;
				InvalidateControls();
				if (CheckedChanged != null)
				{
					CheckedChanged(this);
				}
				Invalidate();
			}
		}

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		protected override void OnClick(EventArgs e)
		{
			if (!_checked)
				Checked = true;
			base.OnClick(e);
		}

		private void InvalidateControls()
		{
			if (!IsHandleCreated || !_checked)
				return;
			foreach (Control c in Parent.Controls)
			{
				if (!ReferenceEquals(c, this) && c is FlatRadioButton)
				{
					((FlatRadioButton)c).Checked = false;
					Invalidate();
				}
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			InvalidateControls();
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
		private Color _borderColor = Helpers.FlatColor;
		private Color _textColor = Color.FromArgb(243, 243, 243);

		public FlatRadioButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			Cursor = Cursors.Hand;
			Size = new Size(100, 22);
			BackColor = Color.FromArgb(60, 70, 73);
			Font = new Font("Segoe UI", 10);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width - 1;
			_h = Height - 1;

			Rectangle Base = new Rectangle(0, 2, Height - 5, Height - 5);
			Rectangle dot = new Rectangle(4, 6, _h - 12, _h - 12);

			var with10 = g;
			with10.SmoothingMode = SmoothingMode.HighQuality;
			with10.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with10.Clear(BackColor);

			switch (_o)
			{
				case _Options.Style1:
					//-- Base
					with10.FillEllipse(new SolidBrush(_baseColor), Base);

					switch (_state)
					{
						case MouseState.Over:
							with10.DrawEllipse(new Pen(_borderColor), Base);
							break;
						case MouseState.Down:
							with10.DrawEllipse(new Pen(_borderColor), Base);
							break;
					}

					//-- If Checked 
					if (Checked)
					{
						with10.FillEllipse(new SolidBrush(_borderColor), dot);
					}
					break;
				case _Options.Style2:
					//-- Base
					with10.FillEllipse(new SolidBrush(_baseColor), Base);

					switch (_state)
					{
						case MouseState.Over:
							//-- Base
							with10.DrawEllipse(new Pen(_borderColor), Base);
							with10.FillEllipse(new SolidBrush(Color.FromArgb(118, 213, 170)), Base);
							break;
						case MouseState.Down:
							//-- Base
							with10.DrawEllipse(new Pen(_borderColor), Base);
							with10.FillEllipse(new SolidBrush(Color.FromArgb(118, 213, 170)), Base);
							break;
					}

					//-- If Checked
					if (Checked)
					{
						//-- Base
						with10.FillEllipse(new SolidBrush(_borderColor), dot);
					}
					break;
			}

			with10.DrawString(Text, Font, new SolidBrush(_textColor), new Rectangle(20, 2, _w, _h), Helpers.NearSf);

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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	[DefaultEvent("TextChanged")]
	public class FlatTextBox : Control
	{
		private int _w;
		private int _h;
		private MouseState _state = MouseState.None;
		private TextBox _tb;

		private HorizontalAlignment _textAlign = HorizontalAlignment.Left;
		[Category("Options")]
		public HorizontalAlignment TextAlign
		{
			get { return _textAlign; }
			set
			{
				_textAlign = value;
				if (_tb != null)
				{
					_tb.TextAlign = value;
				}
			}
		}

		private int _maxLength = 32767;
		[Category("Options")]
		public int MaxLength
		{
			get { return _maxLength; }
			set
			{
				_maxLength = value;
				if (_tb != null)
				{
					_tb.MaxLength = value;
				}
			}
		}

		private bool _readOnly;
		[Category("Options")]
		public bool ReadOnly
		{
			get { return _readOnly; }
			set
			{
				_readOnly = value;
				if (_tb != null)
				{
					_tb.ReadOnly = value;
				}
			}
		}

		private bool _useSystemPasswordChar;
		[Category("Options")]
		public bool UseSystemPasswordChar
		{
			get { return _useSystemPasswordChar; }
			set
			{
				_useSystemPasswordChar = value;
				if (_tb != null)
				{
					_tb.UseSystemPasswordChar = value;
				}
			}
		}

		private bool _multiline;
		[Category("Options")]
		public bool Multiline
		{
			get { return _multiline; }
			set
			{
				_multiline = value;
				if (_tb != null)
				{
					_tb.Multiline = value;

					if (value)
					{
						_tb.Height = Height - 11;
					}
					else
					{
						Height = _tb.Height + 11;
					}

				}
			}
		}

		private bool _focusOnHover = false;
		[Category("Options")]
		public bool FocusOnHover
		{
			get { return _focusOnHover; }
			set { _focusOnHover = value; }
		}

		[Category("Options")]
		public override string Text
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				if (_tb != null)
				{
					_tb.Text = value;
				}
			}
		}

		[Category("Options")]
		public override Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
				if (_tb != null)
				{
					_tb.Font = value;
					_tb.Location = new Point(3, 5);
					_tb.Width = Width - 6;

					if (!_multiline)
					{
						Height = _tb.Height + 11;
					}
				}
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (!Controls.Contains(_tb))
			{
				Controls.Add(_tb);
			}
		}

		private void OnBaseTextChanged(object s, EventArgs e)
		{
			Text = _tb.Text;
		}

		private void OnBaseKeyDown(object s, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				_tb.SelectAll();
				e.SuppressKeyPress = true;
			}
			if (e.Control && e.KeyCode == Keys.C)
			{
				_tb.Copy();
				e.SuppressKeyPress = true;
			}
		}

		protected override void OnResize(EventArgs e)
		{
			_tb.Location = new Point(5, 5);
			_tb.Width = Width - 10;

			if (_multiline)
			{
				_tb.Height = Height - 11;
			}
			else
			{
				Height = _tb.Height + 11;
			}

			base.OnResize(e);
		}

		[Category("Colors")]
		public Color TextColor
		{
			get { return _textColor; }
			set { _textColor = value; }
		}

		public override Color ForeColor
		{
			get { return _textColor; }
			set { _textColor = value; }
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
			_tb.Focus();
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseState.Over;
			if(FocusOnHover) _tb.Focus();
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_state = MouseState.None;
			Invalidate();
		}

		private Color _baseColor = Color.FromArgb(45, 47, 49);
		private Color _textColor = Color.FromArgb(192, 192, 192);
		private Color _borderColor = Helpers.FlatColor;

		public FlatTextBox()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			BackColor = Color.Transparent;

			_tb = new TextBox();
			_tb.Font = new Font("Segoe UI", 10);
			_tb.Text = Text;
			_tb.BackColor = _baseColor;
			_tb.ForeColor = _textColor;
			_tb.MaxLength = _maxLength;
			_tb.Multiline = _multiline;
			_tb.ReadOnly = _readOnly;
			_tb.UseSystemPasswordChar = _useSystemPasswordChar;
			_tb.BorderStyle = BorderStyle.None;
			_tb.Location = new Point(5, 5);
			_tb.Width = Width - 10;

			_tb.Cursor = Cursors.IBeam;

			if (_multiline)
			{
				_tb.Height = Height - 11;
			}
			else
			{
				Height = _tb.Height + 11;
			}

			_tb.TextChanged += OnBaseTextChanged;
			_tb.KeyDown += OnBaseKeyDown;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width - 1;
			_h = Height - 1;

			Rectangle Base = new Rectangle(0, 0, _w, _h);

			var with12 = g;
			with12.SmoothingMode = SmoothingMode.HighQuality;
			with12.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with12.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with12.Clear(BackColor);

			//-- Colors
			_tb.BackColor = _baseColor;
			_tb.ForeColor = _textColor;

			//-- Base
			with12.FillRectangle(new SolidBrush(_baseColor), Base);

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

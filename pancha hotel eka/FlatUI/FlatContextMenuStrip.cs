using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatContextMenuStrip : ContextMenuStrip
	{
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		public FlatContextMenuStrip()
			: base()
		{
			Renderer = new ToolStripProfessionalRenderer(new ColorTable());
			ShowImageMargin = false;
			ForeColor = Color.White;
			Font = new Font("Segoe UI", 8);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		}

		public class ColorTable : ProfessionalColorTable
		{
			[Category("Colors")]
			public Color _BackColor
			{
				get { return _backColor; }
				set { _backColor = value; }
			}

			[Category("Colors")]
			public Color _CheckedColor
			{
				get { return _checkedColor; }
				set { _checkedColor = value; }
			}

			[Category("Colors")]
			public Color _BorderColor
			{
				get { return _borderColor; }
				set { _borderColor = value; }
			}

			private Color _backColor = Color.FromArgb(45, 47, 49);
			private Color _checkedColor = Helpers.FlatColor;
			private Color _borderColor = Color.FromArgb(53, 58, 60);

			public override Color ButtonSelectedBorder
			{
				get { return _backColor; }
			}

			public override Color CheckBackground
			{
				get { return _checkedColor; }
			}

			public override Color CheckPressedBackground
			{
				get { return _checkedColor; }
			}

			public override Color CheckSelectedBackground
			{
				get { return _checkedColor; }
			}

			public override Color ImageMarginGradientBegin
			{
				get { return _checkedColor; }
			}

			public override Color ImageMarginGradientEnd
			{
				get { return _checkedColor; }
			}

			public override Color ImageMarginGradientMiddle
			{
				get { return _checkedColor; }
			}

			public override Color MenuBorder
			{
				get { return _borderColor; }
			}

			public override Color MenuItemBorder
			{
				get { return _borderColor; }
			}

			public override Color MenuItemSelected
			{
				get { return _checkedColor; }
			}

			public override Color SeparatorDark
			{
				get { return _borderColor; }
			}

			public override Color ToolStripDropDownBackground
			{
				get { return _backColor; }
			}
		}
	}
}

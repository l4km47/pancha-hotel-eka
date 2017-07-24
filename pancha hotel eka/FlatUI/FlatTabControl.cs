using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace pancha_hotel_eka.FlatUI
{
	public class FlatTabControl : TabControl
	{
		private int _w;
		private int _h;

		protected override void CreateHandle()
		{
			base.CreateHandle();
			Alignment = TabAlignment.Top;
		}

		[Category("Colors")]
		public Color BaseColor
		{
			get { return _baseColor; }
			set { _baseColor = value; }
		}

		[Category("Colors")]
		public Color ActiveColor
		{
			get { return _activeColor; }
			set { _activeColor = value; }
		}

		private Color _bgColor = Color.FromArgb(60, 70, 73);
		private Color _baseColor = Color.FromArgb(45, 47, 49);
		private Color _activeColor = Helpers.FlatColor;

		public FlatTabControl()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			BackColor = Color.FromArgb(60, 70, 73);

			Font = new Font("Segoe UI", 10);
			SizeMode = TabSizeMode.Fixed;
			ItemSize = new Size(120, 40);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateColors();

			Bitmap b = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(b);
			_w = Width - 1;
			_h = Height - 1;

			var with13 = g;
			with13.SmoothingMode = SmoothingMode.HighQuality;
			with13.PixelOffsetMode = PixelOffsetMode.HighQuality;
			with13.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			with13.Clear(_baseColor);

			try
			{
				SelectedTab.BackColor = _bgColor;
			}
			catch
			{
			}

			for (int i = 0; i <= TabCount - 1; i++)
			{
				Rectangle Base = new Rectangle(new Point(GetTabRect(i).Location.X + 2, GetTabRect(i).Location.Y), new Size(GetTabRect(i).Width, GetTabRect(i).Height));
				Rectangle baseSize = new Rectangle(Base.Location, new Size(Base.Width, Base.Height));

				if (i == SelectedIndex)
				{
					//-- Base
					with13.FillRectangle(new SolidBrush(_baseColor), baseSize);

					//-- Gradiant
					//.fill
					with13.FillRectangle(new SolidBrush(_activeColor), baseSize);

					//-- ImageList
					if (ImageList != null)
					{
						try
						{
							if (ImageList.Images[TabPages[i].ImageIndex] != null)
							{
								//-- Image
								with13.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(baseSize.Location.X + 8, baseSize.Location.Y + 6));
								//-- Text
								with13.DrawString("      " + TabPages[i].Text, Font, Brushes.White, baseSize, Helpers.CenterSf);
							}
							else
							{
								//-- Text
								with13.DrawString(TabPages[i].Text, Font, Brushes.White, baseSize, Helpers.CenterSf);
							}
						}
						catch (Exception ex)
						{
							throw new Exception(ex.Message);
						}
					}
					else
					{
						//-- Text
						with13.DrawString(TabPages[i].Text, Font, Brushes.White, baseSize, Helpers.CenterSf);
					}
				}
				else
				{
					//-- Base
					with13.FillRectangle(new SolidBrush(_baseColor), baseSize);

					//-- ImageList
					if (ImageList != null)
					{
						try
						{
							if (ImageList.Images[TabPages[i].ImageIndex] != null)
							{
								//-- Image
								with13.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(baseSize.Location.X + 8, baseSize.Location.Y + 6));
								//-- Text
								with13.DrawString("      " + TabPages[i].Text, Font, new SolidBrush(Color.White), baseSize, new StringFormat
								{
									LineAlignment = StringAlignment.Center,
									Alignment = StringAlignment.Center
								});
							}
							else
							{
								//-- Text
								with13.DrawString(TabPages[i].Text, Font, new SolidBrush(Color.White), baseSize, new StringFormat
								{
									LineAlignment = StringAlignment.Center,
									Alignment = StringAlignment.Center
								});
							}
						}
						catch (Exception ex)
						{
							throw new Exception(ex.Message);
						}
					}
					else
					{
						//-- Text
						with13.DrawString(TabPages[i].Text, Font, new SolidBrush(Color.White), baseSize, new StringFormat
						{
							LineAlignment = StringAlignment.Center,
							Alignment = StringAlignment.Center
						});
					}
				}
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

			_activeColor = colors.Flat;
		}
	}
}

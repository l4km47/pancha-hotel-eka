using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    [DefaultEvent("TextChanged")]
    public class YoutubeLabel : Control
    {

        #region  Draw Cotnrol 

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            using (SolidBrush tb = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, tb, ClientRectangle);
            }
        }

        #endregion

        #region  Constructors 

        public YoutubeLabel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            ForeColor = Color.Silver;
            Font = new Font("Myriad Pro", 10);
        }

        #endregion

        #region  Events 

        public new event TextChangedEventHandler TextChanged;
        public delegate void TextChangedEventHandler(object sender);

        protected override void OnResize(EventArgs e)
        {
            Height = Font.Height;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (TextChanged != null) TextChanged.Invoke(this);
            Invalidate();
        }

        #endregion

    }
}
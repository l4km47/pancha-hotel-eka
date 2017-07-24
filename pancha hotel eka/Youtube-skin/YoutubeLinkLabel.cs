using System.Drawing;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    class YoutubeLinkLabel : LinkLabel
    {

        #region  Constructors 

        public YoutubeLinkLabel()
        {
            Font = new Font("Myriad Pro", 10);
            BackColor = Color.Transparent;
            LinkColor = Colors.Blue;
            ActiveLinkColor = Color.FromArgb(40, 113, 164);
            VisitedLinkColor = Color.FromArgb(29, 83, 120);
            LinkBehavior = LinkBehavior.HoverUnderline;
        }

        #endregion

    }
}
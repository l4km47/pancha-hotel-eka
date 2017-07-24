using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using pancha_hotel_eka.Navgationbar;
using pancha_hotel_eka.Navgationbar.Themes;
using pancha_hotel_eka.Navgationbar.Themes.Definitions;

namespace pancha_hotel_eka
{
    public sealed partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var z80Navigation1 = new Z80_Navigation();
            splitContainer1.Panel1.Controls.Add(z80Navigation1);

            z80Navigation1.Dock = DockStyle.Fill;
            z80Navigation1.Location = new Point(0, 0);
            z80Navigation1.Name = "z80_Navigation1";
            z80Navigation1.TabIndex = 0;

            BackColor = new ThemeSelector(Theme.Dark).CurrentTheme.ItemDisableBackgroudColor;

            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            z80Navigation1.SelectedItem += Z80_Navigation1_SelectedItem;
            z80Navigation1.Initialize(new DemoItems().Sample1,
                new ThemeSelector(Theme.Dark).CurrentTheme);
            z80Navigation1.ItemSelect(0);
        }

        private void Z80_Navigation1_SelectedItem(NavBarItem item)
        {
            tabControl1.SelectedIndex = item.ID;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private int _distanceCopy;

        private void button8_Click(object sender, EventArgs e)
        {

            if (splitContainer1.SplitterDistance > 37)
            {
                _distanceCopy = splitContainer1.SplitterDistance + 3;
                splitContainer1.SplitterDistance = 37;
            }
            else
            {
                splitContainer1.SplitterDistance = _distanceCopy;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void formSkin1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (splitContainer1.SplitterDistance < 37)
                    splitContainer1.SplitterDistance = 37;
            }
            catch
            {
                //
            }

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }
    }

}

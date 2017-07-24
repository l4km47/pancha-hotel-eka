using System.Collections.Generic;
using pancha_hotel_eka.Navgationbar;

namespace pancha_hotel_eka
{
    public class DemoItems
    {

        public List<NavBarItem> Sample1;
        public List<NavBarItem> Sample2;
        public List<NavBarItem> Sample3;

        public DemoItems()
        {
            // Sample 1. Only Root items. No childs ----------------------------------------------------
            Sample1 = new List<NavBarItem>
            {
                new NavBarItem {ID = 0, Text = ""},
                new NavBarItem {ID = 1, Text = "Reservation"},
                new NavBarItem {ID = 2, Text = "Guests"},
                new NavBarItem {ID = 3, Text = "Restaurent"},
                new NavBarItem {ID = 4, Text = "Store"}
            };

        }
    }
}

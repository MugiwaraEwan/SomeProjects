using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu
{
    class ExitMenuItem : MenuItem
    {
        private ConsoleMenu _menu;

        public ExitMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        //text option to exit the menu
        public override string MenuText()
        {
            return "Exit";
        }

        //once selected menu is false, inactive
        public override void Select()
        {
            _menu.IsActive = false;
        }
    }
}

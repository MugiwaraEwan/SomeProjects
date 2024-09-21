using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu
{
    internal abstract class MenuItem
    { 
        //for selecting menu items
        public abstract void Select();

        //text for the menu
        public abstract string MenuText();
    }
}

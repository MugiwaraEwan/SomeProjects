using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu
{
    internal abstract class ConsoleMenu:MenuItem
    {
        //list of menu items
        protected List<MenuItem> menuItems = new List<MenuItem>();

        //active or inactive menu
        public bool IsActive { get; set; }

        //create menu
        public abstract void CreateMenu();

        //select method for the menu
        public override void Select()
        {
            //while menu is active adds menu text, shows the selection of menu items to choose from
            IsActive = true;
            do
            {
                CreateMenu();
                string output = $"{MenuText()}{Environment.NewLine}";
                int selection = InputChecker.GetIntegerInRange(1, menuItems.Count, this.ToString()) - 1;
                menuItems[selection].Select();
            } while (IsActive);
        }

        //displays each menu item numbered line by line to choose
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(MenuText());
            for (int i = 0; i < menuItems.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {menuItems[i].MenuText()}");
            }
            return sb.ToString();
        }
    }
}

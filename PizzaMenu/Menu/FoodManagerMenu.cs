using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMenu.Menu.Food;

namespace PizzaMenu.Menu
{
    internal class FoodManagerMenu:ConsoleMenu
    {
        private FoodManager _manager;

        //create list of foods
        public List<Recipe> Foods { get; private set; }

        //manager identifier
        public FoodManagerMenu(FoodManager manager)
        {
            _manager = manager;
        }

        //creates menu for pizza, burger and exit
        public override void CreateMenu()
        {
            menuItems.Clear();
            menuItems.Add(new AddPizzaMenuItem(_manager));
            menuItems.Add(new AddBurgerMenuItem(_manager));
            menuItems.Add(new ExitMenuItem(this));
        }

        //main menu text
        public override string MenuText()
        {
            return "Food Takeaway Menu" + Environment.NewLine + _manager.ToString();
        }
    }
}

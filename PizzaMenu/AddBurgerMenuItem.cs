using PizzaMenu.Menu.Food;
using PizzaMenu.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu
{
    internal class AddBurgerMenuItem : MenuItem
    {
        private FoodManager _manager;

        //manager identifier
        public AddBurgerMenuItem(FoodManager manager)
        {
            _manager = manager;
        }

        //text for the burger menu
        public override string MenuText()
        {
            return "Add Burger menu";
        }

        //select method for selecting burgers
        public override void Select()
        {
            StringBuilder sb = new StringBuilder($"{MenuText()}{Environment.NewLine}");
            int i = 1;

            //goes through the list of the burger styles and outputs them as options
            foreach (Recipe.Burger_Style p in Enum.GetValues(typeof(Recipe.Burger_Style)))
            {
                sb.AppendLine($"{i}. {p}");
                i++;
            }
            string burgerMenu = sb.ToString();
            int selectedIndex = InputChecker.GetIntegerInRange(1, Enum.GetValues(typeof(Recipe.Burger_Style)).Length, burgerMenu) - 1;

            //goes throught the list of burger styles from recipe
            Recipe.Burger_Style style = (Recipe.Burger_Style)selectedIndex;

            //asking what extra toppings the customer wants
            int cheese = InputChecker.GetIntegerInRange(BurgerOrder.MinGarnishes, BurgerOrder.MaxGarnishes, "How many extra garnishes of cheese do you want?");
            int friedOnion = InputChecker.GetIntegerInRange(BurgerOrder.MinGarnishes, BurgerOrder.MaxGarnishes, "How many extra garnishes of fried onions do you want?");
            int bacon = InputChecker.GetIntegerInRange(BurgerOrder.MinGarnishes, BurgerOrder.MaxGarnishes, "How many extra garnishes of bacon do you want?");


            //the burger object created
            BurgerOrder burger = new BurgerOrder(style, cheese, friedOnion, bacon);
            _manager.AddFood(burger);
        }
    }
}

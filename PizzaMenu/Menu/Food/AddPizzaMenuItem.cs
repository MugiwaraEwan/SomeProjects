using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMenu.Menu;
using static PizzaMenu.Menu.Food.Recipe;

namespace PizzaMenu.Menu.Food
{
    internal class AddPizzaMenuItem : MenuItem
    {
        private FoodManager _manager;

        public AddPizzaMenuItem(FoodManager manager) 
        {
            _manager = manager;
        }

        public override string MenuText()
        {
            return "Add Pizza menu";
        }

        public override void Select()
        {
            StringBuilder sb = new StringBuilder($"{MenuText()}{Environment.NewLine}");
            int i = 1;
            foreach (Recipe.Pizza_Style p in Enum.GetValues(typeof(Recipe.Pizza_Style)))
            {
                sb.AppendLine($"{i}. {p}");
                i++;
            }
            string colourMenu = sb.ToString();
            int selectedIndex = InputChecker.GetIntegerInRange(1, Enum.GetValues(typeof(Recipe.Pizza_Style)).Length, colourMenu) - 1;
            Recipe.Pizza_Style style = (Recipe.Pizza_Style)selectedIndex;

            //remove toppings
           // EditPizzaToppings selectToppingMenu = new EditPizzaToppings();
           // selectToppingMenu.Select();
            //_Toppings topping = selectToppingMenu.Topping();
                        

            //toppings
            int cheese = InputChecker.GetIntegerInRange(PizzaOrder.MinToppings, PizzaOrder.MaxToppings, "How many extra toppings of cheese do you want?");

            int tomatoSauce = InputChecker.GetIntegerInRange(PizzaOrder.MinToppings, PizzaOrder.MaxToppings, "How many extra toppings of tomato do you want?");

            int ham = InputChecker.GetIntegerInRange(PizzaOrder.MinToppings, PizzaOrder.MaxToppings, "How many extra toppings of ham do you want?");

            int mushroom = InputChecker.GetIntegerInRange(PizzaOrder.MinToppings, PizzaOrder.MaxToppings, "How many extra toppings of mushroom do you want?");

            int pepperoni = InputChecker.GetIntegerInRange(PizzaOrder.MinToppings, PizzaOrder.MaxToppings, "How many extra toppings of pepperoni do you want?");



            PizzaOrder pizza = new PizzaOrder(style, cheese, tomatoSauce, ham, mushroom, pepperoni);
            _manager.AddFood(pizza);
        }
    }
}

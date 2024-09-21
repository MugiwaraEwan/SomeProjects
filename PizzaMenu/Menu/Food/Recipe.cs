using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu.Food
{
    public abstract class Recipe
    {

        //type of pizza
        public enum Pizza_Style { Margherita, HamAndMushroom }
        public Pizza_Style PizzaStyle { set; get; }

        //pizza toppings to remove
        public enum _Toppings { Cheese, TomatoSauce, Ham, Mushroom, Pepperoni}
        public _Toppings Toppings { set; get; }


        //type of burger
        public enum Burger_Style { Plain, Cheese }
        public Burger_Style BurgerStyle { set; get; }

        //abstract price as it would be different for burger and pizza
        public abstract string Price();




    }
}

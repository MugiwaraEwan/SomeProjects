using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu.Food
{
    internal class PizzaOrder : Recipe
    {
        //pizza message
        public override string ToString()
        {

            //printed to the console once ordered
            return $"{PizzaStyle} Pizza with Price: {Price()} \n " +
                $"extra cheese: {Cheese} \n" +
                $"extra tomato sauce: {TomatoSauce} \n" +
                $"extra ham: {Ham} \n" +
                $"extra mushroom: {Mushroom} \n" +
                $"extra pepperoni: {Pepperoni} \n";

        }

        //min and max toppings
        public const int MinToppings = 0;
        public const int MaxToppings = 50;

        //toppings
        public int ADDToppings;
        public float Cheese;
        public float TomatoSauce;
        public float Ham;
        public float Mushroom;
        public float Pepperoni;

        //range for toppings
        public int AddToppings
        {
            get { return ADDToppings; }
            set { if (value >= MinToppings && value <= MaxToppings) { ADDToppings = value; } }
        }

        //method for making the pizza
        public PizzaOrder(Pizza_Style style, float nCheese, float nTomatoSauce, float nHam, float nMushroom, float nPepperoni)
        {
            //AddToppings = nAddToppings;
            Cheese = nCheese;
            TomatoSauce = nTomatoSauce;
            PizzaStyle = style;
            Ham = nHam;
            Mushroom = nMushroom;
            Pepperoni = nPepperoni;

            //writing to file, doing it here as its only called once per food creation so no repeated data in file
            string fileName = "AddedOrder.txt";

            StreamWriter outputFile = new StreamWriter(fileName, true);

            //written to file
            outputFile.WriteLine($"{PizzaStyle} Pizza with Price: {Price()} \n " +
                $"extra cheese: {Cheese} \n" +
                $"extra tomato sauce: {TomatoSauce} \n" +
                $"extra ham: {Ham} \n" +
                $"extra mushroom: {Mushroom} \n" +
                $"extra pepperoni: {Pepperoni} \n");

            outputFile.Close();
        }

        

        //returning the price of the pizza
        public override string Price()
        {
            if (PizzaStyle == Pizza_Style.Margherita)
            {
                //calulating topping and pizza prices together to 2dp
                float currentPizzaPrice = 0;
                float addedPizzaPrice = (float)(5 + Ham * 1.50 + Mushroom * 0.80 + Pepperoni * 1.20);
                currentPizzaPrice = (float)Math.Round((float)addedPizzaPrice, 2);

                string pizzaCost = ($"£{currentPizzaPrice}0");

                //margherita pizza price, cheese and tomato sauce has no extra charges if wanted more, other toppings do
                return pizzaCost;
            }
            else
            {
                //calulating topping and pizza prices together to 2dp
                float currentPizzaPrice = 0;
                float addedPizzaPrice = (float)(6.50 + Pepperoni * 1.20);
                currentPizzaPrice = (float)Math.Round((float)addedPizzaPrice, 2);

                //adding the £ and the 0 at the end
                string pizzaCost = ($"£{currentPizzaPrice}0");

                //ham and mushroom pizza price, only adding more pepperoni needs extra charge
                return pizzaCost;
            }
        }


    }
}

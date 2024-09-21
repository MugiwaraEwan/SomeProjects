using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu.Food
{
    internal class BurgerOrder : Recipe
    {
        //burger message
        public override string ToString()
        {
            return $"{BurgerStyle} Burger with Price: {Price()} \n" +
                $"Extra cheese: {Cheese} \n" +
                $"Extra Fried Onions: {FriedOnion} \n" +
                $"Extra Bacon: {Bacon} \n";
        }

        //min and max garnishes
        public const int MinGarnishes = 0;
        public const int MaxGarnishes = 50;

        //garnishes
        public int ADDGarnishes;
        public int Cheese;
        public int FriedOnion;
        public int Bacon;


        public int AddGarnishes
        {
            get { return ADDGarnishes; }
            set { if (value >= MinGarnishes && value <= MaxGarnishes) { ADDGarnishes = value; } }
        }

        //burger order method
        public BurgerOrder(Burger_Style style, int nCheese, int nFriedOnion, int nBacon)
        {
            Cheese = nCheese;
            FriedOnion = nFriedOnion;
            Bacon = nBacon;

            //writing to file, doing it here as its only called once per food creation so no repeated data in file
            string fileName = "AddedOrder.txt";

            StreamWriter outputFile = new StreamWriter(fileName, true);

            outputFile.WriteLine($"{BurgerStyle} Burger with Price: {Price()} \n" +
                $"Extra cheese: {Cheese} \n" +
                $"Extra Fried Onions: {FriedOnion} \n" +
                $"Extra Bacon: {Bacon} \n");

            outputFile.Close();
        }
        //burger price
        public override string Price()
        {
            if (BurgerStyle == Burger_Style.Plain)
            {
                //calulating garnishes and burger prices together to 2dp
                float currentBurgerPrice = 0;
                float addedBurgerPrice = (float)(3.50 + Cheese * 1 + FriedOnion * 0.8 + Bacon * 1.50);
                currentBurgerPrice = (float)Math.Round((float)addedBurgerPrice, 2);

                string burgerCost = ($"£{currentBurgerPrice}0");

                //plain burger price, all garnishes have a charge
                return burgerCost;

            }
            else
            {
                //ham and mushroom pizza price, only adding more pepperoni needs extra charge
                float currentBurgerPrice = 0;
                float addedBurgerPrice = (float)(4.50 + FriedOnion * 0.8 + Bacon * 1.50);
                currentBurgerPrice = (float)Math.Round((float)addedBurgerPrice, 2);

                string burgerCost = ($"£{currentBurgerPrice}0");

                return burgerCost;
            }
        }

    }
}

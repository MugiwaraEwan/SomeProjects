using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu.Food
{
    internal class FoodManager
    {
        //lists ordered food
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Recipe nFoods in Foods)
            {
                sb.Append(nFoods.ToString() + Environment.NewLine);
            }
            return sb.ToString();
        }
        //list of foods
        public List<Recipe> Foods { get; private set; }

        public FoodManager()
        {
            //food list
            Foods = new List<Recipe>();
        }

        //adding food to the list
        public void AddFood(Recipe nFood)
        {
            Foods.Add(nFood);
        }
    }
}

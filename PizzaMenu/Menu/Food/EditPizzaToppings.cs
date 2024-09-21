using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu.Food
{
    internal class EditPizzaToppings: MenuItem
    {

        private Recipe _recipe;

        public Recipe._Toppings Topping { get; private set; }
 
 
    public EditPizzaToppings(Recipe recipe)
    {
        _recipe = recipe;
    }
 
    public EditPizzaToppings() : this(null)
    {
    }
 
    public override void Select()
    {
        int selectedIndex = InputChecker.GetIntegerInRange(1, Enum.GetValues(typeof(Recipe._Toppings)).Length, ToString()) - 1;
        Topping = (Recipe._Toppings)selectedIndex;
        if (_recipe != null)
        {
            _recipe.Toppings = Topping;
        }
    }
 
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder($"{MenuText()}{Environment.NewLine}");
        int i = 1;
        foreach (Recipe._Toppings colour in Enum.GetValues(typeof(Recipe._Toppings)))
        {
            sb.AppendLine($"{i}. {colour}");
            i++;
        }
        return sb.ToString();
    }
 
    public override string MenuText()
    {
        return "Select Topping to change";
    }
    }
}

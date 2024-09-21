using PizzaMenu.Menu;
using PizzaMenu.Menu.Food;

//welcome message
Console.WriteLine("Welcome to the Store Menu!");

// file
string filePath = "CapstoneMenu.txt";
string[] lines = File.ReadAllLines(filePath);

// getting the name, then the welcome
string[] storeName = lines[0].Split(':');
Console.WriteLine($"Welcome to the {storeName[1]}, what would you like? Here is the menu.");

// to get the toppings and prices out of file
lines[1] = lines[1].Replace("Toppings:[<", "");
lines[1] = lines[1].Replace(">]", "");

// splitting the lines, getting rid of >,<
string[] parts = lines[1].Split(">,<");

// arrays for the toppings and their prices
string[] toppings = new string[parts.Length];
int[] toppingPrice = new int[parts.Length];

for (int i = 0; i < parts.Length; i++)
{
    // after splitting >,< now splitting , to get both topping and price seperately
    string[] subParts = parts[i].Split(',');
    toppings[i] = subParts[0];
    toppingPrice[i] = int.Parse(subParts[1]);
}

// to get pizza types out of file
lines[3] = lines[3].Replace("Pizza:<Name:", "");
lines[3] = lines[3].Replace(">", "");

string[] pizzatype = lines[3].Split(",");
Console.WriteLine(pizzatype[0]);

//creating the menu ---------------------------------------------------------------------------------------
FoodManager shapeManager = new FoodManager();
FoodManagerMenu menu = new FoodManagerMenu(shapeManager);
menu.Select();
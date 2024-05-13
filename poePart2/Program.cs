using System;
using System.Collections.Generic;
using System.Linq;

// Class representing an ingredient
public class Ingredient
{
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public int Calories { get; set; }
    public string FoodGroup { get; set; }
}

// Class representing a step in the recipe
public class Step
{
    public int Number { get; set; }
    public string Description { get; set; }
}

// Class representing a recipe
public class Recipe
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<Step> Steps { get; set; }

    // Delegate for notification when calories exceed 300
    public delegate void RecipeExceedsCaloriesHandler(string recipeName, int totalCalories);
    public event RecipeExceedsCaloriesHandler RecipeExceedsCalories;

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
        Steps = new List<Step>();
    }

    public void EnterRecipe()
    {
        Console.Write("Enter the name of the recipe: ");
        Name = Console.ReadLine();

        Console.Write("Enter the number of ingredients: ");
        int numIngredients = int.Parse(Console.ReadLine());

        for (int i = 0; i < numIngredients; i++)
        {
            Ingredient ingredient = new Ingredient();

            Console.Write($"Enter the name of ingredient {i + 1}: ");
            ingredient.Name = Console.ReadLine();

            Console.Write($"Enter the quantity of {ingredient.Name}: ");
            ingredient.Quantity = double.Parse(Console.ReadLine());

            Console.Write($"Enter the unit of measurement for {ingredient.Name}: ");
            ingredient.Unit = Console.ReadLine();

            Console.Write($"Enter the number of calories for {ingredient.Name}: ");
            ingredient.Calories = int.Parse(Console.ReadLine());

            Console.Write($"Enter the food group for {ingredient.Name}: ");
            ingredient.FoodGroup = Console.ReadLine();

            Ingredients.Add(ingredient);
        }

        Console.Write("Enter the number of steps: ");
        int numSteps = int.Parse(Console.ReadLine());

        for (int i = 0; i < numSteps; i++)
        {
            Step step = new Step();

            Console.Write($"Enter step {i + 1}: ");
            step.Description = Console.ReadLine();
            step.Number = i + 1;

            Steps.Add(step);
        }

        // Calculate total calories and check if it exceeds 300
        int totalCalories = Ingredients.Sum(ing => ing.Calories);
        if (totalCalories > 300)
        {
            RecipeExceedsCalories?.Invoke(Name, totalCalories);
        }
    }

    public void DisplayRecipe()
    {
        Console.WriteLine($"Recipe: {Name}");
        Console.WriteLine("Ingredients:");

        foreach (var ingredient in Ingredients)
        {
            Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
            Console.WriteLine($"- Calories: {ingredient.Calories}");
            Console.WriteLine($"- Food Group: {ingredient.FoodGroup}");
        }

        Console.WriteLine("Steps:");

        foreach (var step in Steps)
        {
            Console.WriteLine($"{step.Number}. {step.Description}");
        }
    }

    public void ScaleRecipe(double factor)
    {
        foreach (var ingredient in Ingredients)
        {
            ingredient.Quantity *= factor;
        }
    }

    public void ResetQuantities()
    {
        // No need to implement for this version
    }

    public void ClearRecipe()
    {
        Ingredients.Clear();
        Steps.Clear();
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Recipe> recipes = new List<Recipe>();

        while (true)
        {
            Console.WriteLine("*******************");

            Console.WriteLine("1. Enter Recipe");
            Console.WriteLine("2. Display Recipes");
            Console.WriteLine("3. Display Recipe");
            Console.WriteLine("4. Scale Recipe");
            Console.WriteLine("5. Reset Quantities");
            Console.WriteLine("6. Clear Recipe");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Recipe recipe = new Recipe();
                    recipe.RecipeExceedsCalories += (name, totalCalories) =>
                    {
                        Console.WriteLine($"Warning: {name} exceeds 300 calories with a total of {totalCalories} calories.");
                    };
                    recipe.EnterRecipe();
                    recipes.Add(recipe);
                    break;
                case 2:
                    DisplayRecipes(recipes);
                    break;
                case 3:
                    DisplayRecipes(recipes);
                    if (recipes.Count > 0)
                    {
                        Console.Write("Enter the index of the recipe to display: ");
                        int index = int.Parse(Console.ReadLine());
                        if (index >= 0 && index < recipes.Count)
                        {
                            recipes[index].DisplayRecipe();
                        }
                        else
                        {
                            Console.WriteLine("Invalid index.");
                        }
                    }
                    break;
                case 4:
                    Console.Write("Enter scaling factor (0.5, 2, or 3): ");
                    double factor = double.Parse(Console.ReadLine());
                    if (recipes.Count > 0)
                    {
                        DisplayRecipes(recipes);
                        Console.Write("Enter the index of the recipe to scale: ");
                        int index = int.Parse(Console.ReadLine());
                        if (index >= 0 && index < recipes.Count)
                        {
                            recipes[index].ScaleRecipe(factor);
                        }
                        else
                        {
                            Console.WriteLine("Invalid index.");
                        }
                    }
                    break;
                case 5:
                    // Reset Quantities not implemented in this version
                    break;
                case 6:
                    if (recipes.Count > 0)
                    {
                        DisplayRecipes(recipes);
                        Console.Write("Enter the index of the recipe to clear: ");
                        int index = int.Parse(Console.ReadLine());
                        if (index >= 0 && index < recipes.Count)
                        {
                            recipes[index].ClearRecipe();
                        }
                        else
                        {
                            Console.WriteLine("Invalid index.");
                        }
                    }
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 7.");
                    break;
            }
        }
    }

    // Method to display all recipes
    static void DisplayRecipes(List<Recipe> recipes)
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
        }
        else
        {
            Console.WriteLine("Recipes:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i}. {recipes[i].Name}");
            }
        }
    }
}

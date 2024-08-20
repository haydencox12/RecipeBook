using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook
{
    public static class RecipeRepository
    {
        private static ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();

        static RecipeRepository()
        {
            InitializePresetRecipes();
        }

        private static void InitializePresetRecipes()
        {
            // Add some preset Breakfast recipes
            recipes.Add(new Recipe { Title = "Classic Pancakes", Ingredients = "Flour, Eggs, Milk, Sugar, Baking Powder", Instructions = "Mix ingredients and cook on a griddle.", Category = "Breakfast", ImagePath = GetImagePath("pancakes.jpg") });
            recipes.Add(new Recipe { Title = "Omelette", Ingredients = "Eggs, Salt, Pepper, Cheese", Instructions = "Beat eggs and cook in a skillet. Add cheese before folding.", Category = "Breakfast", ImagePath = GetImagePath("omelette.jpg") });

            // Add some preset Lunch recipes
            recipes.Add(new Recipe { Title = "Chicken Salad", Ingredients = "Chicken, Lettuce, Tomatoes, Cucumber, Salad Dressing", Instructions = "Combine all ingredients and toss with dressing.", Category = "Lunch", ImagePath = GetImagePath("chickensalad.jpg") });
            recipes.Add(new Recipe { Title = "Grilled Cheese Sandwich", Ingredients = "Bread, Cheese, Butter", Instructions = "Butter bread and grill with cheese in between." , Category = "Lunch", ImagePath = GetImagePath("grilledcheese.jpg") });

            // Add some preset Dinner recipes
            recipes.Add(new Recipe { Title = "Spaghetti Bolognese", Ingredients = "Spaghetti, Ground Beef, Tomato Sauce, Onions, Garlic", Instructions = "Cook spaghetti. Saute beef, onions, garlic, and add tomato sauce.", Category = "Dinner", ImagePath = GetImagePath("spaghetti.jpg") });
            recipes.Add(new Recipe { Title = "Roast Chicken", Ingredients = "Whole chicken, Olive oil, Salt, Pepper, Herbs", Instructions = "Season chicken and roast in oven until cooked.", Category = "Dinner", ImagePath = GetImagePath("chicken.jpg") });
        }

        public static ObservableCollection<Recipe> GetAllRecipes()
        {
            return recipes;
        }

        public static ObservableCollection<Recipe> GetRecipesByCategory(string category)
        {
            return new ObservableCollection<Recipe>(recipes.Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase)));
        }

        public static void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        private static string GetImagePath(string fileName)
        {
            string imagesDir = Path.Combine(Environment.CurrentDirectory, "Images");
            string filePath = Path.Combine(imagesDir, fileName);
            Uri fileUri = new Uri(filePath, UriKind.Absolute);
            return fileUri.ToString();
        }

        public static void RemoveRecipe(Recipe recipe)
        {
            if (recipes.Contains(recipe))
            {
                recipes.Remove(recipe);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadRecipes();
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
            if (addRecipeWindow.ShowDialog() == true) // Check if a recipe was successfully added
            {
                LoadRecipes(); // Reload recipes to include the newly added recipe
            }
        }

        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var recipe = button.DataContext as Recipe;
            if (recipe != null)
            {
                if (MessageBox.Show($"Are you sure you want to delete '{recipe.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    RecipeRepository.RemoveRecipe(recipe);
                    LoadRecipes(); // Refresh the list
                }
            }
        }

        private void LoadRecipes()
        {
            myRecipesListView.ItemsSource = RecipeRepository.GetAllRecipes();
            BreakfastListView.ItemsSource = RecipeRepository.GetRecipesByCategory("Breakfast");
            LunchListView.ItemsSource = RecipeRepository.GetRecipesByCategory("Lunch");
            DinnerListView.ItemsSource = RecipeRepository.GetRecipesByCategory("Dinner");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            var filteredRecipes = RecipeRepository.GetAllRecipes().Where(r => r.Title.ToLower().Contains(searchText) || r.Ingredients.ToLower().Contains(searchText));
            myRecipesListView.ItemsSource = filteredRecipes;
        }
    }
}
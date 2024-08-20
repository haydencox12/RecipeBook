using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;


namespace RecipeBook
{
    public partial class AddRecipeWindow : Window
    {
        private Recipe newRecipe;
        public AddRecipeWindow()
        {
            InitializeComponent();
            newRecipe = new Recipe();
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(titleTextBox.Text) || string.IsNullOrEmpty(ingredientsTextBox.Text) ||
                string.IsNullOrEmpty(instructionsTextBox.Text) || categoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("All fields must be filled, and a category must be selected.");
                return;
            }

            var category = (categoryComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            newRecipe.Title = titleTextBox.Text.Trim();
            newRecipe.Ingredients = ingredientsTextBox.Text.Trim();
            newRecipe.Instructions = instructionsTextBox.Text.Trim();
            newRecipe.Category = category;

            RecipeRepository.AddRecipe(newRecipe);
            MessageBox.Show("Recipe added successfully!");
            this.DialogResult = true;  // Ensure you set DialogResult to true
            this.Close();
        }
        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = CopyImageToAppDirectory(openFileDialog.FileName);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    try
                    {
                        // Convert the local path to a URI the Image control can use
                        Uri imageUri = new Uri(imagePath, UriKind.Absolute);
                        recipeImage.Source = new BitmapImage(imageUri);
                        newRecipe.ImagePath = imageUri.ToString();  // Save the URI as a string
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to load image: {ex.Message}");
                    }
                }
            }
        

    }

        private string CopyImageToAppDirectory(string originalFilePath)
        {
            string imagesDir = System.IO.Path.Combine(Environment.CurrentDirectory, "Images");
            if (!Directory.Exists(imagesDir))
                Directory.CreateDirectory(imagesDir);

            string newFilePath = System.IO.Path.Combine(imagesDir, System.IO.Path.GetFileName(originalFilePath));
            File.Copy(originalFilePath, newFilePath, true);
            return newFilePath;
        }
    }
}

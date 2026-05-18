using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.PL
{
    
    public partial class  Program
    {
        
        
          public void _handleCategoryManagement()
        {
            Console.WriteLine("1.View All Categories \n2.Add Category \n3.Exit");
                 string adminInput = Console.ReadLine()??"";
                 switch (adminInput)
                 {
                     case "1":
                         _ShowAllCategories();
                         break;
                     case "2":
                         _AddCategory();
                         break;
                     case "3":
                         return;
                     default:
                         Console.WriteLine("Enter the Valid option");
                         break;
    
                }
        }
        
        
        
        
        private void _ShowAllCategories()
        {
            Console.WriteLine("\n====================== Categories  ========================\n");
            List<Category>? categories = categoryService.GetAllCategory();
            if(categories == null || categories.Count == 0)
            {
                Console.WriteLine("\n====================== No category found  ========================\n");
                return;
            }
            foreach(var category in categories)
            {
                Console.WriteLine($"Id: {category.CategoryId} | Name: {category.Name}");
            }
        }

        private void _AddCategory()
        {
            Console.WriteLine("\n========= Add New Category =========\n");
            Console.Write("Enter Category Name : ");
            string categoryName = Console.ReadLine()??"";

            if(string.IsNullOrWhiteSpace(categoryName))
            {
                Console.WriteLine("Category name cannot be empty");
                return;
            }

            Category newCategory = new Category { Name = categoryName };

            if(categoryService.AddCategory(newCategory))
            {
                Console.WriteLine($"Category '{categoryName}' added successfully");
            }
            else
            {
                Console.WriteLine("Unable to add new category");
            }
        }

    }

}
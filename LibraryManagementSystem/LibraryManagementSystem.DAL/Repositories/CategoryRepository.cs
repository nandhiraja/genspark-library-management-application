using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;

namespace LibraryManagementSystem.DAL.Repositories
{
    public class CategoryRepository{

        readonly LibraryDbContext _context;
        public CategoryRepository(LibraryDbContext libraryDbContext)
        {
            _context =libraryDbContext;
        }

        public bool AddCategory(Category category)
        {   
            try{
            _context.Categories.Add(category);
            _context.SaveChanges();
            return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to add new Category" ,ex);
                return false;
            }
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
        public Category? GetCategoryByID(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public bool UpdateCategory (Category category)
        {
            Category? oldCategory =  _context.Categories.Find(category.CategoryId);
            if (oldCategory == null)
            {
                return false;
            }
            oldCategory.Name = category.Name;
            _context.SaveChanges();
            return true;

        }

        public bool DeleteCategoryType (Category category)
        {
             _context.Categories.Remove(category);
             _context.SaveChanges();
              
            return true;
        }
    }
}
using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.DAL.DBContext;
using LibraryManagementSystem.DAL.Repositories;

namespace LibraryManagementSystem.BLL.Services
{
    public class CategoryService:ICategoryService
    {
        private CategoryRepository _CategoryRepository;
        private LibraryDbContext _context;
        
        public CategoryService()
        {
            _context = new LibraryDbContext();
            _CategoryRepository =  new CategoryRepository(_context);
        }

        public bool AddCategory(Category category)
        {
            return _CategoryRepository.AddCategory(category);
        }

        public List<Category>? GetAllCategory()
        {
            return _CategoryRepository.GetAllCategories();
        }

        public Category? GetCategoryById(int categoryId)
        {
            return _CategoryRepository.GetCategoryByID(categoryId);
        }

        public List<Category> ListAllCategory()
        {
            return _CategoryRepository.GetAllCategories();
        }

        public bool UpdateCategory(Category category)
        {
            return _CategoryRepository.UpdateCategory(category);
        }

         public bool DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
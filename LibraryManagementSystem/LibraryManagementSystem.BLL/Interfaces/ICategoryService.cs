using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface ICategoryService
    {
        public bool AddCategory(Category category);

        public Category? GetCategoryById(int categoryId);
        public List<Category>? GetAllCategory();

        public bool UpdateCategory(Category category);
        public bool DeleteCategory(Category category);

    }
}
namespace LibraryManagementSystem.Core.Models
{
    
    public class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } =null!;

    public ICollection<BookTitle> BookTitles { get; set; } = new List<BookTitle>();
}
}
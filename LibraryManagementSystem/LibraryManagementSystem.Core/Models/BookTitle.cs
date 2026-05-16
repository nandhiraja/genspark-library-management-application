namespace LibraryManagementSystem.Core.Models
{
    
    public class BookTitle
    {
        public int BookTitleId { get; set; }
        public string Title { get; set; } =null!;
        public string Author { get; set; } =null!;
        public int PublishedYear { get; set; } 
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<BookCopy>? BookCopies { get; set; }

        public BookTitle(){}
    }
}
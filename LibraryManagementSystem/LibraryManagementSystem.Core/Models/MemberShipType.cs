namespace LibraryManagementSystem.Core.Models
{
    public class MembershipType
    {
        public int MembershipTypeId { get; set; }
        public string Name { get; set; } = null!;
        public int MaxBooks { get; set; }
        public int MaxDays { get; set; }

        public ICollection<Member> Members { get; set; } = new List<Member>();
    }

}
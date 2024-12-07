namespace POS_System.Domain.Entities.Interfaces
{
    public interface ILinkable
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}

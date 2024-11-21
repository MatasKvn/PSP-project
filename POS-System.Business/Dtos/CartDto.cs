namespace POS_System.Business.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }

        public int EmployeeVersionId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsCompleted { get; set; }
    }
}

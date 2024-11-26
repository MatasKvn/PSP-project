namespace POS_System.Business.Dtos.Tax
{
    public class TaxResponseDto
    {
        //Primary key
        public int Id { get; set; }

        //Fields
        public int TaxId { get; set; }
        public required string Name { get; set; }
        public required int Rate { get; set; }
        public required bool IsPercentage { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}

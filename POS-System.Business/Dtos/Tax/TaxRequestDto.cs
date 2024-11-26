namespace POS_System.Business.Dtos.Tax
{
    public class TaxRequestDto
    {
        //Fields
        public required string Name { get; set; }
        public required int Rate { get; set; }
        public required bool IsPercentage { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}

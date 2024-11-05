using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("EmployeeServices")]
    public class EmployeeService(int employeeId, int serviceId)
    {
        [Key, Column(Order = 0)]
        public int EmployeeId { get; } = employeeId;
        [Key, Column(Order = 1)]
        public int ServiceId { get; } = serviceId;
    }
}

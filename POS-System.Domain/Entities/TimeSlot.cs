﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("TimeSlots")]
    public class TimeSlot(int id, int employeeId, DateTime startDate, bool isAvailable = true)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        public int EmployeeId { get; } = employeeId;
        [Required]
        public DateTime StartTime { get; } = startDate;
        [Required]
        public bool IsAvailable { get; } = isAvailable;
    }
}

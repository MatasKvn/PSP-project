﻿namespace POS_System.Business.Dtos.Request
{
    public record ServiceRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Duration { get; set; }
        public required int Price { get; set; }
        public required string ImageURL { get; set; }
        public required int EmployeeId { get; set; }
    }
}
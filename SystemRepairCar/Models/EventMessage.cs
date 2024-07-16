using System;

namespace SystemRepairCar.Models
{
    public class EventMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

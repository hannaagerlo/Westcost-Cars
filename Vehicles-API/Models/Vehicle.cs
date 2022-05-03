namespace Vehicles_API.Models
{
    public class Vehicle
    {
        public int Id { get; set; } 
        public string? RegNo { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int ModelYear { get; set; }
        public int Mileage { get; set; }

    }
}
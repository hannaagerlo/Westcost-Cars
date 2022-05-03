using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles_API.ViewModels
{
    public class PostVehicleViewModel
    {
        public string? RegNo { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int ModelYear { get; set; }
        public int Mileage { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
       private readonly VehicleContext _context;
        public VehiclesController(VehicleContext context)
        {
            _context = context;
        }

        // En metod som hämtar alla fordon ...
        // api/v1/vehicles
        [HttpGet]
        public async Task <ActionResult<List<VehicleViewModel>>> ListVehicles(){

            var response = await _context.Vehicles.ToListAsync();
            var vehicleList = new List<VehicleViewModel>();

            foreach(var vehicle in response)
            {
                vehicleList.Add(
                    new VehicleViewModel{
                        VehicleId = vehicle.Id,
                        RegNo = vehicle.RegNo,
                        VehicleName = string.Concat(vehicle.Make, " ", vehicle.Model),
                        ModelYear = vehicle.ModelYear,
                        Mileage = vehicle.Mileage
                    }   
                );
            }
            return Ok(vehicleList);
           
        }

        // api/v1/vehicles/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehiclesById(int id){

            var response = await _context.Vehicles.FindAsync(id);
            if(response == null) 
            return NotFound($"Vi kunde inte hitta någon bil med det id: {id}");
            return Ok(response);
        }
        [HttpGet("byregno/{regNo}")]
          public async Task<ActionResult<Vehicle>> GetVehiclesByRegNo(string regNo){

           var response = await _context.Vehicles.SingleOrDefaultAsync(c => c.RegNo!.ToLower() == regNo.ToLower());
           if(response == null) 
            return NotFound($"Vi kunde inte hitta någon bil med registreringsnumret: {regNo}");
           return Ok(response);
        }

        //Lägger till ett fordon i systemet 
        [HttpPost]
        public async Task<ActionResult<Vehicle>> AddVehicles(PostVehicleViewModel vehicle)
        {
            // Här kontaktar vi databasen och spara ner nya fordonet ...
            // Returnera  rätt statuskod 
            var vehicleToAdd = new Vehicle{
                RegNo = vehicle.RegNo,
                Make = vehicle.Make,
                Model = vehicle.Model,
                ModelYear = vehicle.ModelYear,
                Mileage = vehicle.Mileage

            };
            await _context.Vehicles.AddAsync(vehicleToAdd);
            await _context.SaveChangesAsync(); 
            return StatusCode(201, vehicle);
        }

        // Uppdaterar ett befintligt fordon
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, Vehicle model)
        {
            var response = await _context.Vehicles.FindAsync(id);
            if(response == null) 
            return NotFound($"Vi kunde inte hitta någon bil med det id: {id} som skulle tas bort");

            response.RegNo = model.RegNo;
            response.Make = model.Make;
            response.Model = model.Model;
            response.ModelYear = model.ModelYear;
            response.Mileage = model.Mileage;

            _context.Vehicles.Update(response);
            await _context.SaveChangesAsync();
            return NoContent(); // status kod 204
        }

        // tar bort fordon från systemet
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicles(int id)
        {
            var response = await _context.Vehicles.FindAsync(id); 

            if(response == null) 
            return NotFound($"Vi kunde inte hitta någon bil med det id: {id} som skulle tas bort");

            _context.Vehicles.Remove(response);
            await _context.SaveChangesAsync();
            return NoContent(); // statu kod 204

        }
    }
}
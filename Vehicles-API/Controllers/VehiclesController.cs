using Microsoft.AspNetCore.Mvc;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
        // En metod som hämtar alla fordon ...
        // api/v1/vehicles
        [HttpGet]
        public ActionResult ListVehicles(){
            return StatusCode(200, "{'message':'Det funkar'}");
        }

        // api/v1/vehicles/id
        [HttpGet("{id}")]
        public ActionResult GetVehiclesById(int id){
            return StatusCode(200, "{'message':'Det funkar också'}");
        }
    }
}
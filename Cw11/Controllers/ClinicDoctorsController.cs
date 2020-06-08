using Microsoft.AspNetCore.Mvc;
using Cw11.ClinicServices;
using Cw11.ClinicRequest;


namespace Cw11.Controllers
{
    [Route("api/[clinicdoctors]")]
    [ApiController]
    public class ClinicDoctorsController : ControllerBase
    {

        private readonly IDbClinicService _service;

        public ClinicDoctorsController(IDbClinicService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_service.GetDoctors());
        }


        [HttpPost]
        public IActionResult AddDoctor(AddClinicDoctorRequest req)
        {
            return Ok(_service.AddDoctor(req));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDoctor(int id)
        {
            return Ok(_service.DeleteDoctor(id));
        }

        [HttpPut]
        public IActionResult UpdateDoctor(UpdateClinicDoctorRequest req)
        {
            return Ok(_service.UpdateDoctor(req));
        }


    }
}
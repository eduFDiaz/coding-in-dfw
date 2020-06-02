using AutoMapper;
using coding.API.Data;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using System.Threading.Tasks;
using coding.API.Models.Messages;
using coding.API.Dtos;
using coding.API.Models.Services;
using coding.API.Dtos.Services;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Service> _serviceDal;

        public ServiceController(
            Repository<Service> serviceDal, IConfiguration config, IMapper mapper)
        {
            _serviceDal = serviceDal;
            _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto request)
        {
            var serviceToCreate = _mapper.Map<Service>(request);

            var createdService = await _serviceDal.Add(serviceToCreate);

            return Ok(new ServicePresenter(createdService));

        }


        [HttpDelete("{serviceid}/delete", Name = "Delete Servce")]
        public async Task<IActionResult> Delete(Guid serviceid)
        {
            var serviceToDelete = (await _serviceDal.GetById(serviceid));

            if (serviceToDelete == null)
                return NotFound();

            if (await _serviceDal.Delete(serviceToDelete))
                return NoContent();

            return BadRequest("Cant delete the service!");

        }

        [HttpPut("{serviceid}/update", Name = "Update service")]
        public async Task<IActionResult> Update(Guid serviceid, [FromBody] UpdateServiceDto update)
        {
            var serviceToUpd = (await _serviceDal.GetById(serviceid));

            serviceToUpd.Body = update.Body;

            if (serviceToUpd == null)
                return NotFound();

            if (await _serviceDal.Update(serviceToUpd))
                return NoContent();

            return BadRequest("cant update the service!");

        }

        [HttpGet("foruser/{userid}", Name = "Return all services")]
        public async Task<ActionResult> GetAllMessages(Guid userid)
        {
            var services = (await _serviceDal.ListAsync()).Where(s => s.UserId == userid).ToList();

            return Ok(services);
        }


    }
}
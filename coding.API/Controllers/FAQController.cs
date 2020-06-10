using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.FAQS;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<FAQ> _FAQDal;

        public FAQController(
            Repository<FAQ> FAQDal, IConfiguration config, IMapper mapper)
        {

            _FAQDal = FAQDal;
            _config = config;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateFAQDto request)
        {
            var FAQToCreate = _mapper.Map<FAQ>(request);

            var createdFAQ = await _FAQDal.Add(FAQToCreate);

            return Ok(new FAQPresenter(createdFAQ));

        }

        [HttpGet("all")]
        public async Task<ActionResult> GetFAQs()
        {
            var fAQs = (await _FAQDal.ListAsync());

            var faqsForReturn = _mapper.Map<List<FAQForDetailDto>>(fAQs);

            return Ok(faqsForReturn);
        }


        [Authorize]
        [HttpDelete("{FAQid}/delete", Name = "DeleteFAQ")]
        public async Task<IActionResult> DeleteLan(Guid FAQid)
        {
            var FAQToDelete = (await _FAQDal.GetById(FAQid));

            if (FAQToDelete == null)
                return NotFound();

            if (await _FAQDal.Delete(FAQToDelete))
                return NoContent();

            return BadRequest("Catn erase the FAQ");


        }

        [Authorize]
        [HttpPut("{FAQid}/update", Name = "Update FAQ")]
        public async Task<IActionResult> UpdateLan(Guid FAQid, [FromBody] UpdateFAQDto request)
        {
            var FAQToUpdate = (await _FAQDal.GetById(FAQid));

            if (FAQToUpdate == null)
                return NotFound();

            var toUpd = _mapper.Map(request, FAQToUpdate);


            if (await _FAQDal.Update(toUpd))
                return NoContent();

            return BadRequest("cant update the FAQ!");

        }

    }
}
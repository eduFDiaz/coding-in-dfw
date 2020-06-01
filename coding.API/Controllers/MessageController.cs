using AutoMapper;
using coding.API.Data;
using coding.API.Dtos.Comments;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;



using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Models.Messages;
using coding.API.Dtos;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Message> _messageDal;

        public MessageController(
            Repository<Message> messageDal, IConfiguration config, IMapper mapper)
        {

            _messageDal = messageDal;
            _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateMessageDto request)
        {
            var MessageToCreate = _mapper.Map<Message>(request);

            MessageToCreate.isRead = false;

            var createdMessage = await _messageDal.Add(MessageToCreate);


            return Ok(new MessagePresenter(createdMessage));

        }


        [HttpDelete("{messageid}/delete", Name = "Delete Message")]
        public async Task<IActionResult> DeleteMessage(Guid messageid)
        {
            var messageToDelete = (await _messageDal.GetById(messageid));

            if (messageToDelete == null)
                return NotFound();

            if (await _messageDal.Delete(messageToDelete))
                return NoContent();

            return BadRequest("Cant delete the message!");

        }

        [HttpPut("{messageid}/update", Name = "Update message")]
        public async Task<IActionResult> UpdateMessage(Guid messageid, [FromBody] UpdateMessageDto request)
        {
            var messageToUpd = (await _messageDal.GetById(messageid));

            if (messageToUpd == null)
                return NotFound();

            var toUpd = _mapper.Map(request, messageToUpd);



            if (await _messageDal.Update(toUpd))
                return NoContent();

            return BadRequest("cant update the message!");

        }

        [HttpGet("all", Name = "Return all messages")]
        public async Task<ActionResult> GetAllMessages()
        {
            var messages = (await _messageDal.ListAsync()).ToList();

            return Ok(messages);
        }


    }
}
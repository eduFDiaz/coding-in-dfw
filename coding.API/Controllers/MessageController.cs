using AutoMapper;
using coding.API.Data;
using coding.API.Dtos.Comments;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.Posts.Comments;


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

            var createdMessage = await _messageDal.Add(MessageToCreate);

            return Ok(new MessagePresenter(createdMessage));

        }



        [HttpGet("all")]
        public async Task<ActionResult> GetAllMessages()
        {
            var messages = (await _messageDal.ListAsync()).ToList();

            return Ok(messages);
        }


    }
    }
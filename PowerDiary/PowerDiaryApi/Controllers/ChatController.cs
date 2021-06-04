using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PowerDiaryApi.ViewModels;
using PowerDiaryBusiness;

namespace PowerDiaryApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IChatBusiness _chatBusiness;
        private readonly IMapper _mapper;

        public ChatController(IChatBusiness chatBusiness, IMapper mapper)
        {
            _chatBusiness = chatBusiness;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/{date}")]
        public IActionResult GetDetailedChatData(DateTime date)
        {
            var result = _chatBusiness.GetChatDetailedView(date);
            if (result.Status == ServiceResponseDtoStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, result.FormattedMessage);
            var chatViewModel = _mapper.Map<List<ChatViewModel>>(result.Data);
            return Ok(chatViewModel);
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/{date}")]
        public IActionResult GetHourlyChatData(DateTime date)
        {
            var result = _chatBusiness.GetChatsHourView(date);
            if (result.Status == ServiceResponseDtoStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, result.FormattedMessage);
            var hourlyChatViewModel = _mapper.Map<List<HourlyChatViewModel>>(result.Data);
            return Ok(hourlyChatViewModel);
        }
    }
}

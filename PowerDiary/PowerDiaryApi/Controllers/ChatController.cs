using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PowerDiaryApi.ViewModels;
using PowerDiaryBusiness;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PowerDiaryApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IPowerDiaryBusiness _powerDiaryBusiness;
        private readonly IMapper _mapper;

        public ChatController(IPowerDiaryBusiness powerDiaryBusiness, IMapper mapper)
        {
            _powerDiaryBusiness = powerDiaryBusiness;
            _mapper = mapper;
        }

        // GET: api/<ChatController>
        /// <summary>
        /// Get minute by minute chat history 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/[controller]/[action]/{date}")]
        public IActionResult GetDetailedChatData(DateTime date)
        {
            var result = _powerDiaryBusiness.GetChatDetailedView(date);
            if (result.Status == ServiceResponseDtoStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, result.FormattedMessage);
            var chatViewModel = _mapper.Map<List<ChatViewModel>>(result.Data);
            return Ok(chatViewModel);
        }

        // GET: api/<ChatController>
        /// <summary>
        /// Get hourly aggregated chat history 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/[controller]/[action]/{date}")]
        public IActionResult GetHourlyChatData(DateTime date)
        {
            var result = _powerDiaryBusiness.GetChatsHourView(date);
            if (result.Status == ServiceResponseDtoStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, result.FormattedMessage);
            var hourlyChatViewModel = _mapper.Map<List<HourlyChatViewModel>>(result.Data);
            return Ok(hourlyChatViewModel);
        }
    }
}

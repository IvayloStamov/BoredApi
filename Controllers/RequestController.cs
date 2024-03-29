﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BoredApi.Dtos;
using BoredApi.Services;
using BoredApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<RequestDto>>> GetUserRequests(int userId)
        {
            return await _requestService.GetAllActiveRequestForUserAsync(userId);
        }

        [HttpPut("{userId}, {groupId}")]
        public async Task<ActionResult<RequestDto>> ChangeRequestStatus(int userId, int groupId, ChangeRequestStatusDto request)
        {
            return await _requestService.ChangeRequestStatusAsync(userId, groupId, request);
        }

        [HttpGet("{userId}, {groupId}")]
        public async Task<ActionResult<RequestDto>> GetCertainRequest(int userId, int groupId)
        {
            return await _requestService.GetRequestForUserAsync(userId, groupId);
        }
    }
}

using BoredApi.Data.Models;
using BoredApi.Services;
using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("{adminId}")]
        public async Task<ActionResult<List<GroupDto>>> CreateGroup(int adminId, GroupDto dto)
        {
            return await _groupService.CreateGroupAsync(adminId, dto);
        }


    }
}

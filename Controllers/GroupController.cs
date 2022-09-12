using System.Collections.Generic;
using System.Threading.Tasks;
using BoredApi.Dtos;
using BoredApi.Services;
using BoredApi.Services.Interfaces;
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
        public async Task<ActionResult<List<ReturnGroupDto>>> CreateGroup(int adminId, GroupDto dto)
        {
            return await _groupService.CreateGroupAsync(adminId, dto);
        }

        [HttpPut("{memberId}")]
        public async Task<ActionResult<ReturnGroupDto>> AddNewUser(int groupId, int newUserId, int memberId)
        {
            return await _groupService.AddUserToGroupAsync(groupId, newUserId, memberId);
        }

        [HttpGet]
        public async Task<ActionResult<List<ReturnGroupDto>>> GetAllGroups()
        {
            return await _groupService.ReturnAllGroupsAsync();
        }
        [HttpGet("{groupId}")]
        public async Task<ActionResult<List<UserDto>>> GetAllUserUsersInAGroup(int groupId)
        {
            return await _groupService.ReturnAllUsersFromGroupAsync(groupId);
        }

        [HttpDelete("{ownerId}")]
        public async Task<ActionResult<ReturnGroupDto>> RemoveUser(int groupId, int userId, int ownerId)
        {
            return await _groupService.DeleteUserFromGroupAsync(groupId, userId, ownerId);
        }
    }
}

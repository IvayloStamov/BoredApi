using System.Collections.Generic;
using System.Threading.Tasks;
using BoredApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<ActionResult<List<ReturnGroupDto>>> CreateGroupAsync(int adminId, GroupDto dto);
        public Task<ActionResult<ReturnGroupDto>> AddUserToGroupAsync(int groupId, int newUserId, int ownerId);
        public Task<ActionResult<List<ReturnGroupDto>>> ReturnAllGroupsAsync();
        public Task<ActionResult<ReturnGroupDto>> DeleteUserFromGroupAsync(int groupId, int userId, int ownerId);
        public Task<ActionResult<List<UserDto>>> ReturnAllUsersFromGroupAsync(int groupId);


    }
}

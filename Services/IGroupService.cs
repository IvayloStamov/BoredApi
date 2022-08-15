using BoredApi.Data.Models;
using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services
{
    public interface IGroupService
    {
        public Task<ActionResult<List<ReturnGroupDto>>> CreateGroupAsync(int adminId, GroupDto dto);
        public Task<ActionResult<ReturnGroupDto>> AddUserToGroupAsync(int groupId, int newUserId, int ownerId);
        public Task<ActionResult<List<ReturnGroupDto>>> ReturnAllGroupsAsync();
        public Task<ActionResult<ReturnGroupDto>> DeleteUserFromGroupAsync(int groupId, int userId, int ownerId);

    }
}

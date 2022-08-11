using BoredApi.Data.Models;
using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services
{
    public interface IGroupService
    {
        public Task<ActionResult<List<GroupDto>>> CreateGroupAsync(int adminId, GroupDto dto);
    }
}

using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services
{
    public interface IRequestService
    {
        public Task<ActionResult<List<RequestDto>>> GetAllRequestForUserAsync(int userId);
        public Task<ActionResult<RequestDto>> GetRequestForUserAsync(int userId, int groupId);
        public Task<ActionResult<RequestDto>> ChangeRequestStatusAsync(int userId, int groupId, ChangeRequestStatusDto request);
    }
}

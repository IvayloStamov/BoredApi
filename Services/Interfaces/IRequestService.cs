using System.Collections.Generic;
using System.Threading.Tasks;
using BoredApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services.Interfaces
{
    public interface IRequestService
    {
        public Task<ActionResult<List<RequestDto>>> GetAllActiveRequestForUserAsync(int userId);
        public Task<ActionResult<RequestDto>> GetRequestForUserAsync(int userId, int groupId);
        public Task<ActionResult<RequestDto>> ChangeRequestStatusAsync(int userId, int groupId, ChangeRequestStatusDto request);
    }
}

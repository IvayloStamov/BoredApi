using BoredApi.Data;
using BoredApi.Data.DataModels.Enums;
using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoredApi.Services
{
    public class RequestService : IRequestService
    {
        private readonly BoredApiContext _boredApiContext;

        public RequestService(BoredApiContext boredApiContext)
        {
            _boredApiContext = boredApiContext;
        }

        public async Task<ActionResult<RequestDto>> ChangeRequestStatusAsync(int userId, int groupId, ChangeRequestStatusDto request)
        {
            var user = await _boredApiContext.Users
                .Include(x => x.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception($"A user with the id ({userId}) does not exist.");
            }

            if (!user.UserGroups.Any(x => x.GroupId == groupId))
            {
                throw new Exception("The user is not poart of the group");
            }

            var joinRequests = await _boredApiContext.JoinActivityRequests
                .Include(x => x.User)
                .ThenInclude(y => y.UserGroups)
                .Include(x => x.GroupActivity)
                .Where(x => x.UserId == userId)
                .Where(x => x.GroupActivity.GroupId == groupId)
                .Where(x => x.HasAccepted == Status.Pending)
                .FirstOrDefaultAsync();

            if (joinRequests == null)
            {
                throw new Exception("There are no requests currently.");
            }

            joinRequests.HasAccepted = request.Status;

            await _boredApiContext.SaveChangesAsync();

            RequestDto requestDto = new RequestDto()
            {
                Name = joinRequests.Name,
                HasAccepted = joinRequests.HasAccepted,
            };

            return requestDto;
        }

        public async Task<ActionResult<List<RequestDto>>> GetAllRequestForUserAsync(int userId)
        {

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception($"A user with the id ({userId}) does not exist.");
            }

            var requests = await _boredApiContext.JoinActivityRequests
                .Where(x => x.UserId == userId)
                .Select(x => new RequestDto()
                {
                    Name = x.Name,
                    HasAccepted = x.HasAccepted
                })
                .ToListAsync();

            if (requests.Count == 0)
            {
                throw new Exception("There are not currently requests.");
            }

            return requests;
        }

        public async Task<ActionResult<RequestDto>> GetRequestForUserAsync(int userId, int groupId)
        {
            var user = await _boredApiContext.Users
                .Include(x => x.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception($"A user with the id ({userId}) does not exist.");
            }

            if (!user.UserGroups.Any(x => x.GroupId == groupId))
            {
                throw new Exception("The user is not poart of the group");
            }

            var joinRequests = await _boredApiContext.JoinActivityRequests
                .Include(x => x.User)
                .ThenInclude(y => y.UserGroups)
                .Include(x => x.GroupActivity)
                .Where(x => x.UserId == userId)
                .Where(x => x.GroupActivity.GroupId == groupId)
                .Where(x => x.HasAccepted == Status.Pending)
                .FirstOrDefaultAsync();

            if (joinRequests == null)
            {
                throw new Exception("There are not currently requests.");
            }

            RequestDto requestDto = new RequestDto()
            {
                Name = joinRequests.Name,
                HasAccepted = joinRequests.HasAccepted,
            };

            return requestDto;
        }
    }
}

using BoredApi.Data;
using BoredApi.Data.DataModels.Enums;
using BoredApi.Data.Models.Exceptions;
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
                throw new SuchAUserDoesNotExistException(userId);
            }

            if (!user.UserGroups.Any(x => x.GroupId == groupId))
            {
                throw new UserIsNotPartOfTheGroupException(userId);
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
                throw new NoActiveRequestsException();
            }

            joinRequests.HasAccepted = request.Status;

            await _boredApiContext.SaveChangesAsync();

            var groupActivity = await _boredApiContext.GroupActivities
                .Include(x => x.JoinActivityRequests)
                .Where(x => x.GroupId == groupId)
                .Where(x => x.JoinActivityRequests.Any(z => z.UserId == userId))
                .Where(x => x.Status == Status.Pending)
                .FirstOrDefaultAsync();

            if (joinRequests.HasAccepted == Status.Declined)
            {
                groupActivity.Status = Status.Declined;
                await _boredApiContext.SaveChangesAsync();
            }

            bool isAcceptedByAll = groupActivity.JoinActivityRequests.All(x => x.HasAccepted == Status.Accepted);

            if (isAcceptedByAll)
            {
                groupActivity.Status = Status.Accepted;
                await _boredApiContext.SaveChangesAsync();
            }

            RequestDto requestDto = new RequestDto()
            {
                Name = joinRequests.Name,
                HasAccepted = joinRequests.HasAccepted,
            };

            return requestDto;
        }
        public async Task<ActionResult<List<RequestDto>>> GetAllActiveRequestForUserAsync(int userId)
        {

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new SuchAUserDoesNotExistException(userId);
            }

            var requests = await _boredApiContext.JoinActivityRequests
                .Where(x => x.UserId == userId && x.HasAccepted == Status.Pending)
                .Select(x => new RequestDto()
                {
                    Name = x.Name,
                    HasAccepted = x.HasAccepted
                })
                .ToListAsync();

            if (requests.Count == 0)
            {
                throw new NoActiveRequestsException();
            }

            return requests;
        }
        public async Task<ActionResult<RequestDto>> GetRequestForUserAsync(int userId, int groupId)
        {
            var user = await _boredApiContext.Users
                .Include(x => x.UserGroups)
                .Include(x => x.JoinActivityRequests)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new SuchAUserDoesNotExistException(userId);
            }

            var group = await _boredApiContext.Groups
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (group == null)
            {
                throw new SuchAGroupDoesNotExistException(groupId);
            }

            if (!user.UserGroups.Any(x => x.GroupId == groupId))
            {
                throw new UserIsNotPartOfTheGroupException(userId);
            }

            var joinRequests = await _boredApiContext.JoinActivityRequests
                .Include(x => x.User)
                .ThenInclude(y => y.UserGroups)
                .Include(x => x.GroupActivity)
                .FirstOrDefaultAsync(x => x.UserId == userId
                && x.GroupActivity.GroupId == groupId
                && x.HasAccepted == Status.Pending);

            if (joinRequests == null)
            {
                throw new NoActiveRequestsException();
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

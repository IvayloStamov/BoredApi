using BoredApi.Data;
using BoredApi.Data.Models;
using BoredApi.Data.Models.Exceptions;
using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoredApi.Services
{
    public class GroupService : IGroupService
    {
        private readonly BoredApiContext _boredApiContext;

        public GroupService(BoredApiContext boredApiContext)
        {
            _boredApiContext = boredApiContext;
        }
        public async Task<ActionResult<List<ReturnGroupDto>>> CreateGroupAsync(int ownerId, GroupDto dto)
        {
            var groupDto = await _boredApiContext.Groups
                .FirstOrDefaultAsync(x => x.Name.Equals(dto.Name));

            if (groupDto != null)
            {
                throw new GroupAlreadyExistsException(dto.Name);
            }

            var owner = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == ownerId);
            if (owner == null)
            {
                throw new SuchAUserDoesNotExistException(ownerId);
            }

            // TODO: Optimise Database call, remove break and continue statements, foreach
            for (int i = 0; i < dto.Users.Count; i++)
            {
                var currentUser = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == dto.Users[i]);
                if (currentUser == null)
                {
                    continue;
                }
                if (dto.Users.Count <= 1)
                {
                    break;
                }
                int current = dto.Users[i];
                if (i == dto.Users.Count - 1)
                {
                    break;
                }
                int next = dto.Users[i + 1];

                if (dto.Users[i] == dto.Users[i + 1] || ownerId == dto.Users[i + 1])
                {
                    dto.Users.RemoveAt(i + 1);
                    i = -1;
                }
            }
            Group group = new Group()
            {
                Name = dto.Name,
                OwnerId = ownerId,
                CreateDate = DateTime.Now,
            };

            UserGroup ownerUserGroup = new UserGroup()
            {
                UserId = ownerId,
                GroupId = group.Id,
                UserEntryDate = DateTime.Now,
                isAdmin = true,
                isOwner = true
            };

            group.UserGroups = dto.Users.Select(x => new UserGroup()
            {
                UserId = x,
                GroupId = group.Id,
                UserEntryDate = DateTime.Now,
                isAdmin = false,
                isOwner = false
            }).ToList();

            group.UserGroups.Add(ownerUserGroup);

            if (!group.UserGroups.Any(x => x.UserId == ownerId))
            {
                group.UserGroups.Add(new UserGroup()
                {
                    UserId = ownerId,
                    GroupId = group.Id,
                    UserEntryDate = DateTime.Now
                });
            }


            await _boredApiContext.AddRangeAsync(group.UserGroups);

            await _boredApiContext.AddAsync(group);
            await _boredApiContext.SaveChangesAsync();

            var outputResult = await _boredApiContext.Groups
                .Select(x => new ReturnGroupDto()
                {
                    Name = x.Name,
                    OwnerId = x.OwnerId,
                    Users = x.UserGroups
                    .Select(x => x.UserId)
                    .ToList()
                }).ToListAsync();

            return outputResult;
        }
        public async Task<ActionResult<ReturnGroupDto>> AddUserToGroupAsync(int groupId, int newUserId, int ownerId)
        {

            var group = await _boredApiContext.Groups
                .Include(ug => ug.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (group == null)
            {
                throw new SuchAGroupDoesNotExistException(groupId);
            }

            if (ownerId != group.OwnerId)
            {
                throw new UserIsNotTheOwnerOfTheGroupException(ownerId);
            }

            var newUser = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == newUserId);
            if (newUser == null)
            {
                throw new SuchAUserDoesNotExistException(newUserId);
            }

            foreach (UserGroup ug in group.UserGroups)
            {
                if (ug.UserId == newUserId)
                {
                    throw new UserAlreadyPartOfTheGroupException(newUserId);
                }
            }

            UserGroup userGroup = new UserGroup()
            {
                UserId = newUserId,
                GroupId = groupId,
            };

            group.UserGroups.Add(userGroup);

            await _boredApiContext.SaveChangesAsync();

            Group output = await _boredApiContext.Groups
                .FirstAsync(x => x.Id == groupId);
            ReturnGroupDto groupDto = new ReturnGroupDto()
            {
                Name = output.Name,
                OwnerId = output.OwnerId,
                Users = output.UserGroups.Select(x => x.UserId).ToList()
            };

            return groupDto;
        }
        public async Task<ActionResult<List<ReturnGroupDto>>> ReturnAllGroupsAsync()
        {
            var dtos = await _boredApiContext.Groups
                .Select(x => new ReturnGroupDto()
                {
                    Name = x.Name,
                    OwnerId = x.OwnerId,
                    Users = x.UserGroups
                    .Select(y => y.UserId)
                    .ToList()
                })
                .ToListAsync();

            return dtos;
        }
        public async Task<ActionResult<ReturnGroupDto>> DeleteUserFromGroupAsync(int groupId, int userId, int ownerId)
        {
            var group = await _boredApiContext.Groups
                .Include(ug => ug.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (group == null)
            {
                throw new SuchAGroupDoesNotExistException(groupId);
            }

            if (ownerId != group.OwnerId)
            {
                throw new UserIsNotTheOwnerOfTheGroupException(ownerId);
            }

            if (ownerId == userId)
            {
                throw new OwnerCannotRemoveThemselvesFromTheGroupException();
            }

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new SuchAUserDoesNotExistException(userId);
            }

            bool isInTheGroup = false;

            foreach (UserGroup ug in group.UserGroups)
            {
                if (ug.UserId == userId)
                {
                    isInTheGroup = true;
                }
            }

            if (!isInTheGroup)
            {
                throw new UserIsNotPartOfTheGroupException(userId);
            }

            var userGroupToRemoved = await _boredApiContext.UserGroups.FirstAsync(x => x.GroupId == groupId && x.UserId == userId);

            group.UserGroups.Remove(userGroupToRemoved);
            user.UserGroups.Remove(userGroupToRemoved);

            _boredApiContext.UserGroups.Remove(userGroupToRemoved);
            await _boredApiContext.SaveChangesAsync();

            Group output = await _boredApiContext.Groups
               .FirstAsync(x => x.Id == groupId);
            ReturnGroupDto groupDto = new ReturnGroupDto()
            {
                Name = output.Name,
                OwnerId = output.OwnerId,
                Users = output.UserGroups.Select(x => x.UserId).ToList()
            };

            return groupDto;
        }
    }
}

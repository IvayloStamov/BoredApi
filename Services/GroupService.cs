using BoredApi.Data;
using BoredApi.Data.Models;
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
        public async Task<ActionResult<List<GroupDto>>> CreateGroupAsync(int adminId, GroupDto dto)
        {
            var groupDto = await _boredApiContext.Groups
                .FirstOrDefaultAsync(x => x.Name.Equals(dto.Name));

            User user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == adminId);
            if (user == null)
            {
                throw new Exception($"A user with the id ({adminId}) does not exist.");
            }

            if (groupDto != null)
            {
                throw new Exception($"A group with the name ({dto.Name}) already exists.");
            }

            for (int i = 0; i < dto.Users.Count; i++)
            {
                var currentUser = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == dto.Users[i]);
                if(currentUser == null)
                {
                    continue;
                }
                if(dto.Users.Count <= 1)
                {
                    break;
                }
                int current = dto.Users[i];
                int next = dto.Users[i + 1];

                if (dto.Users[i] == dto.Users[i + 1])
                {
                    dto.Users.RemoveAt(i + 1);
                    i = -1;
                }
            }
            Group group = new Group()
            {
                Name = dto.Name,
                CreateDate = DateTime.Now,
            };

            group.UserGroups = dto.Users.Select(x => new UserGroup()
            {
                UserId = x,
                GroupId = group.Id,
                UserEntryDate = DateTime.Now
            }).ToList();

            if(!group.UserGroups.Any(x => x.UserId == adminId))
            {
                group.UserGroups.Add(new UserGroup()
                {
                    UserId = adminId,
                    GroupId = group.Id,
                    UserEntryDate = DateTime.Now
                });
            }
           

            await _boredApiContext.AddRangeAsync(group.UserGroups);

            await _boredApiContext.AddAsync(group);
            await _boredApiContext.SaveChangesAsync();

            var outputResult = await _boredApiContext.Groups
                .Select(x => new GroupDto()
                {
                    Name = x.Name,
                    Users = x.UserGroups
                    .Select(x => x.UserId)
                    .ToList()
                }).ToListAsync();

            return outputResult;
        }


    }
}

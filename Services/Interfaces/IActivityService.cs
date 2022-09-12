using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services.Interfaces
{
    public interface IActivityService
    {
        public Task<ActionResult<string>> GetRandomActivityInGroupAsync(int userId, int groupId);
        public Task<ActionResult<string>> EndAnActivityAsync(int userId, int groupId);
    }
}

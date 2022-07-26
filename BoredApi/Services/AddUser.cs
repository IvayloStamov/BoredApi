using BoredApi.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BoredApi.Services
{
    public class AddUser
    {
        DataContext data = new DataContext();

       // public async Task<ActionResult<List<User>>> AddUser(UserDto user)
       // {
       //     string username = user.Username;
       //
       //     var userCheck = data.Users.FirstOrDefault(x => x.Username == username);
       //     if (userCheck != null)
       //     {
       //         throw new Exception($"A user with the userhame ({username}) already exists.");                       
       //     }
       //
       //     User newUser = new User
       //     {
       //         Username = user.Username,
       //         FirstName = user.FirstName,
       //         LastName = user.LastName
       //     };


        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using REST_API.Model;
using REST_API.Repository;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/User/[action]")]
    public class UserController
    {
        public UserRepository UserRepo = new UserRepository();
        public static List<User> userCollection = new List<User>();

        [HttpGet]
        [ActionName("GetUsersList")]
        public string Get() 
        {
            userCollection = UserRepo.Get();
            var json = JsonSerializer.Serialize(userCollection);
            return json;
        }

        [HttpPost]
        [ActionName("AddUser")]
        public void Add([FromBody] User[] users)
        {
            foreach (var user in users)
            {
                userCollection.Add(user);
                UserRepo.Add(user);
            }
        }

        [HttpPut]
        [ActionName("ModifyUser")]
        public void Update([FromBody]User user, int id)
        {
            UserRepo.Update(user, id);
        }

        [HttpDelete]
        [ActionName("RemoveUser")]
        public void Delete(int id)
        {
            var element = userCollection.Find(user => user.UserID == id);
            UserRepo.Delete(id);
        }

        [HttpGet]
        [ActionName("UserExist")]
        public string UserExist(string email, string password)
        {
            bool userfound = UserRepository.UserExist(email, password);
            if (userfound)
            {
                return "User Already Exist";
            }

            return "User Doesn't Exist";

        }
    }
}

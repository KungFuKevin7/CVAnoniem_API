using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using REST_API.Model;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/[action]")]
    public class MyController : ControllerBase, IRepository<User>
    {

        public static List<User> userCollection = new List<User>();

        [HttpGet]
        [ActionName("GetUsersList")]
        public string Get() 
        {
            userCollection = QueryRepository.GetUsers();
            var json = JsonSerializer.Serialize(userCollection);
            return json;
        }

        [HttpPost]
        [ActionName("AddUser")]
        public void Add([FromBody] User[] users)
        {
            foreach (var user in users)
            {
                Console.WriteLine(user.EmailAddress);
                userCollection.Add(user);
                QueryRepository.AddUser(user);
            }
        }

        [HttpPut]
        [ActionName("ModifyUser")]
        public void Update([FromBody]User user, int id)
        {
            QueryRepository.UpdateUser(user, id);
        }

        [HttpDelete]
        [ActionName("RemoveUser")]
        public void Delete(int id)
        {
            var element = userCollection.Find(user => user.UserID == id);
            QueryRepository.RemoveUser(id);
        }

        [HttpGet]
        [ActionName("UserExist")]
        public string UserExist(string email, string password)
        {
            bool userfound = QueryRepository.UserExist(email, password);
            if (userfound)
            {
                return "User Already Exist";
            }

            return "User Doesn't Exist";

        }
    }
}

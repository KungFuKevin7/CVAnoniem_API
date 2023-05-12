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
    [Route("api/Message/[action]")]
    public class MessageController
    {
        public static MessageRepository MessageRepo = new MessageRepository();

        public static List<Message> messageCollection = new List<Message>();

        [HttpPost]
        [ActionName("AddMessage")]
        public void Add([FromBody] Message message)
        {
            messageCollection.Add(message);
            MessageRepo.Add(message);
        }

        [HttpDelete]
        [ActionName("DeleteMessage")]
        public void Delete(int id)
        {
            MessageRepo.Delete(id);
        }

        [HttpGet]
        [ActionName("GetMessageList")]
        public string GetMessageList()
        {
            messageCollection = MessageRepo.Get();
            var json = JsonSerializer.Serialize(messageCollection);
            return json;
        }

        [HttpGet]
        [ActionName("GetMessageByID")]
        public string GetMessageByID(int id)
        {
            var json = JsonSerializer.Serialize(MessageRepo.GetByID(id));
            return json;
        }
    }
}

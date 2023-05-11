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
    public class OfferController : ControllerBase
    {

        public static List<Offer> offers = new List<Offer>();

        [HttpGet]
        [ActionName("GetOffersList")]
        public string GetOffersList()
        {
            offers = QueryRepository.GetOffers();
            var json = JsonSerializer.Serialize(offers);
            return json;
        }

    }
}

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
    [Route("api/Offer/[action]")]
    public class OfferController
    {
        public static OfferRepository OfferRepo = new OfferRepository();

        public static List<Offer> offersCollection = new List<Offer>();

        [HttpPost]
        [ActionName("AddOffer")]
        public void Add([FromBody]Offer offer)
        {
            offersCollection.Add(offer);
            OfferRepo.Add(offer);
        }

        [HttpDelete]
        [ActionName("DeleteOffer")]
        public void Delete(int id)
        {
            OfferRepo.Delete(id);
        }

        [HttpPut]
        [ActionName("UpdateOffer")]
        public void Update(Offer updatedOffer, int id)
        {
            OfferRepo.Update(updatedOffer, id);
        }

        [HttpGet]
        [ActionName("GetOffersList")]
        public string GetOffersList()
        {
            offersCollection = OfferRepo.Get();
            var json = JsonSerializer.Serialize(offersCollection);
            return json;
        }

        [HttpGet]
        [ActionName("GetOfferByID")]
        public string GetOfferByID(int id) 
        {
            var json = JsonSerializer.Serialize(OfferRepo.GetByID(id));
            return json;
        }
 
    }
}

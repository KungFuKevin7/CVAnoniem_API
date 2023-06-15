using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using REST_API.Model;
using REST_API.Repository;
using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/[action]")]
    public class OfferController : ControllerBase
    {
        public static OfferRepository OfferRepo = new OfferRepository();

        public static List<Offer> offersCollection = new List<Offer>();

        [HttpPost]
        [ActionName("offer")]
        public HttpStatusCode Add([FromBody]Offer offer)
        {
            OfferRepo.Add(offer);
            return HttpStatusCode.OK;

        }

        [HttpDelete]
        [ActionName("offer")]
        public void Delete(int id)
        {
            OfferRepo.Delete(id);
        }

        [HttpPut]
        [ActionName("offer")]
        public void Update(Offer updatedOffer)
        {
            OfferRepo.Update(updatedOffer, updatedOffer.OfferID);
        }

        [HttpGet]
        [ActionName("offer/all-offers-list")]
        public string GetOffersList()
        {
            offersCollection = OfferRepo.Get();
            var json = JsonSerializer.Serialize(offersCollection);
            return json;
        }

        [HttpGet]
        [ActionName("offer")]
        public string GetOfferByID(int userid) 
        {
            var json = JsonSerializer.Serialize(OfferRepo.GetByID(userid));
            return json;
        }

        [HttpGet]
        [ActionName("offer/offer-by-id")]
        public string GetOfferByOfferID(int offerid)
        {
            var json = JsonSerializer.Serialize(OfferRepo.GetByOfferID(offerid));
            return json;
        }

        [HttpGet]
        [ActionName("offer/search-offers")]
        public string GetOfferByInput(string query)
        {
            var json = JsonSerializer.Serialize(OfferRepo.getOffersByName(query));

            return json;
        }


        [HttpGet]
        [ActionName("offer/user-has-offer")]
        public Task<int>
            GetUserHasOffer(int userid) 
        {
            return OfferRepo.UserHasOffer(userid);
        }

    }
}

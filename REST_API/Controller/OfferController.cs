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
            offersCollection.Add(offer);
            OfferRepo.Add(offer);
            Response.Headers.Add("Access-Control-Allow-Origin", "true");

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
        public void Update(Offer updatedOffer, int id)
        {
            OfferRepo.Update(updatedOffer, id);
        }

        [HttpGet]
        [ActionName("offer/all-offers-list")]
        public string GetOffersList()
        {
            offersCollection = OfferRepo.Get();
            var json = JsonSerializer.Serialize(offersCollection);
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return json;
        }

        [HttpGet]
        [ActionName("offer")]
        public string GetOfferByID(int userid) 
        {
            var json = JsonSerializer.Serialize(OfferRepo.GetByID(userid));
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return json;
        }

        [HttpGet]
        [ActionName("offer/user-has-offer")]
        public bool GetUserHasOffer(int userid) 
        {
            return OfferRepo.UserHasOffer(userid);
        }

    }
}

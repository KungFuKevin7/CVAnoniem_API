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
        public HttpStatusCode Add(IFormFile file, string offer)
        {
            //System.Console.WriteLine(offer);
            Offer offerRec = JsonSerializer.Deserialize<Offer>(offer);



            System.Console.WriteLine(offerRec.Province);
            System.Console.WriteLine(file.FileName);
            
            OfferRepo.AddOrUpdate(offerRec, file);
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
            OfferRepo.AddOrUpdate(updatedOffer);
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
        [ActionName("offer/search-offers")]
        public string GetOfferByInput(string query)
        {
            var json = JsonSerializer.Serialize(OfferRepo.getOffersByName(query));

            return json;
        }


        [HttpGet]
        [ActionName("offer/user-has-offer")]
        public int GetUserHasOffer(int userid) 
        {
            return OfferRepo.UserHasOffer(userid);
        }

    }
}

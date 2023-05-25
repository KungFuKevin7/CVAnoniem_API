using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using REST_API.Model;
using REST_API.Repository;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/[action]")]
    public class SavedOfferController
    {
        public static SavedOffersRepository SavedOfferRepo =
                                 new SavedOffersRepository();

        public static List<SavedOffers> savedOffers = 
                                        new List<SavedOffers>();

        [HttpPost]
        [ActionName("saved-offer")]
        public void Add([FromBody]SavedOffers offer)
        {
            savedOffers.Add(offer);
            SavedOfferRepo.Add(offer);
        }

        [HttpDelete]
        [ActionName("saved-offer")]
        public void Delete(int userid, int offerid)
        {
            int IDtoDelete = SavedOfferRepo.GetByID(userid, offerid);
            SavedOfferRepo.Delete(IDtoDelete);
        }


        [HttpGet]
        [ActionName("saved-offer/full-saved-offers-list")]
        public string GetSavedOffersList()
        {
            savedOffers = SavedOfferRepo.Get();
            var json = JsonSerializer.Serialize(savedOffers);
            return json;
        }

        [HttpGet]
        [ActionName("saved-offer")]
        public string GetOfferByID(int userid)
        {
            var json = JsonSerializer.Serialize(SavedOfferRepo.GetByID(userid));
            return json;
        }

        [HttpGet]
        [ActionName("saved-offer/user-saved-offer")]
        public string GetOfferByID(int userid, int offerid)
        {
            var json = JsonSerializer.Serialize(SavedOfferRepo.GetByID(userid,offerid));
            return json;
        }
    }
}


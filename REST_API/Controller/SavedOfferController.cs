using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using REST_API.Model;
using REST_API.Repository;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/SavedOffers/[action]")]
    public class SavedOfferController
    {
        public static SavedOffersRepository SavedOfferRepo =
                                 new SavedOffersRepository();

        public static List<SavedOffers> savedOffers = 
                                        new List<SavedOffers>();

        [HttpPost]
        [ActionName("AddSavedOffer")]
        public void Add([FromBody]SavedOffers offer)
        {
            savedOffers.Add(offer);
            SavedOfferRepo.Add(offer);
        }

        [HttpDelete]
        [ActionName("DeleteSavedOffer")]
        public void Delete(int id)
        {
            SavedOfferRepo.Delete(id);
        }


        [HttpGet]
        [ActionName("GetSavedOffers")]
        public string GetSavedOffersList()
        {
            savedOffers = SavedOfferRepo.Get();
            var json = JsonSerializer.Serialize(savedOffers);
            return json;
        }

        [HttpGet]
        [ActionName("GetSavedOfferByID")]
        public string GetOfferByID(int id)
        {
            var json = JsonSerializer.Serialize(SavedOfferRepo.GetByID(id));
            return json;
        }

    }
}


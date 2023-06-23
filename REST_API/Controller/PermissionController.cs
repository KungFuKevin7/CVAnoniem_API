﻿using Microsoft.AspNetCore.Mvc;
using REST_API.Model;
using REST_API.Repository;
using System.Text.Json;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/[action]")]
    public class PermissionController
    {
        public static PermissionRepository PermissionRepo = 
                                           new PermissionRepository();

        public static OfferRepository OfferRepo =
                                           new OfferRepository();
        
        public static List<Permission> Permissions =
                                           new List<Permission>();

        [HttpPost]
        [ActionName("permission")]
        public void Add([FromBody] Permission p, int senderid)
        {
            int offerid = OfferRepo.GetByID(senderid).ElementAt(0).OfferID;
            p.OfferID = offerid;
            //Permissions.Add(p);
            PermissionRepo.Add(p);
        }

        [HttpDelete]
        [ActionName("permission")]
        public void Delete(int id)
        {
            PermissionRepo.Delete(id);
        }

        [HttpPut]
        [ActionName("permission")]
        public void Update(Permission permission, int id)
        {
            PermissionRepo.Update(permission, id);
        }

        [HttpGet]
        [ActionName("permission/all-permissions")]
        public string GetPermissionsList()
        {
            Permissions = PermissionRepo.Get();
            var json = JsonSerializer.Serialize(Permissions);
            return json;
        }

        [HttpGet]
        [ActionName("permission")]
        public string GetPermissionsByID(int id)
        {
            var json = JsonSerializer.Serialize(PermissionRepo.GetByID(id));
            return json;
        }
    }
}

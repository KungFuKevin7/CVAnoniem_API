using Microsoft.AspNetCore.Mvc;
using REST_API.Model;
using REST_API.Repository;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/Permission/[action]")]
    public class PermissionController
    {
        public static PermissionRepository PermissionRepo = 
                                           new PermissionRepository();

        public static List<Permission> Permissions =
                                           new List<Permission>();

        [HttpPost]
        [ActionName("AddPermission")]
        public void Add([FromBody] Permission p)
        {
            Permissions.Add(p);
            PermissionRepo.Add(p);
        }

        [HttpDelete]
        [ActionName("RemovePermission")]
        public void Delete(int id)
        {
            PermissionRepo.Delete(id);
        }

        [HttpPut]
        [ActionName("UpdatePermission")]
        public void Update(Permission permission, int id)
        {
            PermissionRepo.Update(permission, id);
        }

        [HttpGet]
        [ActionName("GetAllPermissions")]
        public string GetPermissionsList()
        {
            Permissions = PermissionRepo.Get();
            var json = JsonSerializer.Serialize(Permissions);
            return json;
        }

        [HttpGet]
        [ActionName("GetPermissions")]
        public string GetPermissionsByID(int id)
        {
            var json = JsonSerializer.Serialize(PermissionRepo.GetByID(id));
            return json;
        }
    }
}

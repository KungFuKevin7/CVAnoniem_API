using System.ComponentModel;
using Newtonsoft.Json;

namespace REST_API.Model
{
    public class Permission
    {
        [Bindable(true)]
        public int PermissionID { get; set; }
        [Bindable(true)]
        public int ResumeID { get; set; }
        [Bindable(true)]
        public int EmployerID { get; set; }
        [Bindable(true)]
        public bool ShareFullResume { get; set; }

        [JsonConstructor]
        public Permission()
        {

        }
    }
}

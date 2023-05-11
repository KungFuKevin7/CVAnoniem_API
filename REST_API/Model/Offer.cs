using Newtonsoft.Json;
using System.ComponentModel;

namespace REST_API.Model
{
    public class Offer
    {
        [Bindable(true)]
        public int Id { get; set; }

        [Bindable(true)]
        public string WorkField { get; set; }

        [Bindable(true)]
        public string Description { get; set; }

        [Bindable(true)]
        public string Province { get; set; }

        [Bindable(true)]
        public bool JobSeekerID { get; set; }

        [JsonConstructor]
        public Offer() 
        {
        
        }
    }
}

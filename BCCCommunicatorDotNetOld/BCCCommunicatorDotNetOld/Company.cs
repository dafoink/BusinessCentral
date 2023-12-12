using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCCommunicator
{

    // this is just what we can deserialize what is returned from the /companies API call to make it easier to work with.
    public class CompanyCollection
    {
        public string odatacontext { get; set; }
        public List<Company> value { get; set; }
    }

    public class Company
    {
        public string id { get; set; }
        public string systemVersion { get; set; }
        public int timestamp { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string businessProfileId { get; set; }
        public DateTime systemCreatedAt { get; set; }
        public string systemCreatedBy { get; set; }
        public DateTime systemModifiedAt { get; set; }
        public string systemModifiedBy { get; set; }
    }

}

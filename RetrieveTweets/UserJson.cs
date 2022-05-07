using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RetrieveTweets
{
    class UserJson
    {
        [DataContract]
        public class Rootobject
        {
            [DataMember]
            public Data data { get; set; }
        }

        [DataContract]
        public class Data
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string username { get; set; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RetrieveTweets
{
    public class TweetJson
    {
        [DataContract]
        public class Rootobject
        {
            [DataMember]
            public Datum[] data { get; set; }
            [DataMember]
            public Meta meta { get; set; }
        }

        [DataContract]
        public class Meta
        {
            [DataMember]
            public string oldest_id { get; set; }
            [DataMember]
            public string newest_id { get; set; }
            [DataMember]
            public int result_count { get; set; }
            [DataMember]
            public string next_token { get; set; }
        }

        [DataContract]
        public class Datum
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string text { get; set; }
        }
    }
}

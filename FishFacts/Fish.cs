using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FishFacts {
    class Fish {

        private string fishName_;

        [JsonProperty("title")]
        public string FishName {
            get { return fishName_; }
            set {
                fishName_ = value;
            }
        }

        private string fishDesc_;

        [JsonProperty("extract")]
        public string FishDesc {
            get { return fishDesc_; }
            set {
                fishDesc_ = value;
            }
        }

        public Fish() {
            string ResponseText;
            HttpWebRequest wikiRequest = (HttpWebRequest)WebRequest.Create("https://en.wikipedia.org/api/rest_v1/page/summary/Fish");
            using (HttpWebResponse response = (HttpWebResponse)wikiRequest.GetResponse()) {
                using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                    ResponseText = reader.ReadToEnd();
                }
            }

            FishName = JObject.Parse(ResponseText)["title"].ToString();
            FishDesc = JObject.Parse(ResponseText)["extract"].ToString();


        }



    }
}

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

        private string imageLink_;
        public string ImageLink {
            get { return imageLink_; }
            set {
                imageLink_ = value;
            }
        }

        public Fish() {
            string fishName = GetRandomFish();
            string request = "https://en.wikipedia.org/api/rest_v1/page/summary/" + fishName;

            string ResponseText;
            HttpWebRequest wikiRequest = (HttpWebRequest)WebRequest.Create(request);
            using (HttpWebResponse response = (HttpWebResponse)wikiRequest.GetResponse()) {
                using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                    ResponseText = reader.ReadToEnd();
                }
            }

            FishName = JObject.Parse(ResponseText)["title"].ToString();
            FishDesc = JObject.Parse(ResponseText)["extract"].ToString();

            var obj = JObject.Parse(ResponseText)["originalimage"];
            if (obj != null) { 
                ImageLink = obj.SelectToken("source").ToString();
            }

        }

        public string GetRandomFish() {
            Random r = new Random();
            string[] lines = File.ReadAllLines("fish.txt");
            string fishName = lines[r.Next(lines.Length)];
            return fishName;
        }



    }
}

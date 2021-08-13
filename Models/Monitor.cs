using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiASP.Models
{
    public class Monitor
    {

        public Monitor()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Status { get; set; }

        public string Url { get; set; }

        static async ValueTask<string> GetRespAsc()
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("youtube.com");

            return httpResponseMessage.Content.ToString();
        }
    }
    
}

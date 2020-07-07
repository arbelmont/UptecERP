using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Definitiva.Shared.Infra.Support.Helpers;

namespace Uptec.Erp.Infra.HttpClient
{
    public class JsonContent : StringContent
    {
        public JsonContent(object value)
            : base(JsonConvert.SerializeObject(value).RemoveAccents(), Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(object value, string mediaType)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, mediaType)
        {
        }
    }
}

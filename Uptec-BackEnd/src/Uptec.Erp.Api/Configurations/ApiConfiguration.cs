using System.IO;
using Microsoft.Extensions.Configuration;

namespace Uptec.Erp.Api.Configurations
{
    public static class ApiConfiguration
    {
        private const string App_KEY = "e60564de-b530-4d24-9b43-74fe0db4d4af";

        public static string GetApiKey()
        {
            return App_KEY;
        }
    }
}
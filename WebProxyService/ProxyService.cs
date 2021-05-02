using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Runtime.Caching;

namespace WebProxyService
{
    public class ProxyService : IProxyService
    {
        HttpClient client = new HttpClient();
        ObjectCache cache = MemoryCache.Default;
        CacheItemPolicy defaultCacheItemPolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(2),
        };

        public ProxyService()
        {
            GetStationsOfContract("Lyon");
        }

        public string GetStationsOfContract(string name)
        {
            if (cache.Get(name) == null)
            {
                var response = client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + name + "&apiKey=1e38916a2fba5f218ec0c8f0053dac5aa8cd5567");
                response.Wait();
                cache.Add(name, response.Result, defaultCacheItemPolicy);
            }

            return (string)cache.Get(name);
        }


    }
}

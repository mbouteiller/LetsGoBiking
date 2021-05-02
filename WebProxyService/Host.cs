using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WebProxyService
{
    class Host
    {
        static void Main(string[] args)
        {

            // Create the ServiceHost.
            ServiceHost host = new ServiceHost(typeof(ProxyService));

            // Enable metadata publishing.

            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            host.Open();

            Console.WriteLine("The cache is ready");
            Console.WriteLine("Press <Enter> to stop the service.");
            Console.ReadLine();

            // Close the ServiceHost.
            host.Close();

        }
    }
}

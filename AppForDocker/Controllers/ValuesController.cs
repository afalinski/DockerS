using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;

namespace AppForDocker.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private  Random _random;
        private readonly object _syncObj = new object();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello form AppForDocker!" , $"IP - {GetLocalIpAddress()}", $"Random number - {GenerateRandomNumber()}", $"Time - {DateTime.UtcNow} " };
            
        }

        public string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        private int GenerateRandomNumber()
        {
            lock (_syncObj)
            {
                if (_random == null)
                    _random = new Random(); 
                return _random.Next();
            }
        }
    }
}

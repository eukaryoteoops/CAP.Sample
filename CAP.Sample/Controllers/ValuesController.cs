using System;
using System.Threading.Tasks;
using CAP.Sample.Services;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace CAP.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICapPublisher _capBus;

        public ValuesController(ICapPublisher capPublisher)
        {
            _capBus = capPublisher;
        }

        [HttpGet]
        public async Task Get()
        {
            await _capBus.PublishAsync("routekeyName", 5); //routekeyName, queue name define in startup

        }

        //subscribe in Controller
        [CapSubscribe("routekeyName")]
        public void CheckReceivedMessage(int integer)
        {
            Console.WriteLine($"FromController : {integer}");
        }

    }
}

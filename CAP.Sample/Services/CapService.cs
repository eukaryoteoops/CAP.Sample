using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace CAP.Sample.Services
{
    public class CapService : ICapService, ICapSubscribe
    {
        [CapSubscribe("routekeyName")]
        public void CheckReceivedMessage(string obj)
        {
            Console.WriteLine($"FromService : {obj}");
        }
    }

    public interface ICapService
    {
        void CheckReceivedMessage(string obj);
    }
}

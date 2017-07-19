using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeTofood.Services
{

    public interface IGreeter
    {
        string GetGreeting();

    }

    public class Greeter: IGreeter
    {
        private string _greeting;

        public Greeter(IConfiguration configuration)
        {
            _greeting = configuration["Greeting_JSON"];
        }  
        
        public string GetGreeting()
        {
            //return "Hello from Joao's greeter!";
            //throw new NotImplementedException();

            return _greeting;
        }
    }
}

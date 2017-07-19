using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeTofood.Controllers
{

    //attribute directly on a controller
    //for routing
    //[Route("about")]

    //using tokens, so if refactor or function gets renamed
    //I don't need to change the routing

    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        //blank url/about will go here because url/about/"blank" matches empty string below
        //[Route("")]
        //[Route("phone")]

         
        public string Phone()
        {

            return "1+555-555-5555";
        }

        //url/about/address
        //[Route("address")]

        public string Address()
        {

            return "USA";
        }
    }
}

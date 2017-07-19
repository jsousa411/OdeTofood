using Microsoft.AspNetCore.Mvc;
using OdeTofood.Services;
using OdeTofood.ViewModels;


namespace OdeTofood.Controllers
{
    public class HomeController: Controller
    {
        private IGreeter _greeter;
        private IRestaurantData _restaurantData;
        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {

            _restaurantData = restaurantData;
            _greeter = greeter;

        }


        //action result is the formal way to 
        //encapsulate the decision of a controller,
        //so the controller decides what to do next
        //you can return whatever type controller wants
        //json, xml, string....etc
        public IActionResult Index()
        {
            //this is a helper object
            //this.File()//


            //this.BadRequest();//return an object aht implements
            //IActionsResult that basically returns 
            //a 400 error back to the client
            //this.HttpContext .Response or .Headers this object allows me access to the response object
            //if I want to write into the response object
            //you want to avoid these httpContext bojects inside the controller if possible

            //after adding static data under services, I can just ask for a list of all restaurants
            //var model = new Restaurant { Id = 1, Name = "The House of Kobe" };

            //var model = _restaurantData.GetAll();


            var model = new HomePageViewModels();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetGreeting();

            //API responses friendly
            // return new ObjectResult(model);

            //produces a view result
            return View(model);

        }


        public IActionResult Details(int id)
        {

            var model = _restaurantData.Get(id);

            return View(model);
        }
    }


    //internal class HomePageViewModel
    //{
    //    public HomePageViewModel()
    //    {
    //    }

    //    public Restaurant Restaurants { get; internal set; }
    //}
}

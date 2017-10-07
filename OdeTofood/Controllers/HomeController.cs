using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeTofood.Entites;
using OdeTofood.Services;
using OdeTofood.ViewModels;


namespace OdeTofood.Controllers
{

    //Adding authentication restriction 08/12/2017 -Joao Sousa
    [Authorize]
    public class HomeController: Controller
    {
        private IGreeter _greeter;
        private IRestaurantData _restaurantData;
        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {

            _restaurantData = restaurantData;
            _greeter = greeter;

        }

        public IActionResult getImage()
        {
            PictureDisplay m = new PictureDisplay();

            return View(m.ImagePath);
            
        }

        //action result is the formal way to 
        //encapsulate the decision of a controller,
        //so the controller decides what to do next
        //you can return whatever type controller wants
        //json, xml, string....etc
        
        //allow anonymous use for specific actions
        [AllowAnonymous]
        public IActionResult Index()
        {           
              
            var model = new HomePageViewModels();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetGreeting();
            model.Image = "~/node_modules/images/Montenegro_1.jpg";
            //API responses friendly
            // return new ObjectResult(model);

            //produces a view result
            return View(model);

        }


        public IActionResult Details(int id)
        {



            var model = _restaurantData.Get(id);
              
            if(model == null)
            {

                return RedirectToAction("Index");
            }
            return View(model);
        }
  


        [HttpGet] //action to edit restaurant; only respond to http.Get
        public IActionResult Edit(int id)
        {

            var model = _restaurantData.Get(id);

            //if no model is available

            if (model == null)
            {

                return RedirectToAction("Index");

            }

            //return and edit view wtih the restaurant as a model
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RestaurantEditViewModel model)
        {
            //get the restaurant form database
            var restaurant = _restaurantData.Get(id);

            //ensure the restaurant is found i.e. not already delteted
            if (ModelState.IsValid)
            {

                restaurant.Cuisine = model.Cuisine;
                restaurant.Name = model.Name;
                _restaurantData.Commit();


                return RedirectToAction("Details", new { id = restaurant.Id });
         
            }

            return View(restaurant);

        }

        [HttpGet]//only responds to an httpGet request
        public IActionResult Create()
        {

            return View();
        }

        //only this Create method should receive the form post
        //but mvc does not know this, so we need to put
        //route constrains [HttpGet]

        [HttpPost]//only responds to httpPost 
        [ValidateAntiForgeryToken]//very important when authenticating users with cookies
                                   //also good to use when accepting posted form values
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Cuisine = model.Cuisine;
                newRestaurant.Name = model.Name;//validation of view model will be executed at this tpoint



                newRestaurant = _restaurantData.Add(newRestaurant);

                //after adding restaurant, commit the data
                _restaurantData.Commit();

                //redirect upon post of form to avoid duplicate recreation of data on refresh
                //pass second parameter for routing
                return RedirectToAction("Details", new { id = newRestaurant.Id });
                //View("Details", newRestaurant);
            }

            //redisplay view for user to fix the input
            return View();//if user forgot to input a field or inputted an invalid data 
                          //redisplay the view again with the data already inputted for a resave
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

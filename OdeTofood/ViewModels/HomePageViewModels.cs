using OdeTofood.Entites;
using System.Collections.Generic;


namespace OdeTofood.ViewModels
{
    public class HomePageViewModels
    {

       public string CurrentMessage { get; set; }
       public IEnumerable<Restaurant> Restaurants { get; set; }
       public string Image { get; set; }

    }
}

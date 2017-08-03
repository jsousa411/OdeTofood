using OdeTofood.Entites;
using System.ComponentModel.DataAnnotations;

namespace OdeTofood.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }

    }
}

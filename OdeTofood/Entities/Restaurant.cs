using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OdeTofood.Entites
{ 

    /// <summary>
    /// Any changes to this  file
    /// run: add-migration "name"
    /// </summary>

    public enum CuisineType
    {

        None,
        Italian,
        French,
        Japanese,
        American
    }
    public class Restaurant
    {

        public int Id { get; set; }

       // [DataType(DataType.Password)] //modifies text field input to be hidden
       [Required, MaxLength(80)]
        [Display(Name="Restaurant Name")]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    
    }
}

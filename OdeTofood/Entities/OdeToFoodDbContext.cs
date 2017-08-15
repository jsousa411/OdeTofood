using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OdeTofood.Entites;
using System.Linq;

namespace OdeTofood.Entities
{
    //indicate to the entity framework the type of data to store by passing 
    //in User to the identityDbContext
    public class OdeToFoodDbContext: IdentityDbContext<User>
    {
        public OdeToFoodDbContext(DbContextOptions options): base(options)
        {
            //use this to query the database directly
            //this.Users.Where<>
            //it's better to use through other services proviced 
            //by framework



        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }

    
}

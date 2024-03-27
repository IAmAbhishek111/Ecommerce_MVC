using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // if we have to do any registraion we will do in program.cs 


        }

        /*Adding table in database using EF core*/

        public DbSet<Category> Categories { get; set; }


    }
}

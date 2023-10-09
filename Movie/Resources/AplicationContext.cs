using Microsoft.EntityFrameworkCore;
using Movie.Models;

namespace Movie.Resources
{
  
        public class ApplicationContext : DbContext
        {
            public DbSet<FileImage> Files { get; set; }
            public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
            {
                Database.EnsureCreated();
            }
        }
   
}

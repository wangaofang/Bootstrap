using Microsoft.EntityFrameworkCore;

namespace Bootstrap.Entities
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
           : base(options)
        {
            // Database.EnsureCreated();
            Database.Migrate();   
               
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials{get;set;}

        //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("xxxx connection string");            
        //     base.OnConfiguring(optionsBuilder);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
        }
    }


}
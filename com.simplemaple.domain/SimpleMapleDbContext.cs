using com.simplemaple.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace com.simplemaple.domain
{
    public class SimpleMapleDbContext : DbContext
    {
        public SimpleMapleDbContext(DbContextOptions<SimpleMapleDbContext> options) : base(options) { }
        public SimpleMapleDbContext() { }

        public virtual DbSet<UserEntity> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                var configuration = builder.Build();
                string connectionString = configuration.GetConnectionString("connectStr");
                optionsBuilder.UseMySql(connectionString);
            }
        }
    }
}

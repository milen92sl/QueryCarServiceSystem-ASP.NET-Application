namespace QueryServiceSystem2.Data
{
    using QueryServiceSystem2.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class QueryService2DbContext : IdentityDbContext<User>
    {
        public QueryService2DbContext(DbContextOptions<QueryService2DbContext> options)
                    : base(options)
        {
        }

        public DbSet<Query> Queries { get; init; }

        public DbSet<Car> Cars { get; init; }

        public DbSet<Mechanic> Mechanics { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Query>()
                .HasOne(c => c.Car)
                .WithMany(c => c.Queries)
                .HasForeignKey(c => c.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Query>()
                .HasOne(c => c.Mechanic)
                .WithMany(d => d.Queries)
                .HasForeignKey(c => c.MechanicId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Mechanic>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Mechanic>(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(builder);
        }
    }
}

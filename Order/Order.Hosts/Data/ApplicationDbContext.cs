using Order.Hosts.Data.Entities;
using Order.Hosts.Data.EntityConfigurations;

namespace Order.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<OrderItemEntity> OrderItems { get; set; } = null!;
    public DbSet<OrderOrderEntity> OrderOrders { get; set; } = null!;
    public DbSet<OrderUserEntity> OrderUsers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
        builder.ApplyConfiguration(new OrderOrderEntityTypeConfiguration());
        builder.ApplyConfiguration(new OrderUserEntityTypeConfiguration());
    }
}

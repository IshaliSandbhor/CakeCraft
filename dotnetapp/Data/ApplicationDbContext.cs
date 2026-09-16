using dotnetapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
	public DbSet<Cake> Cakes => Set<Cake>();
	public new DbSet<User> Users => Set<User>();
	public DbSet<ErrorLog> ErrorLogs => Set<ErrorLog>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Cake>().Property(cake => cake.Price).HasPrecision(18, 2);
		modelBuilder.Entity<Cake>().Property(cake => cake.Quantity).HasPrecision(18, 2);
	}
}

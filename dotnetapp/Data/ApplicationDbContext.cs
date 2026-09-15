using dotnetapp.Models; using Microsoft.AspNetCore.Identity.EntityFrameworkCore; using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):IdentityDbContext<ApplicationUser>(options) { public DbSet<Cake> Cakes=>Set<Cake>(); public DbSet<User> Users=>Set<User>(); public DbSet<ErrorLog> ErrorLogs=>Set<ErrorLog>(); }

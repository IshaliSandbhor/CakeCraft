using dotnetapp.Data; using dotnetapp.Models; using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Services;
public class CakeService(ApplicationDbContext context):ICakeService {
 public async Task<IEnumerable<Cake>> GetAllCakes()=>await context.Cakes.AsNoTracking().ToListAsync();
 public async Task<Cake?> GetCakeById(int id)=>await context.Cakes.FindAsync(id);
 public async Task<bool> AddCake(Cake cake){ if(await context.Cakes.AnyAsync(x=>x.Name==cake.Name)) return false; cake.CakeId=0; context.Cakes.Add(cake); await context.SaveChangesAsync(); return true; }
 public async Task<bool> UpdateCake(int id,Cake cake){ var current=await context.Cakes.FindAsync(id); if(current is null)return false; current.Name=cake.Name;current.Category=cake.Category;current.Price=cake.Price;current.Quantity=cake.Quantity;current.CakeImage=cake.CakeImage;await context.SaveChangesAsync();return true; }
 public async Task<bool> DeleteCake(int id){var cake=await context.Cakes.FindAsync(id);if(cake is null)return false;context.Cakes.Remove(cake);await context.SaveChangesAsync();return true;}
}

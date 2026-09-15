using dotnetapp.Models;
namespace dotnetapp.Services;
public interface ICakeService { Task<IEnumerable<Cake>> GetAllCakes(); Task<Cake?> GetCakeById(int id); Task<bool> AddCake(Cake cake); Task<bool> UpdateCake(int id,Cake cake); Task<bool> DeleteCake(int id); }

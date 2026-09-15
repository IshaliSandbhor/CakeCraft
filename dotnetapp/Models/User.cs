using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models;
public class User { public int UserId {get;set;} [Required,EmailAddress] public string Email {get;set;}=""; [Required,MinLength(6)] public string Password {get;set;}=""; [Required,MaxLength(30)] public string Username {get;set;}=""; [Required,RegularExpression(@"^[0-9]{10}$")] public string MobileNumber {get;set;}=""; [Required] public string UserRole {get;set;}=UserRoles.Customer; }

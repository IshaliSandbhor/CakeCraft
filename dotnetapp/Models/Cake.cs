using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models;
public class Cake { public int CakeId {get;set;} [Required,MaxLength(100)] public string Name {get;set;}=""; [Required] public string Category {get;set;}=""; [Range(0.01,double.MaxValue)] public decimal Price {get;set;} [Range(0.01,double.MaxValue)] public decimal Quantity {get;set;} public string CakeImage {get;set;}=""; }

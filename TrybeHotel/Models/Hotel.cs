namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Hotel {
  [Key]
    public int HotelId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int CityId { get; set; }
    public City? City { get; set; }
    public virtual ICollection<Room>? Rooms { get; set; } 
}
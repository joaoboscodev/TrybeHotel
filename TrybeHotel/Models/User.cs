namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User {
  [Key]
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? UserType { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; } // Tornando a propriedade anulável
}
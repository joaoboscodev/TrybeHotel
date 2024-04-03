namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Room {
    [Key]
    public int RoomId { get; set; }

    public string? Name { get; set; } // Tornando a propriedade anulável

    public int Capacity { get; set; }

    public string? Image { get; set; } // Tornando a propriedade anulável

    [ForeignKey("Hotel")]
    public int HotelId { get; set; }

    public Hotel? Hotel { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; }
}
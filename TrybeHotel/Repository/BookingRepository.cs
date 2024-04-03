using TrybeHotel.Models;
using TrybeHotel.Dto;
using System.Globalization;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 9. Refatore o endpoint POST /booking
        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
           if (!DateTime.TryParseExact(booking.CheckIn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkIn) ||
            !DateTime.TryParseExact(booking.CheckOut, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkOut))  {
                throw new ArgumentException("Invalid date format");
            }

            var findUser = _context.Users.First(user => user.Email == email);

            var newBooking = new Booking {
                CheckIn = checkIn,
                CheckOut = checkOut,
                GuestQuant = booking.GuestQuant,
                UserId = findUser.UserId,
                RoomId = booking.RoomId,
            };

            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            var findBooking = _context.Bookings.First(b => b.RoomId == booking.RoomId);
            var findRoom = _context.Rooms.First(r => r.RoomId == findBooking.RoomId);
            var findHotel = _context.Hotels.First(h => h.HotelId == findRoom.HotelId);
            var findCity = _context.Cities.First(c => c.CityId == findHotel.CityId);
                        
            return new BookingResponse {
                BookingId = newBooking.BookingId,
                CheckIn = findBooking.CheckIn.ToString("yyyy-MM-dd"),
                CheckOut = findBooking.CheckOut.ToString("yyyy-MM-dd"),
                GuestQuant = findBooking.GuestQuant,
                Room = new RoomDto {
                    RoomId = findRoom.RoomId,
                    Name = findRoom.Name,
                    Capacity = findRoom.Capacity,
                    Image = findRoom.Image,
                    Hotel = new HotelDto {
                        HotelId = findHotel.HotelId,
                        Name = findHotel.Name,
                        Address = findHotel.Address,
                        CityId = findHotel.CityId,
                        CityName = findCity.Name,
                        State = findCity.State
                    }
                }
            };
        }

        // 10. Refatore o endpoint GET /booking
        public BookingResponse GetBooking(int bookingId, string email)
        {
            var findUser = _context.Users.First(u => u.Email == email);
            var findBooking = _context.Bookings.First(b => b.BookingId == bookingId);

            if (findUser == null || findBooking == null || findUser.UserId != findBooking.UserId) {
                throw new Exception("User or booking not found, or user is not associated with the booking.");
            }

            var findRoom = _context.Rooms.First(r => r.RoomId == findBooking.RoomId);
            var findHotel = _context.Hotels.First(h => h.HotelId == findRoom.HotelId);
            var findCity = _context.Cities.First(c => c.CityId == findHotel.CityId);

            return new BookingResponse {
                BookingId = findBooking.BookingId,
                CheckIn = findBooking.CheckIn.ToString("yyyy-MM-dd"),
                CheckOut = findBooking.CheckOut.ToString("yyyy-MM-dd"),
                GuestQuant = findBooking.GuestQuant,
                Room = new RoomDto {
                    RoomId = findRoom.RoomId,
                    Name = findRoom.Name,
                    Capacity = findRoom.Capacity,
                    Image = findRoom.Image,
                    Hotel = new HotelDto {
                        HotelId = findHotel.HotelId,
                        Name = findHotel.Name,
                        Address = findHotel.Address,
                        CityId = findHotel.CityId,
                        CityName = findCity.Name,
                        State = findCity.State
                    }
                }
            };
        }

        public Room GetRoomById(int RoomId)
        {
             throw new NotImplementedException();
        }

    }

}
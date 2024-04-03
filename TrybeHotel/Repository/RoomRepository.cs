using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 7. Refatore o endpoint GET /room
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
           var roomDtos = from room in _context.Rooms
               join hotel in _context.Hotels
               on room.HotelId equals hotel.HotelId
               where hotel.HotelId == HotelId
               select new RoomDto {
                   RoomId = room.RoomId,
                   Name = room.Name,
                   Capacity = room.Capacity,
                   Image = room.Image,
                   Hotel = new HotelDto {
                       HotelId = hotel.HotelId,
                       Name = hotel.Name,
                       Address = hotel.Address,
                       CityId = hotel.CityId,
                       CityName = hotel.City != null ? hotel.City.Name : null,
                       State = hotel.City != null ? hotel.City.State : null
                   }
               };

            return roomDtos.ToList();
        }

        // 8. Refatore o endpoint POST /room
        public RoomDto AddRoom(Room room) {
            _context.Rooms.Add(room);
			_context.SaveChanges();

            var selectedRoom = _context.Rooms
                .Where(r => r.HotelId == room.HotelId && r.Name == room.Name)
                .Select(r => new RoomDto {
                    RoomId = r.RoomId,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Image = r.Image,
                    Hotel = r.Hotel != null ? new HotelDto {
                        HotelId = r.HotelId,
                        Name = r.Hotel.Name,
                        Address = r.Hotel.Address,
                        CityId = r.Hotel.CityId,
                        CityName = r.Hotel.City != null ? r.Hotel.City.Name : null,
                        State =  r.Hotel.City != null ? r.Hotel.City.State : null
                    } : null
                })
                .FirstOrDefault() ?? new RoomDto();

            return selectedRoom;
        }

        public void DeleteRoom(int RoomId) {
           throw new NotImplementedException();
        }
    }
}
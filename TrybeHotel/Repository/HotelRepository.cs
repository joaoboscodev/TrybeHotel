using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        //  5. Refatore o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            return _context.Hotels
                .Join(_context.Cities,
                    hotel => hotel.CityId,
                    city => city.CityId,
                    (hotel, city) => new HotelDto {
                        HotelId = hotel.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = hotel.CityId,
                        CityName = city.Name,
                        State = city.State
                    })
                .ToList();
        }

        // 6. Refatore o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
           _context.Hotels.Add(hotel);
			_context.SaveChanges();

			var cityName = _context.Cities.Find(hotel.CityId);

            if (cityName == null)
                throw new ArgumentNullException();

			return new HotelDto
			{
				HotelId = hotel.HotelId,
				Name = hotel.Name,
				Address = hotel.Address,
				CityId = hotel.CityId,
				CityName = cityName.Name,
                State = cityName.State
            };
        }
    }
}
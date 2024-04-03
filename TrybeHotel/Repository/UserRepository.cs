using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var postUser = _context.Users.First(u => u.Email == login.Email);
            
            if (postUser == null || postUser.Password != login.Password) {
                throw new Exception("Incorrect e-mail or password");
            }
            return new UserDto {
                UserId = postUser.UserId,
                Name = postUser.Name,
                Email = postUser.Email,
                UserType = postUser.UserType
            }; 
        }
        public UserDto Add(UserDtoInsert user)
        {
            if (_context.Users.Any(u => u.Email == user.Email)) {
                throw new Exception("User email already exists");
            }

            var postUser = new User {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            };

            _context.Users.Add(postUser);
            _context.SaveChanges();
            
            var resultUser = new UserDto { 
                UserId = postUser.UserId,
                Name = postUser.Name,
                Email = postUser.Email,
                UserType = postUser.UserType,
            };
            return resultUser;
        }

        public UserDto GetUserByEmail(string userEmail)
        {
             throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var userDtos = _context.Users
                .Select(user => new UserDto {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    UserType = user.UserType
                });

            return userDtos;
        }

    }
}
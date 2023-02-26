using ChatAPI.DTOs.Requests;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services.Implementation
{
    public class UserService : IUserService
    {
        private const double CACHE_EXPIRATION_MINUTES = 10;

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICache<string, User> _cachedUsersRepository;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ICache<string, User> cachedUsersRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _cachedUsersRepository = cachedUsersRepository;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            // Поиск в Кэше
            if (_cachedUsersRepository.TryGetValue(username, out User? user))
            {
                return user;
            }

            // Если в кэше не оказалось, то поиск в БД
            user = await _userRepository.GetByUsername(username);

            if (user == null)
            {
                throw new EntityNotFoundException("User not found");
            }

            // Добавление в кэш
            _cachedUsersRepository.Set
            (
                user.Username, 
                user,
                TimeSpan.FromMinutes(CACHE_EXPIRATION_MINUTES)
            );

            return user;
        }

        public async Task RegisterUser(RegisterRequestDTO registerRequest)
        {
            string passwordHash = _passwordHasher.HashPassword(registerRequest.Password);

            var user = new User()
            {
                Username = registerRequest.Username,
                PasswordHash = passwordHash
            };

            await _userRepository.Create(user);

            // Добавление в кэш
            _cachedUsersRepository.Set
            (
                user.Username,
                user,
                TimeSpan.FromMinutes(CACHE_EXPIRATION_MINUTES)
            );
        }

        public async Task DeleteUser(User user)
        {
            await _userRepository.Delete(user);

            _cachedUsersRepository.Remove(user.Username);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.Update(user);

            _cachedUsersRepository.Set
            (
                user.Username,
                user,
                TimeSpan.FromMinutes(CACHE_EXPIRATION_MINUTES)
            );
        }

        public async Task<bool> IsUserExists(string username)
        {
            if (_cachedUsersRepository.TryGetValue(username, out _)) 
                return true;

            var user = await _userRepository.GetByUsername(username);

            return user != null;
        }
    }
}

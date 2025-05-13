using AutoMapper;
using TMA.Application.DTOs.Authentication;
using TMA.Domain.Entities;
using TMA.Domain.Interfaces.Repositories;
using TMA.Domain.Interfaces.Services;

namespace TMA.Application.Services.Implementations
{
    public class UsersService(IUsersRepository usersRepository, IMapper mapper) : IUsersService
    {
        public async Task Add(UserDto user)
        {
            var hashedPassword = PasswordHasher.HashPassword(user.Password, out byte[] salt);
            user.Password = $"{hashedPassword}:{Convert.ToBase64String(salt)}";
            await usersRepository.AddAsync(mapper.Map<UserEntity>(user));
        }

        public async Task Delete(Guid id) => await usersRepository.DeleteAsync(id);

        public async Task<IEnumerable<UserDto>> GetAll() =>
            mapper.Map<IEnumerable<UserDto>>(await usersRepository.GetAllAsync());

        public async Task<UserDto?> GetByEmail(string email) =>
            mapper.Map<UserDto?>(await usersRepository.GetByEmail(email));

        public async Task<UserDto?> GetByEmailAndPassword(string email, string password)
        {
            var user = await usersRepository.GetByEmail(email);

            if (user == null)
                return null;

            string[] stringparts = user!.Password.Split(':');
            var bytesalt = Convert.FromBase64String(stringparts[1]);

            var isVerified = PasswordHasher.VerifyPassword(password, stringparts[0], bytesalt);
            return isVerified ? mapper.Map<UserDto?>(user) : null;
        }


        public async Task<UserDto?> GetById(Guid id) =>
            mapper.Map<UserDto?>(await usersRepository.GetByIdAsync(id));

        public async Task Update(UserDto user) => await usersRepository.UpdateAsync(mapper.Map<UserEntity>(user));
    }
}

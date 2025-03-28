using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s.User;
using RentCar.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RentCar.Services
{
    public class AuthorizationService : IAuthorization
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthorizationService(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<int>> RegisterAsync(UserRegisterDTO dto)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExists(dto.Email))
            {
                response.StatusCode = HttpStatusCode.Conflict;
                response.Message = "User with this email already exists";
                return response;
            }

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            response.Message = "User registered successfully";
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        public async Task<ServiceResponse<string>> LoginAsync(UserLoginDTO dto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            User user = await _context.users.FirstOrDefaultAsync(x => x.Email.ToLower() == dto.Email.ToLower());

            if (user is null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "User not found";
                return response;
            }
            else if (!VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.StatusCode= HttpStatusCode.Unauthorized;
                response.Message = "Password is incorrect";
                return response;
            }
            else
            {
                var result = GenerateTokens(user, dto.StaySignedIn);
                response.Data = result.AccessToken;
                response.StatusCode = HttpStatusCode.OK;
            }

            if(dto.StaySignedIn)
            {
                _context.users.Update(user);
                await _context.SaveChangesAsync();
            }

            return response;
        }

        #region private methods

        private async Task<bool> UserExists(string email)
        {
            if (await _context.users.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
                return true;
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(storedHash);
            }
        }

        private TokenDTO GenerateTokens(User user, bool staySignedIn)
        {
            string refreshToken = string.Empty;

            if (staySignedIn)
            {
                refreshToken = GenerateRefreshToken(user);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpirationDate = DateTime.Now.AddDays(2);
            }
            var accessToken = GenerateAccessToken(user);

            return new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        private string GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTOptions:Secret").Value ?? string.Empty));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
                Issuer = _configuration.GetSection("JWTOptions:Issuer").Value,
                Audience = _configuration.GetSection("JWTOptions:Audience").Value
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private string GenerateRefreshToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTOptions:Secret").Value ?? string.Empty));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = credentials,
                Issuer = _configuration.GetSection("JWTOptions:Issuer").Value,
                Audience = _configuration.GetSection("JWTOptions:Audience").Value
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        #endregion
    }
}

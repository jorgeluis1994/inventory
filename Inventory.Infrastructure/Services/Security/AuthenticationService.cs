using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventory.Application.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Inventory.Infrastructure.Security
{
    /// <summary>
    /// Implementación del servicio de autenticación usando PasswordHasher y JWT.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly PasswordHasher<object> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _passwordHasher = new PasswordHasher<object>();
        }

        /// <inheritdoc />
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            return _passwordHasher.HashPassword(null, password);
        }

        /// <inheritdoc />
        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Hashed password cannot be empty.", nameof(hashedPassword));
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentException("Plain password cannot be empty.", nameof(plainPassword));

            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }

        /// <inheritdoc />
        public string GenerateToken(string userId, string userName, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName cannot be empty.", nameof(userName));
            if (expirationDate <= DateTime.UtcNow)
                throw new ArgumentException("Expiration date must be in the future.", nameof(expirationDate));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var keyString = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("JWT key is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expirationDate,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var _secretKey = _configuration["Jwt:Key"];
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],

                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

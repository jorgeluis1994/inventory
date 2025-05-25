using System;

namespace Inventory.Application.Security
{
    /// <summary>
    /// Proporciona métodos para hashear contraseñas, verificar contraseñas y generar tokens de autenticación.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Hashea la contraseña en texto plano.
        /// </summary>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>Contraseña hasheada.</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifica si la contraseña en texto plano coincide con el hash dado.
        /// </summary>
        /// <param name="hashedPassword">Contraseña hasheada.</param>
        /// <param name="plainPassword">Contraseña en texto plano.</param>
        /// <returns>True si coinciden, false en caso contrario.</returns>
        bool VerifyPassword(string hashedPassword, string plainPassword);

        /// <summary>
        /// Genera un token de autenticación para el usuario.
        /// </summary>
        /// <param name="userId">Id del usuario.</param>
        /// <param name="userName">Nombre de usuario.</param>
        /// <param name="role">Rol del usuario.</param>
        /// <param name="expirationDate">Fecha de expiración del token.</param>
        /// <returns>Token JWT (o similar) como string.</returns>
        string GenerateToken(string userId, string userName, DateTime expirationDate);
    }
}

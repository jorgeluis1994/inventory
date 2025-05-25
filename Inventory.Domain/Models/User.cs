using System;

namespace Inventory.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        private string _userName;
        public string UserName => _userName;

        private string _password;
        public string Password => _password;

        private string _email;
        public string Email => _email;

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private User() { }

        public User(string userName, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName is required.", nameof(userName));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.", nameof(password));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format.", nameof(email));

            _userName = userName;
            _password = password; // recuerda usar hash en producción
            _email = email;

            var nowUtc = DateTime.UtcNow;
            CreatedAt = nowUtc;
            UpdatedAt = nowUtc;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void UpdatePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("New password cannot be empty.", nameof(newPassword));

            _password = newPassword; // hash recomendado
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be empty.", nameof(newEmail));

            if (!IsValidEmail(newEmail))
                throw new ArgumentException("Invalid email format.", nameof(newEmail));

            _email = newEmail;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
